using ETicaretApi.Dto.Dtos.AdminProductDtos;
using ETicaretApi.Dto.Dtos.AiDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ETicaretApi.WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AiAssistantController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public AiAssistantController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [HttpPost("Ask")]
        public async Task<IActionResult> Ask([FromBody] AiPromptRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request?.Message))
            {
                return Ok(new AiChatResponseDto { Reply = "Lütfen bir soru veya aradığınız ürünü yazın." });
            }

            var client = _httpClientFactory.CreateClient();

            // 1. Veritabanındaki Güncel Ürünleri Çek
            List<AdminResultProductDto> allProducts = new List<AdminResultProductDto>();
            try
            {
                var prodResponse = await client.GetAsync("https://localhost:7035/api/Products");
                if (prodResponse.IsSuccessStatusCode)
                {
                    var json = await prodResponse.Content.ReadAsStringAsync();
                    allProducts = JsonConvert.DeserializeObject<List<AdminResultProductDto>>(json) ?? new List<AdminResultProductDto>();
                }
            }
            catch { }

            // 2. Katalog Özetini Hazırla
            var catalogText = new StringBuilder();
            foreach (var p in allProducts)
            {
                catalogText.AppendLine($"- Ürün Adı: {p.ProductName} | Fiyat: {p.ProductPrice:N2} TL | Kategori: {p.CategoryName}");
            }

            string apiKey = Environment.GetEnvironmentVariable("GEMINI_API_KEY")
                ?? _configuration["Gemini:ApiKey"]
                ?? string.Empty;

            // 3. Canlı Google Gemini LLM API Çağrısı
            if (!string.IsNullOrWhiteSpace(apiKey) && apiKey != "YOUR_GEMINI_API_KEY_HERE")
            {
                var supportedModels = new[] { "gemini-3.5-flash", "gemma-4-31b-it", "gemini-3.1-flash-lite" };

                var systemPrompt = $"Sen ETicaret mağazasının samimi, yardımsever ve son derece zeki resmi Yapay Zeka (AI) Alışveriş Danışmanısın. " +
                    $"Google Gemini altyapısıyla çalışıyorsun.\n\n" +
                    $"Aşağıda mağazamızda şu an satışta olan TÜM GÜNCEL ÜRÜNLER ve FİYATLARI yer almaktadır:\n\n" +
                    $"{catalogText}\n\n" +
                    $"TALİMATLAR:\n" +
                    $"1. Kullanıcı seninle sohbet ederse (örn: 'nasılsın', 'sen kimsin', 'yapay zeka mısın'), samimi ve doğal bir şekilde Türkçe cevap ver, kendini tanıt.\n" +
                    $"2. Kullanıcı ürün, öneri, bütçe veya hediye tavsiyesi sorduğunda MUTLAKA yukarıdaki mağaza kataloğumuzdaki ürünlerden isim ve TL fiyat vererek öner.\n" +
                    $"3. Emojiler kullanarak enerjik ve ilgi çekici bir üslup takın.\n\n" +
                    $"Kullanıcının Mesajı: \"{request.Message}\"";

                var geminiRequestBody = new
                {
                    contents = new[]
                    {
                        new
                        {
                            parts = new[]
                            {
                                new { text = systemPrompt }
                            }
                        }
                    }
                };

                var geminiContent = new StringContent(JsonConvert.SerializeObject(geminiRequestBody), Encoding.UTF8, "application/json");

                foreach (var modelName in supportedModels)
                {
                    try
                    {
                        var geminiResponse = await client.PostAsync($"https://generativelanguage.googleapis.com/v1beta/models/{modelName}:generateContent?key={apiKey}", geminiContent);

                        if (geminiResponse.IsSuccessStatusCode)
                        {
                            var geminiResultJson = await geminiResponse.Content.ReadAsStringAsync();
                            dynamic? resultObj = JsonConvert.DeserializeObject(geminiResultJson);
                            string replyText = resultObj?.candidates?[0]?.content?.parts?[0]?.text ?? string.Empty;

                            if (!string.IsNullOrWhiteSpace(replyText))
                            {
                                var recommendedCards = FindMatchingProducts(replyText, request.Message, allProducts);

                                return Ok(new AiChatResponseDto
                                {
                                    Reply = replyText,
                                    RecommendedProducts = recommendedCards
                                });
                            }
                        }
                    }
                    catch { }
                }
            }

            // 4. Fallback Eşleme (İnternet/API erişiminde geçici kopma olursa)
            var (fallbackReply, fallbackCards) = GenerateSmartFallbackResponse(request.Message, allProducts);
            return Ok(new AiChatResponseDto
            {
                Reply = fallbackReply,
                RecommendedProducts = fallbackCards
            });
        }

        private List<AiProductRecommendationDto> FindMatchingProducts(string replyText, string userMessage, List<AdminResultProductDto> allProducts)
        {
            var matched = new List<AiProductRecommendationDto>();
            var combined = $"{replyText} {userMessage}".ToLower();

            foreach (var p in allProducts)
            {
                var pNameLower = p.ProductName.ToLower();
                var words = pNameLower.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (combined.Contains(pNameLower) || words.Any(w => w.Length > 3 && combined.Contains(w)))
                {
                    if (!matched.Any(m => m.ProductID == p.ProductID))
                    {
                        matched.Add(new AiProductRecommendationDto
                        {
                            ProductID = p.ProductID,
                            ProductName = p.ProductName,
                            Price = p.ProductPrice,
                            ImageUrl = p.ProductImageURL
                        });
                    }
                }
            }

            return matched.Take(4).ToList();
        }

        private (string reply, List<AiProductRecommendationDto> cards) GenerateSmartFallbackResponse(string message, List<AdminResultProductDto> products)
        {
            var msg = message.ToLower();
            var matchedProducts = new List<AdminResultProductDto>();

            // 1. Bütçe rakamı kontrolü (örn: 5000, 1000)
            var numberMatch = Regex.Match(msg, @"\d+([.,]\d+)?");
            decimal budget = 0;
            if (numberMatch.Success && decimal.TryParse(numberMatch.Value.Replace(".", "").Replace(",", "."), out budget) && budget > 50)
            {
                matchedProducts = products.Where(p => p.ProductPrice <= budget).OrderByDescending(p => p.ProductPrice).Take(3).ToList();
                if (matchedProducts.Any())
                {
                    var sb = new StringBuilder();
                    sb.AppendLine($"✨ Harika! **₺{budget:N0}** bütçeniz için mağazamızdaki en ideal ürünleri buldum:");
                    foreach (var p in matchedProducts)
                    {
                        sb.AppendLine($"• **{p.ProductName}** - ₺{p.ProductPrice:N2}");
                    }
                    sb.AppendLine("\nAşağıdaki kartlardan ürünleri doğrudan inceleyebilir veya sepetinize ekleyebilirsiniz! 🛍️");

                    var cards = matchedProducts.Select(p => new AiProductRecommendationDto
                    {
                        ProductID = p.ProductID,
                        ProductName = p.ProductName,
                        Price = p.ProductPrice,
                        ImageUrl = p.ProductImageURL
                    }).ToList();

                    return (sb.ToString(), cards);
                }
            }

            // 2. Kategori veya anahtar kelime eşleme
            if (msg.Contains("kulaklık") || msg.Contains("oyun") || msg.Contains("gaming") || msg.Contains("ses"))
            {
                matchedProducts = products.Where(p => p.ProductName.ToLower().Contains("kulaklık") || p.ProductName.ToLower().Contains("cloud") || p.ProductName.ToLower().Contains("wireless")).ToList();
            }
            else if (msg.Contains("ayakkabı") || msg.Contains("spor") || msg.Contains("koşu") || msg.Contains("nike"))
            {
                matchedProducts = products.Where(p => p.ProductName.ToLower().Contains("ayakkabı") || p.ProductName.ToLower().Contains("nike") || p.ProductName.ToLower().Contains("max")).ToList();
            }
            else if (msg.Contains("parfüm") || msg.Contains("koku") || msg.Contains("chanel"))
            {
                matchedProducts = products.Where(p => p.ProductName.ToLower().Contains("parfüm") || p.ProductName.ToLower().Contains("chanel") || p.ProductName.ToLower().Contains("edp")).ToList();
            }
            else if (msg.Contains("saat") || msg.Contains("watch"))
            {
                matchedProducts = products.Where(p => p.ProductName.ToLower().Contains("saat") || p.ProductName.ToLower().Contains("watch")).ToList();
            }
            else if (msg.Contains("airfryer") || msg.Contains("mutfak") || msg.Contains("fritöz"))
            {
                matchedProducts = products.Where(p => p.ProductName.ToLower().Contains("airfryer") || p.ProductName.ToLower().Contains("fritöz") || p.ProductName.ToLower().Contains("philips")).ToList();
            }

            if (matchedProducts.Any())
            {
                var sb = new StringBuilder();
                sb.AppendLine("🔍 Aradığınız kritere göre mağazamızdaki en popüler seçenekler şunlar:");
                foreach (var p in matchedProducts)
                {
                    sb.AppendLine($"• **{p.ProductName}** - ₺{p.ProductPrice:N2}");
                }
                sb.AppendLine("\nÜrünün detaylarını görmek için aşağıdaki kartlara tıklayabilirsiniz! ✨");

                var cards = matchedProducts.Take(4).Select(p => new AiProductRecommendationDto
                {
                    ProductID = p.ProductID,
                    ProductName = p.ProductName,
                    Price = p.ProductPrice,
                    ImageUrl = p.ProductImageURL
                }).ToList();

                return (sb.ToString(), cards);
            }

            // 3. Genel karşılama veya öneri
            var topProducts = products.Take(3).ToList();
            var generalSb = new StringBuilder();
            generalSb.AppendLine("Merhaba! 👋 Ben mağazamızın **Yapay Zeka Alışveriş Asistanıyım**.");
            generalSb.AppendLine("Bana bütçenizi (örn: *'5000 TL bütçem var'*) veya aradığınız ürünü (örn: *'Kulaklık önerisi'*) sorabilirsiniz.");
            generalSb.AppendLine("\nŞu an vitrindeki popüler ürünlerimizden bazıları:");
            foreach (var p in topProducts)
            {
                generalSb.AppendLine($"• **{p.ProductName}** (₺{p.ProductPrice:N2})");
            }

            var generalCards = topProducts.Select(p => new AiProductRecommendationDto
            {
                ProductID = p.ProductID,
                ProductName = p.ProductName,
                Price = p.ProductPrice,
                ImageUrl = p.ProductImageURL
            }).ToList();

            return (generalSb.ToString(), generalCards);
        }
    }
}

using System;
using System.Collections.Generic;

namespace ETicaretApi.Dto.Dtos.AiDtos
{
    public class AiPromptRequestDto
    {
        public string Message { get; set; } = string.Empty;
    }

    public class AiProductRecommendationDto
    {
        public Guid ProductID { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
    }

    public class AiChatResponseDto
    {
        public string Reply { get; set; } = string.Empty;
        public List<AiProductRecommendationDto> RecommendedProducts { get; set; } = new List<AiProductRecommendationDto>();
    }
}

using System.Collections.Generic;
using System.Linq;

namespace ETicaretApi.Dto.Dtos.CartDtos
{
    public class CartDto
    {
        public List<CartItemDto> Items { get; set; } = new List<CartItemDto>();
        public decimal SubTotal => Items.Sum(x => x.TotalPrice);
        public decimal ShippingPrice => SubTotal > 0 ? (SubTotal >= 1000 ? 0 : 79.90m) : 0;
        public decimal GrandTotal => SubTotal + ShippingPrice;
    }
}

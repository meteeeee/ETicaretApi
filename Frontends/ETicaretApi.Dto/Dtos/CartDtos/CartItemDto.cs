using System;

namespace ETicaretApi.Dto.Dtos.CartDtos
{
    public class CartItemDto
    {
        public Guid ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? ProductImageURL { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => Price * Quantity;
    }
}

using System;
using System.Collections.Generic;

namespace ETicaretApi.Dto.Dtos.AdminOrderDtos
{
    public class AdminResultOrderDto
    {
        public Guid OrderID { get; set; }
        public Guid UserID { get; set; }
        public string? UserName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string OrderStatus { get; set; }
        public List<AdminResultOrderItemDto> OrderDetails { get; set; } = new List<AdminResultOrderItemDto>();
    }

    public class AdminResultOrderItemDto
    {
        public Guid OrderDetailID { get; set; }
        public Guid OrderID { get; set; }
        public Guid ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? ProductImageURL { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => Quantity * UnitPrice;
    }
}

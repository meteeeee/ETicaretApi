using MediatR;
using System;

namespace ETicaretApi.Application.Features.MediatorDesignPattern.Command.OrderCommands
{
    public class CreateOrderCommand : IRequest<Guid>
    {
        public Guid UserID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string OrderStatus { get; set; }
    }
}

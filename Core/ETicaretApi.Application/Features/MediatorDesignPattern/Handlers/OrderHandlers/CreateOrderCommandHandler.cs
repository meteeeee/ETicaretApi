using ETicaretApi.Application.Features.MediatorDesignPattern.Command.OrderCommands;
using ETicaretApi.Domain.Entities;
using ETicaretApi.Persistence.Context;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ETicaretApi.Application.Features.MediatorDesignPattern.Handlers.OrderHandlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly ProductContext _context;

        public CreateOrderCommandHandler(ProductContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                OrderID = Guid.NewGuid(),
                UserID = request.UserID,
                OrderDate = request.OrderDate == default ? DateTime.Now : request.OrderDate,
                TotalPrice = request.TotalPrice,
                OrderStatus = string.IsNullOrWhiteSpace(request.OrderStatus) ? "Hazırlanıyor" : request.OrderStatus
            };

            await _context.Orders.AddAsync(order, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return order.OrderID;
        }
    }
}

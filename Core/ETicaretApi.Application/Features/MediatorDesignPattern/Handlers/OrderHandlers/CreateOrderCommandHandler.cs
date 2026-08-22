using ETicaretApi.Application.Features.MediatorDesignPattern.Command.OrderCommands;
using ETicaretApi.Domain.Entities;
using ETicaretApi.Persistence.Context;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.Application.Features.MediatorDesignPattern.Handlers.OrderHandlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
    {
        private readonly ProductContext _context;

        public CreateOrderCommandHandler(ProductContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            await _context.Orders.AddAsync(new Order
            {
                UserID = request.UserID,
                OrderDate = request.OrderDate,
                TotalPrice = request.TotalPrice,
                OrderStatus = request.OrderStatus
            });
            await _context.SaveChangesAsync();
        }
    }
}

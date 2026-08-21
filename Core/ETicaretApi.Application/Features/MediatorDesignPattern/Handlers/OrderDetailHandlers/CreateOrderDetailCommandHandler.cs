using ETicaretApi.Application.Features.MediatorDesignPattern.Command.OrderDetailCommands;
using ETicaretApi.Domain.Entities;
using ETicaretApi.Persistence.Context;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.Application.Features.MediatorDesignPattern.Handlers.OrderDetailHandlers
{
    public class CreateOrderDetailCommandHandler : IRequestHandler<CreateOrderDetailCommand>
    {
        private readonly ProductContext _context;

        public CreateOrderDetailCommandHandler(ProductContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateOrderDetailCommand request, CancellationToken cancellationToken)
        {
            _context.OrderDetails.Add(new OrderDetail
            {
                OrderID = request.OrderID,
                ProductID = request.ProductID,
                Quantity = request.Quantity,
                UnitPrice = request.UnitPrice
            });
            await _context.SaveChangesAsync();
        }
    }
}

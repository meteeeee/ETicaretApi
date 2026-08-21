using ETicaretApi.Application.Features.MediatorDesignPattern.Command.OrderDetailCommands;
using ETicaretApi.Persistence.Context;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.Application.Features.MediatorDesignPattern.Handlers.OrderDetailHandlers
{
    public class UpdateOrderDetailCommandHandler : IRequestHandler<UpdateOrderDetailCommand>
    {
        private readonly ProductContext _context;

        public UpdateOrderDetailCommandHandler(ProductContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateOrderDetailCommand request, CancellationToken cancellationToken)
        {
            var values = await _context.OrderDetails.FindAsync(request.OrderDetailID);
            values.OrderID = request.OrderID;
            values.ProductID = request.ProductID;
            values.UnitPrice = request.UnitPrice;
            values.Quantity = request.Quantity;
            await _context.SaveChangesAsync();
        }
    }
}

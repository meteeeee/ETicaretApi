using ETicaretApi.Application.Features.MediatorDesignPattern.Command.OrderCommands;
using ETicaretApi.Persistence.Context;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.Application.Features.MediatorDesignPattern.Handlers.OrderHandlers
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        private readonly ProductContext _context;

        public UpdateOrderCommandHandler(ProductContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var values = await _context.Orders.FindAsync(request.OrderID);
            values.OrderDate = request.OrderDate;
            values.OrderStatus = request.OrderStatus;
            values.TotalPrice = request.TotalPrice;
            values.UserID = request.UserID;
            await _context.SaveChangesAsync();
        }
    }
}

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
    public class RemoveOrderCommandHandler : IRequestHandler<RemoveOrderCommand>
    {
        private readonly ProductContext _context;

        public RemoveOrderCommandHandler(ProductContext context)
        {
            _context = context;
        }

        public async Task Handle(RemoveOrderCommand request, CancellationToken cancellationToken)
        {
            var values = await _context.Orders.FindAsync(request.OrderID);
            _context.Orders.Remove(values);
            await _context.SaveChangesAsync();
        }
    }
}

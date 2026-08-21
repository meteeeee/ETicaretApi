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
    public class RemoveOrderDetailCommandHandler : IRequestHandler<RemoveOrderDetailCommand>
    {
        private readonly ProductContext _context;

        public RemoveOrderDetailCommandHandler(ProductContext context)
        {
            _context = context;
        }

        public async Task Handle(RemoveOrderDetailCommand request, CancellationToken cancellationToken)
        {
            var values = await _context.OrderDetails.FindAsync(request.OrderDetailID);
            _context.OrderDetails.Remove(values);
            await _context.SaveChangesAsync();
        }
    }
}

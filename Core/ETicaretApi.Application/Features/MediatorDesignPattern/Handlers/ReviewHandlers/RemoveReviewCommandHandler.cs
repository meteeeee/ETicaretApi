using ETicaretApi.Application.Features.MediatorDesignPattern.Command.ReviewCommands;
using ETicaretApi.Persistence.Context;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ETicaretApi.Application.Features.MediatorDesignPattern.Handlers.ReviewHandlers
{
    public class RemoveReviewCommandHandler : IRequestHandler<RemoveReviewCommand>
    {
        private readonly ProductContext _context;

        public RemoveReviewCommandHandler(ProductContext context)
        {
            _context = context;
        }

        public async Task Handle(RemoveReviewCommand request, CancellationToken cancellationToken)
        {
            var value = await _context.Reviews.FindAsync(new object[] { request.Id }, cancellationToken);
            if (value != null)
            {
                _context.Reviews.Remove(value);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}

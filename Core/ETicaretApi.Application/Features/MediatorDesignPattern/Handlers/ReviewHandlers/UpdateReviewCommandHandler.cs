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
    public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand>
    {
        private readonly ProductContext _context;

        public UpdateReviewCommandHandler(ProductContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            var value = await _context.Reviews.FindAsync(new object[] { request.ReviewID }, cancellationToken);
            if (value != null)
            {
                value.Comment = request.Comment;
                value.Rating = request.Rating;
                value.ProductID = request.ProductID;
                value.UserID = request.UserID;
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}

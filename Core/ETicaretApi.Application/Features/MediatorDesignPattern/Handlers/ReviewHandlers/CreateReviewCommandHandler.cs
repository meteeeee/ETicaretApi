using ETicaretApi.Application.Features.MediatorDesignPattern.Command.ReviewCommands;
using ETicaretApi.Domain.Entities;
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
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand>
    {
        private readonly ProductContext _context;

        public CreateReviewCommandHandler(ProductContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            _context.Reviews.Add(new Review
            {
                ReviewID = Guid.NewGuid(),
                ProductID = request.ProductID,
                UserID = request.UserID,
                Comment = request.Comment,
                Rating = request.Rating,
                ReviewDate = DateTime.Now
            });
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

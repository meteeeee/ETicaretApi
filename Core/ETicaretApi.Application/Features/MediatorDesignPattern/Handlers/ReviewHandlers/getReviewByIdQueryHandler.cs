using ETicaretApi.Application.Features.MediatorDesignPattern.Queries.ReviewQueries;
using ETicaretApi.Application.Features.MediatorDesignPattern.Results.ReviewResults;
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
    public class getReviewByIdQueryHandler : IRequestHandler<getReviewByIdQuery, getReviewByIdQueryResult>
    {
        private readonly ProductContext _context;

        public getReviewByIdQueryHandler(ProductContext context)
        {
            _context = context;
        }

        public async Task<getReviewByIdQueryResult> Handle(getReviewByIdQuery request, CancellationToken cancellationToken)
        {
            var value = await _context.Reviews.FindAsync(new object[] { request.Id }, cancellationToken);
            if (value == null) return null!;
            return new getReviewByIdQueryResult
            {
                ReviewID = value.ReviewID,
                ProductID = value.ProductID,
                UserID = value.UserID,
                Comment = value.Comment,
                Rating = value.Rating,
                ReviewDate = value.ReviewDate
            };
        }
    }
}

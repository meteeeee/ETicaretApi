using ETicaretApi.Application.Features.MediatorDesignPattern.Queries.ReviewQueries;
using ETicaretApi.Application.Features.MediatorDesignPattern.Results.ReviewResults;
using ETicaretApi.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ETicaretApi.Application.Features.MediatorDesignPattern.Handlers.ReviewHandlers
{
    public class getReviewQueryHandler : IRequestHandler<getReviewQuery, List<getReviewQueryResult>>
    {
        private readonly ProductContext _context;

        public getReviewQueryHandler(ProductContext context)
        {
            _context = context;
        }

        public async Task<List<getReviewQueryResult>> Handle(getReviewQuery request, CancellationToken cancellationToken)
        {
            var values = await _context.Reviews.ToListAsync(cancellationToken);
            return values.Select(x => new getReviewQueryResult
            {
                ReviewID = x.ReviewID,
                ProductID = x.ProductID,
                UserID = x.UserID,
                Comment = x.Comment,
                Rating = x.Rating,
                ReviewDate = x.ReviewDate
            }).ToList();
        }
    }
}

using ETicaretApi.Application.Features.MediatorDesignPattern.Results.ReviewResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.Application.Features.MediatorDesignPattern.Queries.ReviewQueries
{
    public class getReviewByProductIdQuery : IRequest<List<getReviewQueryResult>>
    {
        public Guid ProductId { get; set; }

        public getReviewByProductIdQuery(Guid productId)
        {
            ProductId = productId;
        }
    }
}

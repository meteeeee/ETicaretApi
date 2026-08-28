using ETicaretApi.Application.Features.MediatorDesignPattern.Results.ReviewResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.Application.Features.MediatorDesignPattern.Queries.ReviewQueries
{
    public class getReviewByIdQuery : IRequest<getReviewByIdQueryResult>
    {
        public Guid Id { get; set; }

        public getReviewByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}

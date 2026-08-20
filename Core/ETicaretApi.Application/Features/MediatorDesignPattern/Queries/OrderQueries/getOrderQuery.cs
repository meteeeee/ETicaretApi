using ETicaretApi.Application.Features.MediatorDesignPattern.Results.OrderResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.Application.Features.MediatorDesignPattern.Queries.OrderQueries
{
    public class getOrderQuery : IRequest<List<getOrderQueryResult>>
    {
    }
}

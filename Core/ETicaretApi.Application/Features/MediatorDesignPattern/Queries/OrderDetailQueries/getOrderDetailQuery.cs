using ETicaretApi.Application.Features.MediatorDesignPattern.Results.OrderDetailResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.Application.Features.MediatorDesignPattern.Queries.OrderDetailQueries
{
    public class getOrderDetailQuery : IRequest<List<getOrderDetailQueryResult>>
    {
    }
}

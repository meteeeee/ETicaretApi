using ETicaretApi.Application.Features.MediatorDesignPattern.Queries.OrderQueries;
using ETicaretApi.Application.Features.MediatorDesignPattern.Results.OrderResults;
using ETicaretApi.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.Application.Features.MediatorDesignPattern.Handlers.OrderHandlers
{
    public class getOrderQueryHandler : IRequestHandler<getOrderQuery, List<getOrderQueryResult>>
    {
        private readonly ProductContext _context;

        public getOrderQueryHandler(ProductContext context)
        {
            _context = context;
        }

        public async Task<List<getOrderQueryResult>> Handle(getOrderQuery request, CancellationToken cancellationToken)
        {
            var values = await _context.Orders.ToListAsync();
            return values.Select(x => new getOrderQueryResult
            {
                OrderID = x.OrderID,
                OrderDate = x.OrderDate,
                OrderStatus = x.OrderStatus,
                TotalPrice = x.TotalPrice,
                UserID = x.UserID
            }).ToList();
        }
    }
}

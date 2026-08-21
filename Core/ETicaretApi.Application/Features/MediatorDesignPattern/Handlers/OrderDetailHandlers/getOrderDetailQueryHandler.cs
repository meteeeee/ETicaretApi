using ETicaretApi.Application.Features.MediatorDesignPattern.Queries.OrderDetailQueries;
using ETicaretApi.Application.Features.MediatorDesignPattern.Results.OrderDetailResults;
using ETicaretApi.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.Application.Features.MediatorDesignPattern.Handlers.OrderDetailHandlers
{
    public class getOrderDetailQueryHandler : IRequestHandler<getOrderDetailQuery, List<getOrderDetailQueryResult>>
    {
        private readonly ProductContext _context;

        public getOrderDetailQueryHandler(ProductContext context)
        {
            _context = context;
        }

        public async Task<List<getOrderDetailQueryResult>> Handle(getOrderDetailQuery request, CancellationToken cancellationToken)
        {
            var values = await _context.OrderDetails.ToListAsync();
            return values.Select(x => new getOrderDetailQueryResult
            {
                OrderDetailID = x.OrderDetailID,
                OrderID = x.OrderID,
                ProductID = x.ProductID,
                Quantity = x.Quantity,
                UnitPrice = x.UnitPrice
            }).ToList();
        }
    }
}

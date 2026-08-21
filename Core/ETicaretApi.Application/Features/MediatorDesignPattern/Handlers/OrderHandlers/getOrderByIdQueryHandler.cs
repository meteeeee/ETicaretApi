using ETicaretApi.Application.Features.MediatorDesignPattern.Queries.OrderQueries;
using ETicaretApi.Application.Features.MediatorDesignPattern.Results.OrderResults;
using ETicaretApi.Persistence.Context;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.Application.Features.MediatorDesignPattern.Handlers.OrderHandlers
{
    public class getOrderByIdQueryHandler : IRequestHandler<getOrderByIdQuery, getOrderByIdQueryResult>
    {
        private readonly ProductContext _context;

        public getOrderByIdQueryHandler(ProductContext context)
        {
            _context = context;
        }

        public async Task<getOrderByIdQueryResult> Handle(getOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var values = await _context.Orders.FindAsync(request.OrderID);
            if (values == null) return null;

            return new getOrderByIdQueryResult
            {
                OrderID = values.OrderID,
                OrderDate = values.OrderDate,
                OrderStatus = values.OrderStatus,
                TotalPrice = values.TotalPrice,
                UserID = values.UserID
            };
        }
    }
}

using ETicaretApi.Application.Features.MediatorDesignPattern.Queries.OrderDetailQueries;
using ETicaretApi.Application.Features.MediatorDesignPattern.Results.OrderDetailResults;
using ETicaretApi.Persistence.Context;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.Application.Features.MediatorDesignPattern.Handlers.OrderDetailHandlers
{
    public class getOrderDetailByIdQueryHandler : IRequestHandler<getOrderDetailByIdQuery, getOrderDetailByIdQueryResult>
    {
        private readonly ProductContext _context;

        public getOrderDetailByIdQueryHandler(ProductContext context)
        {
            _context = context;
        }

        public async Task<getOrderDetailByIdQueryResult> Handle(getOrderDetailByIdQuery request, CancellationToken cancellationToken)
        {
            var values = await _context.OrderDetails.FindAsync(request.OrderDetailID);
            return new getOrderDetailByIdQueryResult
            {
                OrderID = values.OrderID,
                ProductID = values.ProductID,
                Quantity = values.Quantity,
                UnitPrice = values.UnitPrice
            };
        }
    }
}

using ETicaretApi.Application.Features.CQRSDesignPattern.Queries.ProductQueries;
using ETicaretApi.Application.Features.CQRSDesignPattern.Results.ProductResults;
using ETicaretApi.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.Application.Features.CQRSDesignPattern.Handlers.ProductHandlers
{
    public class getProductByIdQueryHandler
    {
        private readonly ProductContext _context;

        public getProductByIdQueryHandler(ProductContext context)
        {
            _context = context;
        }

        public async Task<getProductByIdQueryResult> Handle(getProductByIdQuery query)
        {
            var value = await _context.Products.FindAsync(query.ProductID);
            return new getProductByIdQueryResult
            {
                ProductID = value.ProductID,
                ProductCategoryID = value.ProductCategoryID,
                ProductImageURL = value.ProductImageURL,
                ProductName = value.ProductName,
                ProductPrice = value.ProductPrice
            };
        }
    }
}

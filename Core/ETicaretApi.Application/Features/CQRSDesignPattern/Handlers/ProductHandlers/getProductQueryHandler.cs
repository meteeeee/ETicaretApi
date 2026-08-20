using ETicaretApi.Application.Features.CQRSDesignPattern.Results.ProductResults;
using ETicaretApi.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.Application.Features.CQRSDesignPattern.Handlers.ProductHandlers
{
    public class getProductQueryHandler
    {
        private readonly ProductContext _context;

        public getProductQueryHandler(ProductContext context)
        {
            _context = context;
        }

        public async Task<List<getProductQueryResult>> Handle()
        {
            var values = await _context.Products.ToListAsync();
            return values.Select(x=>new getProductQueryResult
            {
                ProductID = x.ProductID,
                ProductCategoryID = x.ProductCategoryID,
                ProductName = x.ProductName,
                ProductImageURL = x.ProductImageURL,
                ProductPrice = x.ProductPrice
            }).ToList();
        }
    }
}

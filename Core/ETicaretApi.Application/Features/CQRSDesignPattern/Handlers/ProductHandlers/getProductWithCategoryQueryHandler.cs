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
    public class getProductWithCategoryQueryHandler
    {
        private readonly ProductContext _context;

        public getProductWithCategoryQueryHandler(ProductContext context)
        {
            _context = context;
        }

        public async Task<List<getProductWithCategoryQueryResult>> Handle()
        {
            var values = await _context.Products.Include(x => x.Category).ToListAsync();
            return values.Select(x => new getProductWithCategoryQueryResult
            {
                ProductID = x.ProductID,
                ProductName = x.ProductName,
                ProductCategoryID = x.ProductCategoryID,
                CategoryName = x.Category != null ? x.Category.CategoryName : "Genel",
                ProductPrice = x.ProductPrice,
                ProductImageURL = x.ProductImageURL
            }).ToList();
        }
    }
}

using ETicaretApi.Application.Features.CQRSDesignPattern.Queries.CategoryQueries;
using ETicaretApi.Application.Features.CQRSDesignPattern.Results.CategoryResults;
using ETicaretApi.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.Application.Features.CQRSDesignPattern.Handlers.CategoryHandlers
{
    public class getCategoryByIdQueryHandler
    {
        private readonly ProductContext _context;

        public getCategoryByIdQueryHandler(ProductContext context)
        {
            _context = context;
        }
        public async Task<getCategoryByIdQueryResult> Handle(getCategoryByIdQuery query)
        {
            var value = await _context.Categories.FindAsync(query.CategoryID);
            return new getCategoryByIdQueryResult
            {
                CategoryID = value.CategoryID,
                CategoryName = value.CategoryName
            };
        }
    }
}
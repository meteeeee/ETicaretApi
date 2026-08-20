using ETicaretApi.Application.Features.CQRSDesignPattern.Results.CategoryResults;
using ETicaretApi.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.Application.Features.CQRSDesignPattern.Handlers.CategoryHandlers
{
    public class getCategoryQueryHandler
    {
        private readonly ProductContext _context;

        public getCategoryQueryHandler(ProductContext context)
        {
            _context = context;
        }
        public async Task<List<getCategoryQueryResult>> Handle()
        {
            var values = await _context.Categories.ToListAsync();
            return values.Select(x=>new getCategoryQueryResult
            {
                CategoryID = x.CategoryID,
                CategoryName = x.CategoryName
            }).ToList();
        }
    }
}
using ETicaretApi.Application.Features.CQRSDesignPattern.Commands.ProductCommands;
using ETicaretApi.Domain.Entities;
using ETicaretApi.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.Application.Features.CQRSDesignPattern.Handlers.ProductHandlers
{
    public class CreateProductCommandHandler
    {
        private readonly ProductContext _context;

        public CreateProductCommandHandler(ProductContext context)
        {
            _context = context;
        }
        public async Task Handle(CreateProductCommand command)
        {
            _context.Products.Add(new Product
            {
                ProductImageURL = command.ProductImageURL,
                ProductCategoryID = command.ProductCategoryID,
                ProductName = command.ProductName,
                ProductPrice = command.ProductPrice
            });
            await _context.SaveChangesAsync();
        }
    }
}
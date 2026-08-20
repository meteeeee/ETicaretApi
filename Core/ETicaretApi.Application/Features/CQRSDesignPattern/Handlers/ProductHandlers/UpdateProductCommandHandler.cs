using ETicaretApi.Application.Features.CQRSDesignPattern.Commands.ProductCommands;
using ETicaretApi.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.Application.Features.CQRSDesignPattern.Handlers.ProductHandlers
{
    public class UpdateProductCommandHandler
    {
        private readonly ProductContext _context;

        public UpdateProductCommandHandler(ProductContext context)
        {
            _context = context;
        }
        public async void Handle(UpdateProductCommand command)
        {
            var value = await _context.Products.FindAsync(command.ProductID);
            value.ProductName = command.ProductName;
            value.ProductPrice = command.ProductPrice;
            value.ProductImageURL = command.ProductImageURL;
            value.ProductCategoryID = command.ProductCategoryID;
            await _context.SaveChangesAsync();
        }
    }
}

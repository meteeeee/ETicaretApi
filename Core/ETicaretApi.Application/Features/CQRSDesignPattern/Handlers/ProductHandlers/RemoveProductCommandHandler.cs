using ETicaretApi.Application.Features.CQRSDesignPattern.Commands.ProductCommands;
using ETicaretApi.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.Application.Features.CQRSDesignPattern.Handlers.ProductHandlers
{
    public class RemoveProductCommandHandler
    {
        private readonly ProductContext _context;

        public RemoveProductCommandHandler(ProductContext context)
        {
            _context = context;
        }
        public async void Handle(RemoveProductCommand command)
        {
            var value = await _context.Products.FindAsync(command.ProductID);
            _context.Products.Remove(value);
            await _context.SaveChangesAsync();
        }
    }
}

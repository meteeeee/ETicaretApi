using ETicaretApi.Application.Features.CQRSDesignPattern.Commands.CategoryCommands;
using ETicaretApi.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.Application.Features.CQRSDesignPattern.Handlers.CategoryHandlers
{
    public class RemoveCategoryCommandHandler
    {
        private readonly ProductContext _context;

        public RemoveCategoryCommandHandler(ProductContext context)
        {
            _context = context;
        }
        public async void Handle(RemoveCategoryCommand command)
        {
            var value = await _context.Products.FindAsync(command.CategoryID);
            _context.Products.Remove(value);
            await _context.SaveChangesAsync();
        }
    }
}

using ETicaretApi.Application.Features.CQRSDesignPattern.Commands.CategoryCommands;
using ETicaretApi.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.Application.Features.CQRSDesignPattern.Handlers.CategoryHandlers
{
    public class UpdateCategoryCommandHandler
    {
        private readonly ProductContext _context;

        public UpdateCategoryCommandHandler(ProductContext context)
        {
            _context = context;
        }
        public async void Handle(UpdateCategoryCommand command)
        {
            var value = await _context.Categories.FindAsync(command.CategoryID);
            value.CategoryName = command.CategoryName;
            await _context.SaveChangesAsync();
        }
    }
}

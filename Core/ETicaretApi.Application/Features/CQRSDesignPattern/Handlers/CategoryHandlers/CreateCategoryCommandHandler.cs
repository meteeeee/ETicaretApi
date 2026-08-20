using ETicaretApi.Application.Features.CQRSDesignPattern.Commands.CategoryCommands;
using ETicaretApi.Domain.Entities;
using ETicaretApi.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.Application.Features.CQRSDesignPattern.Handlers.CategoryHandlers
{
    public class CreateCategoryCommandHandler
    {
        private readonly ProductContext _context;

        public CreateCategoryCommandHandler(ProductContext context)
        {
            _context = context;
        }
        public async Task Handle(CreateCategoryCommand command)
        {
            _context.Categories.Add(new Category
            {
                CategoryName = command.CategoryName
            });
            await _context.SaveChangesAsync();
        }
    }
}

using ETicaretApi.Application.Features.CQRSDesignPattern.Commands.UserRegisterCommands;
using ETicaretApi.Persistence.Context;
using ETicaretApi.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace ETicaretApi.Application.Features.CQRSDesignPattern.Handlers.UserRegisterHandlers
{
    public class CreateUserRegisterCommandHandler
    {
        private readonly ProductContext _productContext;
        private readonly UserManager<AppUser> _userManager;

        public CreateUserRegisterCommandHandler(ProductContext productContext, UserManager<AppUser> userManager)
        {
            _productContext = productContext;
            _userManager = userManager;
        }

        public async Task<IdentityResult> Handle(CreateUserRegisterCommand command)
        {
            var user = new AppUser()
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                UserName = command.UserName,
                Gender = command.Gender,
                Address = command.Address,
                Email = command.Email
            };

            var result = await _userManager.CreateAsync(user, command.Password);
            if (result.Succeeded)
            {
                // Kullanıcıya doğrudan 'User' rolünü ata (AspNetUserRoles tablosunu doldurur)
                await _userManager.AddToRoleAsync(user, "User");
            }
            return result;
        }
    }
}

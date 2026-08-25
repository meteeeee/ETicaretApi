using ETicaretApi.Application.Features.CQRSDesignPattern.Commands.UserRegisterCommands;
using ETicaretApi.Persistence.Context;
using ETicaretApi.Persistence.Identity;
using Microsoft.AspNetCore.Identity;

namespace ETicaretApi.Application.Features.CQRSDesignPattern.Handlers.UserRegisterHandlers
{
    public class CreateUserRegisterCommandHandler
    {
        private readonly ProductContext productContext;
        private readonly UserManager<AppUser> _userManager;
        public CreateUserRegisterCommandHandler(ProductContext productContext, UserManager<AppUser> userManager)
        {
            this.productContext = productContext;
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
            return await _userManager.CreateAsync(user, command.Password);
        }


    }
}

using ETicaretApi.Application.Features.CQRSDesignPattern.Commands.UserRegisterCommands;
using ETicaretApi.Application.Features.CQRSDesignPattern.Handlers.UserRegisterHandlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistersController : ControllerBase
    {
        private readonly CreateUserRegisterCommandHandler _createUserRegisterCommandHandler;

        public RegistersController(CreateUserRegisterCommandHandler createUserRegisterCommandHandler)
        {
            _createUserRegisterCommandHandler = createUserRegisterCommandHandler;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserRegister(CreateUserRegisterCommand command)
        {
            var result = await _createUserRegisterCommandHandler.Handle(command);
            if (result.Succeeded)
            {
                return Ok("Kullanıcı başarıyla eklendi.");
            }
            return BadRequest(result.Errors.Select(e => e.Description));
        }
    }
}

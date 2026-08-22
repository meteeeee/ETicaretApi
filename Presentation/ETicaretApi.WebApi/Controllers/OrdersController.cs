using ETicaretApi.Application.Features.MediatorDesignPattern.Command.OrderCommands;
using ETicaretApi.Application.Features.MediatorDesignPattern.Queries.OrderQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> OrderList()
        {
            var value = await _mediator.Send(new getOrderQuery());
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderCommand command)
        {
            await _mediator.Send(command);
            return Ok("Ekleme işlemi başarılı.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            await _mediator.Send(new RemoveOrderCommand(id));
            return Ok("Silme işlemi başarılı.");
        }

        [HttpGet("GetOrderById")]
        public async Task<IActionResult> getOrderById(Guid id)
        {
            var value = await _mediator.Send(new getOrderByIdQuery(id));
            return Ok(value);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrder(UpdateOrderCommand command)
        {
            await _mediator.Send(command);
            return Ok("Güncelleme işlemi başarılı.");
        }
    }
}

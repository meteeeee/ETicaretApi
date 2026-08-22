using ETicaretApi.Application.Features.MediatorDesignPattern.Command.OrderDetailCommands;
using ETicaretApi.Application.Features.MediatorDesignPattern.Queries.OrderDetailQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderDetailsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> OrderDetailList()
        {
            var value = await _mediator.Send(new getOrderDetailQuery());
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderDetail(CreateOrderDetailCommand command)
        {
            await _mediator.Send(command);
            return Ok("Ekleme işlemi başarılı.");
        }

        [HttpGet("GetOrderDetailById")]
        public async Task<IActionResult> getOrderDetailById(Guid id)
        {
            var value = await _mediator.Send(new getOrderDetailByIdQuery(id));
            return Ok(value);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOrderDetail(Guid id)
        {
            await _mediator.Send(new RemoveOrderDetailCommand(id));
            return Ok("Silme işlemi başarılı.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrderDetail(UpdateOrderDetailCommand command)
        {
            await _mediator.Send(command);
            return Ok("Güncelleme işlemi başarılı.");
        }
    }
}

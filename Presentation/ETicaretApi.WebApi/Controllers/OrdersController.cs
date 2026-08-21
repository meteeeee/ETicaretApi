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
        public IActionResult OrderList()
        {
            var value = _mediator.Send(new getOrderQuery());
            return Ok(value);
        }

        [HttpPost]
        public IActionResult CreateOrder(CreateOrderCommand command)
        {
            _mediator.Send(command);
            return Ok("Ekleme işlemi başarılı.");
        }

        [HttpDelete]
        public IActionResult DeleteOrder(Guid id)
        {
            _mediator.Send(new RemoveOrderCommand(id));
            return Ok("Silme işlemi başarılı.");
        }

        [HttpGet("GetOrderById")]
        public IActionResult getOrderById(Guid id)
        {
            var value = _mediator.Send(new getOrderDetailByIdQuery(id));
            return Ok(value);
        }

        [HttpPut]
        public IActionResult UpdateOrder(UpdateOrderCommand command)
        {
            _mediator.Send(command);
            return Ok("Güncelleme işlemi başarılı.");
        }
    }
}

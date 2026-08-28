using ETicaretApi.Application.Features.MediatorDesignPattern.Command.ReviewCommands;
using ETicaretApi.Application.Features.MediatorDesignPattern.Queries.ReviewQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReviewsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> ReviewList()
        {
            var values = await _mediator.Send(new getReviewQuery());
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReview(Guid id)
        {
            var value = await _mediator.Send(new getReviewByIdQuery(id));
            return Ok(value);
        }

        [HttpGet("GetReviewsByProductId/{productId}")]
        public async Task<IActionResult> GetReviewsByProductId(Guid productId)
        {
            var values = await _mediator.Send(new getReviewByProductIdQuery(productId));
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview(CreateReviewCommand command)
        {
            await _mediator.Send(command);
            return Ok("Yorum başarıyla eklendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(Guid id)
        {
            await _mediator.Send(new RemoveReviewCommand(id));
            return Ok("Yorum başarıyla silindi.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateReview(UpdateReviewCommand command)
        {
            await _mediator.Send(command);
            return Ok("Yorum başarıyla güncellendi.");
        }
    }
}

using ETicaretApi.Application.Features.CQRSDesignPattern.Commands.ProductCommands;
using ETicaretApi.Application.Features.CQRSDesignPattern.Handlers.ProductHandlers;
using ETicaretApi.Application.Features.CQRSDesignPattern.Queries.ProductQueries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly getProductQueryHandler _getProductQueryHandler;
        private readonly getProductByIdQueryHandler _getProductByIdQueryHandler;
        private readonly getProductWithCategoryQueryHandler _getProductWithCategoryQueryHandler;
        private readonly CreateProductCommandHandler _createProductCommandHandler;
        private readonly UpdateProductCommandHandler _updateProductCommandHandler;
        private readonly RemoveProductCommandHandler _removeProductCommandHandler;

        public ProductsController(
            getProductQueryHandler getProductQueryHandler,
            getProductByIdQueryHandler getProductByIdQueryHandler,
            getProductWithCategoryQueryHandler getProductWithCategoryQueryHandler,
            CreateProductCommandHandler createProductCommandHandler,
            UpdateProductCommandHandler updateProductCommandHandler,
            RemoveProductCommandHandler removeProductCommandHandler)
        {
            _getProductQueryHandler = getProductQueryHandler;
            _getProductByIdQueryHandler = getProductByIdQueryHandler;
            _getProductWithCategoryQueryHandler = getProductWithCategoryQueryHandler;
            _createProductCommandHandler = createProductCommandHandler;
            _updateProductCommandHandler = updateProductCommandHandler;
            _removeProductCommandHandler = removeProductCommandHandler;
        }

        [HttpGet]
        public async Task<IActionResult> ProductList()
        {
            var value = await _getProductQueryHandler.Handle();
            return Ok(value);
        }

        [HttpGet("ProductListWithCategory")]
        public async Task<IActionResult> ProductListWithCategory()
        {
            var values = await _getProductWithCategoryQueryHandler.Handle();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductCommand command)
        {
            await _createProductCommandHandler.Handle(command);
            return Ok("Ürün ekleme işlemi başarılı.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            await _removeProductCommandHandler.Handle(new RemoveProductCommand(id));
            return Ok("Silme işlemi başarılı.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductCommand command)
        {
            await _updateProductCommandHandler.Handle(command);
            return Ok("Güncelleme işlemi başarılı.");
        }

        [HttpGet("GetProduct")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            var value = await _getProductByIdQueryHandler.Handle(new getProductByIdQuery(id));
            return Ok(value);
        }
    }
}

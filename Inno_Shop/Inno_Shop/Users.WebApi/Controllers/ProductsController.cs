using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Products.Application.Interfaces;
using Products.Application.Products.Commands.CreateProduct;
using Products.Application.Products.Commands.DeleteProduct;
using Products.Application.Products.Commands.UpdateProduct;
using Products.Application.Products.Queries.GetProductList.GetAllProductList;
using Products.Application.Products.Queries.GetProductsDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.WebApi.Models;

namespace Users.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : BaseController
    {

        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        public ProductsController(IMapper mapper, IProductService productService)
        { _mapper = mapper; _productService = productService; }

        [HttpGet]
        public async Task<ActionResult<ProductListVm>> GetAll()
        {
            var query = new GetProductListQuery
            {

            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
        [HttpGet("{userid}")]
        public async Task<ActionResult<Products.Application.Products.Queries.GetProductList.GetUserProductList.ProductListVm>> GetAllUser(Guid userId)
        {
            var query = new Products.Application.Products.Queries.GetProductList.GetUserProductList.GetProductListQuery
            {
                UserId = UserId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
          [HttpGet("{id}")]
        public async Task<ActionResult<ProductDetailsVm>> Get(Guid id)
        {
            var query = new GetProductsDetailsQuery
            {
                Id = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateProductDto createProductDto)
        {
            var command = _mapper.Map<CreateProductCommand>(createProductDto);
            var userId = await Mediator.Send(command);
            return Ok(userId);
           
        }
        [Authorize]
        [HttpPut]
        public async Task<ActionResult<Guid>> Update([FromBody] UpdateProductDto updateProductDto)
        {
            var command = _mapper.Map<UpdateProductCommand>(updateProductDto);
            await Mediator.Send(command);
            return NoContent();
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, Guid userId)
        {
            var command = new DeleteProductCommand
            {
                Id = id,
                UserId = userId
            };
            await Mediator.Send(command);
            return NoContent();
        }
        [HttpGet("find")]
        public async Task<IActionResult> GetProducts([FromQuery] ProductsFilterDto filter)
        {
            var products = await _productService.GetFilteredProductsAsync(filter);
            return Ok(products);
        }
    }
}

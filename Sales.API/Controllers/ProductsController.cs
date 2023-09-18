using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sales.API.Data;
using Sales.API.Models;
using Sales.API.Models.DTOs;
using Sales.API.Repository.IRepository;

namespace Sales.API.Controllers
{
    [Route("api/Products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProduct _repoProduct;
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public ProductsController(IProduct repoProduct, IMapper mapper, DataContext context)
        {
            _repoProduct = repoProduct;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var listProducts = _repoProduct.GetProducts();

            var listProductsDto = new List<ProductDto>();

            foreach (var products in listProducts)
            {
                listProductsDto.Add(_mapper.Map<ProductDto>(products));
            }
            return Ok(listProductsDto);
        }

        [HttpGet("{productId:int}", Name = "GetProduct")]
        public IActionResult GetProduct(int productId)
        {
            var product = _repoProduct.GetProduct(productId);

            if (product == null)
            {
                return BadRequest();
            }
            var productDto = _mapper.Map<ProductDto>(product);
            return Ok(productDto);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult CreateProduct([FromBody] ProductDto productDto)
        {
            if (productDto == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var createProduct = _mapper.Map<Product>(productDto);
                if (!_repoProduct.CreateProduct(createProduct))
                {
                    ModelState.AddModelError("", "No se pudo crear el registro");
                    return StatusCode(500, ModelState);

                }
                return CreatedAtRoute("GetProduct", new { productId = createProduct.Id }, createProduct);
            }
            catch (Exception)
            {

                ModelState.AddModelError("", "No se pudo crear el registro");
                return StatusCode(500, ModelState);
            }
        }
        [Authorize(Roles = "admin")]
        [HttpPatch("{productoId:int}", Name = "UpdateProduct")]
        public IActionResult UpdateProduct(int productoId, [FromBody] ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = _mapper.Map<Product>(productDto);

            try
            {
                if (!_repoProduct.UpdateProduct(product))
                {
                    ModelState.AddModelError("", "No se pudo actualizar el registro");
                    return StatusCode(500, ModelState);
                }
                return NoContent();

            }
            catch (Exception)
            {

                ModelState.AddModelError("", "No se pudo actualizar el registro");
                return StatusCode(500, ModelState);
            }
          
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("{productId:int}", Name = "DeleteProduct")]
        public IActionResult DeleteProduct(int productId)
        {
            var product = _repoProduct.GetProduct(productId);
            if (product == null)
            {
                return NotFound();
            }

            if (!_repoProduct.DeleteProduct(product))
            {
                ModelState.AddModelError("", "No se pudo eliminando el registro");
                return StatusCode(500, ModelState);

            }
            return NoContent();
        }

        [HttpGet("{initialPrice:decimal},{finalPrice:decimal}", Name = "GetProductsByPrice")]
        public IActionResult GetProductsByPrice(decimal initialPrice, decimal finalPrice)
        {
            var listProducts = _repoProduct.GetProductsByPrice(initialPrice, finalPrice);

            var listProductsDto = new List<ProductDto>();

            foreach (var products in listProducts)
            {
                listProductsDto.Add(_mapper.Map<ProductDto>(products));
            }
            return Ok(listProductsDto);
        }

        [HttpGet("{productName}", Name = "GetProductName")]
        public IActionResult GetProductName(string productName)
        {
            var product = _repoProduct.GetProductName(productName);

            if (product == null)
            {
                return BadRequest();
            }
            var productDto = _mapper.Map<ProductDto>(product);
            return Ok(productDto);
        }

        [HttpGet()]
        [Route("GetAsyncDos")]
        public async Task<IActionResult> GetAsyncDos()
        {
            return Ok(await _context.Products
                            .Include(c => c.Category)
                            .OrderBy(c => c.Id).ToListAsync());
        }
        [HttpGet]
        [Route("GetAsyncTres")]
        public async Task<IActionResult> GetProductsAndCategory()
        {
            var listProducts = await _repoProduct.GetProductsAndCatogory();

            return Ok(listProducts);
        }


        [HttpGet]
        [Route("GetProductsAndCatogory")]
        public async Task<IActionResult> GetProductsAndCatogory()
        {
            var listProducts = await _repoProduct.GetProductsAndCatogory();

            return Ok(listProducts);
        }
    }
}

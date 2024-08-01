using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealHousing.BusinessLayer.Abstract;
using RealHousing.DtoLayer.ProductDtos;
using RealHousing.EntityLayer.Concreate;

namespace RealHousing.ApiLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public IActionResult ProductList()
        {
            var values = _productService.TGetList();
            return Ok(values);
        }
        [HttpGet("ProductListWithCategory")]
        public IActionResult ProductListWithCategory()
        {
            var values=_productService.TGetProductsWithCategories();
            return Ok(values);
        }
        [HttpPost]
        public IActionResult AddProduct(AddProductDto addProductDto)
        {
            var product = new Product()
            {
                ProductTitle=addProductDto.ProductTitle,
                ProductPrice=addProductDto.ProductPrice,
                ProductType=addProductDto.ProductType,
                ProductAdress=addProductDto.ProductAdress,
                BedRoomCount=addProductDto.BedRoomCount,
                BathCount=addProductDto.BathCount,
                Square=addProductDto.Square,
                CoverImageUrl=addProductDto.CoverImageUrl,
                CategoryID=addProductDto.CategoryID
            };
            _productService.TInsert(product);
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var values=_productService.TGetByID(id);
            _productService.TDelete(values);
            return Ok();
        }
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var values = _productService.TGetByID(id);
            return Ok(values);
        }
        [HttpPut]
        public IActionResult UpdateProduct(UpdateProductDto updateProductDto)
        {
            var product = new Product()
            {
                ProductID=updateProductDto.ProductID,
                ProductTitle = updateProductDto.ProductTitle,
                ProductPrice = updateProductDto.ProductPrice,
                ProductType = updateProductDto.ProductType,
                ProductAdress = updateProductDto.ProductAdress,
                BedRoomCount = updateProductDto.BedRoomCount,
                BathCount = updateProductDto.BathCount,
                Square = updateProductDto.Square,
                CoverImageUrl = updateProductDto.CoverImageUrl,
                CategoryID = updateProductDto.CategoryID
            };
            _productService.TUpdate(product);
            return Ok();
        }
    }
}

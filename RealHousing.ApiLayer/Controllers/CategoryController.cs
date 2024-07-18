using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealHousing.BusinessLayer.Abstract;

namespace RealHousing.ApiLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public IActionResult CategoryList()
        {
            var values=_categoryService.TGetList();
            return Ok(values);
        }
        [HttpDelete]
        public IActionResult Delete(int id) 
        {
            var values = _categoryService.TGetByID(id); 
            _categoryService.TDelete(values);
            return Ok();
        }
    }
}

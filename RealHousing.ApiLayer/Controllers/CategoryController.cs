using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealHousing.BusinessLayer.Abstract;
using RealHousing.DtoLayer.CategoryDtos;
using RealHousing.EntityLayer.Concreate;

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
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id) 
        {
            var values = _categoryService.TGetByID(id); 
            _categoryService.TDelete(values);
            return Ok();
        }
        [HttpPost]
        public IActionResult AddCategory(ResultCategoryDto resultCategoryDto)
        {
            Category category = new Category();
            category.CategoryName=resultCategoryDto.CategoryName;
            _categoryService.TInsert(category);
            return Ok();
        }
        [HttpPut]
        public IActionResult UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            Category category = new Category()
            {
                CategoryID = updateCategoryDto.CategoryID,
                CategoryName = updateCategoryDto.CategoryName,
            };
            _categoryService.TUpdate(category);
            return Ok();
        }
        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            var values=_categoryService.TGetByID(id);
            return Ok(values);
        }

    }
}

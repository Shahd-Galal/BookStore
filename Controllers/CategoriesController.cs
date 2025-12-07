using BookStoreApi.Models;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _Services;

        public CategoriesController(ICategoryService services)
        {
            _Services = services;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            try
            {
                var categories = await _Services.GetAll();
                if (categories == null)
                {
                    return NotFound();
                }
                return Ok(categories);
            }
            catch (Exception ex) 
            {
               return BadRequest("Error ");
            } 
        }

        [HttpGet("{id}")]

        public async Task<IActionResult>GetCategory(int id)
        {

            try
            {
                var Category = await _Services.GetById(id);
                if (Category == null)
                    return NotFound();
                return Ok(Category);
            }
            catch (Exception ex)
            {
                return BadRequest("Error ");
            }
        }


        [HttpPost]

        public async Task<IActionResult> CreateAsync(Category dto)
        {
            try
            {
                var Category = new Category { Name = dto.Name };
                await _Services.Create(dto);

                return Ok(Category);
            }
            catch (Exception ex) 
            {
                return BadRequest("Error ");
            }

        }

        [HttpPut ("{id}")]
        public async Task<IActionResult> Update(int id , Category dto)
        {
            try
            {
                var Category = await _Services.Update(id, dto);
                if (Category)
                    return Ok(Category);
                return BadRequest("Category not found");
            }
            catch (Exception ex)
            {
                return BadRequest("Error ");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
           
            try
            {
                var category = await _Services.GetById(id);

                if(category == null)
                    return NotFound();

                var Category = await _Services.Delete(id);
                if (Category == -1)
                    return BadRequest("Category has book Can't be deleted");
                return Ok("Deleted");

            }
            catch (Exception ex) 
            {
               // Console.WriteLine(ex.Message);
                return BadRequest("Delete failed");

            }
        }

      
    }
}

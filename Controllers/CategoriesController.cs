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
            var Categories = await _Services.GetAll();

            return Ok(Categories);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult>GetCategory(int id)
        {
            var Category = await _Services.GetById(id);
            if (Category == null)
                return NotFound();
            return Ok(Category);
        }


        [HttpPost]

        public async Task<IActionResult> CreateAsync(Category dto)
        {
            var Category = new Category { Name = dto.Name };
          await _Services.Create(dto);
           
            return Ok(Category);
        }

        [HttpPut ("{id}")]
        public async Task<IActionResult> Update(int id , Category dto)
        {
            var Category = await _Services.Create(dto); 
            if(Category)
                return Ok(Category);
           

            return BadRequest("");
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
                Console.WriteLine(ex.Message);
                return BadRequest("Delete failed");

            }
        }

      
    }
}

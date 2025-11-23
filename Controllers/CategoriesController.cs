
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync() 
        {
            var Categories = await _context.Categories.Include(c => c.Books).ToListAsync();

            return Ok(Categories);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult>GetCategory(int id)
        {
            var Category = await _context.Categories.Include(c => c.Books).FirstOrDefaultAsync(c => c.Id == id);
            if (Category == null)
                return NotFound();
            return Ok(Category);
        }


        [HttpPost]

        public async Task<IActionResult> CreateAsync(CreateCategoryDto dto)
        {
            var Category = new Category { Name = dto.Name };
          await  _context.Categories.AddAsync(Category);
            _context.SaveChanges();
            return Ok(Category);
        }

        [HttpPut ("{id}")]
        public async Task<IActionResult> Update(int id , CreateCategoryDto dto)
        {
            var Category = await _context.Categories.FindAsync(id); 
            if(Category == null)
                return NotFound();
            Category.Name = dto.Name;
            await _context.SaveChangesAsync();
            return Ok(Category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>Delete(int id)
        {
            var Category = await _context.Categories.Include(c => c.Books).FirstOrDefaultAsync(c => c.Id == id);
            if (Category == null)
                return NotFound();
            if (Category.Books.Count > 0)
                return BadRequest("Category has book Can't be deleted");
            _context.Categories.Remove(Category);
            await _context.SaveChangesAsync();
            return Ok("Deleted");
        }
    }
}

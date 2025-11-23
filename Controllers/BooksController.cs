using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BooksController> _logger;
        public BooksController(ApplicationDbContext context, ILogger<BooksController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string? search, int pageNumber = 1, int PageSize = 10)
        {
            var query = _context.Books.Include(b => b.Category).ToList();


                         //search
            if (!string.IsNullOrWhiteSpace(search))
            {
              query = query.Where(b => b.Title.Contains(search) || b.Author.Contains(search)).ToList();

            }


            //paging
            var totalCount =  query.Count();
            var books =  query.Skip((pageNumber - 1) * PageSize).Take(PageSize);

            //Handel
            if (totalCount == 0)
            {
                return NotFound(new
                {
                    Message = "No books found",
                    Search = search
                });
            }


            return Ok(new
            {
                totalCount = totalCount,
                pageNumber = pageNumber,
                pageSize = PageSize,
                Data = books
            });
        }



            [HttpGet("{id}")]
            public async Task<IActionResult> Get(int id)
            {
                _logger.LogInformation("Getting All books");
                var book = await _context.Books.Include(b => b.Category).FirstOrDefaultAsync(b => b.Id == id);
                if (book == null)
                    return NotFound();
                return Ok(book);
            }

            //Search Endpoint

            //[HttpGet("search")]
            //public async Task<IActionResult> search([FromBody] string title)
            //{
            //    var books = await _context.Books.Where(b =>  b.Title.Contains(title)).ToListAsync();
            //    return Ok(books);
            //}

            //Pagination

            //[HttpGet ("paged")]
            //public async Task<IActionResult>GetPaged(int page = 1, int pageSize = 10)
            //{
            //    var books = await _context.Books.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            //    return Ok(books);
            //}


            [HttpPost]
            public async Task<IActionResult> Create([FromBody] CreateBookDto dto)
            {
                _logger.LogInformation("Getting All books");
                var Category = await _context.Categories.FindAsync(dto.CategoryId);
                if (Category == null)
                    return BadRequest("Invalid CategoryId");

                var book = new Book
                {
                    Title = dto.Title,
                    Author = dto.Author,
                    Year = dto.Year,
                    CategoryId = dto.CategoryId
                };
                _context.Books.Add(book);
                await _context.SaveChangesAsync();
                return Ok(book);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> Update(int id, CreateBookDto dto)
            {
                _logger.LogInformation("Getting All books");
                var book = await _context.Books.FindAsync(id);
                if (book == null)
                    return NotFound();
                //Return 400
                var Category = await _context.Categories.FindAsync(dto.CategoryId);
                if (Category == null)
                    return BadRequest("Invalid CategoryId");

                book.Title = dto.Title;
                book.Author = dto.Author;
                book.Year = dto.Year;
                book.CategoryId = dto.CategoryId;
                await _context.SaveChangesAsync();
                return Ok(book);
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                _logger.LogInformation("Getting All books");
                var book = await _context.Books.FindAsync(id);
                if (book == null)
                    return NotFound();
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
                return Ok(book);
            }
        
    }
}

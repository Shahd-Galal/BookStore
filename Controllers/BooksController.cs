using BookStoreApi.Models;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _Services;
        public BooksController(IBookService Services)
        {
           _Services = Services;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string? search, int pageNumber = 1, int PageSize = 10)
        {

           
            var query = _Services.GetAll(search, pageNumber, PageSize);
            int totalCount = query.Result.Count;
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
                Data = query.Result
            });
        }



            [HttpGet("{id}")]
            public async Task<IActionResult> Get(int id)
            {
                var book = await _Services.GetById(id);
                if (book == null)
                    return NotFound();
                return Ok(book);
            }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Book dto)
        {
         
            var book = await _Services.Create(dto);

        
            return Ok(book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Book dto)
        {
            var book = await _Services.Update(id, dto);
            if (book == null)
                return NotFound();
            //Return 400
            var Category = await _Services.GetCategoryById(id,dto);
            if (Category == null)
                return BadRequest("Invalid CategoryId");
            return Ok(book);
        }

        

            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                var book = await _Services.Delete(id);
                if (book == null)
                    return NotFound();
                return Ok(book);
            }
        
    }


}

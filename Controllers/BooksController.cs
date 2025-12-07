using BookStoreApi.Dtos;
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

            try
            {
                // use await 
                var query = await _Services.GetAll(search, pageNumber, PageSize);
                int totalCount = query.Count;
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
                    data=query
                });
            }
            catch (Exception ex)
            { 
                return BadRequest("Error");
            }
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var book = await _Services.GetById(id);
                if (book == null)
                    return NotFound();
                return Ok(book);
            }
            catch (Exception ex)
            {
                return BadRequest("Error");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookDto dto)
        {
            try
            {
                var Book = await _Services.Create(dto);
                return Ok(Book);
            }catch(Exception ex)
            {
                return BadRequest("Error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BookDto dto)
        {
            try
            {
                var book = await _Services.Update(id, dto);
                    if (book == null)
                        return NotFound();
                //Return 400
                //var Category = await _Services.GetCategoryById(id, dto);
                //if (Category == null)
                //    return BadRequest("Invalid CategoryId");
                return Ok(book);
            }
            catch (Exception ex) 
            {
                return BadRequest("Error");
            }
        }

        

            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
            try
            {
                var book = await _Services.Delete(id);
                if (book == null)
                    return NotFound();
                return Ok(book);
            }catch(Exception ex)
            {
                return BadRequest("Delete failed");
            }
            }
        
    }


}

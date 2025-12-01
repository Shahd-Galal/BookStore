using BookStoreApi.Controllers;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BookStoreApi.Repositories
{
    public class BookRepository : IBookRepository
    {

        private readonly ApplicationDbContext _context;
        private int pageNumber;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;

        }

        public async Task<List<Book>> GetAll(string? search, int pageNumber = 0, int PageSize = 10)
        {
           var query =  _context.Books.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
            {
                query =  query.Where(b => b.Title.Contains(search) || b.Author.Contains(search));

            }


            //paging
            var totalCount = query.Count();
            var page = query.Skip((pageNumber - 1) * PageSize).Take(PageSize).ToList();

            //Handel
            return await query.ToListAsync();
        }

        public async Task<Book?> GetById(int id) => await _context.Books.FirstOrDefaultAsync(b => b.Id == id);

        public async Task Add(Book book) => await _context.Books.AddAsync(book);

        public Task Update(Book book)
        {
            _context.Books.Update(book);
            return Task.CompletedTask;
        }

        public Task Delete(Book book)
        {
            _context.Books.Remove(book);
            return Task.CompletedTask;
        }

        public async Task<List<Book>> SearchAndPaged(string? search, int page, int pageSize)
        {
            var query = _context.Books.Include(b => b.Category).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(b => b.Title.Contains(search)|| b.Author.Contains(search)|| b.Category.Name.Contains(search));
            }

            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return await query.ToListAsync();
            await _context.SaveChangesAsync();
        }

        public Task Save()
        {
            throw new NotImplementedException();
        }
    }
}

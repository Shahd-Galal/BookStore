

using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        private int pageNumber;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Category>>GetAll() => await _context.Categories.ToListAsync();

        public async Task<Category?> GetById(int id) => await _context.Categories.FindAsync(id);

        public async Task Add(Category category) => await _context.Categories.AddAsync(category);

        public Task Update(Category category)
        {
            _context.Categories.Update(category);
            return Task.CompletedTask;
        }

        public Task Delete(Category category)
        {
            _context.Categories.Remove(category);
            return Task.CompletedTask;
        }
        
        public async Task<bool>HasBooks (int categoryId) => await _context.Books.AnyAsync(b => b.CategoryId == categoryId);

        public async Task<List<Category>> SearchAndPaged (string? search , int page, int pageSize)
        {
            var query = _context.Categories.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search)) query = query.Where(c => c.Name.Contains(search)); 

            query = query.Skip((pageNumber -1) * pageSize).Take(pageSize);

            return await query.ToListAsync();
        }
        public async Task Save() => await _context.SaveChangesAsync();
    }
}

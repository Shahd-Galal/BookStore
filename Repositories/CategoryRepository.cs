

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
        public async Task<List<Category>> GetAll()
        {
            return await _context.Categories.Include(c => c.Books).ToListAsync();
                
        }

        public async Task<Category?> GetById(int id)
        {
            return await _context.Categories.Include(c => c.Books).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task Add(Category category)
        {
            await _context.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public Task Update(Category category)
        {
            _context.Categories.Update(category);
            return Task.CompletedTask;
        }

        public async Task<Task> Delete(Category category, int id)
        {
          await  _context.Categories.Include(c => c.Books).FirstOrDefaultAsync(c => c.Id == id);
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

        Task ICategoryRepository.Delete(Category category)
        {
            return Delete(category, category);

        }

        private async Task Delete(Category category1, Category category2)
        {
            throw new NotImplementedException();
        }
    }
}

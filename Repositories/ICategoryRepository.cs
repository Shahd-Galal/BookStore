namespace BookStoreApi.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAll();
        Task<Category> GetById(int id);
        Task Add(Category category);
        Task Update(Category category);
        Task Delete(Category category);
        Task<bool> HasBooks(int categoryId);
        Task<List<Category>> SearchAndPaged(string? search, int pageNumber, int pageSize);
        Task Save();

    }
}

namespace BookStoreApi.Services
{
    public interface ICategoryService
    {

        Task<List<Category>> GetAll();
        Task<Category?> GetById(int id);
        Task<bool> Create(Category dto);
        Task<bool> Update(int id, Category dto);
        Task<int> Delete(int id);
        Task<List<Category>> SearchAndPaged(string? search , int pageNumber, int pageSize);

    }
}

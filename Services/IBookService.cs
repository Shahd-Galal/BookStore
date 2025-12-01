namespace BookStoreApi.Services
{
    public interface IBookService
    {
        Task<List<Book>> GetAll(string? search, int pageNumber = 0, int PageSize = 10);
        Task<Book?> GetById(int id);
        Task<bool> Create(Book dto);
        Task<bool> GetCategoryById(int id, Book dto);
        Task<bool> Update(int id, Book dto);
        Task<bool> Delete(int id);
    }
}

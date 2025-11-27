namespace BookStoreApi.Repositories
{
    public interface IBookRepository
    {

        Task<List<Book>> GetAll();
        Task<Book?> GetById(int id);
        Task Add(Book book);
        Task Update(Book book);
        Task Delete(Book book);
        Task<List<Book>> SearchAndPaged(string? search, int pageNumber, int pageSize);
        Task Save();
    }
}

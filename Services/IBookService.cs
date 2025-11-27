namespace BookStoreApi.Services
{
    public interface IBookService
    {
        Task<List<CreateBookDto>> GetAll();
        Task<CreateBookDto?> GetById(int id);
        Task<bool> Create(CreateBookDto dto);
        Task<bool> Update(int id, CreateBookDto dto);
        Task<bool> Delete(int id);
        Task<List<CreateBookDto>> SearchAndPaged(string? search, int pageNumber, int pageSize);
    }
}

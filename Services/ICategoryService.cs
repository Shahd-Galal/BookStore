namespace BookStoreApi.Services
{
    public interface ICategoryService
    {

        Task<List<CreateCategoryDto>> GetAll();
        Task<CreateCategoryDto?> GetById(int id);
        Task<bool> Create(CreateCategoryDto dto);
        Task<bool> Update(int id, CreateCategoryDto dto);
        Task<int> Delete(int id);
        Task<List<CreateCategoryDto>> SearchAndPaged(string? search , int pageNumber, int pageSize);

    }
}

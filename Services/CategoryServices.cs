using BookStoreApi.Repositories;

namespace BookStoreApi.Services
{
    public class CategoryServices : ICategoryService
    {

        private readonly ICategoryRepository _repo;

        public CategoryServices(ICategoryRepository repo)
        
        { 
            _repo = repo;
        }

        public async Task<List<CreateCategoryDto>> GetAll()
        {
            var data = await _repo.GetAll();
            return data.Select(static c => new CreateCategoryDto
            {
                id = c.Id,
                Name = c.Name
            }).ToList();
        }

        public async Task<CreateCategoryDto?> GetById(int id)
        {
            var category = await _repo.GetById(id);
            if (category == null)
                return null;

            return new CreateCategoryDto
            {
                id = category.Id,
                Name = category.Name
            };
        }

        public async Task<bool> Create(CreateCategoryDto dto)
        {
            var category = new Category
            {
                Name = dto.Name
            };

            await _repo.Add(category);
            await _repo.Save();
            return true;
        }

        public async Task<bool> Update(int id, CreateCategoryDto dto)
        {
            var category = await _repo.GetById(id);
            if (category == null)
                return false;
            category.Name = dto.Name; 
            _repo.Update(category);
            await _repo.Save();
            return true;
        }

        public async Task<int> Delete(int id)
        {
            var category = await _repo.GetById(id);
            if (category == null)
                return 0;
            if (await _repo.HasBooks(id))
                return -1;
            _repo.Delete(category);
            await _repo.Save();
            return 1;
        }


        public async Task<List<CreateCategoryDto>> SearchAndPaged(string? search, int pageNumber, int pageSize)
        {
            var data = await _repo.SearchAndPaged(search, pageNumber, pageSize);

            return data.Select(c => new CreateCategoryDto
            {
                id = c.Id,
                Name = c.Name
            }).ToList();
        }

    }
}

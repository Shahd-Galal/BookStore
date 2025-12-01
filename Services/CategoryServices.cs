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

        public async Task<List<Category>> GetAll()
        {
            var data = await _repo.GetAll();

            return data.Select(static c => new Category
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
        }

        public async Task<Category?> GetById(int id)
        {
            var category = await _repo.GetById(id);
            if (category == null)
                return null;

            return new Category
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public async Task<bool> Create(Category dto)
        {


            if (dto.Name == null)
            {
                return false;
            }
            try
            {
                await _repo.Add(dto);
                return true;
            }
            catch (Exception ex) 
            {
                return false;
            }


         
        }

        public async Task<bool> Update(int id, Category dto)
        {

            try
            {
                var category = await _repo.GetById(id);
                if (category == null)
                    return false;
                category.Name = dto.Name;
                _repo.Update(category);
                await _repo.Save();
                return true;
            }
            catch (Exception ex)
            { 
                return true;
            }
        }

        public async Task<int> Delete(int id)
        {
            try
            {
                var category = await _repo.GetById(id);
                if (category == null)
                    return 0;
                if (await _repo.HasBooks(id))
                    return -1;
                await _repo.Save();
                return 1;
            }
            catch (Exception ex) 
            {
                return -2;
            }

        }


        public async Task<List<Category>> SearchAndPaged(string? search, int pageNumber, int pageSize)
        {
            var data = await _repo.SearchAndPaged(search, pageNumber, pageSize);

            return data.Select(c => new Category
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
        }

    }
}

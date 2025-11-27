using BookStoreApi.Repositories;

namespace BookStoreApi.Services
{
    public class BookServices : IBookService
    {
        private readonly IBookRepository _repo;

        public BookServices(IBookRepository repo)

        {
            _repo = repo;
        }

        public async Task<List<CreateBookDto>> GetAll()
        {
            var data = await _repo.GetAll();
            return data.Select(static b => new CreateBookDto
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
               // CategoryName = b.Category.Name
            }).ToList();
        }

        public async Task<CreateBookDto?> GetById(int id)
        {
            var bookDto = await _repo.GetById(id);
            if (bookDto == null)
                return null;

            return new CreateBookDto
            {
                Id = bookDto.Id,
                Title = bookDto.Title,
                Author = bookDto.Author,
                //CategoryName = book.Category.Name
            };
        }

        public async Task<bool> Create(CreateBookDto dto)
        {
            var bookDto = new Book
            {
                Title = dto.Title,
                Author = dto.Author,
                CategoryId = dto.CategoryId,
            };

            await _repo.Add(bookDto);
            await _repo.Save();
            return true;
        }

        public async Task<bool> Update(int id, CreateBookDto dto)
        {
            var bookDto = await _repo.GetById(id);
            if (bookDto == null)
                return false;

            bookDto.Title = dto.Title;
            bookDto.Author = dto.Author;
            bookDto.CategoryId = dto.CategoryId;
            _repo.Update(bookDto);
            await _repo.Save();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var bookDto = await _repo.GetById(id);
            if (bookDto == null)
                return false;
            _repo.Delete(bookDto);
            await _repo.Save();
            return true;
        }


        public async Task<List<CreateBookDto>> SearchAndPaged(string? search, int pageNumber, int pageSize)
        {
            var data = await _repo.SearchAndPaged(search, pageNumber, pageSize);

            return data.Select(b => new CreateBookDto
            {
             Id = b.Id,
             Title = b.Title,
             Author = b.Author,
            // CategoryName = b.Category.Name
            }).ToList();
        }
    }
}

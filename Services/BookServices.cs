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

        public async Task<List<Book>> GetAll(string? search, int pageNumber = 0, int PageSize = 10)
        {
            var data = await _repo.GetAll(search, pageNumber, PageSize);
            return data.Select(static b => new Book
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
            }).ToList();
        }

        public async Task<Book?> GetById(int id)
        {
            var bookDto = await _repo.GetById(id);
            if (bookDto == null)
                return null;

            return new Book
            {
                Id = bookDto.Id,
                Title = bookDto.Title,
                Author = bookDto.Author,
              
            };
        }

        public async Task<bool> Create(Book dto)
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

        public async Task<bool> GetCategoryById(int id, Book dto)
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




        public async Task<bool> Update(int id, Book dto)
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


        public async Task<List<Book>> SearchAndPaged(string? search, int pageNumber, int pageSize)
        {
            var data = await _repo.SearchAndPaged(search, pageNumber, pageSize);

            return data.Select(b => new Book
            {
             Id = b.Id,
             Title = b.Title,
             Author = b.Author,
           
            }).ToList();
        }

       
    }
}

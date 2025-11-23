namespace BookStoreApi.Dtos
{
    public class CreateBookDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public int CategoryId { get; set; }
    }
}

namespace BookStoreApi.Dtos
{
    public class BookDto
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int Year { get; set; }

        public int CategoryId { get; set; }
    }
}

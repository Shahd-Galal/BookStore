namespace BookStoreApi.Dtos
{
    public class CreateBookDto
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;  
        public int Year { get; set; }
        public int CategoryId { get; set; }
        public int Id { get; internal set; }
     
    }
}

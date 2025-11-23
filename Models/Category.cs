namespace BookStoreApi.Models
{
    public class Category
    {
        public int Id { get; set; }

        [MaxLength(length:100)]
        public string Name { get; set; }

        //Navigation property
        public List<Book> Books { get; set; }
        public Category()
        {
            Books = new List<Book>();
        }
    }
}

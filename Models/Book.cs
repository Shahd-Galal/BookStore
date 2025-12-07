using System.Text.Json.Serialization;

namespace BookStoreApi.Models
{
    public class Book
    {
            public int Id { get; set; }          
            public string Title { get; set; }
            public string Author { get; set; }
            public int Year { get; set; }

            // FK
            public int CategoryId { get; set; }

        // Navigation
        [JsonIgnore]
            public virtual Category? Category { get; set; }
    }
}

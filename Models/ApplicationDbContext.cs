using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        public DbSet<Category> Categories { get; set; }

        public DbSet<Book> Books { get; set; }
    }
}

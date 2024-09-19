using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReadingBookApi.Customized;
using ReadingBookApi.Model;

namespace ReadingBookApi.Context
{
    public class BookDbContext : IdentityDbContext<CustomUser>
    {
        public BookDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<T_Book> t_Books { get; set; }
        public DbSet<T_Review> t_Review_Ratings { get; set; }


    }
}

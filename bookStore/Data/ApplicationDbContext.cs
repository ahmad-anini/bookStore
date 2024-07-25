using bookStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace bookStore.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Category> categories { get; set; }
        public DbSet<Author> authors { get; set; }
        public DbSet<Book> books { get; set; }
        public DbSet<BookCategory> bookCategories { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Category>()
                .Property(x => x.Name).HasMaxLength(30);

            modelBuilder.Entity<Category>().HasIndex(x => x.Name).IsUnique(true);

            modelBuilder.Entity<BookCategory>()
                .HasKey(x => new { x.CategoryId, x.BookId });

            base.OnModelCreating(modelBuilder);
        }
    }
}

using Microsoft.EntityFrameworkCore;

namespace Library.WebAPI.Models
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {

        }

        public DbSet<Adress> Adresses { get; set; }
        public DbSet<AuthBook> AuthBooks { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=LibraryDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthBook>().HasQueryFilter(a => !a.IsDeleted);
            modelBuilder.Entity<Author>().HasQueryFilter(a => !a.IsDeleted);
            modelBuilder.Entity<Book>().HasQueryFilter(b => !b.IsDeleted);
            modelBuilder.Entity<Publisher>().HasQueryFilter(p => !p.IsDeleted);
        }

    }
}

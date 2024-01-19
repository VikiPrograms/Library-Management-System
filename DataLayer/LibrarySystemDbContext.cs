using BusinessLayer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class LibrarySystemDbContext : IdentityDbContext<User>
    {
       public LibrarySystemDbContext() : base()
        {

        }

        public LibrarySystemDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=PC\\SQLEXPRESS;Database=LibrarySystemDbContext;Trusted_Connection=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Name).IsUnique();

                modelBuilder.Entity<ReadingCard>()
            .Property(p => p.DateCreated)
            .HasConversion(new DateOnlyConverter())
            .HasColumnType("date")
            .IsRequired();

            //for the BooksGenres class if I choose to add it
            /*modelBuilder.Entity<BookGenre>()
            .HasKey(bg => new { bg.BookId, bg.GenreId });

            modelBuilder.Entity<BookGenre>()
            .HasOne(bg => bg.Book)
            .WithMany(b => b.BookGenres)
            .HasForeignKey(bg => bg.BookId);

            modelBuilder.Entity<BookGenre>()
            .HasOne(bg => bg.Genre)
            .WithMany(g => g.BookGenres)
            .HasForeignKey(bg => bg.GenreId);
            */
            base.OnModelCreating(modelBuilder);
        }

        public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
        {
            public DateOnlyConverter() : base(
                dateOnly => DateTime.Parse(dateOnly.ToString("yyyy-MM-dd")),
                dateTime => DateOnly.FromDateTime(dateTime.Date))
            {
            }
        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<ReadingCard> ReadingCards { get; set; }

    }
}

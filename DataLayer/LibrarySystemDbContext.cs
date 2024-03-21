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
                optionsBuilder.UseSqlServer("Server=PC\\SQLEXPRESS;Database=LibrarySystemDbContext;Trusted_Connection=True;TrustServerCertificate=True");
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.UserName).IsUnique();   

            modelBuilder.Entity<ReadingCard>()
            .Property(p => p.DateCreated)
            .HasConversion(new DateOnlyConverter())
            .HasColumnType("date")
            .IsRequired();

            modelBuilder.Entity<Book>()
            .Property(p => p.PublicationDate)
            .HasConversion(new DateOnlyConverter())
            .HasColumnType("date")
            .IsRequired();

            modelBuilder.Entity<Author>()
            .Property(a => a.AuthorId)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<Genre>()
            .Property(a => a.GenreId)
            .ValueGeneratedOnAdd();

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

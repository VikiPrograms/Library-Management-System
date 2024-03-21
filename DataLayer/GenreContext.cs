using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class GenreContext : IDb<Genre, int>
    {

        private LibrarySystemDbContext dbContext;

        public GenreContext(LibrarySystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(Genre item)
        {
            try
            {
                List<Book> books = new List<Book>(item.Books.Count);
                foreach (Book booky in item.Books)
                {
                    Book bookFromDb = await dbContext.Books.FindAsync(booky.ISBN);
                    if (bookFromDb != null)
                    {
                        books.Add(bookFromDb);
                    }
                    else
                    {
                        books.Add(booky);
                    }
                }
                item.Books = books;
                dbContext.Genres.Add(item);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(int key)
        {
            try
            {
                Genre genreFromDb = await ReadAsync(key, false, false);

                if (genreFromDb != null)
                {
                    dbContext.Genres.Remove(genreFromDb);
                    await dbContext.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentException("Genre with that id does not exist!");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Exists(int key)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Genre>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Genre> query = dbContext.Genres;

                if (useNavigationalProperties)
                {
                    query = query.Include(a => a.Books);
                }

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Genre> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Genre> query = dbContext.Genres;

                if (useNavigationalProperties)
                {
                    query = query.Include(a => a.Books);
                }

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.FirstOrDefaultAsync(a => a.GenreId == key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(Genre item, bool useNavigationalProperties = false)
        {
            try
            {
                Genre genreFromDb = await ReadAsync(item.GenreId, useNavigationalProperties, false);
                genreFromDb.Name = item.Name;

                if(genreFromDb == null)
                {
                    await CreateAsync(item);
                }

                if (useNavigationalProperties)
                {
                    List<Book> books = new List<Book>(item.Books.Count);

                    foreach (var book in item.Books)
                    {
                        Book bookFromDb = await dbContext.Books.FindAsync(book.ISBN);

                        if (bookFromDb is null)
                        {
                            books.Add(book);
                        }
                        else
                        {
                            books.Add(bookFromDb);
                        }
                    }
                    genreFromDb.Books = books;
                }
                await dbContext.SaveChangesAsync();
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}

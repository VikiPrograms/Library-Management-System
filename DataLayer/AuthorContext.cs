using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class AuthorContext : IDb<Author, int>
    {
        private readonly LibrarySystemDbContext dbContext;

        public AuthorContext(LibrarySystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task CreateAsync(Author item)
        {
            try
            {
                List<Book> books = new List<Book>(item.Books.Count);
                foreach (Book booky in item.Books)
                {
                    Book bookFromDb = await dbContext.Books.FindAsync(booky.ISBN);
                    if(bookFromDb != null)
                    {
                        books.Add(bookFromDb);
                    }
                    else
                    {
                        books.Add(booky);
                    }
                }
                item.Books = books;
                dbContext.Authors.Add(item);
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
                Author authorFromDb = await ReadAsync(key, false, false);

                if (authorFromDb != null)
                {
                    dbContext.Authors.Remove(authorFromDb);
                    await dbContext.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentException("Author with that id does not exist!");
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

        public async Task<List<Author>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Author> query = dbContext.Authors;

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

        public async Task<Author> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Author> query = dbContext.Authors;

                if (useNavigationalProperties)
                {
                    query = query.Include(a => a.Books);
                }

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.FirstOrDefaultAsync(a => a.AuthorId == key);//System.InvalidOperationException: 'A second operation was started on this context instance before a previous operation completed. This is usually caused by different threads concurrently using the same instance of DbContext. For more information on how to avoid threading issues with DbContext, see https://go.microsoft.com/fwlink/?linkid=2097913.'
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(Author item, bool useNavigationalProperties = false)
        {
            try
            {
                Author authorFromDb = await ReadAsync(item.AuthorId, useNavigationalProperties, false);
                authorFromDb.Name = item.Name;

                if (authorFromDb == null) 
                { 
                    await CreateAsync(item);
                }
                //dbContext.Entry(authorFromDb).CurrentValues.SetValues(item);//why have i written that??? i copy pasted it from mr Iliev's code and don't know wtf this is

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

                    authorFromDb.Books = books;
                }

                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

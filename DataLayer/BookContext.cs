using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class BookContext:IDb<Book, string>
    {
        private readonly LibrarySystemDbContext dbContext;

        public BookContext(LibrarySystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(Book item)
        {
            try
            {
                dbContext.Books.Add(item);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(string key)
        {
            try
            {
                //Book booksFromDb = await dbContext.Books.FindAsync(key, false, false);
                Book bookFromDb = await dbContext.Books.FindAsync(key);

                if (bookFromDb != null)
                {
                        dbContext.Books.Remove(bookFromDb);
                    await dbContext.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentException("This book does not exist!");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Exists(string key)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Book>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Book> query = dbContext.Books;                
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

        public async Task<Book> ReadAsync(string key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Book> query = dbContext.Books;

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.FirstOrDefaultAsync(b => b.ISBN == key);
            }
            catch (Exception) 
            { 
                throw;
            }
        }

        public async Task UpdateAsync(Book item, bool useNavigationalProperties = false)
        {
            try
            {
                Book bookFromDb = await ReadAsync(item.ISBN, useNavigationalProperties, false);
                bookFromDb.Title = item.Title;
                bookFromDb.Description = item.Description;
                bookFromDb.Pages = item.Pages;
                bookFromDb.PublicationDate = item.PublicationDate;
                bookFromDb.PickUpDate = item.PickUpDate;
                bookFromDb.ReturnDate = item.ReturnDate;
                bookFromDb.IsPickedUp = item.IsPickedUp;

                if (useNavigationalProperties)
                {                  
                    bookFromDb.Author = item.Author;                   
                    bookFromDb.Genre = item.Genre;                    
                    bookFromDb.ReadingCard = item.ReadingCard;
                    bookFromDb.ReadingCardId = item.ReadingCardId;
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

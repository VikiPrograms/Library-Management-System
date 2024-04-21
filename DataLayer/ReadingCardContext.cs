using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ReadingCardContext : IDb<ReadingCard, int>
    {
        private LibrarySystemDbContext dbContext;

        public ReadingCardContext(LibrarySystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(ReadingCard item)
        {
            try
            {
                ApplicationUser userFromDb = await dbContext.Users.FindAsync(item.User.UserName);
                if(userFromDb == null)
                {
                    item.User = userFromDb;
                }
                List<Book> books = new List<Book>(item.Books.Count);
                foreach (Book book in item.Books)
                {
                    Book bookFromDb = await dbContext.Books.FindAsync(book.ISBN);
                    if(bookFromDb == null)
                    {
                        books.Add(bookFromDb);
                    }
                    else
                    {
                        books.Add(book);
                    }
                    item.Books = books;
                    dbContext.ReadingCards.Add(item);
                    dbContext.SaveChanges();
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(int key)
        {
            try
            {
                ReadingCard readingCardFromDb = await ReadAsync(key, false, false);

                if(readingCardFromDb != null)
                {
                    dbContext.ReadingCards.Remove(readingCardFromDb);
                    dbContext.SaveChangesAsync();
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

        public async Task<List<ReadingCard>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<ReadingCard> query = dbContext.ReadingCards;

                if (useNavigationalProperties)
                {
                    query = query.Include(o => o.Books).Include(u => u.User);
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

        public async Task<ReadingCard> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<ReadingCard> query = dbContext.ReadingCards;

                if (useNavigationalProperties)
                {
                    query = query.Include(rc => rc.User).Include(rc => rc.Books);
                }
                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.FirstOrDefaultAsync(rc => rc.ReadingCardId == key);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task UpdateAsync(ReadingCard item, bool useNavigationalProperties = false)
        {
            try
            {
                ReadingCard readingCardFromDb = await ReadAsync(item.ReadingCardId, useNavigationalProperties, false);
                readingCardFromDb.BorrowedBooks = item.BorrowedBooks;
                readingCardFromDb.NumberOfOverwrites = item.NumberOfOverwrites;
                readingCardFromDb.DateCreated = item.DateCreated;
                readingCardFromDb.UserName = item.UserName;
                //how do i add the name of the user to be updated

                if (useNavigationalProperties)
                {
                    ApplicationUser userFromDb = await dbContext.Users.FindAsync(item.User.Id);

                    if (userFromDb != null)
                    {
                        readingCardFromDb.User = userFromDb;
                    }
                    else
                    {
                        readingCardFromDb.User = item.User;
                    }

                    List<Book> books = new List<Book>(item.Books.Count);
                    foreach (var book in item.Books)
                    {
                        Book bookFromDb = await dbContext.Books.FindAsync(book.ISBN);
                        if(bookFromDb!= null)
                        {
                            books.Add(bookFromDb);
                        }
                        else
                        {
                            books.Add(book);
                        }
                    }
                    readingCardFromDb.Books = books;
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

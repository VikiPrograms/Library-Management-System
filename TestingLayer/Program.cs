using System;
using System.Drawing.Drawing2D;
using BusinessLayer;
using DataLayer;

namespace TestingLayer
{
    public class Program
    {
        static LibrarySystemDbContext dbContext;
        static AuthorContext authorsContext;
        static BookContext booksContext;
        static GenreContext genresContext;
        static ReadingCardContext readingCardsContext;
        static void Main(string[] args)
        {
            try
            {
                dbContext = new LibrarySystemDbContext();
                authorsContext = new AuthorContext(dbContext);
                booksContext = new BookContext(dbContext);
                genresContext = new GenreContext(dbContext);
                readingCardsContext = new ReadingCardContext(dbContext);

                TestAuthorsContextCreate();
                TestAuthorsContextRead();
            }
            catch(Exception)
            {
                throw;
            }
        }

        static async void TestAuthorsContextCreate()
        {
            Console.WriteLine("Author added successfully :)");//read
            Author author = new Author("George Orwell");
            Console.WriteLine("Author added successfully :)");//read
            await authorsContext.CreateAsync(author);//gives the System.UnhandelException in the AuthorsContext Read() method
            Console.WriteLine("Author added successfully :)");//not read
        }

        static async void TestAuthorsContextRead()
        {
            Console.WriteLine("The info about the author has been red successfully!");//only this is read
            Author author1 = await authorsContext.ReadAsync(1);//gives THE SAME System.UnhandelException IN THE AUTHORSCONTEXT ERROR
            Console.WriteLine("The info about the author has been red successfully!");//not read
            Console.WriteLine(author1);
            Console.WriteLine("The info about the author has been red successfully!");//not read
        }

        static async void TestAuthorsContextReadAll()
        {
            IQueryable<Author> authors = (IQueryable<Author>)await authorsContext.ReadAllAsync();

            foreach(Author author in authors)
            {
                Console.WriteLine(author);
            }
        }
        /*
          static void TestAuthorContextUpdate()
        {
            Author authorFromDb = authorsContext.ReadAsync(3);
            Console.WriteLine("Before: ");
            Console.WriteLine(brandFromDb);

            brandFromDb.Name = "TOshko ot 11J";
            brandsContext.Update(brandFromDb);

            Brand updatedBrandFromDb = brandsContext.Read(3);
            Console.WriteLine("After: ");
            Console.WriteLine(updatedBrandFromDb);
        }
         */

    }
}
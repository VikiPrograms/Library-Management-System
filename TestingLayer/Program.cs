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

                TestAuthorsContextCreate().Wait();
                TestAuthorsContextRead().Wait();
                TestAuthorsContextReadAll();
                TestAuthorContextUpdate();
                TestAuthorContextDeleteAsync();
            }
            catch(Exception)
            {
                throw;
            }
        }

        static async Task TestAuthorsContextCreate()
        {
            Console.WriteLine("Author added successfully :)");//read
            Author author = new Author("George Orwell");
            Console.WriteLine("Author added successfully :)");//read
            await authorsContext.CreateAsync(author);//gives the System.UnhandelException in the AuthorsContext Read() method
            Console.WriteLine("Author added successfully :)");//not read
        }

        static async Task TestAuthorsContextRead()
        {
            Console.WriteLine("The info about the author has been red successfully!");//only this is read
            Author author1 = await authorsContext.ReadAsync(1);//gives THE SAME System.UnhandelException IN THE AUTHORSCONTEXT ERROR
            Console.WriteLine("The info about the author has been red successfully!");//not read
            Console.WriteLine(author1);
            Console.WriteLine("The info about the author has been red successfully!");//not read
        }

        static async Task TestAuthorsContextReadAll()
        {
            IEnumerable<Author> authors = await authorsContext.ReadAllAsync();

            foreach(Author author in authors)
            {
                Console.WriteLine(author);
            }
        }

        static async Task TestAuthorContextUpdate()
        {
            Author authorFromDb = await authorsContext.ReadAsync(3);
            Console.WriteLine("Before: ");
            Console.WriteLine(authorFromDb);

            authorFromDb.Name = "TOshko ot 11J";
            await authorsContext.UpdateAsync(authorFromDb);

            Author updatedAuthorFromDb = await authorsContext.ReadAsync(3);
            Console.WriteLine("After: ");
            Console.WriteLine(updatedAuthorFromDb);
        }
        static async Task TestAuthorContextDeleteAsync()
        {
            Console.Write("Id = ");
            int id = Convert.ToInt32(Console.ReadLine());
            Author authorFromDb = await authorsContext.ReadAsync(id);

            Console.WriteLine("Before: {0}", authorFromDb);
            await authorsContext.DeleteAsync(id);

            authorFromDb = await authorsContext.ReadAsync(id);

            if (authorFromDb == null)
            {
                Console.WriteLine($"Author with Id {id} deleted successfully!");
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
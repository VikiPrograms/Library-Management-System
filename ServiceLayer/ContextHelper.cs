using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public static class ContextHelper
    {
        private static LibrarySystemDbContext dbContext;
        private static AuthorContext authorContext;
        private static BookContext bookContext;
        private static GenreContext genreContext;
        private static ReadingCardContext readingCardContext;

        public static LibrarySystemDbContext GetDbContext()
        {
            if(dbContext == null)
            {
                SetDbContext();
            }

            return dbContext;
        }
        public static void SetDbContext()
        {
            dbContext = new LibrarySystemDbContext();
        }

        public static AuthorContext GetAuthorContext()
        {
            if(authorContext == null)
            {
                SetAuthorContext();
            }

            return authorContext;
        }
        public static void SetAuthorContext()
        {
            authorContext = new AuthorContext(GetDbContext());
        }

        public static BookContext GetBookContext()
        {
            if(bookContext == null)
            {
                SetBookContext();
            }
            return bookContext;
        }

        public static void SetBookContext()
        {
            bookContext = new BookContext(GetDbContext());
        }

        public static GenreContext GetGenreContext()
        {
            if(genreContext == null)
            {
                SetGenreContext();
            }
            return genreContext;
        }
        public static void SetGenreContext()
        {
            genreContext = new GenreContext(GetDbContext());
        }

        public static ReadingCardContext ReadingCardContext()
        {
            if(readingCardContext == null)
            {
                SetAuthorContext();
            }
            return readingCardContext;
        }
        public static void SetReadingCard()
        {
            readingCardContext = new ReadingCardContext(GetDbContext());
        }
    }
}

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

        public static LibrarySystemDbContext GetDbContext(bool useNewContext = false)
        {
            if(dbContext == null || useNewContext)
            {
                SetDbContext();
            }

            return dbContext;
        }
        public static void SetDbContext()
        {
            dbContext = new LibrarySystemDbContext();
        }

        public static AuthorContext GetAuthorContext(bool useNewContext = false)
        {
            if(authorContext == null || useNewContext)
            {
                SetAuthorContext();
            }

            return authorContext;
        }
        public static void SetAuthorContext()
        {
            authorContext = new AuthorContext(GetDbContext());
        }

        public static BookContext GetBookContext(bool useNewContext = false)
        {
            if(bookContext == null || useNewContext)
            {
                SetBookContext();
            }
            return bookContext;
        }

        public static void SetBookContext()
        {
            bookContext = new BookContext(GetDbContext());
        }

        public static GenreContext GetGenreContext(bool useNewContext = false)
        {
            if(genreContext == null || useNewContext)
            {
                SetGenreContext();
            }
            return genreContext;
        }
        public static void SetGenreContext()
        {
            genreContext = new GenreContext(GetDbContext());
        }

        public static ReadingCardContext GetReadingCardContext(bool useNewContext = false)
        {
            if(readingCardContext == null || useNewContext)
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

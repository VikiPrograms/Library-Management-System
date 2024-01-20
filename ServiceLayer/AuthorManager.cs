using BusinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class AuthorManager
    {
        private readonly IDb<Author, int> authorContext;

        public AuthorManager(IDb<Author, int> authorContext)
        {
            this.authorContext = authorContext;
        }

        public async Task CreateAsync(Author author)
        {
            // Validate data ... other logic eventually
            await authorContext.CreateAsync(author);
        }

        public async Task<Author> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await authorContext.ReadAsync(key, useNavigationalProperties, isReadOnly);
        }

        public async Task<ICollection<Author>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await authorContext.ReadAllAsync(useNavigationalProperties, isReadOnly);
        }

        public async Task UpdateAsync(Author author, bool useNavigationalProperties = false)
        {
            await authorContext.UpdateAsync(author, useNavigationalProperties);
        }

        public async Task DeleteAsync(int key)
        {
            await authorContext.DeleteAsync(key);
        }

    }
}

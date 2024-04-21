using BusinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class ReadingCardManager
    {
        private readonly ReadingCardContext readingCardContext;

        public ReadingCardManager(ReadingCardContext readingCardContext)
        {
            this.readingCardContext = readingCardContext;
        }

        public async Task CreateAsync(ReadingCard item)
        {
            await readingCardContext.CreateAsync(item);
        }

        public async Task<ReadingCard> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await readingCardContext.ReadAsync(key, useNavigationalProperties, isReadOnly);
        }

        public async Task<List<ReadingCard>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await readingCardContext.ReadAllAsync(useNavigationalProperties, isReadOnly);
        }
        public async Task UpdateAsync(ReadingCard item, bool useNavigationalProperties = false)
        {
            await readingCardContext.UpdateAsync(item, useNavigationalProperties);
        }

        public async Task DeleteAsync(int key)
        {
            await readingCardContext.DeleteAsync(key);
        }
    }
}

using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ReadingCardContext : IDb<ReadingCard, int>
    {
        public async Task CreateAsync(ReadingCard item)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int key)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<ReadingCard>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            throw new NotImplementedException();
        }

        public async Task<ReadingCard> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(ReadingCard item, bool useNavigationalProperties = false)
        {
            throw new NotImplementedException();
        }
    }
}

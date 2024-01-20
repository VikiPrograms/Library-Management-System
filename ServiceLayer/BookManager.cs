﻿using BusinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class BookManager
    {
        private readonly BookContext bookContext;

        public BookManager(BookContext bookContext)
        {
            this.bookContext = bookContext;
        }

        public async Task CreateAsync(Book book)
        {
            await bookContext.CreateAsync(book);
        }

        public async Task<Book> ReadAsync(string key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await bookContext.ReadAsync(key, useNavigationalProperties, isReadOnly);
        }

        public async Task<ICollection<Book>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await bookContext.ReadAllAsync(useNavigationalProperties, isReadOnly);
        }

        public async Task UpdateAsync(Book item, bool useNavigationalProperties = false)
        {
            await bookContext.UpdateAsync(item, useNavigationalProperties);
        }

        public async Task DeleteAsync(string key)
        {
            await bookContext.DeleteAsync(key);
        }

    }
}

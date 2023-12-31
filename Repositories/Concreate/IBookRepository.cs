﻿using Entities.DataTransferObject;
using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Concreate
{
    public interface IBookRepository : IRepositoryBase<Book>
    {
        void CreateOneBook(Book book);

        void UpdateOneBook(Book book);
        void DeleteOneBook(Book book);

       Task<PagedList<Book>> GetAllBooksAsync(BookParamets bookParamets, bool trackChanges);

        Task<Book> GetOneBooksIdAsync(int id, bool trackChange);
        void CreateOneBook(BookDtoForInsertion entity);
    }
}

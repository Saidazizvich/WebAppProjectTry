using Entities.DataTransferObject;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Concreate;
using Repositories.EfCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EfCore
{
    // eger sealed tamomlasak biz u zaman hic kalitim yapamicaz dikkat 
    public sealed class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateOneBook(Book book) => Insert(book);

        public void CreateOneBook(BookDtoForInsertion entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteOneBook(Book book) => Delete(book);





        public Book GetOneBooksId(int id, bool trackChange) => FindAllCondition(b => b.Id.Equals(id), trackChange).SingleOrDefault();


        public void UpdateOneBook(Book book) => Update(book);

        public async Task<PagedList<Book>> GetAllBooksAsync(BookParamets bookParamets, bool trackChanges) 
        {
            var books = await FindAll(trackChanges).FilterBooks(bookParamets.MinPrice, bookParamets.MaxPrice).Search(bookParamets.SearchTerm)
                .Sort(bookParamets.OrderBy)
                .OrderBy(b => b.Id).ToListAsync();

            
            
            return PagedList<Book>.ToPagedList(books,bookParamets.PageNumber,bookParamets.PageSize);
        }   
           

        public async Task<Book> GetOneBooksIdAsync(int id, bool trackChange) => await FindAllCondition(b => b.Id.Equals(id), trackChange).SingleOrDefaultAsync();
      
    }
}

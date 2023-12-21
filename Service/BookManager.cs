using AutoMapper;
using Entities.DataTransferObject;
using Entities.Exceptions;
using Entities.Exeptions;
using Entities.LinkModel;
using Entities.Models;
using Entities.RequestFeatures;
using Repositories.Concreate;
using Services.Concreate;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Services
{
    public class BookManager : IBookService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        private readonly IBookLinks _bookLinks;

        public BookManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper, IBookLinks bookLinks)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
            _bookLinks = bookLinks; 
        }

       

        public async Task<BookDto> CreateOneBookAsync(BookDtoForInsertion bookDto)
        {
            var entity = _mapper.Map<Book>(bookDto);
            _manager.Book.CreateOneBook(entity);
            await  _manager.SaveAsync();
            return _mapper.Map<BookDto>(entity);
        }

       

        public async Task DeleteOneBookAsync(int id, bool trackChanges)
        {
              var entity = await GetOneBookAndByIdCheckExists(id, trackChanges);  
            if (entity is null)
                 throw new BookNotFound(id);
            

            _manager.Book.DeleteOneBook(entity);
            _manager.SaveAsync();
        }

      

        public async Task<(BookDtoForUpdate bookDtoFor, Book book)> GetOneBookForPatchAsync(int id, bool trackChanges)
        {
            var book = await _manager.Book.GetOneBooksIdAsync(id, trackChanges);

                if(book is null)    
                   throw new BookNotFound(id);

            var bookDtofor = _mapper.Map<BookDtoForUpdate>(book);

              
            return (bookDtofor,book);
            
        }

      

        public async Task<BookDto> GetOneBooksByIdAsync(int id, bool trackChanges)
        {
            var entity = await GetOneBookAndByIdCheckExists(id, trackChanges);
            var book = await _manager.Book.GetOneBooksIdAsync(id, trackChanges);
            throw new BookNotFound(id);

            return _mapper.Map<BookDto>(book);
        }

        public async Task SaveChangesForPatchAsync(BookDtoForUpdate bookDtoForUpdate, Book book)
        {
                     _mapper.Map(bookDtoForUpdate, book);
           await _manager.SaveAsync();

        }

       

        public async Task UpdateOneBookAsync(int id, BookDtoForUpdate bookDto, bool trackChanges)
        {
        
            var entity = await GetOneBookAndByIdCheckExists(id, trackChanges);
            entity = _mapper.Map<Book>(bookDto);
             _manager.Book.Update(entity);
                 await _manager.SaveAsync();
        }

        public async Task<Book> GetOneBookAndByIdCheckExists(int id, bool trackChanges)
        {
            var entity = await _manager.Book.GetOneBooksIdAsync(id, trackChanges);
            if (entity is null)
                throw new BookNotFound(id);
            return entity;
        }

     
        public async Task<(LinkResponse linkResponse, MetaData metaData)> GetAllBooksAsync(LinkParametrs linkParametrs, bool trackChanges)
        {
            if (linkParametrs.BookParamets.ValidPriceRange)
                throw new PriceOutRangeBadRequestExeption();

            var booksWithMetaData = await _manager.Book.GetAllBooksAsync(linkParametrs.BookParamets, trackChanges);

            var bookDto = _mapper.Map<IEnumerable<BookDto>>(booksWithMetaData);


            var shapedData = _bookLinks.TryGenerateLinks(bookDto, linkParametrs.BookParamets.Field, linkParametrs.HttpContext);


            return (linkResponse: shapedData, metaData: booksWithMetaData.MetaData);
        }
    }
}

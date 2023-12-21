using Entities.DataTransferObject;
using Entities.LinkModel;
using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Concreate
{
    public interface IBookService
    {
       Task<(LinkResponse linkResponse , MetaData metaData)> GetAllBooksAsync(LinkParametrs linkParametrs, bool trackChanges);

       Task<BookDto> GetOneBooksByIdAsync( int id , bool trackChanges);

         Task <BookDto> CreateOneBookAsync(BookDtoForInsertion book);

        Task UpdateOneBookAsync( int id ,BookDtoForUpdate bookDto,bool trackChanges);

        Task DeleteOneBookAsync( int id ,bool trackChanges);

       Task<(BookDtoForUpdate bookDtoFor, Book book)> GetOneBookForPatchAsync(int id, bool trackChanges);  

        Task SaveChangesForPatchAsync(BookDtoForUpdate bookDtoForUpdate, Book book);
    }
}

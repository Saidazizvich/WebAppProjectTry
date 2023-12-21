using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace Repositories.EfCore.Extensions
{
    // eger Extensions class yazmak istersek static kelimasini kullanmamiz gerekiyor 
    public static class BookRepositoryExtensions
    {
        public static IQueryable<Book> FilterBooks(this IQueryable<Book> books, uint minPrice, uint maxPrice) =>
             books.Where(book => book.Price >= minPrice && book.Price <= maxPrice);

        public static IQueryable<Book> Search(this IQueryable<Book> books , string searchTerm)
        {
            if(string.IsNullOrWhiteSpace(searchTerm))
                return books;

            var lowerCaseTerm = searchTerm.ToLower();
            return books.Where(b=>b.Title.ToLower().Contains(lowerCaseTerm));
        }       


        public static IQueryable<Book> Sort(this IQueryable<Book> books, string orderByQueryString)
        {


            if (string.IsNullOrWhiteSpace(orderByQueryString))  
                 return books.OrderBy(b=>b.Id);



             // siralama islem biz yaptik yani biz burda gozal temiz islem becerdik
            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Book>(orderByQueryString);

        



            if (orderQuery is null)
                return books.OrderBy(b => b.Id);

            return books.OrderBy(orderQuery);
        }    
         
    }
}

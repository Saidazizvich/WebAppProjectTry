using Entities.DataTransferObject;
using Entities.LinkModel;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;
using Services.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookLinks : IBookLinks
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IDateShaping<BookDto> _dateShaping;
        public BookLinks(LinkGenerator linkGenerator, IDateShaping<BookDto> dateShaping)
        {
            _linkGenerator = linkGenerator;
            _dateShaping = dateShaping;
        }

        public LinkResponse TryGenerateLinks(IEnumerable<BookDto> bookDto, string fields, HttpContext httpContext)
        {
            var shapedBooks = ShapedData(bookDto, fields);
            if (ShouldGenerateLinks(httpContext))
                return ReturnLinkedBooks(bookDto, fields, httpContext, shapedBooks);
            return ReturnShapedBooks(shapedBooks);
        }

        private LinkResponse ReturnLinkedBooks(IEnumerable<BookDto> bookDto, string fields, HttpContext httpContext, List<Entity> shapedBooks)
        {
            var bookDtoList = bookDto.ToList();
            for(int index = 0; index < bookDtoList.Count; index++)
            {
                 var boolinks = CreateForBook(httpContext, bookDtoList[index],fields);
                shapedBooks[index].Add("Links", boolinks);
            }

            var bookCollection = new LinkCollectionWrapper<Entity>(shapedBooks);
               return new LinkResponse { Haslinks = true, LinkedEntities = bookCollection};
        }

        private List<Link> CreateForBook(HttpContext httpContext, BookDto bookDto, string fields)
        {
            // burda su on link uyusmasi yapiyoruz
            var links = new List<Link>()
            {
                new Link("a1","b1","c1"),
                new Link("a2","b2","c2"),
            };
            return links;
        }

        private LinkResponse ReturnShapedBooks(List<Entity> shapedBooks)
        {
            return new LinkResponse { ShapedEntities = shapedBooks };
        }

        private bool ShouldGenerateLinks(HttpContext httpContext)
        {
            var mediaType =(MediaTypeHeaderValue)httpContext.Items["AcceptHeaderMediaTpe"];
            return mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
        }

        private List<Entity> ShapedData(IEnumerable<BookDto> bookDto, string fields)
        {
          return _dateShaping.ShapeData(bookDto, fields).Select(b=>b.Entity).ToList();    
        }


    }
}

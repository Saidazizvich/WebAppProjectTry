using Entities.DataTransferObject;
using Entities.LinkModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Concreate
{
    public interface IBookLinks
    {
        LinkResponse TryGenerateLinks(IEnumerable<BookDto> bookDto, string fields, HttpContext httpContext);
    }
}

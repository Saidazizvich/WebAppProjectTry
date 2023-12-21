using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;


namespace Entities.DataTransferObject
{
    public record LinkParametrs 
    {
        public BookParamets BookParamets { get; init; }

        public HttpContext HttpContext { get; init; }

    }
}

using AutoMapper;
using Entities.DataTransferObject;
using Entities.Exeptions;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Presentation.ActionFilter;
using Services.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ServiceFilter(typeof(LogFilterAttribute),Order =2)]
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly IServiceManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;   

        // Dependencies Injection dir

        public BooksController(IServiceManager manager, ILoggerService logger,IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;   
        }


        [HttpGet]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task <IActionResult> GetAllBooksAsync([FromQuery]BookParamets bookParamets)
        {

            var linkparameters = new LinkParametrs()
            {
                BookParamets = bookParamets, 
                 HttpContext = HttpContext
            };    
           
                var result = await _manager.BookService.GetAllBooksAsync(linkparameters,false);

            Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(result.metaData));

            return result.linkResponse.Haslinks ?
            Ok(result.linkResponse.LinkedEntities) : Ok(result.linkResponse.ShapedEntities);
           
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOneBooks([FromRoute(Name = "id")] int id)
        {
            
                var book = await _manager.BookService.GetOneBooksByIdAsync(id, false);
                return Ok(book);

            if (book is null)
                throw new BookNotFound(id);
                return Ok(book);
           
        }


        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost]
        public async Task< IActionResult> CreatOneBooks([FromBody] BookDtoForInsertion bookDto)
        {
            var book = await _manager.BookService.CreateOneBookAsync(bookDto);

                return StatusCode(201, book);          
        }

        [HttpPut]
        public async Task< IActionResult> UpdateBook([FromRoute(Name = "id")] int id, [FromBody] BookDtoForUpdate bookDto)
        {



          
                if (bookDto is null)
                    return BadRequest();

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

               await _manager.BookService.UpdateOneBookAsync(id, bookDto, false);

                return NoContent();
           
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBook([FromRoute(Name = "id")] int id)
        {
            var entity = await _manager.BookService.GetOneBooksByIdAsync(id, false);

           
                if (entity is null)
                    return NotFound(new
                    {
                        Statuscode = 404,
                        message = $"Book with id:{id} cloud not found."

                    });

               await _manager.BookService.DeleteOneBookAsync(id, false);

                return NoContent();
          
        }

        [HttpPatch]
        public async Task<IActionResult> PatrialBookUpdate([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<BookDtoForUpdate> bookPatch)
        {
            if(bookPatch is null) 
                  return UnprocessableEntity(ModelState); 

              var reuslt = await _manager.BookService.GetOneBookForPatchAsync(id, false);

            bookPatch.ApplyTo(reuslt.bookDtoFor, ModelState);

            TryValidateModel(reuslt.bookDtoFor);

            if (ModelState.IsValid)
                return UnprocessableEntity(ModelState);

           await _manager.BookService.SaveChangesForPatchAsync(reuslt.bookDtoFor,reuslt.book);    
          

            return NoContent();
          
        }
    }
}

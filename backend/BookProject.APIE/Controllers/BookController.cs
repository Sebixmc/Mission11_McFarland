using BookProject.API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        
        private BookDbContext _bookDbContext;
        
        public BookController(BookDbContext context)
        {
            _bookDbContext = context;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            var something = _bookDbContext.Books.ToList();
            return something;
        }
    }
}
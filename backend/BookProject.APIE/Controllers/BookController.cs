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

        public IActionResult GetAllBooks(int pageHowMany = 5,int pageNum = 1)
        {
            var something = _bookDbContext.Books.ToList()
                .Skip((pageNum-1)*pageHowMany)
                .Take(pageHowMany)
                .ToList();

            var totalNumBooks = _bookDbContext.Books.Count();

            var someObject = new
            {
                Books = something,
                TotalNumBooks = totalNumBooks
            };
            return Ok(someObject);
        }
    }
}
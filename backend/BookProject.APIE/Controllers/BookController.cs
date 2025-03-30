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

        [HttpGet]
        public IActionResult GetAllBooks(int pageHowMany = 5,int pageNum = 1, [FromQuery] List<string>? category = null)
        {
            IQueryable<Book> query = _bookDbContext.Books.AsQueryable();
            if (category != null && category.Any())
            {
                query = query.Where(c => category.Contains(c.Category));
            }
            
            var totalNumBooks = query.Count();
            
            var something = query
                .Skip((pageNum-1)*pageHowMany)
                .Take(pageHowMany)
                .ToList();

            

            var someObject = new
            {
                Books = something,
                TotalNumBooks = totalNumBooks
            };
            return Ok(someObject);
        }
        
        
        [HttpGet("GetBookCategories")]
        public IActionResult GetBookCategories()
        {
            var bookCategories = _bookDbContext.Books
                .Select(b => b.Category)
                .Distinct()
                .ToList();
            
            return Ok(bookCategories);
        }
        
    }
}
using BookService.DTO;
using BookService.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Controllers
{
    [Route("api/rents")]
    [ApiController]
    public class RentsController : ControllerBase
    {
        private readonly IRentService _rentService;

        public RentsController(IRentService rentService)
        {
            _rentService = rentService;
        }

        [Authorize(Roles = "Admin,Librarian")]
        [HttpGet]
        public async Task<ActionResult<List<RentDTO>>> GetAll()
        {
            return await _rentService.GetAllRents();
        }

        [Authorize(Roles = "Admin,Librarian")]
        [HttpGet("{book-id:int}")]
        public async Task<ActionResult<List<RentDTO>>> GetByBookId(int bookId)
        {
            return await _rentService.GetRentsByBookId(bookId);
        }

        [Authorize(Roles = "Admin,Librarian")]
        [HttpGet("{username:string}")]
        public async Task<ActionResult<List<RentDTO>>> GetByUsername(string username)
        {
            return await _rentService.GetRentsByUsername(username);
        }

        [Authorize]
        [HttpGet("my")]
        public async Task<ActionResult<List<RentDTO>>> GetByMyUsername()
        {
            var username = User.Claims.First(c => c.Type == "Username").Value;
            return await _rentService.GetRentsByUsername(username);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<string>> Rent(RentBookDTO rent)
        {
            await _rentService.RentBook(rent);
            return Ok(string.Format("User: {0} successfully rented book with id: {1}", rent.Username, rent.BookId));
        }

        [Authorize(Roles = "Admin,Librarian")]
        [HttpPut("return")]
        public async Task<ActionResult<string>> Return(RentBookDTO rent)
        {
            await _rentService.ReturnBook(rent.BookId, rent.Username);
            return Ok(string.Format("User: {0} successfully returned book with id: {1}", rent.Username, rent.BookId));
        }
    }
}

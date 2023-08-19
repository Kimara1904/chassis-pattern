using BookService.DTO;
using BookService.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<AuthorDTO>>> GetAll()
        {
            return await _authorService.GetAllAuthors();
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<AuthorDTO>> GetById(int idAuhor)
        {
            return await _authorService.GetAuthor(idAuhor);
        }

        [Authorize(Roles = "Admin,Librarian")]
        [HttpPost]
        public async Task<ActionResult<AuthorDTO>> Create([FromForm] CreateAuthorDTO newAuthor)
        {
            var ret = await _authorService.CreateAuthor(newAuthor);
            return Ok(ret);
        }

        [Authorize(Roles = "Admin,Librarian")]
        [HttpPatch("{id:int}")]
        public async Task<ActionResult<AuthorDTO>> Update(int id, [FromForm] EditAuthorDTO newAuthorInfo)
        {
            var ret = await _authorService.UpdateAuthor(id, newAuthorInfo);
            return Ok(ret);
        }

        [Authorize(Roles = "Admin,Librarian")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _authorService.DeleteAuthor(id);
            return NoContent();
        }
    }
}

using JokesApi.DTOs.Author;
using JokesApi.Helpers;
using JokesApi.Models.CustomExceptionsModels;
using JokesApi.Services.Author;
using Microsoft.AspNetCore.Mvc;

namespace JokesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorsService _authorsService;
        public AuthorsController(IAuthorsService authorsService)
        {
            _authorsService = authorsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var authorList = await _authorsService.GetAllAsync();
            return Ok(authorList);
        }

        [HttpGet("{nameOrID}")]
        public async Task<IActionResult> GetByNameOrIdAsync(string nameOrId)
        {
            try
            {
                var author = await _authorsService.GetByNameOrIdAsync(nameOrId);
                return Ok(author);
            }
            catch (NoIdentifierFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateById(int id, [FromBody] AuthorUpdate authorUpdate)
        {
            try
            {
                var updatedAuthor = await _authorsService.UpdateAsync(id, authorUpdate);
                return Ok(updatedAuthor);
            }
            catch (NoIdentifierFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (DuplicateIdentifierException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AuthorCreate authorCreate)
        {
            try
            {
                var createdAuthor = await _authorsService.CreateAsync(authorCreate);
                return Ok(createdAuthor);
            }
            catch (DuplicateIdentifierException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var success = await _authorsService.DeleteAsync(id);
                if (success)
                {
                    return Ok(SuccessMessagesEnum.AuthorDeleted);
                }
                else
                {
                    return StatusCode(500);
                }
            }
            catch (NoIdentifierFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}

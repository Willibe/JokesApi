using JokesApi.DTOs.Joke;
using JokesApi.Helpers;
using JokesApi.Models.CustomExceptionsModels;
using JokesApi.Services.Joke;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace JokesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JokesController : ControllerBase
    {
        private readonly IJokesService _jokesService;
        public JokesController(IJokesService jokesService)
        {
            _jokesService = jokesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var jokeList = await _jokesService.GetAllAsync();
            return Ok(jokeList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var joke = await _jokesService.GetByIdAsync(id);
                return Ok(joke);
            }
            catch (NoIdentifierFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("filter/{languageId}")]
        public async Task<IActionResult> FilterByLanguage(string languageId)
        {
            var jokes = await _jokesService.FilterByLanguageAsync(languageId);
            return Ok(jokes);
        }

        [HttpGet("filter/{languageId}/{categoryId}")]
        public async Task<IActionResult> FilterByLanguage(string languageId, string categoryId)
        {
            var jokes = await _jokesService.FilterByLanguageAndCategoryAsync(languageId, categoryId);
            return Ok(jokes);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] JokeUpdate jokeUpdate)
        {
            try
            {
                var updatedJoke = await _jokesService.UpdateAsync(id, jokeUpdate);
                return Ok(updatedJoke);
            }
            catch (NoIdentifierFoundException ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] JokeCreate jokeCreate)
        {
            try
            {
                var createdJoke = await _jokesService.CreateAsync(jokeCreate);
                return Ok(createdJoke);
            }
            catch (NoIdentifierFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool success = await _jokesService.DeleteAsync(id);
                if (success)
                {
                    return Ok(SuccessMessagesEnum.JokeDeleted);
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

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePartially(int id, [FromBody] JokeUpdatePartially jokeUpdatePartially)
        {
            try
            {
                var updatedJoke = await _jokesService.UpdatePartially(id, jokeUpdatePartially);
                return Ok(updatedJoke);
            }
            catch (NoIdentifierFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

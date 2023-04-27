using JokesApi.DTOs.Category;
using JokesApi.DTOs.Language;
using JokesApi.Helpers;
using JokesApi.Models.CustomExceptionsModels;
using JokesApi.Services.Category;
using JokesApi.Services.Language;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JokesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguagesController : ControllerBase
    {
        private readonly ILanguagesService _languagesService;
        public LanguagesController(ILanguagesService languagesService)
        {
            _languagesService = languagesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var languageList = await _languagesService.GetAllAsync();
            return Ok(languageList);
        }

        [HttpGet("{nameOrId}")]
        public async Task<IActionResult> GetByNameOrIdAsync(string nameOrId)
        {
            try
            {
                var language = await _languagesService.GetByNameOrIdAsync(nameOrId);
                return Ok(language);
            }
            catch(NoIdentifierFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateById(int id, [FromBody] LanguageUpdate languageUpdate)
        {
            try
            {
                var updatedLanguage = await _languagesService.UpdateAsync(id, languageUpdate);
                return Ok(updatedLanguage);
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
        public async Task<IActionResult> Create([FromBody] LanguageCreate languageCreate)
        {
            try
            {
                var createdLanguage = await _languagesService.CreateAsync(languageCreate);
                return Ok(createdLanguage);
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
                var success = await _languagesService.DeleteAsync(id);
                if (success)
                {
                    return Ok(SuccessMessagesEnum.LanguageDeleted);
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

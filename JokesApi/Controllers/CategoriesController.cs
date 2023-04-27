using JokesApi.DTOs.Author;
using JokesApi.DTOs.Category;
using JokesApi.Helpers;
using JokesApi.Models.CustomExceptionsModels;
using JokesApi.Services.Author;
using JokesApi.Services.Category;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JokesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _categoriesService;
        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categoryList = await _categoriesService.GetAllAsync();
            return Ok(categoryList);
        }

        [HttpGet("{nameOrId}")]
        public async Task<IActionResult> GetByNameOrIdAsync(string nameOrId)
        {
            try
            {
                var category = await _categoriesService.GetByNameOrIdAsync(nameOrId);
                return Ok(category);
            }
            catch(NoIdentifierFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateById(int id, [FromBody] CategoryUpdate categoryUpdate)
        {
            try
            {
                var updatedCategory = await _categoriesService.UpdateAsync(id, categoryUpdate);
                return Ok(updatedCategory);
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
        public async Task<IActionResult> Create([FromBody] CategoryCreate categoryCreate)
        {
            try
            {
                var createdCategory = await _categoriesService.CreateAsync(categoryCreate);
                return Ok(createdCategory);
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
                var success = await _categoriesService.DeleteAsync(id);
                if (success)
                {
                    return Ok(SuccessMessagesEnum.CategoryDeleted);
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

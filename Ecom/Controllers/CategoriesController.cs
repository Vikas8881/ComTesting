#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Data;
using Ecommerce.Model;
using AutoMapper;
using Ecommerce.DTOs.Category;
using Ecommerce.Static;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly IMapper mapper;
        private readonly ILogger<CategoriesController> logger;
        public CategoriesController(AppDBContext context,IMapper mapper, ILogger<CategoriesController> logger)
        {
            _context = context;
            this.mapper = mapper;
            this.logger = logger;
        }

        // GET: api/Categories
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<CategoryReadDTO>>> GetCategories()
        {
            logger.LogInformation($"Request to {nameof(GetCategories)}");
            try
            {
                logger.LogInformation($"Successfully Get Categories");
                var category=await _context.Categories.ToListAsync();
                var categoryDTO = mapper.Map<IEnumerable<CategoryReadDTO>>(category);
                return Ok(categoryDTO);
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Error Performing Get in {nameof(GetCategories)}");
                return StatusCode(500,Messages.Error500Message);
            }
           
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDetailsDTO>> GetCategory(int id)
        {
            logger.LogInformation($"Request to {nameof(GetCategory)}");
            try
            {
                var categories = await _context.Categories.ProjectTo<CategoryDetailsDTO>(mapper.ConfigurationProvider).FirstOrDefaultAsync(a=>a.ID==id);
               
                if(categories == null)
                {
                    logger.LogWarning($"Record not found:{nameof(GetCategory)}");
                    return NotFound();
                }
                return categories;
            }
            catch (Exception e)
            {
                logger.LogError(e, $" Error Performing GET in {nameof(GetCategory)}");
                return StatusCode(500, Messages.Error500Message);
            }
          
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, CategoryUpdateDTO category)
        {
            if (id != category.ID)
            {
                logger.LogError($"Error Performing GET in {nameof(PutCategory)}-ID: {id}");

                return BadRequest();
            }
            var categories=await _context.Categories.FindAsync(id);
           if(categories == null)
            {
                logger.LogWarning($"No Record found with this {nameof(PutCategory)}-ID: {id}");

                return NoContent();
            }
           mapper.Map<CategoryUpdateDTO>(categories);
            _context.Entry(categories).State=EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                logger.LogInformation($"Data Updated Successfully {nameof(PutCategory)}-ID: {id}");

            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!await CategoryExists(id))
                {
                    logger.LogWarning($"No Record found with this {nameof(PutCategory)}-ID: {id}");

                    return NotFound();
                }
                else
                {
                    logger.LogError(e, $" Error Performing Update in {nameof(PutCategory)}");
                    return StatusCode(500, Messages.Error500Message);
                }
            }

            return NoContent();
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CategoryCreateDTO>> PostCategory(CategoryCreateDTO categoryDTO)
        {
            logger.LogInformation($"Category Create Attempted");
            try
            {
                var category = mapper.Map<Category>(categoryDTO);
                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetCategory", new { id = category.ID }, category);
            }
            catch (Exception e)
            {

                logger.LogError(e, $" Error Performing Post in {nameof(PostCategory)}");
                return StatusCode(500, Messages.Error500Message);
            }

          
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> CategoryExists(int id)
        {
            return await _context.Categories.AnyAsync(e => e.ID == id);
        }
    }
}

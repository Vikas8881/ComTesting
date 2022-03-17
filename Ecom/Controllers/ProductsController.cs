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
using Ecommerce.DTOs.Product;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Ecommerce.Static;
using AutoMapper.QueryableExtensions;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDBContext _context;
       
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ILogger<ProductsController> logger;

        public ProductsController(AppDBContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment, ILogger<ProductsController> logger)
        {
            _context = context;
            this.mapper = mapper;
            this.webHostEnvironment = webHostEnvironment;
            this.logger = logger;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductReadDTO>>> GetProducts()
        {
            logger.LogInformation($"Request to {nameof(GetProducts)}");
            try
            {
                var product = await _context.Products.ToListAsync();
              
                var products = mapper.Map<IEnumerable<ProductReadDTO>>(product);
                logger.LogInformation($"Successfully Get Products");
                return Ok(products);
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Error Performing Get in {nameof(GetProducts)}");
                return StatusCode(500, Messages.Error500Message);
            }
           
        }
        
        // GET: api/Products/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<ProductDetailsDTO>> GetProduct(int id)
        //{
        //    logger.LogInformation($"Request to {nameof(GetProduct)}");
        //    try
        //    {
        //        var products = await _context.Products.ProjectTo<ProductDetailsDTO>
        //        (mapper.ConfigurationProvider).FirstOrDefaultAsync(a=>a.ID == id);
        //        if(products == null)
        //        {
        //            logger.LogWarning($"Record Not Found: {nameof(GetProduct)}");
        //            return NotFound();
        //        }
        //        logger.LogWarning($"Successfully Get Record: {nameof(GetProduct)}");
        //        return Ok(products);
        //    }
        //    catch (Exception e)
        //    {
        //        logger.LogError(e, $" Error Performing GET in {nameof(GetProduct)}");
        //        return StatusCode(500, Messages.Error500Message);
        //    }
        //}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDetailsDTO>> GetProduct(int id)
        {
            logger.LogInformation($"Request to {nameof(GetProduct)}");
            try
            {
                var products = await _context.Products.ProjectTo<ProductDetailsDTO>(mapper.ConfigurationProvider).FirstOrDefaultAsync(a => a.ID == id);

                if (products == null)
                {
                    logger.LogWarning($"Record not found:{nameof(GetProduct)}");
                    return NotFound();
                }
                return products;
            }
            catch (Exception e)
            {
                logger.LogError(e, $" Error Performing GET in {nameof(GetProduct)}");
                return StatusCode(500, Messages.Error500Message);
            }

        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductUpdateDTO productUpdateDTO)
        {

            if (id != productUpdateDTO.ID)
            {
                logger.LogError($"Error Performing GET in {nameof(PutProduct)}-ID: {id}");

                return BadRequest();
            }
            var product = await _context.Products.FindAsync(id);
            if(product == null)
            {
                return NoContent();
                logger.LogWarning($"No Record found with this {nameof(PutProduct)}-ID: {id}");
            }
            mapper.Map(productUpdateDTO, product);
            _context.Entry(productUpdateDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                logger.LogInformation($"Data Updated Successfully {nameof(PutProduct)}-ID: {id}");
            }
            catch (DbUpdateConcurrencyException e)
            {
                if(!await ProductExists(id))
                {
                    logger.LogWarning($"No Record found with this {nameof(PutProduct)}-ID: {id}");
                    return NotFound();
                }
                else
                {
                    logger.LogError($"Error Performing Update in {nameof(PutProduct)}-ID: {id}");
                    return StatusCode(500, Messages.Error500Message);
                }
            }
            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductCreateDTO>> PostProduct(ProductCreateDTO productCreateDTO)
        {
            logger.LogInformation($"Product Create Attempted");
            try
            {
                var product=mapper.Map<Product>(productCreateDTO);
                product.thumbnail = CreateFile(productCreateDTO.ImageData, productCreateDTO.OriginalImageName);
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return CreatedAtAction("PostProduct", new { id = productCreateDTO.ID }, productCreateDTO);

            }
            catch (Exception e)
            {
                logger.LogError(e, $" Error Performing Post in {nameof(PostProduct)}");
                return StatusCode(500, Messages.Error500Message);
            }

        }
        private string CreateFile(string imageBase64, string Product_Image1)
        {
            var url = HttpContext.Request.Host.Value;
            var ext = Path.GetExtension(Product_Image1);
            var fileName = $"{Guid.NewGuid().ToString()}.{ext}";
            var path = $"{webHostEnvironment.WebRootPath}\\thumbnail\\{fileName}";
            byte[] image = Convert.FromBase64String(imageBase64);
            var fileStream = System.IO.File.Create(path);
            fileStream.Write(image, 0, image.Length);
            fileStream.Close();
            return $"https://{url}/thumbnail/{fileName}";
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> ProductExists(int id)
        {
            return await _context.Products.AnyAsync(e => e.Id == id);
        }
        //Loading Data from Store Procedure 
        [HttpGet("ViewAddImages/{ID:int}")]
        public async Task<IEnumerable<SP_GetViewimageRecordResult>> productForAddImages(int? ID)
        {
            return await _context.GetProcedures().SP_GetViewimageRecordAsync(ID);
        }

    }
}

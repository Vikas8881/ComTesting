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
using Ecommerce.DTOs.Images;
using Ecommerce.Static;
using Ecommerce.DTOs.Product;
using AutoMapper.QueryableExtensions;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImageController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ILogger<ProductImageController> logger;

        public ProductImageController(AppDBContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment, ILogger<ProductImageController> logger)
        {
            _context = context;
            this.mapper = mapper;
          
            this.logger = logger;
            this.webHostEnvironment = webHostEnvironment;
        }

        // GET: api/ProductImage
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImageReadDTO>>> GetProductImages()
        {
            logger.LogInformation($"Request to {nameof(GetProductImages)}");
            try
            {
                logger.LogInformation($"Successfully Get GetProductImages");
                var category = await _context.Product_Images.ToListAsync();
                var categoryDTO = mapper.Map<IEnumerable<ImageReadDTO>>(category);
                return Ok(categoryDTO);
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Error Performing Get in {nameof(GetProductImages)}");
                return StatusCode(500, Messages.Error500Message);
            }

        }

        // GET: api/ProductImage/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ImageDetailsDTO>> GetProductImage(int id)
        {
            logger.LogInformation($"Request to {nameof(GetProductImage)}");
            try
            {
                var ProductImage = await _context.Product_Images.ProjectTo<ImageDetailsDTO>(mapper.ConfigurationProvider).FirstOrDefaultAsync(a => a.ID == id);

                if (ProductImage == null)
                {
                    logger.LogWarning($"Record not found:{nameof(GetProductImage)}");
                    return NotFound();
                }
                return ProductImage;
            }
            catch (Exception e)
            {
                logger.LogError(e, $" Error Performing GET in {nameof(GetProductImage)}");
                return StatusCode(500, Messages.Error500Message);
            }

        }

        // PUT: api/ProductImage/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductImage(int id, ImageUpdateDTO ProductImage)
        {

            if (id != ProductImage.ID)
            {
                return BadRequest();
            }
            var pimage = await _context.Product_Images.FindAsync(id);
            if (pimage == null)
            {
                return NotFound();
            }
            if (string.IsNullOrEmpty(ProductImage.Product_Image1) == false)
            {
                ProductImage.Product_Image1 = CreateFile(ProductImage.ImageData,ProductImage.ImageData);
                var picName = Path.GetFileName(ProductImage.Product_Image1);
                var path = $"{webHostEnvironment.WebRootPath}\\productimage{picName}";
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }

            mapper.Map(ProductImage, pimage);

            _context.Entry(pimage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ProductImageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
          
        }

        // POST: api/ProductImage
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ImageCreateDTO>> PostProductImage(ImageCreateDTO ProductImage)
        {
            var pimage = mapper.Map<Product_Image>(ProductImage);
            pimage.Image = CreateFile(ProductImage.ImageData, ProductImage.OriginalImageName);
            _context.Product_Images.Add(pimage);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetProductImage", new { id = ProductImage.ID }, ProductImage);

        }

        // DELETE: api/ProductImage/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductImage(int id)
        {
            var ProductImage = await _context.Product_Images.FindAsync(id);
            if (ProductImage == null)
            {
                return NotFound();
            }

            _context.Product_Images.Remove(ProductImage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> ProductImageExists(int id)
        {
            return await _context.Product_Images.AnyAsync(e => e.Id == id);
        }
        private string CreateFile(string imageBase64, string Product_Image1)
        {
            var url = HttpContext.Request.Host.Value;
            var ext = Path.GetExtension(Product_Image1);
            var fileName = $"{Guid.NewGuid().ToString()}.{ext}";
            var path = $"{webHostEnvironment.WebRootPath}\\productimage\\{fileName}";
            byte[] image = Convert.FromBase64String(imageBase64);
            var fileStream = System.IO.File.Create(path);
            fileStream.Write(image, 0, image.Length);
            fileStream.Close();
            return $"https://{url}/productimage/{fileName}";
        }
    }
}

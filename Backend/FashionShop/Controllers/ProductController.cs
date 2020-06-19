using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FashionShop.Data;
using FashionShop.DTOs;
using FashionShop.Mapping;
using FashionShop.models;
using FashionShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Drawing.Imaging;

namespace FashionShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly EShopContext _context;
        private readonly IMapper _mapper;
        public ProductController(EShopContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        //get all products
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            var products = await _context.Products.Include(p => p.ListImages)
            .Select(p => _mapper.Map<ProductDTO>(p))
                                .ToListAsync();
            return products;
        }

        //get product image, name, price to display in index
        [HttpGet("index")]
        public async Task<ActionResult<IEnumerable<indexProductDTO>>> GetIndexProducts()
        {
            var products = await _context.Products
            .Include(p => p.ListImages)
            .Select(p => _mapper.Map<indexProductDTO>(p)).ToListAsync();
            foreach (var p in products)
            {
                p.Image = convertImage(p.Image);
            }
            return products;
        }

        //get product by id
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            var product = await _context.Products
            .Include(p => p.ListImages)
            .SingleOrDefaultAsync(p => p.Id == id);

            foreach (var img in product.ListImages)
            {
                img.Path = convertImage(img.Name);
            }
            return _mapper.Map<ProductDTO>(product);
        }

        //get image cart
        [HttpGet("img/{id}")]
        public ActionResult<String> GetImageCart(int id)
        {
            var img = "s_" + _context.ProductImages
                        .Where(i => i.ProductForeignKey == id)
                        .Select(i => i.Name)
                        .FirstOrDefault();
            img = convertImage(img);

            return img;
            //return product;
        }

        [HttpGet("image/{id}")]
        public ActionResult<String> GetImage(int id)
        {
            var img = _context.ProductImages
                        .Where(i => i.Id == id)
                        .Select(i => i.Name)
                        .FirstOrDefault();
            img = convertImage(img);

            return img;
            //return product;
        }

        //get name product
        [HttpGet("name/{id}")]
        public ActionResult<String> GetName(int id)
        {
            var name = _context.Products
                        .Where(p => p.Id == id)
                        .Select(p => p.Name)
                        .FirstOrDefault();

            return name;
            //return product;
        }

        //create product
        [HttpPost]
        public ActionResult<Product> CreateProduct(ProductDTO productDTO)
        {

            var product = _mapper.Map<Product>(productDTO);
            product.ListImages = null;
            _context.Products.Add(product);
            _context.SaveChanges();
            int id = product.Id;
            foreach (var image in productDTO.ListImages)
            {
                if (saveImage(image.Path, image.Name))
                {
                    image.Path = null;
                    image.ProductForeignKey = id;
                    _context.ProductImages.Add(image);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("error when upload image");
                }
            }
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
            //return product;
        }


        //updata product data
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            try
            {
                _context.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        private bool ProductExists(int id)
        {
            return _context.Products.Any(p => p.Id == id);
        }

        //delete product by id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }


        [HttpGet]
        [Route("type")]
        public async Task<ActionResult<IEnumerable<ProductType>>> getType()
        {
            return await _context.ProductType.ToListAsync();
        }


        //get and convert image to base64
        private string convertImage(string name)
        {
            string extension = Path.GetExtension(name);
            ImageBase64 img64 = GetExtension(name);
            string base64String = img64.Replace;
            try
            {
                string imageConvert = getPathImage() + name;
                if (System.IO.File.Exists(imageConvert))
                {
                    using (Image img = Image.FromFile(imageConvert))
                    {
                        if (img != null)
                        {
                            using (MemoryStream ms = new MemoryStream())
                            {
                                img.Save(ms, img64.FormatImage);
                                ms.Position = 0;
                                byte[] imageBytes = ms.ToArray();
                                base64String += Convert.ToBase64String(imageBytes);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
            return base64String;

        }

        private ImageBase64 GetExtension(string name)
        {
            string extension = Path.GetExtension(name);
            string replace = "data:image/jpeg;base64,";
            ImageFormat format = ImageFormat.Jpeg;
            switch (extension)
            {
                case "jpg":
                    replace = "data:image/jpg;base64,";
                    break;
                case "png":
                    replace = "data:image/png;base64,";
                    format = ImageFormat.Png;
                    break;
                default:
                    break;
            }
            return new ImageBase64
            {
                Replace = replace,
                FormatImage = format,
            };
        }
        //save image to App_Data
        private string getPathImage()
        {
            var imagePath = @"\App_Data\Upload\Images\";
            var fullPath = _env.ContentRootPath + imagePath;
            return fullPath;
        }
        private bool saveImage(string base64, string name)
        {
            var imageBase64 = GetExtension(name);
            string imageUpload = getPathImage() + name;
            string imageSmallUpload = getPathImage() + "s_" + name;
            string x = base64.Replace(imageBase64.Replace, "");
            byte[] imageBytes = Convert.FromBase64String(x);

            //System.IO.File.WriteAllBytes(uploadPath, imageBytes);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            Image imageSmall = resizeImage(image, 60, 80);
            
            image.Save(imageUpload, imageBase64.FormatImage);
            imageSmall.Save(imageSmallUpload, imageBase64.FormatImage);
            
            return true;
        }
        public Image resizeImage(Image img, int width, int height)
        {
            Bitmap b = new Bitmap(width, height);
            Graphics g = Graphics.FromImage((Image)b);

            g.DrawImage(img, 0, 0, width, height);
            g.Dispose();

            return (Image)b;
        }

    }
}
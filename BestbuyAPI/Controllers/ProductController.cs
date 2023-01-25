using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BestbuyAPI.Models;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BestbuyAPI.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _db = new AppDbContext();
        private static readonly HttpClient http = new HttpClient();


        #region GET actions
        //GET: "api/products" || retrieve list of all products
        public async Task<ActionResult<List<Product>>> Get()
        {
            return await _db.Products.OrderBy(x => x.Name).ToListAsync();
        }

        //GET: "api/products/{id}" || get single product by product id
        //One parameter: "{id}" - product id
        //Returns 404 message if no product exists
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            return await _db.Products.FindAsync(id);
        }

        //GET: "api/products/{product_name} || get single product by product name
        //Returns 404 if no product exists
        [HttpGet("{productName}")]
        public async Task<ActionResult<Product>> Get(string productName)
        {
            return await _db.Products.SingleOrDefaultAsync(p => p.Name == productName);
        }
        #endregion

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> PutProduct(int id, Product product)
        {
            if (id != product.ProductID) return BadRequest();

            _db.Products.Update(product);

            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Post(Product product)
        {
            var search = _db.Products.Find(product.Name);
            if (search != null) return search;

            _db.Products.Add(product);
            var result = await _db.SaveChangesAsync();

            return Created($"api/products/{product.ProductID}", product);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = _db.Products.Find(id);
            if (product == null) return null;

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();

            return null;
        }
        
    }
}


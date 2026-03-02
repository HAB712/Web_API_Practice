using api.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdController : ControllerBase
    {
        public static List<Product> prodlist = new List<Product>()
        {
            new Product { Id = 1, Name = "Laptop", Category = "Electronics", Price = 150000, StockQuantity = 5},
            new Product { Id = 2, Name = "Headphones", Category = "Electronics", Price = 5000, StockQuantity = 10},
            new Product { Id = 3, Name = "Chair", Category = "Furniture", Price = 8000, StockQuantity = 0}
        };
        private static int _nextid = 4;


          // ===============  Add Product ===================== //

        [HttpPost]
        public ActionResult Createprod(Product product)
        {
            if (product == null)
                return BadRequest("Invalid product data.");

            product.Id = _nextid++;

            prodlist.Add(product);
            return Ok("Product Successfully Added");
        }

        // ===============  Show Product List ===================== //

        [HttpGet]
        public ActionResult ShowList()
        {
            return Ok(prodlist);
        }

        // ===============  Show Specific Product\ ===================== //

        [HttpGet("{id}")]
        public ActionResult Sp_Prod(int id)
        {
            var product = prodlist.FirstOrDefault(p => p.Id == id);

            if (product == null)
                return BadRequest("product data not found.");

            return Ok(product);
        }


        // ===============  Edit Product ===================== //

        [HttpPut("editprod/{id}")]
        public ActionResult EditProd(int id,[FromBody] int quantityChange)
        {
            var product = prodlist.FirstOrDefault(p => p.Id == id);

            if (product == null)
                return BadRequest("product not found.");

            product.StockQuantity += quantityChange;

            if (product.StockQuantity < 0) product.StockQuantity = 0;

            return Ok(product);
        }

        // ===============  Delete Product ===================== //

        [HttpDelete("{id}")]
        public ActionResult DeleteProd(int id)
        {
            var product = prodlist.FirstOrDefault(p => p.Id == id);

            if (product == null)
                return BadRequest("product data not found.");

            prodlist.Remove(product);
            return Ok("Product deleted successfully.");
        }

    }
}

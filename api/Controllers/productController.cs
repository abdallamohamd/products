using api.dto;
using api.model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class productController : ControllerBase
    {
        private readonly appcontext appcontext;

        public productController(appcontext appcontext)
        {
            this.appcontext = appcontext;
        }

        [HttpGet("id")]
        public IActionResult get(int id)
        {
            product product = appcontext.products.FirstOrDefault(x => x.ProductId == id);
            productdto productdto = new productdto();
            productdto.ProductId = product.ProductId;
            productdto.Description = product.Description;
            productdto.Price = product.Price;
            productdto.Name = product.Name;
            productdto.SupplierId = product.SupplierId;
            productdto.QuantityInStock = product.QuantityInStock;
            return Ok(productdto);
        }
        [HttpGet]
        public IActionResult all()
        {
            List<product> products = appcontext.products.ToList();
            List<productdto> productsdto = new List<productdto>();
            
            foreach (product item in products)
            {
                productdto productdto1 = new productdto();
                productdto1.SupplierId = item.SupplierId;
                productdto1.ProductId = item.ProductId;
                productdto1.Price = item.Price;
                productdto1.Description = item.Description;
                productdto1.Name = item.Name;
                productdto1.QuantityInStock = item.QuantityInStock;
                productsdto.Add(productdto1);
            }
            return Ok(productsdto);
        }
        [HttpPut("up")]
        public IActionResult edite(int id,productdto productdto)
        {
            product product= appcontext.products.FirstOrDefault(x=>x.ProductId==id);
           if(product != null)
            {
                product.Description = productdto.Description;
                product.Name = productdto.Name;
                product.SupplierId = productdto.SupplierId;
                product.Price = productdto.Price;
                product.QuantityInStock = productdto.QuantityInStock;
                appcontext.SaveChanges();
                return Ok();

            }
            return BadRequest("this id not found ");
        }

        [HttpPost ("ad")]
        public IActionResult add(productdto productdto)
        {
            product product = new product();
            product.SupplierId = productdto.SupplierId;
            product.Description= productdto.Description;    
            product.Name= productdto.Name;
            product.Price= productdto.Price;    
            product.QuantityInStock= productdto.QuantityInStock;
            appcontext.products.Add(product);
            appcontext.SaveChanges();
            return Ok();
        }
        [HttpDelete("del")]
        public IActionResult delete(int id)
        {
            product product = appcontext.products.FirstOrDefault(x => x.ProductId == id);
            if(product != null)
            {
                appcontext.Remove(product);
                appcontext.SaveChanges(); 
                return Ok();
            }
            return BadRequest("this id not found ");
        }



    }
}

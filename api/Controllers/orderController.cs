using api.dto;
using api.model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class orderController : ControllerBase
    {
        private readonly appcontext appcontext;

        public orderController(appcontext appcontext)
        {
            this.appcontext = appcontext;
        }
        [HttpGet("p")]
        public IActionResult get(int id)
        {
           order order = appcontext.orders.FirstOrDefault(x => x.Id == id);
            return Ok(order);
        }
        [HttpGet]
        public IActionResult all()
        {
            List<order> orders= appcontext.orders.ToList();
            return Ok(orders);
        }
        [HttpPost]
        public IActionResult add(orderdto orderdto)
        {
            if (ModelState.IsValid==true)
            {

                order order = new order();
                order.orederdate = DateTime.Now;
                order.quantity = orderdto.quantity;
                order.productid = orderdto.productid;
                product product = appcontext.products.FirstOrDefault(x => x.ProductId == orderdto.productid);

                if (orderdto.quantity != null && orderdto.quantity < product.QuantityInStock )
                {
                    product.QuantityInStock -= orderdto.quantity;
                    order.price= orderdto.quantity * product.Price;
                    appcontext.orders.Add(order);
                    appcontext.SaveChanges();
                    return Created();
                }
                return BadRequest("quantity is not valid");
            }
            return BadRequest();
        }

        [HttpPut]
        public IActionResult edite(int id ,orderdto orderdto)
        {
            if (ModelState.IsValid == true)
            {
                order order = appcontext.orders.FirstOrDefault(x => x.Id == id);

                order.productid = orderdto.productid;

                product product = appcontext.products.FirstOrDefault(x => x.ProductId == orderdto.productid);

                int difference =  orderdto.quantity - order.quantity;

                if (difference != null && difference < product.QuantityInStock)
                {
                    product.QuantityInStock -= difference;
                    order.quantity = orderdto.quantity;
                    order.price = product.Price * orderdto.quantity;
                    appcontext.SaveChanges();
                    return Ok();
                }
            }
            return BadRequest(ModelState);
        }
        [HttpDelete]
        public IActionResult delete(int id)
        {
            order order = appcontext.orders.FirstOrDefault(x => x.Id == id);

            if( order != null)
            {
                appcontext.Remove(order);
                appcontext.SaveChanges();
                return Ok();
            }
            return BadRequest("not found");
        }

    }
}

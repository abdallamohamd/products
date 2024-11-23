using api.dto;
using api.model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class supplierController : ControllerBase
    {
        private readonly appcontext appcontext;

        public supplierController(appcontext appcontext)
        {
            this.appcontext = appcontext;
        }

        [HttpGet("g")]
        public IActionResult get(int id)
        {
            supplier supplier =appcontext.suppliers.FirstOrDefault(x=>x.supplierid==id);
            supplierdto supplierdto = new supplierdto();
            supplierdto.supplierid =supplier.supplierid;
            supplierdto.address = supplier.address;
            supplierdto.suppliername = supplier.suppliername;
            supplierdto.phone= supplier.phone;

            return Ok(supplierdto);
        }
        [HttpGet]
        public IActionResult all()
        {
            List<supplier> suppliers=appcontext.suppliers.Include(x=>x.products).ToList();
            List<supplierdto> supplierdtos=new List<supplierdto>();
            foreach(supplier item in suppliers)
            {
                supplierdto supplierdto=new supplierdto();
                supplierdto.phone=item.phone;
                supplierdto.address=item.address;
                supplierdto.supplierid=item.supplierid;
                supplierdto.suppliername=item.suppliername;
                supplierdto.p_count = item.products.Count();
                supplierdto.products=item.products.Select(x=>x.Name).ToArray();
                supplierdtos.Add(supplierdto);
            }
            return Ok(supplierdtos);
        }
        [HttpPut("up")]
        public IActionResult edite(int id, supplierdto supplierdto)
        {
            supplier supplier= appcontext.suppliers.FirstOrDefault(x=>x.supplierid==id);
            if (supplier != null)
            {
                supplier.address=supplierdto.address;   
                supplier.suppliername=supplierdto.suppliername;
                supplier.phone=supplierdto.phone;
                appcontext.SaveChanges();
                return Ok();
            }
            return BadRequest("not found");
        }
        [HttpPost]
        public IActionResult add(supplierdto supplierdto)
        {
            supplier supplier = new supplier(); 
            supplier.suppliername = supplierdto.suppliername;
            supplier.address = supplierdto.address;
            supplier.phone = supplierdto.phone;
            appcontext.suppliers.Add(supplier);
            appcontext.SaveChanges();
            return Created();
        }
        [HttpDelete("del")]
        public IActionResult delete(int id) 
        {
            supplier supplier =appcontext.suppliers.FirstOrDefault(x=>x.supplierid == id);  
            if(supplier != null)
            {
                appcontext.suppliers.Remove(supplier);
                appcontext.SaveChanges();
                return Ok();
            }
            return BadRequest("not found");
        }



    }
}

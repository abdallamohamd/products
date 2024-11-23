using api.dto;
using api.model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class accountController : ControllerBase
    {
        private readonly UserManager<appuser> userManager;
        private readonly IConfiguration configuration;

        public accountController(UserManager<appuser> userManager,IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        [HttpPost("reg")]
        public async Task <IActionResult> regester(regesterdto regesterdto)
        {
            if (ModelState.IsValid)
            {
                appuser appuser= new appuser();
                appuser.adderss = regesterdto.address;
                appuser.PhoneNumber = regesterdto.phone;
                appuser.UserName = regesterdto.name;
                appuser.PasswordHash = regesterdto.paasword;
                IdentityResult result=  await userManager.CreateAsync(appuser, regesterdto.paasword);
                if(result.Succeeded)
                {
                    return Ok();
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        public async Task <IActionResult> log( logdto logdto) 
        {
            if (ModelState.IsValid==true)
            {
                appuser appuser = await userManager.FindByNameAsync(logdto.name);
                if (appuser != null)
                {
                    bool found = await userManager.CheckPasswordAsync(appuser, logdto.password);
                    if (found==true)
                    {
                        List<Claim> claims = new List<Claim>();
                        claims.Add(new Claim (ClaimTypes.NameIdentifier,appuser.Id));
                        claims.Add(new Claim(ClaimTypes.Name,appuser.UserName));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()));
                        var roles =await userManager.GetRolesAsync(appuser); 
                        foreach(var role in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role,role));
                        }

                        var sinkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:key"]));

                        SigningCredentials sin = new SigningCredentials(sinkey,SecurityAlgorithms.HmacSha256);

                        JwtSecurityToken mytoken = new JwtSecurityToken(
                            audience: configuration["jwt:adu"],
                            issuer: configuration["jwt:issu"],
                            expires:DateTime.Now.AddHours(1),
                            claims: claims,
                            signingCredentials :sin
                            );
                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                            expiration = DateTime.Now.AddHours(1),
                        });
                    }
                }
                ModelState.AddModelError("", "name or password in valid");
            }
            return BadRequest(ModelState);
        }

    }
}

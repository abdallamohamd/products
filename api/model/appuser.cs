using Microsoft.AspNetCore.Identity;

namespace api.model
{
    public class appuser :IdentityUser
    {
         public string?  adderss {  get; set; }
    }
}

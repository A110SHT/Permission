using Microsoft.AspNetCore.Mvc;
using PermissionRepo.DataLayers;

namespace PermissionRepo.Controllers
{
    //[Authorize]
    public class UserController : BaseController
    {
        public UserController(MyDatabase myDatabase) : base(myDatabase)
        {
                
        }
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}

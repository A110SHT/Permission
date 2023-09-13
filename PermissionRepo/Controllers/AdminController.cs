using Microsoft.AspNetCore.Mvc;
using PermissionRepo.DataLayers;

namespace PermissionRepo.Controllers
{
    //[Authorize]
    public class AdminController :BaseController
    {
        public AdminController(MyDatabase myDatabase) : base(myDatabase)
        {
            
        }
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}

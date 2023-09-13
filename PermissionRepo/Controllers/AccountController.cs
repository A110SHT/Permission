using Microsoft.AspNetCore.Mvc;
using PermissionRepo.DataLayers;

namespace PermissionRepo.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(MyDatabase myDatabase) :base(myDatabase)
        {
            
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string username)
        {

            /// you can just simply use cookie authenthication. for login , use [authroize] is controller
            if(username == null)
            {
                StaticGlobalValue.RoleId = 0;
                return View();
            }
            else if(username =="admin")
            {
                StaticGlobalValue.RoleId = 1;
                return RedirectToAction("dashboard", "admin");
            }
            else if (username == "user")
            {
                StaticGlobalValue.RoleId = 2;
                return RedirectToAction("dashboard", "user");
            }
            else if (username == "superadmin")
            {
                StaticGlobalValue.RoleId = 3;
            }
            return View();
           
        }
        [HttpGet]
        public IActionResult Logout()
        {
            StaticGlobalValue.RoleId = 0;
            return RedirectToAction("Login");
        }


    }
}

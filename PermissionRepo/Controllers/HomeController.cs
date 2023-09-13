using Microsoft.AspNetCore.Mvc;
using PermissionRepo.DataLayers;
using PermissionRepo.Models;
using System.Data;
using System.Diagnostics;

namespace PermissionRepo.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        public readonly MyDatabase myDatabase;
        public HomeController(ILogger<HomeController> logger, MyDatabase myDatabase):base(myDatabase)
        {
            _logger = logger;
            this.myDatabase = myDatabase;
        }

        public IActionResult Index()
        {


          

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
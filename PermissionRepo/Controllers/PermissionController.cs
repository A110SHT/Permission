using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PermissionRepo.DataLayers;
using PermissionRepo.Models;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace PermissionRepo.Controllers
{
    //[Authorize]
    public class PermissionController : BaseController
    {
        private readonly IActionDescriptorCollectionProvider provider;
        public readonly MyDatabase myDatabase;
        public PermissionController(IActionDescriptorCollectionProvider provider, MyDatabase myDatabase):base(myDatabase)
        {
            this.provider = provider;
            this.myDatabase = myDatabase;
        }
        [HttpPost]
        public async Task<IActionResult> SavePermission(RoleMangeModelSummary rolesummary)
        {
            foreach (var item in rolesummary.RoleManageModels)
            {
                var mdl = new RoleManageModel()
                {
                    Id=item.Id,
                    RoleId = rolesummary.RoleID,
                    CAId = item.CAId,
                    HasAccess = item.HasAccess
                };
                if (item.Id == 0)
                {
                    myDatabase.RoleManageModel.Add(mdl);
                    await myDatabase.SaveChangesAsync();
                }
                else
                {
                    myDatabase.RoleManageModel.Update(mdl);
                    await myDatabase.SaveChangesAsync();
                }
            }
            return RedirectToAction("GivePermission");
        }



        [HttpGet]
        public async Task<IActionResult> GivePermission(int role)
        {

            List<ViewRoleManageModel> lst = (
                   from ca in myDatabase.ControllerAction
                   join rm in myDatabase.RoleManageModel on ca.Id equals rm.CAId
                   where (rm.RoleId == role)

                   select new ViewRoleManageModel
                   {
                       CAId = ca.Id,
                       Id = rm.Id,
                       ControllerName = ca.Controller,
                       DisplayName = ca.Action,
                       URl = ca.ActionUrl,
                       HasAccess = rm.HasAccess
                   }).ToList();
            if (lst == null || lst.Count == 0)
            {
                var list = await myDatabase.ControllerAction.ToListAsync();
                lst = new List<ViewRoleManageModel>();
                foreach (var item in list)
                {
                    lst.Add(new ViewRoleManageModel()
                    {
                        CAId = item.Id,
                        ControllerName = item.Controller,
                        DisplayName = item.Action,
                        URl = item.ActionUrl,
                    });
                }


            }
            RoleMangeModelSummary roleMangeModelSummary = new();
            roleMangeModelSummary.RoleManageModels = lst;
            roleMangeModelSummary.RoleID=role;

            return View(roleMangeModelSummary);
        }

        [HttpGet]
        //[Route("/permission/getallactionurl")]
        public async Task<IActionResult> GetAllActionUrl()
        {

            // you can get route url also
            var routes = provider.ActionDescriptors.Items.
                Where(p => p.DisplayName.Contains("PermissionRepo.Controllers")).Select(x => new
                {
                    Action = x.RouteValues["Action"],
                    Controller = x.RouteValues["Controller"]
                }).ToList();
            string con = string.Empty;
            ViewBag.Clear = 1;
            RoleManageModel objRoleManageModel = new RoleManageModel();
            List<string> list = new List<string>();
            string json = JsonSerializer.Serialize(routes);
            List<ControllerActionInfo> lstControllerActions = JsonSerializer.Deserialize<List<ControllerActionInfo>>(json);
            string[] publiccontroller = new string[] { "Account", "Home" }; // just remove the public controller 
            foreach (var item in lstControllerActions)
            {
                if(!publiccontroller.Contains(item.Controller))
                {
                    item.ActionUrl = $"/{item.Controller}/{item.Action}";
                    item.ActionUrl = item.ActionUrl.ToLower();
                    myDatabase.ControllerAction.Add(item);
                    await myDatabase.SaveChangesAsync();
                }
          
            }
            return RedirectToAction("GivePermission");
        }

    }
}

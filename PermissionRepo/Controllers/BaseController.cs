using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PermissionRepo.DataLayers;
using PermissionRepo.Models;

namespace PermissionRepo.Controllers
{
    public class BaseController : Controller
    {
        public readonly MyDatabase myDatabase;
        public BaseController(MyDatabase myDatabase)
        {
            this.myDatabase = myDatabase;
        }
        [NonAction]
        private void FillPermissionList()
        {
            if(StaticGlobalValue.ListPermission!=null || StaticGlobalValue.ListPermission.Count == 0)
            {// i just fill the static list from database. when project load. its your choice when you fill the list
                List<ViewRoleManageModel> lst = (
                from ca in myDatabase.ControllerAction
                join rm in myDatabase.RoleManageModel on ca.Id equals rm.CAId
                select new ViewRoleManageModel
                {
                    CAId = ca.Id,
                    Id = rm.Id,
                    RoleID=rm.RoleId,
                    ControllerName = ca.Controller,
                    DisplayName = ca.Action,
                    URl = ca.ActionUrl,
                    HasAccess = rm.HasAccess
                }).ToList();

                StaticGlobalValue.ListPermission = lst;

            }
            
        }
        [NonAction]
        private bool HasPermission(string path,int roleid)
        {

            /// if super admin just give all access;
            if (StaticGlobalValue.RoleId == 3)
            {
                return true;
            }
            path = path.ToLower();
            string[] publiccontroller = new string[] { "account", "home","" };
            string controller = path.Split('/')[1];
            /// retrun true for public controller
            if (publiccontroller.Contains(controller.ToLower()))
            {
                return true;
            }
            /// you can use memorycache here for store all urlwithroled
            /// 
            foreach (var item in StaticGlobalValue.ListPermission)
            {
                if(item.URl== path && item.RoleID==roleid && item.HasAccess)
                {
                    return true;
                }
            } 
            return false;
        }
        [NonAction]
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            FillPermissionList();
            if (!HasPermission(context.HttpContext.Request.Path, StaticGlobalValue.RoleId))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "App",
                    action = "Unauthorize"
                }));
                return;
            }

            await next();

        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace PermissionRepo.Models
{
    public class RoleMangeModelSummary
    {
        public int RoleID { get; set; }
        public List<ViewRoleManageModel> RoleManageModels { get; set; } = new List<ViewRoleManageModel>();
    }

    public class RoleManageModel
    {
        public int Id { get; set; }
        public int CAId { get; set; }
        public int RoleId { get; set; }
        public bool HasAccess { get; set; } 

    }
    public class ViewRoleManageModel
    {
        [Key]
        public int Id { get; set; }
        public string ControllerName { get; set; }
        public int ControllerID { get; set; }
        public bool IsActive { get; set; }
        //public bool IsDeleted { get; set; }
        public int RoleID { get; set; }
        public bool HasAccess { get; set; }
        public int ACLKeyID { get; set; }
        public string DisplayName { get; set; }
        public string URl { get; set; }
       /// <summary>
       /// public bool Status { get; set; }
       /// </summary>
        public int CAId { get; set; }
       // public int GeneralID { get; set; }
        //public int RoleID { get; set; }
        //public string GeneralRoleName { get; set; }
    }
}

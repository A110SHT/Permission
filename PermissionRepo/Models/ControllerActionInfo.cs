using System.ComponentModel.DataAnnotations;

namespace PermissionRepo.Models
{
    public class ControllerActionInfo
    {
        [Key]
        public int Id { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string ActionUrl { get;set; }
    }
}

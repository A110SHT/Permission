using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PermissionRepo.Models;
using System;

namespace PermissionRepo.DataLayers
{
    public class MyDatabase: DbContext
    {
        public MyDatabase(DbContextOptions<MyDatabase> options): base(options)
        { }

        public DbSet<ControllerActionInfo> ControllerAction { get; set; }   
        public DbSet<RoleManageModel> RoleManageModel { get; set; }   
    }
}

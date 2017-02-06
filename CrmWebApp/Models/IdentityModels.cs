using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CrmWebApp.Models
{
    // 可以通过向 ApplicationUser 类添加更多属性来为用户添加配置文件数据。若要了解详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=317594。
    public class ApplicationUser : IdentityUser
    {
        public string TrueName { get; set; }
        
        public string QQ { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // 请注意，authenticationType 必须与 CookieAuthenticationOptions.AuthenticationType 中定义的相应项匹配
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // 在此处添加自定义用户声明
            return userIdentity;
        }
    }

    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }
        public ApplicationRole(string name) : base(name) { }
        public ApplicationRole(string name, string description,string parentRole)
            : this(name)
        {
            this.Description = description;
            this.ParentRole = parentRole;
        }

        public string Description { get; set; }//角色表里新增加的字段
        public string ParentRole { get; set; }
    }



    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public new IDbSet<ApplicationRole> Roles { get; set; }//一定要重写这个方法，不然能用，网页中也能获取数据，就是代码里点不出来~~

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }    
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CrmWebApp.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "电子邮件")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "代码")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "记住此浏览器?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "记住我?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "用户名")]
        [RegularExpression(@"^[a-zA-Z_0-9]+$", ErrorMessage = "用户名不能有特殊字符")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "姓名")]
        [Required]
        public string TrueName { get; set; }
        [Required]
        [Display(Name = "手机")]
        [Phone]
        public string PhoneNumber { get; set; }
        [Display(Name = "QQ")]
        public string QQ { get; set; }
        [Required]
        [Display(Name = "邮箱")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "区域")]
        public string ServeAreaName { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }
    }

    public class EditUserViewModel
    {
        public EditUserViewModel() { }

        public EditUserViewModel(ApplicationUser user)
        {
            this.UserName = user.UserName;
            this.TrueName = user.TrueName;
            this.PhoneNumber = user.PhoneNumber;
            this.QQ = user.QQ;
            this.Email = user.Email;
        }

        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }
        [Display(Name = "姓名")]
        [Required]
        public string TrueName { get; set; }
        [Required]
        [Display(Name = "手机")]
        [Phone]
        public string PhoneNumber { get; set; }
        [Display(Name = "QQ")]
        public string QQ { get; set; }
        [Required]
        [Display(Name = "邮箱")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "区域")]
        public string ServeAreaName { get; set; }
    }

    public class EditRoleViewModel
    {
        public EditRoleViewModel() { }
        public EditRoleViewModel(ApplicationRole role)
        {
            this.RoleId = role.Id;
            this.RoleName = role.Name;
            this.Description = role.Description;
            this.ParentRole = role.ParentRole;
        }
        [Display(Name = "ID")]
        public string RoleId { get; set; }
        [Required]
        [Display(Name = "角色名")]
        public string RoleName { get; set; }
        [Display(Name = "描述")]
        public string Description { get; set; }
        [Display(Name = "父角色")]
        public string ParentRole { get; set; }

    }

    public class SelectRoleEditorViewModel
    {
        public bool Selected { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public string ParentRole { get; set; }

        public SelectRoleEditorViewModel() { }
        public SelectRoleEditorViewModel(ApplicationRole role)
        {
            this.RoleId = role.Id;
            this.RoleName = role.Name;
            this.Description = role.Description;
            this.ParentRole = role.ParentRole;
        }
    }

    public class SelectUserRolesViewModel
    {
        public List<SelectRoleEditorViewModel> RoleEditorViews { get; set; }
        public string UserName { get; set; }
        public string TrueName { get; set; }

        public SelectUserRolesViewModel()
        {
            if (this.RoleEditorViews == null)
            {
                this.RoleEditorViews = new List<SelectRoleEditorViewModel>();
            }
        }

        public SelectUserRolesViewModel(ApplicationUser user)
        {
            if (this.RoleEditorViews == null)
            {
                this.RoleEditorViews = new List<SelectRoleEditorViewModel>();
            }

            this.UserName = user.UserName;
            this.TrueName = user.TrueName;

            ApplicationDbContext db = new ApplicationDbContext();
            var allRoles = db.Roles;
            foreach (var role in allRoles)
            {
                var rvm = new SelectRoleEditorViewModel(role);
                this.RoleEditorViews.Add(rvm);
            }
            foreach (var userRole in user.Roles)
            {
                var checkUserRole = this.RoleEditorViews.Find(r => r.RoleId == userRole.RoleId);
                checkUserRole.Selected = true;
            }
        }
    }
}

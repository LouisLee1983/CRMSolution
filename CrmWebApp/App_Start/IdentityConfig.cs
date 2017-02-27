using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using CrmWebApp.Models;
using System.Configuration;
using System.Diagnostics;
using System.Net.Mail;
using System.Text;

namespace CrmWebApp
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // 在此处插入电子邮件服务可发送电子邮件。
            //<mail RequireValid="true" SmtpServer="smtp.exmail.qq.com" SmtpPort="25" EmailUserName="你的名字" 
            //EmailAddress ="xxx@xx.xxx" EmailPwd="xxx" EnableSSL="false" EnablePwdCheck="false" />
            MailConfig mailConfig = new MailConfig();
            mailConfig.EnableSSL = true;
            mailConfig.RequireValid = true;
            mailConfig.SmtpServer = "smtp.qq.com";
            mailConfig.SmtpPort = 25;
            mailConfig.EmailAddress = "425451886@qq.com";
            mailConfig.EmailPwd = "cpkehayuptjibgbc";
            mailConfig.EmailUserName = "jinyuan.li";
            mailConfig.EnablePwdCheck = false;

            if (mailConfig.RequireValid)
            {
                // 设置邮件内容  
                var mail = new MailMessage(
                    new MailAddress(mailConfig.EmailAddress, mailConfig.EmailUserName),
                    new MailAddress(message.Destination)
                    );
                mail.Subject = message.Subject;
                mail.Body = message.Body;
                mail.IsBodyHtml = true;
                mail.BodyEncoding = Encoding.GetEncoding(936);    //邮件正文的编码， 设置不正确， 接收者会收到乱码
                // 设置SMTP服务器  
                var smtp = new SmtpClient(mailConfig.SmtpServer, mailConfig.SmtpPort);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(mailConfig.EmailAddress, mailConfig.EmailPwd);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                smtp.SendMailAsync(mail);
            }

            return Task.FromResult(0);
        }
    }

    public class MailConfig : ConfigurationSection
    {
        /// <summary>  
        /// 注册时是否需要验证邮箱  
        /// </summary>  
        [ConfigurationProperty("RequireValid", DefaultValue = "false", IsRequired = true)]
        public bool RequireValid
        {
            get
            {
                return (bool)this["RequireValid"];
            }
            set
            {
                this["RequireValid"] = value;
            }
        }
        /// <summary>  
        /// SMTP服务器  
        /// </summary>  
        [ConfigurationProperty("SmtpServer", IsRequired = true)]
        public string SmtpServer
        {
            get
            {
                return (string)this["SmtpServer"];
            }
            set
            {
                this["SmtpServer"] = value;
            }
        }
        /// <summary>  
        /// 默认端口25（设为-1让系统自动设置）  
        /// </summary>  
        [ConfigurationProperty("SmtpPort", DefaultValue = "25", IsRequired = true)]
        public int SmtpPort
        {
            get
            {
                return (int)this["SmtpPort"];
            }
            set
            {
                this["SmtpPort"] = value;
            }
        }
        /// <summary>  
        /// 地址  
        /// </summary>  
        [ConfigurationProperty("EmailAddress", IsRequired = true)]
        public string EmailAddress
        {
            get
            {
                return (string)this["EmailAddress"];
            }
            set
            {
                this["EmailAddress"] = value;
            }
        }
        /// <summary>  
        /// 账号  
        /// </summary>  
        [ConfigurationProperty("EmailUserName", IsRequired = true)]
        public string EmailUserName
        {
            get
            {
                return (string)this["EmailUserName"];
            }
            set
            {
                this["EmailUserName"] = value;
            }
        }
        /// <summary>  
        /// 密码  
        /// </summary>  
        [ConfigurationProperty("EmailPwd", IsRequired = true)]
        public string EmailPwd
        {
            get
            {
                return (string)this["EmailPwd"];
            }
            set
            {
                this["EmailPwd"] = value;
            }
        }
        /// <summary>  
        /// 是否使用SSL连接  
        /// </summary>  
        [ConfigurationProperty("EnableSSL", DefaultValue = "false", IsRequired = false)]
        public bool EnableSSL
        {
            get
            {
                return (bool)this["EnableSSL"];
            }
            set
            {
                this["EnableSSL"] = value;
            }
        }
        /// <summary>  
        ///   
        /// </summary>  
        [ConfigurationProperty("EnablePwdCheck", DefaultValue = "false", IsRequired = false)]
        public bool EnablePwdCheck
        {
            get
            {
                return (bool)this["EnablePwdCheck"];
            }
            set
            {
                this["EnablePwdCheck"] = value;
            }
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // 在此处插入 SMS 服务可发送短信。
            return Task.FromResult(0);
        }
    }

    // 配置此应用程序中使用的应用程序用户管理器。UserManager 在 ASP.NET Identity 中定义，并由此应用程序使用。
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // 配置用户名的验证逻辑
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // 配置密码的验证逻辑
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
            };

            // 配置用户锁定默认值
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // 注册双重身份验证提供程序。此应用程序使用手机和电子邮件作为接收用于验证用户的代码的一个步骤
            // 你可以编写自己的提供程序并将其插入到此处。
            manager.RegisterTwoFactorProvider("电话代码", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "你的安全代码是 {0}"
            });
            manager.RegisterTwoFactorProvider("电子邮件代码", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "安全代码",
                BodyFormat = "你的安全代码是 {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole, string> roleStore) : base(roleStore)
        {
        }
        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            return new ApplicationRoleManager(new RoleStore<ApplicationRole>(context.Get<ApplicationDbContext>()));
        }
    }

    // 配置要在此应用程序中使用的应用程序登录管理器。
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }

    public class IdentityManager
    {
        // Swap ApplicationRole for IdentityRole:
        RoleManager<ApplicationRole> _roleManager = new RoleManager<ApplicationRole>(
            new RoleStore<ApplicationRole>(new ApplicationDbContext()));

        UserManager<ApplicationUser> _userManager = new UserManager<ApplicationUser>(
            new UserStore<ApplicationUser>(new ApplicationDbContext()));

        ApplicationDbContext _db = new ApplicationDbContext();


        public bool RoleExists(string name)
        {
            return _roleManager.RoleExists(name);
        }


        public bool CreateRole(string name, string description = "", string parentRole = "")
        {
            // Swap ApplicationRole for IdentityRole:
            var idResult = _roleManager.Create(new ApplicationRole(name, description, parentRole));
            return idResult.Succeeded;
        }


        public bool CreateUser(ApplicationUser user, string password)
        {
            var idResult = _userManager.Create(user, password);
            return idResult.Succeeded;
        }


        public bool AddUserToRole(string userId, string roleName)
        {
            var idResult = _userManager.AddToRole(userId, roleName);
            return idResult.Succeeded;
        }


        public void ClearUserRoles(string userId)
        {
            var user = _userManager.FindById(userId);
            var currentRoles = new List<IdentityUserRole>();

            currentRoles.AddRange(user.Roles);
            foreach (var role in currentRoles)
            {
                ApplicationRole ar = _roleManager.FindById(role.RoleId);
                _userManager.RemoveFromRole(userId, ar.Name);
            }
        }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Web.Security;
namespace FEPVWebApiOwinHost.Models
{
    // 用作 AccountController 操作的参数的模型。



    public class ChangePasswordBindingModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "当前密码")]
        public string OldPassword { get; set; }


        public string UserID { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "新密码")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认新密码")]
        [Compare("NewPassword", ErrorMessage = "新密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }
    }

    public interface IMembershipService
    {

        bool ChangePassword(string userName, string oldPassword, string newPassword);
    }

    public class AccountMembershipService : IMembershipService
    {
        private readonly MembershipProvider _provider;
        public AccountMembershipService()
            : this(null)
        {
        }

        public AccountMembershipService(MembershipProvider provider)
        {
            _provider = provider ?? Membership.Provider;
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("值不能为 null 或为空。", "userName");
            if (String.IsNullOrEmpty(oldPassword)) throw new ArgumentException("值不能为 null 或为空。", "oldPassword");
            if (String.IsNullOrEmpty(newPassword)) throw new ArgumentException("值不能为 null 或为空。", "newPassword");

            // 在某些出错情况下，基础 ChangePassword() 将引发异常，
            // 而不是返回 false。
            try
            {
                MembershipUser currentUser = _provider.GetUser(userName, true /* userIsOnline */);
                return currentUser.ChangePassword(oldPassword, newPassword);
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (MembershipPasswordException)
            {
                return false;
            }
        }
    }

    public class UpdatePass
    {

        public string username { set; get; }
        public string oldP { set; get; }

        public string newPassword { set; get; }

    }
}

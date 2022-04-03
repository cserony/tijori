using ESOL_BO.DbAccess.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESOL_EDU.Helper
{
    public class AppHelper
    {
        public static UserDomain UserInfo()
        {
            UserDomain userInfo = new UserDomain();
            if (HttpContext.Current.Session["UserInfo"] != null)
            {
                userInfo = HttpContext.Current.Session["UserInfo"] as UserDomain;
                return userInfo;
            }
            else
            {
                return null;
            }
        }
        public static int OrgId
        {
            get { return UserInfo().OrgId; }
        }
        public static int BranchId
        {
            get
            {
                return UserInfo().BranchId;
            }
        }
        public static int AppId
        {
            get
            {
                return (int)HttpContext.Current.Session["AppId"];
            }
        }
        public static int RoleId
        {
            get { return UserInfo().RoleId; }
        }
        public static int UserId
        {
            get { return UserInfo().UserId; }
        }
        public static int EmpId
        {
            get { return UserInfo().EmpId; }
        }
        public static string UserName
        {
            get { return UserInfo().UserName; }
        }
        public static string Name
        {
            get { return UserInfo().Name; }
        }
        public static string RoleName
        {
            get { return UserInfo().RoleName; }
        }
        public static string OrgName
        {
            get { return UserInfo().OrgName; }
        }
        public static string OrgAddress
        {
            get { return UserInfo().AddressLine; }
        }
        public static string BranchName
        {
            get { return UserInfo().BranchName; }
        }
        public static bool IsActive
        {
            get { return UserInfo().IsActive; }
        }
        public static DateTime LastActivityDate
        {
            get { return DateTime.Now; }
        }
        public static string CreatedBy
        {
            get { return UserInfo().UserName; }
        }
        public static string UpdatedBy
        {
            get { return UserInfo().UserName; }
        }
        public static DateTime CreatedDate
        {
            get { return DateTime.Now.Date; }
        }
        public static DateTime UpdatedDate
        {
            get { return DateTime.Now.Date; }
        }
        public static bool CheckPermission(string controller, string action)
        {
            var menuList = HttpContext.Current.Session["MenuList"] as List<UserMenuDomain>;
            if (menuList == null)
            {
                return false;
            }
            else
            {
                var controllerName = menuList.Where(e => e.ControllerName == controller);
                foreach (var menu in controllerName)
                {
                    if (action == "Create" && menu.IsCreate == 1)
                    {
                        return true;
                    }
                    if (action == "Edit" && menu.IsEdit == 1)
                    {
                        return true;
                    }
                    if (action == "Delete" && menu.IsDelete == 1)
                    {
                        return true;
                    }
                    if (action == "View" && menu.IsView == 1)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}
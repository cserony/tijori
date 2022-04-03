using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ESOL_BO.Ecommerce;
using ESOL_BO.DbAccess;
using System.Web.Security;

namespace ESOL_EDU.Helper
{
    public class CustomerHelper
    {
        public static EcommerceCustomerDomain UserInfo()
        {
            EcommerceCustomerDomain userInfo = new EcommerceCustomerDomain();
            if (HttpContext.Current.Session["CustomerPortal"] != null)
            {
                userInfo = HttpContext.Current.Session["CustomerPortal"] as EcommerceCustomerDomain;
                return userInfo;
            }
            else
            {
                return null;
            }
        }
        public static int CustomerId
        {
            get
            {
                return UserInfo().CustomerId;
            }
        }
        public static string CustomerName
        {
            get { return UserInfo().CustomerName; }
        }
        public static string Email
        {
            get { return UserInfo().Email; }
        }
        public static string PresentAddress
        {
            get { return UserInfo().PresentAddress; }
        }
        public static string MobileNo
        {
            get { return UserInfo().MobileNo; }
        }
        public static string PermanentAddress
        {
            get { return UserInfo().PermanentAddress; }
        }
        public static string DeliveryAddress
        {
            get { return UserInfo().DeliveryAddress; }
        }
    }
}
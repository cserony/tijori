using ESOL_BO.DbAccess.Security;
using ESOL_BO.Ecommerce;
using ESOL_BO.Hr;
using ESOL_BO.Merchant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESOL_BO.DbAccess
{
    public class DataAccess
    {
        #region Security
        public static UserInfoDao UserInfoDao { get { return new UserInfoDao(); } }
        public static RolePermissionDao RolePermissionDao { get { return new RolePermissionDao(); } }
        public static UserRoleDao UserRoleDao { get { return new UserRoleDao(); } }
        public static ChangePasswordDao ChangePasswordDao { get { return new ChangePasswordDao(); } }
        public static CompanyDao CompanyDao { get { return new CompanyDao(); } }
        public static ShowroomDao ShowroomDao { get { return new ShowroomDao(); } }
        #endregion
        #region Merchant
        public static MerchantUser MerchantUser { get { return new MerchantUser(); } }
        #endregion
        #region Ecommerce
        public static EcommerceBrand EcommerceBrand {get{return new EcommerceBrand(); }}
        public static EcommerceCategory EcommerceCategory { get { return new EcommerceCategory(); } }
        public static EcommerceColor EcommerceColor { get { return new EcommerceColor(); } }
        public static EcommerceSize EcommerceSize { get { return new EcommerceSize(); } }
        public static EcommerceSubCategory EcommerceSubCategory { get {return new EcommerceSubCategory(); } }
        public static EcommerceSubSubCategory EcommerceSubSubCategory { get { return new EcommerceSubSubCategory(); } }
        public static EcommerceUnit EcommerceUnit { get { return new EcommerceUnit(); } }
        public static EcommerceWeight EcommerceWeight { get { return new EcommerceWeight(); } }
        public static EcommerceCardType EcommerceCardType { get { return new EcommerceCardType(); } }
        public static EcommerceCurrency EcommerceCurrency { get { return new EcommerceCurrency(); } }
        public static EcommerceCustomer EcommerceCustomer { get { return new EcommerceCustomer(); } }
        public static EcommerceHomePageSlider EcommerceHomePageSlider { get { return new EcommerceHomePageSlider(); } }
        public static EcommercePaymentType EcommercePaymentType { get { return new EcommercePaymentType(); } }
        public static EcommerceServiceType EcommerceServiceType { get { return new EcommerceServiceType(); } }
        public static EcommerceVatType EcommerceVatType { get { return new EcommerceVatType(); } }
        public static EcommerceBadge EcommerceBadge { get { return new EcommerceBadge(); } }
        public static EcommerceProduct EcommerceProduct { get { return new EcommerceProduct(); } }
        public static EcommerceProductStockInfo EcommerceProductStockInfo { get { return new EcommerceProductStockInfo(); } }
        public static EcommerceNotice EcommerceNotice { get { return new EcommerceNotice(); } }
        public static EcommerceOrder EcommerceOrder { get { return new EcommerceOrder(); } }
        public static EcommerceOrderDetails EcommerceOrderDetails { get { return new EcommerceOrderDetails(); } }
        public static EcommerceProductGallery EcommerceProductGallery { get { return new EcommerceProductGallery(); } }
        public static EcommerceSubscriber EcommerceSubscriber { get { return new EcommerceSubscriber(); } }
        public static EcommerceShippingDetails EcommerceShippingDetails { get { return new EcommerceShippingDetails(); } }
        public static EcommerceCustomerReview EcommerceCustomerReview { get { return new EcommerceCustomerReview(); } }
        public static EcommerceCoupon EcommerceCoupon { get { return new EcommerceCoupon(); } }
        public static EcommerceShippingZone EcommerceShippingZone { get { return new EcommerceShippingZone(); } }
        public static EcommerceRecentlyViews EcommerceRecentlyViews { get { return new EcommerceRecentlyViews(); } }
        public static EcommerceWishlist EcommerceWishlist { get { return new EcommerceWishlist(); } }

        #endregion
        #region HR
        public static HrBloodGroup HrBloodGroup { get { return new HrBloodGroup(); } }
        public static HrReligion HrReligion { get { return new HrReligion(); } }
        public static HrDesignation HrDesignation { get { return new HrDesignation(); } }
        public static Division Division { get { return new Division(); } }
        public static District District { get { return new District(); } }
        public static Thana Thana { get { return new Thana(); } }
        public static PostOffice PostOffice { get { return new PostOffice(); } }
        #endregion
    }
}
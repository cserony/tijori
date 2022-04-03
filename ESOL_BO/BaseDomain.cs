using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESOL_BO.DbAccess
{
    public class BaseDomain
    {
        public int OrgId { get; set; }
        public int BranchId { get; set; }
        public string OrgName { get; set; }
        public string BranchName { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public int EmpId { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int Create { get; set; }
        public int Edit { get; set; }
        public int Delete { get; set; }
        public int View { get; set; }
        public bool IsActive { get; set; }
        public string Status { get; set; }
        public string StatementType { get; set; }
        public string AddressLine { get; set; }
        public string Date { get; set; }
        public string ImageName { get; set; }
        public int MerchantId { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string Slug { get; set; }
        public string Path { get; set; }
        public string BadgeName { get; set; }
        public int CatCount { get; set; }
        public string ProductName { get; set; }
        public string SKU { get; set; }
        public string Year { get; set; }
        public string Day { get; set; }
        public string CustomerName { get; set; }
        public string OrgAddress { get; set; }
        public string OrgMobileNo { get; set; }
        public int StatusId { get; set; }        
    }
}

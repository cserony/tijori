using ESOL_BO.DbAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESOL_BO.Merchant
{
    public class MerchantUserDomain : BaseDomain
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Gender { get; set; }
        public string RelativesName { get; set; }
        public string RelativesMobileNo { get; set; }
        public string EducationalQualification { get; set; }
        public string PassingYear { get; set; }
        public string InstitutionName { get; set; }
        public string BrandName { get; set; }
        public string BrandAddress { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string RoutingNumber { get; set; }
        public string YourSelf { get; set; }
        public string Photo { get; set; }
        public byte[] StudentPhoto { get; set; }
        public bool IsAccept { get; set; }
        public bool Men { get; set; }
        public bool Women { get; set; }
        public bool Childern { get; set; }
        public bool Unisex { get; set; }
    }
    public class MerchantUser
    {
        public int InsertOrUpdate(MerchantUserDomain entity)
        {
            int save = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_MerchantAccountAll", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OrgId", SqlDbType.Int).Value = entity.OrgId;
                    cmd.Parameters.Add("@MerchantId", SqlDbType.Int).Value = entity.MerchantId;
                    cmd.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = entity.UserName;
                    cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = entity.Password;
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = entity.Name;
                    cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = entity.Address;
                    cmd.Parameters.Add("@Mobile", SqlDbType.NVarChar).Value = entity.Mobile;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = entity.Email;
                    cmd.Parameters.Add("@Gender", SqlDbType.NChar).Value = entity.Gender;
                    cmd.Parameters.Add("@RelativesName", SqlDbType.NVarChar).Value = entity.RelativesName;
                    cmd.Parameters.Add("@RelativesMobileNo", SqlDbType.NVarChar).Value = entity.RelativesMobileNo;
                    cmd.Parameters.Add("@EducationalQualification", SqlDbType.NVarChar).Value = entity.EducationalQualification;
                    cmd.Parameters.Add("@PassingYear", SqlDbType.NVarChar).Value = entity.PassingYear;
                    cmd.Parameters.Add("@InstitutionName", SqlDbType.NVarChar).Value = entity.InstitutionName;
                    cmd.Parameters.Add("@BrandName", SqlDbType.NVarChar).Value = entity.BrandName;
                    cmd.Parameters.Add("@BrandAddress", SqlDbType.NVarChar).Value = entity.BrandAddress;
                    cmd.Parameters.Add("@AccountName", SqlDbType.NVarChar).Value = entity.AccountName;
                    cmd.Parameters.Add("@AccountNumber", SqlDbType.NVarChar).Value = entity.AccountNumber;
                    cmd.Parameters.Add("@BankName", SqlDbType.NVarChar).Value = entity.BankName;
                    cmd.Parameters.Add("@BankBranch", SqlDbType.NVarChar).Value = entity.BankBranch;
                    cmd.Parameters.Add("@RoutingNumber", SqlDbType.NVarChar).Value = entity.RoutingNumber;
                    cmd.Parameters.Add("@YourSelf", SqlDbType.NVarChar).Value = entity.YourSelf;
                    cmd.Parameters.Add("@Photo", SqlDbType.Image).Value = entity.StudentPhoto;
                    cmd.Parameters.Add("@IsAccept", SqlDbType.NVarChar).Value = entity.IsAccept;
                    cmd.Parameters.Add("@IsActive", SqlDbType.NVarChar).Value = entity.IsActive;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar).Value = entity.CreatedBy;
                    cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = entity.CreatedDate;
                    cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar).Value = entity.UpdatedBy;
                    cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = entity.UpdatedDate;
                    cmd.Parameters.Add("@Men", SqlDbType.NVarChar).Value = entity.Men;
                    cmd.Parameters.Add("@Women", SqlDbType.NVarChar).Value = entity.Women;
                    cmd.Parameters.Add("@Childern", SqlDbType.NVarChar).Value = entity.Childern;
                    cmd.Parameters.Add("@Unisex", SqlDbType.NVarChar).Value = entity.Unisex;
                    cmd.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = entity.StatementType;
                    save = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return save;
        }
        public List<MerchantUserDomain> GetByAll(int id)
        {
            List<MerchantUserDomain> lists = new List<MerchantUserDomain>();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("select * from MerchantAccount where OrgId='" + id + "' order by MerchantId desc", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        MerchantUserDomain list = new MerchantUserDomain();
                        list.OrgId = Convert.ToInt32(reader["OrgId"]);
                        list.MerchantId = Convert.ToInt32(reader["MerchantId"]);
                        list.UserName = reader["UserName"].ToString();
                        list.Password = reader["Password"].ToString();
                        list.Name = reader["Name"].ToString();
                        list.Address = reader["Address"].ToString();
                        list.Mobile = reader["Mobile"].ToString();
                        list.Email = reader["Email"].ToString();
                        list.Gender = reader["Gender"].ToString();
                        list.RelativesName = reader["RelativesName"].ToString();
                        list.RelativesMobileNo = reader["RelativesMobileNo"].ToString();
                        list.EducationalQualification = reader["EducationalQualification"].ToString();
                        list.PassingYear = reader["PassingYear"].ToString();
                        list.InstitutionName = reader["InstitutionName"].ToString();
                        list.BrandName = reader["BrandName"].ToString();
                        list.BrandAddress = reader["BrandAddress"].ToString();
                        list.AccountName = reader["AccountName"].ToString();
                        list.AccountNumber = reader["AccountNumber"].ToString();
                        list.BankName = reader["BankName"].ToString();
                        list.BankBranch = reader["BankBranch"].ToString();
                        list.RoutingNumber = reader["RoutingNumber"].ToString();
                        list.YourSelf = reader["YourSelf"].ToString();
                        list.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);                        
                        lists.Add(list);
                    }
                    con.Close();
                }
            }
            return lists;
        }
        public MerchantUserDomain GetById(int orgId, int id)
        {
            MerchantUserDomain list = new MerchantUserDomain();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("select * from MerchantAccount where OrgId='" + orgId + "' and MerchantId='" + id + "'", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list = new MerchantUserDomain();
                        list.OrgId = Convert.ToInt32(reader["OrgId"]);
                        list.MerchantId = Convert.ToInt32(reader["MerchantId"]);
                        list.UserName = reader["UserName"].ToString();
                        list.Password = reader["Password"].ToString();
                        list.Name = reader["Name"].ToString();
                        list.Address = reader["Address"].ToString();
                        list.Mobile = reader["Mobile"].ToString();
                        list.Email = reader["Email"].ToString();
                        list.Gender = reader["Gender"].ToString();
                        list.RelativesName = reader["RelativesName"].ToString();
                        list.RelativesMobileNo = reader["RelativesMobileNo"].ToString();
                        list.EducationalQualification = reader["EducationalQualification"].ToString();
                        list.PassingYear = reader["PassingYear"].ToString();
                        list.InstitutionName = reader["InstitutionName"].ToString();
                        list.BrandName = reader["BrandName"].ToString();
                        list.BrandAddress = reader["BrandAddress"].ToString();
                        list.AccountName = reader["AccountName"].ToString();
                        list.AccountNumber = reader["AccountNumber"].ToString();
                        list.BankName = reader["BankName"].ToString();
                        list.BankBranch = reader["BankBranch"].ToString();
                        list.RoutingNumber = reader["RoutingNumber"].ToString();
                        list.YourSelf = reader["YourSelf"].ToString();
                        list.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                        list.Men = Convert.ToBoolean(reader["Men"]);
                        list.Women = Convert.ToBoolean(reader["Women"]);
                        list.Childern = Convert.ToBoolean(reader["Childern"]);
                        list.Unisex = Convert.ToBoolean(reader["Unisex"]);
                    }
                    con.Close();
                }
            }
            return list;
        }
        public byte[] ViewPhoto(int orgId, string id)
        {
            byte[] photo = null;
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("select Photo from MerchantAccount where OrgId='" + orgId + "' and MerchantId='" + id + "'", con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (!string.IsNullOrEmpty(reader["Photo"].ToString()))
                            photo = (byte[])reader["Photo"];
                    }
                    con.Close();
                }
            }
            return photo;
        }
    }
}

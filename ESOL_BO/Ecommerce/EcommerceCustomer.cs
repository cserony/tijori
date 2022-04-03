using ESOL_BO.DbAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESOL_BO.Ecommerce
{
    public class EcommerceCustomerDomain : BaseDomain
    {
        public int CustomerId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CustomerName { get; set; }
        public int GenderId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PresentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string DeliveryAddress { get; set; }
        public bool VerifiedStatus { get; set; }
    }
    public class EcommerceCustomer
    {
        public int InsertOrUpdate(EcommerceCustomerDomain entity)
        {
            int save = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_EcommerceCustomerAll", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CustomerId", SqlDbType.Int).Value = entity.CustomerId;
                    cmd.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = entity.UserName;
                    cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = entity.Password;
                    cmd.Parameters.Add("@CustomerName", SqlDbType.NVarChar).Value = entity.CustomerName;
                    cmd.Parameters.Add("@GenderId", SqlDbType.Int).Value = entity.GenderId;
                    cmd.Parameters.Add("@DateOfBirth", SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@MobileNo", SqlDbType.NVarChar).Value = entity.MobileNo;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = entity.Email;
                    cmd.Parameters.Add("@PresentAddress", SqlDbType.NVarChar).Value = entity.PresentAddress;
                    cmd.Parameters.Add("@PermanentAddress", SqlDbType.NVarChar).Value = entity.PermanentAddress;
                    cmd.Parameters.Add("@DeliveryAddress", SqlDbType.NVarChar).Value = entity.DeliveryAddress;
                    cmd.Parameters.Add("@ImageName", SqlDbType.NVarChar).Value = entity.ImageName;
                    cmd.Parameters.Add("@VerifiedStatus", SqlDbType.NVarChar).Value = entity.VerifiedStatus;
                    cmd.Parameters.Add("@IsActive", SqlDbType.NVarChar).Value = entity.IsActive;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar).Value = entity.CreatedBy;
                    cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar).Value = entity.UpdatedBy;
                    cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = entity.StatementType;
                    save = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return save;
        }
        public EcommerceCustomerDomain Login(EcommerceCustomerDomain entity)
        {
            EcommerceCustomerDomain userInfo = new EcommerceCustomerDomain();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetByCusterLogin", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = entity.UserName;
                    cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = entity.Password;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        userInfo = new EcommerceCustomerDomain();
                        userInfo.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                        userInfo.UserName = Convert.ToString(reader["UserName"]);
                        userInfo.Password = Convert.ToString(reader["Password"]);
                        userInfo.CustomerName = Convert.ToString(reader["CustomerName"]);
                        userInfo.MobileNo = Convert.ToString(reader["MobileNo"]);
                        userInfo.Email = Convert.ToString(reader["Email"]);
                        userInfo.ImageName = Convert.ToString(reader["ImageName"]);
                        userInfo.PresentAddress = Convert.ToString(reader["PresentAddress"]);
                        userInfo.PermanentAddress = Convert.ToString(reader["PermanentAddress"]);
                        userInfo.DeliveryAddress = Convert.ToString(reader["DeliveryAddress"]);
                    }
                    con.Close();
                }
            }
            return userInfo;
        }
        public List<EcommerceCustomerDomain> GetByOrg(string where)
        {
            List<EcommerceCustomerDomain> lists = new List<EcommerceCustomerDomain>();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(where, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        EcommerceCustomerDomain list = new EcommerceCustomerDomain();
                        list.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                        list.UserName = reader["UserName"].ToString();
                        list.Password = reader["Password"].ToString();
                        list.CustomerName = reader["CustomerName"].ToString();
                        list.GenderId = Convert.ToInt32(reader["GenderId"]);
                        list.DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                        list.MobileNo = reader["MobileNo"].ToString();
                        list.Email = reader["Email"].ToString();
                        list.PresentAddress = reader["PresentAddress"].ToString();
                        list.PermanentAddress = reader["PermanentAddress"].ToString();
                        list.DeliveryAddress = reader["DeliveryAddress"].ToString();
                        list.ImageName = reader["ImageName"].ToString();
                        list.VerifiedStatus = Convert.ToBoolean(reader["VerifiedStatus"]);
                        list.IsActive = Convert.ToBoolean(reader["IsActive"]);
                        list.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                        lists.Add(list);
                    }
                    con.Close();
                }
            }
            return lists;
        }
        public EcommerceCustomerDomain GetById(string where)
        {
            EcommerceCustomerDomain list = new EcommerceCustomerDomain();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(where, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list = new EcommerceCustomerDomain();
                        list.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                        list.UserName = reader["UserName"].ToString();
                        list.Password = reader["Password"].ToString();
                        list.CustomerName = reader["CustomerName"].ToString();
                        list.GenderId = Convert.ToInt32(reader["GenderId"]);
                        list.DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                        list.MobileNo = reader["MobileNo"].ToString();
                        list.Email = reader["Email"].ToString();
                        list.PresentAddress = reader["PresentAddress"].ToString();
                        list.PermanentAddress = reader["PermanentAddress"].ToString();
                        list.DeliveryAddress = reader["DeliveryAddress"].ToString();
                        list.ImageName = reader["ImageName"].ToString();
                        list.VerifiedStatus = Convert.ToBoolean(reader["VerifiedStatus"]);
                        list.IsActive = Convert.ToBoolean(reader["IsActive"]);
                    }
                    con.Close();
                }
            }
            return list;
        }
        public int DeliveryAddressUpdate(EcommerceCustomerDomain entity)
        {
            int save = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                string sql = @"Update EcommerceCustomer set DeliveryAddress='" + entity.DeliveryAddress + "' where CustomerId='" + entity.CustomerId + "'";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    save = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return save;
        }
        public int ChangePassword(EcommerceCustomerDomain entity)
        {
            int save = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                string sql = @"Update EcommerceCustomer set Password='" + entity.Password + "' where CustomerId='" + entity.CustomerId + "'";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    save = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return save;
        }
    }
}

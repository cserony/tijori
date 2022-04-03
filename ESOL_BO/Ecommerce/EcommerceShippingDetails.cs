using ESOL_BO.DbAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ESOL_BO.Ecommerce
{
    public class EcommerceShippingDetailsDomain : BaseDomain
    {
        public int ShippingId { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
    }
    public class EcommerceShippingDetails
    {
        public int InsertOrUpdate(EcommerceShippingDetailsDomain entity)
        {
            int save = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_EcommerceShippingDetailsAll", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ShippingId", SqlDbType.Int).Value = entity.ShippingId;
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = entity.Name;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = entity.Email;
                    cmd.Parameters.Add("@Mobile", SqlDbType.NVarChar).Value = entity.Mobile;
                    cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = entity.Address;
                    cmd.Parameters.Add("@City", SqlDbType.NVarChar).Value = entity.City;
                    cmd.Parameters.Add("@PostCode", SqlDbType.NVarChar).Value = entity.PostCode;
                    cmd.Parameters.Add("@IsActive", SqlDbType.NVarChar).Value = entity.IsActive;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar).Value = entity.CreatedBy;
                    cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar).Value = entity.UpdatedBy;
                    cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = entity.StatementType;
                    save = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                }
            }
            return save;
        }
        public List<EcommerceShippingDetailsDomain> GetByAll()
        {
            List<EcommerceShippingDetailsDomain> lists = new List<EcommerceShippingDetailsDomain>();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("select * from EcommerceShippingDetails order by ShippingId desc", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        EcommerceShippingDetailsDomain list = new EcommerceShippingDetailsDomain();
                        list.ShippingId = Convert.ToInt32(reader["ShippingId"]);
                        list.Name = reader["Name"].ToString();
                        list.Email = reader["Email"].ToString();
                        list.Mobile = reader["Mobile"].ToString();
                        list.Address = reader["Address"].ToString();
                        list.City = reader["City"].ToString();
                        list.PostCode = reader["PostCode"].ToString();
                        list.IsActive = Convert.ToBoolean(reader["IsActive"]);
                        lists.Add(list);
                    }
                    con.Close();
                }
            }
            return lists;
        }
        public EcommerceShippingDetailsDomain GetById(int id)
        {
            EcommerceShippingDetailsDomain list = new EcommerceShippingDetailsDomain();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("select * from EcommerceShippingDetails where ShippingId='" + id + "'", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list = new EcommerceShippingDetailsDomain();
                        list.ShippingId = Convert.ToInt32(reader["ShippingId"]);
                        list.Name = reader["Name"].ToString();
                        list.Email = reader["Email"].ToString();
                        list.Mobile = reader["Mobile"].ToString();
                        list.Address = reader["Address"].ToString();
                        list.City = reader["City"].ToString();
                        list.PostCode = reader["PostCode"].ToString();
                        list.IsActive = Convert.ToBoolean(reader["IsActive"]);
                    }
                    con.Close();
                }
            }
            return list;
        }
    }
}

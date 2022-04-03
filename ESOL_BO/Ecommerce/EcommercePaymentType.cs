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
    public class EcommercePaymentTypeDomain : BaseDomain
    {
        public int PaymentTypeId { get; set; }
        public string PaymentTypeName { get; set; }
    }
    public class EcommercePaymentType
    {
        public int InsertOrUpdate(EcommercePaymentTypeDomain entity)
        {
            int save = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_EcommercePaymentTypeAll", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OrgId", SqlDbType.Int).Value = entity.OrgId;
                    cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = entity.BranchId;
                    cmd.Parameters.Add("@PaymentTypeId", SqlDbType.Int).Value = entity.PaymentTypeId;
                    cmd.Parameters.Add("@PaymentTypeName", SqlDbType.NVarChar).Value = entity.PaymentTypeName;
                    cmd.Parameters.Add("@IsActive", SqlDbType.NVarChar).Value = entity.IsActive;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar).Value = entity.CreatedBy;
                    cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = entity.CreatedDate;
                    cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar).Value = entity.UpdatedBy;
                    cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = entity.UpdatedDate;
                    cmd.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = entity.StatementType;
                    save = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return save;
        }
        public List<EcommercePaymentTypeDomain> GetByOrg(string where)
        {
            List<EcommercePaymentTypeDomain> lists = new List<EcommercePaymentTypeDomain>();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(where, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        EcommercePaymentTypeDomain list = new EcommercePaymentTypeDomain();
                        list.PaymentTypeId = Convert.ToInt32(reader["PaymentTypeId"]);
                        list.PaymentTypeName = reader["PaymentTypeName"].ToString();
                        lists.Add(list);
                    }
                    con.Close();
                }
            }
            return lists;
        }
        public EcommercePaymentTypeDomain GetById(string where)
        {
            EcommercePaymentTypeDomain list = new EcommercePaymentTypeDomain();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(where, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list = new EcommercePaymentTypeDomain();
                        list.PaymentTypeId = Convert.ToInt32(reader["PaymentTypeId"]);
                        list.PaymentTypeName = reader["PaymentTypeName"].ToString();
                    }
                    con.Close();
                }
            }
            return list;
        }
    }
}

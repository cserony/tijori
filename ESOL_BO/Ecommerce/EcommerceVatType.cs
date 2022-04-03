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
    public class EcommerceVatTypeDomain : BaseDomain
    {
        public int VatTypeId { get; set; }
        public string VatTypeName { get; set; }
    }
    public class EcommerceVatType
    {
        public int InsertOrUpdate(EcommerceVatTypeDomain entity)
        {
            int save = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_EcommerceVatTypeAll", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OrgId", SqlDbType.Int).Value = entity.OrgId;
                    cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = entity.BranchId;
                    cmd.Parameters.Add("@VatTypeId", SqlDbType.Int).Value = entity.VatTypeId;
                    cmd.Parameters.Add("@VatTypeName", SqlDbType.NVarChar).Value = entity.VatTypeName;
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
        public List<EcommerceVatTypeDomain> GetByOrg(string where)
        {
            List<EcommerceVatTypeDomain> lists = new List<EcommerceVatTypeDomain>();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(where, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        EcommerceVatTypeDomain list = new EcommerceVatTypeDomain();
                        list.VatTypeId = Convert.ToInt32(reader["VatTypeId"]);
                        list.VatTypeName = reader["VatTypeName"].ToString();
                        lists.Add(list);
                    }
                    con.Close();
                }
            }
            return lists;
        }
        public EcommerceVatTypeDomain GetById(string where)
        {
            EcommerceVatTypeDomain list = new EcommerceVatTypeDomain();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(where, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list = new EcommerceVatTypeDomain();
                        list.VatTypeId = Convert.ToInt32(reader["VatTypeId"]);
                        list.VatTypeName = reader["VatTypeName"].ToString();
                    }
                    con.Close();
                }
            }
            return list;
        }
    }
}

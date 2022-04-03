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
    public class EcommerceServiceTypeDomain : BaseDomain
    {
        public int ServiceTypeId { get; set; }
        public string ServiceTypeName { get; set; }
    }
    public class EcommerceServiceType
    {
        public int InsertOrUpdate(EcommerceServiceTypeDomain entity)
        {
            int save = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_EcommerceServiceTypeAll", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OrgId", SqlDbType.Int).Value = entity.OrgId;
                    cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = entity.BranchId;
                    cmd.Parameters.Add("@ServiceTypeId", SqlDbType.Int).Value = entity.ServiceTypeId;
                    cmd.Parameters.Add("@ServiceTypeName", SqlDbType.NVarChar).Value = entity.ServiceTypeName;
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
        public List<EcommerceServiceTypeDomain> GetByOrg(string where)
        {
            List<EcommerceServiceTypeDomain> lists = new List<EcommerceServiceTypeDomain>();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(where, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        EcommerceServiceTypeDomain list = new EcommerceServiceTypeDomain();
                        list.ServiceTypeId = Convert.ToInt32(reader["ServiceTypeId"]);
                        list.ServiceTypeName = reader["ServiceTypeName"].ToString();
                        lists.Add(list);
                    }
                    con.Close();
                }
            }
            return lists;
        }
        public EcommerceServiceTypeDomain GetById(string where)
        {
            EcommerceServiceTypeDomain list = new EcommerceServiceTypeDomain();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(where, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list = new EcommerceServiceTypeDomain();
                        list.ServiceTypeId = Convert.ToInt32(reader["ServiceTypeId"]);
                        list.ServiceTypeName = reader["ServiceTypeName"].ToString();
                    }
                    con.Close();
                }
            }
            return list;
        }
    }
}

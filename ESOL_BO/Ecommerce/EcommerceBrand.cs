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
    public class EcommerceBrandDomain : BaseDomain
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
    }
    public class EcommerceBrand
    {
        public int InsertOrUpdate(EcommerceBrandDomain entity)
        {
            int save = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_EcommerceBrandAll", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OrgId", SqlDbType.Int).Value = entity.OrgId;
                    cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = entity.BranchId;
                    cmd.Parameters.Add("@BrandId", SqlDbType.Int).Value = entity.BrandId;
                    cmd.Parameters.Add("@BrandName", SqlDbType.NVarChar).Value = entity.BrandName;
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
        public List<EcommerceBrandDomain> GetByOrg(string where)
        {
            List<EcommerceBrandDomain> lists = new List<EcommerceBrandDomain>();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(where, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        EcommerceBrandDomain list = new EcommerceBrandDomain();
                        list.BrandId = Convert.ToInt32(reader["BrandId"]);
                        list.BrandName = reader["BrandName"].ToString();
                        lists.Add(list);
                    }
                    con.Close();
                }
            }
            return lists;
        }
        public EcommerceBrandDomain GetById(string where)
        {
            EcommerceBrandDomain list = new EcommerceBrandDomain();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(where, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list = new EcommerceBrandDomain();
                        list.BrandId = Convert.ToInt32(reader["BrandId"]);
                        list.BrandName = reader["BrandName"].ToString();
                    }
                    con.Close();
                }
            }
            return list;
        }
    }
}

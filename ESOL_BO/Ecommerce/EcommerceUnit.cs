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
    public class EcommerceUnitDomain : BaseDomain
    {
        public int UnitId { get; set; }
        public string UnitName { get; set; }
    }
    public class EcommerceUnit
    {
        public int InsertOrUpdate(EcommerceUnitDomain entity)
        {
            int save = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_EcommerceUnitAll", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OrgId", SqlDbType.Int).Value = entity.OrgId;
                    cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = entity.BranchId;
                    cmd.Parameters.Add("@UnitId", SqlDbType.Int).Value = entity.UnitId;
                    cmd.Parameters.Add("@UnitName", SqlDbType.NVarChar).Value = entity.UnitName;
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
        public List<EcommerceUnitDomain> GetByOrg(string where)
        {
            List<EcommerceUnitDomain> lists = new List<EcommerceUnitDomain>();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(where, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        EcommerceUnitDomain list = new EcommerceUnitDomain();
                        list.UnitId = Convert.ToInt32(reader["UnitId"]);
                        list.UnitName = reader["UnitName"].ToString();
                        lists.Add(list);
                    }
                    con.Close();
                }
            }
            return lists;
        }
        public EcommerceUnitDomain GetById(string where)
        {
            EcommerceUnitDomain list = new EcommerceUnitDomain();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(where, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list = new EcommerceUnitDomain();
                        list.UnitId = Convert.ToInt32(reader["UnitId"]);
                        list.UnitName = reader["UnitName"].ToString();
                    }
                    con.Close();
                }
            }
            return list;
        }
    }
}

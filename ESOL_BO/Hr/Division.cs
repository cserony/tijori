using ESOL_BO.DbAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESOL_BO.Hr
{
    public class DivisionDomain : BaseDomain
    {
        public int DivisionId { get; set; }
        public string DivisionName { get; set; }
    }
    public class Division
    {
        public int InsertOrUpdate(DivisionDomain entity)
        {
            int save = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_DivisionAll", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OrgId", SqlDbType.Int).Value = entity.OrgId;
                    cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = entity.BranchId;
                    cmd.Parameters.Add("@DivisionId", SqlDbType.Int).Value = entity.DivisionId;
                    cmd.Parameters.Add("@DivisionName", SqlDbType.NVarChar).Value = entity.DivisionName;
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
        public List<DivisionDomain> GetByOrg(string where)
        {
            List<DivisionDomain> lists = new List<DivisionDomain>();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(where, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        DivisionDomain list = new DivisionDomain();
                        list.OrgId = Convert.ToInt32(reader["OrgId"]);
                        list.DivisionId = Convert.ToInt32(reader["DivisionId"]);
                        list.DivisionName = reader["DivisionName"].ToString();
                        lists.Add(list);
                    }
                    con.Close();
                }
            }
            return lists;
        }
        public DivisionDomain GetById(string where)
        {
            DivisionDomain list = new DivisionDomain();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(where, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list = new DivisionDomain();
                        list.OrgId = Convert.ToInt32(reader["OrgId"]);
                        list.DivisionId = Convert.ToInt32(reader["DivisionId"]);
                        list.DivisionName = reader["DivisionName"].ToString();
                    }
                    con.Close();
                }
            }
            return list;
        }
    }
}

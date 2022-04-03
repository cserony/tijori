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
    public class HrDesignationDomain : BaseDomain
    {
        public int DesignationId { get; set; }
        public string DesignationName { get; set; }
    }
    public class HrDesignation
    {
        public int InsertOrUpdate(HrDesignationDomain entity)
        {
            int save = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Sp_HrDesignationAll", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OrgId", SqlDbType.Int).Value = entity.OrgId;
                    cmd.Parameters.Add("@DesignationId", SqlDbType.Int).Value = entity.DesignationId;
                    cmd.Parameters.Add("@DesignationName", SqlDbType.NVarChar).Value = entity.DesignationName;
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
        public List<HrDesignationDomain> GetByOrg(string where)
        {
            List<HrDesignationDomain> lists = new List<HrDesignationDomain>();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(where, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        HrDesignationDomain list = new HrDesignationDomain();
                        list.OrgId = Convert.ToInt32(reader["OrgId"]);
                        list.DesignationId = Convert.ToInt32(reader["DesignationId"]);
                        list.DesignationName = reader["DesignationName"].ToString();
                        lists.Add(list);
                    }
                    con.Close();
                }
            }
            return lists;
        }
        public HrDesignationDomain GetById(string where)
        {
            HrDesignationDomain list = new HrDesignationDomain();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(where, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list = new HrDesignationDomain();
                        list.OrgId = Convert.ToInt32(reader["OrgId"]);
                        list.DesignationId = Convert.ToInt32(reader["DesignationId"]);
                        list.DesignationName = reader["DesignationName"].ToString();
                    }
                    con.Close();
                }
            }
            return list;
        }
    }
}

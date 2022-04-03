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
    public class HrReligionDomain : BaseDomain
    {
        public int ReligionId { get; set; }
        public string ReligionName { get; set; }
    }
    public class HrReligion
    {
        public int InsertOrUpdate(HrReligionDomain entity)
        {
            int save = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Sp_ReligionAll", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OrgId", SqlDbType.Int).Value = entity.OrgId;
                    cmd.Parameters.Add("@ReligionId", SqlDbType.Int).Value = entity.ReligionId;
                    cmd.Parameters.Add("@ReligionName", SqlDbType.NVarChar).Value = entity.ReligionName;
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
        public List<HrReligionDomain> GetByOrg(string where)
        {
            List<HrReligionDomain> lists = new List<HrReligionDomain>();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(where, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        HrReligionDomain list = new HrReligionDomain();
                        list.ReligionId = Convert.ToInt32(reader["ReligionId"]);
                        list.ReligionName = reader["ReligionName"].ToString();
                        lists.Add(list);
                    }
                    con.Close();
                }
            }
            return lists;
        }
        public HrReligionDomain GetById(string where)
        {
            HrReligionDomain list = new HrReligionDomain();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(where, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list = new HrReligionDomain();
                        list.ReligionId = Convert.ToInt32(reader["ReligionId"]);
                        list.ReligionName = reader["ReligionName"].ToString();
                    }
                    con.Close();
                }
            }
            return list;
        }
    }
}

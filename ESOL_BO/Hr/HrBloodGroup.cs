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
    public class HrBloodGroupDomain : BaseDomain
    {
        public int BloodGroupId { get; set; }
        public string BloodGroupName { get; set; }
    }
    public class HrBloodGroup
    {
        public int InsertOrUpdate(HrBloodGroupDomain entity)
        {
            int save = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Sp_BloodGroupAll", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OrgId", SqlDbType.Int).Value = entity.OrgId;
                    cmd.Parameters.Add("@BloodGroupId", SqlDbType.Int).Value = entity.BloodGroupId;
                    cmd.Parameters.Add("@BloodGroupName", SqlDbType.NVarChar).Value = entity.BloodGroupName;
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
        public List<HrBloodGroupDomain> GetByOrg(string where)
        {
            List<HrBloodGroupDomain> lists = new List<HrBloodGroupDomain>();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(where, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        HrBloodGroupDomain list = new HrBloodGroupDomain();
                        list.BloodGroupId = Convert.ToInt32(reader["BloodGroupId"]);
                        list.BloodGroupName = reader["BloodGroupName"].ToString();
                        lists.Add(list);
                    }
                    con.Close();
                }
            }
            return lists;
        }
        public HrBloodGroupDomain GetById(string where)
        {
            HrBloodGroupDomain list = new HrBloodGroupDomain();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(where, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list = new HrBloodGroupDomain();
                        list.BloodGroupId = Convert.ToInt32(reader["BloodGroupId"]);
                        list.BloodGroupName = reader["BloodGroupName"].ToString();
                    }
                    con.Close();
                }
            }
            return list;
        }
    }
}

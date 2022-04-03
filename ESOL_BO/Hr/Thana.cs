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
    public class ThanaDomain : BaseDomain
    {
        public int ThanaId { get; set; }
        public int DistrictId { get; set; }
        public string ThanaName { get; set; }
        public string DistrictName { get; set; }
    }
    public class Thana
    {
        public int InsertOrUpdate(ThanaDomain entity)
        {
            int save = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_ThanaAll", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OrgId", SqlDbType.Int).Value = entity.OrgId;
                    cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = entity.BranchId;                    
                    cmd.Parameters.Add("@ThanaId", SqlDbType.Int).Value = entity.ThanaId;
                    cmd.Parameters.Add("@DistrictId", SqlDbType.Int).Value = entity.DistrictId;
                    cmd.Parameters.Add("@ThanaName", SqlDbType.NVarChar).Value = entity.ThanaName;
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
        public List<ThanaDomain> GetByOrg(string where)
        {
            List<ThanaDomain> lists = new List<ThanaDomain>();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(where, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ThanaDomain list = new ThanaDomain();
                        list.OrgId = Convert.ToInt32(reader["OrgId"]);
                        list.ThanaId = Convert.ToInt32(reader["ThanaId"]);
                        list.DistrictId = Convert.ToInt32(reader["DistrictId"]);
                        list.ThanaName = reader["ThanaName"].ToString();
                        list.DistrictName = reader["DistrictName"].ToString();
                        lists.Add(list);
                    }
                    con.Close();
                }
            }
            return lists;
        }
        public ThanaDomain GetById(string where)
        {
            ThanaDomain list = new ThanaDomain();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(where, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list = new ThanaDomain();
                        list.OrgId = Convert.ToInt32(reader["OrgId"]);
                        list.ThanaId = Convert.ToInt32(reader["ThanaId"]);
                        list.DistrictId = Convert.ToInt32(reader["DistrictId"]);
                        list.ThanaName = reader["ThanaName"].ToString();
                    }
                    con.Close();
                }
            }
            return list;
        }
    }
}

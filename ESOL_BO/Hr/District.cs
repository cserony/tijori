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
    public class DistrictDomain : BaseDomain
    {
        public int DistrictId { get; set; }
        public int DivisionId { get; set; }
        public string DistrictName { get; set; }
        public string DivisionName { get; set; }
    }
    public class District
    {
        public int InsertOrUpdate(DistrictDomain entity)
        {
            int save = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_DistrictAll", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OrgId", SqlDbType.Int).Value = entity.OrgId;
                    cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = entity.BranchId;
                    cmd.Parameters.Add("@DistrictId", SqlDbType.Int).Value = entity.DistrictId;
                    cmd.Parameters.Add("@DivisionId", SqlDbType.Int).Value = entity.DivisionId;
                    cmd.Parameters.Add("@DistrictName", SqlDbType.NVarChar).Value = entity.DistrictName;
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
        public List<DistrictDomain> GetByOrg(string where)
        {
            List<DistrictDomain> lists = new List<DistrictDomain>();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(where, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        DistrictDomain list = new DistrictDomain();
                        list.OrgId = Convert.ToInt32(reader["OrgId"]);
                        list.DistrictId = Convert.ToInt32(reader["DistrictId"]);
                        list.DistrictName = reader["DistrictName"].ToString();
                        list.DivisionName = reader["DivisionName"].ToString();
                        lists.Add(list);
                    }
                    con.Close();
                }
            }
            return lists;
        }
        public DistrictDomain GetById(string where)
        {
            DistrictDomain list = new DistrictDomain();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(where, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list = new DistrictDomain();
                        list.DistrictId = Convert.ToInt32(reader["DistrictId"]);
                        list.DivisionId = Convert.ToInt32(reader["DivisionId"]);
                        list.DistrictName = reader["DistrictName"].ToString();
                    }
                    con.Close();
                }
            }
            return list;
        }
    }
}

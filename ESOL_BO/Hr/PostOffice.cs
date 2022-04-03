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
    public class PostOfficeDomain : BaseDomain
    {
        public int PostOfficeId { get; set; }
        public int ThanaId { get; set; }
        public string PostOfficeName { get; set; }
        public string PostCode { get; set; }
        public string ThanaName { get; set; }
    }
    public class PostOffice
    {
        public int InsertOrUpdate(PostOfficeDomain entity)
        {
            int save = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_PostOfficeAll", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OrgId", SqlDbType.Int).Value = entity.OrgId;
                    cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = entity.BranchId;
                    cmd.Parameters.Add("@PostOfficeId", SqlDbType.Int).Value = entity.PostOfficeId;
                    cmd.Parameters.Add("@ThanaId", SqlDbType.Int).Value = entity.ThanaId;                    
                    cmd.Parameters.Add("@PostOfficeName", SqlDbType.NVarChar).Value = entity.PostOfficeName;
                    cmd.Parameters.Add("@PostCode", SqlDbType.NVarChar).Value = entity.PostCode;
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
        public List<PostOfficeDomain> GetByOrg(string where)
        {
            List<PostOfficeDomain> lists = new List<PostOfficeDomain>();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(where, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        PostOfficeDomain list = new PostOfficeDomain();
                        list.OrgId = Convert.ToInt32(reader["OrgId"]);
                        list.ThanaId = Convert.ToInt32(reader["ThanaId"]);
                        list.PostOfficeId = Convert.ToInt32(reader["PostOfficeId"]);
                        list.PostOfficeName = reader["PostOfficeName"].ToString();
                        list.PostCode = reader["PostCode"].ToString();
                        list.ThanaName = reader["ThanaName"].ToString();
                        lists.Add(list);
                    }
                    con.Close();
                }
            }
            return lists;
        }
        public PostOfficeDomain GetById(string where)
        {
            PostOfficeDomain list = new PostOfficeDomain();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(where, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list = new PostOfficeDomain();
                        list.OrgId = Convert.ToInt32(reader["OrgId"]);
                        list.ThanaId = Convert.ToInt32(reader["ThanaId"]);
                        list.PostOfficeId = Convert.ToInt32(reader["PostOfficeId"]);
                        list.PostOfficeName = reader["PostOfficeName"].ToString();
                        list.PostCode = reader["PostCode"].ToString();
                    }
                    con.Close();
                }
            }
            return list;
        }
    }
}

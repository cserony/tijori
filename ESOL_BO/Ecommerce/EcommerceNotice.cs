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
    public class EcommerceNoticeDomain : BaseDomain
    {
        public int NoticeId { get; set; }
        public string NoticeName { get; set; }
        public string Description { get; set; }
    }
    public class EcommerceNotice
    {
        public int InsertOrUpdate(EcommerceNoticeDomain entity)
        {
            int save = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_EcommerceNoticeAll", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OrgId", SqlDbType.Int).Value = entity.OrgId;
                    cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = entity.BranchId;
                    cmd.Parameters.Add("@NoticeId", SqlDbType.Int).Value = entity.NoticeId;
                    cmd.Parameters.Add("@NoticeName", SqlDbType.NVarChar).Value = entity.NoticeName;
                    cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = entity.Description;
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
        public List<EcommerceNoticeDomain> GetByOrg(string where)
        {
            List<EcommerceNoticeDomain> lists = new List<EcommerceNoticeDomain>();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(where, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        EcommerceNoticeDomain list = new EcommerceNoticeDomain();
                        list.NoticeId = Convert.ToInt32(reader["NoticeId"]);
                        list.NoticeName = reader["NoticeName"].ToString();
                        list.Description = reader["Description"].ToString();
                        lists.Add(list);
                    }
                    con.Close();
                }
            }
            return lists;
        }
        public EcommerceNoticeDomain GetById(string where)
        {
            EcommerceNoticeDomain list = new EcommerceNoticeDomain();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(where, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list = new EcommerceNoticeDomain();
                        list.NoticeId = Convert.ToInt32(reader["NoticeId"]);
                        list.NoticeName = reader["NoticeName"].ToString();
                        list.Description = reader["Description"].ToString();
                    }
                    con.Close();
                }
            }
            return list;
        }
    }
}

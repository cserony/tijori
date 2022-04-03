using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ESOL_BO.DbAccess.Security
{
    public class UserRoleDomain : BaseDomain
    {
        public bool IsParent { get; set; }
        public int ParentRoleId { get; set; }
    }
    public class UserRoleDao
    {
        public int InsertOrUpdate(UserRoleDomain entity)
        {
            int save = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_UserRoleAll", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OrgId", SqlDbType.Int).Value = entity.OrgId;
                    cmd.Parameters.Add("@RoleId", SqlDbType.Int).Value = entity.RoleId;
                    cmd.Parameters.Add("@RoleName", SqlDbType.NVarChar).Value = entity.RoleName;
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
        public List<UserRoleDomain> GetByOrg(int id)
        {
            List<UserRoleDomain> lists = new List<UserRoleDomain>();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("select * from UserRole where OrgId='" + id + "' order by RoleId desc", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        UserRoleDomain list = new UserRoleDomain();
                        list.OrgId = Convert.ToInt32(reader["OrgId"]);
                        list.RoleId = Convert.ToInt32(reader["RoleId"]);
                        list.RoleName = reader["RoleName"].ToString();
                        lists.Add(list);
                    }
                    con.Close();
                }
            }
            return lists;
        }
        public UserRoleDomain GetById(int orgId, int id)
        {
            UserRoleDomain list = new UserRoleDomain();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("select * from UserRole where OrgId='" + orgId + "' and RoleId='" + id + "'", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list = new UserRoleDomain();
                        list.OrgId = Convert.ToInt32(reader["OrgId"]);
                        list.RoleId = Convert.ToInt32(reader["RoleId"]);
                        list.RoleName = reader["RoleName"].ToString();
                    }
                    con.Close();
                }
            }
            return list;
        }
    }
}
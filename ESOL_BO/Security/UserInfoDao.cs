using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ESOL_BO.DbAccess.Security
{
    public class UserDomain : BaseDomain
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
    public class LoginDomain
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class UserInfoDao
    {
        public UserDomain Login(LoginDomain entity)
        {
            UserDomain userInfo = new UserDomain();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetbyLogin", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = entity.UserName;
                    cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = entity.Password;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        userInfo = new UserDomain();
                        userInfo.UserId = Convert.ToInt32(reader["UserId"]);
                        userInfo.OrgId = Convert.ToInt32(reader["OrgId"]);
                        userInfo.BranchId = Convert.ToInt32(reader["BranchId"]);
                        userInfo.UserName = Convert.ToString(reader["UserName"]);
                        userInfo.Password = Convert.ToString(reader["Password"]);
                        userInfo.IsActive = Convert.ToBoolean(reader["IsActive"]);
                        userInfo.RoleId = Convert.ToInt32(reader["RoleId"]);
                        userInfo.RoleName = Convert.ToString(reader["RoleName"]);
                        userInfo.OrgName = Convert.ToString(reader["OrgName"]);
                        userInfo.BranchName = Convert.ToString(reader["BranchName"]);
                        userInfo.EmpId = Convert.ToInt32(reader["EmpId"]);
                    }
                    con.Close();
                }
            }
            return userInfo;
        }
        public int InsertOrUpdate(UserDomain entity)
        {
            int save = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_UserInfoAll", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OrgId", SqlDbType.Int).Value = entity.OrgId;
                    cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = entity.BranchId;
                    cmd.Parameters.Add("@RoleId", SqlDbType.Int).Value = entity.RoleId;
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = entity.UserId;
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = entity.Name;
                    cmd.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = entity.UserName;
                    cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = entity.Password;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = entity.Email;
                    cmd.Parameters.Add("@IsActive", SqlDbType.NVarChar).Value = entity.IsActive;
                    cmd.Parameters.Add("@EmpId", SqlDbType.Int).Value = entity.EmpId;
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
        public List<UserDomain> GetByOrg(int orgId, int branchId)
        {
            List<UserDomain> lists = new List<UserDomain>();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("select ui.*,ur.RoleName from UserInfo ui left join UserRole ur on ur.OrgId=ui.OrgId and ur.RoleId=ui.RoleId where ui.OrgId='" + orgId + "' and ui.BranchId='" + branchId + "' order by ui.UserId desc", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        UserDomain list = new UserDomain();
                        list.OrgId = Convert.ToInt32(reader["OrgId"]);
                        list.BranchId = Convert.ToInt32(reader["BranchId"]);
                        list.RoleId = Convert.ToInt32(reader["RoleId"]);
                        list.UserId = Convert.ToInt32(reader["UserId"]);
                        list.Name = reader["Name"].ToString();
                        list.UserName = reader["UserName"].ToString();
                        list.Password = reader["Password"].ToString();
                        list.Email = reader["Email"].ToString();
                        list.IsActive = Convert.ToBoolean(reader["IsActive"]);
                        list.RoleName = reader["RoleName"].ToString();
                        lists.Add(list);
                    }
                    con.Close();
                }
            }
            return lists;
        }
        public UserDomain GetById(int orgId,int branchId, int id)
        {
            UserDomain list = new UserDomain();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("select ui.*,ur.RoleName from UserInfo ui left join UserRole ur on ur.OrgId=ui.OrgId and ur.RoleId=ui.RoleId where ui.OrgId='" + orgId + "' and ui.BranchId='" + branchId + "' and ui.UserId='" + id + "'", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list = new UserDomain();
                        list.OrgId = Convert.ToInt32(reader["OrgId"]);
                        list.BranchId = Convert.ToInt32(reader["BranchId"]);
                        list.RoleId = Convert.ToInt32(reader["RoleId"]);
                        list.UserId = Convert.ToInt32(reader["UserId"]);
                        list.UserName = reader["UserName"].ToString();
                        list.Password = reader["Password"].ToString();
                        list.Email = reader["Email"].ToString();
                        list.IsActive = Convert.ToBoolean(reader["IsActive"]);
                        list.RoleName = reader["RoleName"].ToString();
                        list.EmpId = Convert.ToInt32(reader["EmpId"]);
                        list.Email = reader["Email"].ToString();
                    }
                    con.Close();
                }
            }
            return list;
        }
    }
}
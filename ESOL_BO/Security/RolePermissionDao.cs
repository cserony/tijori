using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace ESOL_BO.DbAccess.Security
{
    public class ReportPermissionDomain : BaseDomain
    {
        public int AppId { get; set; }
        public int ReportId { get; set; }
        public string ReportName { get; set; }
        public bool IsPermission { get; set; }
    }
    public class RolePermissionDomain : BaseDomain
    {
        public int AppId { get; set; }
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public bool IsCreate { get; set; }
        public bool IsEdit { get; set; }
        public bool IsDelete { get; set; }
        public bool IsView { get; set; }
        public int RolePermissionId { get; set; }
        
    }
    public class UserMenuDomain : BaseDomain
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public int ModuleId { get; set; }
        public int AppId { get; set; }
        public string MenuIcon { get; set; }
        public string ModuleName { get; set; }
        public string ControllerName { get; set; }
        public string CreateAction { get; set; }
        public string EditAction { get; set; }
        public string DeleteAction { get; set; }
        public string ViewAction { get; set; }
        public int ParentMenuId { get; set; }
        public int Priority { get; set; }
        public string PageUrl { get; set; }
        public int IsCreate { get; set; }
        public int IsEdit { get; set; }
        public int IsView { get; set; }
        public int IsDelete { get; set; }
    }
    public class UserApplicationDomain : BaseDomain
    {
        public int ApplicationId { get; set; }
        public string ApplicationName { get; set; }
    }
    public class UserModuleDomain : BaseDomain
    {
        public int ApplicationId { get; set; }
        public string ApplicationName { get; set; }
        public int ModuleId { get; set; }
        public string MenuIcon { get; set; }
        public string ModuleName { get; set; }
    }
    public class RolePermissionDao
    {
        public int Save(RolePermissionDomain entity)
        {
            int rValue = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Sp_RolePermissionInsertAndDelete", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OrgId", SqlDbType.Int).Value = entity.OrgId;
                    cmd.Parameters.Add("@RolePermissionId", SqlDbType.Int).Value = entity.RolePermissionId;
                    cmd.Parameters.Add("@RoleId", SqlDbType.Int).Value = entity.RoleId;
                    cmd.Parameters.Add("@AppId", SqlDbType.Int).Value = entity.AppId;
                    cmd.Parameters.Add("@ModuleId", SqlDbType.Int).Value = entity.ModuleId;
                    cmd.Parameters.Add("@MenuId", SqlDbType.Int).Value = entity.MenuId;
                    cmd.Parameters.Add("@IsCreate", SqlDbType.Int).Value = entity.Create;
                    cmd.Parameters.Add("@IsEdit", SqlDbType.Int).Value = entity.Edit;
                    cmd.Parameters.Add("@IsDelete", SqlDbType.Int).Value = entity.Delete;
                    cmd.Parameters.Add("@IsView", SqlDbType.Int).Value = entity.View;
                    rValue = cmd.ExecuteNonQuery();
                }
                con.Close();
            }
            return rValue;
        }
        #region Security Part
        public List<UserModuleDomain> GetModule(int orgId, int appId)
        {
            List<UserModuleDomain> lists = new List<UserModuleDomain>();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("select ModuleId,ModuleName from UserModule where OrgId=" + orgId + " and AppId=" + appId + " order by ModuleName asc", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        UserModuleDomain list = new UserModuleDomain();
                        list.ModuleId = Convert.ToInt32(reader["ModuleId"]);
                        list.ModuleName = reader["ModuleName"].ToString();
                        lists.Add(list);
                    }
                    con.Close();
                }
            }
            return lists;
        }
        public List<UserApplicationDomain> GetUserWiseApp(int orgId, int roleId, string userName)
        {
            List<UserApplicationDomain> lists = new List<UserApplicationDomain>();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_SecGetUserWiseApp", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OrgId", SqlDbType.Int).Value = orgId;
                    cmd.Parameters.Add("@RoleId", SqlDbType.Int).Value = roleId;
                    cmd.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = userName;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        UserApplicationDomain list = new UserApplicationDomain();
                        list.OrgId = Convert.ToInt32(reader["OrgId"]);
                        list.ApplicationId = Convert.ToInt32(reader["ApplicationId"]);
                        list.ApplicationName = reader["ApplicationName"].ToString();
                        list.IsActive = Convert.ToBoolean(reader["IsActive"]);
                        lists.Add(list);
                    }
                    con.Close();
                }
            }
            return lists;
        }
        public List<UserMenuDomain> GetUserWiseMenu(int orgId, int? appId, string userName)
        {
            List<UserMenuDomain> lists = new List<UserMenuDomain>();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_SecGetUserWiseMenu", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OrgId", SqlDbType.Int).Value = orgId;
                    cmd.Parameters.Add("@AppId", SqlDbType.Int).Value = appId;
                    cmd.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = userName;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        UserMenuDomain userMenu = new UserMenuDomain();
                        userMenu.MenuId = Convert.ToInt32(reader["MenuId"]);
                        userMenu.ParentMenuId = Convert.ToInt32(reader["ParentMenuId"]);
                        userMenu.MenuIcon = Convert.ToString(reader["MenuIcon"]);
                        userMenu.MenuName = Convert.ToString(reader["MenuName"]);
                        userMenu.AppId = Convert.ToInt32(reader["AppId"]);
                        userMenu.ModuleId = Convert.ToInt32(reader["ModuleId"]);
                        userMenu.ModuleName = Convert.ToString(reader["ModuleName"]);
                        userMenu.PageUrl = Convert.ToString(reader["PageUrl"]);
                        userMenu.ControllerName = Convert.ToString(reader["ControllerName"]);
                        userMenu.CreateAction = Convert.ToString(reader["CreateAction"]);
                        userMenu.EditAction = Convert.ToString(reader["EditAction"]);
                        userMenu.DeleteAction = Convert.ToString(reader["DeleteAction"]);
                        userMenu.ViewAction = Convert.ToString(reader["ViewAction"]);
                        userMenu.IsCreate = Convert.ToInt32(reader["IsCreate"]);
                        userMenu.IsEdit = Convert.ToInt32(reader["IsEdit"]);
                        userMenu.IsDelete = Convert.ToInt32(reader["IsDelete"]);
                        userMenu.IsView = Convert.ToInt32(reader["IsView"]);
                        lists.Add(userMenu);
                    }
                    con.Close();
                }
            }
            return lists;
        }
        public List<UserRoleDomain> GetChieldRole(int orgId, int roleId)
        {
            List<UserRoleDomain> lists = new List<UserRoleDomain>();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Sp_SecGetChieldRole", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OrgId", SqlDbType.Int).Value = orgId;
                    cmd.Parameters.Add("@RoleId", SqlDbType.Int).Value = roleId;
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
        public List<RolePermissionDomain> GetRolePermission(int moduleId, int roleId, int pRoleId)
        {
            List<RolePermissionDomain> lists = new List<RolePermissionDomain>();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Sp_SecGetRoleWisePermission", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@RoleId", SqlDbType.Int).Value = roleId;
                    cmd.Parameters.Add("@ModuleId", SqlDbType.Int).Value = moduleId;
                    cmd.Parameters.Add("@ParentRoleId", SqlDbType.Int).Value = pRoleId;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        RolePermissionDomain list = new RolePermissionDomain();
                        list.RolePermissionId = Convert.ToInt32(reader["RolePermissionId"]);
                        list.MenuId = Convert.ToInt32(reader["MenuId"]);
                        list.MenuName = Convert.ToString(reader["MenuName"]);
                        list.ModuleId = Convert.ToInt32(reader["ModuleId"]);
                        list.RoleId = Convert.ToInt32(reader["RoleId"]);
                        list.AppId = Convert.ToInt32(reader["AppId"]);
                        if (Convert.ToInt32(reader["IsCreate"]) == 1) list.IsCreate = true;
                        else list.IsCreate = false;
                        if (Convert.ToInt32(reader["IsEdit"]) == 1) list.IsEdit = true;
                        else list.IsEdit = false;
                        if (Convert.ToInt32(reader["IsDelete"]) == 1) list.IsDelete = true;
                        else list.IsDelete = false;
                        if (Convert.ToInt32(reader["IsView"]) == 1) list.IsView = true;
                        else list.IsView = false;

                        lists.Add(list);
                    }
                    con.Close();
                }
            }
            return lists;
        }

        #endregion
        #region Report Permission Part
        public List<ReportPermissionDomain> GetReportPermission(int orgId,int appId, int roleId)
        {
            List<ReportPermissionDomain> lists = new List<ReportPermissionDomain>();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Sp_GetReportPermission", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OrgId", SqlDbType.Int).Value = orgId;
                    cmd.Parameters.Add("@RoleId", SqlDbType.Int).Value = roleId;
                    cmd.Parameters.Add("@AppId", SqlDbType.Int).Value = appId;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ReportPermissionDomain list = new ReportPermissionDomain();
                        list.IsPermission = Convert.ToBoolean(reader["permission"]);
                        list.ReportName = reader["ReportName"].ToString();
                        list.ReportId = Convert.ToInt32(reader["ReportId"].ToString());
                        list.AppId = Convert.ToInt32(reader["AppId"].ToString());
                        lists.Add(list);
                    }
                    con.Close();
                }
            }
            return lists;
        }
        public int SaveReportPermission(ReportPermissionDomain entity)
        {
            int rValue = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Sp_reportPermissionAll", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OrgId", SqlDbType.Int).Value = entity.OrgId;
                    cmd.Parameters.Add("@ReportId", SqlDbType.Int).Value = entity.ReportId;
                    cmd.Parameters.Add("@AppId", SqlDbType.Int).Value = entity.AppId;
                    cmd.Parameters.Add("@RoleId", SqlDbType.Int).Value = entity.RoleId;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar).Value = entity.CreatedBy;
                    cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = entity.CreatedDate;
                    rValue = cmd.ExecuteNonQuery();
                }
                con.Close();
            }
            return rValue;
        }
        #endregion
    }
}
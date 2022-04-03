using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ESOL_BO.DbAccess.Security
{
    public class ChangePasswordDomain : BaseDomain
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
    public class ChangePasswordDao
    {
        public int ChangePassword(ChangePasswordDomain entity)
        {
            int rValue = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Update UserInfo Set Password='" + entity.Password + "',UpdatedBy='" + entity.UpdatedBy + "',UpdatedDate='" + entity.UpdatedDate + "' where OrgId='" + entity.OrgId + "' and BranchId='" + entity.BranchId + "' and UserName='" + entity.UserName + "' and UserId = '" + entity.UserId + "'", con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    rValue = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rValue;
        }

    }
}
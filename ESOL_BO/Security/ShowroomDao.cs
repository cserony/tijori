using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ESOL_BO.DbAccess.Security
{
    public class ShowroomDomain : BaseDomain
    {
        public string BranchCode { get; set; }
        public string ContactPerson { get; set; }
    }
    public class ShowroomDao
    {
        public int InsertOrUpdate(ShowroomDomain entity)
        {
            int save = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_BranchAll", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OrgId", SqlDbType.Int).Value = entity.OrgId;
                    cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = entity.BranchId;
                    cmd.Parameters.Add("@BranchCode", SqlDbType.NVarChar).Value = entity.BranchCode;
                    cmd.Parameters.Add("@BranchName", SqlDbType.NVarChar).Value = entity.BranchName;
                    cmd.Parameters.Add("@ContactPerson", SqlDbType.NVarChar).Value = entity.ContactPerson;
                    cmd.Parameters.Add("@MobileNo", SqlDbType.NVarChar).Value = entity.MobileNo;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = entity.Email;
                    cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = entity.Address;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = entity.IsActive;
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
        public List<ShowroomDomain> GetAll(int id)
        {
            List<ShowroomDomain> lists = new List<ShowroomDomain>();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("select * from Branch where OrgId='" + id + "' order by BranchId asc", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ShowroomDomain list = new ShowroomDomain();
                        list.BranchId = Convert.ToInt32(reader["BranchId"]);
                        list.BranchCode = reader["BranchCode"].ToString();
                        list.BranchName = reader["BranchName"].ToString();
                        list.ContactPerson = reader["ContactPerson"].ToString();
                        list.MobileNo = reader["MobileNo"].ToString();
                        list.Email = reader["Email"].ToString();
                        list.Address = reader["Address"].ToString();
                        list.IsActive = Convert.ToBoolean(reader["IsActive"]);
                        if(list.IsActive == true)
                        {
                            list.Status = "Active";
                        }else
                        {
                            list.Status = "Inactive";
                        }
                        list.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                        lists.Add(list);
                    }
                    con.Close();
                }
            }
            return lists;
        }
        public ShowroomDomain GetById(int id)
        {
            ShowroomDomain list = new ShowroomDomain();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("select * from Branch where BranchId='" + id + "'", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list = new ShowroomDomain();
                        list.BranchId = Convert.ToInt32(reader["BranchId"]);
                        list.BranchCode = reader["BranchCode"].ToString();
                        list.BranchName = reader["BranchName"].ToString();
                        list.ContactPerson = reader["ContactPerson"].ToString();
                        list.MobileNo = reader["MobileNo"].ToString();
                        list.Email = reader["Email"].ToString();
                        list.Address = reader["Address"].ToString();
                        list.IsActive = Convert.ToBoolean(reader["IsActive"]);
                    }
                    con.Close();
                }
            }
            return list;
        }
    }    
}
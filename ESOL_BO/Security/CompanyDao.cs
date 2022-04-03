using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ESOL_BO.DbAccess.Security
{
    public class CompanyDomain : BaseDomain
    {
        public int Id { get; set; }
        public string ContactPerson { get; set; }
        public DateTime AgreementDate { get; set; }
    }
    public class CompanyDao
    {
        public int Update(CompanyDomain entity)
        {
            int update = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_OrganizationAll", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OrgId", SqlDbType.Int).Value = entity.OrgId;
                    cmd.Parameters.Add("@OrgName", SqlDbType.NVarChar).Value = entity.OrgName;
                    cmd.Parameters.Add("@ContactPerson", SqlDbType.NVarChar).Value = entity.ContactPerson;
                    cmd.Parameters.Add("@MobileNo", SqlDbType.NVarChar).Value = entity.MobileNo;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = entity.Email;
                    cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = entity.Address;
                    cmd.Parameters.Add("@IsActive", SqlDbType.NVarChar).Value = true;
                    cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar).Value = entity.UpdatedBy;
                    cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = entity.UpdatedDate;
                    cmd.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = "Update";
                    update = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return update;
        }

        public List<CompanyDomain> GetAll()
        {
            List<CompanyDomain> lists = new List<CompanyDomain>();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("select * from Organization", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        CompanyDomain list = new CompanyDomain();
                        list.OrgId = Convert.ToInt32(reader["OrgId"]);
                        list.OrgName = reader["OrgName"].ToString();
                        list.ContactPerson = reader["ContactPerson"].ToString();
                        list.MobileNo = reader["MobileNo"].ToString();
                        list.Email = reader["Email"].ToString();
                        list.Address = reader["Address"].ToString();                        
                        lists.Add(list);
                    }
                    con.Close();
                }
            }
            return lists;
        }

        public CompanyDomain GetById(int id)
        {
            CompanyDomain list = new CompanyDomain();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("select * from Organization where OrgId='" + id + "'", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list = new CompanyDomain();
                        list.OrgId = Convert.ToInt32(reader["OrgId"]);
                        list.OrgName = reader["OrgName"].ToString();
                        list.ContactPerson = reader["ContactPerson"].ToString();
                        list.MobileNo = reader["MobileNo"].ToString();
                        list.Email = reader["Email"].ToString();
                        list.Address = reader["Address"].ToString();
                    }
                    con.Close();
                }
            }
            return list;
        }
    }
}
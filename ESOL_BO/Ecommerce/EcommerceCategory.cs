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
    public class EcommerceCategoryDomain : BaseDomain
    {
        public int CategoryId { get; set; }
    }
    public class EcommerceCategory
    {
        public int InsertOrUpdate(EcommerceCategoryDomain entity)
        {
            int save = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_EcommerceCategoryAll", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OrgId", SqlDbType.Int).Value = entity.OrgId;
                    cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = entity.BranchId;
                    cmd.Parameters.Add("@CategoryId", SqlDbType.Int).Value = entity.CategoryId;
                    cmd.Parameters.Add("@CategoryName", SqlDbType.NVarChar).Value = entity.CategoryName;
                    cmd.Parameters.Add("@ImageName", SqlDbType.NVarChar).Value = entity.ImageName;
                    cmd.Parameters.Add("@IsActive", SqlDbType.NVarChar).Value = entity.IsActive;
                    cmd.Parameters.Add("@Slug", SqlDbType.NVarChar).Value = entity.Slug;
                    cmd.Parameters.Add("@Path", SqlDbType.NVarChar).Value = entity.Path;
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
        public List<EcommerceCategoryDomain> GetByOrg(string where)
        {
            List<EcommerceCategoryDomain> lists = new List<EcommerceCategoryDomain>();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(where, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        EcommerceCategoryDomain list = new EcommerceCategoryDomain();
                        list.CategoryId = Convert.ToInt32(reader["CategoryId"]);
                        list.CategoryName = reader["CategoryName"].ToString();
                        list.ImageName = reader["ImageName"].ToString();
                        list.Slug = reader["Slug"].ToString();
                        lists.Add(list);
                    }
                    con.Close();
                }
            }
            return lists;
        }
        public List<EcommerceCategoryDomain> CategoryAll(string where)
        {
            List<EcommerceCategoryDomain> lists = new List<EcommerceCategoryDomain>();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(where, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        EcommerceCategoryDomain list = new EcommerceCategoryDomain();
                        list.CategoryId = Convert.ToInt32(reader["CategoryId"]);
                        list.CategoryName = reader["CategoryName"].ToString();
                        list.ImageName = reader["ImageName"].ToString();
                        list.Slug = reader["Slug"].ToString();
                        list.CatCount = Convert.ToInt32(reader["CatCount"]);
                        lists.Add(list);
                    }
                    con.Close();
                }
            }
            return lists;
        }
        public EcommerceCategoryDomain GetById(string where)
        {
            EcommerceCategoryDomain list = new EcommerceCategoryDomain();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(where, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list = new EcommerceCategoryDomain();
                        list.CategoryId = Convert.ToInt32(reader["CategoryId"]);
                        list.CategoryName = reader["CategoryName"].ToString();
                        list.ImageName = reader["ImageName"].ToString();
                        list.Slug = reader["Slug"].ToString();
                        list.Path = reader["Path"].ToString();
                    }
                    con.Close();
                }
            }
            return list;
        }
    }
}

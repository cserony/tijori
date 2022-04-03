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
    public class EcommerceSubSubCategoryDomain : BaseDomain
    {
        public int SubSubCategoryId { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public string SubSubCategoryName { get; set; }
    }
    public class EcommerceSubSubCategory
    {
        public int InsertOrUpdate(EcommerceSubSubCategoryDomain entity)
        {
            int save = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_EcommerceSubSubCategoryAll", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OrgId", SqlDbType.Int).Value = entity.OrgId;
                    cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = entity.BranchId;
                    cmd.Parameters.Add("@SubSubCategoryId", SqlDbType.Int).Value = entity.SubSubCategoryId;
                    cmd.Parameters.Add("@SubCategoryId", SqlDbType.Int).Value = entity.SubCategoryId;
                    cmd.Parameters.Add("@CategoryId", SqlDbType.Int).Value = entity.CategoryId;
                    cmd.Parameters.Add("@SubSubCategoryName", SqlDbType.NVarChar).Value = entity.SubSubCategoryName;
                    cmd.Parameters.Add("@ImageName", SqlDbType.NVarChar).Value = entity.ImageName;
                    cmd.Parameters.Add("@Slug", SqlDbType.NVarChar).Value = entity.Slug;
                    cmd.Parameters.Add("@Path", SqlDbType.NVarChar).Value = entity.Path;
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
        public List<EcommerceSubSubCategoryDomain> GetByOrg(string where)
        {
            List<EcommerceSubSubCategoryDomain> lists = new List<EcommerceSubSubCategoryDomain>();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(where, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        EcommerceSubSubCategoryDomain list = new EcommerceSubSubCategoryDomain();
                        list.SubSubCategoryId = Convert.ToInt32(reader["SubSubCategoryId"]);
                        list.SubSubCategoryName = reader["SubSubCategoryName"].ToString();
                        list.ImageName = reader["ImageName"].ToString();
                        list.CategoryName = reader["CategoryName"].ToString();
                        list.SubCategoryName = reader["SubCategoryName"].ToString();
                        list.Slug = reader["Slug"].ToString();
                        lists.Add(list);
                    }
                    con.Close();
                }
            }
            return lists;
        }
        public EcommerceSubSubCategoryDomain GetById(string where)
        {
            EcommerceSubSubCategoryDomain list = new EcommerceSubSubCategoryDomain();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(where, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list = new EcommerceSubSubCategoryDomain();                        
                        list.CategoryId = Convert.ToInt32(reader["CategoryId"]);
                        list.SubCategoryId = Convert.ToInt32(reader["SubCategoryId"]);
                        list.SubSubCategoryId = Convert.ToInt32(reader["SubSubCategoryId"]);
                        list.SubSubCategoryName = reader["SubSubCategoryName"].ToString();
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

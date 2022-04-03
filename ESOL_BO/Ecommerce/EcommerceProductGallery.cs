using ESOL_BO.DbAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ESOL_BO.Ecommerce
{
    public class EcommerceProductGalleryDomain : BaseDomain
    {
        public int GalleryId { get; set; }
        public int ProductId { get; set; }
        public string GalleryPathOne { get; set; }
        public string GalleryPathTwo { get; set; }
        public string GalleryPathThree { get; set; }
        public string GalleryPathFour { get; set; }
        public string GalleryPathFive { get; set; }
    }
    public class EcommerceProductGallery
    {
        public int InsertOrUpdate(EcommerceProductGalleryDomain entity)
        {
            int save = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_EcommerceProductGalleryAll", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OrgId", SqlDbType.Int).Value = entity.OrgId;
                    cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = entity.BranchId;
                    cmd.Parameters.Add("@GalleryId", SqlDbType.Int).Value = entity.GalleryId;
                    cmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = entity.ProductId;
                    cmd.Parameters.Add("@GalleryPathOne", SqlDbType.NVarChar).Value = entity.GalleryPathOne;
                    cmd.Parameters.Add("@GalleryPathTwo", SqlDbType.NVarChar).Value = entity.GalleryPathTwo;
                    cmd.Parameters.Add("@GalleryPathThree", SqlDbType.NVarChar).Value = entity.GalleryPathThree;
                    cmd.Parameters.Add("@GalleryPathFour", SqlDbType.NVarChar).Value = entity.GalleryPathFour;
                    cmd.Parameters.Add("@GalleryPathFive", SqlDbType.NVarChar).Value = entity.GalleryPathFive;
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
        public List<EcommerceProductGalleryDomain> GetByAll()
        {
            List<EcommerceProductGalleryDomain> lists = new List<EcommerceProductGalleryDomain>();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("select * from EcommerceProductGallery order by GalleryId desc", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        EcommerceProductGalleryDomain list = new EcommerceProductGalleryDomain();
                        list.GalleryId = Convert.ToInt32(reader["GalleryId"]);
                        list.ProductId = Convert.ToInt32(reader["ProductId"]);
                        list.GalleryPathOne = reader["GalleryPathOne"].ToString();
                        list.GalleryPathTwo = reader["GalleryPathTwo"].ToString();
                        list.GalleryPathThree = reader["GalleryPathThree"].ToString();
                        list.GalleryPathFour = reader["GalleryPathFour"].ToString();
                        list.GalleryPathFive = reader["GalleryPathFive"].ToString();
                        list.IsActive = Convert.ToBoolean(reader["IsActive"]);
                        lists.Add(list);
                    }
                    con.Close();
                }
            }
            return lists;
        }
        public EcommerceProductGalleryDomain GetById(int id)
        {
            EcommerceProductGalleryDomain list = new EcommerceProductGalleryDomain();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("select * from EcommerceProductGallery where GalleryId='" + id + "'", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list = new EcommerceProductGalleryDomain();
                        list.GalleryId = Convert.ToInt32(reader["GalleryId"]);
                        list.ProductId = Convert.ToInt32(reader["ProductId"]);
                        list.GalleryPathOne = reader["GalleryPathOne"].ToString();
                        list.GalleryPathTwo = reader["GalleryPathTwo"].ToString();
                        list.GalleryPathThree = reader["GalleryPathThree"].ToString();
                        list.GalleryPathFour = reader["GalleryPathFour"].ToString();
                        list.GalleryPathFive = reader["GalleryPathFive"].ToString();
                        list.IsActive = Convert.ToBoolean(reader["IsActive"]);
                    }
                    con.Close();
                }
            }
            return list;
        }
    }
}

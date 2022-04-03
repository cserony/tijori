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
    public class EcommerceWishlistDomain : BaseDomain
    {
        public int WishlistId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public bool isActive { get; set; }
        public double SalePrice { get; set; }
        public double Quantity { get; set; }
    }
    public class EcommerceWishlist
    {
        public int InsertOrUpdate(EcommerceWishlistDomain entity)
        {
            int save = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_EcommerceWishlistAll", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@WishlistId", SqlDbType.Int).Value = entity.WishlistId;
                    cmd.Parameters.Add("@CustomerId", SqlDbType.Int).Value = entity.CustomerId;
                    cmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = entity.ProductId;
                    cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = entity.CreatedDate;
                    cmd.Parameters.Add("@isActive", SqlDbType.Bit).Value = entity.isActive;
                    cmd.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = entity.StatementType;
                    save = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return save;
        }
        public List<EcommerceWishlistDomain> GetByCustomerId(int customerId)
        {
            List<EcommerceWishlistDomain> lists = new List<EcommerceWishlistDomain>();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("select w.*,si.Quantity,p.SalePrice,p.ImagePath,p.Slug,p.ProductName,p.SKU from EcommerceWishlist w left join EcommerceProduct p on p.ProductId=w.ProductId left join EcommerceProductStockInfo si on si.ProductId=p.ProductId where w.CustomerId=" + customerId+" order by w.CreatedDate desc", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        EcommerceWishlistDomain list = new EcommerceWishlistDomain();
                        list.WishlistId = Convert.ToInt32(reader["WishlistId"]);
                        list.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                        list.ProductId = Convert.ToInt32(reader["ProductId"]);
                        list.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                        list.SalePrice = Convert.ToDouble(reader["SalePrice"]);
                        list.Date = list.CreatedDate.ToString("dd-MMM-yyyy");
                        list.ProductName = reader["ProductName"].ToString();
                        list.SKU = reader["SKU"].ToString();
                        list.Slug = reader["Slug"].ToString();
                        list.ImageName = reader["ImagePath"].ToString();
                        if (!string.IsNullOrEmpty(reader["Quantity"].ToString()))
                        {
                            list.Quantity = Convert.ToDouble(reader["Quantity"]);
                        }
                        lists.Add(list);
                    }
                    con.Close();
                }
            }
            return lists;
        }
        public EcommerceWishlistDomain GetById(int wishlistId)
        {
            EcommerceWishlistDomain list = new EcommerceWishlistDomain();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("select * from EcommerceWishlist where WishlistId='"+wishlistId+"'", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list = new EcommerceWishlistDomain();
                        list.WishlistId = Convert.ToInt32(reader["WishlistId"]);
                        list.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                        list.ProductId = Convert.ToInt32(reader["ProductId"]);
                        list.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                        list.Date = list.CreatedDate.ToString("dd-MMM-yyyy");
                    }
                    con.Close();
                }
            }
            return list;
        }
    }
}

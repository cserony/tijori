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
    public class EcommerceProductDomain : BaseDomain
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int SubSubCategoryId { get; set; }
        public string ImagePath { get; set; }
        public int BadgeId { get; set; }
        public int BrandId { get; set; }
        public int ColorId { get; set; }
        public int ServiceTypeId { get; set; }
        public int SizeId { get; set; }
        public int UnitId { get; set; }
        public int WeightId { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public double RegularPrice { get; set; }
        public int Discount { get; set; }
        public double SalePrice { get; set; }
        public string PurchaseNote { get; set; }
        public string ProductVideo { get; set; }
        public string BarCode { get; set; }
        public string QrCode { get; set; }
        public double Quantity { get; set; }
    }
    public class EcommerceProduct
    {
        public int InsertOrUpdate(EcommerceProductDomain entity)
        {
            int save = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_EcommerceProductAll", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OrgId", SqlDbType.Int).Value = entity.OrgId;
                    cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = entity.BranchId;
                    cmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = entity.ProductId;
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = entity.UserId;
                    cmd.Parameters.Add("@ProductName", SqlDbType.NVarChar).Value = entity.ProductName;
                    cmd.Parameters.Add("@CategoryId", SqlDbType.Int).Value = entity.CategoryId;
                    cmd.Parameters.Add("@SubCategoryId", SqlDbType.Int).Value = entity.SubCategoryId;
                    cmd.Parameters.Add("@SubSubCategoryId", SqlDbType.Int).Value = entity.SubSubCategoryId;
                    cmd.Parameters.Add("@ImagePath", SqlDbType.NVarChar).Value = entity.ImagePath;
                    cmd.Parameters.Add("@BadgeId", SqlDbType.Int).Value = entity.BadgeId;
                    cmd.Parameters.Add("@BrandId", SqlDbType.Int).Value = entity.BrandId;
                    cmd.Parameters.Add("@ColorId", SqlDbType.Int).Value = entity.ColorId;
                    cmd.Parameters.Add("@ServiceTypeId", SqlDbType.Int).Value = entity.ServiceTypeId;
                    cmd.Parameters.Add("@SizeId", SqlDbType.Int).Value = entity.SizeId;
                    cmd.Parameters.Add("@UnitId", SqlDbType.Int).Value = entity.UnitId;
                    cmd.Parameters.Add("@WeightId", SqlDbType.Int).Value = entity.WeightId;
                    cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = entity.Description;
                    cmd.Parameters.Add("@ShortDescription", SqlDbType.NVarChar).Value = entity.ShortDescription;
                    cmd.Parameters.Add("@RegularPrice", SqlDbType.Decimal).Value = entity.RegularPrice;
                    cmd.Parameters.Add("@Discount", SqlDbType.Int).Value = entity.Discount;
                    cmd.Parameters.Add("@SalePrice", SqlDbType.Decimal).Value = entity.SalePrice;
                    cmd.Parameters.Add("@SKU", SqlDbType.NVarChar).Value = entity.SKU;
                    cmd.Parameters.Add("@PurchaseNote", SqlDbType.NVarChar).Value = entity.PurchaseNote;
                    cmd.Parameters.Add("@ProductVideo", SqlDbType.NVarChar).Value = entity.ProductVideo;
                    cmd.Parameters.Add("@BarCode", SqlDbType.NVarChar).Value = entity.BarCode;
                    cmd.Parameters.Add("@QrCode", SqlDbType.NVarChar).Value = entity.QrCode;
                    cmd.Parameters.Add("@Slug", SqlDbType.NVarChar).Value = entity.Slug;
                    cmd.Parameters.Add("@Path", SqlDbType.NVarChar).Value = entity.Path;
                    cmd.Parameters.Add("@IsActive", SqlDbType.NVarChar).Value = entity.IsActive;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar).Value = entity.CreatedBy;
                    cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = entity.CreatedDate;
                    cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar).Value = entity.UpdatedBy;
                    cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = entity.UpdatedDate;
                    cmd.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = entity.StatementType;
                    save = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                }
            }
            return save;
        }
        public List<EcommerceProductDomain> GetByOrg(int userId)
        {
            List<EcommerceProductDomain> lists = new List<EcommerceProductDomain>();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("select * from EcommerceProduct", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        EcommerceProductDomain list = new EcommerceProductDomain();
                        list.ProductId = Convert.ToInt32(reader["ProductId"]);
                        list.UserId = Convert.ToInt32(reader["UserId"]);
                        list.ProductName = reader["ProductName"].ToString();
                        list.CategoryId = Convert.ToInt32(reader["CategoryId"]);
                        list.SubCategoryId = Convert.ToInt32(reader["SubCategoryId"]);
                        list.SubSubCategoryId = Convert.ToInt32(reader["SubSubCategoryId"]);
                        list.ImagePath = reader["ImagePath"].ToString();
                        list.BadgeId = Convert.ToInt32(reader["BadgeId"]);
                        list.BrandId = Convert.ToInt32(reader["BrandId"]);
                        list.ColorId = Convert.ToInt32(reader["ColorId"]);
                        list.ServiceTypeId = Convert.ToInt32(reader["ServiceTypeId"]);
                        list.SizeId = Convert.ToInt32(reader["SizeId"]);
                        list.UnitId = Convert.ToInt32(reader["UnitId"]);
                        list.WeightId = Convert.ToInt32(reader["WeightId"]);
                        list.Description = reader["Description"].ToString();
                        list.ShortDescription = reader["ShortDescription"].ToString();
                        list.RegularPrice = Convert.ToDouble(reader["RegularPrice"]);
                        list.Discount = Convert.ToInt32(reader["Discount"]);
                        list.SalePrice = Convert.ToDouble(reader["SalePrice"]);
                        list.SKU = reader["SKU"].ToString();
                        list.PurchaseNote = reader["PurchaseNote"].ToString();
                        list.ProductVideo = reader["ProductVideo"].ToString();
                        list.BarCode = reader["BarCode"].ToString();
                        list.QrCode = reader["QrCode"].ToString();
                        list.Slug = reader["Slug"].ToString();
                        list.Path = reader["Path"].ToString();
                        lists.Add(list);
                    }
                    con.Close();
                }
            }
            return lists;
        }
        public EcommerceProductDomain GetById(int productId)
        {
            EcommerceProductDomain list = new EcommerceProductDomain();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("select * from EcommerceProduct where ProductId='"+productId+"'", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list = new EcommerceProductDomain();
                        list.ProductId = Convert.ToInt32(reader["ProductId"]);
                        list.UserId = Convert.ToInt32(reader["UserId"]);
                        list.ProductName = reader["ProductName"].ToString();
                        list.CategoryId = Convert.ToInt32(reader["CategoryId"]);
                        list.SubCategoryId = Convert.ToInt32(reader["SubCategoryId"]);
                        list.SubSubCategoryId = Convert.ToInt32(reader["SubSubCategoryId"]);
                        list.ImagePath = reader["ImagePath"].ToString();
                        list.BadgeId = Convert.ToInt32(reader["BadgeId"]);
                        list.BrandId = Convert.ToInt32(reader["BrandId"]);
                        list.ColorId = Convert.ToInt32(reader["ColorId"]);
                        list.ServiceTypeId = Convert.ToInt32(reader["ServiceTypeId"]);
                        list.SizeId = Convert.ToInt32(reader["SizeId"]);
                        list.UnitId = Convert.ToInt32(reader["UnitId"]);
                        list.WeightId = Convert.ToInt32(reader["WeightId"]);
                        list.Description = reader["Description"].ToString();
                        list.ShortDescription = reader["ShortDescription"].ToString();
                        list.RegularPrice = Convert.ToDouble(reader["RegularPrice"]);
                        list.Discount = Convert.ToInt32(reader["Discount"]);
                        list.SalePrice = Convert.ToDouble(reader["SalePrice"]);
                        list.SKU = reader["SKU"].ToString();
                        list.PurchaseNote = reader["PurchaseNote"].ToString();
                        list.ProductVideo = reader["ProductVideo"].ToString();
                        list.BarCode = reader["BarCode"].ToString();
                        list.QrCode = reader["QrCode"].ToString();
                        list.Slug = reader["Slug"].ToString();
                        list.Path = reader["Path"].ToString();
                    }
                    con.Close();
                }
            }
            return list;
        }
        public List<EcommerceProductDomain> GetByProductId(int productId)
        {
            List<EcommerceProductDomain> lists = new List<EcommerceProductDomain>();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("select * from EcommerceProduct where ProductId='" + productId + "'", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        EcommerceProductDomain list = new EcommerceProductDomain();
                        list.ProductId = Convert.ToInt32(reader["ProductId"]);
                        list.UserId = Convert.ToInt32(reader["UserId"]);
                        list.ProductName = reader["ProductName"].ToString();
                        list.CategoryId = Convert.ToInt32(reader["CategoryId"]);
                        list.SubCategoryId = Convert.ToInt32(reader["SubCategoryId"]);
                        list.SubSubCategoryId = Convert.ToInt32(reader["SubSubCategoryId"]);
                        list.ImagePath = reader["ImagePath"].ToString();
                        list.BadgeId = Convert.ToInt32(reader["BadgeId"]);
                        list.BrandId = Convert.ToInt32(reader["BrandId"]);
                        list.ColorId = Convert.ToInt32(reader["ColorId"]);
                        list.ServiceTypeId = Convert.ToInt32(reader["ServiceTypeId"]);
                        list.SizeId = Convert.ToInt32(reader["SizeId"]);
                        list.UnitId = Convert.ToInt32(reader["UnitId"]);
                        list.WeightId = Convert.ToInt32(reader["WeightId"]);
                        list.Description = reader["Description"].ToString();
                        list.ShortDescription = reader["ShortDescription"].ToString();
                        list.RegularPrice = Convert.ToDouble(reader["RegularPrice"]);
                        list.Discount = Convert.ToInt32(reader["Discount"]);
                        list.SalePrice = Convert.ToDouble(reader["SalePrice"]);
                        list.SKU = reader["SKU"].ToString();
                        list.PurchaseNote = reader["PurchaseNote"].ToString();
                        list.ProductVideo = reader["ProductVideo"].ToString();
                        list.BarCode = reader["BarCode"].ToString();
                        list.QrCode = reader["QrCode"].ToString();
                        list.Slug = reader["Slug"].ToString();
                        list.Path = reader["Path"].ToString();
                        lists.Add(list);
                    }
                    con.Close();
                }
            }
            return lists;
        }
        public List<EcommerceProductDomain> GetAllProduct(string slug, int id)
        {
            List<EcommerceProductDomain> lists = new List<EcommerceProductDomain>();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_EcommerceShopData", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@slug", SqlDbType.NVarChar).Value = slug;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        EcommerceProductDomain list = new EcommerceProductDomain();
                        list.ProductId = Convert.ToInt32(reader["ProductId"]);
                        list.ProductName = reader["ProductName"].ToString();
                        list.ImagePath = reader["ImagePath"].ToString();
                        list.Path = reader["Path"].ToString();
                        list.BadgeName = reader["BadgeName"].ToString();
                        list.Slug = reader["Slug"].ToString();
                        list.RegularPrice = Convert.ToDouble(reader["RegularPrice"]);
                        list.Discount = Convert.ToInt32(reader["Discount"]);
                        list.SalePrice = Convert.ToDouble(reader["SalePrice"]);
                        lists.Add(list);
                    }
                    con.Close();
                }
            }
            return lists;
        }
        public EcommerceProductDomain GetSingleProduct(string slug)
        {
            EcommerceProductDomain list = new EcommerceProductDomain();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("select p.CategoryId,p.SubCategoryId,p.SubSubCategoryId,p.ProductId,p.ProductName,p.ImagePath,p.RegularPrice,p.Discount,p.SalePrice,p.Slug,p.Path,b.BadgeName,p.Description,p.ShortDescription,si.Quantity from EcommerceProduct p left join EcommerceBadge b on b.BadgeId=p.BadgeId left join EcommerceProductStockInfo si on si.ProductId=p.ProductId where p.Slug='" + slug+"'", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list = new EcommerceProductDomain();
                        list.ProductId = Convert.ToInt32(reader["ProductId"]);
                        list.CategoryId = Convert.ToInt32(reader["CategoryId"]);
                        list.SubCategoryId = Convert.ToInt32(reader["SubCategoryId"]);
                        list.SubSubCategoryId = Convert.ToInt32(reader["SubSubCategoryId"]);
                        list.ProductId = Convert.ToInt32(reader["ProductId"]);
                        list.ProductName = reader["ProductName"].ToString();
                        list.ImagePath = reader["ImagePath"].ToString();
                        list.Path = reader["Path"].ToString();
                        list.BadgeName = reader["BadgeName"].ToString();
                        list.Slug = reader["Slug"].ToString();
                        list.RegularPrice = Convert.ToDouble(reader["RegularPrice"]);
                        list.Discount = Convert.ToInt32(reader["Discount"]);
                        list.SalePrice = Convert.ToDouble(reader["SalePrice"]);
                        list.Description = reader["Description"].ToString();
                        list.ShortDescription = reader["ShortDescription"].ToString();
                        if (!string.IsNullOrEmpty(reader["Quantity"].ToString()))
                        {
                            list.Quantity = Convert.ToDouble(reader["Quantity"]);
                        }                        
                    }
                    con.Close();
                }
            }
            return list;
        }
        public List<EcommerceProductDomain> GetProductRelated(int catId, string type)
        {
            List<EcommerceProductDomain> lists = new List<EcommerceProductDomain>();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_EcommerceProductRelated", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CategoryId", SqlDbType.Int).Value = catId;
                    cmd.Parameters.Add("@Type", SqlDbType.NVarChar).Value = type;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        EcommerceProductDomain list = new EcommerceProductDomain();
                        list.ProductId = Convert.ToInt32(reader["ProductId"]);
                        list.ProductName = reader["ProductName"].ToString();
                        list.ImagePath = reader["ImagePath"].ToString();
                        list.Path = reader["Path"].ToString();
                        list.BadgeName = reader["BadgeName"].ToString();
                        list.Slug = reader["Slug"].ToString();
                        list.RegularPrice = Convert.ToDouble(reader["RegularPrice"]);
                        list.Discount = Convert.ToInt32(reader["Discount"]);
                        list.SalePrice = Convert.ToDouble(reader["SalePrice"]);
                        lists.Add(list);
                    }
                    con.Close();
                }
            }
            return lists;
        }
    }
}

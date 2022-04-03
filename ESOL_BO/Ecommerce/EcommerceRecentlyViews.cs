using ESOL_BO.DbAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ESOL_BO.Ecommerce
{
    public class EcommerceRecentlyViewsDomain : BaseDomain
    {
        public int ViewId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public DateTime ViewDate { get; set; }
    }
    public class EcommerceRecentlyViews
    {
        public int InsertOrUpdate(EcommerceRecentlyViewsDomain entity)
        {
            int save = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("EcommerceRecentlyViews", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ViewId", SqlDbType.Int).Value = entity.ViewId;
                    cmd.Parameters.Add("@CustomerId", SqlDbType.Int).Value = entity.CustomerId;
                    cmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = entity.ProductId;
                    cmd.Parameters.Add("@ViewDate", SqlDbType.DateTime).Value = entity.ViewDate;
                    cmd.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = entity.StatementType;
                    save = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return save;
        }
        public List<EcommerceRecentlyViewsDomain> GetByAll()
        {
            List<EcommerceRecentlyViewsDomain> lists = new List<EcommerceRecentlyViewsDomain>();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("select c.CustomerId,c.CustomerName,c.MobileNo,p.ProductName,cat.CategoryName from EcommerceRecentlyViews v left join EcommerceCustomer c on c.CustomerId=v.CustomerId left join EcommerceProduct p on p.ProductId=v.ProductId left join EcommerceCategory cat on cat.CategoryId=p.CategoryId group by c.CustomerName,c.MobileNo,p.ProductName,cat.CategoryName,c.CustomerId", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        EcommerceRecentlyViewsDomain list = new EcommerceRecentlyViewsDomain();
                        list.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                        list.CustomerName = reader["CustomerName"].ToString();
                        list.MobileNo = reader["MobileNo"].ToString();
                        list.ProductName = reader["ProductName"].ToString();
                        list.CategoryName = reader["CategoryName"].ToString();
                        lists.Add(list);
                    }
                    con.Close();
                }
            }
            return lists;
        }
        public EcommerceRecentlyViewsDomain GetById(int id)
        {
            EcommerceRecentlyViewsDomain list = new EcommerceRecentlyViewsDomain();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("select * from EcommerceRecentlyViews where ViewId='" + id + "'", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list = new EcommerceRecentlyViewsDomain();
                        list.ViewId = Convert.ToInt32(reader["ViewId"]);
                        list.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                        list.ProductId = Convert.ToInt32(reader["ProductId"]);
                        list.ViewDate = Convert.ToDateTime(reader["ViewDate"]);
                    }
                    con.Close();
                }
            }
            return list;
        }
    }
}

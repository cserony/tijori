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
    public class EcommerceOrderDetailsDomain : BaseDomain
    {
        public int OrderDetailsId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public double Quantity { get; set; }
        public double ItemPrice { get; set; }
        public double TotalPrice { get; set; }
        public double Discount { get; set; }
        public DateTime OrderDate { get; set; }
    }
    public class EcommerceOrderDetails
    {
        public int InsertOrUpdate(EcommerceOrderDetailsDomain entity)
        {
            int save = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_EcommerceOrderDetails", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OrderDetailsId", SqlDbType.Int).Value = entity.OrderDetailsId;
                    cmd.Parameters.Add("@OrderId", SqlDbType.Int).Value = entity.OrderId;
                    cmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = entity.ProductId;
                    cmd.Parameters.Add("@ItemPrice", SqlDbType.Decimal).Value = entity.ItemPrice;
                    cmd.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = entity.Quantity;
                    cmd.Parameters.Add("@Discount", SqlDbType.Decimal).Value = entity.Discount;
                    cmd.Parameters.Add("@TotalPrice", SqlDbType.Decimal).Value = entity.TotalPrice;
                    cmd.Parameters.Add("@OrderDate", SqlDbType.DateTime).Value = entity.OrderDate;
                    cmd.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = entity.StatementType;
                    save = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return save;
        }
        public List<EcommerceOrderDetailsDomain> GetByOrderId(int orderId)
        {
            List<EcommerceOrderDetailsDomain> lists = new List<EcommerceOrderDetailsDomain>();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("select od.*,p.ProductName,p.SKU from EcommerceOrderDetails od left join EcommerceProduct p on p.ProductId=od.ProductId where od.OrderId='"+orderId+"'", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        EcommerceOrderDetailsDomain list = new EcommerceOrderDetailsDomain();
                        list.OrderDetailsId = Convert.ToInt32(reader["OrderDetailsId"]);
                        list.OrderId = Convert.ToInt32(reader["OrderId"]);
                        list.ProductId = Convert.ToInt32(reader["ProductId"]);
                        list.ItemPrice = Convert.ToDouble(reader["ItemPrice"]);
                        list.Quantity = Convert.ToDouble(reader["Quantity"]);
                        list.Discount = Convert.ToDouble(reader["Discount"]);
                        list.TotalPrice = Convert.ToDouble(reader["TotalPrice"]);
                        list.OrderDate = Convert.ToDateTime(reader["OrderDate"]);
                        list.ProductName = reader["ProductName"].ToString();
                        list.SKU = reader["SKU"].ToString();
                        lists.Add(list);
                    }
                    con.Close();
                }
            }
            return lists;
        }
        public EcommerceOrderDetailsDomain GetById(int id)
        {
            EcommerceOrderDetailsDomain list = new EcommerceOrderDetailsDomain();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("select * from EcommerceOrderDetails where OrderDetailsId='"+id+"'", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list = new EcommerceOrderDetailsDomain();
                        list.OrderDetailsId = Convert.ToInt32(reader["OrderDetailsId"]);
                        list.OrderId = Convert.ToInt32(reader["OrderId"]);
                        list.ProductId = Convert.ToInt32(reader["ProductId"]);
                        list.ItemPrice = Convert.ToDouble(reader["ItemPrice"]);
                        list.Quantity = Convert.ToDouble(reader["Quantity"]);
                        list.Discount = Convert.ToDouble(reader["Discount"]);
                        list.TotalPrice = Convert.ToDouble(reader["TotalPrice"]);
                        list.OrderDate = Convert.ToDateTime(reader["OrderDate"]);
                    }
                    con.Close();
                }
            }
            return list;
        }
    }
}

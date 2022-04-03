using ESOL_BO.DbAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ESOL_BO.Ecommerce
{
    public class EcommerceOrderDomain : BaseDomain
    {
        public int OrderId { get; set; }
        public string InvoiceNo { get; set; }
        public int CustomerId { get; set; }
        public int PaymentId { get; set; }
        public int ShippingId { get; set; }
        public double Discount { get; set; }
        public double Taxes { get; set; }
        public int CouponId { get; set; }
        public double TotalAmount { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime OrderDate { get; set; }
        public bool Dispatched { get; set; }
        public DateTime DispatchedDate { get; set; }
        public bool IsShipped { get; set; }
        public DateTime ShippingDate { get; set; }
        public bool IsDeliver { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Notes { get; set; }
        public bool CancelOrder { get; set; }
        public string IPAddress { get; set; }
        public string Browser { get; set; }
        public List<EcommerceOrderDetailsDomain> ItemList = new List<EcommerceOrderDetailsDomain>();
    }
    public class EcommerceOrderRequestUpdate : BaseDomain
    {
        public int UpdateId { get; set; }
        public int OrderId { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public int UpdateById { get; set; }
        public int UserTypeId { get; set; }
        public string Remarks { get; set; }
    }
    public class RecentlyViews : BaseDomain
    {
        public int ViewId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public DateTime ViewDate { get; set; }
    }
    public class EcommerceOrder
    {
        public int InsertOrUpdate(EcommerceOrderDomain entity)
        {
            int save = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_EcommerceOrderAll", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OrderId", SqlDbType.Int).Value = entity.OrderId;
                    cmd.Parameters.Add("@InvoiceNo", SqlDbType.NVarChar).Value = entity.InvoiceNo;
                    cmd.Parameters.Add("@CustomerId", SqlDbType.Int).Value = entity.CustomerId;
                    cmd.Parameters.Add("@PaymentId", SqlDbType.Int).Value = entity.PaymentId;
                    cmd.Parameters.Add("@ShippingId", SqlDbType.Int).Value = entity.ShippingId;
                    cmd.Parameters.Add("@Discount", SqlDbType.Decimal).Value = entity.Discount;
                    cmd.Parameters.Add("@Taxes", SqlDbType.Decimal).Value = entity.Taxes;
                    cmd.Parameters.Add("@CouponId", SqlDbType.Int).Value = entity.CouponId;
                    cmd.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = entity.TotalAmount;
                    cmd.Parameters.Add("@IsCompleted", SqlDbType.Bit).Value = entity.IsCompleted;
                    cmd.Parameters.Add("@OrderDate", SqlDbType.DateTime).Value = entity.OrderDate;
                    cmd.Parameters.Add("@Dispatched", SqlDbType.Bit).Value = entity.Dispatched;
                    cmd.Parameters.Add("@DispatchedDate", SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@IsShipped", SqlDbType.Bit).Value = entity.IsShipped;
                    cmd.Parameters.Add("@ShippingDate", SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@IsDeliver", SqlDbType.Bit).Value = entity.IsDeliver;
                    cmd.Parameters.Add("@DeliveryDate", SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@Notes", SqlDbType.VarChar).Value = entity.Notes;
                    cmd.Parameters.Add("@CancelOrder", SqlDbType.Bit).Value = entity.CancelOrder;
                    cmd.Parameters.Add("@IPAddress", SqlDbType.NVarChar).Value = entity.IPAddress;
                    cmd.Parameters.Add("@Browser", SqlDbType.NVarChar).Value = entity.Browser;
                    cmd.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = entity.StatementType;
                    save = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                }
            }
            return save;
        }
        public int InsertOrderRequestUpdate(EcommerceOrderRequestUpdate entity)
        {
            int save = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_EcommerceOrderRequestUpdateAll", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UpdateId", SqlDbType.Int).Value = entity.UpdateId;
                    cmd.Parameters.Add("@OrderId", SqlDbType.Int).Value = entity.OrderId;
                    cmd.Parameters.Add("@UpdateDateTime", SqlDbType.DateTime).Value = entity.UpdateDateTime;
                    cmd.Parameters.Add("@UpdateById", SqlDbType.Int).Value = entity.UpdateById;
                    cmd.Parameters.Add("@StatusId", SqlDbType.Int).Value = entity.StatusId;
                    cmd.Parameters.Add("@UserTypeId", SqlDbType.Int).Value = entity.UserTypeId;
                    cmd.Parameters.Add("@Remarks", SqlDbType.NVarChar).Value = entity.Remarks;
                    cmd.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = entity.StatementType;
                    save = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                }
            }
            return save;
        }
        public int InsertRecentlyViews(RecentlyViews entity)
        {
            int save = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_EcommerceRecentlyViewsAll", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ViewId", SqlDbType.Int).Value = entity.ViewId;
                    cmd.Parameters.Add("@CustomerId", SqlDbType.Int).Value = entity.CustomerId;
                    cmd.Parameters.Add("@ViewDate", SqlDbType.DateTime).Value = entity.ViewDate;
                    cmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = entity.ProductId;
                    cmd.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = entity.StatementType;
                    save = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                }
            }
            return save;
        }
        public List<EcommerceOrderDomain> GetByAll(string where)
        {
            List<EcommerceOrderDomain> lists = new List<EcommerceOrderDomain>();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("select o.*,c.CustomerName,sd.Mobile,sd.Address,s.StatusName from EcommerceOrder o left join EcommerceCustomer c on c.CustomerId=o.CustomerId left join EcommerceShippingDetails sd on sd.ShippingId=o.ShippingId left join EcommerceOrderStatus s on s.StatusId=o.StatusId" + where, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        EcommerceOrderDomain list = new EcommerceOrderDomain();
                        list.OrderId = Convert.ToInt32(reader["OrderId"]);
                        list.InvoiceNo = reader["InvoiceNo"].ToString();
                        list.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                        list.PaymentId = Convert.ToInt32(reader["PaymentId"]);
                        list.ShippingId = Convert.ToInt32(reader["ShippingId"]);
                        list.Discount = Convert.ToDouble(reader["Discount"]);
                        list.Taxes = Convert.ToDouble(reader["Taxes"]);
                        list.CouponId =Convert.ToInt32(reader["CouponId"]);
                        list.TotalAmount = Convert.ToDouble(reader["TotalAmount"]);
                        list.IsCompleted = Convert.ToBoolean(reader["IsCompleted"]);
                        list.OrderDate = Convert.ToDateTime(reader["OrderDate"]);                        
                        list.IPAddress = reader["IPAddress"].ToString();
                        list.Browser = reader["Browser"].ToString();
                        list.CustomerName = reader["CustomerName"].ToString();
                        list.MobileNo = reader["Mobile"].ToString();
                        list.Address = reader["Address"].ToString();
                        list.Status = reader["StatusName"].ToString();
                        lists.Add(list);
                    }
                    con.Close();
                }
            }
            return lists;
        }
        public EcommerceOrderDomain GetById(int id)
        {
            EcommerceOrderDomain list = new EcommerceOrderDomain();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("select o.*,c.CustomerName,sd.Mobile,sd.Address,org.OrgName,org.Address as OrgAddress,org.MobileNo as OrgMobileNo,[dbo].[InitCap](dbo.GET_NUM2WORD(TotalAmount))as Word,pt.PaymentTypeName from EcommerceOrder o left join EcommerceCustomer c on c.CustomerId=o.CustomerId left join EcommerceShippingDetails sd on sd.ShippingId=o.ShippingId left join Organization org on org.OrgId=1 left join EcommercePaymentType pt on pt.PaymentTypeId=o.PaymentId where o.OrderId='" + id + "'", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list = new EcommerceOrderDomain();
                        list.OrderId = Convert.ToInt32(reader["OrderId"]);
                        list.InvoiceNo = reader["InvoiceNo"].ToString();
                        list.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                        list.PaymentId = Convert.ToInt32(reader["PaymentId"]);
                        list.ShippingId = Convert.ToInt32(reader["ShippingId"]);
                        list.Discount = Convert.ToDouble(reader["Discount"]);
                        list.Taxes = Convert.ToDouble(reader["Taxes"]);
                        list.CouponId = Convert.ToInt32(reader["CouponId"]);
                        list.TotalAmount = Convert.ToDouble(reader["TotalAmount"]);
                        list.IsCompleted = Convert.ToBoolean(reader["IsCompleted"]);
                        list.OrderDate = Convert.ToDateTime(reader["OrderDate"]);
                        list.IPAddress = reader["IPAddress"].ToString();
                        list.Browser = reader["Browser"].ToString();
                        list.CustomerName = reader["CustomerName"].ToString();
                        list.MobileNo = reader["Mobile"].ToString();
                        list.Address = reader["Address"].ToString();
                        list.OrgName = reader["OrgName"].ToString();
                        list.OrgAddress = reader["OrgAddress"].ToString();
                        list.OrgMobileNo = reader["OrgMobileNo"].ToString();
                        list.Notes = reader["Word"].ToString();
                        list.Path = reader["PaymentTypeName"].ToString();
                    }
                    con.Close();
                }
            }
            return list;
        }
        public string AutoId()
        {
            string code = "";
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("select count(OrderId)+1 as code from EcommerceOrder", con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        code = reader["code"].ToString();
                    }
                    con.Close();
                }
            }
            return code;
        }
        public List<EcommerceOrderDomain> GetByCustomerId(int customerId)
        {
            List<EcommerceOrderDomain> lists = new List<EcommerceOrderDomain>();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("select o.OrderId,o.InvoiceNo,o.OrderDate,o.TotalAmount,s.StatusId,s.StatusName from EcommerceOrder o left join EcommerceOrderStatus s on s.StatusId=(select top 1 StatusId from EcommerceOrderRequestUpdate where OrderId=o.OrderId order by o.OrderId desc) where o.CustomerId=" + customerId+" order by o.OrderDate desc", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        EcommerceOrderDomain list = new EcommerceOrderDomain();
                        list.OrderId = Convert.ToInt32(reader["OrderId"]);
                        list.StatusId = Convert.ToInt32(reader["StatusId"]);
                        list.InvoiceNo = reader["InvoiceNo"].ToString();
                        list.TotalAmount = Convert.ToDouble(reader["TotalAmount"]);
                        list.OrderDate = Convert.ToDateTime(reader["OrderDate"]);
                        list.Date = list.OrderDate.ToString("dd-MMM-yyyy");
                        list.Status = reader["StatusName"].ToString();
                        lists.Add(list);
                    }
                    con.Close();
                }
            }
            return lists;
        }
        public EcommerceOrderDomain GetByCustomerInvoice(int customerId, int id)
        {
            EcommerceOrderDomain list = new EcommerceOrderDomain();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("select o.*,c.CustomerName,sd.Mobile,sd.Address,org.OrgName,org.Address as OrgAddress,org.MobileNo as OrgMobileNo,[dbo].[InitCap](dbo.GET_NUM2WORD(TotalAmount))as Word,pt.PaymentTypeName from EcommerceOrder o left join EcommerceCustomer c on c.CustomerId=o.CustomerId left join EcommerceShippingDetails sd on sd.ShippingId=o.ShippingId left join Organization org on org.OrgId=1 left join EcommercePaymentType pt on pt.PaymentTypeId=o.PaymentId where o.CustomerId='" + customerId + "' and o.OrderId='" + id + "'", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list = new EcommerceOrderDomain();
                        list.OrderId = Convert.ToInt32(reader["OrderId"]);
                        list.InvoiceNo = reader["InvoiceNo"].ToString();
                        list.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                        list.PaymentId = Convert.ToInt32(reader["PaymentId"]);
                        list.ShippingId = Convert.ToInt32(reader["ShippingId"]);
                        list.Discount = Convert.ToDouble(reader["Discount"]);
                        list.Taxes = Convert.ToDouble(reader["Taxes"]);
                        list.CouponId = Convert.ToInt32(reader["CouponId"]);
                        list.TotalAmount = Convert.ToDouble(reader["TotalAmount"]);
                        list.IsCompleted = Convert.ToBoolean(reader["IsCompleted"]);
                        list.OrderDate = Convert.ToDateTime(reader["OrderDate"]);
                        list.IPAddress = reader["IPAddress"].ToString();
                        list.Browser = reader["Browser"].ToString();
                        list.CustomerName = reader["CustomerName"].ToString();
                        list.MobileNo = reader["Mobile"].ToString();
                        list.Address = reader["Address"].ToString();
                        list.OrgName = reader["OrgName"].ToString();
                        list.OrgAddress = reader["OrgAddress"].ToString();
                        list.OrgMobileNo = reader["OrgMobileNo"].ToString();
                        list.Notes = reader["Word"].ToString();
                        list.Path = reader["PaymentTypeName"].ToString();
                    }
                    con.Close();
                }
            }
            return list;
        }
    }
}

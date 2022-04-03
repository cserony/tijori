using ESOL_BO.DbAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ESOL_BO.Ecommerce
{
    public class EcommerceCouponDomain : BaseDomain
    {
        public int CouponId { get; set; }
        public string DiscountType { get; set; }
        public string CouponCode { get; set; }
        public string Description { get; set; }
        public double CouponAmount { get; set; }
        public DateTime CouponFromDate { get; set; }
        public DateTime CouponToDate { get; set; }
        public double MinimumSpend { get; set; }
        public double MaximumSpend { get; set; }
        public int UsageLimitPerUser { get; set; }
        public bool IsEmail { get; set; }
    }
    public class EcommerceCoupon
    {
        public int InsertOrUpdate(EcommerceCouponDomain entity)
        {
            int save = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_EcommerceCouponAll", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CouponId", SqlDbType.Int).Value = entity.CouponId;
                    cmd.Parameters.Add("@DiscountType", SqlDbType.VarChar).Value = entity.DiscountType;
                    cmd.Parameters.Add("@CouponCode", SqlDbType.VarChar).Value = entity.CouponCode;
                    cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = entity.Description;
                    cmd.Parameters.Add("@CouponAmount", SqlDbType.Decimal).Value = entity.CouponAmount;
                    cmd.Parameters.Add("@CouponFromDate", SqlDbType.DateTime).Value = entity.CouponFromDate;
                    cmd.Parameters.Add("@CouponToDate", SqlDbType.DateTime).Value = entity.CouponToDate;
                    cmd.Parameters.Add("@MinimumSpend", SqlDbType.Decimal).Value = entity.MinimumSpend;
                    cmd.Parameters.Add("@MaximumSpend", SqlDbType.Decimal).Value = entity.MaximumSpend;
                    cmd.Parameters.Add("@UsageLimitPerUser", SqlDbType.Int).Value = entity.UsageLimitPerUser;
                    cmd.Parameters.Add("@IsEmail", SqlDbType.NVarChar).Value = entity.IsEmail;
                    cmd.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = entity.StatementType;
                    save = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return save;
        }
        public List<EcommerceCouponDomain> GetByAll()
        {
            List<EcommerceCouponDomain> lists = new List<EcommerceCouponDomain>();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("select *,CASE WHEN DiscountType='F' THEN 'Percentage Discount' WHEN DiscountType='P' THEN 'Fixed Cart Discount'  ELSE '' END as Status from EcommerceCoupon order by CouponId desc", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        EcommerceCouponDomain list = new EcommerceCouponDomain();
                        list.CouponId = Convert.ToInt32(reader["CouponId"]);
                        list.DiscountType = reader["DiscountType"].ToString();
                        list.CouponCode = reader["CouponCode"].ToString();
                        list.Description = reader["Description"].ToString();
                        list.CouponAmount = Convert.ToDouble(reader["CouponAmount"]);
                        list.CouponFromDate = Convert.ToDateTime(reader["CouponFromDate"]);
                        list.CouponToDate = Convert.ToDateTime(reader["CouponToDate"]);
                        list.Date = list.CouponToDate.ToString("dd-MMM-yyyy");
                        list.MinimumSpend = Convert.ToDouble(reader["MinimumSpend"]);
                        list.MaximumSpend = Convert.ToDouble(reader["MaximumSpend"]);
                        list.UsageLimitPerUser = Convert.ToInt32(reader["UsageLimitPerUser"]);
                        list.IsEmail = Convert.ToBoolean(reader["IsEmail"]);
                        list.Status = reader["Status"].ToString();
                        lists.Add(list);
                    }
                    con.Close();
                }
            }
            return lists;
        }
        public EcommerceCouponDomain GetById(int id)
        {
            EcommerceCouponDomain list = new EcommerceCouponDomain();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("select * from EcommerceCoupon where CouponId='" + id + "'", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list = new EcommerceCouponDomain();
                        list.CouponId = Convert.ToInt32(reader["CouponId"]);
                        list.DiscountType = reader["DiscountType"].ToString();
                        list.CouponCode = reader["CouponCode"].ToString();
                        list.Description = reader["Description"].ToString();
                        list.CouponAmount = Convert.ToDouble(reader["CouponAmount"]);
                        list.CouponFromDate = Convert.ToDateTime(reader["CouponFromDate"]);
                        list.CouponToDate = Convert.ToDateTime(reader["CouponToDate"]);
                        list.MinimumSpend = Convert.ToDouble(reader["MinimumSpend"]);
                        list.MaximumSpend = Convert.ToDouble(reader["MaximumSpend"]);
                        list.UsageLimitPerUser = Convert.ToInt32(reader["UsageLimitPerUser"]);
                        list.IsEmail = Convert.ToBoolean(reader["IsEmail"]);
                    }
                    con.Close();
                }
            }
            return list;
        }
    }
}

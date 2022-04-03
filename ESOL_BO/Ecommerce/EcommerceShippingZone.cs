using ESOL_BO.DbAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ESOL_BO.Ecommerce
{
    public class EcommerceShippingZoneDomain : BaseDomain
    {
        public int ShippingZoneId { get; set; }
        public int DivisionId { get; set; }
        public string ShippingMethod { get; set; }
        public string Title { get; set; }
        public double Cost { get; set; }
        public string FreeShippingRequiresId { get; set; }
        public double MinimumOrderAmount { get; set; }
        public bool IsCoupon { get; set; }
    }
    public class EcommerceShippingZone
    {
        public int InsertOrUpdate(EcommerceShippingZoneDomain entity)
        {
            int save = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_EcommerceShippingZoneAll", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ShippingZoneId", SqlDbType.Int).Value = entity.ShippingZoneId;
                    cmd.Parameters.Add("@DivisionId", SqlDbType.Int).Value = entity.DivisionId;
                    cmd.Parameters.Add("@ShippingMethod", SqlDbType.NVarChar).Value = entity.ShippingMethod;
                    cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = entity.Title;
                    cmd.Parameters.Add("@Cost", SqlDbType.Decimal).Value = entity.Cost;
                    cmd.Parameters.Add("@FreeShippingRequiresId", SqlDbType.NVarChar).Value = entity.FreeShippingRequiresId;
                    cmd.Parameters.Add("@MinimumOrderAmount", SqlDbType.Decimal).Value = entity.MinimumOrderAmount;
                    cmd.Parameters.Add("@IsCoupon", SqlDbType.NVarChar).Value = entity.IsCoupon;
                    cmd.Parameters.Add("@IsActive", SqlDbType.NVarChar).Value = entity.IsActive;
                    cmd.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = entity.StatementType;
                    save = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return save;
        }
        public List<EcommerceShippingZoneDomain> GetByAll()
        {
            List<EcommerceShippingZoneDomain> lists = new List<EcommerceShippingZoneDomain>();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("select sz.*,d.DivisionName,CASE WHEN sz.ShippingMethod='FR' THEN 'Flat Rate' WHEN sz.ShippingMethod='FS' THEN 'Free Shipping' WHEN sz.ShippingMethod='LP' THEN 'Local Pickup'  ELSE '-' END as MethodStatus,CASE WHEN sz.FreeShippingRequiresId='OA' THEN 'A minimum order amount' WHEN sz.FreeShippingRequiresId='SC' THEN 'A minimum order amount AND a coupon'  ELSE 'N/A' END as ShippingRequires from EcommerceShippingZone sz left join Division d on d.DivisionId=sz.DivisionId order by sz.ShippingZoneId desc", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        EcommerceShippingZoneDomain list = new EcommerceShippingZoneDomain();
                        list.ShippingZoneId = Convert.ToInt32(reader["ShippingZoneId"]);
                        list.DivisionId = Convert.ToInt32(reader["DivisionId"]);
                        list.ShippingMethod = reader["ShippingMethod"].ToString();
                        list.Title = reader["Title"].ToString();
                        list.Cost = Convert.ToDouble(reader["Cost"]);
                        list.FreeShippingRequiresId =reader["FreeShippingRequiresId"].ToString();
                        list.MinimumOrderAmount = Convert.ToDouble(reader["MinimumOrderAmount"]);
                        list.IsCoupon = Convert.ToBoolean(reader["IsCoupon"]);
                        list.IsActive = Convert.ToBoolean(reader["IsActive"]);
                        list.Day = reader["DivisionName"].ToString();
                        list.Status = reader["MethodStatus"].ToString();
                        list.StatementType = reader["ShippingRequires"].ToString();
                        lists.Add(list);
                    }
                    con.Close();
                }
            }
            return lists;
        }
        public EcommerceShippingZoneDomain GetById(int id)
        {
            EcommerceShippingZoneDomain list = new EcommerceShippingZoneDomain();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("select * from EcommerceShippingZone where ShippingZoneId='" + id + "'", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list = new EcommerceShippingZoneDomain();
                        list.ShippingZoneId = Convert.ToInt32(reader["ShippingZoneId"]);
                        list.DivisionId = Convert.ToInt32(reader["DivisionId"]);
                        list.ShippingMethod = reader["ShippingMethod"].ToString();
                        list.Title = reader["Title"].ToString();
                        list.Cost = Convert.ToDouble(reader["Cost"]);
                        list.FreeShippingRequiresId = reader["FreeShippingRequiresId"].ToString();
                        list.MinimumOrderAmount = Convert.ToDouble(reader["MinimumOrderAmount"]);
                        list.IsCoupon = Convert.ToBoolean(reader["IsCoupon"]);
                        list.IsActive = Convert.ToBoolean(reader["IsActive"]);
                    }
                    con.Close();
                }
            }
            return list;
        }
    }
}

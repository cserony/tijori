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
    public class EcommerceHomePageSliderDomain : BaseDomain
    {
        public int SliderId { get; set; }
        public string SliderName { get; set; }
        public string SliderTag { get; set; }
        public string Description { get; set; }
        public int OrderBy { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public double Amount { get; set; }
    }
    public class EcommerceHomePageSlider
    {
        public int InsertOrUpdate(EcommerceHomePageSliderDomain entity)
        {
            int save = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_EcommerceHomePageSliderAll", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OrgId", SqlDbType.Int).Value = entity.OrgId;
                    cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = entity.BranchId;
                    cmd.Parameters.Add("@SliderId", SqlDbType.Int).Value = entity.SliderId;
                    cmd.Parameters.Add("@SliderName", SqlDbType.NVarChar).Value = entity.SliderName;
                    cmd.Parameters.Add("@SliderTag", SqlDbType.NVarChar).Value = entity.SliderTag;
                    cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = entity.Description;
                    cmd.Parameters.Add("@OrderBy", SqlDbType.Int).Value = entity.OrderBy;
                    cmd.Parameters.Add("@ImageName", SqlDbType.NVarChar).Value = entity.ImageName;
                    cmd.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = entity.FromDate;
                    cmd.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = entity.ToDate;
                    cmd.Parameters.Add("@MerchantId", SqlDbType.Int).Value = entity.MerchantId;
                    cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = entity.Amount;
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
        public List<EcommerceHomePageSliderDomain> GetByOrg(string where)
        {
            List<EcommerceHomePageSliderDomain> lists = new List<EcommerceHomePageSliderDomain>();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(where, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        EcommerceHomePageSliderDomain list = new EcommerceHomePageSliderDomain();
                        list.SliderId = Convert.ToInt32(reader["SliderId"]);
                        list.SliderName = reader["SliderName"].ToString();
                        list.SliderTag = reader["SliderTag"].ToString();
                        list.Description = reader["Description"].ToString();
                        list.OrderBy = Convert.ToInt32(reader["OrderBy"]);
                        list.ImageName = reader["ImageName"].ToString();
                        list.FromDate = Convert.ToDateTime(reader["FromDate"]);
                        list.ToDate = Convert.ToDateTime(reader["ToDate"]);
                        list.MerchantId = Convert.ToInt32(reader["MerchantId"]);
                        list.Amount = Convert.ToDouble(reader["Amount"]);
                        list.IsActive = Convert.ToBoolean(reader["IsActive"]);
                        lists.Add(list);
                    }
                    con.Close();
                }
            }
            return lists;
        }
        public EcommerceHomePageSliderDomain GetById(string where)
        {
            EcommerceHomePageSliderDomain list = new EcommerceHomePageSliderDomain();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(where, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list = new EcommerceHomePageSliderDomain();
                        list.SliderId = Convert.ToInt32(reader["SliderId"]);
                        list.SliderName = reader["SliderName"].ToString();
                        list.SliderTag = reader["SliderTag"].ToString();
                        list.Description = reader["Description"].ToString();
                        list.OrderBy = Convert.ToInt32(reader["OrderBy"]);
                        list.ImageName = reader["ImageName"].ToString();
                        list.FromDate = Convert.ToDateTime(reader["FromDate"]);
                        list.ToDate = Convert.ToDateTime(reader["ToDate"]);
                        list.MerchantId = Convert.ToInt32(reader["MerchantId"]);
                        list.Amount = Convert.ToDouble(reader["Amount"]);
                    }
                    con.Close();
                }
            }
            return list;
        }
    }
}

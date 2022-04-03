using ESOL_BO.DbAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ESOL_BO.Ecommerce
{
    public class EcommerceCustomerReviewDomain : BaseDomain
    {
        public int ReviewId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public string Review { get; set; }
        public int Rate { get; set; }
        public bool IsPublished { get; set; }
    }
    public class EcommerceCustomerReview
    {
        public int InsertOrUpdate(EcommerceCustomerReviewDomain entity)
        {
            int save = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_EcommerceCustomerReviewAll", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ReviewId", SqlDbType.Int).Value = entity.ReviewId;
                    cmd.Parameters.Add("@CustomerId", SqlDbType.Int).Value = entity.CustomerId;
                    cmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = entity.ProductId;
                    cmd.Parameters.Add("@Review", SqlDbType.NVarChar).Value = entity.Review;
                    cmd.Parameters.Add("@Rate", SqlDbType.Int).Value = entity.Rate;
                    cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = entity.CreatedDate;
                    cmd.Parameters.Add("@IsPublished", SqlDbType.NVarChar).Value = entity.IsPublished;
                    cmd.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = entity.StatementType;
                    save = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return save;
        }
        public List<EcommerceCustomerReviewDomain> GetByAll()
        {
            List<EcommerceCustomerReviewDomain> lists = new List<EcommerceCustomerReviewDomain>();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("select * from EcommerceCustomerReview order by ReviewId desc", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        EcommerceCustomerReviewDomain list = new EcommerceCustomerReviewDomain();
                        list.ReviewId = Convert.ToInt32(reader["ReviewId"]);
                        list.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                        list.ProductId = Convert.ToInt32(reader["ProductId"]);
                        list.Review = reader["Review"].ToString();
                        list.Rate = Convert.ToInt32(reader["Rate"]);
                        list.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                        list.IsPublished = Convert.ToBoolean(reader["IsPublished"]);
                        lists.Add(list);
                    }
                    con.Close();
                }
            }
            return lists;
        }
        public EcommerceCustomerReviewDomain GetById(int id)
        {
            EcommerceCustomerReviewDomain list = new EcommerceCustomerReviewDomain();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("select * from EcommerceCustomerReview where ReviewId='" + id + "'", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list = new EcommerceCustomerReviewDomain();
                        list.ReviewId = Convert.ToInt32(reader["ReviewId"]);
                        list.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                        list.ProductId = Convert.ToInt32(reader["ProductId"]);
                        list.Review = reader["Review"].ToString();
                        list.Rate = Convert.ToInt32(reader["Rate"]);
                        list.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                        list.IsPublished = Convert.ToBoolean(reader["IsPublished"]);
                    }
                    con.Close();
                }
            }
            return list;
        }
    }
}

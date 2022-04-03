using ESOL_BO.DbAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ESOL_BO.Ecommerce
{
    public class EcommerceSubscriberDomain : BaseDomain
    {
        public int SubscriberId { get; set; }
    }
    public class EcommerceSubscriber
    {
        public int InsertOrUpdate(EcommerceSubscriberDomain entity)
        {
            int save = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_EcommerceSubscriberAll", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@SubscriberId", SqlDbType.Int).Value = entity.SubscriberId;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = entity.Email;
                    cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = entity.CreatedDate;
                    cmd.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = entity.StatementType;
                    save = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return save;
        }
        public List<EcommerceSubscriberDomain> GetByAll()
        {
            List<EcommerceSubscriberDomain> lists = new List<EcommerceSubscriberDomain>();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("select * from EcommerceSubscriber order by SubscriberId desc", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        EcommerceSubscriberDomain list = new EcommerceSubscriberDomain();
                        list.SubscriberId = Convert.ToInt32(reader["SubscriberId"]);
                        list.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                        list.Date = list.CreatedDate.ToString("dd-MMM-yyyy");
                        list.Email = reader["Email"].ToString();
                        lists.Add(list);
                    }
                    con.Close();
                }
            }
            return lists;
        }
        public EcommerceSubscriberDomain GetById(int id)
        {
            EcommerceSubscriberDomain list = new EcommerceSubscriberDomain();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("select * from EcommerceSubscriber where SubscriberId='" + id + "'", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list = new EcommerceSubscriberDomain();
                        list.SubscriberId = Convert.ToInt32(reader["SubscriberId"]);
                        list.Email = reader["Email"].ToString();
                    }
                    con.Close();
                }
            }
            return list;
        }
    }
}

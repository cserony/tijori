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
    public class EcommerceCardTypeDomain : BaseDomain
    {
        public int CardTypeId { get; set; }
        public string CardTypeName { get; set; }
    }
    public class EcommerceCardType
    {
        public int InsertOrUpdate(EcommerceCardTypeDomain entity)
        {
            int save = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_EcommerceCardTypeAll", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OrgId", SqlDbType.Int).Value = entity.OrgId;
                    cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = entity.BranchId;
                    cmd.Parameters.Add("@CardTypeId", SqlDbType.Int).Value = entity.CardTypeId;
                    cmd.Parameters.Add("@CardTypeName", SqlDbType.NVarChar).Value = entity.CardTypeName;
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
        public List<EcommerceCardTypeDomain> GetByOrg(string where)
        {
            List<EcommerceCardTypeDomain> lists = new List<EcommerceCardTypeDomain>();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(where, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        EcommerceCardTypeDomain list = new EcommerceCardTypeDomain();
                        list.CardTypeId = Convert.ToInt32(reader["CardTypeId"]);
                        list.CardTypeName = reader["CardTypeName"].ToString();
                        lists.Add(list);
                    }
                    con.Close();
                }
            }
            return lists;
        }
        public EcommerceCardTypeDomain GetById(string where)
        {
            EcommerceCardTypeDomain list = new EcommerceCardTypeDomain();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(where, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list = new EcommerceCardTypeDomain();
                        list.CardTypeId = Convert.ToInt32(reader["CardTypeId"]);
                        list.CardTypeName = reader["CardTypeName"].ToString();
                    }
                    con.Close();
                }
            }
            return list;
        }
    }
}

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
    public class EcommerceCurrencyDomain : BaseDomain
    {
        public int CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencySymbol { get; set; }
    }
    public class EcommerceCurrency
    {
        public int InsertOrUpdate(EcommerceCurrencyDomain entity)
        {
            int save = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_EcommerceCurrencyAll", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OrgId", SqlDbType.Int).Value = entity.OrgId;
                    cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = entity.BranchId;
                    cmd.Parameters.Add("@CurrencyId", SqlDbType.Int).Value = entity.CurrencyId;
                    cmd.Parameters.Add("@CurrencyName", SqlDbType.NVarChar).Value = entity.CurrencyName;
                    cmd.Parameters.Add("@CurrencySymbol", SqlDbType.NVarChar).Value = entity.CurrencySymbol;
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
        public List<EcommerceCurrencyDomain> GetByOrg(string where)
        {
            List<EcommerceCurrencyDomain> lists = new List<EcommerceCurrencyDomain>();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(where, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        EcommerceCurrencyDomain list = new EcommerceCurrencyDomain();
                        list.CurrencyId = Convert.ToInt32(reader["CurrencyId"]);
                        list.CurrencyName = reader["CurrencyName"].ToString();
                        list.CurrencySymbol = reader["CurrencySymbol"].ToString();
                        lists.Add(list);
                    }
                    con.Close();
                }
            }
            return lists;
        }
        public EcommerceCurrencyDomain GetById(string where)
        {
            EcommerceCurrencyDomain list = new EcommerceCurrencyDomain();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(where, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list = new EcommerceCurrencyDomain();
                        list.CurrencyId = Convert.ToInt32(reader["CurrencyId"]);
                        list.CurrencyName = reader["CurrencyName"].ToString();
                        list.CurrencySymbol = reader["CurrencySymbol"].ToString();
                    }
                    con.Close();
                }
            }
            return list;
        }
    }
}

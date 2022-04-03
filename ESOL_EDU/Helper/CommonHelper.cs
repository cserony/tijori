using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Text;
using ESOL_BO.DbAccess;


namespace ESOL_EDU.Helper
{
    public class GetData
    {
        public int Code { get; set; }
        public string Name { get; set; }
    }
    public class CommonHelper
    {
        public static int Delete(string tblName, string where)
        {
            int delete = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                string query = "DELETE FROM " + tblName + " " + where + " ";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    delete = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return delete;
        }
        public static string Count(string count, string tblName, string where)
        {
            string total = "";
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                string query = "Select COUNT(" + count + ") as count FROM " + tblName + " " + where + " ";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        total = reader["count"].ToString();
                    }
                    con.Close();
                }
            }
            return total;
        }
        public static int CheckName(string tblName, string where)
        {
            int i = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {

                string query = "SELECT * FROM " + tblName + " " + where + " ";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            return i = 1;
                        }
                        else
                        {
                            return i;
                        }
                    }
                }
            }
        }
        public List<GetData> GetList(string vCol, string nCol, string tblName, string where, string orderby)
        {
            List<GetData> lst = new List<GetData>();
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                string sql = "SELECT " + vCol + "," + nCol + " FROM " + tblName + " " + where + " " + orderby;
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            GetData gd = new GetData();
                            gd.Code = Convert.ToInt32(reader[vCol]);
                            gd.Name = reader[nCol].ToString();
                            lst.Add(gd);
                        }
                    }
                    con.Close();
                }
            }
            return lst;
        }
        public string ReplaceStringForSlag(string inpute)

        {
            string Returnstring = "";
            try
            {
                Returnstring = inpute.ToLower().Trim().Replace(" ", "").Replace("_", "").Replace("-", "").Replace("+", "").Replace("/", "").Replace(".", "").Replace("(", "").Replace(")", "");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Returnstring;
        }
        public string Password()
        {
            try
            {
                string[] charPool = "1-2-3-4-5-6".Split('-');
                StringBuilder rs = new StringBuilder();
                int length = 6;
                Random rnd = new Random();
                while (length-- > 0)
                {
                    int index = (int)(rnd.NextDouble() * charPool.Length);
                    if (charPool[index] != "-")
                    {
                        rs.Append(charPool[index]);
                        charPool[index] = "-";
                    }
                    else
                        length++;
                }
                return rs.ToString();
            }
            catch (Exception ex)
            {
                //ErrorLog.WriteErrorLog("Barcode", ex.ToString(), ex.Message);
            }
            return "";
        }
    }
}
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
    public class EcommerceProductStockInfoDomain : BaseDomain
    {
        public int StockId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public double Quantity { get; set; }
        public double MRP { get; set; }
        public int SalesId { get; set; }
        public int ReturnId { get; set; }
        public DateTime TransectionDate { get; set; }
        public int TransectionType { get; set; }
    }
    public class EcommerceProductStockInfo
    {
        public int InsertOrUpdate(EcommerceProductStockInfoDomain entity)
        {
            int save = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_EcommerceProductStockInfoAll", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OrgId", SqlDbType.Int).Value = entity.OrgId;
                    cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = entity.BranchId;
                    cmd.Parameters.Add("@StockId", SqlDbType.Int).Value = entity.StockId;
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = entity.UserId;
                    cmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = entity.ProductId;
                    cmd.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = entity.Quantity;
                    cmd.Parameters.Add("@MRP", SqlDbType.Decimal).Value = entity.MRP;
                    cmd.Parameters.Add("@SalesId", SqlDbType.Int).Value = entity.SalesId;
                    cmd.Parameters.Add("@ReturnId", SqlDbType.Int).Value = entity.ReturnId;
                    cmd.Parameters.Add("@TransectionDate", SqlDbType.DateTime).Value = entity.TransectionDate;
                    cmd.Parameters.Add("@TransectionType", SqlDbType.Int).Value = entity.TransectionType;
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
    }
}

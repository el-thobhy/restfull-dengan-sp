using ApiPointOfSales.DataModel;
using ApiPointOfSales.Services;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ApiPointOfSales.Repository
{
    public class SalesOrderRepository : RepositoryInterface<SalesOrder>
    {
        private readonly string _connectionString; 
        public SalesOrderRepository(string connectionString)
        {
            _connectionString = connectionString;
        }


        public async Task Create(SqlConnection connection, SalesOrder model, SqlTransaction trans)
        {
            SqlCommand cmd = new SqlCommand("CreateSalesOrder", connection, trans);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@OrderDate", model.OrderDate);
            cmd.Parameters.AddWithValue("@SalesOrderNo", model.SalesOrderNo);
            cmd.Parameters.AddWithValue("@CustCode", model.CustCode);
            await cmd.ExecuteNonQueryAsync();
                
        }

        public bool Delete(string salesOrderNo)
        {
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DeleteSalesOrder", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SalesOrderNo", salesOrderNo);
                SalesOrder result = ReadById(salesOrderNo);
                cmd.ExecuteNonQuery();
                return result != null ? true : false;
            }
        }

        public SalesOrder ReadById(string salesOrderNo)
        {
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("ReadSalesOrder", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SalesOrderNo", salesOrderNo);
                SqlDataReader read = cmd.ExecuteReader();
                if (read.Read())
                {
                    SalesOrder order = new SalesOrder();
                    order.OrderDate = Convert.ToDateTime(read["OrderDate"]);
                    order.SalesOrderNo = read["SalesOrderNo"].ToString() ?? "";
                    order.CustCode = read["CustCode"].ToString() ?? "";
                    return order;
                }
                return new SalesOrder();
            }
        }

        public void Update(SalesOrder model)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("UpdateSalesOrder", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@OrderDate", model.OrderDate);
                cmd.Parameters.AddWithValue("@SalesOrderNo", model.SalesOrderNo);
                cmd.Parameters.AddWithValue("@CustCode", model.CustCode);

                cmd.ExecuteNonQuery();
            }
        }
    }
}

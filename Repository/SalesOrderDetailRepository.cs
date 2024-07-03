using ApiPointOfSales.DataModel;
using ApiPointOfSales.Services;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ApiPointOfSales.Repository
{
    public class SalesOrderDetailRepository
    {
        private readonly string _connectionString;


        public SalesOrderDetailRepository(string connectionString)
        {
            _connectionString = connectionString;
        }


        public async Task Create(SqlConnection connection,SalesOrderDetail model, SqlTransaction trans)
        {
            SqlCommand cmd = new SqlCommand("CreateSalesOrderDetail", connection, trans);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SalesOrderNo", model.SalesOrderNo);
            cmd.Parameters.AddWithValue("@ProductCode", model.ProductCode);
            cmd.Parameters.AddWithValue("@Qty", model.Qty);
            cmd.Parameters.AddWithValue("@Price", model.Price);

            await cmd.ExecuteNonQueryAsync();
        }

        public void Delete(string salesOrderNo, string productCode)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("DeleteSalesOrderDetail", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SalesOrderNo", salesOrderNo);
                cmd.Parameters.AddWithValue("@ProductCode", productCode);

                cmd.ExecuteNonQuery();
            }
        }

        public SalesOrderDetail ReadById(string salesOrderNo, string productCode)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("ReadSalesOrderDetail", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SalesOrderNo", salesOrderNo);
                cmd.Parameters.AddWithValue("@ProductCode", productCode);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    SalesOrderDetail salesOrderDetail = new SalesOrderDetail();
                    salesOrderDetail.SalesOrderNo = reader["SalesOrderNo"].ToString() ?? "";
                    salesOrderDetail.ProductCode = reader["ProductCode"].ToString() ?? "";
                    salesOrderDetail.Qty = Convert.ToInt32(reader["Qty"]);
                    salesOrderDetail.Price = Convert.ToDecimal(reader["Price"]);

                    return salesOrderDetail;
                }

                return new SalesOrderDetail();
            }
        }

        public void Update(SalesOrderDetail model)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("UpdateSalesOrderDetail", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SalesOrderNo", model.SalesOrderNo);
                cmd.Parameters.AddWithValue("@ProductCode", model.ProductCode);
                cmd.Parameters.AddWithValue("@Qty", model.Qty);
                cmd.Parameters.AddWithValue("@Price", model.Price);

                cmd.ExecuteNonQuery();
            }
        }
    }
}

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

        public bool Delete(string salesOrderNo)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("DeleteSalesOrderDetail", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SalesOrderNo", salesOrderNo);
                List<SalesOrderDetail> check = ReadById(salesOrderNo);
                cmd.ExecuteNonQuery();
                return check.Count != 0 ? true : false ;
            }
        }

        public List<SalesOrderDetail> ReadById(string salesOrderNo)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("ReadSalesOrderDetail", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SalesOrderNo", salesOrderNo);

                SqlDataReader reader = cmd.ExecuteReader();
                List<SalesOrderDetail> result = new List<SalesOrderDetail>();

                while (reader.Read())
                {
                    SalesOrderDetail salesOrderDetail = new SalesOrderDetail
                    {
                        SalesOrderNo = reader["SalesOrderNo"].ToString() ?? "",
                        ProductCode = reader["ProductCode"].ToString() ?? "",
                        Qty = Convert.ToInt32(reader["Qty"]),
                        Price = Convert.ToDecimal(reader["Price"])
                    };

                    result.Add(salesOrderDetail);
                }
                return result;
            }
        }

        public async Task Update(SqlConnection connection, SalesOrderDetail model, SqlTransaction trans)
        {
            SqlCommand cmd = new SqlCommand("UpdateSalesOrderDetail", connection, trans);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SalesOrderNo", model.SalesOrderNo);
            cmd.Parameters.AddWithValue("@ProductCode", model.ProductCode);
            cmd.Parameters.AddWithValue("@Qty", model.Qty);
            cmd.Parameters.AddWithValue("@Price", model.Price);

            await cmd.ExecuteNonQueryAsync();
        }
    }
}

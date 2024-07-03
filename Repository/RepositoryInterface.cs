using Microsoft.Data.SqlClient;

namespace ApiPointOfSales.Repository
{
    public interface RepositoryInterface<T>
    {
        Task Create(SqlConnection connection, T model, SqlTransaction trans);
        T ReadById(string id);
        Task Update(SqlConnection connection, T model, SqlTransaction trans);
        bool Delete(string id);
    }
}

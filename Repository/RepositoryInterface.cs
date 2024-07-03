using Microsoft.Data.SqlClient;

namespace ApiPointOfSales.Repository
{
    public interface RepositoryInterface<T>
    {
        Task Create(SqlConnection connection, T model, SqlTransaction trans);
        T ReadById(string id);
        void Update(T model);
        void Delete(string id);
    }
}

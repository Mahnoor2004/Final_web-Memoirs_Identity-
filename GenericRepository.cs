using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using Dapper;
using WebApplication1.Models;

namespace WebApplication1.Models
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly string connectionString;

        public GenericRepository(string conn)
        {
            connectionString = conn;
        }
        public void Add(string userEmail, TEntity entity)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var tableName = typeof(TEntity).Name;
                var properties = typeof(TEntity).GetProperties().Where(p => p.Name != "Id");

                // Include the email column
                var columnsNames = "UserEmail," + string.Join(",", properties.Select(p => p.Name));
                var parameterNames = "@UserEmail," + string.Join(",", properties.Select(p => "@" + p.Name));

                var query = $"INSERT INTO {tableName} ({columnsNames}) VALUES ({parameterNames})";

                var parameters = new DynamicParameters();
                // Add the email parameter
                parameters.Add("@UserEmail", userEmail);
                foreach (var property in properties)
                {
                    parameters.Add("@" + property.Name, property.GetValue(entity));
                }

                connection.Execute(query, parameters);
            }
        }


        //public void Add(TEntity entity)
        //{
        //    using (var connection = new SqlConnection(connectionString))
        //    {
        //        var tableName = typeof(TEntity).Name;
        //        var properties = typeof(TEntity).GetProperties().Where(p => p.Name != "Id");

        //        var columnsNames = string.Join(",", properties.Select(p => p.Name));
        //        var parameterNames = string.Join(",", properties.Select(p => "@" + p.Name));

        //        var query = $"INSERT INTO {tableName} ({columnsNames}) VALUES ({parameterNames})";

        //        var parameters = new DynamicParameters();
        //        foreach (var property in properties)
        //        {
        //            parameters.Add("@" + property.Name, property.GetValue(entity));
        //        }

        //        connection.Execute(query, parameters);
        //    }
        //}

        //public TEntity Get(int id)
        //{
        //    using (var connection = new SqlConnection(connectionString))
        //    {
        //        var tableName = typeof(TEntity).Name;
        //        var query = $"SELECT * FROM {tableName} WHERE Id = @Id";
        //        return connection.QuerySingleOrDefault<TEntity>(query, new { Id = id });
        //    }
        //}


        public IEnumerable<TEntity> GetAll(string userEmail)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var tableName = typeof(TEntity).Name;
                var query = $"SELECT * FROM {tableName} WHERE UserEmail = @UserEmail";
                return connection.Query<TEntity>(query, new { UserEmail = userEmail }).ToList();
            }
        }

        public void Update(TEntity entity, string userEmail)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var tableName = typeof(TEntity).Name;
                var properties = typeof(TEntity).GetProperties().Where(p => p.Name != "Id");

                var setClause = string.Join(",", properties.Select(p => $"{p.Name} = @{p.Name}"));
                var query = $"UPDATE {tableName} SET {setClause} WHERE Id = @Id";

                var parameters = new DynamicParameters(entity);
                parameters.Add("Id", entity.GetType().GetProperty("Id").GetValue(entity));

                connection.Execute(query, parameters);
            }
        }
        public TEntity GetById(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var tableName = typeof(TEntity).Name;
                var query = $"SELECT * FROM {tableName} WHERE Id = @Id";
                return connection.QueryFirstOrDefault<TEntity>(query, new { Id = id });
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var tableName = typeof(TEntity).Name;
                var query = $"DELETE FROM {tableName} WHERE Id = @Id";
                connection.Execute(query, new { Id = id });
            }
        }
    }
}
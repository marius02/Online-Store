using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OnlineStore.DataAccess.Data.IRepository;

namespace OnlineStore.DataAccess.Data.Repository
{
    public class SP_Call : ISP_Call
    {
        private readonly ApplicationDbContext _db;
        private static string _connectionString = "";

        public SP_Call(ApplicationDbContext db)
        {
            _db = db;
            _connectionString = _db.Database.GetDbConnection().ConnectionString;
        }

        public void Dispose()
        {
           _db.Dispose();
        }

        public T Single<T>(string procedureName, DynamicParameters param = null)
        {
            using var sqlCon = new SqlConnection(_connectionString);
            sqlCon.Open();
            return (T)Convert.ChangeType(sqlCon.ExecuteScalar(procedureName, param, commandType:CommandType.StoredProcedure), typeof(T));
        }

        public void Execute(string procedureName, DynamicParameters param = null)
        {
            using var sqlCon = new SqlConnection(_connectionString);
            sqlCon.Open();
            sqlCon.Execute(procedureName, param, commandType: CommandType.StoredProcedure);
        }

        public T OneRecord<T>(string procedureName, DynamicParameters param = null)
        {
            using var sqlCon = new SqlConnection(_connectionString);
            sqlCon.Open();
            var value = sqlCon.Query(procedureName, param, commandType: CommandType.StoredProcedure);

            return (T)Convert.ChangeType(value.FirstOrDefault(), typeof(T));
        }

        public IEnumerable<T> List<T>(string procedureName, DynamicParameters param = null)
        {
            using var sqlCon = new SqlConnection(_connectionString);
            sqlCon.Open();
            return sqlCon.Query<T>(procedureName, param, commandType: CommandType.StoredProcedure);
        }

        public Tuple<IEnumerable<T1>, IEnumerable<T2>> List<T1, T2>(string procedureName, DynamicParameters param = null)
        {
            using (var sqlCon = new SqlConnection(_connectionString))
            {
                sqlCon.Open();
                var result =
                    SqlMapper.QueryMultiple(sqlCon, procedureName,param, commandType: CommandType.StoredProcedure);
                var item1 = result.Read<T1>().ToList();
                var item2 = result.Read<T2>().ToList();

                if (item1 != null && item2 != null)
                {
                    return new Tuple<IEnumerable<T1>, IEnumerable<T2>>(item1, item2);
                }
            }

            return new Tuple<IEnumerable<T1>, IEnumerable<T2>>(new List<T1>(), new List<T2>());
        }
    }
}

using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Npgsql;

namespace TaxiparkWeb.Models
{
    public static class TaxiDbContext
    {
        private static string connectionString = "User ID=postgres; password=5689; Host=localhost; Port=5432; Database=Taxipark;";


        public static IEnumerable<T> ReturnListAll<T>(string procedureName)
        {
            using (IDbConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                return con.Query<T>(procedureName);
            }
        }

        public static void ExecuteWithoutReturn(string procedureName, DynamicParameters param = null)
        {
            using (IDbConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                con.Execute(procedureName);
            } 
        }
    }
}
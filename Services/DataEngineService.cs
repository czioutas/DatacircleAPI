using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using DatacircleAPI.Application;
using DatacircleAPI.Models;
using MySql.Data.MySqlClient;

namespace DatacircleAPI.Services
{
    public class DataEngineService
    {
        private Metric _metric;
        private Datasource _datasource;

        public async Task<ArrayList> GetData(Metric metric, Datasource datasource)
        {
            this._metric = metric;
            this._datasource = datasource;
            
            
            switch (this._datasource.Type)
            {
                case DatacircleAPI.Models.Type.Mysql:
                    return await this.getMySQLData();
                default:
                    throw new ResponseException("Datasource type not recognized.");
            }
        }

        protected async Task<ArrayList> getMySQLData()
        {
            string host = this._datasource.ConnectionDetails.Host;
            string port = this._datasource.ConnectionDetails.Port.ToString();
            string user = this._datasource.ConnectionDetails.Username;
            string password = this._datasource.ConnectionDetails.Password;
            string database = this._datasource.ConnectionDetails.Database;

            ArrayList data = new ArrayList();

            string ConnectionString = "host=" + host + ";";
            ConnectionString += "port=" + port + ";";
            ConnectionString += "user id=" + user + ";";
            ConnectionString += "password=" + password + ";";
            ConnectionString += "database=" + database + ";";

            Console.WriteLine(ConnectionString);
            MySqlConnection connection = new MySqlConnection(ConnectionString);           

            await connection.OpenAsync();
            var cmd = connection.CreateCommand();
            cmd.CommandText = this._metric.Query.ToString();

            DbDataReader reader = await cmd.ExecuteReaderAsync();

            while (reader.Read())
            {
                List<string> row = new List<string>();
                for (int col = 0; col < reader.FieldCount; col++)
                {
                    row.Add(reader[col].ToString());         
                }
                data.Add(row);
            }

            connection.Close();

            return data;
        }
    }
}

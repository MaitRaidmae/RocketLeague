using System;
using System.Collections.Generic;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace RocketLeague
{
    public sealed class ConnectionManager
    {
        private readonly string oradb = "Data Source=XE;User Id=RocketLeague;Password=GA5nX9tTZv5DFlx4W7eE;";
        public OracleConnection connection;
        private static ConnectionManager instance;


        private ConnectionManager() {
            connection = new OracleConnection(oradb); // C#
            connection.Open();
        }
        
        public void Dispose()
        {
            connection.Dispose(); 
        }

        public OracleConnection GetConnection()
        {
            return connection;
        }

        public static ConnectionManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ConnectionManager();
                }

                return instance;
            }
        }
    }
}

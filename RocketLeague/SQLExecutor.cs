using System;
using System.Collections.Generic;
using System.Data;
using Oracle.ManagedDataAccess.Client;


namespace RocketLeague
{
    class SQLExecutor
    {

        public static OracleDataReader GetSQLResults(string query)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = ConnectionManager.Instance.GetConnection();
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            dr.Read();
            return dr;
        }

        public static int ExecuteSQL(string query)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = ConnectionManager.Instance.GetConnection();
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            int updateCount = cmd.ExecuteNonQuery();
            return updateCount;
        }


        public static int CallPLSQLFunction(string functionToCall, Dictionary<string,Dictionary<string,dynamic>> parameters)
        {
            OracleDbType dataType;
            OracleCommand cmd = new OracleCommand();
            cmd.Parameters.Add("row_code", OracleDbType.Int32, 32767);
            cmd.Parameters["row_code"].Direction = ParameterDirection.ReturnValue;
            foreach (KeyValuePair<string, Dictionary<string, dynamic>> parameterType in parameters)
            {
                dataType = GetDataType(parameterType.Key);
                foreach (KeyValuePair<string,dynamic> parameter in parameterType.Value)
                {
                    cmd.Parameters.Add(parameter.Key, OracleDbType.Varchar2).Value = parameter.Value;
                }                 
            }

            cmd.Connection = ConnectionManager.Instance.GetConnection();
            cmd.CommandText = functionToCall;
            cmd.CommandType = CommandType.StoredProcedure;
            int updatedRows = cmd.ExecuteNonQuery();
            string returnString = cmd.Parameters["row_code"].Value.ToString();
            return Int32.Parse(returnString);
        }

        private static OracleDbType GetDataType(string CSharpType)
        {
            switch (CSharpType)
            {
                case ("int"): return OracleDbType.Int32;
                case ("string"): return OracleDbType.Varchar2;
                case ("dateTime"): return OracleDbType.TimeStamp;
                case ("decimal"): return OracleDbType.Decimal;
                default: return OracleDbType.Varchar2;                
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;

namespace RocketLeague
{
    class RocketLeague
    {
        static void Main(string[] args)
        {
            while (true) {
                
                int lastUnixTime = Request.GetLastUnixTime();

                int currentUnixTime = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                if (currentUnixTime - lastUnixTime > 120)
                {
                    Utils.WriteToConsole("Checking For New Data");
                    string jsonString = APIInterface.GetPlayerData();
                    Request request = JsonParser.ParseJson(jsonString);
                    if (request.IsAnUpdate())
                    {
                        Utils.WriteToConsole("Adding new Data To Database");
                        request.WriteRequestToDatabase();
                    }
                }
                System.Threading.Thread.Sleep(120000);
            }
        }
    }
}

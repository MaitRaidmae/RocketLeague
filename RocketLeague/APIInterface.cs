using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace RocketLeague
{    
    static class APIInterface
    {
        private static readonly string APIKEY = "IKI5V455DJC2FT81TW594MQGDVJM1D3I";
        private static readonly string STEAM_ID = "76561197996792134";
        private static readonly string PLATFORM_ID = "1";
        private static readonly string ROOT_URL = @"https://api.rocketleaguestats.com/v1";
        

        public static string GetPlayerData()
        {
            string returnContent;
            string query = $"unique_id={STEAM_ID}&platform_id={PLATFORM_ID}";
            string url = $"{ROOT_URL}/player?{query}";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add($"Authorization:{APIKEY}");
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                returnContent = reader.ReadToEnd();
            }
            return returnContent;
        }


        public static string GetPlayerDataDEBUG()
        {
            return @"{""uniqueId"":""76561197996792134"",""displayName"":""Hundisilm"",""platform"":{ ""id"":1,""name"":""Steam""},""avatar"":""https://steamcdn-a.akamaihd.net/steamcommunity/public/images/avatars/fe/fef49e7fa7e1997310d705b2a6158ff8dc1cdfeb_full.jpg"",""profileUrl"":""https://rocketleaguestats.com/profile/Steam/76561197996792134"",""signatureUrl"":""http://signature.rocketleaguestats.com/normal/Steam/76561197996792134.png"",""stats"":{""wins"":1070,""goals"":3045,""mvps"":176,""saves"":2950,""shots"":5552,""assists"":614},""rankedSeasons"":{""3"":{""10"":{""rankPoints"":382,""matchesPlayed"":31,""tier"":5,""division"":2},""11"":{""rankPoints"":463,""matchesPlayed"":42,""tier"":5,""division"":4},""12"":{""rankPoints"":458,""matchesPlayed"":12,""tier"":6,""division"":1},""13"":{""rankPoints"":0,""matchesPlayed"":0,""tier"":0,""division"":0}},""4"":{""10"":{""rankPoints"":453,""matchesPlayed"":111,""tier"":4,""division"":3},""11"":{""rankPoints"":472,""matchesPlayed"":18,""tier"":5,""division"":1},""12"":{""rankPoints"":541,""matchesPlayed"":8,""tier"":0,""division"":0}},""5"":{""10"":{""rankPoints"":453,""matchesPlayed"":6,""tier"":0,""division"":0},""11"":{""rankPoints"":472,""matchesPlayed"":4,""tier"":0,""division"":0},""12"":{""rankPoints"":541,""matchesPlayed"":4,""tier"":0,""division"":0}},""6"":{""10"":{""rankPoints"":436,""matchesPlayed"":14,""tier"":6,""division"":1},""11"":{""rankPoints"":528,""matchesPlayed"":10,""tier"":7,""division"":2},""12"":{""rankPoints"":607,""matchesPlayed"":10,""tier"":9,""division"":1}},""7"":{""10"":{""rankPoints"":424,""matchesPlayed"":0,""tier"":0,""division"":0},""11"":{""rankPoints"":723,""matchesPlayed"":0,""tier"":0,""division"":0},""12"":{""rankPoints"":424,""matchesPlayed"":0,""tier"":0,""division"":0},""13"":{""rankPoints"":600,""matchesPlayed"":0,""tier"":0,""division"":0}},""8"":{""10"":{""rankPoints"":452,""matchesPlayed"":8,""tier"":0,""division"":0},""11"":{""rankPoints"":733,""matchesPlayed"":70,""tier"":10,""division"":2},""12"":{""rankPoints"":418,""matchesPlayed"":300,""tier"":6,""division"":2},""13"":{""rankPoints"":806,""matchesPlayed"":103,""tier"":11,""division"":1}}},""lastRequested"":1530611007,""createdAt"":1476640463,""updatedAt"":1530629224,""nextUpdateAt"":1530629404}";
        }

    }
}

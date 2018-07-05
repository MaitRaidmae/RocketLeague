using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketLeague
{
    class JsonParser
    {
        public static Request ParseJson(string jsonContent)
        {
            List<PlaylistStats> playLists = new List<PlaylistStats>();
            int updatedAt = 0;
            int nextUpdateAt = 0;
            int wins = 0;
            int goals = 0;
            int mvps = 0;
            int saves = 0;
            int shots = 0;
            int assists = 0;
            SortedList<string, object> jsonList = new SortedList<string, object>();
            jsonList = JsonConvert.DeserializeObject<SortedList<string, object>>(jsonContent);

            foreach (KeyValuePair<string,dynamic> listItem in jsonList)
            {
                switch (listItem.Key)
                {
                    case "stats":
                        wins = (int)listItem.Value["wins"];
                        goals = (int)listItem.Value["goals"];
                        mvps = (int)listItem.Value["mvps"];
                        saves = (int)listItem.Value["saves"];
                        shots = (int)listItem.Value["shots"];
                        assists = (int)listItem.Value["assists"];
                        break;
                    case "rankedSeasons":
                        playLists = ParsePlayListData(listItem.Value);
                        break;
                    case "updatedAt":
                        updatedAt = (Int32) listItem.Value;
                        break;
                    case "nextUpdateAt":
                        nextUpdateAt = (Int32) listItem.Value;
                        break;
                    default:
                        break;
                }
            }
            Request returnRequest = new Request(updatedAt,nextUpdateAt,wins,goals,mvps,saves,shots,assists,playLists);
            return returnRequest;
        }

        private static List<PlaylistStats> ParsePlayListData(JObject playListsJson)
        {
            int season;
            int rankPoints;
            int matchesPlayed;
            int tier;
            int division;
            PlaylistStats playList;
            List<PlaylistStats> playLists = new List<PlaylistStats>();
            foreach (KeyValuePair<string,JToken> seasonObject in playListsJson)
            {
                season = Int32.Parse(seasonObject.Key);
                foreach (JProperty playListObject in seasonObject.Value.Children())
                {
                    Int32 playlistID = Int32.Parse(playListObject.Name);
                    rankPoints = (int) playListObject.Value["rankPoints"];
                    matchesPlayed = (int)playListObject.Value["matchesPlayed"];
                    tier = (int)playListObject.Value["tier"];
                    division = (int)playListObject.Value["division"];
                    playList = new PlaylistStats(season, playlistID, rankPoints, matchesPlayed, tier, division);
                    playLists.Add(playList);
                }
            }

            return playLists;
        }
    }
}

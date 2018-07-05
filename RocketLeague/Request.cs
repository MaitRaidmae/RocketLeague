using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;

namespace RocketLeague
{
    class Request
    {
        private Int32 updatedAt;
        private Int32 nextUpdateAt;
        private Int32 timestamp;
        private Int32 wins;
        private Int32 goals;
        private Int32 mvps;
        private Int32 saves;
        private Int32 shots;
        private Int32 assists;
        private List<PlaylistStats> playListStats;
        private bool isThisAnUpdate;
        private string updatedPlayList;
        private List<PlaylistStats> updatedPlayLists;

        public Request(Int32 updatedAt, Int32 nextUpdateAt, Int32 wins, Int32 goals, Int32 mvps, Int32 saves, Int32 shots, Int32 assists, List<PlaylistStats> playListStats)
        {
            this.updatedAt = updatedAt;
            this.nextUpdateAt = nextUpdateAt;
            this.timestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            this.wins = wins;
            this.goals = goals;
            this.mvps = mvps;
            this.saves = saves;
            this.shots = shots;
            this.assists = assists;
            this.playListStats = playListStats;
            updatedPlayLists = CheckMatchCounts();
            if (updatedPlayLists.Count < 1)
            {
                this.isThisAnUpdate = false;
            } else if (updatedPlayLists.Count == 1)
            {
                this.isThisAnUpdate = true;
                this.updatedPlayList = updatedPlayLists[0].GetPlayListName();
            } else
            {
                this.isThisAnUpdate = true;
                this.updatedPlayList = "Multiple";
            }
        }

        public bool IsAnUpdate()
        {
            return this.isThisAnUpdate;
        }


        public List<PlaylistStats> CheckMatchCounts()
        {
            List<PlaylistStats> updatedPlayLists = new List<PlaylistStats>();
            int currentSeason;
            foreach (PlaylistStats playlist in playListStats) {
                currentSeason = PlaylistStats.GetCurrentSeason(playlist.GetPlayListId());
                if(playlist.GetSeasonNumber() >= currentSeason && playlist.CheckIfUpdated())
                {
                    updatedPlayLists.Add(playlist);
                }
            }
            return updatedPlayLists;
        }

        public static int GetLastUnixTime()
        {
            string query = $"SELECT max(req_timestamp) FROM B_REQUESTS";
            OracleDataReader dataReader = SQLExecutor.GetSQLResults(query);
            if (dataReader.GetValue(0) != DBNull.Value)
            {
                return dataReader.GetInt32(0);
            }
            else
            {
                return 0;
            }
        }

        public void WriteRequestToDatabase()
        {
            Dictionary<string,Dictionary<string, dynamic>> parameters = new Dictionary<string, Dictionary<string, dynamic>>();
            Dictionary<string, dynamic> intParameters = new Dictionary<string, dynamic>();
            Dictionary<string, dynamic> stringParameters = new Dictionary<string, dynamic>();
            intParameters.Add("par_req_updated_at", updatedAt);
            intParameters.Add("par_req_next_update_at", nextUpdateAt);
            intParameters.Add("par_req_timestamp", timestamp);
            intParameters.Add("par_req_wins", wins);
            intParameters.Add("par_req_goals", goals);
            intParameters.Add("par_req_mvps", mvps);
            intParameters.Add("par_req_saves", saves);
            intParameters.Add("par_req_shots", shots);
            intParameters.Add("par_req_assits", assists);
            parameters.Add("int", intParameters);
            stringParameters.Add("par_req_playlist_type", updatedPlayList);
            parameters.Add("string", stringParameters);
            int req_code = SQLExecutor.CallPLSQLFunction("P_REQUESTS.INSERT_ROW", parameters);
            foreach (PlaylistStats playList in updatedPlayLists)
            {
                playList.WritePlayListStatsToTable(req_code);
            }
        }
    }
}

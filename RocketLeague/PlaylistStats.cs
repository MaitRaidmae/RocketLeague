using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketLeague
{
    class PlaylistStats
    {
        private int seasonNumber;
        private int playListId;
        private String playListName;
        private int rankPoints;
        private int matchesPlayed;
        private int tier;
        private int division;

        public PlaylistStats(int seasonNumber, int playListId, int rankPoints, int matchesPlayed, int tier, int division)
        {
            this.seasonNumber = seasonNumber;
            this.playListId = playListId;
            this.playListName = GetPlayListName();
            this.rankPoints = rankPoints;
            this.matchesPlayed = matchesPlayed;
            this.tier = tier;
            this.division = division;
        }


        public string GetPlayListName()
        {
            switch (this.playListId) {
                case 10: return "Solo Duel";
                case 11: return "2v2";
                case 12: return "3v3 Solo";
                case 13: return "3v3";
                default: return "Unknown";
            }
        }

        public bool CheckIfUpdated() {
            Int32 currentLoggedMatchesPlayed = GetLatestMatchesCount(this.playListId, this.seasonNumber);
            if (currentLoggedMatchesPlayed < this.matchesPlayed)
            {
                return true;
            } else
            {
                return false;
            }            
        }

        public static int GetLatestMatchesCount(int playListId, int seasonNumber)
        {
            string query = $"SELECT max(pst_matches_played) FROM B_PLAYLIST_STATS WHERE pst_playList_Id = '{playListId}' AND pst_season_number = '{seasonNumber}'";
            OracleDataReader dataReader = SQLExecutor.GetSQLResults(query);
            Int32 currentMatchCount;
            if (dataReader.GetValue(0) != DBNull.Value)
            {
                currentMatchCount = dataReader.GetInt32(0);
            } else
            {
                currentMatchCount = 0;
            }
            return currentMatchCount;
        }

        public static int GetCurrentSeason(int playListId)
        {
            string query = $"SELECT max(pst_season_number) FROM B_PLAYLIST_STATS WHERE pst_playList_Id = '{playListId}'";
            OracleDataReader dataReader = SQLExecutor.GetSQLResults(query);
            if (dataReader.GetValue(0) != DBNull.Value)
            {
                return dataReader.GetInt32(0);
            } else
            {
                return 0;
            }
        }

        public int GetPlayListId()
        {
            return playListId;
        }

        public int GetSeasonNumber()
        {
            return seasonNumber;
        }


        public void WritePlayListStatsToTable(int requestCode)
        {
            string executeStmt = $"INSERT INTO b_playlist_stats (PST_REQ_CODE,PST_SEASON_NUMBER,PST_PLAYLIST_ID," +
                $"PST_PLAYLIST_NAME,PST_MATCHES_PLAYED,PST_RANK_POINTS,PST_TIER,PST_DIVISION) VALUES" +
                $" (" +
                $"{requestCode}," +
                $"{seasonNumber}," +
                $"{playListId}," +
                $"'{playListName}'," +
                $"{matchesPlayed}," +
                $"{rankPoints}," +                
                $"{tier}," +
                $"{division})";

            int execute = SQLExecutor.ExecuteSQL(executeStmt);
        }
    }
}

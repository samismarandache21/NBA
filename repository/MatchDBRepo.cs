using Microsoft.Data.SqlClient;
using NBA.domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBA.repository
{
    internal class MatchDBRepo:AbstractDBRepo<long, Match>
    {
        public MatchDBRepo(string conn, string user, string password):base(conn, user, password) { }

        protected override void updateLastId()
        {
            base.updateLastId();
        }

        protected override void read()
        {
            using(var conn = new SqlConnection(this.conn))
            {
                conn.Open();
                string query = @"SELECT M.id, M.team1_id, M.team2_id, M.match_date, T.name
                                FROM MATCHES M
                                INNER JOIN TEAMS T ON T.id = M.team1_id or T.id = M.team2_id
                                ORDER BY M.id";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    Team team1 = new Team(reader.GetString(4));
                    team1.Id = reader.GetInt64(1);

                    reader.Read();
                    Team team2 = new Team(reader.GetString(4));
                    team2.Id = reader.GetInt64(2);

                    Match match = new Match(team1, team2, reader.GetDateTime(3));
                    match.Id = reader.GetInt64(0);

                    base.Add(match);
                }

            }
            foreach (var x in FindAll())
                Console.WriteLine(x);
        }

        public IEnumerable<Match> MatchesInPeriod(DateTime start, DateTime end)
        {
            return FindAll().Where(m => m.Date.CompareTo(start) > 0 && m.Date.CompareTo(end) < 0);
        }

        private int GetTeamPoints(long matchId, long teamId)
        {
            using(var conn = new SqlConnection(this.conn))
            {
                conn.Open();
                string query = @"SELECT SUM(points)
                                FROM PLAYER_ROLES PR
                                INNER JOIN MATCHES M ON PR.match_id = M.id
                                INNER JOIN PLAYERS P ON P.id = PR.player_id
                                WHERE M.id = @MatchId AND P.team_id = @TeamId";

                SqlCommand cmd = new SqlCommand(query,conn);
                cmd.Parameters.AddWithValue("@TeamId", teamId);
                cmd.Parameters.AddWithValue("@MatchId", matchId);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                return reader.GetInt32(0);
            }
        }

        public IList<int> GetMatchScore(Match match)
        {
            using (var conn = new SqlConnection(this.conn))
            {
                IList<int> score = new List<int>();
                score.Add(GetTeamPoints(match.Id, match.Team1.Id));
                score.Add(GetTeamPoints(match.Id, match.Team2.Id));
                return score;
            }
        }

    }
}

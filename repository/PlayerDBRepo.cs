using Microsoft.Data.SqlClient;
using NBA.domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBA.repository
{
    internal class PlayerDBRepo: AbstractDBRepo<long, Player>
    {
        public PlayerDBRepo(string conn, string user, string pass):base(conn,user,pass) { }

        protected override void updateLastId()
        {
            base.updateLastId();
        }

        protected override void read()
        {
            using(var conn = new SqlConnection(this.conn))
            {
                conn.Open();
                string query = @"SELECT P.id, P.team_id, T.name, S.name, S.school
                                FROM PLAYERS P
                                INNER JOIN STUDENTS S ON S.id = P.id
                                INNER JOIN TEAMS T on P.team_id = T.id";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Team team = new Team(reader.GetString(2));
                    team.Id = reader.GetInt64(1);

                    Player player = new Player(reader.GetString(3), reader.GetString(4), team);
                    player.Id = reader.GetInt64(0);

                    base.Add(player);
                }
            }
        }

        public IList<Player> GetPlayers(string t_name)
        {
            using (var conn = new SqlConnection(this.conn))
            {

                IList<Player> list = new List<Player>();
                conn.Open();
                string query = @"SELECT P.id, P.team_id, T.name, S.name, S.school
                                FROM PLAYERS P
                                INNER JOIN TEAMS T ON T.id = P.team_id
                                INNER JOIN STUDENTS S ON P.id = S.id
                                WHERE T.name = @TeamName
                                ";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TeamName", t_name);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Team team = new Team(reader.GetString(2));
                    team.Id = reader.GetInt64(1);

                    Player player = new Player(reader.GetString(3), reader.GetString (4), team);
                    player.Id = reader.GetInt64(0);

                    list.Add(player);
                }
                return list;
            }
        }

        public IList<Player> GetActive(string t_name, long match_id)
        {

            using (var conn = new SqlConnection(this.conn)) {
                IList<Player> list = new List<Player>();
                conn.Open();
                string query = @"SELECT P.id, P.team_id, T.name, S.name, S.school
                                FROM PLAYER_ROLES PR
                                INNER JOIN PLAYERS P ON P.id = PR.player_id
                                INNER JOIN TEAMS T ON T.id = P.team_id
                                INNER JOIN STUDENTS S ON S.id = P.id
                                WHERE PR.match_id = @MatchId AND PR.role = 'Titular' AND T.name = @TeamName
                                ";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("TeamName", t_name);
                cmd.Parameters.AddWithValue("MatchId", match_id);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Team team = new Team(reader.GetString(2));
                    team.Id = reader.GetInt64(1);

                    Player player = new Player(reader.GetString(3), reader.GetString(4), team);
                    player.Id = reader.GetInt64(0);

                    list.Add(player);
                }
                return list;
            }
                
            
        }
    }
}

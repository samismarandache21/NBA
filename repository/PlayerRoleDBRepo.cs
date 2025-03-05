using Microsoft.Data.SqlClient;
using NBA.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBA.repository
{
    internal class PlayerRoleDBRepo:AbstractDBRepo<long, PlayerRole>
    {
        public PlayerRoleDBRepo(string conn, string user, string pass):base(conn, user, pass) { }
        protected override void updateLastId()
        {
            base.updateLastId();
        }
        protected override void read()
        {
            using(var conn = new SqlConnection(this.conn))
            {
                conn.Open();
                string query = @"SELECT PR.id, PR.player_id, PR.match_id, PR.points, PR.role
                                FROM PLAYER_ROLES PR
                                ";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Enum.TryParse<Role>(reader.GetString(4), out Role p_role);
                    PlayerRole role = new PlayerRole(reader.GetInt64(1), reader.GetInt64(2), reader.GetInt32(3), p_role);
                    role.Id = reader.GetInt64(0);

                    base.Add(role);

                }
            }
        }
    }
}

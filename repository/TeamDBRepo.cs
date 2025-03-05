using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using MySql.Data;
using NBA.domain;

namespace NBA.repository
{
    internal class TeamDBRepo:AbstractDBRepo<long, Team>
    {
        public TeamDBRepo(string conn, string user, string password):base(conn, user, password) { }

        protected override void updateLastId()
        {
            base.updateLastId();
        }

        protected override void read()
        {
            using(var conn = new SqlConnection(this.conn))
            {
                conn.Open();
                string query = @"Select T.id, T.name FROM TEAMS T";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Team team = new Team(reader.GetString(1));
                    team.Id = reader.GetInt64(0);
                    base.Add(team);
                }
            }
        }
    }
}

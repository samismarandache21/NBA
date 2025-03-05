using NBA.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using MySql.Data;

namespace NBA.repository
{
    internal class StudentDBRepo: AbstractDBRepo<long, Student>
    {
        public StudentDBRepo(string conn, string user, string password):base(conn, user, password)
        {
        }

        protected override void updateLastId()
        {
            base.updateLastId();
        }

        protected override void read()
        {
            using( var conn = new SqlConnection(this.conn))
            {
                conn.Open();
                string query = @"SELECT S.id, S.name, S.school FROM STUDENTS S";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    Student student = new Student(reader.GetString(1), reader.GetString(2));
                    student.Id = reader.GetInt32(0);

                    base.Add(student);
                }
            }
        }
    }
}

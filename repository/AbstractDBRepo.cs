using NBA.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBA.repository
{
    internal abstract class AbstractDBRepo<ID, E>: InMemoryRepo<ID, E> where E : Entity<ID> where ID : IComparable<ID>
    {
        protected string? conn;
        protected string? user;
        protected string? password;

        public AbstractDBRepo(string conn, string user, string password)
        {
            this.conn = conn;
            this.user = user;
            this.password = password;

            read();
        }

        protected abstract void read();
    }
}

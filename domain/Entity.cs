using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBA.domain
{
    internal abstract class Entity<ID> where ID : notnull
    {
        private ID? id;

        public ID? Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public override bool Equals(object? obj)
        {
            if(obj ==null || obj.GetType() != GetType()) return false;
            Entity<ID> other = (Entity<ID>)obj;
            return Id!.Equals(other.Id);

        }

        public override int GetHashCode()
        {
            return Id!.GetHashCode();
        }
    }
}

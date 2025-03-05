using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBA.domain
{
    internal class Team: Entity<long>
    {
        private string? name;
        public Team() { }

        public Team(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get { return name!; }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}

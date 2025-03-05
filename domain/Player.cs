using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBA.domain
{
    internal class Player : Student
    {
        private Team? team;
        public Player() { }

        public Player(string name, string school, Team team): base(name, school)
        {
            this.team = team;
        }

        public Team Team
        {
            get { return team; }
        }

        public override string ToString()
        {
            return Name + " | " + School + " | " + Team;
        }
    }
}

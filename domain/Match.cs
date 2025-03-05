using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBA.domain
{
    internal class Match: Entity<long>
    {
        private Team? team1;
        private Team? team2;
        private DateTime date;

        public Match() { }

        public Match(Team? team1, Team? team2, DateTime date)
        {
            this.team1 = team1;
            this.team2 = team2;
            this.date = date;
        }

        public Team? Team1 { get { return team1!; } }
        public Team? Team2 { get {  return team2!; } }
        public DateTime Date { get { return date;} }

        public override string ToString()
        {
            return Team1.ToString() + " || " + Team2.ToString() + " || " + Date.ToString("yyyy-mm-dd hh:mm");
        }
    }
}

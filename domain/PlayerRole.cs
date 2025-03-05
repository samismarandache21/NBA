using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBA.domain
{
    internal class PlayerRole: Entity<long>
    {
        private long playerId;
        private long matchId;
        private int points;
        private Role role;

        public PlayerRole() { }

        public PlayerRole(long playerId, long matchId, int points, Role role)
        {
            this.playerId = playerId;
            this.matchId = matchId;
            this.points = points;
            this.role = role;
        }

        public long PlayerId { get { return playerId; } }
        public long MatchId { get { return matchId; } }
        public int Points { get { return points; } }
        public Role Role { get { return role; } }

        public override string ToString()
        {
            return PlayerId + " | " + MatchId + " | " + Points + " | " + Role.ToString();
        }


    }
}

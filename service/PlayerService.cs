using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBA.domain;
using NBA.repository;

namespace NBA.service
{
    internal class PlayerService
    {
        private PlayerDBRepo repo;

        public PlayerService(PlayerDBRepo repo)
        {
            this.repo = repo;
        }

        public IList<Player> GetPlayers(string name)
        {
            return repo.GetPlayers(name);
        }

        public IList<Player> GetActive(string name, long matchId)
        {
            return repo.GetActive(name,matchId);
        }
    }
}

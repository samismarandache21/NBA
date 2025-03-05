using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NBA.domain;
using NBA.repository;
using Match = NBA.domain.Match;

namespace NBA.service
{
    internal class MatchService
    {
        private MatchDBRepo repo;

        public MatchService(MatchDBRepo repo)
        {
            this.repo = repo;
        }

        public IEnumerable<Match> GetMatcheInPeriod(DateTime start, DateTime end)
        {
            return repo.MatchesInPeriod(start, end);
        }

        public IList<int> GetMatchesScore(long matchID, out string home, out string away)
        {
            Match match = repo.FindOne(matchID);
            Console.WriteLine(match);
            if(match == null)
            {
                home = string.Empty;
                away = string.Empty;
                return [];
            }
            home = match.Team1.Name;
            away = match.Team2.Name;
            return repo.GetMatchScore(match);
        }

        public Match FindOne(long matchID)
        {
            return repo.FindOne(matchID);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Azure.Identity;
using NBA.domain;
using NBA.service;
using Match = NBA.domain.Match;

namespace NBA.console
{
    internal class ui
    {
        private PlayerService PlayerService;
        private MatchService MatchService;

        public ui(PlayerService playerService, MatchService matchService)
        {
            this.PlayerService = playerService;
            this.MatchService = matchService;
        }

        private void menu()
        {
            Console.WriteLine("1. Afisati jucatorii unei echipe");
            Console.WriteLine("2. Afisati jucatorii unei echipe care au jucat intr-un meci dat");
            Console.WriteLine("3. Afisati meciurile dintr-un interval");
            Console.WriteLine("4. Afisati scorul meciului");
            Console.WriteLine("5. EXIT");
        }

        private void handleTeamPlayers()
        {
            Console.WriteLine("Introduceti numele unei echipe: ");
            string teamName = Console.ReadLine();

            IList<Player> list = PlayerService.GetPlayers(teamName);
            foreach(Player player in list)
            {
                Console.WriteLine(player);
            }
        }

        private void handleActive()
        {
            Console.WriteLine("Introduceti numele unei echipe: ");
            string teamName = Console.ReadLine();
            Console.WriteLine("Introduceti id ul unui meci: ");
            int.TryParse(Console.ReadLine(), out int matchId);
            IList<Player> list = PlayerService.GetActive(teamName, matchId);
            foreach(Player player in list)
            { Console.WriteLine(player); }
        }
        private DateTime getDate()
        {
            Console.WriteLine("An: ");
            int.TryParse(Console.ReadLine(), out int an);
            Console.WriteLine("Luna: ");
            int.TryParse(Console.ReadLine(), out int luna);
            Console.WriteLine("zi: ");
            int.TryParse(Console.ReadLine(), out int zi);
            return new DateTime(an, luna, zi);
        }

        private void handleMatchesInPeriod()
        {
            Console.WriteLine("De la data: ");
            DateTime start = getDate();
            Console.WriteLine("Pana la data: ");
            DateTime end = getDate();

            IEnumerable<Match> list = MatchService.GetMatcheInPeriod(start, end);
            foreach (Match match in list)
            {
                Console.WriteLine(match);
            }
        }

        private void handleMatchScore()
        {
            Console.WriteLine("Introduceti id ul meciului: ");
            long.TryParse(Console.ReadLine(), out long matchId);

            IList<int> score = MatchService.GetMatchesScore(matchId, out string home, out string away);
            if (score.Count() != 0)
            {
                Console.WriteLine(home + " " + score[0] + " - " + score[1] + " " + away);
            }
            else
                Console.WriteLine("Nu exista acest meci");
        }

        public void RunApp()
        {
            bool still_running = true;

            while (still_running)
            {
                menu();
                string input = Console.ReadLine()!;

                switch (input)
                {
                    case "1":
                        handleTeamPlayers();
                        break;
                    case "2":
                        handleActive();
                        break;
                    case "3":
                        handleMatchesInPeriod();
                        break;
                    case "4":
                        handleMatchScore();
                        break;
                    default:
                        Console.WriteLine("Ending...");
                        still_running = false;
                        break;
                }
            }
        }

    }
}

using NBA.console;
using NBA.domain;
using NBA.service;
using NBA.console;
using NBA.domain;
using NBA.repository;
using NBA.service;

public class Program
{

    private static List<Team> getTeams()
    {
        List<Team> teams = new List<Team>();

        Team t1 = new Team("T1");
        t1.Id = 1;
        Team t2 = new Team("T2");
        t2.Id = 2;
        Team t3 = new Team("T3");
        t3.Id = 3;
        Team t4 = new Team("T4");
        t3.Id = 4;
        Team t5 = new Team("T5");
        t3.Id = 5;

        teams.Add(t1); teams.Add(t2); teams.Add(t3); teams.Add(t4); teams.Add(t5);
        return teams;
    }
    public static void Main(string[] args)
    {
        InMemoryRepo<long, Team> teamRepo = new InMemoryRepo<long, Team>();

        List<Team> teams = getTeams();
        foreach (Team team in teams)
        {
            teamRepo.Add(team);
        }

        string connectionString = "Data Source=DESKTOP-RO89ATP;Initial Catalog=NBA;Integrated Security=True;Trust Server Certificate=True";
        TeamDBRepo teamDBRepo = new TeamDBRepo(connectionString, "", "");
        PlayerDBRepo playerDBRepo = new PlayerDBRepo(connectionString, "", "");
        MatchDBRepo matchDBRepo = new MatchDBRepo(connectionString, "", "");
        PlayerRoleDBRepo roleDBRepo = new PlayerRoleDBRepo(connectionString, "", "");
        PlayerService playerService = new PlayerService(playerDBRepo);
        MatchService matchService = new MatchService(matchDBRepo);
        ui console = new ui(playerService, matchService);
        console.RunApp();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teamwork_Projects
{
    class Program
    {
        static void Main(string[] args)
        {
            int teamsCount = int.Parse(Console.ReadLine());
            List<Team> teams = Team.CreateTeams(teamsCount);
            Team.AddMembersToTeams(teams);
            List<Team> teamsToDisband = new List<Team>();
            Team.PrintTeams(teams, teamsToDisband);

        }
    }
}
 
class Team
{
    public string Name { get; set; }
    public string Creator { get; set; }
    public List<string> Members { get; set; }
 
    public static List<Team> CreateTeams(int teamsCount)
    {
        List<Team> teams = new List<Team>();
 
        for (int i = 0; i < teamsCount; i++)
        {
            string[] teamArgs = Console.ReadLine().Split('-');
            string teamCreator = teamArgs[0];
            string teamName = teamArgs[1];
            if (teams.Any(t => t.Name == teamName))
            {
                Console.WriteLine("Team {0} was already created!",teamName);
            }
            else if (teams.Any(t => t.Creator == teamCreator))
            {
                Console.WriteLine("{0} cannot create another team!",teamCreator);
            }
            else
            {
                Team currentTeam = new Team();
                currentTeam.Name = teamName;
                currentTeam.Creator = teamCreator;
                currentTeam.Members = new List<string>();
                currentTeam.Members.Add(teamCreator);
                teams.Add(currentTeam);
                Console.WriteLine("Team {0} has been created by {1}!",currentTeam.Name, currentTeam.Creator);
            }
 
        }
        return teams;
    }
 
    public static void AddMembersToTeams(List<Team> teams)
    {
        string[] memberArgs = Console.ReadLine().Split(new char[] { '-', '>' }, StringSplitOptions.RemoveEmptyEntries);
        while (!memberArgs.Contains("end of assignment"))
        {
            string memberName = memberArgs[0];
            string memberTeam = memberArgs[1];
            Team currentTeam = new Team();
 
            if (!teams.Any(t => t.Name == memberTeam))
            {
                Console.WriteLine("Team {0} does not exist!", memberTeam);
            }
            else
            {
                bool foundMember = false;
                foreach (var team in teams)
                {
                    if (team.Members.Any(m => m == memberName))
                    {
                        foundMember = true;
                        Console.WriteLine("Member {0} cannot join team {1}!",memberName,team.Name);
                        break;
                    }
                }
                if (!foundMember)
                {
                    currentTeam = teams.First(t => t.Name == memberTeam);
                    currentTeam.Members.Add(memberName);
                }
            }
            memberArgs = Console.ReadLine().Split(new char[] { '-', '>' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
 
    public static void PrintTeams(List<Team> teams, List<Team> teamsToDisband)
    {
        foreach (var team in teams.OrderByDescending(t => t.Members.Count).ThenBy(t => t.Name))
        {
            Team currentTeam = team;
            currentTeam.Members.RemoveAt(0);
            if (currentTeam.Members.Count == 0)
            {
                teamsToDisband.Add(currentTeam);
                continue;
            }
 
            Console.WriteLine("{0}", team.Name);
            Console.WriteLine("- {0}",team.Creator);
            foreach (var member in team.Members.OrderBy(n => n))
            {
                Console.WriteLine("-- {0}", member);
            }
        }
 
        //print dissolute teams
        Console.WriteLine("Teams to disband:");
        foreach (var team in teamsToDisband.OrderBy(t => t.Name))
        {
            Console.WriteLine("{0}",team.Name);
        }    
    }
}

        
    


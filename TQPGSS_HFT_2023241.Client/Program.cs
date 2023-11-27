using System;
using TQPGSS_HFT_2023241.Repository;
using System.Linq;
using TQPGSS_HFT_2023241.Models;
using TQPGSS_HFT_2023241.Logic;
using ConsoleTools;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace TQPGSS_HFT_2023241.Client
{
    internal class Program
    {
        static DriverLogic driverLogic;
        static TeamLogic teamLogic;
        static GrandPrixLogic grandPrixLogic;
        static void Create(string entity)
        {
            if (entity=="Driver")
            {
                Driver d = new Driver();
                Console.Write("Enter the drivers name: ");
                d.Name=Console.ReadLine();
                driverLogic.Create(d);
            }
            else
            {
                if (entity=="Team")
                {
                    Team t = new Team();
                    Console.Write("Enter the teams name: ");
                    t.Name=Console.ReadLine();
                    teamLogic.Create(t);
                }
                else
                {
                    if (entity=="GrandPrix")
                    {
                        GrandPrix g = new GrandPrix();
                        Console.Write("Enter the GrandPrixs name: ");
                        g.Name=Console.ReadLine();
                        grandPrixLogic.Create(g);
                    }
                }
            }
            Console.ReadKey();
        }
        static void List(string entity)
        {
            if (entity == "Driver")
            {
                var items = driverLogic.ReadAll();
                foreach (var item in items)
                {
                    Console.WriteLine($"{item.Id} {item.Name} {item.Age} {item.Points} {item.TeamId}");
                    
                }
            }
            else
            {
                if (entity == "Team")
                {
                    var items = teamLogic.ReadAll();
                    foreach (var item in items)
                    {
                        Console.WriteLine($"{item.Id} {item.Name} {item.Driver1} {item.Driver2} {item.Points} {item.Principal}");

                    }
                }
                else
                {
                    if (entity == "GrandPrix")
                    {
                        var items = grandPrixLogic.ReadAll();
                        foreach (var item in items)
                        {
                            Console.WriteLine($"{item.Id} {item.Name} {item.Date} {item.WhoWon}");

                        }
                    }
                }
            }
            Console.ReadKey();
        }
        static void Delete(string entity)
        {
            if (entity == "Driver")
            {
                Console.Write("Enter the id you want to delete: ");
                int id = int.Parse(Console.ReadLine());
                driverLogic.Delete(id);
            }
            else
            {
                if (entity == "Team")
                {
                    Console.Write("Enter the id you want to delete: ");
                    int id = int.Parse(Console.ReadLine());
                    teamLogic.Delete(id);
                }
                else
                {
                    if (entity == "GrandPrix")
                    {
                        Console.Write("Enter the id you want to delete: ");
                        int id = int.Parse(Console.ReadLine());
                        grandPrixLogic.Delete(id);
                    }
                }
            }
            Console.ReadKey();
        }
        static void Update(string entity)
        {
            Console.WriteLine(entity + "updating...");
            Console.ReadKey();
        }
        static void TeamsDrivers()
        {
            Console.Write("Enter the name of the team you want to get the drivers for: ");
            string teamName = Console.ReadLine();
            var q1 = teamLogic.teamsDrivers(teamName);
            foreach (var item in q1)
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();
        }
        static void DriverWins()
        {
            Console.Write("Enter the name of the driver you want to get his wins: ");
            string driverName = Console.ReadLine();
            var q2 = driverLogic.driverWins(driverName);
            foreach (var item in q2)
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();
        }
        static void AvaragePointsPerGrandPrixByTeams()
        {
            var q3 = teamLogic.avaragePointsPerGrandPrix();
            foreach (var item in q3)
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();
        }
        static void FirstAndSecondDriverByPoints()
        {
            var q4 = teamLogic.firstAndSecondDriverByPoints();
            foreach (var item in q4)
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();
        }
        static void WinnerOfTheCircuit()
        {
            Console.Write("Enter the name of the circuit you want to get the winner: ");
            string circuit = Console.ReadLine();
            var q5 = grandPrixLogic.winnerOfTheCircuit(circuit);
            foreach (var item in q5)
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();
        }
        static void AvaragePointPerGrandPrixByDrivers()
        {
            var q6 = driverLogic.avaragePointPerGrandPrix();
            foreach (var item in q6)
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();
        }
        static void WhoWonTheMost()
        {
            var q7 = driverLogic.whoWonTheMost();
            foreach (var item in q7)
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();
        }
        static void GrandPrixDetails()
        {
            Console.Write("Enter the name of the circuit you want the details for: ");
            string circuitName = Console.ReadLine();
            var q8 = grandPrixLogic.grandPrixDetails(circuitName);
            foreach (var item in q8)
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();
        }
        static void Main(string[] args)
        {
            F1DbContext ctx = new F1DbContext();
            var DriverRepo = new DriverRepository(ctx);
            var TeamRepo = new TeamRepository(ctx);
            var GrandPrixRepo = new GrandPrixRepository(ctx);
            driverLogic = new DriverLogic(DriverRepo, GrandPrixRepo);
            teamLogic = new TeamLogic(TeamRepo, DriverRepo, GrandPrixRepo);
            grandPrixLogic = new GrandPrixLogic(GrandPrixRepo, DriverRepo);


            var driverSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Driver"))
                .Add("Create", () => Create("Driver"))
                .Add("Delete", () => Delete("Driver"))
                .Add("Update", () => Update("Driver"))
                .Add("DriverWins", () => DriverWins())
                .Add("AvaragePointPerGrandPrix", () => AvaragePointPerGrandPrixByDrivers())
                .Add("WhoWonTheMost", () => WhoWonTheMost())
                .Add("Exit", ConsoleMenu.Close);

            var teamSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Team"))
                .Add("Create", () => Create("Team"))
                .Add("Delete", () => Delete("Team"))
                .Add("Update", () => Update("Team"))
                .Add("TeamsDrivers", () => TeamsDrivers())
                .Add("AvaragePointsPerGrandPrix", () => AvaragePointsPerGrandPrixByTeams())
                .Add("FirstAndSecondDriverByPoints", () => FirstAndSecondDriverByPoints())
                .Add("Exit", ConsoleMenu.Close);

            var grandPrixSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("GrandPrix"))
                .Add("Create", () => Create("GrandPrix"))
                .Add("Delete", () => Delete("GrandPrix"))
                .Add("Update", () => Update("GrandPrix"))
                .Add("WinnerOfTheCircuit", () => WinnerOfTheCircuit())
                .Add("GrandPrixDetails", () => GrandPrixDetails())
                .Add("Exit", ConsoleMenu.Close);

            var menu = new ConsoleMenu(args, level: 0)
                .Add("Drivers", () => driverSubMenu.Show())
                .Add("Teams", () => teamSubMenu.Show())
                .Add("GrandPrixes", () => grandPrixSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);
            menu.Show();

        }
    }
}

using System;

using System.Linq;
using TQPGSS_HFT_2023241.Models;

using ConsoleTools;
using System.Collections.Generic;
using System.Collections;
//using System.Net.Http.Headers;

namespace TQPGSS_HFT_2023241.Client
{
    internal class Program
    {
        static void Create(string entity)
        {
            if (entity=="Driver")
            {
                Driver d = new Driver();
                Console.Write("Enter the drivers name: ");
                d.Name=Console.ReadLine();
                rest.Post(d, "driver");
            }
            else
            {
                if (entity=="Team")
                {
                    Team t = new Team();
                    Console.Write("Enter the teams name: ");
                    t.Name=Console.ReadLine();
                    rest.Post(t, "team");
                }
                else
                {
                    if (entity=="GrandPrix")
                    {
                        GrandPrix g = new GrandPrix();
                        Console.Write("Enter the GrandPrixs name: ");
                        g.Name=Console.ReadLine();
                        rest.Post(g, "grandprix");
                    }
                }
            }
            Console.ReadKey();
        }
        static void List(string entity)
        {
            if (entity == "Driver")
            {
                List<Driver> drivers = rest.Get<Driver>("driver");
                foreach (var item in drivers)
                {
                    Console.WriteLine($"{item.Id} {item.Name} {item.Age} {item.Points} {item.TeamId}");

                }
            }
            else
            {
                if (entity == "Team")
                {
                    //var items = teamLogic.ReadAll();
                    //foreach (var item in items)
                    //{
                    //    Console.WriteLine($"{item.Id} {item.Name} {item.Driver1} {item.Driver2} {item.Points} {item.Principal}");

                    //}
                    List<Team> teams = rest.Get<Team>("team");
                    foreach (var item in teams)
                    {
                        Console.WriteLine($"{item.Id} {item.Name} {item.Driver1} {item.Driver2} {item.Points} {item.Principal}");

                    }
                }
                else
                {
                    if (entity == "GrandPrix")
                    {
                        //var items = grandPrixLogic.ReadAll();
                        //foreach (var item in items)
                        //{
                        //    Console.WriteLine($"{item.Id} {item.Name} {item.Date} {item.WhoWon}");

                        //}
                        List<GrandPrix> grandprixs = rest.Get<GrandPrix>("grandprix");
                        foreach (var item in grandprixs)
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
                rest.Delete(id, "driver");
            }
            else
            {
                if (entity == "Team")
                {
                    Console.Write("Enter the id you want to delete: ");
                    int id = int.Parse(Console.ReadLine());
                    rest.Delete(id, "team");
                }
                else
                {
                    if (entity == "GrandPrix")
                    {
                        Console.Write("Enter the id you want to delete: ");
                        int id = int.Parse(Console.ReadLine());
                        rest.Delete(id, "grandprix");
                    }
                }
            }
            Console.ReadKey();
        }
        static void Update(string entity)
        {
            if (entity == "Driver")
            {
                Console.Write("Enter the driver's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Driver item = rest.Get<Driver>(id, "driver");
                Console.Write($"New name [old: {item.Name}]: ");
                string name = Console.ReadLine();
                item.Name = name;
                rest.Put(item, "driver");
            }
            else
            {
                if (entity == "Team")
                {
                    Console.Write("Enter the team's id to update: ");
                    int id = int.Parse(Console.ReadLine());
                    Team item = rest.Get<Team>(id, "team");
                    Console.Write($"New name [old: {item.Name}]: ");
                    string name = Console.ReadLine();
                    item.Name = name;
                    rest.Put(item, "team");

                }
                else
                {
                    if (entity == "GrandPrix")
                    {
                        Console.Write("Enter the grandprix's id to update: ");
                        int id = int.Parse(Console.ReadLine());
                        GrandPrix item = rest.Get<GrandPrix>(id, "grandprix");
                        Console.Write($"New name [old: {item.Name}]: ");
                        string name = Console.ReadLine();
                        item.Name = name;
                        rest.Put(item, "grandprix");
                    }
                }
            }
            Console.ReadKey();
        }
        static void TeamsDrivers()
        {
            List<object> a = rest.Get<object>("TeamStat/TeamsDrivers");
            foreach (var item in a)
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();
        }
        static void DriverWins()
        {

            List<object> a = rest.Get<object>("DriverStat/DriverWins");
            foreach (var item in a)
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();
        }
        static void AvaragePointsPerGrandPrixByTeams()
        {

            List<object> a = rest.Get<object>("TeamStat/AvaragePointsPerGrandPrixByTeams");
            foreach (var item in a)
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();
        }
        static void FirstAndSecondDriverByPoints()
        {
            List<object> a = rest.Get<object>("TeamStat/FirstAndSecondDriverByPoints");
            foreach (var item in a)
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();
        }
        static void WinnerOfTheCircuit()
        {
            List<object> a = rest.Get<object>("GrandPrixStat/WinnerOfTheCircuit");
            foreach (var item in a)
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();
        }
        static void AvaragePointPerGrandPrixByDrivers()
        {
            List<object> a = rest.Get<object>("DriverStat/AvaragePointPerGrandPrixByDrivers");
            foreach (var item in a)
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();
        }
        static void WhoWonTheMost()
        {
            List<object> a = rest.Get<object>("DriverStat/WhoWonTheMost");
            foreach (var item in a)
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();
        }
        static void GrandPrixDetails()
        {
            List<object> a = rest.Get<object>("GrandPrixStat/GrandPrixDetails");
            foreach (var item in a)
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();
        }
        static RestService rest;
        static void Main(string[] args)
        {
            rest = new RestService("http://localhost:18928/");

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

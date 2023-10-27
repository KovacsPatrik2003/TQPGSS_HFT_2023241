using System;
using TQPGSS_HFT_2023241.Repository;
using System.Linq;
using TQPGSS_HFT_2023241.Models;
using TQPGSS_HFT_2023241.Logic;

namespace TQPGSS_HFT_2023241.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            F1DbContext ctx = new F1DbContext();
            var DriverRepo = new DriverRepository(ctx);
            var TeamRepo=new TeamRepository(ctx);
            var driverlogic=new DriverLogic(DriverRepo);
            var teamLogic = new TeamLogic(TeamRepo);

            Console.Write("Enter the name of the team you want to get the drivers for: ");
            string teamName=Console.ReadLine();
            var q1 = teamLogic.teamsDrivers(teamName, DriverRepo);
            foreach (var item in q1)
            {
                Console.WriteLine(item);
            }
        }
    }
}

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
            var GrandPrixRepo = new GrandPrixRepository(ctx);
            var driverlogic=new DriverLogic(DriverRepo);
            var teamLogic = new TeamLogic(TeamRepo);
            var grandPrixLogic = new GrandPrixLogic(GrandPrixRepo);

            Console.Write("Enter the name of the team you want to get the drivers for: ");
            string teamName=Console.ReadLine();
            var q1 = teamLogic.teamsDrivers(teamName, DriverRepo);
            foreach (var item in q1)
            {
                Console.WriteLine(item);
            }

            Console.Write("Enter the name of the driver you want to get his wins: ");
            string driverName = Console.ReadLine();
            var q2 = driverlogic.driverWins(driverName, GrandPrixRepo);
            foreach (var item in q2)
            {
                Console.WriteLine(item);
            }

            var q3 = teamLogic.avaragePointsPerGrandPrix(GrandPrixRepo);
            foreach (var item in q3)
            {
                Console.WriteLine(item);
            }

            var q4 = teamLogic.firstAndSecondDriverByPoints(DriverRepo);
            foreach (var item in q4)
            {
                Console.WriteLine(item);
            }

            Console.Write("Enter the name of the circuit you want to get the winner: ");
            string circuit = Console.ReadLine();
            var q5 = grandPrixLogic.winnerOfTheCircuit(circuit, DriverRepo);
            foreach (var item in q5)
            {
                Console.WriteLine(item);
            }

            var q6 = driverlogic.avaragePointPerGrandPrix(GrandPrixRepo);
            foreach (var item in q6)
            {
                Console.WriteLine(item);
            }

        }
    }
}

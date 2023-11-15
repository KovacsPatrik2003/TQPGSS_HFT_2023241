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
            var driverlogic=new DriverLogic(DriverRepo,GrandPrixRepo);
            var teamLogic = new TeamLogic(TeamRepo,DriverRepo,GrandPrixRepo);
            var grandPrixLogic = new GrandPrixLogic(GrandPrixRepo,DriverRepo);

            Console.Write("Enter the name of the team you want to get the drivers for: ");
            string teamName=Console.ReadLine();
            var q1 = teamLogic.teamsDrivers(teamName);
            foreach (var item in q1)
            {
                Console.WriteLine(item);
            }
            
            Console.Write("Enter the name of the driver you want to get his wins: ");
            string driverName = Console.ReadLine();
            var q2 = driverlogic.driverWins(driverName);
            foreach (var item in q2)
            {
                Console.WriteLine(item);
            }
            
            var q3 = teamLogic.avaragePointsPerGrandPrix();
            foreach (var item in q3)
            {
                Console.WriteLine(item);
            }

            var q4 = teamLogic.firstAndSecondDriverByPoints();
            foreach (var item in q4)
            {
                Console.WriteLine(item);
            }
            
            Console.Write("Enter the name of the circuit you want to get the winner: ");
            string circuit = Console.ReadLine();
            var q5 = grandPrixLogic.winnerOfTheCircuit(circuit);
            foreach (var item in q5)
            {
                Console.WriteLine(item);
            }
            
            var q6 = driverlogic.avaragePointPerGrandPrix();
            foreach (var item in q6)
            {
                Console.WriteLine(item);
            }

            var q7 = driverlogic.whoWonTheMost();
            foreach (var item in q7)
            {
                Console.WriteLine(item);
            }

            Console.Write("Enter the name of the circuit you want the details for: ");
            string circuitName=Console.ReadLine();
            var q8 = grandPrixLogic.grandPrixDetails(circuitName);
            foreach (var item in q8)
            {
                Console.WriteLine(item);
            }
        }
    }
}

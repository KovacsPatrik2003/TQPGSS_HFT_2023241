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
            var repo = new DriverRepository(ctx);
            var driverlogic=new DriverLogic(repo);
            Driver a=new Driver()
            {
                Id=100,
                Name="Nikita Mazepin",
                Points=0,
                TeamId=ctx.Teams.Where(x=>x.Name=="Haas Ferrari").Select(x=>x.Id).First()
            };
            driverlogic.Create(a);
            var q1 = ctx.Drivers.Select(x => x.Name);
            
            foreach (var item in q1)
            {
                Console.WriteLine(item);
            }
        }
    }
}

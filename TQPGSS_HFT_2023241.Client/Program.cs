using System;
using TQPGSS_HFT_2023241.Repository;
using System.Linq;

namespace TQPGSS_HFT_2023241.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            F1DbContext ctx = new F1DbContext();

            var q1 = ctx.Drivers.Select(x => x.Name);
            foreach (var item in q1)
            {
                Console.WriteLine(item);
            }
        }
    }
}

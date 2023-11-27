using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TQPGSS_HFT_2023241.Models;
using TQPGSS_HFT_2023241.Repository;

namespace TQPGSS_HFT_2023241.Logic
{
    public class GrandPrixLogic : IGrandPrixLogic
    {
        IRepository<GrandPrix> repo;
        IRepository<Driver> d;
        public GrandPrixLogic(IRepository<GrandPrix> repo, IRepository<Driver> d)
        {
            this.repo = repo;
            this.d = d;
        }
        public void Create(GrandPrix item)
        {
            if (item.Name.Length < 3)
            {
                throw new ArgumentException("name is too short...");
            }
            this.repo.Create(item);
        }
        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public GrandPrix Read(int id)
        {
            return this.repo.Read(id);
        }
        public IQueryable<GrandPrix> ReadAll()
        {
            return this.repo.ReadAll();
        }
        public void Update(GrandPrix item)
        {
            this.repo.Update(item);
        }

        public IEnumerable winnerOfTheCircuit(string name)
        {
            return from x in repo.ReadAll()
                   let whowonId = x.WhoWon
                   let driverName = d.ReadAll().Where(x => x.Id == whowonId).Select(x => x.Name).First()
                   where x.Name == name
                   select driverName;
        }

        public IEnumerable grandPrixDetails(string name)
        {
            return from x in repo.ReadAll()
                   where x.Name == name
                   select new
                   {
                       Id = x.Id,
                       Name = x.Name,
                       Date = x.Date,
                       Winner = d.ReadAll().Where(y => y.Id == x.WhoWon).Select(x => x.Name).First()
                   };
        }
    }
}

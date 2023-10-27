using System;
using System.Collections;
using System.Linq;
using System.Runtime.Versioning;
using TQPGSS_HFT_2023241.Models;
using TQPGSS_HFT_2023241.Repository;

namespace TQPGSS_HFT_2023241.Logic
{
    public class DriverLogic : IDriverLogic
    {
        IRepository<Driver> repo;
        public DriverLogic(IRepository<Driver> repo)
        {
            this.repo = repo;
        }
        public void Create(Driver item)
        {
            this.repo.Create(item);
        }
        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Driver Read(int id)
        {
            return this.repo.Read(id);
        }
        public IQueryable<Driver> ReadAll()
        {
            return this.repo.ReadAll();
        }
        public void Update(Driver item)
        {
            this.repo.Update(item);
        }

        public IEnumerable driverWins(string name, IRepository<GrandPrix> g)
        {
            return from a in g.ReadAll()
                     let winner = a.WhoWon
                     let driver = repo.ReadAll().Where(x => x.Name == name).Select(x => x.Id).First()
                     where driver == winner
                     select a.Name;
        }
    }
}

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
        IRepository<GrandPrix> g;
        public DriverLogic(IRepository<Driver> repo, IRepository<GrandPrix> g)
        {
            this.repo = repo;
            this.g = g;
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

        public IEnumerable driverWins(string name)
        {
            return from a in g.ReadAll()
                     let winner = a.WhoWon
                     let driver = repo.ReadAll().Where(x => x.Name == name).Select(x => x.Id).First()
                     where driver == winner
                     select a.Name;
        }

        public IEnumerable avaragePointPerGrandPrix()
        {
            return from x in repo.ReadAll()
                   let numberOfGranPrixes = g.ReadAll().Select(x => x).Count()
                   select Math.Round((double)x.Points / numberOfGranPrixes, 2);
        }

        public IEnumerable whoWonTheMost()
        {
            var q7 = from x in g.ReadAll()
                     group x by x.WhoWon into grp
                     select grp.Key;
            var q70 = q7.ToList();
            q70.Sort();
            return from x in repo.ReadAll()
                         where q70.Take(1).First() == x.Id
                         select x.Name;
        }
    }
}

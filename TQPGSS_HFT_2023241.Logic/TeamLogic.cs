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
    public class TeamLogic : ITeamLogic
    {
        IRepository<Team> repo;
        IRepository<Driver> d;
        IRepository<GrandPrix> g;
        public TeamLogic(IRepository<Team> repo, IRepository<Driver> d, IRepository<GrandPrix> g)
        {
            this.repo = repo;
            this.d = d;
            this.g = g;
        }
        public void Create(Team item)
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
        public Team Read(int id)
        {
            return this.repo.Read(id);
        }
        public IQueryable<Team> ReadAll()
        {
            return this.repo.ReadAll();
        }
        public void Update(Team item)
        {
            this.repo.Update(item);
        }
        public IEnumerable teamsDrivers(string teamName)
        {
            var result= from s in d.ReadAll()
                     let team = repo.ReadAll().Where(x => x.Name == teamName).Select(x => x.Id).First()
                     where team == s.TeamId
                     select s.Name;
            try
            {
                if (result.Count() != 0)
                {
                    return result;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {

                return new List<string>() { "This team does not exist." };
            }
        }

        public IEnumerable avaragePointsPerGrandPrix()
        {
            return from x in repo.ReadAll()
                   let numberOfGrandPrixes = g.ReadAll().Select(x => x.Id).Count()
                   select Math.Round(((double)x.Points / numberOfGrandPrixes), 2);
        }

        public IEnumerable firstAndSecondDriverByPoints()
        {
            return from m in repo.ReadAll()
                   let driver1 = m.Driver1
                   let driver2 = m.Driver2
                   let driver1Points = d.ReadAll().Where(x => x.Id == driver1).Select(x => x.Points).First()
                   let driver2Points = d.ReadAll().Where(x => x.Id == driver2).Select(x => x.Points).First()
                   select new
                   {
                       team = m.Name,
                       Driver1 = driver1,
                       Driver2 = driver2,
                       Driver1Points = driver1Points,
                       Driver2Points = driver2Points,
                       first = driver1Points > driver2Points ? driver1 : driver2,
                       second = driver2Points < driver1Points ? driver2 : driver1
                   };
        }
    }
}

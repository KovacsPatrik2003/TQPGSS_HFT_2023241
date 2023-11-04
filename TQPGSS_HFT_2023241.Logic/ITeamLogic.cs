using System.Collections;
using System.Linq;
using TQPGSS_HFT_2023241.Models;
using TQPGSS_HFT_2023241.Repository;

namespace TQPGSS_HFT_2023241.Logic
{
    public interface ITeamLogic
    {
        void Create(Team item);
        void Delete(int id);
        Team Read(int id);
        IQueryable<Team> ReadAll();
        void Update(Team item);
        IEnumerable teamsDrivers(string teamName);
        IEnumerable avaragePointsPerGrandPrix();
        IEnumerable firstAndSecondDriverByPoints();
    }
}
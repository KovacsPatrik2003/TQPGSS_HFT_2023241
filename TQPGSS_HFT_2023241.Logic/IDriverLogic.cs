using System.Collections;
using System.Linq;
using TQPGSS_HFT_2023241.Models;
using TQPGSS_HFT_2023241.Repository;

namespace TQPGSS_HFT_2023241.Logic
{
    public interface IDriverLogic
    {
        void Create(Driver item);
        void Delete(int id);
        Driver Read(int id);
        IQueryable<Driver> ReadAll();
        void Update(Driver item);
        IEnumerable driverWins(string name, IRepository<GrandPrix> g);
    }
}
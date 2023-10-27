using System.Collections;
using System.Linq;
using TQPGSS_HFT_2023241.Models;
using TQPGSS_HFT_2023241.Repository;

namespace TQPGSS_HFT_2023241.Logic
{
    public interface IGrandPrixLogic
    {
        void Create(GrandPrix item);
        void Delete(int id);
        GrandPrix Read(int id);
        IQueryable<GrandPrix> ReadAll();
        void Update(GrandPrix item);
        IEnumerable winnerOfTheCircuit(string name, IRepository<Driver> d);
        IEnumerable grandPrixDetails(string name,IRepository<Driver> d);
    }
}
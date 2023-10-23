using System.Linq;
using TQPGSS_HFT_2023241.Models;

namespace TQPGSS_HFT_2023241.Logic
{
    public interface IDriverLogic
    {
        void Create(Driver item);
        void Delete(int id);
        Driver Read(int id);
        IQueryable<Driver> ReadAll();
        void Update(Driver item);
    }
}
using System;
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
        public GrandPrixLogic(IRepository<GrandPrix> repo)
        {
            this.repo = repo;
        }
        public void Create(GrandPrix item)
        {
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TQPGSS_HFT_2023241.Models;

namespace TQPGSS_HFT_2023241.Repository
{
    public class GrandPrixRepository : Repository<GrandPrix>, IRepository<GrandPrix>
    {
        public GrandPrixRepository(F1DbContext ctx) : base(ctx)
        {
        }

        public override GrandPrix Read(int id)
        {
            return ctx.GrandPrixes.FirstOrDefault(x => x.Id == id);
        }

        public override void Update(GrandPrix item)
        {
            var old=Read(item.Id);
            foreach (var prop in old.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(item));
            }
            ctx.SaveChanges();
        }
    }
}

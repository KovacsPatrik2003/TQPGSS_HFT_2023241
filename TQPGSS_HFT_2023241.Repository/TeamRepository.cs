using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TQPGSS_HFT_2023241.Models;

namespace TQPGSS_HFT_2023241.Repository
{
    public class TeamRepository : Repository<Team>, IRepository<Team>
    {
        public TeamRepository(F1DbContext ctx) : base(ctx)
        {
        }

        public override Team Read(int id)
        {
            return ctx.Teams.FirstOrDefault(t => t.Id == id);
        }

        public override void Update(Team item)
        {
            var old=Read(item.Id);
            foreach (var prop in old.GetType().GetProperties())
            {
                //prop.SetValue(old, prop.GetValue(item));
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    prop.SetValue(old, prop.GetValue(item));
                }
            }
            ctx.SaveChanges();
        }
    }
}

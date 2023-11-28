using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TQPGSS_HFT_2023241.Models;

namespace TQPGSS_HFT_2023241.Repository
{
    public class DriverRepository : Repository<Driver>, IRepository<Driver>
    {
        public DriverRepository(F1DbContext ctx) : base(ctx)
        {
        }

        public override Driver Read(int id)
        {
            return ctx.Drivers.FirstOrDefault(x => x.Id == id);
        }

        public override void Update(Driver item)
        {
            var oldItem = Read(item.Id);
            foreach (var prop in oldItem.GetType().GetProperties())
            {
                //prop.SetValue(oldItem, prop.GetValue(item));

                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    prop.SetValue(oldItem, prop.GetValue(item));
                }
            }
            ctx.SaveChanges();
        }
    }
}

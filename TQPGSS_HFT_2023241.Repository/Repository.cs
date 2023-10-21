﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TQPGSS_HFT_2023241.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected F1DbContext ctx;
        public Repository(F1DbContext ctx)
        {
            this.ctx = ctx;
        }

        public void Create(T item)
        {
            ctx.Set<T>().Add(item);
            ctx.SaveChanges();
        }

        public void Delete(int id)
        {
            ctx.Set<T>().Remove(Read(id));
            ctx.SaveChanges();
        }

        public IQueryable<T> ReadAll()
        {
            return ctx.Set<T>();
        }

        public abstract void Update(T item);
        public abstract T Read(int id);
    }
}

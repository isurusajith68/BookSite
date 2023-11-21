using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ZeroToHero.DataAccess.Data;
using ZeroToHero.DataAccess.Repository.IRepository;
namespace ZeroToHero.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();


        }
      public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(System.Linq.Expressions.Expression<Func<T,bool>>filter)
        {
            
        }

        IEnumerable<T> IRepository<T>.GetAll()
        {
            throw new NotImplementedException();
        }

        void IRepository<T>.Remove(T entity)
        {
            throw new NotImplementedException();
        }

        void IRepository<T>.RemoveRange(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }
    }
}

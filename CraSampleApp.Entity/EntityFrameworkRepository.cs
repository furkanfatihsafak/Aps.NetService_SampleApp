using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraSampleApp.Entity
{

    public interface IEntityFrameworkRepository<TEntity>
    {
        List<TEntity> ToList();
        IQueryable<TEntity> GetQueryable();
        bool Add(TEntity entity);
        bool Update(TEntity entity);
        bool Remove(object id);
        TEntity GetById(object id);
    }

    


    public class EntityFrameworkRepository<TEntity> : IEntityFrameworkRepository<TEntity> where TEntity : class
    {
        private DbContext _context;
        public EntityFrameworkRepository(DbContext context)
        {
            _context = context;
        }

        public List<TEntity> ToList()
        {

            var dbSet = _context.Set<TEntity>();
            return dbSet.ToList();
        }


        public IQueryable<TEntity> GetQueryable()
        {
            var dbSet = _context.Set<TEntity>();
            return dbSet.AsQueryable();
        }


        public bool Add(TEntity entity)
        {

            var dbSet = _context.Set<TEntity>();
            dbSet.Add(entity);
            _context.SaveChanges();
            return true;
        }
        public bool Update(TEntity entity)
        {
            _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
            return true;
        }

        public bool Remove(object id)
        {
            var dbSet = _context.Set<TEntity>();
            var entity = dbSet.Find(id);
            dbSet.Remove(entity);
            _context.SaveChanges();
            return true;
        }


        public TEntity GetById(object id)
        {
            var dbSet = _context.Set<TEntity>();
            return dbSet.Find(id);
        }


    }
}

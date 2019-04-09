using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.EntityFrameworkDataAccess
{
    public class EFGenericRepository<T> : IDataRepository<T> where T: class
    {
        private DbSet<T> _set;
        private CareerCloudContext _context;
        public EFGenericRepository(bool createProxy = true)
        {
            _context = new CareerCloudContext(createProxy);
            _set = _context.Set<T>();
        }
        public void Add(params T[] items)
        {
            foreach(var poco in items)
            {
                //_context.Entry(poco).State = EntityState.Added;
                _set.Add(poco);
            }
            SaveChanges();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            //throw new NotImplementedException();
        }

        public IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {

            IQueryable<T> query = _set;
            foreach (Expression<Func<T, object>> navProp in navigationProperties)
            {
                query = query.Include<T, object>(navProp);
            }
            return query.ToList();

        }

        public IList<T> GetList(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> query = _set;
            foreach (Expression<Func<T, object>> navProp in navigationProperties)
            {
                query = query.Include<T, object>(navProp);
            }
            return query.Where(where).ToList();
        }

        public T GetSingle(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {

            IQueryable<T> query = _set;
            foreach(Expression<Func<T, object>> navProp in navigationProperties)
            {
                query = _set.Include<T,object>(navProp);
                
            }
            return query.Where(where).FirstOrDefault();
       
        }

        public void Remove(params T[] items)
        {
            foreach(var poco in items)
            {
                _context.Entry(poco).State = EntityState.Deleted;

            }
            SaveChanges();
        }

        public void Update(params T[] items)
        {
            foreach(var poco in items)
            {
                _context.Entry(poco).State = EntityState.Modified;
            }
            SaveChanges();
        }
        private void SaveChanges()
        {
            try
            {
                _context.SaveChanges();
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Repositories.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EfCore
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class 
    {
         protected readonly RepositoryContext _context;

        public RepositoryBase(RepositoryContext context)
        {
            _context = context;
        }

        public void Delete(T t) => _context.Set<T>().Remove(t);


        public IQueryable<T> FindAll(bool trackChange) => !trackChange ? _context.Set<T>().AsNoTracking() : _context.Set<T>();


        public IQueryable<T> FindAllCondition(Expression<Func<T, bool>> expression, bool trackChange)
            => !trackChange ? _context.Set<T>().Where(expression).AsNoTracking() : _context.Set<T>().Where(expression);
       
        public void Insert(T t) => _context.Set<T>().Add(t);

        public void Update(T t) => _context.Set<T>().Update(t);
      
    }
   
}

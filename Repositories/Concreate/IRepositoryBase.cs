using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Concreate
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll(bool trackChange);

        IQueryable<T> FindAllCondition(Expression<Func<T,bool>>expression,bool trackChange);

        void Update(T t);
        void Delete(T t);
        void Insert(T t);

    }
}

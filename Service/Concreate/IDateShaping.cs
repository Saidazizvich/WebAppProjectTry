using Entities.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Concreate
{
    public interface IDateShaping<T>
    {
        // requestparametr class icinda field tamomladik ve business esa una bir metod yazdik ve impelement etik  
        IEnumerable<ShaperEntity> ShapeData(IEnumerable<T> entities, string fieldString);

        ShaperEntity ShapeData(T entity, string fieldString);
    }
}

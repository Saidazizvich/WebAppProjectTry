using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exeptions
{
    public abstract class NotFound : Exception
    {
        // global hata yontemi buda daha iyidir yani try catch kullanmiyacaz dikkat!!!
        protected NotFound(string message) : base(message) 
        {
        
        
        }
        

        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
      // burda abstract yaptimiz icin hic bir zaman new yapamaz ve kalitim alir sonra esa ovveride eder
    public abstract class BestBadRequestExeption : Exception
    {
        protected BestBadRequestExeption(string message) : base(message)
        {

        }

      

       
    }   
}

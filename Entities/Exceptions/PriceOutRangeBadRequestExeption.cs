using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class PriceOutRangeBadRequestExeption : BestBadRequestExeption
    {
        public PriceOutRangeBadRequestExeption() : base("Maxium price should be less 1000 and greater than 10")
        {
            

        }
    }
}

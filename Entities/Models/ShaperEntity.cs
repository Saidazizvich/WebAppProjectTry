using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class ShaperEntity
    {
         //ExpandoObject yerini artik shaperentity aliyor cunku bunun icinda refernce olarak entity alyor dikkat  
        public int Id { get; set; }

        public Entity Entity { get; set; }

        public ShaperEntity()
        {
            Entity = new Entity(); // dikkat
        }
    }
}

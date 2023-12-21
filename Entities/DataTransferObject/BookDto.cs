using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObject
{
    public record BookDto // bu ise Mapper islemi dikkat neden bunu kullaniyoruz cunku
                           // veri kopya yapariz ve kullaniciye kopyani gosteririz ve bu kopyadan ustundan kullanici isle yapar
    {
        public int Id { get; init; }
        public String Title { get; init; }
        public decimal Price { get; init; }
    }
}

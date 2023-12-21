namespace Entities.RequestFeatures
{
    public class BookParamets : RequestParametrs
    {
        public uint MinPrice { get; set; }

        public uint MaxPrice { get; set; }

        public bool ValidPriceRange => MaxPrice > MinPrice;    

        public String? SearchTerm { get; set; } // search islemi ekledik dikkat !!

        public BookParamets()
        {
            OrderBy = "id";
        }      

    }
}

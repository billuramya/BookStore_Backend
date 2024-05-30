using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entity
{
    public class BookEntity
    {
        public int BookId { get; set; }

        public string Title { get; set; }
        public string Author { get; set; }
        public long OriginalPrice { get; set; }
        public long DiscountPrice {  get; set; }

        public decimal Ratting { get; set; }
        public int RatedPersons { get; set; }
        

        public string Description { get; set; }

        public int Quantity { get; set; }

        public string Image { get; set; }
    }
}
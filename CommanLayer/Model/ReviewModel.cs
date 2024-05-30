using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommanLayer.Model
{
    public class ReviewModel
    {
        public string Review { get; set; }

        public int Star { get; set; }

        public int BookId { get; set; }

        public int UserId { get; set; }
    }
}

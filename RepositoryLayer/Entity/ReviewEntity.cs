using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entity
{
    public class ReviewEntity
    {
        public string FullName { get; set; }

        public string Reviews { get; set; }

        public int Star { get; set; }

        public int BookId { get; set; }
    }
}

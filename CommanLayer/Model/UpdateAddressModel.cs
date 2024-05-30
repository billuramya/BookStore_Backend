using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommanLayer.Model
{
    public class UpdateAddressModel
    {
        public int UserId { get; set; }

        public int AdID { get; set; }

        public string FullAddress { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Type { get; set; }
    }
}

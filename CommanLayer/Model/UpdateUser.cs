﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Models
{
    public class UpdateUser
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public long MobileNumber { get; set; }
    }
}
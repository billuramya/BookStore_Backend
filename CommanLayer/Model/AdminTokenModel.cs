﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommanLayer.Model
{
    public class AdminTokenModel
    {
        public int AdminId { get; set; }

        public string AdminName { get; set; }


        public string Email { get; set; }

        public string Token { get; set; }


        public string Password { get; set; }
    }
}

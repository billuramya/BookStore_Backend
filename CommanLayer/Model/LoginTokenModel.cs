﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Models
{
    public class LoginTokenModel
    {
        public int UserId { get; set; }

        public string FullName { get; set; }


        public string Email { get; set; }

        public string Token { get; set; }

        public long MobileNumber { get; set; }


        public string Password { get; set; }

    }
}
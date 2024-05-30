﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entity
{
    public class UserEntity
    {
        public int UserId {  get; set; }
        public string FullName {  get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public long MobileNumber { get; set; }
    }
}

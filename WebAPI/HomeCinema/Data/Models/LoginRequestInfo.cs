﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeCinema.Data
{
    public class LoginRequestInfo
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}

﻿using BimApp.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BimApp.API.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);



    }
}

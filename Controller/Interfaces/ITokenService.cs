using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Controller.Interfaces
{
    public interface ITokenService
    {
        String CreateToken(AppUser user);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace api.Controller.Hellpers
{
    public class QueryObject
    {
        public String? Symbol { get; set; } = null;
        public String? CompanyName { get; set; } = null;
    }
}
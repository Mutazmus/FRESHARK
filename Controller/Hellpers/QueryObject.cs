using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace api.Controller.Hellpers
{
    public class QueryObject
    {
        // Fillter Or Search
        public String? Symbol { get; set; } = null;
        public String? CompanyName { get; set; } = null;

        // SortBy
        public string? SortBy { get; set; } = null;
        public bool IsDecsending { get; set; } = false;

        // Pagination
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
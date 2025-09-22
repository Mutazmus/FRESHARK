using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Controller.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<Stock>> GetUserPortfolio(AppUser user);

        Task<Portfolio> CraeteAsync(Portfolio portfolio);

        Task<Portfolio> DeletePortfolio(AppUser appUser, String Symbol);
    }
}
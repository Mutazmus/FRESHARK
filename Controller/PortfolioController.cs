using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Controller.Interfaces;
using api.Data.Extentions;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace api.Controller
{
    [Route("api/portfoloi")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {

        private readonly IStockRepository _stockRepository;
        private readonly UserManager<AppUser> _userManager;

        private readonly IPortfolioRepository _portfolioRepository;
        public PortfolioController(IStockRepository stockRepository, UserManager<AppUser> userManger, IPortfolioRepository portfolioRepository)
        {
            _stockRepository = stockRepository;
            _userManager = userManger;
            _portfolioRepository = portfolioRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortFolio()
        {
            var username = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(username);
            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
            return Ok(userPortfolio);

        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string Symbol)
        {
            var nameName = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(nameName);
            var stock = await _stockRepository.GetStockBySymbol(Symbol);
            if (stock == null) return BadRequest("Stock Not Found");

            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
            if (userPortfolio.Any(e => e.Symbol.ToLower() == Symbol.ToLower())) return BadRequest("Cannot Add Same Stock to Portfolio ");

            var portfolioModel = new Portfolio
            {
                StockId = stock.Id,
                AppUserId = appUser.Id,
            };

            await _portfolioRepository.CraeteAsync(portfolioModel);
            if (portfolioModel == null)
            {
                return StatusCode(500, "Could not Create");
            }
            else
            {
                return Created();
            }

        }


        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio(string Symbol)
        {
            var userName = User.GetUserName();
            var appUse = await _userManager.FindByNameAsync(userName);
            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUse);
            var filteredStock = userPortfolio.Where(u => u.Symbol.ToLower() == Symbol.ToLower()).ToList();
            if (filteredStock.Count() == 1)
            {
                await _portfolioRepository.DeletePortfolio(appUse, Symbol);
            }
            else
            {
                return BadRequest("Stock not in your portfolio");
            }
            return Ok();
        }
    }
}
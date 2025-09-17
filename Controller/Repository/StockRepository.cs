using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Controller.Hellpers;
using api.Controller.Interfaces;
using api.Data;
using api.Models;
using api.Models.Dtos.StocsDto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Controller.Repository
{
    public class StockRepository : IStockRepository

    {
        private readonly ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (stockModel == null)
            {
                return null;
            }
            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        {
            var stocks = _context.Stocks.Include(x => x.Comments).AsQueryable();

            // Filter OR Search
            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks = _context.Stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
            }
            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stocks = _context.Stocks.Where(s => s.Symbol.Contains(query.Symbol));
            }

            // SortBy
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDecsending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
                }
            }

            // Pagination
            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();

        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.Include(x=>x.Comments).FirstOrDefaultAsync(c=>c.Id==id);
        }

        public Task<bool> StockExist(int id)
        {

            return _context.Stocks.AnyAsync(x => x.Id == id);
           
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequest stockDto)
        {
            var stockExist = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (stockExist == null)
            {
                return null;
            }
            stockExist.Symbol = stockDto.Symbol;
            stockExist.CompanyName = stockDto.CompanyName;
            stockExist.Purchase = stockDto.Purchase;
            stockExist.LastDiv = stockDto.LastDiv;
            stockExist.Industry = stockDto.Industry;
            stockExist.MarketCap = stockDto.MarketCap;

            await _context.SaveChangesAsync();
            return stockExist;
        }

    }
}
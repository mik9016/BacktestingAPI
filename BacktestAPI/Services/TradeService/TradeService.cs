using System;
using BacktestAPI.Data;
using BacktestAPI.Models;
using BacktestAPI.Services.CalculationService;
using BacktestAPI.Services.TradeService;

namespace BacktestAPI.Services.TradeService
{
	public class TradeService : ITradeService
    {
		private readonly DataContext _context;


        public TradeService(DataContext context)
		{
			_context = context;
		}

		public async Task<List<Trade>> GetAllTrades()
		{
            var trades = await _context.Trades.ToListAsync();

            return trades;
        }

        public async Task<List<Trade>> AddTrade(Trade trade)
        {
            _context.Trades.Add(trade);
            await _context.SaveChangesAsync();
            return await _context.Trades.ToListAsync();
        }

        public async Task<List<Trade>>? UpdateTrade(int id, Trade request)
        {
            var trade = await _context.Trades.FindAsync(id);
            if (trade is null)
                return null;

            // Make separate Class to handle calculations

            await _context.SaveChangesAsync();

            return await _context.Trades.ToListAsync(); ;
        }

        public async Task<Trade>? GetSingleTrade(int id)
        {
            var trade = await _context.Trades.FindAsync(id);
            if (trade is null)
                return null;
            return trade;
        }

        public async Task<List<Trade>>? DeleteTrade(int id, Trade request)
        {
            var trade = await _context.Trades.FindAsync(id);
            if (trade is null)
                return null;

            _context.Trades.Remove(trade);
            await _context.SaveChangesAsync();
            return await _context.Trades.ToListAsync(); 
        }
    }
}


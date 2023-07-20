using System;
using BacktestAPI.Models;

namespace BacktestAPI.Services.TradeService
{
	public interface ITradeService
	{
		
            Task<List<Trade>> GetAllTrades();
            Task<Trade>? GetSingleTrade(int id);
            Task<List<Trade>> AddTrade(Trade trade);
            Task<List<Trade>>? UpdateTrade(int id, Trade request);
            Task<List<Trade>>? DeleteTrade(int id, Trade request);
        
	}
}


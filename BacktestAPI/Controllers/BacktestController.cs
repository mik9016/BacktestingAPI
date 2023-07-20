using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BacktestAPI.Models;
using BacktestAPI.Services.CalculationService;
using BacktestAPI.Services.TradeService;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BacktestAPI
{
    [ApiController]
    [Route("api/backtest")]
    public class BacktestController : ControllerBase
    {
        private readonly ITradeService _tradeService;

        public BacktestController(ITradeService tradeService)
        {
            _tradeService = tradeService;
        }


        // GET: api/values
        [HttpGet]
        public async Task<List<Trade>> GetAllTrades()
        {
            var allTrades = await _tradeService.GetAllTrades();
            return allTrades;
        }

        // GET api/values/5
        #nullable disable
        [HttpGet("{id}")]
        public async Task<ActionResult<Trade>> GetSingleTrade(int id)
        {
            var result = await _tradeService.GetSingleTrade(id);
            if (result is null)
                return NotFound("Trade not found");
            return Ok(result);
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<List<Trade>>> AddTrade([FromBody] Trade trade)
        {
            var result = await _tradeService.AddTrade(trade);
            return Ok(result);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Trade>> UpdateTrade(int id, Trade request)
        {
            var result = await _tradeService.UpdateTrade(id, request);
            if (result is null)
                return NotFound("Trade not found!");
            return Ok(result);
        }
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Trade>> DeleteTrade(int id, Trade request)
        {
            var result = await _tradeService.DeleteTrade(id,request);
            if (result is null)
                return NotFound("Trade not found!");
            return Ok(result);
        }
        #nullable restore

    }
}


using BacktestAPI.Models;
using BacktestAPI.Services.TradeService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BacktestAPI
{
    [ApiController]
    [Route("api/backtest")]
    [Authorize]
    public class BacktestController : ControllerBase
    {
        private readonly ITradeService _tradeService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BacktestController(ITradeService tradeService, IHttpContextAccessor httpContextAccessor)
        {
            _tradeService = tradeService;
            _httpContextAccessor = httpContextAccessor;
        }


        // GET: api/values
        [HttpGet]
        public async Task<List<Trade>> GetAllTrades()
        {

            var allTrades = await _tradeService.GetAllTrades();
            return allTrades;
        }


        // POST api/values
        [HttpPost]
        public async Task<ActionResult<List<Trade>>> AddTrade([FromBody] Trade trade)
        {
            // get current userId from token
            var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst("userId").Value);
            if (userId != trade.UserId)
            {
                return BadRequest("You can add Trades only for your user!");
            }
            var result = await _tradeService.AddTrade(trade);
            return Ok(result);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Trade>> UpdateTrade(int id, Trade request)
        {
            var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst("userId").Value);
            if (userId != request.UserId)
            {
                return BadRequest("You can update Trades only for your user!");
            }
            var result = await _tradeService.UpdateTrade(id, request);
            if (result is null)
                return NotFound("Trade not found!");
            return Ok(result);
        }
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Trade>> DeleteTrade(int id, Trade request)
        {
            var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst("userId").Value);
            if (userId != request.UserId)
            {
                return BadRequest("You can delete Trades only for your user!");
            }
            var result = await _tradeService.DeleteTrade(id,request);
            if (result is null)
                return NotFound("Trade not found!");
            return Ok(result);
        }
        #nullable restore

    }
}


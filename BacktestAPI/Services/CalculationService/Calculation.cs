using System;
using System.Collections.Generic;
using BacktestAPI.Models;

namespace BacktestAPI.Services.CalculationService
{
	public class Calculation
	{
		/* Calculate :
		 * - Current Ballance +
		 * - Win Number +
		 * - Los Number +
		 * - Win Percentage +
		 * - Loss Percentage +
		 * - Number of Trades +
		 * 
		 */

		public static int CalculateTradesNumber(List<Trade> trades)
		{
			if (trades is not null)
				return trades.Count();

            throw new ArgumentNullException(nameof(trades), "Trades list cannot be null.");

        }

		public static Dictionary<string, int> CalculateWinsLosses(List<Trade> trades, float fee)
		{
            if (trades is null)
            {
                throw new ArgumentNullException(nameof(trades), "Trades list cannot be null.");
            }

            int winsCount = 0;
            int lossesCount = 0;
            int breakevenCount = 0;

            foreach (Trade trade in trades)
            {
                float resultAfterFee = trade.ResultInDollars - fee;

                if (resultAfterFee > 0)
                {
                    winsCount++;
                }
                else if (resultAfterFee < 0)
                {
                    lossesCount++;
                }
                else
                {
                    breakevenCount++;
                }
            }

            Dictionary<string, int> result = new Dictionary<string, int>();
            result.Add("wins", winsCount);
            result.Add("losses", lossesCount);
            result.Add("breakeven", breakevenCount);

            return result;

        }

        public static Dictionary<string, float> CalculateWinsLossesPercentage(int wins,int losses,int breakevens)
        {

            if (wins < 0 || losses < 0 || breakevens < 0)
            {
                throw new ArgumentException("Wins, losses, and breakevens counts cannot be negative.");
            }

            int wholeSum = wins + losses + breakevens;

            // Check for division by zero
            if (wholeSum == 0)
            {
                throw new ArgumentException("The sum of wins, losses, and breakevens cannot be zero.");
            }

			float winsPercent = (wins * 100) / wholeSum;
			float lossesPercent = (losses * 100) / wholeSum;
			float breakevensPercent = (breakevens * 100) / wholeSum;

			Dictionary<string, float> result = new Dictionary<string, float>();

			result.Add("winsPercent", (float)Math.Round(winsPercent,2));
			result.Add("winsPercent", (float)Math.Round(lossesPercent,2));
			result.Add("winsPercent", (float)Math.Round(breakevensPercent,2));

			return result;

        }

		public static float CalculateCurrentBallance(List<Trade> trades, float startBalance)
		{

			foreach(Trade trade in trades)
			{
				 startBalance += trade.ResultInDollars; 
			}

			return startBalance;
		}


	}
}


using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BacktestAPI.Models
{
	public class Trade
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

		public string Ratio { get; set; } = string.Empty;
		public float EntryInDollars { get; set; }
		public float EntryInPercents { get; set; }
		public float ResultInDollars { get; set; }
		public float ResultInPercents { get; set; }
		public string InstrumentName { get; set; } = string.Empty;
		public int UserId { get; set; }
		public string StrategyName { get; set; } = string.Empty;



	}
}


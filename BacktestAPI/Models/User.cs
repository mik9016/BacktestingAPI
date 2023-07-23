using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BacktestAPI.Models;

namespace BacktestAPI
{
	public class User
	{
        [Key]
        public int Id { get; set; }	

		public string Username { get; set; } = string.Empty;
		public string PasswordHash { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public ICollection<Trade>? Trades { get; set; }


	}
}



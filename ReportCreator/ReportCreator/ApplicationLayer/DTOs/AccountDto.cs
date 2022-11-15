using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.ApplicationLayer.DTOs
{
    public class AccountDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public MoneyDto AccountBalance { get; set; }
        public MoneyDto Overdraft { get; set; }
        public CurrencyNameDto Currency { get; set; }
        public Guid OwnerId { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Account: " + Id + "\n");
            sb.Append("Name: " + Name + "\n");
            sb.Append("Available Funds: " + AccountBalance + Overdraft + "\n");
            sb.Append("Balance: " + AccountBalance + "\n");

            return sb.ToString();
        }
    }
}

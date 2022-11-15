using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.ApplicationLayer.DTOs
{
    public class TransactionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public MoneyDto Value { get; set; }
        public string Type { get; set; }
        public string Frequency { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public Guid BudgetId { get; set; }
        public Guid AccountId { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Transaction: " + Id + "\n");
            sb.Append("Value: " + Value.ToString() + "\n");
            sb.Append("Date: " + Date.ToString("dd/MM/yyyy") + "\n");

            return sb.ToString();
        }
    }
}

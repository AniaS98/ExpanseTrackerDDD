using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.ApplicationLayer.DTOs
{
    public enum TransactionStatusDto
    {
        Upcomming,
        Planned,
        Owed
    }

    public enum TransactionFrequencyDto
    {
        OneTime,
        Reoccuring
    }

    public enum TransactionTypeDto
    {
        Income,
        Expanse,
        Transfer,
        Exchange,
        Borrowed,
        Lent
    }

    public class TransactionDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public TransactionTypeDto Type { get; set; }
        public MoneyDto Value { get; set; }
        public CategoryDto TransactionCategory { get; set; }
        public DateTime TransactionDate { get; set; }
        public TransactionStatusDto Status { get; set; }
        public TransactionFrequencyDto Frequency { get; set; }
        public RecurrencyDto TransactionRecurrency { get; set; }
        public string Note { get; set; }
        public string Contractor { get; set; }
        public Guid AccountId { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Transaction: " + Id + "\n");
            sb.Append("Value: " + Value.ToString() + "\n");
            sb.Append("Date: " + TransactionDate.ToString("dd/MM/yyyy") + "\n");
            sb.Append("Description: " + Description + "\n");

            return sb.ToString();
        }
    }
}

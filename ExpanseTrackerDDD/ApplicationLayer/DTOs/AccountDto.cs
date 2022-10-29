using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.ApplicationLayer.DTOs
{
    public enum AccountTypeDto
    {
        BankAccount,
        Cash,
        SavingsAccount,
        BankAccountWithOverdraft,
        CreditCard,
        Investement,
        Loan
    }

    public class AccountDto
    {
        public Guid Id { get; set; }
        public MoneyDto Balance { get; set; }
        public MoneyDto Overdraft { get; set; }
        public string Name { get; set; }
        public string AccountNumber { get; set; }
        public AccountTypeDto Type { get; set; }
        public CurrencyNameDto Currency { get; set; }
        public Guid UserId { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Account: " + Id + "\n");
            sb.Append("Name: " + Name + "\n");
            sb.Append("Available Funds: " + Balance + Overdraft + "\n");
            sb.Append("Balance: " + Balance + "\n");
            sb.Append("Type: " + Type + "\n");

            return sb.ToString();
        }
    }
}

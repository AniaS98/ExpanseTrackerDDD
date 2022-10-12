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
        public string Name { get; set; }
        public string AccountNumber { get; set; }
        public AccountTypeDto Type { get; set; }
        public CurrencyNameDto Currency { get; set; }
        public string Color { get; set; }
        public Guid UserId { get; set; }
    }
}

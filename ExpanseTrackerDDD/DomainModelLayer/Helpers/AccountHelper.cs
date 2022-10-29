using ExpanseTrackerDDD.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.DomainModelLayer.Helpers
{
    public class AccountHelper
    {
        /// <summary>
        /// Sprawdzenie czy konto może zostać obciążone
        /// </summary>
        /// <param name="account"></param>
        /// <param name="money"></param>
        public static void VerifyAccountBalance(Account account, Money money)
        {
            //Sprawdzenie stanu konta bez limitu debetowego
            if(account.Overdraft == null)
            {
                if (account.Balance.Amount - money.Amount <  0.00m)
                    throw new Exception("Account's balance not suffitient");
            }
            else
            {
                //Sprawdzenie stanu konta z limitem debetowym
                if(account.Balance.Amount + account.Overdraft.Amount - money.Amount < 0.00m)
                    throw new Exception("Account's balance not suffitient");
            }
        }

        /// <summary>
        /// Metoda służy do akutalizacji konta, która nastepuje przy wprowadzaniu zmian w transakcji lub w przypadku usunięciu transakcji
        /// </summary>
        /// <param name="account"></param>
        /// <param name="transaction"></param>
        /// <param name="newValue"></param>
        public static void UpdateAccountAfterUpdateBalance(Account account, Transaction transaction, Money newValue)
        {
            //Obliczanie różnicy w kwocie transakcji i sprawdzenie, czy ta może zostać wykonana
            //VerifyAccountBalance(account, moneyDifference); //tego chyba nie będę używać
            
            //Aktualizacja bilansu
            account.UpdateBalance(newValue);
        }

        /// <summary>
        /// Metoda służy do aktualizacji konta, która następuje w przypadku zupełnie nowej transakcji
        /// </summary>
        /// <param name="account"></param>
        /// <param name="transaction"></param>
        public static void UpdateAccountBalance(Account account, Transaction transaction)
        {
            if (transaction.Type == TransactionType.Income || transaction.Type == TransactionType.Borrowed)
                account.UpdateBalance(transaction.Value);
            else if (transaction.Type == TransactionType.Expanse || transaction.Type == TransactionType.Lent)
                account.UpdateBalance(-transaction.Value);
        }




    }
}

using ExpanseTrackerDDD.ApplicationLayer.DTOs;
using ExpanseTrackerDDD.ApplicationLayer.Mappers;
using ExpanseTrackerDDD.InfrastructureLayer.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpanseTrackerDDD.ApplicationLayer.Queries.Handlers
{
    public class QueryHandler
    {
        private ETContext _context;
        private AccountMapper _accountMapper;
        private UserMapper _userMapper;
        private TransactionMapper _transactionMapper;

        public QueryHandler(ETContext context, AccountMapper accountMapper, UserMapper userMapper, TransactionMapper transactionMapper)
        {
            _context = context;
            _accountMapper = accountMapper;
            _userMapper = userMapper;
            _transactionMapper = transactionMapper;
        }

        #region AccountQueries
       
        public List<AccountDto> Execute(GetAllAccountsQuery query)
        {
            //var accounts = _context.Accounts.AsNoTracking().ToList();
            var accounts = _context.Accounts.ToList();
            return this._accountMapper.Map(accounts);
        }

        public List<AccountDto> Execute(GetAllAccountsOfUserQuery query)
        {
            var accounts = _context.Accounts.Where(a => a.UserId == query.UserId).ToList();
            return this._accountMapper.Map(accounts);
        }

        public AccountDto Execute(GetAccountQuery query)
        {
            var account = _context.Accounts.Where(a => a.Id == query.AccountId).FirstOrDefault();
            return this._accountMapper.Map(account);
        }

        #endregion

        #region UserQueries
        
        public List<UserDto> Execute(GetAllUsersQuery query)
        {
            var users = _context.Users.AsNoTracking().ToList();
            return this._userMapper.Map(users);
        }

        #endregion

        #region TransactionQueries

        public List<TransactionDto> Execute(GetAllUnsettledTransactionsQuery query)
        {
            var transactions = _context.Transactions.Where(t => (int)t.Status == 1).ToList();
            return this._transactionMapper.Map(transactions);
        }

        #endregion

    }
}

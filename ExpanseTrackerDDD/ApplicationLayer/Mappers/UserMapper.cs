using ExpanseTrackerDDD.ApplicationLayer.DTOs;
using ExpanseTrackerDDD.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.ApplicationLayer.Mappers
{
    public class UserMapper
    {
        public List<UserDto> Map(IList<User> users)
        {
            List<UserDto> result = new List<UserDto>();
            foreach (User user in users)
            {
                UserDto userDto = Map(user);
                result.Add(userDto);
            }

            return result;
        }

        public UserDto Map(User user)
        {
            UserDto userDto = new UserDto()
            {
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName,
                Login = user.Login,
                Password = user.Password,
                AccountDtos = new List<AccountDto>(),
                BudgetDtos = new List<BudgetDto>()
            };
            foreach (Account a in user.Accounts)
            {
                userDto.AccountDtos.Add( AccountMapper.Map(a));
            }
            foreach (Budget b in user.Budgets)
            {
                userDto.BudgetDtos.Add(BudgetMapper.Map(b));
            }
            return userDto;
        }



    }
}

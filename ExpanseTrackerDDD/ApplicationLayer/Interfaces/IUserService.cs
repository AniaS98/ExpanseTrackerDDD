using ExpanseTrackerDDD.ApplicationLayer.Services;
using ExpanseTrackerDDD.ApplicationLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using ExpanseTrackerDDD.DomainModelLayer.Models;

namespace ExpanseTrackerDDD.ApplicationLayer.Interfaces
{
    interface IUserService : IApplicationService
    {
        void CreateUser(UserDto userDto);
        User Login(string login, string password);
        void ChangePassword(Guid id, string password, string repeatPassword);
    }
}

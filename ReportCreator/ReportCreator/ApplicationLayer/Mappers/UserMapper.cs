using ReportCreator.ApplicationLayer.DTOs;
using ReportCreator.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.ApplicationLayer.Mappers
{
    public class UserMapper
    {
        public UserDto Map(User user)
        {
            return new UserDto()
            {
                Id = user.Id,
                Status = user.Status
            };

        }
    }
}

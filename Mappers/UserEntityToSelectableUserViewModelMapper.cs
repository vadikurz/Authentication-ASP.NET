using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data.Entities;
using WebApplication.Models;

namespace WebApplication.Mappers
{
    public class UserEntityToSelectableUserViewModelMapper : IMapper<User,SelectableUserViewModel>
    {
        public SelectableUserViewModel Map(User source) =>
            new()
            {
                UserName = source.UserName,
                Email = source.Email,
                Id = source.Id,
                LastAuthorization = source.LastAuthorization,
                RegistrationDate = source.RegistrationDate
            };
    }
}

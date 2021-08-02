using WebApplication.Data.Entities;
using WebApplication.Models;

namespace WebApplication.Mappers
{
    public class UserEntityToSelectableUserViewModelMapper : IMapper<User, SelectableUserViewModel>
    {
        public SelectableUserViewModel Map(User source) =>
            new()
            {
                UserName = source.UserName,
                Email = source.Email,
                Id = source.Id,
                RegisteredAt = source.RegisteredAt,
                LastAuthorizedAt = source.LastAuthorizedAt,
                IsBaned = source.IsBanned
            };
    }
}

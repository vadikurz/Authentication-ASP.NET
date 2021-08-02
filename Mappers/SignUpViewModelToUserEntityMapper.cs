using WebApplication.Data.Entities;
using WebApplication.Models;

namespace WebApplication.Mappers
{
    public class SignUpViewModelToUserEntityMapper : IMapper<SignUpViewModel, User>
    {
        public User Map(SignUpViewModel source) =>
            new()
            {
                UserName = source.UserName,
                Email = source.Email
            };
    }
}

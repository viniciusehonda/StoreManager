using InternalUserService.Domain;

namespace InternalUserService.Application.Abstractions.Authentication;

public interface ITokenProvider
{
    string Create(User user);
}

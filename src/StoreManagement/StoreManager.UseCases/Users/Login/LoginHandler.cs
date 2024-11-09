using Ardalis.Result;
using StoreManagement.Core;
using StoreManager.SharedKernel;

namespace StoreManagement.UseCases;

public class LoginHandler(IRepository<User> _repository)
  : ICommandHandler<LoginCommand, Result<LoginResult>>
{
  public async Task<Result<LoginResult>> Handle(LoginCommand request,
    CancellationToken cancellationToken)
  {
        UserByEmailSpec spec = new UserByEmailSpec(request.Email);
        var entity = await _repository.FirstOrDefaultAsync(spec, cancellationToken);

        if (entity == null) return Result.Invalid(new ValidationError("Invalid Email/Password."));

        if (!entity.Password.Verify(request.Password)) return Result.Invalid(new ValidationError("Invalid Email/Password."));

        return Result.Success(new LoginResult("token"));
    }
}
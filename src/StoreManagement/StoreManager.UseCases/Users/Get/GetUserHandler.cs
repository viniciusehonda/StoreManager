using Ardalis.Result;
using StoreManagement.Core;
using StoreManager.SharedKernel;

namespace StoreManagement.UseCases;
public class GetUserHandler(IReadRepository<User> _repository)
: IQueryHandler<GetUserQuery, Result<UserDTO>>
{
  public async Task<Result<UserDTO>> Handle(GetUserQuery request, CancellationToken cancellationToken)
  {
    UserByIdSpec spec = new UserByIdSpec(request.userId);
    var entity = await _repository.FirstOrDefaultAsync(spec, cancellationToken);

    if (entity == null) return Result.NotFound();

    return new UserDTO(entity.Id, entity.FirstName, entity.LastName, entity.Email);
  }
}

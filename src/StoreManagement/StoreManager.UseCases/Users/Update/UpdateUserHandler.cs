using Ardalis.Result;
using StoreManagement.Core;
using StoreManager.SharedKernel;
namespace StoreManagement.UseCases;

public class UpdateUserHandler(IRepository<User> _repository)
: ICommandHandler<UpdateUserCommand, Result<UserDTO>>
{
    public async Task<Result<UserDTO>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _repository.GetByIdAsync(request.userId, cancellationToken);

        if (existingUser == null)
        {
            return Result.NotFound();
        }

        existingUser.UpdateFirstName(request.newFirstName!);
        existingUser.UpdateLastName(request.newLastName);
        existingUser.UpdateEmail(request.newEmail);

        await _repository.UpdateAsync(existingUser, cancellationToken);

        return Result.Success(new UserDTO(existingUser.Id,
        existingUser.FirstName, existingUser.LastName, existingUser.Email));
    }
}

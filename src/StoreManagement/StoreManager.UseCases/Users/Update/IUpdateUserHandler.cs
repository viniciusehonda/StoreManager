using Ardalis.Result;

namespace StoreManagement.UseCases
{
    public interface IUpdateUserHandler
    {
        Task<Result<UserDTO>> Handle(UpdateUserCommand request, CancellationToken cancellationToken);
    }
}
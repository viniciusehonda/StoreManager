using System.Windows.Input;
using Ardalis.Result;
using StoreManager.SharedKernel;

namespace StoreManagement.UseCases;

public record UpdateUserCommand(Guid userId, string newFirstName, string newLastName, string newEmail) : ICommand<Result<UserDTO>>;

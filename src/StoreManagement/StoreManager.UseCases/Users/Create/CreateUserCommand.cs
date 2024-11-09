using Ardalis.Result;
using StoreManager.SharedKernel;

namespace StoreManagement.UseCases;

/// <summary>
/// Create a new Contributor.
/// </summary>
/// <param name="Name"></param>
public record CreateUserCommand(string FirstName, string LastName, string Email, string Password) : ICommand<Result<Guid>>;
using Ardalis.Result;
using StoreManager.SharedKernel;

namespace StoreManagement.UseCases;

/// <summary>
/// Log in user.
/// </summary>
/// <param name="Email"></param>
public record LoginCommand(string Email, string Password) : ICommand<Result<LoginResult>>;
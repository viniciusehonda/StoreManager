using Ardalis.Result;
using StoreManager.SharedKernel;

namespace StoreManagement.UseCases;

public record GetUserQuery(Guid userId) : IQuery<Result<UserDTO>>;

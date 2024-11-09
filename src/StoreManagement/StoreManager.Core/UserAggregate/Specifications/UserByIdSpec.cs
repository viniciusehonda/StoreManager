using Ardalis.Specification;

namespace StoreManagement.Core;

public class UserByIdSpec : Specification<User>
{
    public UserByIdSpec(Guid userId)
    {
        Query
            .Where(user => user.Id == userId);
    }
}

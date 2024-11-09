using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Core;

public class UserByEmailSpec : Specification<User>
{
    public UserByEmailSpec(string email)
    {
        Query
            .Where(user => user.Email == email);
    }
}

using SharedKernel;

namespace InternalUserService.Domain;

public class User : Entity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public bool IsActive { get; private set; } = true;

    public UserRole Role { get; private set; } = UserRole.Manager;
}
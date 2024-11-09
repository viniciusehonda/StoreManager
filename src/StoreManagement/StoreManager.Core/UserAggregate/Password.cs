using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using StoreManager.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Core;

public class Password(string passwordHash, string passwordSalt) : ValueObject
{
    public string PasswordHash { get; private set; } = passwordHash;
    public string PasswordSalt { get; private set; } = passwordSalt;

    public void HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password!,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));

        PasswordHash = hashed;
        PasswordSalt = Convert.ToBase64String(salt);
    }

    public bool Verify(string passwordInput)
    {
        var hashInput = KeyDerivation.Pbkdf2(
            password: passwordInput!,
            salt: Convert.FromBase64String(PasswordSalt),
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8);

        return CryptographicOperations.FixedTimeEquals(Convert.FromBase64String(PasswordHash),
         hashInput);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return PasswordHash;
    }
}

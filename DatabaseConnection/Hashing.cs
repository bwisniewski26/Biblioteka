using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace ProjektZaliczeniowyPR3.DatabaseConnection;



public class Hashing 
{
    public string GetHash(string Password, byte[] salt)
    {
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            salt: salt,
            password: Password,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
        return hashed;
    }

    public byte[] GetSalt()
    {
        byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
        return salt;
    }
}
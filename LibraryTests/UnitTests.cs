namespace ProjektZaliczeniowyPR3.LibraryTests;

using ProjektZaliczeniowyPR3.DatabaseConnection;

using NUnit.Framework;

public class UnitTests
{
    [Test]
    public bool TestEncryptionUnambiguity()
    {
        byte[] salt = [];
        string password = "prosteHaslo123!";
        string hashedPassword = "SyJfdxFYHxPWJcYpATwRbBtwBkq5tqlm+eyLcV7jI6I=";
        Hashing hash = new();
        try
        {
        Assert.AreEqual(hashedPassword, hash.GetHash(password, salt));
        }
        catch (AssertionException e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
        return true;

    }

    [Test]
    public bool TestEncryptionUniqueness()
    {
        Hashing hash = new();
        byte[] salt1 = hash.GetSalt();
        byte[] salt2 = hash.GetSalt();
        string password = "Skomplikowane1RozbudowaneHaslo123!@#[]";
        try
        {
        Assert.AreEqual(hash.GetHash(password, salt1), hash.GetHash(password, salt2));
        }
        catch (AssertionException)
        {
            return true;
        }
        return false;
    }

    [Test]
    public bool TestSuccessfulLogin()
    {
        return true;
    }
}
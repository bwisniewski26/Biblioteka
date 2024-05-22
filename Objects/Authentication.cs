using ProjektZaliczeniowyPR3.Data;
using ProjektZaliczeniowyPR3.DatabaseConnection;
using ProjektZaliczeniowyPR3.Models;
using ConnectionInfo = ProjektZaliczeniowyPR3.DatabaseConnection.ConnectionInfo;

namespace ProjektZaliczeniowyPR3.Objects;


public class Authentication
{
    
    public LoginStatus Login(string username, string password)
    {
        ConnectionInfo info = new();
        Hashing hash = new();
        if (!info.TryConnection())
        {
            return LoginStatus.ConnectionFailed;
        }

        using (var db = new LibraryContext())
        {
            try
            {
                User user = db.users.Single(user => user.Username == username);
                string hashedPassword = hash.GetHash(password, user.salt);
                if (hashedPassword == user.Password)
                {
                    return LoginStatus.LoginSuccessful;
                } 
                return LoginStatus.PasswordIncorrect;
            }
            catch (Exception e)
            {
                return LoginStatus.UserNotFound;
            }
        }
    }

    public User RetrieveUserInformation(string username)
    {
        ConnectionInfo info = new();
        User notFound = new();
        notFound.Id = -1;
        
        if (!info.TryConnection())
        {
            return notFound;
        }

        using (var db = new LibraryContext())
        {
            User user = db.users.Single(user => user.Username == username);
            return user;
        }
    }
}
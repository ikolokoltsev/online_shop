using static IOUtilsApp.IOUtils;

namespace UserApp;

class User
{
    public string Username;
    public string Email;
    string _password;

    public User(string username, string email, string password)
    {
        Username = username;
        Email = email;
        _password = password;
    }

    public bool HasEmailAlreadyUsed(string email, List<User> users)
    {
        return users.Any(user => user.Email == email);
    }
    
    public bool HasUsernameAlreadyUsed(string email, List<User> users)
    {
        return users.Any(user => user.Email == email);
    }

    public bool HasUserExists(string userIdentity, List<User> users)
    {
        return users.Any(user => user.Username == userIdentity || user.Email == userIdentity);
    }

    // TODO: Remove it from here to somewhere better place
    public bool isAuthorized(string username)
    {
        return Username == username;
    }

    public bool TryLogIn(string userIdentity, string password, List<User> users)
    {
        if (HasUserExists(userIdentity, users))
        {
            User user = users.Find(user => user.Username == userIdentity || user.Email == userIdentity);
            return user._password == password;
        }

        return false;
    }

    // TODO: Should I include this here?
    public void CreateUser(string username, string email, string password, List<User> users)
    {
        if (HasUserExists(username, users))
        {
            ColorizedPrint("You can't create user with this credentials", ConsoleColor.Red);
        }
        else
        {
            users.Add(new User(username, email, password));
            ColorizedPrint("User created", ConsoleColor.Green);
        }
    }

    public void PrintThing()
    {
        Console.WriteLine("thing");
    }
}
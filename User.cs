using static IOUtilsApp.IOUtils;

namespace UserApp;

class User
{
    public readonly Guid Id = Guid.NewGuid();
    public string Username;
    public string Email;
    public string Password;

    public User(string username, string email, string password)
    {
        Username = username;
        Email = email;
        Password = password;
    }

    bool HasUserExists(string userIdentity, List<User> users)
    {
        return users.Any(user => user.Username == userIdentity || user.Email == userIdentity);
    }

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
    
}
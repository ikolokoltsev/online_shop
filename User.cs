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

    public bool HasUserExists(string userIdentity, List<User> users)
    {
        return users.Any(user =>
            user.Username.Equals(userIdentity, StringComparison.OrdinalIgnoreCase) ||
            user.Email.Equals(userIdentity, StringComparison.OrdinalIgnoreCase));
    }
    

    public bool TryLogIn(string userIdentity, string password)
    {
        return (Username.Equals(userIdentity, StringComparison.OrdinalIgnoreCase) ||
                Email.Equals(userIdentity, StringComparison.OrdinalIgnoreCase)) && _password == password;
    }

    public static void SaveUsersToFile(List<User> users, string fileName)
    {
        var users_lines = users.Select(user => $"{user.Username},{user.Email},{user._password}");
        File.WriteAllLines(fileName, users_lines);
    }

    public static void SavveUserToFile(User user, string filePath)
    {
        File.AppendAllLines(filePath, new []{$"{user.Username},{user.Email},{user._password}"});
    }
}
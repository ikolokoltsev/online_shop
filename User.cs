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

    // TODO: Remove it from here to somewhere better place
    public bool isAuthorized(string username)
    {
        return Username == username;
    }

    public bool TryLogIn(string userIdentity, string password)
    {
        return (Username.Equals(userIdentity, StringComparison.OrdinalIgnoreCase) ||
                Email.Equals(userIdentity, StringComparison.OrdinalIgnoreCase)) && _password == password;
    }
}
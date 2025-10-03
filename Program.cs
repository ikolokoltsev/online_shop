using UserApp;
using SessionApp;
using static IOUtilsApp.IOUtils;

bool is_main_menu = true;
List<User> users = new List<User>();

Session session = new Session(Guid.Empty);

while (is_main_menu)
{
    ColorizedPrint("Welcome to the Best Shop Ever!");
    if (session.isAuthorized())
    {
        // AuthorizedMenu
        Console.WriteLine("Authorized!");
        break;
    }
    else
    {
        // UnauthorizedMenu
        Console.WriteLine("Not authorized!");
        bool is_unauthorized_menu = true;
        while (is_unauthorized_menu)
        {
            Console.WriteLine("Please select an option:");
            Console.WriteLine("1. Log in");
            Console.WriteLine("2. Sign up");
            string unauthorized_menu_input = StringUserInput();
            switch (unauthorized_menu_input)
            {
                case "1":
                case "Log in":
                    session.TryLogIn("", "", users);
                    break;
            }
        }
    }
}
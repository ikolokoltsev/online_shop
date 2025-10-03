using UserApp;
using ItemApp;
using static IOUtilsApp.IOUtils;

bool is_main_menu = true;
List<User> users = new List<User>();
users.Add(new User("admin", "admin@mail.com", "adMin"));
List<Item> items = new List<Item>();
items.Add(new Item("Ball", "it's a ball! 🤷🏻‍♂️", 13.5, "admin"));
items.Add(new Item("Table", null, 13.5, "admin"));
User? active_user = null;


while (is_main_menu)
{
    ColorizedPrint("Welcome to the Best Shop Ever!");
    if (active_user != null)
    {
        bool is_authorized_menu = true;
        while (is_authorized_menu)
        {
            // AuthorizedMenu
            ColorizedPrint("Authorized!");
            ColorizedPrint($"Hello {active_user.Username}");
            ColorizedPrint("Please select an option:");
            ColorizedPrint("1. Add new item");
            ColorizedPrint("2. Brows all items");
            ColorizedPrint("3. Logout");
            string authorized_menu_input = StringUserInput();
            switch (authorized_menu_input)
            {
                case "1":
                case "Add new item":
                    ColorizedPrint("Enter the name of the item:");
                    string add_item_name_input = StringUserInput();
                    ColorizedPrint("Enter the description of the item:");
                    string add_item_description_input = StringUserInput();
                    ColorizedPrint("Enter the price of the item:");
                    string add_item_price_input = StringUserInput();
                    Double.TryParse(add_item_price_input, out double add_item_price);
                    items.Add(new Item(add_item_name_input, add_item_description_input, add_item_price,
                        active_user.Username));
                    break;
                case "2":
                case "Brows all items":
                    ColorizedPrint("-----------------------------", ConsoleColor.DarkCyan);
                    foreach (Item item in items)
                    {
                        if (item.BelongsTo != active_user.Username)
                        {
                            ColorizedPrint($"The {item.Name} with a description below");
                            string description_to_display = item.Description != null && item.Description.Length > 0
                                ? item.Description
                                : "";
                            ColorizedPrint(description_to_display);
                            ColorizedPrint($"Belongs to {item.BelongsTo}");
                            ColorizedPrint("-----------------------------", ConsoleColor.DarkGray);
                        }
                    }

                    ColorizedPrint("-----------------------------", ConsoleColor.DarkCyan);
                    ColorizedPrint("Press any key to exit...");
                    Console.ReadLine();

                    break;
                case "3":
                case "Logout":
                    ColorizedPrint("Login out");
                    active_user = null;
                    is_authorized_menu = false;
                    break;
            }
        }
    }
    else
    {
        // UnauthorizedMenu
        ColorizedPrint("Not authorized!", ConsoleColor.Yellow);
        bool is_unauthorized_menu = true;
        while (is_unauthorized_menu)
        {
            ColorizedPrint("Please select an option:");
            ColorizedPrint("1. Log in");
            ColorizedPrint("2. Sign up");
            string unauthorized_menu_input = StringUserInput();
            switch (unauthorized_menu_input)
            {
                case "1":
                case "Log in":
                    ColorizedPrint("Please enter your username or an email:");
                    string login_username_input = StringUserInput();
                    ColorizedPrint("Please enter your password:");
                    string login_password_input = StringUserInput();
                    foreach (var user in users)
                    {
                        bool isAuthenticated = user.TryLogIn(login_username_input, login_password_input, users);
                        if (isAuthenticated)
                        {
                            active_user = user;
                            is_unauthorized_menu = false;
                        }
                        else
                        {
                            ColorizedPrint("Wrong username or password!", ConsoleColor.DarkRed);
                        }
                    }

                    break;
                case "2":
                case "Sign up":
                    ColorizedPrint("Enter your username:");
                    string signup_username_input = StringUserInput();
                    bool is_username_valid = true;
                    foreach (User user in users)
                    {
                        bool user_exists = user.HasUserExists(signup_username_input, users);
                        if (user_exists)
                        {
                            ColorizedPrint("This username already taken!", ConsoleColor.DarkRed);
                            is_username_valid = false;
                        }
                    }

                    if (is_username_valid)
                    {
                        ColorizedPrint("Enter your email:");
                        string signup_email_input = StringUserInput();
                        ColorizedPrint("Enter your password:");
                        string signup_password_input = StringUserInput();
                        users.Add(new User(signup_username_input, signup_email_input, signup_password_input));
                    }

                    break;
            }
        }
    }
}
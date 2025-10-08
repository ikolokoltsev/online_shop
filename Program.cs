using UserApp;
using ItemApp;
using TradeOfferApp;
using static IOUtilsApp.IOUtils;


static void FileSystemInit(List<User> users, List<Item> items, List<TradeOffer> trade_offers)
{
    ColorizedPrint("File system initializing!", ConsoleColor.DarkBlue);
    if (File.Exists("users.csv"))
    {
        ColorizedPrint("Users file exists!", ConsoleColor.DarkGreen);
        string[] persisted_users = File.ReadAllLines("users.csv");
        foreach (string persisted_user in persisted_users)
        {
            string[] parsed_persisted_user = persisted_user.Split(",");
            users.Add(new User(parsed_persisted_user[0], parsed_persisted_user[1], (parsed_persisted_user[2])));
        }
    }
    else
    {
        ColorizedPrint("Users file doesn't exist!", ConsoleColor.DarkYellow);
        users.Add(new User("admin", "admin@gmail.com", "adMin"));
        users.Add(new User("JoshDub", "joshD@gmail.com", "JoshDub"));
        users.Add(new User("Leny", "mistertwister@gmail.com", "LolLul"));
        users.Add(new User("Marcus", "rick@gmail.com", "rock"));
        users.Add(new User("Robert", "lookandtook@gmail.com", "What"));
        ColorizedPrint("Default users initialized!", ConsoleColor.DarkGreen);
        User.SaveUsersToFile(users, "users.csv");
        ColorizedPrint("Users file created!", ConsoleColor.DarkGreen);
    }

    if (File.Exists("items.csv"))
    {
        ColorizedPrint("Items file exists!", ConsoleColor.DarkGreen);
        string[] persisted_items = File.ReadAllLines("items.csv");
        foreach (string persisted_item in persisted_items)
        {
            string[] parsed_persisted_item = persisted_item.Split(",");
            items.Add(new Item(parsed_persisted_item[0], parsed_persisted_item[1], (parsed_persisted_item[2])));
        }
    }
    else
    {
        ColorizedPrint("Items file doesn't exist!", ConsoleColor.DarkYellow);
        items.Add(new Item("Ball", "it's a ball! 🤷🏻‍♂️", "admin"));
        items.Add(new Item("Table", null, "admin"));
        items.Add(new Item("Cahir", null, "Leny"));
        items.Add(new Item("Bun", null, "JoshDub"));
        items.Add(new Item("Bike", null, "JoshDub"));
        items.Add(new Item("Shoe", null, "JoshDub"));
        items.Add(new Item("Sandwich", null, "Marcus"));
        items.Add(new Item("Rock", null, "Robert"));
        ColorizedPrint("Default items initialized!", ConsoleColor.DarkGreen);
        Item.SaveItemsToFile(items, "items.csv");
        ColorizedPrint("Items file created!", ConsoleColor.DarkGreen);
    }

    if (File.Exists("trade_offers.csv"))
    {
        ColorizedPrint("Trade offers file exists!", ConsoleColor.DarkGreen);
        string[] persisted_trade_offers = File.ReadAllLines("trade_offers.csv");
        foreach (string persisted_trade_offer in persisted_trade_offers)
        {
            string[] parsed_persisted_trade_offer = persisted_trade_offer.Split(",");
            User offer_from = users.Find(user => user.Username == parsed_persisted_trade_offer[0]);
            User offer_to = users.Find(user => user.Username == parsed_persisted_trade_offer[1]);
            List<Item> itemsToTrade = parsed_persisted_trade_offer[3].Split("|")
                .Select(itemId => items.Find(item => item.ItemId == itemId)).ToList();
            TradeOfferStatus status = Enum.Parse<TradeOfferStatus>(parsed_persisted_trade_offer[2]); 
            trade_offers.Add(new TradeOffer(offer_from, offer_to, itemsToTrade, status));
        }
    }
    else
    {
        ColorizedPrint("Trade offers file doesn't exist!", ConsoleColor.DarkYellow);
        File.Create("trade_offers.csv").Close();
        ColorizedPrint("Trade offers file created!", ConsoleColor.DarkGreen);
    }
}

List<User> users = new List<User>();
List<Item> items = new List<Item>();
List<TradeOffer> trade_offers = new List<TradeOffer>();

FileSystemInit(users, items, trade_offers);

bool is_main_menu = true;

User? active_user = null;

while (is_main_menu)
{
    ColorizedPrint("Welcome to the Best Shop Ever!");
    if (active_user != null)
    {
        bool is_authorized_menu = true;
        while (is_authorized_menu)
        {
            Console.Clear();
            ColorizedPrint("Authorized!");
            ColorizedPrint("Please select an option:");
            ColorizedPrint("1. Add new item");
            ColorizedPrint("2. Browse other users' items");
            ColorizedPrint("3. Browse my trade offers");
            ColorizedPrint("4. Browse income trade offers");
            ColorizedPrint("5. Browse completed requests");
            ColorizedPrint("6. Logout");
            string authorized_menu_input = StringUserInput();
            switch (authorized_menu_input)
            {
                case "1":
                case "Add new item":
                    Console.Clear();
                    ColorizedPrint("Enter the name of the item:");
                    string add_item_name_input = StringUserInput();
                    ColorizedPrint("Enter the description of the item:");
                    string add_item_description_input = StringUserInput();

                    Item new_item = new Item(add_item_name_input, add_item_description_input,
                        active_user.Username);
                    items.Add(new_item);
                    Item.SaveItemToFile(new_item, "items.csv");
                    break;
                case "2":
                case "Browse other users' items":
                {
                    Console.Clear();
                    ColorizedPrint("-----------------------------", ConsoleColor.DarkCyan);
                    Dictionary<int, Item> users_items_dict = new Dictionary<int, Item>();
                    int users_items_index = 0;
                    foreach (Item item in items)
                    {
                        if (item.BelongsTo != active_user.Username)
                        {
                            ColorizedPrint($"{users_items_index}. {item.Name}");
                            string description_to_display = item.Description != null && item.Description.Length > 0
                                ? item.Description
                                : "-";
                            ColorizedPrint($"Description: {description_to_display}");
                            ColorizedPrint($"Belongs to: {item.BelongsTo}");
                            ColorizedPrint("-----------------------------", ConsoleColor.DarkGray);

                            users_items_dict.Add(users_items_index, item);
                            users_items_index++;
                        }
                    }

                    if (users_items_dict.Count == 0)
                    {
                        ColorizedPrint("No items from other users available.", ConsoleColor.Yellow);
                        break;
                    }

                    bool is_create_offer_menu = true;
                    while (is_create_offer_menu)
                    {
                        ColorizedPrint("-----------------------------", ConsoleColor.DarkCyan);
                        ColorizedPrint("1. Create a trade offer");
                        ColorizedPrint("2. Back to menu");
                        string create_trade_offer_menu_input = StringUserInput();

                        switch (create_trade_offer_menu_input)
                        {
                            case "1":
                            case "Create a trade offer":
                            {
                                ColorizedPrint("Enter username or item index of the person you want to trade with:");
                                string offer_to_user_input = StringUserInput();

                                User offer_to_user = null;
                                if (int.TryParse(offer_to_user_input, out int parsed_offer_to_user_input) &&
                                    users_items_dict.ContainsKey(parsed_offer_to_user_input))
                                {
                                    offer_to_user = users.Find(u =>
                                        u.Username == users_items_dict[parsed_offer_to_user_input].BelongsTo);
                                }
                                else
                                {
                                    offer_to_user = users.Find(user =>
                                        user.Username.Equals(offer_to_user_input, StringComparison.OrdinalIgnoreCase));
                                }

                                if (offer_to_user == null)
                                {
                                    ColorizedPrint("User not found!", ConsoleColor.DarkRed);
                                    break;
                                }

                                List<Item> offer_to_user_items =
                                    items.FindAll(item => item.BelongsTo == offer_to_user.Username);
                                if (offer_to_user_items.Count == 0)
                                {
                                    ColorizedPrint($"{offer_to_user.Username} has no items.", ConsoleColor.Yellow);
                                    break;
                                }

                                ColorizedPrint($"Items owned by {offer_to_user.Username}:", ConsoleColor.DarkCyan);
                                int offer_to_user_item_index = 0;
                                foreach (Item offer_to_user_item in offer_to_user_items)
                                {
                                    ColorizedPrint($"{offer_to_user_item_index}. {offer_to_user_item.Name}");
                                    offer_to_user_item_index++;
                                }

                                ColorizedPrint("Enter comma-separated indices of items you want to receive:");
                                string wanted_items_input = StringUserInput();
                                List<string> wanted_indexes = wanted_items_input.Split(',')
                                    .Select(selected_index => selected_index.Trim()).ToList();
                                List<Item> wanted_items = new List<Item>();

                                foreach (string wanted_item_index in wanted_indexes)
                                {
                                    int parsed_wanted_item_index;
                                    try
                                    {
                                        parsed_wanted_item_index = int.Parse(wanted_item_index);
                                        wanted_items.Add(offer_to_user_items[parsed_wanted_item_index]);
                                    }
                                    catch
                                    {
                                        ColorizedPrint($"{wanted_item_index} is not a valid number!",
                                            ConsoleColor.DarkRed);
                                    }
                                }

                                if (wanted_items.Count == 0)
                                {
                                    ColorizedPrint("You didn't like anything?", ConsoleColor.DarkRed);
                                    break;
                                }

                                List<Item> my_items = items.FindAll(item => item.BelongsTo == active_user.Username);
                                if (my_items.Count == 0)
                                {
                                    ColorizedPrint("You have no items to offer.", ConsoleColor.Yellow);
                                    break;
                                }

                                ColorizedPrint("Your items available for trade:", ConsoleColor.DarkCyan);
                                int my_item_index = 0;
                                foreach (Item my_item in my_items)
                                {
                                    ColorizedPrint($"{my_item_index}. {my_item.Name}");
                                    my_item_index++;
                                }

                                ColorizedPrint("Enter comma-separated indices of your items to offer:");
                                string offer_input = StringUserInput();
                                List<string> offer_indices = offer_input.Split(',')
                                    .Select(selected_index => selected_index.Trim()).ToList();
                                List<Item> offered_items = new List<Item>();

                                foreach (string offered_item_index in offer_indices)
                                {
                                    int parsed_offered_item_index;
                                    try
                                    {
                                        parsed_offered_item_index = int.Parse(offered_item_index);
                                        offered_items.Add(my_items[parsed_offered_item_index]);
                                    }
                                    catch
                                    {
                                        ColorizedPrint($"{offered_item_index} is not a valid number!",
                                            ConsoleColor.DarkRed);
                                    }
                                }

                                if (offered_items.Count == 0)
                                {
                                    ColorizedPrint("No items selected to offer.", ConsoleColor.DarkRed);
                                    break;
                                }

                                ColorizedPrint("-----------------------------", ConsoleColor.DarkCyan);
                                ColorizedPrint($"You are offering to {offer_to_user.Username}:");
                                foreach (var offered_item in offered_items)
                                    ColorizedPrint($"- {offered_item.Name}", ConsoleColor.Gray);

                                ColorizedPrint("In exchange for:");
                                foreach (var wanted_item in wanted_items)
                                    ColorizedPrint($"- {wanted_item.Name}", ConsoleColor.Gray);

                                ColorizedPrint("-----------------------------", ConsoleColor.DarkCyan);
                                ColorizedPrint("1. Confirm offer");
                                ColorizedPrint("2. Cancel");
                                string confirm_input = StringUserInput();

                                switch (confirm_input)
                                {
                                    case "1":
                                    case "Confirm offer":
                                    {
                                        if (active_user.Username == "Robert" && offer_to_user.Username == "Marcus" &&
                                            offered_items.Any(item => item.Name == "Rock"))
                                        {
                                            ColorizedPrint("Congratulations! You have managed to find an easter egg");
                                            ColorizedPrint("Now the dialogue will be shown");
                                            ColorizedPrint("Please, press any key after each row.");
                                            ColorizedPrint("And press any key to start the dialogue. Enjoy!");
                                            Console.ReadKey();
                                            Console.Clear();
                                            ColorizedPrint("Marcus: Robert I don't like this rock");
                                            Console.ReadKey();
                                            ColorizedPrint("Robert: Oh, sorry. I don't like this rock eather");
                                            Console.ReadKey();
                                            ColorizedPrint("Marcus: Robert!");
                                            Console.ReadKey();
                                            ColorizedPrint("Robert: Yes?");
                                            Console.ReadKey();
                                            ColorizedPrint("Marcus: It's pissing me of!");
                                            ColorizedPrint("Marcus don't like this rock, it's pissing him of");
                                            ColorizedPrint("No deal!");
                                            ColorizedPrint("Trade offer won't be created.");
                                            ColorizedPrint("Press any key to exit...");
                                            Console.ReadKey();
                                        }
                                        else
                                        {
                                            string offer_id = $"{active_user.Username}{offer_to_user.Username}";

                                            bool is_trade_offer_exists = trade_offers.Count > 0 &&
                                                                         trade_offers.Any(to =>
                                                                             to.OfferId == offer_id &&
                                                                             to.Status == TradeOfferStatus.Pending);

                                            if (is_trade_offer_exists)
                                            {
                                                ColorizedPrint("A pending offer already exists with this user.",
                                                    ConsoleColor.DarkRed);
                                            }
                                            else
                                            {
                                                List<Item> combined_items = new List<Item>();
                                                combined_items.AddRange(wanted_items);
                                                combined_items.AddRange(offered_items);
                                                TradeOffer new_trade_offer = new TradeOffer(active_user, offer_to_user,
                                                    combined_items);
                                                trade_offers.Add(new_trade_offer);
                                                TradeOffer.SaveTradeOfferToFile(new_trade_offer, "trade_offers.csv");
                                                ColorizedPrint("Trade offer created successfully!", ConsoleColor.Green);
                                                ColorizedPrint("Press any key to exit...");
                                                Console.ReadLine();
                                            }
                                        }

                                        break;
                                    }

                                    case "2":
                                    case "Cancel":
                                        ColorizedPrint("Trade offer creation canceled.", ConsoleColor.Yellow);
                                        ColorizedPrint("Press any key to exit...");
                                        Console.ReadLine();
                                        is_create_offer_menu = false;
                                        break;
                                }

                                Console.ReadLine();
                                break;
                            }

                            case "2":
                            case "Back to menu":
                                is_create_offer_menu = false;
                                break;

                            default:
                                ColorizedPrint("Invalid input! Try again.", ConsoleColor.DarkRed);
                                break;
                        }
                    }

                    break;
                }

                case "3":
                case "Browse my trade offers":
                    Console.Clear();
                    if (trade_offers.Count > 0)
                    {
                        List<TradeOffer> my_trade_offers = trade_offers.FindAll(trade_offer =>
                            trade_offer.OfferFrom.Username == active_user.Username);
                        if (my_trade_offers.Count > 0)
                        {
                            Dictionary<int, TradeOffer> my_offers_dict = new Dictionary<int, TradeOffer>();
                            int my_offer_index = 0;
                            ColorizedPrint("-----------------------------", ConsoleColor.DarkCyan);
                            foreach (TradeOffer trade_offer in my_trade_offers)
                            {
                                ColorizedPrint(
                                    $"You had sent an offer with id {my_offer_index} to {trade_offer.OfferTo.Username}");
                                ColorizedPrint("Why in the love of God those items interested you?");
                                foreach (Item item_to_trade in trade_offer.ItemsToTrade)
                                {
                                    if (item_to_trade.BelongsTo != active_user.Username)
                                    {
                                        ColorizedPrint(item_to_trade.Name);
                                    }
                                }

                                ColorizedPrint("-----------------------------", ConsoleColor.DarkGray);

                                ColorizedPrint("And you decided that it ill be goo idea,");
                                ColorizedPrint("tp offer this priceless treasurers?");
                                foreach (Item item_to_trade in trade_offer.ItemsToTrade)
                                {
                                    if (item_to_trade.BelongsTo == active_user.Username)
                                    {
                                        ColorizedPrint(item_to_trade.Name);
                                    }
                                }

                                my_offers_dict.Add(my_offer_index, trade_offer);
                                my_offer_index++;
                            }

                            ColorizedPrint("-----------------------------", ConsoleColor.DarkCyan);
                        }
                    }
                    else
                    {
                        ColorizedPrint("I don't want to upset you, but there are no trade offers yet.");
                    }

                    ColorizedPrint("Press any key to exit...");
                    Console.ReadLine();
                    break;
                case "4":
                case "Browse income trade offers":

                    Console.Clear();
                    if (trade_offers.Count == 0)
                    {
                        ColorizedPrint("No trade offers available yet.", ConsoleColor.Yellow);
                        ColorizedPrint("Press any key to exit...");
                        Console.ReadLine();
                        break;
                    }

                    List<TradeOffer> income_trade_offers = trade_offers
                        .FindAll(to =>
                            to.OfferTo.Username == active_user.Username && to.Status == TradeOfferStatus.Pending);

                    if (income_trade_offers.Count == 0)
                    {
                        ColorizedPrint("You have no incoming trade offers.", ConsoleColor.Yellow);
                        ColorizedPrint("Press any key to exit...");
                        Console.ReadLine();
                        break;
                    }

                    Dictionary<int, TradeOffer> income_offers_dict = new Dictionary<int, TradeOffer>();

                    ColorizedPrint("-----------------------------", ConsoleColor.DarkCyan);
                    int income_trade_offers_index = 0;
                    foreach (TradeOffer income_trade_offer in income_trade_offers)
                    {
                        TradeOffer offer = income_trade_offers[income_trade_offers_index];
                        ColorizedPrint($"{income_trade_offers_index}. Offer from {offer.OfferFrom.Username}");
                        ColorizedPrint($"Offer ID: {offer.OfferId}");
                        ColorizedPrint("-----------------------------", ConsoleColor.DarkGray);
                        income_offers_dict.Add(income_trade_offers_index, offer);
                        income_trade_offers_index++;
                    }

                    ColorizedPrint("Enter index of offer to view, or press Enter to go back:");
                    string offer_index_input = StringUserInput();

                    if (!int.TryParse(offer_index_input, out int offer_index) ||
                        !income_offers_dict.ContainsKey(offer_index))
                    {
                        ColorizedPrint("Invalid input! Try again.", ConsoleColor.DarkRed);
                        break;
                    }

                    TradeOffer selected_offer = income_offers_dict[offer_index];

                    List<Item> offered_to_you = selected_offer.ItemsToTrade
                        .FindAll(item => item.BelongsTo == selected_offer.OfferFrom.Username);
                    List<Item> requested_from_you = selected_offer.ItemsToTrade
                        .FindAll(item => item.BelongsTo == selected_offer.OfferTo.Username);

                    ColorizedPrint("-----------------------------", ConsoleColor.DarkCyan);
                    ColorizedPrint($"Offer from: {selected_offer.OfferFrom.Username}", ConsoleColor.Cyan);

                    ColorizedPrint("They want these items from you:", ConsoleColor.Yellow);
                    foreach (var item in requested_from_you)
                        ColorizedPrint($"- {item.Name}");

                    ColorizedPrint("They offer you these items in return:", ConsoleColor.Green);
                    foreach (var item in offered_to_you)
                        ColorizedPrint($"- {item.Name}");

                    ColorizedPrint("-----------------------------", ConsoleColor.DarkCyan);
                    ColorizedPrint("1. Accept offer");
                    ColorizedPrint("2. Deny offer");
                    ColorizedPrint("3. Back");

                    string action_input = StringUserInput();

                    switch (action_input)
                    {
                        case "1":
                        case "Accept offer":
                            foreach (var item in requested_from_you)
                                item.BelongsTo = selected_offer.OfferFrom.Username;

                            foreach (var item in offered_to_you)
                                item.BelongsTo = selected_offer.OfferTo.Username;

                            selected_offer.Status = TradeOfferStatus.Accepted;

                            ColorizedPrint("Trade offer accepted! Items have been exchanged.", ConsoleColor.Green);
                            break;

                        case "2":
                        case "Deny offer":
                            selected_offer.Status = TradeOfferStatus.Denied;
                            ColorizedPrint("Trade offer denied.", ConsoleColor.DarkRed);
                            break;

                        default:
                            ColorizedPrint("Back to menu.", ConsoleColor.Gray);
                            break;
                    }

                    Console.ReadLine();
                    break;

                case "5":
                case "Browse completed requests":
                    Console.Clear();
                    if (trade_offers.Count > 0)
                    {
                        List<TradeOffer> completed_trade_offers =
                            trade_offers.FindAll(trade_offer => trade_offer.Status != TradeOfferStatus.Pending);
                        if (completed_trade_offers.Count > 0)
                        {
                            List<TradeOffer> my_completed_trade_offers = completed_trade_offers.FindAll(trade_offer =>
                                trade_offer.OfferFrom.Username == active_user.Username);
                            List<TradeOffer> incoming_completed_trade_offers =
                                completed_trade_offers.FindAll(trade_offer =>
                                    trade_offer.OfferTo.Username == active_user.Username);
                            if (my_completed_trade_offers.Count > 0)
                            {
                                foreach (TradeOffer my_completed_trade_offer in my_completed_trade_offers)
                                {
                                    List<Item> offered_items =
                                        my_completed_trade_offer.ItemsToTrade.FindAll(item =>
                                            item.BelongsTo == active_user.Username);
                                    List<Item> wanted_items =
                                        my_completed_trade_offer.ItemsToTrade.FindAll(item =>
                                            item.BelongsTo != active_user.Username);
                                    ColorizedPrint(
                                        $"You wanted to trade with {my_completed_trade_offer.OfferTo.Username}:");
                                    ColorizedPrint("You offered items:");
                                    foreach (Item item in offered_items)
                                    {
                                        ColorizedPrint(item.Name);
                                    }

                                    ColorizedPrint("And you want to trade those items for:");
                                    foreach (Item item in wanted_items)
                                    {
                                        ColorizedPrint(item.Name);
                                    }

                                    ColorizedPrint($"The trade was {my_completed_trade_offer.Status}");
                                }
                            }

                            if (incoming_completed_trade_offers.Count > 0)
                            {
                                foreach (TradeOffer incoming_completed_trade_offer in incoming_completed_trade_offers)
                                {
                                    List<Item> offered_items =
                                        incoming_completed_trade_offer.ItemsToTrade.FindAll(item =>
                                            item.BelongsTo != active_user.Username);
                                    List<Item> wanted_items =
                                        incoming_completed_trade_offer.ItemsToTrade.FindAll(item =>
                                            item.BelongsTo == active_user.Username);
                                    ColorizedPrint(
                                        $"{incoming_completed_trade_offer.OfferTo.Username} wanted to trade with you:");
                                    ColorizedPrint(
                                        $"{incoming_completed_trade_offer.OfferTo.Username} offered you items:");
                                    foreach (Item item in offered_items)
                                    {
                                        ColorizedPrint(item.Name);
                                    }

                                    ColorizedPrint("And want to trade for the following items from your list:");
                                    foreach (Item item in wanted_items)
                                    {
                                        ColorizedPrint(item.Name);
                                    }

                                    ColorizedPrint($"The trade was {incoming_completed_trade_offer.Status}");
                                }
                            }
                        }
                        else
                        {
                            ColorizedPrint("I don't want to upset you, but there are no completed trade offers yet.");
                        }
                    }
                    else
                    {
                        ColorizedPrint("I don't want to upset you, but there are no trade offers yet.");
                        ColorizedPrint("Press any key to exit...");
                        Console.ReadLine();
                    }

                    break;
                case "6":
                case "Logout":
                    Console.Clear();
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
                    bool isAuthenticated = false;
                    foreach (User user in users)
                    {
                        isAuthenticated = user.TryLogIn(login_username_input, login_password_input);
                        if (isAuthenticated)
                        {
                            active_user = user;
                            is_unauthorized_menu = false;
                            break;
                        }
                    }

                    if (!isAuthenticated)
                    {
                        ColorizedPrint("Wrong username or password!", ConsoleColor.DarkRed);
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
                        User new_user = new User(signup_username_input, signup_email_input, signup_password_input);
                        users.Add(new_user);
                        User.SavveUserToFile(new_user, "users.csv");
                    }

                    break;
            }
        }
    }
}
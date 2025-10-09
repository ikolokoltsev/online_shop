# Trading system

Yes, I called it an online shop, but... Yeah, I'm stupid, sorry about that

P.S. Namings are hard

Long story short, it's a console-based item trading application that allows users to create accounts, items, and
exchange them through trade offers.

## Features

- User authentication (login/signup/logout)
- Item management (create and browse items)
- Trade offer system (create, accept, or deny offers)
- Persistent data storage using CSV files
- Colorized console output for better UX

## How to Use

First things first!

1. Run the application:

```bash
  dotnet build
  dotnet run

```

2. You'll be presented with the main menu

### If you don't have an account(yet)

1. Select `2. Sign up` from the main menu(For this and any other menu option, type an index or the option name)

```bash
  2
```

or

```bash
  Sign up
```

But it's better and easier to use the index.

2. Enter a unique username
3. Provide an email address
4. Come up with a strong password 💪
5. You'll be automatically returned to the login menu (Welcome to the club)

### If you have an account(Nice!)

1. Select `1. Log in`
2. Enter your username or email(Not case-sensitive)
3. Enter your password(case-sensitive)

### Authorize features

#### 1. Add New Item

- Enter item name
- Provide an optional description
- Item will be added to your inventory

#### 2. Browse Other Users' Items

- View all items owned by other users
- Create trade offers by selecting items you want(use indexes, that's easier)
- Offer your own items in exchange

#### 3. Browse My Trade Offers

- View all trade offers you've sent
- See the status of your offers

#### 4. Browse Income Trade Offers

- View trade offers from other users
- Accept or deny offers(Or do nothing, and keep those offers with you forever! Ahahahaha!)
- When accepted, items are automatically exchanged. Yes, we will do the shipping 😎

#### 5. Browse Completed Requests

- View the history of all accepted or denied trade offers. It's always cool to remember good old times
- See both incoming and outgoing completed trades

#### 6. Logout

- Return to the main menu. What did you expect? It won't drive you to some party. I know it can be a cool feature, maybe
  will be implemented in the future

## Implementation Choices

### Data Persistence

**CSV File Storage**: The application uses three CSV files for data persistence:

- `users.csv` - Stores user credentials
- `items.csv` - Stores item information
- `trade_offers.csv` - Stores trade offer data

**Rationale**: Because JSON is a fat ass 🤷🏻‍♂️.

### Object-Oriented Design Decisions

#### Composition Over Inheritance

The application uses **composition** rather than inheritance:

- `TradeOffer` contains `User` objects (OfferFrom, OfferTo)
- `TradeOffer` contains a `List<Item>` for items being traded
- `Item` references its owner via a string property (`BelongsTo`)

**Rationale**:

- No clear "is-a" relationships exist between entities
- Composition provides flexibility - trade offers can reference users without tight coupling
- Easier to modify relationships without affecting class hierarchies

#### Why Not Inheritance?

Inheritance wasn't used because `User`, `Item`, and `TradeOffer` are distinct entities with no shared behavior, have
no ```is a``` relationship.

### Data Structure Choices

#### Dictionary for UI Navigation

Dictionaries map display indices to objects:

```csharp
Dictionary<int, Item> users_items_dict = new Dictionary<int, Item>();
```

**Rationale**: Provides O(1) lookup when the user selects an item by index, making the UI responsive.

### Trade Offer Item Storage

Items in trade offers are stored as a pipe-separated string:

```
Rock-Robert|Sandwich-Marcus
```

**Rationale**: Allows multiple items per trade while maintaining CSV compatibility. Items are identified by composite
key (`ItemId = $"{name}-{belongsTo}"`).


## File Structure

```
users.csv        - Username, Email, Password
items.csv        - Name, Description, BelongsTo
trade_offers.csv - OfferFrom.Username, OfferTo.Username, Status, ItemIds (pipe-separated)
```

## Known Limitations

- No password hashing (passwords stored in plain text)
- Limited error recovery for corrupted CSV files
- Trade offers can't be modified once created

---

## Easter Egg

There's a hidden dialogue between users "Robert" and "Marcus" involving a rock. Try to find it! 😎

## Nice to have in the future

- Implement password hashing for security
- Add trade offer modification/cancellation from the sender
- Add trade offer modification from the receiver
- Include item search functionality

---

## 📝 TODOs

### Password improvements
- [ ] Make password input hidden. PasswordInput in util IO utils.
- [ ] Password hashing
### Trade offers
- [ ] Implement trade offer update functionality. 
### Items menu
- [ ] Users should be able to search and filter the items
### Menu improvements
- [ ] Implement Terminal User Interface (TUI) menu navigation instead of user's text input.


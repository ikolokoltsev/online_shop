namespace ItemApp;

class Item
{
    public string Name;
    public string? Description;
    public double Price;
    public string BelongsTo;
    public Item(string name, string? description, double price, string belongsTo)
    {
        Name = name;
        Description = description;
        Price = price;
        BelongsTo = belongsTo;
    }
}
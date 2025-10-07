namespace ItemApp;

class Item
{
    public string Name;
    public string? Description;
    public string BelongsTo;

    public Item(string name, string? description, string belongsTo)
    {
        Name = name;
        Description = description;
        BelongsTo = belongsTo;
    }
}
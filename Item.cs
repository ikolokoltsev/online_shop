namespace ItemApp;

class Item
{
    public string ItemId;
    public string Name;
    public string? Description;
    public string BelongsTo;

    public Item(string name, string? description, string belongsTo)
    {
        Name = name;
        Description = description;
        BelongsTo = belongsTo;
        ItemId = $"{name}-{belongsTo}";
    }

    public static void SaveItemsToFile(List<Item> items, string fileName)
    {
        var items_lines = items.Select(item => $"{item.Name},{item.Description},{item.BelongsTo}");
        File.WriteAllLines(fileName, items_lines);
    }

    public static void SaveItemToFile(Item item, string filePath)
    {
        File.AppendAllLines(filePath, new[] { $"{item.Name},{item.Description},{item.BelongsTo}" });
    }
}
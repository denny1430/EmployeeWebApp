public class Menu
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Url { get; set; } // Can be null if it's only a parent
    public int? ParentId { get; set; } // Null if main menu
    public List<Menu> Children { get; set; } = new List<Menu>();
}


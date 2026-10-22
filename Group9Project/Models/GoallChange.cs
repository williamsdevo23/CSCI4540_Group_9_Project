using SQLite;

public class GoalChange
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public int GoalId { get; set; }
    public string Action { get; set; } = "";
    public string OldValue { get; set; } = "";
    public string NewValue { get; set; } = "";
    public DateTime ChangedAt { get; set; }
}

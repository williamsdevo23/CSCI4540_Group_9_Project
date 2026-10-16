using SQLite;

namespace AssignmentTracker.Models;

public class Assignment
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Title { get; set; } = "";

    public string Course { get; set; } = "";

    public DateTime DueDate { get; set; }

    public bool IsCompleted { get; set; }
}
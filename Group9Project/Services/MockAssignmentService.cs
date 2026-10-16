using AssignmentTracker.Models;

namespace AssignmentTracker.Services;

public class MockAssignmentService
{
    public List<Assignment> GetAssignments()
    {
        return new List<Assignment>
        {
            new Assignment
            {
                Id = 1,
                Title = "Math Homework",
                Course = "Calculus",
                DueDate = DateTime.Today.AddDays(3),
                IsCompleted = false
            },
            new Assignment
            {
                Id = 2,
                Title = "Biology Lab Report",
                Course = "Biology",
                DueDate = DateTime.Today.AddDays(5),
                IsCompleted = false
            }
        };
    }
}
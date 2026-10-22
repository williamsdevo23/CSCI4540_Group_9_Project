using SQLite;
using GoalTracker.Models;

namespace GoalTracker.Services;

public class GoalDatabaseService
{
    private SQLiteAsyncConnection? _database;

    private async Task InitAsync()
    {
        if (_database != null)
            return;

        string dbPath = Path.Combine(
            FileSystem.AppDataDirectory,
            "goals.db3");

        var database = new SQLiteAsyncConnection(dbPath);

        await database.CreateTableAsync<Goal>();
        await database.CreateTableAsync<GoalChange>();

        _database = database;
    }

    // READ: Get all saved goals
    public async Task<List<Goal>> GetGoalsAsync()
    {
        await InitAsync();

        return await _database!.Table<Goal>().ToListAsync();
    }

    // READ: Get change tracking history
    public async Task<List<GoalChange>> GetChangesAsync()
    {
        await InitAsync();

        return await _database!.Table<GoalChange>()
            .OrderByDescending(c => c.Id)
            .ToListAsync();
    }

    // CREATE: Add a new goal
    public async Task AddGoalAsync(Goal goal)
    {
        await InitAsync();

        await _database!.RunInTransactionAsync(conn =>
        {
            conn.Insert(goal);

            conn.Insert(new GoalChange
            {
                GoalId = goal.Id,
                Action = "CREATE",
                OldValue = "",
                NewValue = goal.Title,
                ChangedAt = DateTime.UtcNow
            });
        });
    }

    // UPDATE: Modify an existing goal
    public async Task UpdateGoalAsync(Goal goal)
    {
        await InitAsync();

        await _database!.RunInTransactionAsync(conn =>
        {
            var oldGoal = conn.Find<Goal>(goal.Id);

            if (oldGoal == null)
                throw new InvalidOperationException(
                    "Goal not found.");

            conn.Update(goal);

            conn.Insert(new GoalChange
            {
                GoalId = goal.Id,
                Action = "UPDATE",
                OldValue = oldGoal.Title,
                NewValue = goal.Title,
                ChangedAt = DateTime.UtcNow
            });
        });
    }

    // DELETE: Remove an existing goal
    public async Task DeleteGoalAsync(int goalId)
    {
        await InitAsync();

        await _database!.RunInTransactionAsync(conn =>
        {
            var goal = conn.Find<Goal>(goalId);

            if (goal == null)
                throw new InvalidOperationException(
                    "Goal not found.");

            conn.Delete(goal);

            conn.Insert(new GoalChange
            {
                GoalId = goalId,
                Action = "DELETE",
                OldValue = goal.Title,
                NewValue = "",
                ChangedAt = DateTime.UtcNow
            });
        });
    }
}

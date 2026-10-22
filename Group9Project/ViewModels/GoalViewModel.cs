using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GoalTracker.Models;
using GoalTracker.Services;

namespace GoalTracker.ViewModels;

public partial class GoalViewModel : ObservableObject
{
    private readonly GoalDatabaseService _database;

    public ObservableCollection<Goal> Goals { get; } = new();

    public ObservableCollection<GoalChange> Changes { get; } = new();

    [ObservableProperty]
    private string title = "";

    [ObservableProperty]
    private string description = "";

    [ObservableProperty]
    private Goal? selectedGoal;

    public GoalViewModel(GoalDatabaseService database)
    {
        _database = database;
    }

    // Load goals from SQLite
    [RelayCommand]
    private async Task LoadGoalsAsync()
    {
        var goals = await _database.GetGoalsAsync();

        Goals.Clear();

        foreach (var goal in goals)
            Goals.Add(goal);
    }

    // Load change tracking history
    [RelayCommand]
    private async Task LoadChangesAsync()
    {
        var changes = await _database.GetChangesAsync();

        Changes.Clear();

        foreach (var change in changes)
            Changes.Add(change);
    }

    // Create a goal
    [RelayCommand]
    private async Task AddGoalAsync()
    {
        if (string.IsNullOrWhiteSpace(Title))
            return;

        var goal = new Goal
        {
            Title = Title,
            Description = Description,
            IsCompleted = false
        };

        await _database.AddGoalAsync(goal);

        ClearFields();

        await RefreshAsync();
    }

    // Select a goal for editing
    [RelayCommand]
    private void EditGoal(Goal goal)
    {
        SelectedGoal = goal;
        Title = goal.Title;
        Description = goal.Description;
    }

    // Save updated goal information
    [RelayCommand]
    private async Task UpdateGoalAsync()
    {
        if (SelectedGoal == null ||
            string.IsNullOrWhiteSpace(Title))
            return;

        var updatedGoal = new Goal
        {
            Id = SelectedGoal.Id,
            Title = Title,
            Description = Description,
            IsCompleted = SelectedGoal.IsCompleted
        };

        await _database.UpdateGoalAsync(updatedGoal);

        ClearFields();

        await RefreshAsync();
    }

    // Delete a goal
    [RelayCommand]
    private async Task DeleteGoalAsync(Goal goal)
    {
        await _database.DeleteGoalAsync(goal.Id);

        ClearFields();

        await RefreshAsync();
    }

    private async Task RefreshAsync()
    {
        await LoadGoalsAsync();
        await LoadChangesAsync();
    }

    private void ClearFields()
    {
        Title = "";
        Description = "";
        SelectedGoal = null;
    }
}

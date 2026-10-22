using GoalTracker.ViewModels;

namespace GoalTracker;

public partial class MainPage : ContentPage
{
    private readonly GoalViewModel _viewModel;

    public MainPage(GoalViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.LoadGoalsCommand.ExecuteAsync(null);
        await _viewModel.LoadChangesCommand.ExecuteAsync(null);
    }
}
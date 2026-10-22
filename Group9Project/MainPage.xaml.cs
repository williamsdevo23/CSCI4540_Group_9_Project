using GoalTracker.ViewModels;

namespace Group9Project;

public partial class MainPage : ContentPage
{
    private readonly GoalViewModel _viewModel;

    public MainPage(GoalViewModel viewModel)
    {
        _viewModel = viewModel;
        BindingContext = _viewModel;
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.LoadGoalsCommand.ExecuteAsync(null);
        await _viewModel.LoadChangesCommand.ExecuteAsync(null);
    }
}
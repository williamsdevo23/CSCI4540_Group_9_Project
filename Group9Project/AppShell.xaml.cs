namespace Group9Project;

public partial class AppShell : Shell
{
	public AppShell(MainPage mainPage)
	{
		InitializeComponent();
		MainPageShellContent.Content = mainPage;
	}
}

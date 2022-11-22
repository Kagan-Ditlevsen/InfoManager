namespace InfoAndroidApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(LogPage), typeof(LogPage));
        Routing.RegisterRoute(nameof(SetupPage), typeof(SetupPage));
    }
}

using InfoAndroidApp.Models;

namespace InfoAndroidApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        DeviceDisplay.KeepScreenOn = true;
    }
}


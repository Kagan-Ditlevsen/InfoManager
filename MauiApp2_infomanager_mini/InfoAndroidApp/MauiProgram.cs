namespace InfoAndroidApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("FontAwesome-6.ttf", "FA6");
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .ConfigureEssentials(essentials =>
            {
                essentials.UseVersionTracking();
            });

        builder.Services.AddSingleton<MainPage>();

        builder.Services.AddTransient<LogPage>();

        return builder.Build();
    }
    #region GeoCoding
    // https://stackoverflow.com/questions/70976168/how-to-display-google-maps-on-net-maui
    public static async Task<string> GeoFromAddress(string address)
    {
        IEnumerable<Location> locations = await Geocoding.Default.GetLocationsAsync(address);

        Location location = locations?.FirstOrDefault();

        if (location != null)
            return $"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}";
        else
            return "Error: Not Found";
    }
    private static async Task<string> GeoToAddress(double latitude = 47.673988, double longitude = -122.121513)
    {
        IEnumerable<Placemark> placemarks = await Geocoding.Default.GetPlacemarksAsync(latitude, longitude);

        Placemark placemark = placemarks?.FirstOrDefault();

        if (placemark != null)
        {
            return
                $"AdminArea:       {placemark.AdminArea}\n" +
                $"CountryCode:     {placemark.CountryCode}\n" +
                $"CountryName:     {placemark.CountryName}\n" +
                $"FeatureName:     {placemark.FeatureName}\n" +
                $"Locality:        {placemark.Locality}\n" +
                $"PostalCode:      {placemark.PostalCode}\n" +
                $"SubAdminArea:    {placemark.SubAdminArea}\n" +
                $"SubLocality:     {placemark.SubLocality}\n" +
                $"SubThoroughfare: {placemark.SubThoroughfare}\n" +
                $"Thoroughfare:    {placemark.Thoroughfare}\n";

        }

        return "";
    }
    #endregion
    #region Device
    public static string ReadDeviceDisplay()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        sb.AppendLine($"Pixel width: {DeviceDisplay.Current.MainDisplayInfo.Width} / Pixel Height: {DeviceDisplay.Current.MainDisplayInfo.Height}");
        sb.AppendLine($"Density: {DeviceDisplay.Current.MainDisplayInfo.Density}");
        sb.AppendLine($"Orientation: {DeviceDisplay.Current.MainDisplayInfo.Orientation}");
        sb.AppendLine($"Rotation: {DeviceDisplay.Current.MainDisplayInfo.Rotation}");
        sb.AppendLine($"Refresh Rate: {DeviceDisplay.Current.MainDisplayInfo.RefreshRate}");

        return sb.ToString();
    }
    public static string ReadDeviceInfo()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        sb.AppendLine($"Model: {DeviceInfo.Current.Model}");
        sb.AppendLine($"Manufacturer: {DeviceInfo.Current.Manufacturer}");
        sb.AppendLine($"Name: {DeviceInfo.Name}");
        sb.AppendLine($"OS Version: {DeviceInfo.VersionString}");
        sb.AppendLine($"Refresh Rate: {DeviceInfo.Current}");
        sb.AppendLine($"Idiom: {DeviceInfo.Current.Idiom}");
        sb.AppendLine($"Platform: {DeviceInfo.Current.Platform}");

        bool isVirtual = DeviceInfo.Current.DeviceType switch
        {
            DeviceType.Physical => false,
            DeviceType.Virtual => true,
            _ => false
        };

        sb.AppendLine($"Virtual device? {isVirtual}");

        return sb.ToString();
    }
    #endregion

    #region ToDo Code
    /************************** Detect and react to orientation change */
    //DeviceDisplay.MainDisplayInfoChanged += Current_MainDisplayInfoChanged;
    //DeviceDisplay.Current.MainDisplayInfoChanged += Current_MainDisplayInfoChanged;
    //private void Current_MainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
    //{
    //    Button btn1 = (Button)ActionButtonList.Children[0];
    //    Button btn2 = (Button)ActionButtonList.Children[1];
    //    if (DeviceDisplay.Current.MainDisplayInfo.Orientation == DisplayOrientation.Portrait)
    //    {
    //        FlexLayout.SetBasis(btn1, new FlexBasis(0.50f, true));
    //        FlexLayout.SetBasis(btn2, new FlexBasis(0.50f, true));
    //    }
    //    else
    //    {
    //        FlexLayout.SetBasis(btn1, new FlexBasis(0.25f, true));
    //        FlexLayout.SetBasis(btn2, new FlexBasis(0.25f, true));
    //    }
    //    Common.Alert("Message", DeviceDisplay.Current.MainDisplayInfo.Orientation.ToString());
    //}
    #endregion
}

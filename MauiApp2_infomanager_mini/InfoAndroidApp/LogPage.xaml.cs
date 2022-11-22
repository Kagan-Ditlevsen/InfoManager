using CommunityToolkit.Mvvm.Input;
using InfoAndroidApp.Models;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Layouts;
using Newtonsoft.Json;
using System.Linq;

namespace InfoAndroidApp;

public partial class LogPage : ContentPage
{
    public LogPage()
    {
        InitializeComponent();

        List<DailyType> dtList = new List<DailyType>();
        DailyType dt;
        using (var context = new Db())
        {
            dtList = context.DailyType;
            dt = dtList.Single(x => x.typeId == 1053);
        }
        Button btn = new Button
        {
            Text = dt.internalTitle,
            ContentLayout = new Button.ButtonContentLayout(Button.ButtonContentLayout.ImagePosition.Left, 5),
            ImageSource = new FontImageSource() { FontFamily = "FA6", Glyph = FA6.GetByString(dt.iconCss) },
            HorizontalOptions = LayoutOptions.FillAndExpand,
            WidthRequest = 150,
            ClassId = dt.typeId.ToString(),
        };
        btn.Clicked += ActionButton_Clicked;

        ActionButtonList.Children.Add(btn);
    }

    private void ActionButton_Clicked(object sender, EventArgs e)
    {
        Button btn = (Button)sender;

        Common.Alert("Click", btn.ClassId);
    }
    protected override void OnAppearing()
    {
        string setup = Common.ApiCall("setup").Result;
        Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(setup);
        var x = myDeserializedClass.DailyType.Single(x => x.typeId == 1053);
        label.Text = $"{x.internalTitle} [{x.iconCss}]";

        base.OnAppearing();
    }
}
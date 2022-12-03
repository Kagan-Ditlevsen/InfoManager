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

        List<DailyType> dtList = new();
        //DailyType dt;
        using (var context = new Db().Conn())
        {
            dtList = context.DailyType;
            //dt = dtList.Single(x => x.typeId == 1053);
        }
        foreach (DailyType type in dtList)
        {
            Button btn = new Button
            {
                Text = type.internalTitle,
                ContentLayout = new Button.ButtonContentLayout(Button.ButtonContentLayout.ImagePosition.Left, 5),
                ImageSource = new FontImageSource() { FontFamily = "FA6", Glyph = FA6.GetByString(type.iconCss) },
                HorizontalOptions = LayoutOptions.FillAndExpand,
                WidthRequest = 150,
                MinimumHeightRequest = 150,
                MaximumHeightRequest = 250,
                ClassId = type.typeId.ToString(),
            };
            btn.Clicked += ActionButton_Clicked;

            //zzz.Children.Add(btn);
        }
    }

    private void ActionButton_Clicked(object sender, EventArgs e)
    {
        Button btn = (Button)sender;

        Common.Alert("Click", btn.ClassId);
    }
    protected override void OnAppearing()
    {
        string setup = Api.Get("setup").Result;
        Db myDeserializedClass = JsonConvert.DeserializeObject<Db>(setup);
        var x = myDeserializedClass.DailyType.Single(x => x.typeId == 1053);
        //label.Text = $"{x.internalTitle} [{x.iconCss}]";

        base.OnAppearing();
    }
}
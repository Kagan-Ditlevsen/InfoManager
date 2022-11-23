using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoAndroidApp
{
    internal class Common
    {
        public static async void Alert(string title, string message)
        {
            await Shell.Current.DisplayAlert(title, message, "Ok");
        }
    }
}
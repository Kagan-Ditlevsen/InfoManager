using IM.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace IM.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}
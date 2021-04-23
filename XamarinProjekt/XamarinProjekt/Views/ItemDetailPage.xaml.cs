using System.ComponentModel;
using Xamarin.Forms;
using XamarinProjekt.ViewModels;

namespace XamarinProjekt.Views
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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinProjekt.Models;
using XamarinProjekt.ViewModels;
using XamarinProjekt.Views;

namespace XamarinProjekt.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeatherPage : ContentPage
    {
        WeatherViewModel _viewModel;

        public WeatherPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new WeatherViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}
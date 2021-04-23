using System;
using System.Collections.Generic;
using Xamarin.Forms;
using XamarinProjekt.ViewModels;
using XamarinProjekt.Views;

namespace XamarinProjekt
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}

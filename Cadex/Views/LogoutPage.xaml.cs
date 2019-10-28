using System;
using System.Collections.Generic;
using Cadex.Services;
using Xamarin.Forms;

namespace Cadex.Views
{
    public partial class LogoutPage : ContentPage
    {
        public LogoutPage()
        {
            InitializeComponent();

            AppSession.login = false;

            Application.Current.MainPage = new Nav();
        }
    }
}

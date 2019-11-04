using System;
using System.Collections.Generic;
using Cadex.Services;
using Xamarin.Forms;

namespace Cadex.Views
{
    public partial class LogoutPage : ContentPage
    {
        string key;

        public LogoutPage(string key)
        {
            InitializeComponent();

            this.key = key;

            AppSession.login = false;

            Application.Current.MainPage = new Nav(key);
        }
    }
}

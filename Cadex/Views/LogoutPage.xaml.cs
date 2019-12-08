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

            //Ændre AppSession til false.
            AppSession.login = false;
            AppSession.IT = false;

            //Når du er logget ud bliver du sendt tilbage til startsiden.
            Application.Current.MainPage = new Nav(key);
        }
    }
}

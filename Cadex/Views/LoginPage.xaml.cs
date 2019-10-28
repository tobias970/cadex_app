using System;
using System.Collections.Generic;
using Cadex.Services;
using Xamarin.Forms;

namespace Cadex.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            AppSession.login = true;
            Application.Current.MainPage = new Nav();
        }

    }
}

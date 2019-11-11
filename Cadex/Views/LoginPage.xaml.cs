using System;
using System.Collections.Generic;
using Cadex.Services;
using Cadex.ViewModels;
using Xamarin.Forms;

namespace Cadex.Views
{
    public partial class LoginPage : ContentPage
    {
        string key;

        public LoginPage()
        {
            InitializeComponent();
        }

        void Login_Button_Clicked(object sender, System.EventArgs e)
        {
            //Instance af klasser og en reference til objected.
            LoginViewModel auth = new LoginViewModel();
            Validate valid = new Validate();

            //Henter API token fra LoginViewModel og sender brugernavn og kodeord til APIen.
            key = auth.HentNoegle(username.Text, password.Text);

            //Validere tokenen for at se om det er en valid token.
            bool status = valid.validatekey(key);

            //Tjekker om resultatet af validate er true, hvis ikke bliver du ikke logget ind.
            if (status)
            {
                fejl.IsVisible = false;
                AppSession.login = true;
                Application.Current.MainPage = new Nav(key);
            }
            else
            {
                AppSession.login = false;
                fejl.IsVisible = true;
            }
            
            //throw new NotImplementedException();
        }

    }
}

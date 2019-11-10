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
            //Oprettelse af objekt til klassen hvor API metoderne er i.
            LoginViewModel auth = new LoginViewModel();
            Validate valid = new Validate();

            //Henter API token fra en metode i klassen "APIMethods" og sender brugernavn og kodeord til APIen.
            key = auth.HentNoegle(username.Text, password.Text);

            
            bool status = valid.validatekey(key);
            Console.WriteLine("Den endelig status : " + status);
            

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

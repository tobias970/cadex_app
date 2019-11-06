using System;
using System.Collections.Generic;
using Cadex.Services;
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
            APIMethods apimetoder = new APIMethods();

            //Henter API token fra en metode i klassen "APIMethods" og sender brugernavn og kodeord til APIen.
            key = apimetoder.HentNyNoegle(username.Text, password.Text);


            bool status = apimetoder.validatekey(key);
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

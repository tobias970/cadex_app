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

            //AppSession.login = true;
            //Application.Current.MainPage = new Nav();
        }

        void Login_Button_Clicked(object sender, System.EventArgs e)
        {
            



            //Oprettelse af objekt til klassen hvor API metoderne er i.
            APIMethods apimetoder = new APIMethods();
            key = apimetoder.HentNyNoegle(username.Text, password.Text);
            //Henter API token fra en metode i klassen "APIMethods" og sender brugernavn og kodeord til APIen.
            //key = apimetoder.HentNyNoegle(username.Text, password.Text);

            //Udskriver noeglen fra APIen.
            //Console.WriteLine(key);

            Console.WriteLine("Den endelig key : " + key);

            AppSession.login = true;

            //Sover i 2 sekunder for at APIen kan godkende token.
            //System.Threading.Thread.Sleep(2000);

            //Skifter navigations side til "AfdelingPage".
            Application.Current.MainPage = new Nav(key);

            //throw new NotImplementedException();
        }

    }
}

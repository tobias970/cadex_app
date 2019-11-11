using System;
using System.Collections.Generic;
using Cadex.Services;
using Cadex.ViewModels;
using Xamarin.Forms;

namespace Cadex.Views
{
    public partial class NewsAddPage : ContentPage
    {
        //Instance af klasser og en reference til objected.
        NewsAddViewModel nyheder = new NewsAddViewModel();
        Validate valid = new Validate();

        string key;

        public NewsAddPage(string key)
        {
            InitializeComponent();

            this.key = key;

        }
        void Button_NavBack_Pressed(object sender, System.EventArgs e)
        {
            //Navigere tilbage til startsiden.
            Application.Current.MainPage = new Nav(key);
        }
        void Button_OpretNyhed_Pressed(object sender, System.EventArgs e)
        {
            //Kalder metoden "validatekey" og gemmer statussen.
            bool status = valid.validatekey(key);

            //Tjekker statussen fra resultatet af validatekey metoden.
            if (status)
            {
                //Tjekker om labels indeholder noget.
                if (newstitle.Text != "" && newsdesc.Text != "")
                {
                    //Kalder metoden "OpretNyhed" og gemmer resultatet.
                    bool Stat = nyheder.OpretNyhed(key, newstitle.Text, newsdesc.Text);

                    //Tjekker om resultatet er true, så nyheden er oprettet.
                    if (Stat)
                    {
                        DisplayAlert("Nyhed oprettet", "Nyheden er oprettet med succes", "OK");
                        newstitle.Text = "";
                        newsdesc.Text = "";
                        fejl.IsVisible = false;
                    }
                    else
                    {
                        DisplayAlert("Fejl", "Nyheden blev ikke oprettet", "OK");
                    }
                }
                else
                {
                    fejl.IsVisible = true;
                }
            }
            //Hvis tokenen ikke er valid bliver man smidt tilbage til startsiden.
            else
            {
                DisplayAlert("Fejl", "Du er blevet logget ud", "OK");
                AppSession.login = false;
                Application.Current.MainPage = new Nav(key);
            }

        }
    }
}

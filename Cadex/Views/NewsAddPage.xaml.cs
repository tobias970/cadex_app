using System;
using System.Collections.Generic;
using Cadex.Services;
using Cadex.ViewModels;
using Xamarin.Forms;

namespace Cadex.Views
{
    public partial class NewsAddPage : ContentPage
    {
        string key;
        NewsAddViewModel nyheder = new NewsAddViewModel();
        Validate valid = new Validate();

        public NewsAddPage(string key)
        {
            InitializeComponent();

            this.key = key;

        }
        void Button_NavBack_Pressed(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new Nav(key);
        }
        void Button_OpretNyhed_Pressed(object sender, System.EventArgs e)
        {
            bool status = valid.validatekey(key);
            if (status)
            {
                if (newstitle.Text != "" && newsdesc.Text != "")
                {

                    bool Stat = nyheder.OpretNyhed(key, newstitle.Text, newsdesc.Text);
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
            else
            {
                DisplayAlert("Fejl", "Du er blevet logget ud", "OK");
                AppSession.login = false;
                Application.Current.MainPage = new Nav(key);
            }

        }
    }
}

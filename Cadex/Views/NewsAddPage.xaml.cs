using System;
using System.Collections.Generic;
using Cadex.Services;
using Xamarin.Forms;

namespace Cadex.Views
{
    public partial class NewsAddPage : ContentPage
    {
        string key;

        public NewsAddPage(string key)
        {
            InitializeComponent();

            this.key = key;

            Console.WriteLine(AppSession.login);
        }
        void Button_NavBack_Pressed(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new Nav(key);
        }
        void Button_OpretNyhed_Pressed(object sender, System.EventArgs e)
        {
            if (newstitle.Text != "" || newsdesc.Text != "")
            {
                APIMethods apimetoder = new APIMethods();
                bool Stat = apimetoder.OpretNyhed(key, newstitle.Text, newsdesc.Text);
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
    }
}

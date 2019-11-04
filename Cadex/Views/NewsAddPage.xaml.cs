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
    }
}

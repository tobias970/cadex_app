using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Cadex.Views
{
    public partial class ManNewsPage : ContentPage
    {
        string key;

        public ManNewsPage(string key)
        {
            InitializeComponent();

            this.key = key;
        }
        void Button_AddNews_Pressed(object sender, System.EventArgs e)
        {
            //Navigere dig over til ADD news siden.
            Application.Current.MainPage = new NewsAddPage(key);

            //throw new NotImplementedException();
        }
        void Button_EditNews_Pressed(object sender, System.EventArgs e)
        {
            //Navigere dig over til Edit news siden.
            Application.Current.MainPage = new NewsEditPage(key);

            //throw new NotImplementedException();
        }
        void Button_DeleteNews_Pressed(object sender, System.EventArgs e)
        {
            //Navigere dig over til Delete news siden.
            Application.Current.MainPage = new NewsDeletePage(key);

            //throw new NotImplementedException();
        }
    }
}

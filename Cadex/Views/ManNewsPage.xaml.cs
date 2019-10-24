using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Cadex.Views
{
    public partial class ManNewsPage : ContentPage
    {
        public ManNewsPage()
        {
            InitializeComponent();
        }
        void Button_AddNews_Pressed(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new NewsAddPage();
            //throw new NotImplementedException();
        }
        void Button_EditNews_Pressed(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new NewsEditPage();
            //throw new NotImplementedException();
        }
        void Button_DeleteNews_Pressed(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new NewsDeletePage();
            //throw new NotImplementedException();
        }
    }
}

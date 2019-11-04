using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Cadex.Views
{
    public partial class ProductsAddPage : ContentPage
    {
        string key;

        public ProductsAddPage(string key)
        {
            InitializeComponent();

            this.key = key;
        }
        void Button_NavBack_Pressed(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new Nav(key);
        }
    }
}

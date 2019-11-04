using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Cadex.Views
{
    public partial class ManProductsPage : ContentPage
    {
        string key;

        public ManProductsPage(string key)
        {
            InitializeComponent();

            this.key = key;
        }
        void Button_AddProducts_Pressed(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new ProductsAddPage(key);
            //throw new NotImplementedException();
        }
        void Button_EditProducts_Pressed(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new ProductsEditPage(key);
            //throw new NotImplementedException();
        }
        void Button_DeleteProducts_Pressed(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new ProductsDeletePage(key);
            //throw new NotImplementedException();
        }
    }
}

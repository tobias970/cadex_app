using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Cadex.Views
{
    public partial class ManProductsPage : ContentPage
    {
        public ManProductsPage()
        {
            InitializeComponent();
        }
        void Button_AddProducts_Pressed(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new ProductsAddPage();
            //throw new NotImplementedException();
        }
        void Button_EditProducts_Pressed(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new ProductsEditPage();
            //throw new NotImplementedException();
        }
        void Button_DeleteProducts_Pressed(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new ProductsDeletePage();
            //throw new NotImplementedException();
        }
    }
}

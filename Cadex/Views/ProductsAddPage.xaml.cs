using System;
using System.Collections.Generic;
using Cadex.Services;
using Cadex.ViewModels;
using Xamarin.Forms;

namespace Cadex.Views
{
    public partial class ProductsAddPage : ContentPage
    {
        string key;
        ProductsAddViewModel produkter = new ProductsAddViewModel();
        Validate valid = new Validate();

        public ProductsAddPage(string key)
        {
            InitializeComponent();

            this.key = key;
        }
        void Button_NavBack_Pressed(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new Nav(key);
        }
        void Button_OpretProdukt_Pressed(object sender, System.EventArgs e)
        {
            bool status = valid.validatekey(key);
            if (status)
            {
                if (producttitle.Text != "" && productdesc.Text != "" && productpris.Text != "")
                {
                    bool Stat = produkter.OpretProdukt(key, producttitle.Text, productdesc.Text, productpris.Text);
                    if (Stat)
                    {
                        DisplayAlert("Produkt oprettet", "Produktet er oprettet med succes", "OK");

                        producttitle.Text = "";
                        productdesc.Text = "";
                        productpris.Text = "";

                        fejl.IsVisible = false;
                    }
                    else
                    {
                        DisplayAlert("Fejl", "Produktet blev ikke oprettet", "OK");
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

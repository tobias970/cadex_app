﻿using System;
using System.Collections.Generic;
using Cadex.Services;
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
        void Button_OpretProdukt_Pressed(object sender, System.EventArgs e)
        {
            if (producttitle.Text != "" && productdesc.Text != "" && productpris.Text != "")
            {
                APIMethods apimetoder = new APIMethods();
                bool Stat = apimetoder.OpretProdukt(key, producttitle.Text, productdesc.Text, productpris.Text);
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
    }
}

﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Cadex.Views
{
    public partial class ProductsEditPage : ContentPage
    {
        public ProductsEditPage()
        {
            InitializeComponent();
        }
        void Button_NavBack_Pressed(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new Nav();
        }
    }
}

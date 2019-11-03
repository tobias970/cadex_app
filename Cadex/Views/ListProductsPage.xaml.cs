﻿using System;
using System.Collections.Generic;
using System.IO;
using Cadex.Services;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace Cadex.Views
{
    public partial class ListProductsPage : ContentPage
    {
        public ListProductsPage()
        {
            InitializeComponent();

            GenerateElements();
           
        }

        public void GenerateElements()
        {
            APIMethods apimetoder = new APIMethods();
            JObject result = (JObject)apimetoder.HentProdukter();

            int antalprodukter = (int)result["productCount"];

            //string images = (string)result["products"][0]["images"][0]["image"];
            //Console.WriteLine("Billede : " + images);

            int i = 0;
            while (i < antalprodukter)
            {
                Frame ramme = new Frame
                {
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    HasShadow = false,
                    BorderColor = Color.Black,
                    Margin = new Thickness (10,0,10,30),
                    WidthRequest = 300,
                };
                StackLayout Alt = new StackLayout
                {

                };
                
                StackLayout billeder = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                };
                //Loop på denne

                
                if (result["products"][i]["images"].HasValues)
                {
                    string base64String = (string)result["products"][i]["images"][0]["image"];
                    Console.WriteLine("Billedenr : " + base64String);

                    //Console.WriteLine("BASE64 : " + base64String);
                    byte[] Base64Stream = Convert.FromBase64String(base64String);
                    Console.WriteLine("BASE64S : " + Base64Stream);

                    Image billede = new Image
                    {
                        Source = ImageSource.FromStream(() => new MemoryStream(Base64Stream)),
                        WidthRequest = 500,
                        HeightRequest = 200
                    };

                    billeder.Children.Add(billede);
                }


                ScrollView Scroller = new ScrollView
                {
                    Orientation = ScrollOrientation.Horizontal,
                    Content = billeder,
                };

                Label titel = new Label
                {
                    Text = (string)result["products"][i]["name"],
                    FontSize = Device.GetNamedSize (NamedSize.Subtitle, typeof(Label)),
                };
                Label beskrivelse = new Label
                {
                    Text = (string)result["products"][i]["description"],
                };

                StackLayout stackpris = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,

                };
                Label pris = new Label
                {
                    Text = "Pris :",
                };
                Label prisen = new Label
                {
                    Text = (string)result["products"][i]["price"],
                };

                stackpris.Children.Add(pris);
                stackpris.Children.Add(prisen);

                Alt.Children.Add(Scroller);
                Alt.Children.Add(titel);
                Alt.Children.Add(beskrivelse);
                Alt.Children.Add(stackpris);

                ramme.Content = Alt;
            

                stack.Children.Add(ramme);

                i++;
            }
        }
    }
}

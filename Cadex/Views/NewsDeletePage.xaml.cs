﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace Cadex.Views
{
    public partial class NewsDeletePage : ContentPage
    {
        string key;

        public NewsDeletePage(string key)
        {
            InitializeComponent();

            this.key = key;

            Console.WriteLine("Starter slet news");
            GenerateElements();
        }

        void Button_NavBack_Pressed(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new Nav(key);
        }
        
        async void OnAlertYesNoClicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Slet", "Er du sikker på du vil slette denne?", "Ja", "Nej");
            Debug.WriteLine("Answer: " + answer);

            if (answer == true)
            {
                Console.WriteLine("Slet denne");
                //Kald metode som sletter news
                Button btn = (Button)sender;
                Console.WriteLine("Her : " + btn.ClassId);
            }
        }

        public void GenerateElements()
        {

            int i = 1;
           
            while (i < 10)
            {
                
                StackLayout alt = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal,
                };
                Button knap = new Button()
                {
                    Text = "Slet",
                    BorderColor = Color.Black,
                    BorderWidth = 1,
                    WidthRequest = 70,
                    TextColor = Color.Black,
                    Margin = new Thickness(0, 0, 10, 0),
                    ClassId = i.ToString(),    
                };
                
                knap.Clicked += OnAlertYesNoClicked;
                

                Label titel = new Label()
                {
                    Text = "Ny chef",
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                };

                alt.Children.Add(knap);
                alt.Children.Add(titel);

                Frame ramme = new Frame()
                {
                    Padding = 10,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    WidthRequest = 260,
                    Margin = new Thickness(0, 5, 0, 0),
                    HasShadow = false,
                    BorderColor = Color.Black,
                    Content = alt,
                };
                stack.Children.Add(ramme);

                i++;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Cadex.Services;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace Cadex.Views
{
    public partial class NewsDeletePage : ContentPage
    {
        string key;
        APIMethods apimetoder = new APIMethods();

        public NewsDeletePage(string key)
        {
            InitializeComponent();

            this.key = key;

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
                //Kald metode som sletter news
                Button btn = (Button)sender;
                
                bool Stat =  apimetoder.SletNyhed(key, btn.ClassId);

                if (Stat)
                {
                    await DisplayAlert("Nyhed slettet", "Nyheden er slettet", "OK");

                    stack.Children.Clear();
                    GenerateElements();
                }
                else
                {
                    await DisplayAlert("Fejl", "Nyheden blev ikke slettet", "OK");
                }
            }
        }

        public void GenerateElements()
        {
            JObject result = (JObject)apimetoder.HentNyheder(key);

            int i = 0;

            foreach (var nyhederenkelt in result["newsPosts"])
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
                    ClassId = (string)result["newsPosts"][i]["id"],    
                };
                
                knap.Clicked += OnAlertYesNoClicked;
                

                Label titel = new Label()
                {
                    Text = (string)result["newsPosts"][i]["title"],
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

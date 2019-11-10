using System;
using System.Collections.Generic;
using System.Diagnostics;
using Cadex.Services;
using Cadex.ViewModels;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace Cadex.Views
{
    public partial class ProductsDeletePage : ContentPage
    {
        string key;
        ProductsDeleteViewModel produkter = new ProductsDeleteViewModel();
        ListProductsViewModel HentProdukter = new ListProductsViewModel();
        Validate valid = new Validate();

        public ProductsDeletePage(string key)
        {
            InitializeComponent();

            this.key = key;
            
            bool status = valid.validatekey(key);
            if (status)
            {
                GenerateElements();
            }
            else
            {
                DisplayAlert("Fejl", "Du er blevet logget ud", "OK");
                AppSession.login = false;
                Application.Current.MainPage = new Nav(key);
            }
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

                bool Stat = produkter.SletProdukt(key, btn.ClassId);

                if (Stat)
                {
                    await DisplayAlert("Produkt slettet", "Produktet er slettet", "OK");

                    stack.Children.Clear();
                    GenerateElements();
                }
                else
                {
                    await DisplayAlert("Fejl", "Produktet blev ikke slettet", "OK");
                }
            }
        }

        public void GenerateElements()
        {
            JObject result = HentProdukter.HentProdukter();

            int i = 0;

            foreach (var produkterenkelt in result["products"])
            {

                StackLayout alt = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal,
                };
                Button knap = new Button()
                {
                    Text = "Slet",
                    BorderColor = Color.Black,
                    BackgroundColor = Color.FromHex("#eb4d4b"),
                    BorderWidth = 1,
                    WidthRequest = 70,
                    TextColor = Color.Black,
                    Margin = new Thickness(0, 0, 10, 0),
                    ClassId = (string)result["products"][i]["id"]
                };
                knap.Clicked += OnAlertYesNoClicked;

                Label titel = new Label()
                {
                    Text = (string)result["products"][i]["name"],
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

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
        ProductsDeleteViewModel produkter = new ProductsDeleteViewModel();
        ListProductsViewModel HentProdukter = new ListProductsViewModel();
        Validate valid = new Validate();

        string key;

        public ProductsDeletePage(string key)
        {
            InitializeComponent();

            this.key = key;

            //Tjekker om key stadig er valid.
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
            //Sender dig tilbage til startsiden.
            Application.Current.MainPage = new Nav(key);
        }

        async void OnAlertYesNoClicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Slet", "Er du sikker på du vil slette denne?", "Ja", "Nej");
            Debug.WriteLine("Answer: " + answer);

            //Tjekker om det blev svaret ja.
            if (answer == true)
            {
                //Henter knappen der er blevet trykket på.
                Button btn = (Button)sender;

                bool Stat = produkter.SletProdukt(key, btn.ClassId);

                //Tjekker om produktet er blevet slettet.
                if (Stat)
                {
                    await DisplayAlert("Produkt slettet", "Produktet er slettet", "OK");

                    //Tømmer stack'en
                    stack.Children.Clear();

                    //Henter produkter igen.
                    GenerateElements();
                }
                else
                {
                    await DisplayAlert("Fejl", "Produktet blev ikke slettet", "OK");
                }
            }
        }

        //Metode der generere xaml elementerne.
        public void GenerateElements()
        {
            //Kalder metoden "HentNyheder" og gemmer det i en variabel.
            JObject result = HentProdukter.HentProdukter();

            bool produkt = false;
            int i = 0;

            //Looper igennem for hver nyhed i der er i objected.
            foreach (var produkterenkelt in result["products"])
            {
                //Hvis der er produkter skal den ikke vise "Ingen produkter".
                ingen.IsVisible = false;
                produkt = true;

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
            if (produkt == false)
            {
                ingen.IsVisible = true;
            }
        }
    }
}

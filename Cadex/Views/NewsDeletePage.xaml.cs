using System;
using System.Collections.Generic;
using System.Diagnostics;
using Cadex.Services;
using Cadex.ViewModels;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace Cadex.Views
{
    public partial class NewsDeletePage : ContentPage
    {
        //Instance af klasser og en reference til objected.
        NewsDeleteViewModel nyheder = new NewsDeleteViewModel();
        ListNewsViewModel hentnyheder = new ListNewsViewModel();
        Validate valid = new Validate();

        string key;

        public NewsDeletePage(string key)
        {
            InitializeComponent();

            this.key = key;

            //Kalder metoden "validatekey" og gemmer resultatet.
            bool status = valid.validatekey(key);

            //Tjekker om statussen er true ellers bliver du sendt tilbage til startsiden.
            if (status)
            {
                //Kalder metoden "GenerateElements" og laver xaml elementerne.
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
            //Navigere tilbage til startsiden.
            Application.Current.MainPage = new Nav(key);
        }
        
        async void OnAlertYesNoClicked(object sender, EventArgs e)
        {
            //Opretter en DisplayAlert.
            bool answer = await DisplayAlert("Slet", "Er du sikker på du vil slette denne?", "Ja", "Nej");

            //Tjekker om der bliver trykket ja i DesplayAlert'en.
            if (answer == true)
            {
                //Henter knappen der er blevet trykket på.
                Button btn = (Button)sender;

                //Kalder metoden "SletNyhed" og gemmer resultatet.
                bool Stat = nyheder.SletNyhed(key, btn.ClassId);

                //Tjekker om statussen er true altså om nyheden er slettet.
                if (Stat)
                {
                    await DisplayAlert("Nyhed slettet", "Nyheden er slettet", "OK");

                    //Opdatere xaml'en. Sletter først alt i stack'en for derefter at generere xaml elementerne.
                    stack.Children.Clear();
                    GenerateElements();
                }
                else
                {
                    await DisplayAlert("Fejl", "Nyheden blev ikke slettet", "OK");
                }
            }
        }

        //Metode der generere xaml elementerne.
        public void GenerateElements()
        {
            //Kalder metoden "HentNyheder" og gemmer det i en variabel.
            JObject result = hentnyheder.HentNyheder(key);

            int i = 0;
            bool nyhed = false;

            //Looper igennem for hver nyhed i der er i objected.
            foreach (var nyhederenkelt in result["newsPosts"])
            {
                //Hvis der er nyheder skal den ikke vise "Ingen nyheder".
                ingen.IsVisible = false;
                nyhed = true;

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
                    BackgroundColor = Color.FromHex("#eb4d4b"),
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
            if (nyhed == false)
            {
                ingen.IsVisible = true;
            }
        }
    }
}

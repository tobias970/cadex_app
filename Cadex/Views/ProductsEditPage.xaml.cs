using System;
using System.Collections.Generic;
using Cadex.Services;
using Cadex.ViewModels;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace Cadex.Views
{
    public partial class ProductsEditPage : ContentPage
    {
        ProductsEditViewModel produkter = new ProductsEditViewModel();
        ListProductsViewModel HentProdukter = new ListProductsViewModel();
        Validate valid = new Validate();

        //Opretter list'er med xaml elementer i.
        List<Button> editknapper = new List<Button>();
        List<Button> saveknapper = new List<Button>();
        List<Entry> titlerentry = new List<Entry>();
        List<Entry> beskrivelseentry = new List<Entry>();
        List<Entry> prisentry = new List<Entry>();

        string key;

        public ProductsEditPage(string key)
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
            //Sender tilbage til startsiden.
            Application.Current.MainPage = new Nav(key);
        }

        void Button_edit_pressed(object sender, EventArgs e)
        {
            //Henter knappen der er blevet trykket på.
            Button btn = (Button)sender;

            //Gemmer id'et for knappen.
            string knappen = btn.ClassId;

            //Finder position "edit" i variablen.
            int start = knappen.IndexOf("edit", StringComparison.Ordinal);

            //Gemmer pladsen i array'et.
            int knapperne = Convert.ToInt32(knappen.Substring(0, start));

            //Bruger variablen til at finde de rigtige elementer.
            Button save = saveknapper[knapperne];
            Entry titel = titlerentry[knapperne];
            Entry beskriv = beskrivelseentry[knapperne];
            Entry pris = prisentry[knapperne];

            //Ændre status for xaml elementerne.
            titel.IsEnabled = true;
            beskriv.IsEnabled = true;
            pris.IsEnabled = true;
            save.IsEnabled = true;
            btn.IsEnabled = false;

        }

        void Button_save_pressed(object sender, EventArgs e)
        {
            //Henter knappen der er blevet trykket på.
            Button btn = (Button)sender;

            //Gemmer id'et for knappen.
            string knappen = btn.ClassId;

            //Finder position "edit" i variablen.
            int start = knappen.IndexOf("save", StringComparison.Ordinal);

            //Gemmer pladsen i array'et.
            int knapperne = Convert.ToInt32(knappen.Substring(0, start));

            //Gemmer id'et for produktet.
            int productid = Convert.ToInt32(knappen.Substring(start + 4));

            //Bruger variablen til at finde de rigtige elementer.
            Button edit = editknapper[knapperne];
            Entry titel = titlerentry[knapperne];
            Entry beskriv = beskrivelseentry[knapperne];
            Entry pris = prisentry[knapperne];

            //Kalder metoden "UpdateProdukt" og gemmer resultatet.
            bool Stat = produkter.UpdateProdukt(key, productid, titel.Text, beskriv.Text, pris.Text);

            //Tjekker om statussen er true altså om produktet er slettet.
            if (Stat)
            {
                DisplayAlert("Produkt opdateret", "Produktet er opdateret", "OK");

                titel.IsEnabled = false;
                beskriv.IsEnabled = false;
                pris.IsEnabled = false;
                edit.IsEnabled = true;
                btn.IsEnabled = false;
            }
            else
            {
                DisplayAlert("Fejl", "Nyheden blev ikke opdateret", "OK");
            }
        }

        //Metode der generere xaml elementerne.
        public void GenerateElements()
        {
            //Kalder metoden "HentProdukter" og gemmer det i en variabel.
            JObject result = HentProdukter.HentProdukter();

            int i = 0;

            //Looper igennem for hver produkt "i" der er i objected.
            foreach (var produkterenkelt in result["products"])
            {
                //Opretter variabler med nyhedsnr og handling og id for produktet.
                string editknap = i + "edit" + (string)result["products"][i]["id"];
                string saveknap = i + "save" + (string)result["products"][i]["id"];

                Button edit = new Button
                {
                    Text = "Ret",
                    BorderWidth = 1,
                    WidthRequest = 70,
                    TextColor = Color.Black,
                    ClassId = editknap,
                    BackgroundColor = Color.Orange
                };
                edit.Clicked += Button_edit_pressed;
                editknapper.Add(edit);

                Button save = new Button
                {
                    Text = "Gem",
                    BorderWidth = 1,
                    WidthRequest = 70,
                    TextColor = Color.Black,
                    IsEnabled = false,
                    ClassId = saveknap,
                    BackgroundColor = Color.FromHex("#20bf6b")
                };
                save.Clicked += Button_save_pressed;
                saveknapper.Add(save);

                StackLayout knapper = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Margin = new Thickness(0, 0, 0, 20),
                };
                knapper.Children.Add(edit);
                knapper.Children.Add(save);

                Label title = new Label
                {
                    Text = "Titel : ",
                    VerticalTextAlignment = TextAlignment.Center,
                };
                Entry skrivtitle = new Entry
                {
                    Text = (string)result["products"][i]["name"],
                    WidthRequest = 170,
                    IsEnabled = false,
                    MaxLength = 200
                };
                titlerentry.Add(skrivtitle);

                StackLayout titler = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Margin = new Thickness(0, 0, 0, 10),
                };
                titler.Children.Add(title);
                titler.Children.Add(skrivtitle);


                Label beskrivelse = new Label
                {
                    Text = "Beskrivelse : ",
                    VerticalTextAlignment = TextAlignment.Center,
                };
                Entry skrivbeskrivelse = new Entry
                {
                    Margin = new Thickness(0, 0, 0, 0),
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    MaxLength = 300,
                    WidthRequest = 300,
                    HeightRequest = 150,
                    VerticalTextAlignment = TextAlignment.Start,
                    Text = (string)result["products"][i]["description"],
                    Keyboard = default,
                    IsEnabled = false,
                };
                beskrivelseentry.Add(skrivbeskrivelse);

                StackLayout beskrivelser = new StackLayout
                {
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                };
                beskrivelser.Children.Add(beskrivelse);
                beskrivelser.Children.Add(skrivbeskrivelse);

                Label pris = new Label
                {
                    Text = "Pris : ",
                    VerticalTextAlignment = TextAlignment.Center,
                };
                Entry prisen = new Entry
                {
                    Text = (string)result["products"][i]["price"],
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    MaxLength = 30,
                    WidthRequest = 110,
                    Placeholder = "Pris",
                    Keyboard = Keyboard.Numeric,
                    IsEnabled = false,
                };
                prisentry.Add(prisen);

                StackLayout priser = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                };

                StackLayout alt = new StackLayout
                {

                };
                priser.Children.Add(pris);
                priser.Children.Add(prisen);

                alt.Children.Add(knapper);
                alt.Children.Add(titler);
                alt.Children.Add(beskrivelser);
                alt.Children.Add(priser);

                Frame ramme = new Frame
                {
                    Margin = new Thickness(0, 0, 0, 30),
                    Content = alt,
                };
                stack.Children.Add(ramme);

                i++;
            }
        }
    }
}

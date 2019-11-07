using System;
using System.Collections.Generic;
using Cadex.Services;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace Cadex.Views
{
    public partial class ProductsEditPage : ContentPage
    {
        string key;
        APIMethods apimetoder = new APIMethods();

        List<Button> editknapper = new List<Button>();
        List<Button> saveknapper = new List<Button>();
        List<Entry> titlerentry = new List<Entry>();
        List<Entry> beskrivelseentry = new List<Entry>();
        List<Entry> prisentry = new List<Entry>();

        public ProductsEditPage(string key)
        {
            InitializeComponent();

            this.key = key;

            bool status = apimetoder.validatekey(key);
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

        void Button_edit_pressed(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            Console.WriteLine("EDIT : " + btn.ClassId);
            string knappen = btn.ClassId;
            int start = knappen.IndexOf("edit", StringComparison.Ordinal);
            Console.WriteLine("START nr : " + start);

            int knapperne = Convert.ToInt32(knappen.Substring(0, start));
            Console.WriteLine("KNAPPERNE : " + knapperne);

            Button save = saveknapper[knapperne];
            Entry titel = titlerentry[knapperne];
            Entry beskriv = beskrivelseentry[knapperne];
            Entry pris = prisentry[knapperne];

            titel.IsEnabled = true;
            beskriv.IsEnabled = true;
            pris.IsEnabled = true;
            save.IsEnabled = true;
            btn.IsEnabled = false;

        }

        void Button_save_pressed(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            Console.WriteLine("SAVE : " + btn.ClassId);

            string knappen = btn.ClassId;
            int start = knappen.IndexOf("save", StringComparison.Ordinal);
            //Console.WriteLine("START nr : " + start);

            int knapperne = Convert.ToInt32(knappen.Substring(0, start));
            //Console.WriteLine("KNAPPERNE : " + knapperne);
            int productid = Convert.ToInt32(knappen.Substring(start + 4));
            Console.WriteLine("PRODUCTID : " + productid);

            Button edit = editknapper[knapperne];
            Entry titel = titlerentry[knapperne];
            Entry beskriv = beskrivelseentry[knapperne];
            Entry pris = prisentry[knapperne];

            bool Stat = apimetoder.UpdateProdukt(key, productid, titel.Text, beskriv.Text, pris.Text);

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

        public void GenerateElements()
        {
            JObject result = (JObject)apimetoder.HentProdukter();

            int i = 0;

            foreach (var produkterenkelt in result["products"])
            {
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

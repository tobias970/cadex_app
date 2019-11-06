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

        public ProductsEditPage(string key)
        {
            InitializeComponent();

            this.key = key;

            GenerateElements();
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
            //Console.WriteLine("START nr : " + start);

            int knapperne = Convert.ToInt32(knappen.Substring(0, start));
            //Console.WriteLine("KNAPPERNE : " + knapperne);

            Button save = saveknapper[knapperne];
            Entry titel = titlerentry[knapperne];
            Entry beskriv = beskrivelseentry[knapperne];

            titel.IsEnabled = true;
            beskriv.IsEnabled = true;
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

            bool Stat = apimetoder.UpdateNyhed(key, productid, titel.Text, beskriv.Text);

            if (Stat)
            {
                DisplayAlert("Produkt opdateret", "Produktet er opdateret", "OK");

                titel.IsEnabled = false;
                beskriv.IsEnabled = false;
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
            JObject result = (JObject)apimetoder.HentNyheder(key);

            int i = 0;

            foreach (var nyhederenkelt in result["newsPosts"])
            {
                string editknap = i + "edit" + (string)result["newsPosts"][i]["id"];
                string saveknap = i + "save" + (string)result["newsPosts"][i]["id"];

                Button edit = new Button
                {
                    Text = "Ret",
                    BorderWidth = 1,
                    WidthRequest = 70,
                    TextColor = Color.Black,
                };
                Button save = new Button
                {
                    Text = "Gem",
                    BorderWidth = 1,
                    WidthRequest = 70,
                    TextColor = Color.Black,
                    IsEnabled = false,
                };
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
                    Placeholder = "Titel",
                    WidthRequest = 170,
                    IsEnabled = false,
                };
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
                    MaxLength = 15,
                    WidthRequest = 300,
                    HeightRequest = 150,
                    VerticalTextAlignment = TextAlignment.Start,
                    Placeholder = "Beskrivelse",
                    Keyboard = default,
                    IsEnabled = false,
                };
                StackLayout beskrivelser = new StackLayout
                {
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                };
                beskrivelser.Children.Add(beskrivelse);
                beskrivelser.Children.Add(skrivbeskrivelse);

                /*
                <StackLayout Orientation="Horizontal" HorizontalOptions="CenterAndExpand">
                            <Label Text="Price : " VerticalTextAlignment="Center"/>
                            <Entry Text="" HorizontalOptions="CenterAndExpand" MaxLength="15" WidthRequest="110" Placeholder="Price" Keyboard="Numeric"/>
                        </StackLayout>
                 */
                Label pris = new Label
                {
                    Text = "Pris : ",
                    VerticalTextAlignment = TextAlignment.Center,
                };
                Entry prisen = new Entry
                {
                    Text = "",
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    MaxLength = 15,
                    WidthRequest = 110,
                    Placeholder = "Pris",
                    Keyboard = Keyboard.Numeric,
                    IsEnabled = false,
                };
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

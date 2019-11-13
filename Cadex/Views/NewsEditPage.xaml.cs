using System;
using System.Collections.Generic;
using Cadex.Services;
using Cadex.ViewModels;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace Cadex.Views
{
    public partial class NewsEditPage : ContentPage
    {
        //Instance af klasser og en reference til objected.
        NewsEditViewModel nyheder = new NewsEditViewModel();
        ListNewsViewModel hentnyheder = new ListNewsViewModel();
        Validate valid = new Validate();

        //Opretter list'er med xaml elementer i.
        List<Button> editknapper = new List<Button>();
        List<Button> saveknapper = new List<Button>();
        List<Entry> titlerentry = new List<Entry>();
        List<Entry> beskrivelseentry = new List<Entry>();

        string key;

        public NewsEditPage(string key)
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

            //Ændre status for xaml elementerne.
            titel.IsEnabled = true;
            beskriv.IsEnabled = true;
            save.IsEnabled = true;
            btn.IsEnabled = false;
            
        }

        void Button_save_pressed(object sender, EventArgs e)
        {
            //Henter knappen der er blevet trykket på.
            Button btn = (Button)sender;

            //Gemmer id'et for knappen.
            string knappen = btn.ClassId;

            //Finder position "save" i variablen.
            int start = knappen.IndexOf("save", StringComparison.Ordinal);

            //Gemmer pladsen i array'et.
            int knapperne = Convert.ToInt32(knappen.Substring(0, start));

            //Gemmer id'et for nyheden.
            int newsid = Convert.ToInt32(knappen.Substring(start + 4));

            //Bruger variablen til at finde de rigtige elementer.
            Button edit = editknapper[knapperne];
            Entry titel = titlerentry[knapperne];
            Entry beskriv = beskrivelseentry[knapperne];

            //Kalder metoden "UpdateNyhed" og gemmer resultatet.
            bool Stat = nyheder.UpdateNyhed(key, newsid, titel.Text, beskriv.Text);

            //Tjekker om statussen er true altså om nyheden er slettet.
            if (Stat)
            {
                DisplayAlert("Nyhed opdateret", "Nyheden er opdateret", "OK");

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

        //Metode der generere xaml elementerne.
        public void GenerateElements()
        {
            //Kalder metoden "HentNyheder" og gemmer det i en variabel.
            JObject result = hentnyheder.HentNyheder(key);

            int i = 0;

            //Looper igennem for hver nyhed "i" der er i objected.
            foreach (var nyhederenkelt in result["newsPosts"])
            {
                //Opretter variabler med nyhedsnr og handling og id for nyheden.
                string editknap = i + "edit" + (string)result["newsPosts"][i]["id"];
                string saveknap = i + "save" + (string)result["newsPosts"][i]["id"];

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
                    Text = (string)result["newsPosts"][i]["title"],
                    WidthRequest = 170,
                    MaxLength = 100,
                    IsEnabled = false,
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
                    Text = (string)result["newsPosts"][i]["content"],
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

                StackLayout alt = new StackLayout
                {

                };
                alt.Children.Add(knapper);
                alt.Children.Add(titler);
                alt.Children.Add(beskrivelser);

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

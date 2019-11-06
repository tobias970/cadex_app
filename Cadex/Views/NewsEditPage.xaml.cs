using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Cadex.Views
{
    public partial class NewsEditPage : ContentPage
    {
        string key;
        List<Button> editknapper = new List<Button>();
        List<Button> saveknapper = new List<Button>();

        public NewsEditPage(string key)
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

            Button save = saveknapper[Convert.ToInt32(btn.ClassId.Substring(4))];
            save.IsEnabled = true;
            btn.IsEnabled = false;
        }

        void Button_save_pressed(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            Console.WriteLine("SAVE : " + btn.ClassId);

            Button edit = editknapper[Convert.ToInt32(btn.ClassId.Substring(4))];
            edit.IsEnabled = true;
            btn.IsEnabled = false;
        }

        public void GenerateElements()
        {

            int i = 0;
            while (i < 10)
            {
                string editknap = "edit" + i;
                string saveknap = "save" + i;

                Button edit = new Button
                {
                    Text = "Ret",
                    BorderWidth = 1,
                    WidthRequest = 70,
                    TextColor = Color.Black,
                    ClassId = editknap
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
                    ClassId = saveknap
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

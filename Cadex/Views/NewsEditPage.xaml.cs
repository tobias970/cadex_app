using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Cadex.Views
{
    public partial class NewsEditPage : ContentPage
    {
        public NewsEditPage()
        {
            InitializeComponent();

            GenerateElements();
        }
        void Button_NavBack_Pressed(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new Nav();
        }


        public void GenerateElements()
        {

            int i = 0;
            while (i < 10)
            {
                Button edit = new Button
                {
                    Text = "Edit",
                    BorderWidth = 1,
                    WidthRequest = 70,
                    TextColor = Color.Black,
                };
                Button save = new Button
                {
                    Text = "Save",
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
                    Text = "Title : ",
                    VerticalTextAlignment = TextAlignment.Center,
                };
                Entry skrivtitle = new Entry
                {
                    Placeholder = "Title",
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
                    Text = "Description : ",
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
                    Placeholder = "Description",
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

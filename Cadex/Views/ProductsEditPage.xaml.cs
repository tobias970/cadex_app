using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Cadex.Views
{
    public partial class ProductsEditPage : ContentPage
    {
        string key;

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
        public void GenerateElements()
        {
            /*
            <Frame Margin="0,0,0,30">
                    <StackLayout>
                        <StackLayout Orientation="Horizontal" HorizontalOptions="CenterAndExpand" Margin="0,0,0,20">
                            <Button Text="Edit" BorderWidth="1" WidthRequest="70" TextColor="Black"/>
                            <Button Text="Save" BorderWidth="1" WidthRequest="70" TextColor="Black"/>
                        </StackLayout>
                        <StackLayout Orientation="Horizontal" HorizontalOptions="CenterAndExpand">
                            <Label Text="Title : " VerticalTextAlignment="Center"/>
                            <Entry Placeholder="Title"/>
                        </StackLayout>
                        <StackLayout HorizontalOptions="CenterAndExpand">
                            <Label Text="Description : " VerticalTextAlignment="Center"/>
                            <Entry Text="" Margin="0,0,0,0" HorizontalOptions="CenterAndExpand" MaxLength="15" WidthRequest="300" HeightRequest="150" VerticalTextAlignment="Start" Placeholder="Description" Keyboard="Default"/>
                        </StackLayout>
                        <StackLayout Orientation="Horizontal" HorizontalOptions="CenterAndExpand">
                            <Label Text="Price : " VerticalTextAlignment="Center"/>
                            <Entry Text="" HorizontalOptions="CenterAndExpand" MaxLength="15" WidthRequest="110" Placeholder="Price" Keyboard="Numeric"/>
                        </StackLayout>
                    </StackLayout>
                </Frame>
*/

            int i = 0;
            while (i < 10)
            {
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

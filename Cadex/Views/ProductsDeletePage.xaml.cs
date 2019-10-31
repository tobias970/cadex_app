using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Cadex.Views
{
    public partial class ProductsDeletePage : ContentPage
    {
        public ProductsDeletePage()
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
            /*
            <Frame Padding="10" HorizontalOptions="CenterAndExpand" WidthRequest="260" Margin="0,5,0,0" HasShadow="False" OutlineColor="Black">
                    <StackLayout Orientation="Horizontal">
                        <Button Text="Slet" BorderColor="Black" BorderWidth="1" WidthRequest="70" TextColor="Black" Margin="0,0,10,0"/>
                        <Label Text="Ford Focus RS 500" VerticalTextAlignment="Center" HorizontalOptions="StartAndExpand"/>
                    </StackLayout>
                </Frame>
             */

            int i = 0;
            while (i < 10)
            {

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
                    Margin = new Thickness(0, 0, 10, 0),
                };
                Label titel = new Label()
                {
                    Text = "Ford Focus RS 500",
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

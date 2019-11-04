using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Cadex.Views
{
    public partial class ListNewsPage : ContentPage
    {
        public ListNewsPage(string key)
        {
            InitializeComponent();

            GenerateElements();
        }
        public void GenerateElements()
        {
            int i = 0;
            while (i < 10)
            {
            
                StackLayout tekster = new StackLayout
                {

                };

                Label titel = new Label
                {
                    Text = "Ny Chef",
                    FontSize = Device.GetNamedSize(NamedSize.Subtitle, typeof(Label)),
                };
                Label beskrivelse = new Label
                {
                    Text = "Håber i bliver glade for den nye chef. Han glæder sig til sin første arbejdsdag d. 1 december. PS han tager kage med.",

                };

                tekster.Children.Add(titel);
                tekster.Children.Add(beskrivelse);

                StackLayout omnyhed = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal
                };

                Label forfatter = new Label
                {
                    Text = "tola",
                    FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                };
                Label dato = new Label
                {
                    Text = "04-11-2019",
                    FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                    HorizontalOptions = LayoutOptions.EndAndExpand,
                };
                omnyhed.Children.Add(forfatter);
                omnyhed.Children.Add(dato);

                tekster.Children.Add(omnyhed);

                Frame ramme = new Frame
                {
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    HasShadow = false,
                    BorderColor = Color.Black,
                    Margin = new Thickness(10, 0, 10, 25),
                    WidthRequest = 300,
                    Content = tekster,
                };
                stack.Children.Add(ramme);

                i++;
            }
        }
    }
}

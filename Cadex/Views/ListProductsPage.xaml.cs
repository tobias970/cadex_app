using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Cadex.Views
{
    public partial class ListProductsPage : ContentPage
    {
        public ListProductsPage()
        {
            InitializeComponent();

            GenerateElements();
            
        }

        public void GenerateElements()
        {

            //Frame -> Stacklayout -> Scrollview -> Stacklayout -> Image
            //Frame -> Stacklayout -> Label
            //Frame -> Stacklayout -> Label
            //Frame -> Stacklayout -> Stacklayout -> Label
            //Frame -> Stacklayout -> Stacklayout -> Label
            int i = 0;
            while (i < 20)
            {


                Frame ramme = new Frame
                {
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    HasShadow = false,
                    BorderColor = Color.Black,
                    Margin = new Thickness (10,0,10,30),
                };
                StackLayout Alt = new StackLayout
                {

                };

            
                StackLayout billeder = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                };
                //Loop på denne
                Image billede = new Image
                {
                    Source = "Assets/cadex_virksomhed.jpg",
                };
            
                billeder.Children.Add(billede);

                ScrollView Scroller = new ScrollView
                {
                    Orientation = ScrollOrientation.Horizontal,
                    Content = billeder,
                };

                Label titel = new Label
                {
                    Text = "Ford Focus RS 500",
                    FontSize = Device.GetNamedSize (NamedSize.Subtitle, typeof(Label)),
                };
                Label beskrivelse = new Label
                {
                    Text = "Denne Ford focus er utrolig god og har en top hastighed på 300 km/t. Den er desuden udstyret med nogle gode sæder som giver en god komfort.",
                };

                StackLayout stackpris = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,

                };
                Label pris = new Label
                {
                    Text = "Pris :",
                };
                Label prisen = new Label
                {
                    Text = "500.000",
                };

                stackpris.Children.Add(pris);
                stackpris.Children.Add(prisen);

                Alt.Children.Add(Scroller);
                Alt.Children.Add(titel);
                Alt.Children.Add(beskrivelse);
                Alt.Children.Add(stackpris);

                ramme.Content = Alt;
            

                stack.Children.Add(ramme);

                i++;
            }
        }
    }
}

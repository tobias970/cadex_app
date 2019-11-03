using System;
using System.Collections.Generic;
using Cadex.Services;
using Newtonsoft.Json.Linq;
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

            APIMethods apimetoder = new APIMethods();
            apimetoder.HentProdukter();
            JObject result = apimetoder.result;

            int antalprodukter = (int)result["productCount"];
            Console.WriteLine("PRODUKTER : " + antalprodukter);

            
                //Console.WriteLine("NAVN : " + result["products"][i]["name"]);
                
            


            int i = 0;
            while (i < antalprodukter)
            {


                Frame ramme = new Frame
                {
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    HasShadow = false,
                    BorderColor = Color.Black,
                    Margin = new Thickness (10,0,10,30),
                    WidthRequest = 300,
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
                    Text = (string)result["products"][i]["name"],
                    FontSize = Device.GetNamedSize (NamedSize.Subtitle, typeof(Label)),
                };
                Label beskrivelse = new Label
                {
                    Text = (string)result["products"][i]["description"],
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
                    Text = (string)result["products"][i]["price"],
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

using System;
using System.Collections.Generic;
using System.IO;
using Cadex.Services;
using Cadex.ViewModels;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace Cadex.Views
{
    public partial class ListProductsPage : ContentPage
    {
        //Instance af klasser og en reference til objected.
        ListProductsViewModel produkter = new ListProductsViewModel();

        public ListProductsPage()
        {
            InitializeComponent();

            //Kalder metode når klassen bliver lavet.
            GenerateElements();
           
        }

        //Metode der generere xaml elementerne.
        public void GenerateElements()
        {
            //Gemmer resultatet fra HentProdukter i en variabel.
            JObject result = produkter.HentProdukter();

            int i = 0;
            bool produkt = false;

            //Looper igennem for hver produkt der er i variablen.
            foreach (var produkterenkelt in result["products"])
            {
                //Hvis der er produkter skal den ikke vise "Ingen produkter".
                ingen.IsVisible = false;
                produkt = true;

                Frame ramme = new Frame
                {
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    HasShadow = false,
                    BorderColor = Color.Black,
                    Margin = new Thickness (10,0,10,30),
                    WidthRequest = 300
                };
                StackLayout Alt = new StackLayout
                {
                    
                };
                
                StackLayout billeder = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal
                };

                //Indsætter alle billederne for hver produkt i stacklayoutet ovenfor. 
                int y = 0;
                foreach (var produktbillede in result["products"][i]["images"])
                {
                    if (result["products"][i]["images"].HasValues)
                    {
                        string base64String = (string)result["products"][i]["images"][y]["image"];
                        byte[] Base64Stream = Convert.FromBase64String(base64String);

                        Image billede = new Image
                        {
                            Source = ImageSource.FromStream(() => new MemoryStream(Base64Stream)),
                            WidthRequest = 300,
                            HeightRequest = 200
                        };

                        billeder.Children.Add(billede);
                    }
                    y++;
                }

                //Hvis produktet ikke har et billede indsætter den et standard billede.
                if (!result["products"][i]["images"].HasValues)
                {
                    Image billede = new Image
                    {
                        Source = "Assets/billedesenere.png",
                        WidthRequest = 500,
                        HeightRequest = 200,
                    };

                    billeder.Children.Add(billede);
                }

                ScrollView Scroller = new ScrollView
                {
                    Orientation = ScrollOrientation.Horizontal,
                    Content = billeder
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

            if (produkt == false)
            {
                ingen.IsVisible = true;
            }
        }
    }
}

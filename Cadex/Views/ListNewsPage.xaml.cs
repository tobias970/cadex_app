using System;
using System.Collections.Generic;
using Cadex.Services;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace Cadex.Views
{
    public partial class ListNewsPage : ContentPage
    {
        string key;
        APIMethods apimetoder = new APIMethods();

        public ListNewsPage(string key)
        {
            InitializeComponent();

            this.key = key;

            bool status = apimetoder.validatekey(key);
            if (status)
            {
                GenerateElements();
            }
            else
            {
                DisplayAlert("Fejl", "Du er blevet logget ud", "OK");
                AppSession.login = false;
                Application.Current.MainPage = new Nav(key);
            }
        }
        public void GenerateElements()
        {
            JObject result = (JObject)apimetoder.HentNyheder(key);

            int i = 0;
            foreach (var nyhederenkelt in result["newsPosts"])
            {
                Console.WriteLine("Nyhed : " + i);

                StackLayout tekster = new StackLayout
                {

                };

                Label titel = new Label
                {
                    Text = (string)result["newsPosts"][i]["title"],
                    FontSize = Device.GetNamedSize(NamedSize.Subtitle, typeof(Label)),
                };
                Label beskrivelse = new Label
                {
                    Text = (string)result["newsPosts"][i]["content"],

                };

                tekster.Children.Add(titel);
                tekster.Children.Add(beskrivelse);

                StackLayout omnyhed = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal
                };

                Label forfatter = new Label
                {
                    Text = (string)result["newsPosts"][i]["author"],
                    FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                };

                DateTime utc = (DateTime)result["newsPosts"][i]["created_at"];
                DateTime date = utc.ToLocalTime();
                Label dato = new Label
                {
                    Text = date.ToString("MM/dd/yyyy HH:mm:ss"),
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

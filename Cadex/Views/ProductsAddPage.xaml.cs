using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Cadex.Services;
using Cadex.ViewModels;
using Plugin.Media;
using Xamarin.Forms;

namespace Cadex.Views
{
    public partial class ProductsAddPage : ContentPage
    {
        //Instance af klasser og en reference til objected.
        ProductsAddViewModel produkter = new ProductsAddViewModel();
        Validate valid = new Validate();

        string key;

        public ProductsAddPage(string key)
        {
            InitializeComponent();

            this.key = key;
        }
        void Button_NavBack_Pressed(object sender, System.EventArgs e)
        {
            //Navigere tilbage til startsiden.
            Application.Current.MainPage = new Nav(key);
        }

        void GetPicture_Button(object sender, System.EventArgs e)
        {
            Getbillede();
        }
        private async void Getbillede()
        {
            try
            {
                var media = CrossMedia.Current;

                //Vælg photo
                var file = await media.PickPhotoAsync();

                Console.WriteLine("BILLEDE : " + file);

                // Venter til filen er skrevet
                while (File.ReadAllBytes(file.Path).Length == 0)
                {
                    System.Threading.Thread.Sleep(1);
                }

                //Convertere billedet fra Byte[].
                billedeinput.Source = ImageSource.FromStream(() => new MemoryStream(File.ReadAllBytes(file.Path)));
                Console.WriteLine("DER ER INDSAT ET BILLEDE");
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception Caught: Der er ikke valgt noget billede");
            }
        }

        void Button_OpretProdukt_Pressed(object sender, System.EventArgs e)
        {
            bool status = valid.validatekey(key);
            if (status)
            {
                if (producttitle.Text != "" && productdesc.Text != "" && productpris.Text != "")
                {
                    bool Stat = produkter.OpretProdukt(key, producttitle.Text, productdesc.Text, productpris.Text);
                    if (Stat)
                    {
                        DisplayAlert("Produkt oprettet", "Produktet er oprettet med succes", "OK");

                        producttitle.Text = "";
                        productdesc.Text = "";
                        productpris.Text = "";

                        fejl.IsVisible = false;
                    }
                    else
                    {
                        DisplayAlert("Fejl", "Produktet blev ikke oprettet", "OK");
                    }
                }
                else
                {
                    fejl.IsVisible = true;
                }
            }
            else
            {
                DisplayAlert("Fejl", "Du er blevet logget ud", "OK");
                AppSession.login = false;
                Application.Current.MainPage = new Nav(key);
            }

            
        }
    }
}

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
        string stringbillede = "";

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

        async void GetPicture_ButtonAsync(object sender, System.EventArgs e)
        {
            //Kalder metode der som bruges til at vælge billede fra galleriet.
            await Getbillede();
        }
        private async Task Getbillede()
        {
            try
            {
                var media = CrossMedia.Current;

                //Vælg photo
                var file = await media.PickPhotoAsync();

                // Venter til filen er skrevet
                while (File.ReadAllBytes(file.Path).Length == 0)
                {
                    System.Threading.Thread.Sleep(1);
                }

                //Convertere billedet fra Byte[].
                //billedebyte = File.ReadAllBytes(file.Path);

                //Convertere billede fra Byte[] til Base64.
                stringbillede = Convert.ToBase64String(File.ReadAllBytes(file.Path));

                //Indsætter billedet i imageviewet.
                billedeinput.Source = ImageSource.FromStream(() => new MemoryStream(File.ReadAllBytes(file.Path)));
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception Caught: Der er ikke valgt noget billede");
                Console.WriteLine(e);
            }
        }

        void Button_OpretProdukt_Pressed(object sender, System.EventArgs e)
        {
            bool status = valid.validatekey(key);

            //Tjekker om key er valid.
            if (status)
            {
                //Tjekker om felterne er tomme.
                if (producttitle.Text != "" && productdesc.Text != "" && productpris.Text != "")
                {
                    //Opretter produkt med indholdet fra felterne.
                    var values = produkter.OpretProdukt(key, producttitle.Text, productdesc.Text, productpris.Text);

                    bool Stat = values.Item1;
                    int produktid = values.Item2;

                    //Tjekker om produktet blev oprettet med succes.
                    if (Stat)
                    {
                        //Tjekker om der er valgt et billede.
                        if (stringbillede != "")
                        {
                            //Uploader billede det valgte billede.
                            bool billedestatus = produkter.UploadBillede(key, produktid, stringbillede);

                            //Tjekker om billedet blev uploadet.
                            if (billedestatus)
                            {
                                DisplayAlert("Produkt oprettet", "Produktet er oprettet med succes", "OK");

                                producttitle.Text = "";
                                productdesc.Text = "";
                                productpris.Text = "";
                                billedeinput.Source = null;

                                fejl.IsVisible = false;
                            }
                            else
                            {
                                DisplayAlert("Fejl", "Produktet blev ikke oprettet", "OK");
                            }
                        }
                        else
                        {
                            DisplayAlert("Produkt oprettet uden billede", "Produktet er oprettet med succes", "OK");
                            Console.WriteLine("Der er intet billede..");
                        }                        
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

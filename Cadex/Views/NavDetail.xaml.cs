using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cadex.Services;
using Cadex.ViewModels;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cadex
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavDetail : ContentPage
    {
        //public JObject produkter;
        public NavDetail()
        {
            InitializeComponent();

            //Instance af klassen og en reference til objected.
            HomeViewModel companyinfo = new HomeViewModel();

            //Henter virksomhedsinfo fra "HentVirkInfo" metoden.
            var values = companyinfo.HentVirkInfo();

            //Ændre teksten på labels ud fra værdierne fra metoden "HentVirkInfo".
            infotitle.Text = values.Item1;
            infodesc.Text = values.Item2;
            infotlf.Text = values.Item3;
            infomail.Text = values.Item4;
        }
        
    }
}

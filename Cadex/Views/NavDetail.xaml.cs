using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cadex.Services;
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

            APIMethods apimetoder = new APIMethods();
            //JObject result = (JObject)apimetoder.HentVirkInfo();
            var values = apimetoder.HentVirkInfo();

            infotitle.Text = values.Item1;
            infodesc.Text = values.Item2;
            infotlf.Text = values.Item3;
            infomail.Text = values.Item4;
        }
        
    }
}

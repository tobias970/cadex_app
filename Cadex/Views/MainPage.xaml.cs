using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Cadex
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            //Application.Current.MainPage = new Nav();
        }
        /*void Start(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new Nav();
            //throw new NotImplementedException();
        }*/
    }
    
}

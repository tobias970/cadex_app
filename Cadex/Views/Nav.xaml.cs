﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cadex.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cadex
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(true)]
    public partial class Nav : MasterDetailPage
    {
        //Opretter liste over navigations sider.
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        string key;

        public Nav(string key)
        {
            InitializeComponent();

            this.key = key;

            //Detailsiden dækker mastersiden.
            MasterBehavior = MasterBehavior.Popover;

            //Sætter startsiden til Home.
            MenuPages.Add((int)MenuItemType.Home, (NavigationPage)Detail);
        }

        //Sender dig over til en ny navigations side efter id'et på det menupunkt du har trykket på.
        public async Task NavigateFromMenu(int id)
        {
            //Tjekker om id'et der er blevet trykket på findes i listen.
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.Home:
                        MenuPages.Add(id, new NavigationPage(new MainPage()));
                        break;
                    case (int)MenuItemType.Products:
                        MenuPages.Add(id, new NavigationPage(new ListProductsPage()));
                        break;
                    case (int)MenuItemType.News:
                        MenuPages.Add(id, new NavigationPage(new ListNewsPage(key)));
                        break;
                    case (int)MenuItemType.ManProducts:
                        MenuPages.Add(id, new NavigationPage(new ManProductsPage(key)));
                        break;
                    case (int)MenuItemType.ManNews:
                        MenuPages.Add(id, new NavigationPage(new ManNewsPage(key)));
                        break;
                    case (int)MenuItemType.Login:
                        MenuPages.Add(id, new NavigationPage(new LoginPage()));
                        break;
                    case (int)MenuItemType.Logout:
                        MenuPages.Add(id, new NavigationPage(new LogoutPage(key)));
                        break;
                }
            }
            //Sætter den trykkede side lig med newPage.
            var newPage = MenuPages[id];

            //Sætter ny Detail side efter hvilket menupunkt der bliver trykket på.
            if (newPage != null)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
            
        }
    }
}
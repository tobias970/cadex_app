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
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();

        public Nav()
        {
            InitializeComponent();

            Console.WriteLine("NAV LAVET");

            
                var answer = DisplayAlert("Sikker usømmelig omgang med App", "Er du sikker på du vil starte denne App?", "Yes", "No");
                Debug.WriteLine("Answer: " + answer);

               /* if (answer == true)
                {
                    Console.WriteLine("Slet denne");
                    //Kald metode som sletter news
                }*/
            


            MasterBehavior = MasterBehavior.Popover;

            MenuPages.Add((int)MenuItemType.Home, (NavigationPage)Detail);
        }


        public async Task NavigateFromMenu(int id)
        {
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
                        MenuPages.Add(id, new NavigationPage(new ListNewsPage()));
                        break;
                    case (int)MenuItemType.ManProducts:
                        MenuPages.Add(id, new NavigationPage(new ManProductsPage()));
                        break;
                    case (int)MenuItemType.ManNews:
                        MenuPages.Add(id, new NavigationPage(new ManNewsPage()));
                        break;
                    case (int)MenuItemType.Login:
                        MenuPages.Add(id, new NavigationPage(new LoginPage()));
                        break;
                    case (int)MenuItemType.Logout:
                        MenuPages.Add(id, new NavigationPage(new LogoutPage()));
                        break;
                }
            }

            var newPage = MenuPages[id];

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
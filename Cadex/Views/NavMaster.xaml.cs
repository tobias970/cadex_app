﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cadex
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavMaster : ContentPage
    {

        Nav RootPage { get => Application.Current.MainPage as Nav; }
        List<NavMenuItem> menuItems;
        public NavMaster()
        {
            InitializeComponent();

            menuItems = new List<NavMenuItem>
            {
                new NavMenuItem {Id = MenuItemType.Home, Title="Home" },
                new NavMenuItem {Id = MenuItemType.Login, Title="Login" }
            };

            MenuItemsListView.ItemsSource = menuItems;

            MenuItemsListView.SelectedItem = menuItems[0];

            Console.WriteLine("MenuItems : " + menuItems[0].Id);
            

            MenuItemsListView.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                int id = (int)((NavMenuItem)e.SelectedItem).Id;

                MenuItemsListView.SelectedItem = null;

                await RootPage.NavigateFromMenu(id);
            };

            
        }
    }
}

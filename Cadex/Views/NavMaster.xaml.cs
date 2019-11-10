using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Cadex.Services;
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

            menuItems = CreateMavMenu();

            MenuItemsListView.ItemsSource = menuItems;
            MenuItemsListView.SelectedItem = menuItems[0];

            MenuItemsListView.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                int id = (int)((NavMenuItem)e.SelectedItem).Id;

                MenuItemsListView.SelectedItem = null;

                await RootPage.NavigateFromMenu(id);
            };

            
        }

        private List<NavMenuItem> CreateMavMenu()
        {
            if (AppSession.login)
            {
                menuItems = new List<NavMenuItem>
                {
                    new NavMenuItem {Id = MenuItemType.Home, Title="Hjem" },
                    new NavMenuItem {Id = MenuItemType.Products, Title="Produkter" },
                    new NavMenuItem {Id = MenuItemType.News, Title="Nyheder" },
                    new NavMenuItem {Id = MenuItemType.ManProducts, Title="Håndtere Produkter" },
                    new NavMenuItem {Id = MenuItemType.ManNews, Title="Håndtere Nyheder" },
                    new NavMenuItem {Id = MenuItemType.Logout, Title="Log ud" }
                };
            }
            else
            {
                menuItems = new List<NavMenuItem>
                {
                    new NavMenuItem {Id = MenuItemType.Home, Title="Hjem" },
                    new NavMenuItem {Id = MenuItemType.Products, Title="Produkter" },
                    new NavMenuItem {Id = MenuItemType.Login, Title="Log ind" }
                };
            }

            return menuItems;
        }
    }
}

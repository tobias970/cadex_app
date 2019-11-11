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
        //Opretter Rootpage som klassen "Nav"
        Nav RootPage { get => Application.Current.MainPage as Nav; }

        //Opretter List'en "menuItems"
        List<NavMenuItem> menuItems;

        public NavMaster()
        {
            InitializeComponent();

            //Kalder metoden "CreateMavMenu" og sætter den lig med "menuItems".
            menuItems = CreateMavMenu();

            //Sætter det ind i xaml listview'et.
            MenuItemsListView.ItemsSource = menuItems;

            //Tjekker markeret menuitem.
            MenuItemsListView.SelectedItem = menuItems[0];

            //Navigere om til det valgte markeret menu item.
            MenuItemsListView.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                int id = (int)((NavMenuItem)e.SelectedItem).Id;

                //Fjerner markeringen at menuen.
                MenuItemsListView.SelectedItem = null;

                //Bruger metoden "NavigateFromMenu" fra klassen "Nav" til at navigere om til det valgte menuid.
                await RootPage.NavigateFromMenu(id);
            };   
        }

        //Opretter menuen med titlerne der skal stå på menuen.
        private List<NavMenuItem> CreateMavMenu()
        {
            //Tjekker om du er logget ind ellers får du ikke alle menupunkterne.
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

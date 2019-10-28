using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadex
{
    public enum MenuItemType
    {
        Home,
        Products,
        News,
        ManProducts,
        ManNews,
        Login,
        Logout,
        About
    }
    public class NavMenuItem
    {
        public MenuItemType Id { get; set; }
        public string Title { get; set; }
        public Type TargetType { get; set; }
    }
}

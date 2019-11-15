using System;
using Cadex.Models;

namespace Cadex.ViewModels
{
    public class NewsDeleteViewModel
    {
        NewsModel nyheder = new NewsModel();

        public bool SletNyhed(string token, string identity)
        {
            //Kalder modellen med data og sletter nyheden og returnere det til view'et.
            bool Stat = nyheder.SletNyhed(token, identity);

            return Stat;
        }
    }
}

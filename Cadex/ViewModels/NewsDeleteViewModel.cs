using System;
using Cadex.Models;

namespace Cadex.ViewModels
{
    public class NewsDeleteViewModel
    {
        public bool SletNyhed(string token, string identity)
        {
            //Kalder modellen med data og sletter nyheden og returnere det til view'et.
            NewsModel nyheder = new NewsModel();
            bool Stat = nyheder.SletNyhed(token, identity);

            return Stat;
        }
    }
}

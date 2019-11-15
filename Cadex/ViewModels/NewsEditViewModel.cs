using System;
using Cadex.Models;

namespace Cadex.ViewModels
{
    public class NewsEditViewModel
    {
        NewsModel nyheder = new NewsModel();

        public bool UpdateNyhed(string token, int identity, string overskrift, string beskrivelse)
        {
            //Kalder modellen med data og updatere nyheden og returnere det til view'et.
            bool Stat = nyheder.UpdateNyhed(token, identity, overskrift, beskrivelse);

            return Stat;
        }
    }
}

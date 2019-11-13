using System;
using Cadex.Models;

namespace Cadex.ViewModels
{
    public class NewsEditViewModel
    {
        public bool UpdateNyhed(string token, int identity, string overskrift, string beskrivelse)
        {
            //Kalder modellen med data og updatere nyheden og returnere det til view'et.
            NewsModel nyheder = new NewsModel();

            bool Stat = nyheder.UpdateNyhed(token, identity, overskrift, beskrivelse);

            return Stat;
        }
    }
}

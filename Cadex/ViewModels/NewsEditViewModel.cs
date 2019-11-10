using System;
using Cadex.Models;

namespace Cadex.ViewModels
{
    public class NewsEditViewModel
    {
        public bool UpdateNyhed(string token, int identity, string overskrift, string beskrivelse)
        {
            NewsModel nyheder = new NewsModel();

            bool Stat = nyheder.UpdateNyhed(token, identity, overskrift, beskrivelse);

            return Stat;
        }
    }
}

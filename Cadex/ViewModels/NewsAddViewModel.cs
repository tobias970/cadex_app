using System;
using Cadex.Models;

namespace Cadex.ViewModels
{
    public class NewsAddViewModel : BaseViewModel
    {
        public NewsAddViewModel()
        {
            Title = "Add News";
        }

        public bool OpretNyhed(string token, string overskrift, string beskrivelse)
        {
            //Kalder modellen med data og opretter nyheden og returnere det til view'et.
            NewsModel nyheder = new NewsModel();
            bool Stat = nyheder.OpretNyhed(token, overskrift, beskrivelse);

            return Stat;
        }
    }
}

using System;
using Cadex.Models;
using Newtonsoft.Json.Linq;

namespace Cadex.ViewModels
{
    public class ListNewsViewModel : BaseViewModel
    {
        public ListNewsViewModel()
        {
            Title = "Nyheder";
        }

        public JObject HentNyheder(string key)
        {
            //Kalder modellen med data og returnere til view'et.
            NewsModel nyheder = new NewsModel();
            JObject result = (JObject)nyheder.HentNyheder(key);

            return result;
        }
    }
}

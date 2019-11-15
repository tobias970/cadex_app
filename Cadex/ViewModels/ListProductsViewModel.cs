using System;
using Cadex.Models;
using Newtonsoft.Json.Linq;

namespace Cadex.ViewModels
{
    public class ListProductsViewModel : BaseViewModel
    {
        ProductModel produkter = new ProductModel();

        public ListProductsViewModel()
        {
            Title = "Produkter";
        }

        public JObject HentProdukter()
        {
            //Kalder modellen og returnere til view'et.
            JObject result = (JObject)produkter.HentProdukter();

            return result;
        }
    }
}

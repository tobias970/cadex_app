using System;
using Cadex.Models;
using Newtonsoft.Json.Linq;

namespace Cadex.ViewModels
{
    public class ListProductsViewModel : BaseViewModel
    {
        public ListProductsViewModel()
        {
            Title = "Produkter";
        }

        public JObject HentProdukter()
        {
            ProductModel produkter = new ProductModel();
            JObject result = (JObject)produkter.HentProdukter();

            return result;
        }
    }
}

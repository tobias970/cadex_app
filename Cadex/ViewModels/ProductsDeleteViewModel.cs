using System;
using Cadex.Models;

namespace Cadex.ViewModels
{
    public class ProductsDeleteViewModel
    {
        public bool SletProdukt(string token, string identity)
        {
            //Kalder modellen med data og sletter produktet og returnere det til view'et.
            ProductModel produkter = new ProductModel();
            bool Stat = produkter.SletProdukt(token, identity);

            return Stat;
        }
    }
}

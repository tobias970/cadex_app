using System;
using Cadex.Models;

namespace Cadex.ViewModels
{
    public class ProductsDeleteViewModel
    {
        ProductModel produkter = new ProductModel();

        public bool SletProdukt(string token, string identity)
        {
            //Kalder modellen med data og sletter produktet og returnere det til view'et.
            bool Stat = produkter.SletProdukt(token, identity);

            return Stat;
        }
    }
}

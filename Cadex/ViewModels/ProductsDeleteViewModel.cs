using System;
using Cadex.Models;

namespace Cadex.ViewModels
{
    public class ProductsDeleteViewModel
    {
        public bool SletProdukt(string token, string identity)
        {
            ProductModel produkter = new ProductModel();
            bool Stat = produkter.SletProdukt(token, identity);

            return Stat;
        }
    }
}

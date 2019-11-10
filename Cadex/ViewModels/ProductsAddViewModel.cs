using System;
using Cadex.Models;

namespace Cadex.ViewModels
{
    public class ProductsAddViewModel
    {
        public bool OpretProdukt(string token, string overskrift, string beskrivelse, string pris)
        {
            ProductModel produkter = new ProductModel();
            bool Stat = produkter.OpretProdukt(token, overskrift, beskrivelse, pris);

            return Stat;
        }
    }
}

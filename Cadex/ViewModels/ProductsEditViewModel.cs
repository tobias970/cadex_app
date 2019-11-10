using System;
using Cadex.Models;

namespace Cadex.ViewModels
{
    public class ProductsEditViewModel
    {
        public bool UpdateProdukt(string token, int identity, string overskrift, string beskrivelse, string pris)
        {
            ProductModel produkter = new ProductModel();

            bool Stat = produkter.UpdateProdukt(token, identity, overskrift, beskrivelse, pris);

            return Stat;
        }
    }
}

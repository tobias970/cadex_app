using System;
using Cadex.Models;

namespace Cadex.ViewModels
{
    public class ProductsEditViewModel
    {
        public bool UpdateProdukt(string token, int identity, string overskrift, string beskrivelse, string pris)
        {
            //Kalder modellen med data og updatere produktet og returnere det til view'et.
            ProductModel produkter = new ProductModel();

            bool Stat = produkter.UpdateProdukt(token, identity, overskrift, beskrivelse, pris);

            return Stat;
        }
    }
}

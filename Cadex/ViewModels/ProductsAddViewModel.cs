using System;
using Cadex.Models;

namespace Cadex.ViewModels
{
    public class ProductsAddViewModel
    {
        public (bool, int) OpretProdukt(string token, string overskrift, string beskrivelse, string pris)
        {
            //Kalder modellen med data og opretter produktet og returnere det til view'et.
            ProductModel produkter = new ProductModel();
            var values = produkter.OpretProdukt(token, overskrift, beskrivelse, pris);

            bool Stat = values.Item1;
            int produktid = values.Item2;

            return (Stat, produktid);
        }

        public bool UploadBillede(string token, int produktid, string billede)
        {
            //Kalder modellen med data og uploader billedet til et produktet og returnere det til view'et.
            ProductModel produkter = new ProductModel();
            bool Stat = produkter.UploadBillede(token, produktid, billede);

            return Stat;
        }
    }
}

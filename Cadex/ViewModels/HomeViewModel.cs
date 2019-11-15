using System;
using Cadex.Models;

namespace Cadex.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        CompanyModel companyinfo = new CompanyModel();

        public HomeViewModel()
        {
            Title = "Hjem";
        }

        public (string, string, string, string) HentVirkInfo()
        {
            //Kalder modellen og returnere data til view'et.
            var values = companyinfo.HentVirkInfo();

            string title = values.Item1;
            string desc = values.Item2;
            string tlf = values.Item3;
            string mail = values.Item4;

            return (title, desc, tlf, mail);
        }
    }
}

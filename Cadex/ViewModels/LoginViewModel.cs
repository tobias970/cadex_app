using System;
using Cadex.Models;

namespace Cadex.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        
        public LoginViewModel()
        {
            Title = "Login side";
        }

        public string HentNoegle(string brugernavn, string kodeord)
        {
            AuthModel auth = new AuthModel();
            string result = auth.HentNyNoegle(brugernavn, kodeord);

            return result;
        }
    }
}

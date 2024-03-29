﻿using System;
using Cadex.Models;

namespace Cadex.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        AuthModel auth = new AuthModel();

        public LoginViewModel()
        {
            Title = "Login side";
        }

        public string HentNoegle(string brugernavn, string kodeord)
        {
            //Kalder modellen med data og returnere en nøgle til view'et.
            string result = auth.HentNyNoegle(brugernavn, kodeord);

            return result;
        }
    }
}

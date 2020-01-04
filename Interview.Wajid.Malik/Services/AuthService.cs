using Interview.Wajid.Malik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interview.Wajid.Malik.Services
{
    public class AuthService : IAuthService
    {
        private readonly ClientConfiguration config;
        private string token = null;

        public AuthService(ClientConfiguration config)
        {
            this.config = config;
        }

        public string AuthenticationUrl
        {
            get { return config.AuthenticationUrl; }
        }

        public bool IsAuthenticated
        {
            get { return false; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interview.Wajid.Malik.Services
{
    public interface IAuthService
    {
        string AuthenticationUrl { get; }
        bool IsAuthenticated { get; }
    }
}

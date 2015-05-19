using CustomProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Core.Providers
{
    public class CommonProvider
    {
        private static FunctionProvider functionProvider;
        private static AuthenticationProvider authenticationProvider;
        private static SecurityProvider securityProvider;

        private CommonProvider() { }

        public static FunctionProvider FunctionProvider
        {
            get
            {
                if (functionProvider == null)
                {
                    functionProvider = new FunctionProvider();
                }
                return functionProvider;
            }
        }

        public static AuthenticationProvider AuthenticationProvider
        {
            get
            {
                if (authenticationProvider == null)
                {
                    authenticationProvider = new AuthenticationProvider();
                }
                return authenticationProvider;
            }
        }

        public static SecurityProvider SecurityProvider
        {
            get
            {
                if (securityProvider == null)
                {
                    securityProvider = new SecurityProvider();
                }
                return securityProvider;
            }
        }
    }
}

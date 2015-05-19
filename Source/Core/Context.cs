using CustomProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class Context
    {
        private static FunctionProvider functionProvider;
        private static AuthenticationProvider authenticationProvider;

        private Context() {  }

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
    }
}

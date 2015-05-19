using CustomProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class SingleTon
    {
        private static FunctionProvider functionProvider;

        private SingleTon() { }

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
    }
}

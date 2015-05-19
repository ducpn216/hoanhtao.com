﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Service.CCU
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new Service1() 
            };
            ServiceBase.Run(ServicesToRun);

//#if(!DEBUG)
//          ServiceBase[] ServicesToRun;
//            ServicesToRun = new ServiceBase[] 
//            { 
//                new Service1() 
//            };
//            ServiceBase.Run(ServicesToRun);
//#else
//            Service1 myServ = new Service1();
//            myServ.Start();
//            // here Process is my Service function
//            // that will run when my service onstart is call
//            // you need to call your own method or function name here instead of Process();
//#endif
        }
    }
}

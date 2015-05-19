using Core.API;
using Core.Providers;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.FrontEnd.App_Code
{
    public class Factory
    {
        private static GameAPI gameAPI;
        public static GameAPI GameAPI
        {
            get
            {
                if (gameAPI != null)
                {
                    return gameAPI;
                }
                else
                {
                    gameAPI = new GameAPI();
                    return gameAPI;
                }
            }
        }

        private static PaymentRepository paymentRepository;
        public static PaymentRepository PaymentRepository
        {
            get
            {
                if (paymentRepository != null)
                {
                    return paymentRepository;
                }
                else
                {
                    paymentRepository = new PaymentRepository();
                    return paymentRepository;
                }
            }
        }

        private static PaymentProvider paymentProvider;
        public static PaymentProvider PaymentProvider
        {
            get
            {
                if (paymentRepository != null)
                {
                    return paymentProvider;
                }
                else
                {
                    paymentProvider = new PaymentProvider();
                    return paymentProvider;
                }
            }
        }

        private static UserRepository userRepository;
        public static UserRepository UserRepository
        {
            get
            {
                if (userRepository != null)
                {
                    return userRepository;
                }
                else
                {
                    userRepository = new UserRepository();
                    return userRepository;
                }
            }
        }

        private static SupportRepository supportRepository;
        public static SupportRepository SupportRepository
        {
            get
            {
                if (supportRepository != null)
                {
                    return supportRepository;
                }
                else
                {
                    supportRepository = new SupportRepository();
                    return supportRepository;
                }
            }
        }

        private static ServerRepository serverRepository;
        public static ServerRepository ServerRepository
        {
            get
            {
                if (serverRepository != null)
                {
                    return serverRepository;
                }
                else
                {
                    serverRepository = new ServerRepository();
                    return serverRepository;
                }
            }
        }
    }
}
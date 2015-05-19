using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.BackEnd.App_Code
{
    public class Factory
    {
        private static PidRepository pidRepository;
        private static InsideRepository insideRepository;
        private static ToolLogRepository toolLogRepository;

        public static PidRepository PidRepository
        {
            get
            {
                if (pidRepository == null)
                {
                    pidRepository = new PidRepository();
                    return pidRepository;
                }

                return pidRepository;
            }
        }

        public static InsideRepository InsideRepository
        {
            get
            {
                if (insideRepository == null)
                {
                    insideRepository = new InsideRepository();
                    return insideRepository;
                }

                return insideRepository;
            }
        }

        public static ToolLogRepository ToolLogRepository
        {
            get
            {
                if (toolLogRepository == null)
                {
                    toolLogRepository = new ToolLogRepository();
                    return toolLogRepository;
                }

                return toolLogRepository;
            }
        }

        private static SupportRepository supportRepository;
        public static SupportRepository SupportRepository
        {
            get
            {
                if (supportRepository == null)
                {
                    supportRepository = new SupportRepository();
                    return supportRepository;
                }

                return supportRepository;
            }
        }

        private static UserRepository userRepository;
        public static UserRepository UserRepository
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository();
                    return userRepository;
                }

                return userRepository;
            }
        }
    }
}
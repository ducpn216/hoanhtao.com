using Core;
using Core.API;
using Core.API.GameApiResponses;
using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Service.CCU
{
    public partial class Service1 : ServiceBase
    {
        const string ABSOLUTE_FOLDER_PATH = @"C:\Website\Service.CCU\Logs\";
        private System.Timers.Timer timer;

        public Service1()
        {
            InitializeComponent();

            FileLog.FullErrorPath = ABSOLUTE_FOLDER_PATH;
            DAL.ConnectionConfig.WebConnectionString = ConfigurationManager.ConnectionStrings["WebSqlConnection"].ConnectionString;
            DAL.ConnectionConfig.InsideConnectionString = ConfigurationManager.ConnectionStrings["InsideSqlConnection"].ConnectionString;
            //DAL.ConnectionConfig.WebConnectionString = "data source=10.72.100.5;initial catalog=HT;user id=htlogin;password=dumamay!@#;";
            //DAL.ConnectionConfig.InsideConnectionString = "data source=10.72.100.5;initial catalog=HT_Inside;user id=htlogin;password=dumamay!@#;";
        }

        protected override  void OnStart(string[] args)
        {
            //Start();

             //Set up a timer to trigger every minute.
            this.timer = new System.Timers.Timer();
            this.timer.AutoReset = true;
            this.timer.Interval = 60000; // 60 seconds
            this.timer.Enabled = true;
            this.timer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimer);
            this.timer.Start();

            //Start();
        }

        public async void Start()
        {
            try
            {
                InsideRepository insideRepository = new InsideRepository();
                ServerRepository serverRepository = new ServerRepository();
                GameAPI gameAPI = new GameAPI();

                List<Server> serverList = serverRepository.GetList(true, true);

                foreach (var server in serverList)
                {
                    OnlineNumberResponse response = await gameAPI.GetOnlineNumber(server.ServerId);
                    if (response != null)
                    {
                        int ccu = response.info.Num;
                        FileLog.Write("Start", "Start", "CCU = " + ccu, true);
                        int flag = insideRepository.CacheCCU(ccu, server.ServerId, DateTime.Now);
                        FileLog.Write("Start", "Start", "Flag = " + flag);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                FileLog.Write("Start", "Start", ex.ToString());
                //Log(ex.ToString());
            }
            
        }

        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            try
            {
                FileLog.Write("Timer", "OnTimer", args.SignalTime.ToString("dd/MM/yyyy hh-mm-ss"));

                if (args.SignalTime.Minute % 15 == 0)
                {
                    Start();
                }
            }
            catch (Exception ex)
            {
                FileLog.Write("Timer", "OnTimer", ex.ToString());
            }
            
            // TODO: Insert monitoring activities here.
            //eventLog1.WriteEntry("Monitoring the System", EventLogEntryType.Information, eventId++);
            
        }

        protected override void OnStop()
        {
            this.timer.Stop();
            this.timer = null;
        }

        public static void Log(string str)
        {
            string fileName = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString("00") + "-" + DateTime.Now.Day.ToString("00") + ".txt";

            string fullPath = ABSOLUTE_FOLDER_PATH + fileName;

            using (StreamWriter writer = new StreamWriter(fullPath, true, Encoding.UTF8))
            {
                writer.WriteLine(DateTime.Now.ToString() + "\t " + str);
                
            }
        }
    }
}

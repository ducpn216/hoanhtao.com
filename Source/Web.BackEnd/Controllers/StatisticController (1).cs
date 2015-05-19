using Core.Constants;
using Core.Providers;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Web.BackEnd.App_Code;
using Web.BackEnd.Models;
using Web.BackEnd.Models.Statistic;
using Web.FrontEnd.App_Code.Controllers;

namespace Web.BackEnd.Controllers
{

    public class StatisticController : BackEndController
    {
        InsideRepository insideRepository = new InsideRepository();

        public StatisticController()
        {

        }

        #region CCU
        [HttpGet]
        public ActionResult CCU()
        {
            CCUModel ccuModel = new CCUModel()
            {
                ServerList = BackEndUtilities.GetServerList(null),
                Date = DateTime.Now
            };

            return View(ccuModel);
        }

        public JsonResult GetCCU(int? serverId, string date)
        {
            List<CCUModel> list = new List<CCUModel>();

            DateTime dt = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(date))
            {
                if (!DateTime.TryParse(date, new CultureInfo(GlobalConstants.CULTURE_VIETNAM), DateTimeStyles.None, out dt))
                {
                    dt = DateTime.Now;
                }
                //dt = Convert.ToDateTime(date, new CultureInfo(GlobalConstants.CULTURE_VIETNAM));
            }

            if (serverId.HasValue)
            {
                DataSet dataSet = insideRepository.GetCCU(serverId.Value, dt);

                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    DataTable dataTale = dataSet.Tables[0];

                    foreach (DataRow row in dataTale.Rows)
                    {
                        CCUModel model = new CCUModel()
                        {
                            CCU = Convert.ToInt32(row["CCU"].ToString()),
                            Time = Convert.ToDateTime(row["date"]).ToString("hh:mm")
                        };

                        list.Add(model);
                    }
                }

            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region NRU
        [HttpGet]
        public ActionResult NRU(string from, string to)
        {
            NRUModel nruModel = new NRUModel();

            DateTime fromDate = DateTime.Now;
            DateTime toDate = DateTime.Now;

            if (!string.IsNullOrWhiteSpace(from))
            {
                if (!DateTime.TryParse(from, new CultureInfo(GlobalConstants.CULTURE_VIETNAM), DateTimeStyles.None, out fromDate))
                {
                    fromDate = DateTime.Now;
                }
            }

            if (!string.IsNullOrWhiteSpace(to))
            {
                if (!DateTime.TryParse(to, new CultureInfo(GlobalConstants.CULTURE_VIETNAM), DateTimeStyles.None, out toDate))
                {
                    toDate = DateTime.Now;
                }
            }

            if (!string.IsNullOrWhiteSpace(from) && !string.IsNullOrWhiteSpace(to))
            {
                fromDate = new DateTime(fromDate.Year, fromDate.Month, fromDate.Day, 0, 0, 0);
                toDate = new DateTime(fromDate.Year, fromDate.Month, fromDate.Day, 23, 59, 59);

                nruModel.NRU = insideRepository.GetNRU(fromDate, toDate);
            }

            return View(nruModel);
        }
        #endregion

        #region Report

        [HttpGet]
        public ActionResult Report()
        {
            ReportModel ccuModel = new ReportModel()
            {
                ServerList = BackEndUtilities.GetServerList(null),
            };

            return View(ccuModel);
        }

        [HttpPost]
        public ActionResult Report(ReportModel model)
        {
            List<CCUModel> list = new List<CCUModel>();

            if (model.SelectedServerId > 0 && !string.IsNullOrWhiteSpace(model.From) && !string.IsNullOrWhiteSpace(model.To))
            {
                DateTime fromDate = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(model.From))
                {
                    if (!DateTime.TryParse(model.From, new CultureInfo(GlobalConstants.CULTURE_VIETNAM), DateTimeStyles.None, out fromDate))
                    {
                        fromDate = DateTime.Now;
                    }
                }

                DateTime toDate = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(model.To))
                {
                    if (!DateTime.TryParse(model.To, new CultureInfo(GlobalConstants.CULTURE_VIETNAM), DateTimeStyles.None, out toDate))
                    {
                        toDate = DateTime.Now;
                    }
                }

                DataSet dataSet = insideRepository.GetReport(model.SelectedServerId, fromDate, toDate);
                model.DataList = dataSet;
            }

            model.ServerList = BackEndUtilities.GetServerList(model.SelectedServerId);

            return View(model);
        }
        #endregion

        #region Report

        [HttpGet]
        public ActionResult ReportPid()
        {
            PidRepository pidRepository = new DAL.Repositories.PidRepository();

            ReportPidModel ccuModel = new ReportPidModel()
            {
                PidList = BackEndUtilities.GetPidList()
            };

            return View(ccuModel);
        }

        [HttpPost]
        public ActionResult ReportPid(ReportPidModel model)
        {
            List<CCUModel> list = new List<CCUModel>();

            if (string.IsNullOrEmpty(model.SelectedPid))
            {
                model.SelectedPid = "";
            }
            
            if (!string.IsNullOrWhiteSpace(model.From) && !string.IsNullOrWhiteSpace(model.To))
            {
                DateTime fromDate = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(model.From))
                {
                    if (!DateTime.TryParse(model.From, new CultureInfo(GlobalConstants.CULTURE_VIETNAM), DateTimeStyles.None, out fromDate))
                    {
                        fromDate = DateTime.Now;
                    }
                }

                DateTime toDate = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(model.To))
                {
                    if (!DateTime.TryParse(model.To, new CultureInfo(GlobalConstants.CULTURE_VIETNAM), DateTimeStyles.None, out toDate))
                    {
                        toDate = DateTime.Now;
                    }
                }

                DataSet dataSet = insideRepository.ReportMarketing(model.SelectedPid, fromDate, toDate);
                model.DataList = dataSet;
            }

            model.PidList = BackEndUtilities.GetPidList(model.SelectedPid);

            return View(model);
        }
        #endregion

        #region PaymentHistory

        [HttpGet]
        public ActionResult PaymentHistory()
        {
            PaymentHistoryModel model = new PaymentHistoryModel()
            {
               
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult PaymentHistory(PaymentHistoryModel model)
        {
            List<CCUModel> list = new List<CCUModel>();

            if (!string.IsNullOrEmpty(model.Username))
            {
                model.DataList = Factory.InsideRepository.GetTransferHistory(model.Username);
            }
            else
            {
                model.DataList = null;
            }

            return View(model);
        }
        #endregion


        #region CREV
        [HttpGet]
        public ActionResult CREV()
        {
            CREVModel ccuModel = new CREVModel()
            {
                GetDate = DateTime.Now,
                GeneralTotal = Factory.InsideRepository.GetCREVTotal()
            };

            return View(ccuModel);
        }

        public JsonResult GetCREV(string date)
        {
            List<CREVModel> list = new List<CREVModel>();

            DateTime dt = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(date))
            {
                if (!DateTime.TryParse(date, new CultureInfo(GlobalConstants.CULTURE_VIETNAM), DateTimeStyles.None, out dt))
                {
                    dt = DateTime.Now;
                }
                //dt = Convert.ToDateTime(date, new CultureInfo(GlobalConstants.CULTURE_VIETNAM));
            }

            DataSet dataSet = insideRepository.GetCREV(dt);

            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                DataTable dataTale = dataSet.Tables[0];

                foreach (DataRow row in dataTale.Rows)
                {
                    CREVModel model = new CREVModel()
                    {
                        Total = Convert.ToInt32(row["Total"].ToString()),
                        Date = Convert.ToDateTime(row["Date"]).ToString("dd/MM")
                    };

                    list.Add(model);
                }
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
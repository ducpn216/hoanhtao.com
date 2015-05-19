using Core;
using DAL.Reponses;
using DAL.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.FrontEnd.Controllers
{
    public class TrackingController : Controller
    {
        TrackingRepository trackingRepository = new TrackingRepository();

        public JsonResult sttpaymentpartner(string pkey, DateTime? startdate, DateTime? enddate)
        {
            List<TrackingResponse> list = new List<TrackingResponse>();
            object result = new
            {
                status = 1,
                result = list
            };

            try
            {
                if (!string.IsNullOrWhiteSpace(pkey) && startdate.HasValue && enddate.HasValue)
                {
                    list = trackingRepository.GetTurnOverByDate(pkey, startdate.Value, enddate.Value);
                    result = new
                    {
                        status = 1,
                        result = JsonConvert.SerializeObject(list)
                    };
                }
            }
            catch (Exception ex)
            {
                FileLog.Write("Tracking", "sttpaymentpartner", ex.ToString());
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult sttpaymentpartner_month(string pkey, int? year, int? month)
        {
            List<TrackingResponse> list = new List<TrackingResponse>();
            object result = new
            {
                status = 1,
                result = list
            };

            try
            {
                if (!string.IsNullOrWhiteSpace(pkey) && year.HasValue && month.HasValue)
                {
                    list = trackingRepository.GetTurnOverByMonth(pkey, year.Value, month.Value);
                    result = new
                    {
                        status = 1,
                        result = JsonConvert.SerializeObject(list)
                    };
                }
            }
            catch (Exception ex)
            {
                FileLog.Write("Tracking", "sttpaymentpartner_month", ex.ToString());
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult sttpaymentpartner_hour(string pkey, DateTime? date)
        {
            List<TrackingHourResponse> list = new List<TrackingHourResponse>();
            object result = new
            {
                status = 1,
                result = list
            };

            try
            {
                if (!string.IsNullOrWhiteSpace(pkey) && date.HasValue)
                {
                    list = trackingRepository.GetTurnOverByHour(pkey, date.Value);
                    result = new
                    {
                        status = 1,
                        result = JsonConvert.SerializeObject(list)
                    };
                }
            }
            catch (Exception ex)
            {
                FileLog.Write("Tracking", "sttpaymentpartner_hour", ex.ToString());
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult sttregistpartner(string pkey, DateTime? startdate, DateTime? enddate)
        {
            List<TrackingResponse> list = new List<TrackingResponse>();
            object result = new
            {
                status = 1,
                result = list
            };

            try
            {
                if (!string.IsNullOrWhiteSpace(pkey) && startdate.HasValue && enddate.HasValue)
                {
                    list = trackingRepository.GetRegisterByDate(pkey, startdate.Value, enddate.Value);
                    result = new
                    {
                        status = 1,
                        result = JsonConvert.SerializeObject(list)
                    };
                }
            }
            catch (Exception ex)
            {
                FileLog.Write("Tracking", "sttregistpartner", ex.ToString());
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult sttregistpartner_hour(string pkey, DateTime? date)
        {
            List<TrackingHourResponse> list = new List<TrackingHourResponse>();
            object result = new
            {
                status = 1,
                result = list
            };

            try
            {
                if (!string.IsNullOrWhiteSpace(pkey) && date.HasValue)
                {
                    list = trackingRepository.GetRegisterByHour(pkey, date.Value);
                    result = new
                    {
                        status = 1,
                        result = JsonConvert.SerializeObject(list)
                    };
                }
            }
            catch (Exception ex)
            {
                FileLog.Write("Tracking", "sttregistpartner_hour", ex.ToString());
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult sttregistpartner_month(string pkey, int? year, int? month)
        {
            List<TrackingResponse> list = new List<TrackingResponse>();
            object result = new
            {
                status = 1,
                result = list
            };

            try
            {
                if (!string.IsNullOrWhiteSpace(pkey) && year.HasValue && month.HasValue)
                {
                    list = trackingRepository.GetRegisterByMonth(pkey, year.Value, month.Value);
                    result = new
                    {
                        status = 1,
                        result = JsonConvert.SerializeObject(list)
                    };
                }
            }
            catch (Exception ex)
            {
                FileLog.Write("Tracking", "sttregistpartner_month", ex.ToString());
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult sttloginpartner()
        //{
        //    object result = new
        //    {
        //        status = 1,
        //        result = 0
        //    };

        //    return Json(result);
        //}

        //public JsonResult sttloginpartner_hour()
        //{
        //    object result = new
        //    {
        //        status = 1,
        //        result = 0
        //    };

        //    return Json(result);
        //}

        //public JsonResult sttloginpartner_month()
        //{
        //    object result = new
        //    {
        //        status = 1,
        //        result = 0
        //    };

        //    return Json(result);
        //}

        //public JsonResult Suserplaygame()
        //{
        //    object result = new
        //    {
        //        status = 1,
        //        result = new object[] { "nau" }
        //    };

        //    return Json(result);
        //}
    }
}
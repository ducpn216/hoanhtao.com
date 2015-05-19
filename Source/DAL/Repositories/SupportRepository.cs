using Core;
using DAL;
using DAL.Models;
using DAL.Reponses;
using Domain.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Repositories
{
    public class SupportRepository
    {
        static Dictionary<int, string> supportTypeList = new Dictionary<int, string>()
        {
            {(int)Core.Enums.SupportType.Game, "Lỗi Game"},       
            {(int)Core.Enums.SupportType.Payment, "Nạp tiền"},
            {(int)Core.Enums.SupportType.TransferGold, "Chuyển tiền vào game"},
            {(int)Core.Enums.SupportType.Account, "Thông tin tài khoản"},
            {(int)Core.Enums.SupportType.Hack, "Hack"},
            {(int)Core.Enums.SupportType.Service, "Dịch vụ"},
            {(int)Core.Enums.SupportType.Event, "Sự kiện"},
            {(int)Core.Enums.SupportType.Install, "Lỗi cài đặt"},
            {(int)Core.Enums.SupportType.Other, "Khác"}
        };

        public List<SupportTypeResponse> GetSupportTypeList()
        {
            List<SupportTypeResponse> list = new List<SupportTypeResponse>();

            foreach (KeyValuePair<int, string> item in supportTypeList)
            {
                SupportTypeResponse view = new SupportTypeResponse()
                {
                    SupportTypeId = item.Key,
                    SupportTypeName = item.Value.ToString()
                };

                list.Add(view);
            }

            return list;
        }

        public static string GetSupportTypeName(object supportTypeId)
        {
            if (supportTypeId != null)
            {
                return supportTypeList[Convert.ToInt32(supportTypeId)].ToString();
            }
            else
            {
                return "";
            }
        }

        public List<Support> GetListByUserId(int userId, int page, int numberOfRow, ref int totalRecord, Enums.SupportStatus? status = null)
        {
            List<Support> list = new List<Support>();
            IQueryable<Support> iSupport = DBContext.Context.Supports.Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedDate);

            if (status.HasValue)
            {
                iSupport = iSupport.Where(x => x.Status == (int)status.Value);
            }

            totalRecord = iSupport.Count();

            int startRow = (page - 1) * numberOfRow;

            list = iSupport.Skip(startRow).Take(numberOfRow).ToList();

            return list;
        }

        public List<Support> GetListByStatus(int status, int page, int numberOfRow, ref int totalRecord)
        {
            List<Support> list = new List<Support>();
            IQueryable<Support> iSupport = DBContext.Context.Supports.Where(x => x.Status == status)
                .OrderByDescending(x => x.CreatedDate);

            totalRecord = iSupport.Count();

            int startRow = (page - 1) * numberOfRow;
            list = iSupport.Skip(startRow).Take(numberOfRow).ToList();

            return list;
        }

        public Support GetSupportById(int userId, int supportId)
        {
            Support support = DBContext.Context.Supports.Where(x => x.UserId == userId && x.SupportId == supportId).FirstOrDefault();

            return support;
        }

        public List<SupportDetail> GetSupportDetailList(int userId, int supportId)
        {
            List<SupportDetail> list = new List<SupportDetail>();
            Support support = DBContext.Context.Supports.Where(x => x.UserId == userId && x.SupportId == supportId).FirstOrDefault();

            if (support != null)
            {
                list = DBContext.Context.SupportDetails.Where(x => x.SupportId == supportId)
                    .OrderBy(x => x.SupportDetailId).ToList();
            }

            return list;
        }

        public int Insert(int userId, int serverID, string title, short status, short supportTypeId)
        {
            DAL.Models.Support support = new DAL.Models.Support()
            {
                UserId = userId,
                Title = title,
                CreatedDate = DateTime.Now,
                Status = (short)status,
                SupportTypeId = supportTypeId,
                ServerID = serverID
            };

            DBContext.Context.Supports.InsertOnSubmit(support);
            DBContext.Context.SubmitChanges();

            return support.SupportId;
        }

        public bool UpdateSupportStatus(int supportId, Enums.SupportStatus status, string gmName = "")
        {
            Support support = DBContext.Context.Supports.Where(x => x.SupportId == supportId).FirstOrDefault();

            if (support != null)
            {
                support.Status = (short)status;

                if (!string.IsNullOrEmpty(gmName))
                {
                    support.GM = gmName;
                }

                DBContext.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public int InsertSupportDetail(SupportDetail detail)
        {
            DBContext.Context.SupportDetails.InsertOnSubmit(detail);
            DBContext.Context.SubmitChanges();

            return detail.SupportDetailId;
        }

        #region Delete
        public bool DeleteSupport(int userId, int supportId)
        {
            Support support = DBContext.Context.Supports.Where(x => x.UserId == userId && x.SupportId == supportId).FirstOrDefault();

            if (support != null)
            {
                DBContext.Context.Supports.DeleteOnSubmit(support);

                List<SupportDetail> list = DBContext.Context.SupportDetails.Where(x => x.SupportId == supportId)
                    .OrderByDescending(x => x.CreatedDate).ToList();

                DBContext.Context.SupportDetails.DeleteAllOnSubmit(list);
                DBContext.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool DeleteSupport(int supportId)
        {
            Support support = DBContext.Context.Supports.Where(x => x.SupportId == supportId).FirstOrDefault();

            if (support != null)
            {
                DBContext.Context.Supports.DeleteOnSubmit(support);

                List<SupportDetail> list = DBContext.Context.SupportDetails.Where(x => x.SupportId == supportId)
                    .OrderByDescending(x => x.CreatedDate).ToList();

                DBContext.Context.SupportDetails.DeleteAllOnSubmit(list);
                DBContext.Context.SubmitChanges();
                return true;
            }

            return false;
        }
        #endregion

    }
}

using Core;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class BannerRepository
    {
        public List<Banner> GetList(int gameId, Enums.Status? status, int page, int numberOfRow, ref int total)
        {
            IQueryable<Banner> list = DBContext.Context.Banners.OrderByDescending(x => x.BannerId);

            if (status.HasValue)
            {
                list = list.Where(x => x.Status == Convert.ToInt32(status));
            }

            List<Banner> bannerList = list.Skip((page - 1) * numberOfRow).Take(numberOfRow).ToList();

            if (bannerList != null && bannerList.Count > 0)
            {
                total = list.Count();
            }

            return bannerList;
        }

        public List<Banner> GetList(int gameId, Enums.Status? status, int numberOfRow)
        {
            IQueryable<Banner> list = DBContext.Context.Banners.OrderByDescending(x => x.BannerId);

            if (status.HasValue)
            {
                list = list.Where(x => x.Status == Convert.ToInt32(status));
            }

            List<Banner> bannerList = list.Take(numberOfRow).ToList();
            return bannerList;
        }

        public Banner GetById(int bannerId)
        {
            return DBContext.Context.Banners.Where(x => x.BannerId == bannerId).FirstOrDefault();
        }

        public int Insert(Banner banner)
        {
            DBContext.Context.Banners.InsertOnSubmit(banner);
            DBContext.Context.SubmitChanges();
            return banner.BannerId;
        }

        public bool Update(Banner banner)
        {
            Banner item = GetById(banner.BannerId);
            if (item != null)
            {
                item.ImagePath = (banner.ImagePath != "") ? banner.ImagePath : item.ImagePath;
                item.Title = banner.Title;
                item.Description = banner.Description;
                item.BannerTypeId = banner.BannerTypeId;
                item.Status = banner.Status;
                item.Link = banner.Link;

                DBContext.Context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int bannerId)
        {
            Banner item = GetById(bannerId);
            if (item != null)
            {
                DBContext.Context.Banners.DeleteOnSubmit(item);
                DBContext.Context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

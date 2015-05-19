using Core;
using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.FrontEnd.Models.Abstracts
{
    public class InsideModel
    {
        public InsideModel()
        {
            CamNang = new Models.CamNangModel();

            CategoryRepository categoryRepository = new CategoryRepository();
            var categoryList = categoryRepository.GetAll(Enums.Status.Active);

            CamNang.BiKip = categoryList.Where(x => x.ParentId == (int)Enums.CamNang.BiKipHanhTau).ToList();
            CamNang.GiaTangSucManh = categoryList.Where(x => x.ParentId == (int)Enums.CamNang.GiaTangSucManh).ToList();
            CamNang.HuongDan = categoryList.Where(x => x.ParentId == (int)Enums.CamNang.HuongDanLuyenCap).ToList();
            CamNang.ThongTin = categoryList.Where(x => x.ParentId == (int)Enums.CamNang.ThongTinCoBan).ToList();
            CamNang.TinhNang = categoryList.Where(x => x.ParentId == (int)Enums.CamNang.TinhNang).ToList();
            CamNang.VoLamSoNhap = categoryList.Where(x => x.ParentId == (int)Enums.CamNang.VoLamSoNhap).ToList();
        }

        public CamNangModel CamNang
        {
            get;
            set;
        }

        public int SelectedCategoryId { get; set; }
    }
}
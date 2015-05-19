using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.API
{
    public class ApiEnums
    {
        public enum QueryItemType
        {
            All = 0,
            TuiDo = 1,
            HomDo = 2,
            TrenNguoi = 3,
            DoMuaLaiDuoc = 4,
            GianHang = 5,
            TinVatCu = 6,
            NhiemVu = 7,
            LoLuyenBaoGiap = 8,
            TrangBiThu = 9,
            EpNgoc = 10
        }

        public enum PropertyItemType
        {
            CongKich = 1,
            PhongNgu = 2,
            ThapPhap = 3,
            BaoKich = 4,
            SinhMenh = 5,
            NoiLuc = 6,
            MienGiamSatThuong = 7
        }

        public enum CheckCharDetailType
        {
            All = 0,
            Basic = 1,
            ThuocTinh = 2,
            ThuCuoi = 3,
            DanhKiem = 4,
            BaoGiap = 5,
            ThucChien = 6,
            KinhMach = 7,
            Thu = 8,
            KyNang = 9
        }

        public enum SellStatus
        {
            HaKe = 0,
            LenKe = 1,
            KhongDoi = -1
        }

        public enum SendAll
        {
            None = 0,
            Offline= 1,
            Online = 2,
            
        }
    }
}

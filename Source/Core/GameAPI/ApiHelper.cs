using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.API
{
    public class ApiHelper
    {
        public string GetQueryItemTypeName(ApiEnums.QueryItemType type)
        {
            string value = "";

            switch (type)
            {
                case ApiEnums.QueryItemType.All:
                    value = "Tất cả";
                    break;
                case ApiEnums.QueryItemType.DoMuaLaiDuoc:
                    value = "Đồ mua lại được";
                    break;
                case ApiEnums.QueryItemType.EpNgoc:
                    value = "Ép ngọc";
                    break;
                case ApiEnums.QueryItemType.GianHang:
                    value = "Gian hàng";
                    break;
                case ApiEnums.QueryItemType.HomDo:
                    value = "Hòm đồ";
                    break;
                case ApiEnums.QueryItemType.LoLuyenBaoGiap:
                    value = "Lò luyện bảo giáp";
                    break;
                case ApiEnums.QueryItemType.NhiemVu:
                    value = "Nhiệm vụ";
                    break;
                case ApiEnums.QueryItemType.TinVatCu:
                    value = "Tín vật cũ";
                    break;
                case ApiEnums.QueryItemType.TrangBiThu:
                    value = "Trang bị thử";
                    break;
                case ApiEnums.QueryItemType.TrenNguoi:
                    value = "Trên người";
                    break;
                case ApiEnums.QueryItemType.TuiDo:
                    value = "Túi đồ";
                    break;
                default:
                    break;
            }

            return value;
        }

        public string GetItemPropertyName(ApiEnums.PropertyItemType type)
        {
            string value = "";

            switch (type)
            {
                case ApiEnums.PropertyItemType.BaoKich:
                    value = "Bạo kích";
                    break;
                case ApiEnums.PropertyItemType.CongKich:
                    value = "Công kích";
                    break;
                case ApiEnums.PropertyItemType.MienGiamSatThuong:
                    value = "Miễn giảm sát thương";
                    break;
                case ApiEnums.PropertyItemType.NoiLuc:
                    value = "Nội lực";
                    break;
                case ApiEnums.PropertyItemType.PhongNgu:
                    value = "Phòng ngự";
                    break;
                case ApiEnums.PropertyItemType.SinhMenh:
                    value = "Sinh mệnh";
                    break;
                case ApiEnums.PropertyItemType.ThapPhap:
                    value = "Thân pháp";
                    break;
                default:
                    break;
            }

            return value;
        }
    }
}

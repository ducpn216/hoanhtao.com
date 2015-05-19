using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core
{
    public class Enums
    {
        public enum Games
        {
            HT = 1
        }

        public enum PostType
        {
            News = 1,
            Event = 2,
            CamNang = 3,
            //GioiThieu = 4,
            //TanThu = 5
            FAQ = 6
        }

        public enum ErrorCode
        {
            Success = 1,
            Warning = -1,
            Error = 0
        }

        public enum Status
        {
            Active = 1,
            InActive = 0
        }

        public enum SupportStatus
        {
            NotResolve = 0,
            Resolved = 1,
            Closed = 2
        }

        public enum BannerTypes
        {
            Main = 1,
            Launcher = 2
        }

        public enum PaymentStatus
        {
            OK = 1,
            NumberErrorMax = 2,
            InvalidTime = 3
        }

        public enum ServiceExecutionStatus
        {
            Success = 0,
            Fail = 1
        }

        public enum UserGroup
        {
            Normal = 0,
            Admin = 1,
            GameMaster = 2,
            Editor = 3,
            IT = 4,
            ATM = 5,
            Guest = -1
        }

        public enum LoginStatus
        {
            Success = 0,
            Locked = 1
        }

        public enum SupportType
        {
            Game = 1,
            Payment = 2,
            TransferGold = 3,
            Account = 4,
            Hack = 5,
            Service = 6,
            Event = 7,
            Install = 8,
            Other = 9
        }

        public enum ServerStatus
        {
            New = 1,
            Hot = 2
        }

        public enum Method
        {
            GET,
            POST
        }

        public enum LoginType
        {
            Facebook,
            Google
        }

        public enum SellStatus
        {
            HaKe = 0,
            LenKe = 1,
            KhongThayDoi = -1
        }

        public enum UploadStatus
        {
            Success = 1,
            Fail = 0,
            InvalidExtension = 2,
        }

        public enum CamNang
        {
            BiKipHanhTau = 58,
            ThongTinCoBan = 1,
            VoLamSoNhap = 3,
            HuongDanLuyenCap = 4,
            GiaTangSucManh = 5,
            TinhNang = 6
        }

        public enum BackEndFunction
        {
            CamNang = 1,
            TinTuc = 2,
            Banner = 3,
            Server = 4,
            SendMail = 5,
            AddExp = 6,
            LockAccount = 7,
            DeleteItem = 8,
            JumpMap = 9,
            BanChat = 10,
            KickUser = 11,
            AddMoney = 12,
            MoneyRate = 13

        }
    }
}
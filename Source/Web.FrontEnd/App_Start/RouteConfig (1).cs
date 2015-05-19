using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web.FrontEnd
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "CamNang",
                url: "CamNang",
                defaults: new { controller = "CamNang", action = "Index" }
            );

            routes.MapRoute(
                name: "CamNangList",
                url: "CamNang/DanhSach/{categoryId}",
                defaults: new { controller = "CamNang", action = "List" }
            );

            routes.MapRoute(
                name: "CamNangChitiet",
                url: "CamNang/ChiTiet/{postId}",
                defaults: new { controller = "CamNang", action = "Detail", postId = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "PostDetail",
                url: "TinTuc/ChiTiet/{postId}",
                defaults: new { controller = "Post", action = "Detail", postId = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "PostList",
                url: "TinTuc/DanhSach/{postTypeId}",
                defaults: new { controller = "Post", action = "Index", type = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Game",
                url: "Game/{serverId}",
                defaults: new { controller = "Game", action = "Index", serverId = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ChoiNgay",
                url: "ChoiNgay/",
                defaults: new { controller = "PlayGame", action = "Index", serverId = UrlParameter.Optional }
            );

            #region Account
            routes.MapRoute(
                name: "ForgetPassword",
                url: "QuenMatKhau/",
                defaults: new { controller = "Account", action = "ForgetPassword", serverId = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "ChangePassword",
                url: "DoiMatKhau/",
                defaults: new { controller = "Account", action = "ChangePassword", serverId = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Register",
                url: "DangKy/",
                defaults: new { controller = "Account", action = "Register", serverId = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Login",
                url: "DangNhap/",
                defaults: new { controller = "Account", action = "Login", serverId = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Logout",
                url: "Thoat/",
                defaults: new { controller = "Account", action = "Logout", serverId = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Profile",
                url: "HoSo/",
                defaults: new { controller = "Account", action = "Edit", serverId = UrlParameter.Optional }
            );
            #endregion

            routes.MapRoute(
                name: "NapThe",
                url: "NapThe/",
                defaults: new { controller = "Payment", action = "Index", serverId = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

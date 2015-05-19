using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.BackEnd.App_Code;

namespace Web.BackEnd.Controllers
{
    public class CaptchaController : Controller
    {
        public ActionResult Index()
        {
            FileContentResult result;

            string text = RandomStringGenerator(1).ToLower();
            SessionManager.Captcha = text;

            Random random = new Random();

            using (var memStream = new System.IO.MemoryStream())
            {
                int width = 50;
                int height = 45;

                using (Bitmap bitmap = new Bitmap(width, height))
                {
                    using (Graphics graphic = Graphics.FromImage(bitmap))
                    {
                        HatchBrush hatchBrush = new HatchBrush(HatchStyle.Cross, Color.Orange, Color.White);
                        SolidBrush solidBrush = new SolidBrush(Color.DimGray);
                        Font font = new Font("Arial", 26);

                        //Rectangle rect = new Rectangle(0, 0, 137, 30);
                        graphic.FillRectangle(hatchBrush, 0, 0, width, height);
                        //PointF fpoint = new PointF(10, 3);
                        graphic.DrawString(text, font, solidBrush, 5, 3);

                        Response.ContentType = "image/jpeg";
                        bitmap.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                        result = this.File(memStream.GetBuffer(), "image/jpeg");
                    }
                }
            }

            return result;
        }

        string RandomStringGenerator(int length)
        {
            Random random = new Random();
            string value = "";
            string[] strArray = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", 
                "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", 
                "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", 
                "w", "x", "y", "z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

            int j = 0;
            for (int i = 0; i < length; i++)
            {
                j = random.Next(0, 62);
                value += strArray[j];
            }
            return value;
        }
    }
}
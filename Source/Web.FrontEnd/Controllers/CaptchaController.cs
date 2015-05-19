using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.FrontEnd.App_Code;

namespace Web.FrontEnd.Controllers
{
    public class CaptchaController : Controller
    {
        const int DEFAULT_LENGTH = 3;
        const int QUICK_REGISTER_LENGTH = 2;
        const int FONT_SIZE = 18;
        const int WIDTH = 80;
        const int HEIGHT = 37;

        private static readonly Random getrandom = new Random();
        private static readonly object syncLock = new object();
        public static int GetRandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return getrandom.Next(min, max);
            }
        }

        public ActionResult Index()
        {
            string text = RandomStringGenerator(DEFAULT_LENGTH);
            SessionManager.Captcha = text;

            FileContentResult result = Setup(text, DEFAULT_LENGTH, WIDTH, HEIGHT, 26);
            return result;
        }

        public ActionResult QuickRegister()
        {
            string text = RandomStringGenerator(QUICK_REGISTER_LENGTH);
            SessionManager.QuickRegisterCaptcha = text;

            FileContentResult result = Setup(text, QUICK_REGISTER_LENGTH, 70, 45, 26);
            return result;
        }

        public ActionResult Register()
        {
            string text = RandomStringGenerator(DEFAULT_LENGTH);
            SessionManager.RegisterCaptcha = text;

            FileContentResult result = Setup(text, QUICK_REGISTER_LENGTH, WIDTH, HEIGHT, FONT_SIZE);
            return result;
        }

        public ActionResult Login()
        {
            string text = RandomStringGenerator(DEFAULT_LENGTH);
            SessionManager.LoginCaptcha = text;

            FileContentResult result = Setup(text, QUICK_REGISTER_LENGTH, 50, 45, 26);
            return result;
        }

        #region Setup
        FileContentResult Setup(string text, int length, int width, int height, int fontSize)
        {
            FileContentResult fileResult = null;

            using (var memStream = new System.IO.MemoryStream())
            {
                using (Bitmap bitmap = new Bitmap(width, height))
                {
                    using (Graphics graphic = Graphics.FromImage(bitmap))
                    {
                        HatchBrush hatchBrush = new HatchBrush(HatchStyle.Percent05, Color.Orange, Color.White);
                        //SolidBrush solidBrush = new SolidBrush(Color.DimGray);
                        //Font font = new Font("Arial", 26);

                        //Rectangle rect = new Rectangle(0, 0, 137, 30);
                        SolidBrush blueBrush = new SolidBrush(Color.Yellow);
                        graphic.FillRectangle(hatchBrush, 0, 0, width, height);
                        //PointF fpoint = new PointF(10, 3);

                        float x = 5;
                        char[] array = text.ToArray();

                        for (int i = 0; i < array.Length; i++)
                        {
                            string str = array[i].ToString();

                            Font font = new Font(RandomFont(), FONT_SIZE, RandomFontStyle());
                            SolidBrush solidBrush = new SolidBrush(RandomColor());
                            SizeF stringSize = graphic.MeasureString(str, font);

                            graphic.DrawString(str, font, solidBrush, x, 3);
                            x = x + stringSize.Width;
                        }

                        Response.ContentType = "image/jpeg";
                        bitmap.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                        fileResult = this.File(memStream.GetBuffer(), "image/jpeg");
                    }
                }
            }

            return fileResult;
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
            return value.ToLower();
        }

        string RandomFont()
        {
            //FontFamily[] array = FontFamily.Families;
            //string[] array = { "Arial", "Tahoma", "calibri", "Cursive", "Roman", "Impact", "Verdana", "Comic Sans", "Monospace", 
            //                     "Courier"};
            string[] array = { "Arial", "Tahoma", "sans-serif", "Monospace" };
            int num = GetRandomNumber(0, array.Count() - 1);
            return array[2];
        }

        Color RandomColor()
        {
            Random random = new Random();

            Color[] colorArray = new Color[] { Color.Blue, Color.Red, Color.Black, Color.Brown, Color.DimGray, 
                Color.DarkBlue, Color.DarkGreen, Color.DarkRed, Color.Maroon,  Color.Indigo, Color.MediumBlue, 
                Color.MidnightBlue, Color.Purple, Color.Orchid, Color.SaddleBrown, Color.Sienna, Color.LightSeaGreen
            };

            int max = colorArray.Count();
            int num = GetRandomNumber(0, max - 1);
            //int num = random.Next(0, colorArray.Count() - 1);

            return Color.Blue;
        }

        FontStyle RandomFontStyle()
        {
            return FontStyle.Italic;
        }
        #endregion

    }
}
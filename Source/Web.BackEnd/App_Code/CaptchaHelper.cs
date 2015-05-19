using BotDetect.Web.UI.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.BackEnd.App_Code
{
    public static class CaptchaHelper
    {
        public static MvcCaptcha GetSampleCaptcha()
        {
            // create the control instance
            MvcCaptcha sampleCaptcha = new MvcCaptcha("Captcha");

            // set up client-side processing of the Captcha code input textbox
            sampleCaptcha.UserInputClientID = "CaptchaCode";
            sampleCaptcha.CodeStyle = BotDetect.CodeStyle.Numeric;
            sampleCaptcha.ImageStyle = BotDetect.ImageStyle.BlackOverlap;
            sampleCaptcha.CodeLength = 1;
            sampleCaptcha.ImageSize = new System.Drawing.Size(50, 50);
            return sampleCaptcha;
        }
    }
}
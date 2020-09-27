using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ContactManagement.UI.Models
{
    public class Common
    {
        public static string URLServicePath { get { return ConfigurationManager.AppSettings["URLServicePath"]; } }
    }
}
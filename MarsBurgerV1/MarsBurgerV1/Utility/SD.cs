using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarsBurgerV1.Utility
{
    public static class SD
    {
        public const string EndUserRole = "Customer";
        public const string AdminUserRole = "Admin";

        //-----------------Button-Coloring-Login-Screen----------
        public const string Google = "danger";
        public const string Facebook = "primary";
        public static string GetColorforLogin(string service)
        {
            if (service.ToLower().Equals("google"))
                return Google;
            if (service.ToLower().Equals("facebook"))
                return Facebook;
            return "";
        }
        //-----------------Button-Coloring-Login-Screen----------
    }
}
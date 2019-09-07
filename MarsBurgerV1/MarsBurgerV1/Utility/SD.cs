using MarsBurgerV1.Models;
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
        //-----------------Search-Filters for Orders-------------
        public const string ByOrderStatus = "byOrderStatus";
        public const string byOrderID = "byOrderId";
        //-----------------OrderTypeEnumManagement
        public enum ItemType
        {
            Meal = 1,
            Drink = 2,
            SideDish = 3,
            Addon = 4
        }
        public enum OrderStatus
        {
            OrderReceived = 1,
            InPreperation = 2,
            OnTheWay = 3,
            ReadyForPickup = 4,
            Closed = 5
        }
    }


}
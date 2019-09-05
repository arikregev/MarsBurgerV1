using MarsBurgerV1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarsBurgerV1.Utility
{
    public class ThComparer : IComparer<Thumbnail>
    {
        private string Cookie;
        public ThComparer(string s)
        { 
            Cookie = s;
        }
        public int Compare(Thumbnail x, Thumbnail y)
        {
            int countx = 0;
            int county = 0;
            foreach(char i in Cookie)
            {
                if (i.ToString().Equals(x.MealId.ToString()))
                {
                    countx++;
                    break;
                }
                if (i.ToString().Equals(y.MealId.ToString()))
                    county++;
            }
            return countx - county;
        }
    }
}
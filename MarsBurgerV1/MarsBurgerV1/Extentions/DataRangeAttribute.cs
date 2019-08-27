 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MarsBurgerV1.Extentions
{
    public class DataRangeAttribute : RangeAttribute
    {
        public DataRangeAttribute(string minimumVal) : base(typeof(DateTime), minimumVal, DateTime.Now.ToShortDateString())
        { }
    }
}
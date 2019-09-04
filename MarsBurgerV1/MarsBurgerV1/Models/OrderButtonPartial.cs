using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MarsBurgerV1.Models
{
    public class OrderButtonPartial
    {
        public string ButtonType { get; set; }
        public string Action { get; set; }
        public string Glyph { get; set; }
        public string Text { get; set; }
        public int? OrderId { get; set; }

        public string ActionParameter
        {
            get
            {
                var param = new StringBuilder(@"/");
                if (OrderId != null && OrderId > 0)
                {
                    param.Append(String.Format("{0}", OrderId));
                }
                return param.ToString();
            }

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MarsBurgerV1.Models
{
    public class IndividualButtonPartial
    {
        public string ButtonType { get; set; }
        public string Action { get; set; }
        public string Glyph { get; set; }
        public string Text { get; set; }

        public int? AccountTypeId { get; set; }
        public int? DrinkId { get; set; }
        public string UserId { get; set; }
        public int? AddonID { get; set; }
        public string ActionParameter
        {
            get
            {
                var param = new StringBuilder(@"/");
                if(AccountTypeId != null && AccountTypeId > 0)
                {
                    param.Append(String.Format("{0}", AccountTypeId));
                }
                if (DrinkId != null && DrinkId > 0)
                {
                    param.Append(String.Format("{0}", DrinkId));
                }
                if (AddonID != null && AddonID > 0)
                {
                    param.Append(String.Format("{0}", AddonID));
                }
                if (UserId != null && UserId.Trim().Length > 0)
                {
                    param.Append(String.Format("{0}", UserId));
                }
                return param.ToString();
            }
            
        }
    }
}
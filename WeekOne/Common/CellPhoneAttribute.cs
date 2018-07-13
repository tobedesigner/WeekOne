using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace WeekOne.Common {
    public class CellPhoneAttribute : DataTypeAttribute {
        public CellPhoneAttribute() : base(DataType.Text) {
            ErrorMessage = "手機格式請參照 OOOO-OOOOOO";
        }

        public override bool IsValid(object value) {
            string phone = (string)value, 
                pattern = @"\d{4}-\d{6}";

            if(string.IsNullOrEmpty(phone)) {
                return true;
            }

            Regex rgx = new Regex(pattern);

            return rgx.IsMatch(phone);
        }
    }
}
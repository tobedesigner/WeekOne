using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeekOne.Models;

namespace WeekOne.Common {
    public class Selector {
        public static List<SelectListItem> GetCList() {
            var list = new List<SelectListItem> { 
                new SelectListItem { Text = "客戶名稱", Value = "1" },
                new SelectListItem { Text = "統一編輯", Value = "2" },
                new SelectListItem { Text = "電話", Value = "3" },
                new SelectListItem { Text = "傳真", Value = "4" },
                new SelectListItem { Text = "地址", Value = "5" }
            };

            return list;
        }

        public static List<SelectListItem> GetJobTitle()
        {
            客戶聯絡人Repository CCRepo = RepositoryHelper.Get客戶聯絡人Repository();
            var list = new List<SelectListItem>();
            var data = CCRepo.All();

            list.Add(new SelectListItem { Text = "全部", Value = "" });
            foreach (var item in data)
            {
                list.Add(new SelectListItem { Text = item.職稱, Value = item.職稱 });
            }

            return list;
        }

        /// <summary>
        /// 客戶分類清單
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> GetCustomerClass()
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem { Text = "無", Value = "0" },
                new SelectListItem { Text = "分類一", Value = "1" },
                new SelectListItem { Text = "分類二", Value = "2" },
                new SelectListItem { Text = "分類三", Value = "3" }
            };

            return list;
        }
    }
}
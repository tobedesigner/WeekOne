using System;
using System.Linq;
using System.Collections.Generic;
using WeekOne.Common;

namespace WeekOne.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{
        public override IQueryable<客戶資料> All()
        {
            return base.All().Where(c => c.是否已刪除 == false);
        }

        public override void Delete(客戶資料 entity)
        {
            entity.是否已刪除 = true;
        }

        public 客戶資料 Find(int? id) {
            return All().FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<客戶資料> Search(string key)
        {
            return All().Where(c => c.客戶名稱.Contains(key));
        }

        public IQueryable<客戶資料> Search(string sortBy, string key) {
            var data = All();
            switch(sortBy) {
                case "1":
                    data = data.Where(c => c.客戶名稱.Contains(key));
                    break;
                case "2":
                    data = data.Where(c => c.統一編號.Contains(key));
                    break;
                case "3":
                    data = data.Where(c => c.電話.Contains(key));
                    break;
                case "4":
                    data = data.Where(c => c.傳真.Contains(key));
                    break;
                case "5":
                    data = data.Where(c => c.地址.Contains(key));
                    break;
                default:
                    break;
            }

            return data;
        }
    }

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}
using System;
using System.Linq;
using System.Collections.Generic;
	
namespace WeekOne.Models
{   
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{
        public override IQueryable<客戶銀行資訊> All()
        {
            return base.All().Where(c => c.是否已刪除 == false);
        }

        public override void Delete(客戶銀行資訊 entity)
        {
            entity.是否已刪除 = true;
        }

        public void Delete(int id) {
            var list = All().Where(c => c.客戶Id == id);
            foreach(var item in list) {
                item.是否已刪除 = true;
            }
        }

        public 客戶銀行資訊 Find(int? id)
        {
            return All().FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<客戶銀行資訊> Search(string key)
        {
            return All().Where(c => c.銀行名稱 == key);
        }
	}

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}
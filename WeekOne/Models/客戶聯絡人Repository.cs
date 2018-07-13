using System;
using System.Linq;
using System.Collections.Generic;
	
namespace WeekOne.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
        public override IQueryable<客戶聯絡人> All()
        {
            return base.All().Where(c => c.是否已刪除 == false);
        }

        public override void Delete(客戶聯絡人 entity)
        {
            entity.是否已刪除 = true;
        }

        public void Delete(int id) {
            var list = All().Where(c => c.客戶Id == id);
            foreach(var item in list) {
                item.是否已刪除 = true;
            }
        }

        public 客戶聯絡人 Find(int? id)
        {
            return All().FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<客戶聯絡人> Search(string key)
        {
            return All().Where(c => c.姓名.Contains(key));
        }

        public IEnumerable<客戶聯絡人> Filter(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return All();
            }
            else
            {
                return All().Where(c => c.職稱 == key);
            }
        }

        public bool IsExistEmail(int 客戶id, string mail, string name)
        {
            var result = All().Where(c => c.客戶Id == 客戶id && c.Email == mail && c.姓名 != name);

            return result.Count() > 0 ? true : false;
        }
	}

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}
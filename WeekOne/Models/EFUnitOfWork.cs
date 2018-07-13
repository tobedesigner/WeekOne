using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace WeekOne.Models
{
	public class EFUnitOfWork : IUnitOfWork
	{
		public DbContext Context { get; set; }

		public EFUnitOfWork()
		{
			Context = new 客戶資料Entities();
		}

		public void Commit()
		{
            try
            {
			    Context.SaveChanges();

            }
            catch (DbEntityValidationException ex) {
                var errorMessage = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                throw new DbEntityValidationException(errorMessage.ToString(), ex.EntityValidationErrors);
            }
            //{
            //    var entityError = ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage);
            //    var getFullMessage = string.Join("; ", entityError);
            //    var exceptionMessage = string.Concat(ex.Message, "errors are: ", getFullMessage);
            //    //NLog
            //    //LogException(new Exception(string.Format("File : {0} {1}.", logFile.FullName, exceptionMessage), ex));
            //    throw ex;
            //}
		}
		
		public bool LazyLoadingEnabled
		{
			get { return Context.Configuration.LazyLoadingEnabled; }
			set { Context.Configuration.LazyLoadingEnabled = value; }
		}

		public bool ProxyCreationEnabled
		{
			get { return Context.Configuration.ProxyCreationEnabled; }
			set { Context.Configuration.ProxyCreationEnabled = value; }
		}
		
		public string ConnectionString
		{
			get { return Context.Database.Connection.ConnectionString; }
			set { Context.Database.Connection.ConnectionString = value; }
		}
	}
}

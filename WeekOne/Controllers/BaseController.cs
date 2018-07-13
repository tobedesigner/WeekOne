using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeekOne.Models;

namespace WeekOne.Controllers
{
    public abstract class BaseController : Controller {
        public  IUnitOfWork unitOfWork = RepositoryHelper.GetUnitOfWork();
    }
}
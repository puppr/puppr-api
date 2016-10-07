using Puppr.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Puppr.API.Infrastructure
{
    public class SecuredController : ApiController
    {
        protected PupprDataContext Db = new PupprDataContext();

        protected Owner CurrentUser
        {
            get
            {
                return Db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                Db.Dispose();
            }   
        }
    }
}
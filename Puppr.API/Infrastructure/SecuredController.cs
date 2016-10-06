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
        protected PupprDataContext db = new PupprDataContext();

        protected Owner CurrentUser
        {
            get
            {
                return db.Users.FirstOrDefault(u => u.UserName == this.User.Identity.Name);
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }
    }
}
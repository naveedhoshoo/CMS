using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CMS
{
    public class UserController : ApiController
    {
        public string SendEmail(string Emailid)
        {
            string emailVerified = "Email Verified";
            return emailVerified;

        }
    }
}
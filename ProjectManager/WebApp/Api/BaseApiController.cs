using Serilog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace WebApp.Api
{
    public class BaseApiController : ApiController
    {
        // Declaration
        public delegate IHttpActionResult WebDelegateMethod();

        protected virtual IHttpActionResult Try(WebDelegateMethod method)
        {
            try
            {
               return method();
            }
            catch (Exception exception)
            {
                Log.Fatal(exception.Message, exception);
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }
    }
}
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
                if (!ModelState.IsValid)
                {
                    Log.Fatal("model state is not valid");
                    return BadRequest(ModelState);
                }
                return method();
            }
            catch (KeyNotFoundException execKeyNotFound)
            {
                Log.Fatal(execKeyNotFound.Message, execKeyNotFound);
                return BadRequest(execKeyNotFound.Message);
            }
            catch (ArgumentNullException execArgumentNullException)
            {
                Log.Fatal(execArgumentNullException.Message, execArgumentNullException);
                return BadRequest(execArgumentNullException.Message);
            }
            catch (ArgumentException execArgumentException)
            {
                Log.Fatal(execArgumentException.Message, execArgumentException);
                return BadRequest(execArgumentException.Message);
            }
            catch (Exception exception)
            {
                Log.Fatal(exception.Message, exception);
                //return StatusCode(HttpStatusCode.InternalServerError);
                throw exception;
            }
        }
    }
}
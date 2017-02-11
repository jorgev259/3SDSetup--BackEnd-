using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;

namespace sdHelper.Controllers
{
    public class cleanerController : ApiController
    {
        [HttpPost]
        public string test([FromBody]string stamp)
        {
            var response = "Success!";

            try{
                var server = HttpContext.Current.Server.MapPath("~/temp/");
                Directory.Delete(server + "downloads" + stamp, true);
                Directory.Delete(server + stamp, true);
                File.Delete(server + stamp + ".zip");
            }
            catch (Exception e)
            {
                response = e.Message;
            }

            return response;
        }
    }
}

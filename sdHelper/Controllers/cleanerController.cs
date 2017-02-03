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
                var folder = stamp.Remove(stamp.Length - 4);
                Directory.Delete(server + "downloads" + folder, true);
                Directory.Delete(server + folder, true);
                File.Delete(server + stamp);
            }
            catch (Exception e)
            {
                response = e.Message;
            }

            return response;
        }
    }
}

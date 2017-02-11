using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using sdHelper.Models;
using System.Web.Http.Cors;


namespace sdHelper.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class handlerController : ApiController
    {
        [HttpGet]
        public async System.Threading.Tasks.Task<HttpResponseMessage> Get(String ver, String step, String stamp)
        {
            var server = HttpContext.Current.Server;
            var Request = HttpContext.Current.Request;
            var req_data = JsonConvert.DeserializeObject<dynamic>(ver);
            var step_list = JsonConvert.DeserializeObject<dynamic>(step);

            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/temp/" + stamp));
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/temp/" + "downloads" + stamp));

            for (int i = 0; i < step_list.Count; i++)
            {
                String actual_step = step_list[i].ToString();
                switch (actual_step)
                {
                    case "soundhax":
                        strap.soundhax_step(req_data, stamp);
                        break;
                    case "d9(hb)":
                        await strap.d9_hb(stamp);
                        break;
                    case "install":
                        await strap.install(stamp,req_data);
                        break;
                }
            }

            var response = strap.pack(stamp);

            return response;

        }
    }
}

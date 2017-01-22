using Octokit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace sdHelper.Models
{
    public class strap
    {
        //Code adapted from smealum.github.io/3ds/ (Not mine)
         public static void payload_url(dynamic req_data, string folder)
        {
            List<String> v = new List<string>();

            for (int i = 1; i < ((ICollection)req_data).Count; i++)
            {
                v.Add(req_data[i.ToString()].Value.ToString());
            }

            v.Add(req_data["0"].Value.ToString());

            var url = "http://smealum.github.io/ninjhax2/JL1Xf2KFVm/otherapp/" + getFilenameFromVersion(v) + ".bin";

            download_from_url(url, folder, "otherapp.bin");
        }

        public static string getRegion(List<String> v)
        {
            return v[4];
        }

        public static string getFirmVersion(List<String> v)
        {
            if (v[5] == "NEW")
            {
                return "N3DS";
            }
            else
            {
                if (Convert.ToInt32(Convert.ToInt32(v[0])) < 5)
                {
                    return "PRE5";
                }
                else
                {
                    return "POST5";
                }
            }
        }

        public static string getMenuVersion(List<String> v)
        {
            if (v[4] == "K")
            {
                if (Convert.ToInt32(Convert.ToInt32(v[0])) == 9)
                {
                    if (Convert.ToInt32(Convert.ToInt32(v[1])) == 6)
                    {
                        return "6166_kor";
                    }
                    else if (Convert.ToInt32(v[1]) > 6)
                    {
                        return "7175_kor";
                    }
                }
                else if (Convert.ToInt32(v[0]) == 10)
                {
                    if (Convert.ToInt32(v[1]) == 0)
                    {
                        return "7175_kor";
                    }
                    else if (Convert.ToInt32(v[1]) == 1)
                    {
                        return "8192_kor";
                    }
                    else if (Convert.ToInt32(v[1]) == 2)
                    {
                        return "9216_kor";
                    }
                    else if (Convert.ToInt32(v[1]) == 3)
                    {
                        return "10240_kor";
                    }
                    else if (Convert.ToInt32(v[1]) >= 6)
                    {
                        return "12288_kor";
                    }
                    else if (Convert.ToInt32(v[1]) >= 4)
                    {
                        return "11266_kor";
                    }
                }
                else if (Convert.ToInt32(v[0]) == 11)
                {
                    if (Convert.ToInt32(v[1]) == 0)
                    {
                        return "12288_kor";
                    }
                    else if (Convert.ToInt32(v[1]) == 1 || Convert.ToInt32(v[1]) == 2)
                    {
                        return "13312_kor";
                    }
                }
            }
            else
            {
                if (Convert.ToInt32(v[0]) == 9)
                {
                    if (Convert.ToInt32(v[1]) == 0 || Convert.ToInt32(v[1]) == 1)
                    {
                        return "11272";
                    }
                    else if (Convert.ToInt32(v[1]) == 2)
                    {
                        return "12288";
                    }
                    else if (Convert.ToInt32(v[1]) == 3)
                    {
                        return "13330";
                    }
                    else if (Convert.ToInt32(v[1]) == 4)
                    {
                        return "14336";
                    }
                    else if (Convert.ToInt32(v[1]) == 5)
                    {
                        return "15360";
                    }
                    else if (Convert.ToInt32(v[1]) == 6)
                    {
                        return "16404";
                    }
                    else if (Convert.ToInt32(v[1]) == 7)
                    {
                        return "17415";
                    }
                    else if (Convert.ToInt32(v[1]) == 9 && v[4] == "U")
                    {
                        return "20480_usa";
                    }
                    else if (Convert.ToInt32(v[1]) >= 8)
                    {
                        return "19456";
                    }
                }
                else if (Convert.ToInt32(v[0]) == 10)
                {
                    if (Convert.ToInt32(v[1]) == 0)
                    {
                        if (v[4] == "U")
                        {
                            return "20480_usa";
                        }
                        else
                        {
                            return "19456";
                        }
                    }
                    else if (Convert.ToInt32(v[1]) == 1)
                    {
                        if (v[4] == "U")
                        {
                            return "21504_usa";
                        }
                        else
                        {
                            return "20480";
                        }
                    }
                    else if (Convert.ToInt32(v[1]) == 2)
                    {
                        if (v[4] == "U")
                        {
                            return "22528_usa";
                        }
                        else
                        {
                            return "21504";
                        }
                    }
                    else if (Convert.ToInt32(v[1]) == 3)
                    {
                        if (v[4] == "U")
                        {
                            return "23552_usa";
                        }
                        else
                        {
                            return "22528";
                        }
                    }
                    else if (Convert.ToInt32(v[1]) == 4 || Convert.ToInt32(v[1]) == 5)
                    {
                        if (v[4] == "U")
                        {
                            return "24578_usa";
                        }
                        else
                        {
                            return "23554";
                        }
                    }
                    else if (Convert.ToInt32(v[1]) >= 6)
                    {
                        if (v[4] == "U")
                        {
                            return "25600_usa";
                        }
                        else
                        {
                            return "24576";
                        }
                    }
                }
                else if (Convert.ToInt32(v[0]) == 11)
                {
                    if (Convert.ToInt32(v[1]) == 0)
                    {
                        if (v[4] == "U")
                        {
                            return "25600_usa";
                        }
                        else
                        {
                            return "24576";
                        }
                    }
                    else
                    {
                        if (v[4] == "U")
                        {
                            return "26624_usa";
                        }
                        else
                        {
                            return "25600";
                        }
                    }
                }
            }

            return "default";
        }


        public static string getMsetVersion(List<String> v)
        {
            if (Convert.ToInt32(v[0]) == 9 && Convert.ToInt32(v[1]) < 6)
            {
                return "8203";
            }
            else
            {
                return "9221";
            }
        }

        public static string getFilenameFromVersion(List<String> v)
        {
            return getFirmVersion(v) + "_" + getRegion(v) + "_" + getMenuVersion(v) + "_" + getMsetVersion(v);
        }
        //End of smealum code

        //Concatenates soundhax url and downloads the file
         public static void download_soundhax(dynamic req_data, string folder)
        {
            var server = HttpContext.Current.Server;

            String console = req_data["0"].Value.ToString();
            String region = req_data["5"].Value.ToString();

            switch (console)
            {
                case "OLD":
                    console = "o3ds";
                    break;

                case "NEW":
                    console = "n3ds";
                    break;

                default:
                    break;
            }

            switch (region)
            {
                case "E":
                    region = "eur";
                    break;
                case "U":
                    region = "usa";
                    break;
                case "J":
                    region = "jpn";
                    break;
                case "K":
                    region = "kor";
                    break;
                default:
                    break;
            }

            var url = "https://github.com/nedwill/soundhax/raw/master/soundhax-" + region + "-" + console + ".m4a";
            var filename = "soundhax-" + region + "-" + console + ".m4a";
            strap.download_from_url(url, folder, filename);
            Directory.Move(server.MapPath("~/temp/" + "downloads" + folder + "/" + filename), server.MapPath("~/temp/" + folder + "/" + filename));
        }

        //Extracts zip file
         public static void extract_zip(string filename, string folder)
        {
            string zipPath = HttpContext.Current.Server.MapPath("~/temp/" + "downloads" + folder + "/" + filename);
            string extractPath = HttpContext.Current.Server.MapPath("~/temp/" + folder + "/");

            ZipFile.ExtractToDirectory(zipPath, extractPath);
            
        }

        //Downloads file from specififed route and saves it on temporary folder
         public static void download_from_url(String url, String folder, String filename)
        {
            using (WebClient webClient = new WebClient())
            {
                var path = HttpContext.Current.Server.MapPath("~/temp/" + "downloads" + folder + "/" + filename);
                webClient.DownloadFile(url, path);
                
            }
        }

        //Get latest release url from github repo
        public static async Task<String> repo_url(string author, string repo)
        {
            var client = new GitHubClient(new Octokit.ProductHeaderValue("my-cool-app"));
            var releases = await client.Repository.Release.GetAll(author, repo);
            return releases[0].Assets[0].BrowserDownloadUrl;
        }

        //Sets soundhax as hb entrypoint
        public static void soundhax_step(dynamic req_data, string stamp)
        {
            var server = HttpContext.Current.Server;

            strap.payload_url(req_data, stamp);
            strap.download_soundhax(req_data, stamp);
            if (!File.Exists(server.MapPath("~/temp/" + "downloads" + stamp + "/starter.zip")))
            {
                download_from_url("http://smealum.github.io/ninjhax2/starter.zip", stamp, "starter.zip");
            }
            strap.extract_zip("starter.zip", stamp);
            Directory.Move(server.MapPath("~/temp/" + stamp + "/starter/3ds"), server.MapPath("~/temp/" + stamp + "/3ds"));
            Directory.Move(server.MapPath("~/temp/" + stamp + "/starter/boot.3dsx"), server.MapPath("~/temp/" + stamp + "/boot.3dsx"));
            Directory.Move(server.MapPath("~/temp/" + "downloads" + stamp + "/otherapp.bin"), server.MapPath("~/temp/" + stamp + "/otherapp.bin"));
            Directory.Delete(server.MapPath("~/temp/" + stamp + "/starter"));
        }

        //Sets everything ready to run decrypt9 from hbl
        public static async Task d9_hb(string stamp)
        {
            var server = HttpContext.Current.Server;

            Directory.CreateDirectory(server.MapPath("~/temp/" + stamp + "/files9"));
            var url = await strap.repo_url("d0k3", "Decrypt9WIP");
            download_from_url(url, stamp, "d9.zip");
            extract_file("d9.zip", "Decrypt9WIP.bin", "safehaxpayload.bin", "", stamp);
        }

        //Extracts desired file to a path
        public static void extract_file(string zip,string filename_input,string filename_output,string folder,string stamp)
        {
            var server = HttpContext.Current.Server;
            using (ZipArchive archive = ZipFile.OpenRead(server.MapPath("~/temp/" + "downloads" + stamp + "/" + zip)))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (entry.FullName == filename_input)
                    {
                        entry.ExtractToFile(Path.Combine(server.MapPath("~/temp/" + stamp + "/" + folder), filename_output), true);
                    }
                }
            }
        }

        //Packs the folder to a .zip file
        public static HttpResponseMessage pack(string name){
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            var path = HttpContext.Current.Server.MapPath("~/temp/");

            ZipFile.CreateFromDirectory(path + name, path + name + ".zip");
            string filename = name + ".zip";
            string filePath = HttpContext.Current.Server.MapPath("~/temp/") + filename;


            using (MemoryStream ms = new MemoryStream())
            {
                using (FileStream file = new FileStream(filePath, System.IO.FileMode.Open, FileAccess.Read))
                {
                    byte[] bytes = new byte[file.Length];
                    file.Read(bytes, 0, (int)file.Length);
                    ms.Write(bytes, 0, (int)file.Length);

                    
                    httpResponseMessage.Content = new ByteArrayContent(bytes.ToArray());
                    httpResponseMessage.Content.Headers.Add("x-filename", filename);
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    httpResponseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = filename;
                    httpResponseMessage.StatusCode = HttpStatusCode.OK;
                    
                }                
            }
            

            return httpResponseMessage;
        }
    }
}
using Octokit;
using SevenZip;
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
            var server = HttpContext.Current.Server.MapPath("~/temp/");

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
            Directory.Move(server + "downloads" + folder + "/" + filename, server + folder + "/" + filename);
        }

        //Extracts zip file
         public static void extract_zip(string filename, string folder)
        {
            string zipPath = HttpContext.Current.Server.MapPath("~/temp/") + "downloads" + folder + "/" + filename;
            string extractPath = HttpContext.Current.Server.MapPath("~/temp/") + folder + "/";

            ZipFile.ExtractToDirectory(zipPath, extractPath);
            
        }

        public static void extract_7z(string filename, string folder, string stamp)
        {
            string zipPath = HttpContext.Current.Server.MapPath("~/temp/") + "downloads" + stamp + "/" + filename;
            string extractPath = HttpContext.Current.Server.MapPath("~/temp/") + folder;

            SevenZipBase.SetLibraryPath(HttpContext.Current.Server.MapPath("~/Scripts/7za.dll"));

            using (var file = new SevenZipExtractor(zipPath))
            {
                file.ExtractArchive(extractPath);
            }

        }

        //Downloads file from specififed route and saves it on temporary folder
         public static void download_from_url(String url, String folder, String filename)
        {
            using (WebClient webClient = new WebClient())
            {
                var path = HttpContext.Current.Server.MapPath("~/temp/") + "downloads" + folder + "/" + filename;
                webClient.DownloadFile(url, path);
                
            }
        }

        //Get latest release url from github repo
        public static async Task<String> repo_url(string author, string repo, Object file)
        {
            //Add your own client github credentials
            client.Credentials = tokenAuth;

            var releases = await client.Repository.Release.GetAll(author, repo);
            var url = "";
            if (file is bool || releases[0].Assets.Count() == 1)
            {
                url = releases[0].Assets[0].BrowserDownloadUrl;
            }
            else if(file is String)
            {
                for (int i = 0; i < releases[0].Assets.Count(); i++)
                {
                    if (releases[0].Assets[i].BrowserDownloadUrl.Contains(file.ToString()))
                    {
                        url = releases[0].Assets[i].BrowserDownloadUrl;
                        i = releases[0].Assets.Count();
                    }
                }
            }
            
            return url;
        }

        //Sets soundhax as hb entrypoint
        public static void soundhax_step(dynamic req_data, string stamp)
        {
            var server = HttpContext.Current.Server.MapPath("~/temp/");

            strap.payload_url(req_data, stamp);
            strap.download_soundhax(req_data, stamp);
            if (!File.Exists(server + "downloads" + stamp + "/starter.zip"))
            {
                download_from_url("http://smealum.github.io/ninjhax2/starter.zip", stamp, "starter.zip");
            }
            strap.extract_zip("starter.zip", stamp);
            Directory.Move(server + stamp + "/starter/3ds", server + stamp + "/3ds");
            Directory.Move(server + stamp + "/starter/boot.3dsx", server + stamp + "/boot.3dsx");
            Directory.Move(server + "downloads" + stamp + "/otherapp.bin", server + stamp + "/otherapp.bin");
            Directory.Delete(server + stamp + "/starter");
        }

        //Sets everything ready to run decrypt9 from hbl
        public static async Task d9_hb(string stamp)
        {
            var server = HttpContext.Current.Server.MapPath("~/temp/");

            Directory.CreateDirectory(server + stamp + "/files9");
            download_from_url(await strap.repo_url("TiniVi", "safehax", false), stamp, "safehax.3dsx");
            download_from_url(await strap.repo_url("nedwill", "fasthax", false), stamp, "fasthax.3dsx");
            download_from_url(await strap.repo_url("d0k3", "Decrypt9WIP",".zip"), stamp, "d9.zip");
            extract_file("d9.zip", "Decrypt9WIP.bin", "safehaxpayload.bin", "", stamp, "zip");
            Directory.Move(server + "downloads" + stamp + "/safehax.3dsx", server + stamp + "/3ds/safehax.3dsx");
            Directory.Move(server + "downloads" + stamp + "/fasthax.3dsx", server + stamp + "/3ds/fasthax.3dsx");
        }

        //Sets all the files needed to install a9lh with Luma
        public static async Task install(String stamp, dynamic req_data)
        {
            var server = HttpContext.Current.Server.MapPath("~/temp/");

            Directory.CreateDirectory(server + stamp + "/cias");
            Directory.CreateDirectory(server + stamp + "/a9lh");
            Directory.CreateDirectory(server + stamp + "/luma/payloads");

            if (!Directory.Exists(server + stamp + "/3ds"))
            {
                soundhax_step(req_data, stamp);
            }

            download_from_url(await repo_url("Plailect", "SafeA9LHInstaller", false), stamp, "a9lhinstaller.7z");
            download_from_url(await repo_url("AuroraWright", "arm9loaderhax", false), stamp, "a9lhrelease.7z");
            download_from_url(await repo_url("yellows8", "hblauncher_loader", false), stamp, "hblauncher_loader.zip");
            download_from_url(await repo_url("Hamcha", "lumaupdate", ".cia"), stamp, "lumaupdater.cia");
            download_from_url(await repo_url("Steveice10", "FBI",".cia"), stamp, "FBI.cia");
            download_from_url(await repo_url("AuroraWright", "Luma3DS", false), stamp, "luma.7z");
            download_from_url(await repo_url("d0k3", "Hourglass9", ".zip"), stamp, "Hourglass9.zip");
            download_from_url(await repo_url("Cruel", "DspDump", false), stamp, "DspDump.3dsx");


            extract_7z("a9lhinstaller.7z", stamp,stamp);
            extract_7z("a9lhrelease.7z", stamp + "\\a9lh", stamp);
            extract_file("hblauncher_loader.zip", "hblauncher_loader.cia", "hblauncher_loader.cia", "cias", stamp, "zip");
            Directory.Move(server + "downloads" + stamp + "/lumaupdater.cia",server + stamp + "/cias/lumaupdater.cia");
            Directory.Move(server + "downloads" + stamp + "/FBI.cia", server + stamp + "/cias/FBI.cia");

            //Luma arm9loaderhax.bin
            extract_7z("luma.7z", "downloads" + stamp + "\\luma", stamp);
            File.Delete(server + stamp + "/arm9loaderhax.bin");
            Directory.Move(server + "downloads" + stamp + "/luma/arm9loaderhax.bin", server + stamp + "/arm9loaderhax.bin");

            extract_file("Hourglass9.zip", "Hourglass9.bin", "start_Hourglass9.bin", "luma/payloads",stamp,"zip");
            Directory.Move(server + "downloads" + stamp + "/DspDump.3dsx", server + stamp + "/3ds/DspDump.3dsx");
        }

        //Extracts desired file to a path
        public static void extract_file(string zip, string filename_input, string filename_output, string folder, string stamp, string type)
        {
            var server = HttpContext.Current.Server.MapPath("~/temp/");
                using (ZipArchive archive = ZipFile.OpenRead(server + "downloads" + stamp + "/" + zip))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        if (entry.FullName == filename_input)
                        {
                            entry.ExtractToFile(Path.Combine(server + stamp + "/" + folder, filename_output), true);
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
            string filePath = path + filename;


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
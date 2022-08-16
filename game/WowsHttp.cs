using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using YuyukoRecord.game.data;
using YuyukoRecord.utils;

namespace YuyukoRecord.game
{
    internal class WowsHttp
    {
        private static readonly string HOME = "https://client.api.wows.shinoaki.com";
        private static readonly string ASIS = "https://api.worldofwarships.asia";
        private static readonly string CN = "https://api.wowsgame.cn";
        private static readonly string EU = "https://api.worldofwarships.eu";
        private static readonly string NA = "https://api.worldofwarships.com";
        private static readonly string RU = "https://api.worldofwarships.ru";

        private static readonly string VORTEX = "/api/accounts/search/autocomplete/";
        public static GameUser SearchUser(string server,string userName)
        {
            string url = Url(server).Replace("api", "vortex")+ VORTEX + userName;
            string jsonData =  SourcesLoad.Get(url);
            GameUser user = new GameUser();
            user.UserName = userName;
            user.Hide = true;
            if (string.IsNullOrEmpty(jsonData))
            {
                return user;
            }
        }

        public static string WowsJsonStatus(string data)
        {
            JToken token = JToken.Parse(data);
            //查找
            int status = token["code"].Value<int>();
            if (status == 200)
            {
                return token["data"].Value<string>();
            }
            return null;
        }

        private static string Url(string server)
        {
            switch (server)
            {
                case "asia":return ASIS;
                case "cn": return CN;
                case "eu": return EU;
                case "ru": return RU;
                    default:return NA;
            }
        }

        public static string PostFrom(string url, Dictionary<string, string> map)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json;charset=UTF-8";
            request.Timeout = 20000;

            NameValueCollection outgoingQueryString = HttpUtility.ParseQueryString(String.Empty);
            foreach (var item in map)
            {
                outgoingQueryString.Add(item.Key, item.Value);
            }
            string fromData = outgoingQueryString.ToString();
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                //string json = JsonConvert.SerializeObject(map);
                //streamWriter.Write(json);
                streamWriter.Write(fromData, 0, fromData.Length);
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string encoding = response.Headers["content-encoding"];
            string retString;
            if (encoding != null && "gzip".Equals(encoding))
            {
                retString = GzipUtils.Decompress(response);
            }
            else
            {
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
            }
            return retString;
        }
    }
}

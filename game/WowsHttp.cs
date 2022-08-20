using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using YuyukoRecord.config;
using YuyukoRecord.game.data;
using YuyukoRecord.game.temp;
using YuyukoRecord.utils;

namespace YuyukoRecord.game
{
    internal class WowsHttp
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static readonly string HOME = ApiConfig.Instance.HttpServer;
        private static readonly string ASIS = "https://api.worldofwarships.asia";
        private static readonly string CN = "https://api.wowsgame.cn";
        private static readonly string EU = "https://api.worldofwarships.eu";
        private static readonly string NA = "https://api.worldofwarships.com";
        private static readonly string RU = "https://api.worldofwarships.ru";

        private static readonly string VORTEX = "/api/accounts/search/autocomplete/";
        private static readonly string VORTEX_SHIP = "/api/accounts/";
        private static readonly string API_KEY = ApiConfig.Instance.ApiKey;


        public static GameUser ShinoAki(string server, Vehicles vehicles)
        {
            //搜索用户
            GameUser gameUser = SearchUser(server, vehicles.Name);
            if (!gameUser.Hide)
            {
                try
                {
                    //请求公会
                    string clanData =  ClanUserVort(server,gameUser.AccountId);
                    Dictionary<string, string> mapClan = new Dictionary<string, string>();
                    mapClan.Add("server", server);
                    mapClan.Add("data", Convert.ToBase64String(GzipUtils.Compress(Encoding.UTF8.GetBytes(clanData))));
                    //
                    string resultClan = WowsJsonStatus(PostJson(HOME + "/public/wows/parse/clan/user", mapClan));
                    ClanUser clanUser = null;
                    if (!string.IsNullOrEmpty(resultClan))
                    {
                        clanUser = JsonConvert.DeserializeObject<ClanUser>(resultClan);
                    }
                    //请求战舰数据
                    string data = server.Equals("cn") ? ShipAllVort(server, gameUser.AccountId) : ShipAllDev(server, gameUser.AccountId);
                    if (string.IsNullOrEmpty(data))
                    {
                        return gameUser;
                    }
                    Dictionary<string, string> map = new Dictionary<string, string>();
                    map.Add("accountId", gameUser.AccountId.ToString());
                    map.Add("server",server);
                    map.Add("userName",gameUser.UserName);
                    map.Add("shipId",vehicles.ShipId.ToString());
                    map.Add("data", Convert.ToBase64String(GzipUtils.Compress(Encoding.UTF8.GetBytes(data))));
                    string result = WowsJsonStatus(PostJson(HOME + "/public/wows/parse/ship/all", map));
                    if (!string.IsNullOrEmpty(result))
                    {
                        gameUser = JsonConvert.DeserializeObject<GameUser>(result);
                    }
                    if (clanUser != null)
                    {
                        gameUser.ClanTag = clanUser.Tag;
                        gameUser.ClanColor = clanUser.ColorRgb;
                    }
                }
                catch (Exception ex)
                {
                    log.Error(vehicles.Name + " 请求解析服务器异常! server=" + server + "  " + ex);
                }
            }
            //这里要判断一次是否自己的团队
            gameUser.MyTeam = vehicles.Relation <= 1;
            return gameUser;
        }

        private static string WowsJsonStatus(string data)
        {
            JToken token = JToken.Parse(data);
            //查找
            int status = token["code"].Value<int>();
            if (status == 200)
            {
                return token["data"].ToString();
            }
            return null;
        }

        public static string Get(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "application/json;charset=UTF-8";
                request.Timeout = 20000;
                request.Host = HttpHost(url);
                request.Headers.Add("Accept-Encoding", "gzip,deflate");
                request.Headers.Add("Accept-Language", "zh-CN");
                request.Accept = "*/*";
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

                }
                response.Close();
                return retString;
            }
            catch (Exception e)
            {
                log.Error(url + " 请求失败!" + e.Message);
            }
            return null;
        }

        public static string PostFrom(string url, Dictionary<string, string> map)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json;charset=UTF-8";
            request.Timeout = 20000;
            request.Host = HttpHost(url);
            request.Headers.Add("Accept-Encoding", "gzip,deflate");
            request.Headers.Add("Accept-Language", "zh-CN");
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

        public static string PostJson(string url, Dictionary<string, string> map)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json;charset=UTF-8";
            request.Timeout = 20000;
            request.Host = HttpHost(url);
            request.Headers.Add("Accept-Encoding", "gzip,deflate");
            request.Headers.Add("Accept-Language", "zh-CN");
            /*NameValueCollection outgoingQueryString = HttpUtility.ParseQueryString(String.Empty);
            foreach (var item in map)
            {
                outgoingQueryString.Add(item.Key, item.Value);
            }
            string fromData = outgoingQueryString.ToString();*/
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(map);
                streamWriter.Write(json);
                //streamWriter.Write(fromData, 0, fromData.Length);
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

        private static string ShipAllDev(string server, long accountId)
        {
            Dictionary<string, string> map = new Dictionary<string, string>();
            map.Add("account_id", accountId.ToString());
            map.Add("application_id", API_KEY);
            //map.Add("extra", "pvp_solo,pvp_div2,pvp_div3,rank_solo");
            try
            {
                return PostFrom(Url(server)+ "/wows/ships/stats/", map);
            }
            catch (Exception ex)
            {
                log.Error(accountId + " 请求ship失败 server=" + server + " error=" + ex);
            }
            return null;
        }

        private static string ShipAllVort(string server, long accountId)
        {
            string url = Url(server).Replace("api", "vortex") + VORTEX_SHIP + accountId + "/ships/";
            try
            {
                return Get(url);
            }catch (Exception ex)
            {
                log.Error(accountId + " 请求vortex ship失败 server=" + server + " error=" + ex);
            }
            return null;
        }

        private static string ClanUserVort(string server, long accountId)
        {
            string url = Url(server).Replace("api", "vortex") + VORTEX_SHIP + accountId + "/clans/";
            try
            {
                return Get(url);
            }
            catch (Exception ex)
            {
                log.Error(accountId + " 请求vortex ship失败 server=" + server + " error=" + ex);
            }
            return null;
        }

        private static GameUser SearchUser(string server, string userName)
        {
            GameUser user = new GameUser();
            user.UserName = userName;
            user.Hide = true;
            string url = Url(server).Replace("api", "vortex") + VORTEX + userName+"/";
            try
            {
                string jsonData = Get(url);
                if (!string.IsNullOrEmpty(jsonData))
                {
                    foreach (var item in WowsJsonVortex(jsonData))
                    {
                        if (item["name"].Value<string>().Equals(userName))
                        {
                            user.AccountId = item["spa_id"].Value<long>();
                            user.Hide = item["hidden"].Value<bool>();
                            return user;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(userName + " 请求异常! server=" + server + "  " + ex);
            }
            return user;
        }


        private static JToken WowsJsonVortex(string data)
        {
            JToken token = JToken.Parse(data);
            //查找
            string status = token["status"].Value<string>();
            if (status.Equals("ok"))
            {
                return token["data"];
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

        private static string HttpHost(string url)
        {
            string replace = url.Replace("http://", "").Replace("https://", "");
            if (replace.Contains("/"))
            {
                return replace.Substring(0, replace.IndexOf("/"));
            }
            return replace;
        }
    }
}

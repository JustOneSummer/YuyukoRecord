using Newtonsoft.Json;

namespace YuyukoRecord.config
{
    internal class ApiConfig
    {
        private string httpServer;
        private string mqttServer;
        private string mqUserName;
        private string mqPassword;
        private string apiKey;

        private static ApiConfig instance;

        public static void LoadInit()
        {
           string result =  SourcesLoad.Get("https://yuyuko-1253221348.cos.ap-shanghai.myqcloud.com/app/api/API.json");
            Instance = JsonConvert.DeserializeObject<ApiConfig>(result);
        }

        public string HttpServer { get => httpServer; set => httpServer = value; }
        public string MqttServer { get => mqttServer; set => mqttServer = value; }
        public string ApiKey { get => apiKey; set => apiKey = value; }
        internal static ApiConfig Instance { get => instance; set => instance = value; }
        public string MqUserName { get => mqUserName; set => mqUserName = value; }
        public string MqPassword { get => mqPassword; set => mqPassword = value; }
    }
}

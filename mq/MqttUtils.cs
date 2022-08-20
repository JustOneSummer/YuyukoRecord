using uPLibrary.Networking.M2Mqtt;
using YuyukoRecord.config;

namespace YuyukoRecord.mq
{
    internal class MqttUtils
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static readonly string MQ_URL = ApiConfig.Instance.MqttServer;
        public static readonly string CLIENT_ID = "yuyuko_" + SourcesLoad.GetCpuID();
        public static readonly string USER_NAME = ApiConfig.Instance.MqUserName;
        public static readonly string PASSWORD = ApiConfig.Instance.MqPassword;

        public static readonly string TOPIC_PUSH_SERVER = "wows/client/push/server/" + CLIENT_ID;
        public static readonly string TOPIC_PUSH_REAL = "wows/client/push/real/" + CLIENT_ID;
        public static readonly string TOPIC_POLL_REAL = "wows/client/poll/real/" + CLIENT_ID;

        /// <summary>
        /// 连接
        /// </summary>
        public static MqttClient Init()
        {
            string[] url = MQ_URL.Split(':');
            MqttClient client = new MqttClient(url[0], int.Parse(url[1]), false, null, null, MqttSslProtocols.TLSv1_2);
            client.ProtocolVersion = MqttProtocolVersion.Version_3_1_1;
            byte status = Connect(client);
            log.Info("初始化连接状态=" + status);
            return client;
        }

        public static byte Connect(MqttClient client)
        {
            return client.Connect(CLIENT_ID, USER_NAME, PASSWORD);
        }
    }
}

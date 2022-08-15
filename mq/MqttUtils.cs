using uPLibrary.Networking.M2Mqtt;

namespace YuyukoRecord.mq
{
    internal class MqttUtils
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public const string MQ_URL = "mq.wows.shinoaki.com:1883";
        public static string CLIENT_ID = "yuyuko_" + SourcesLoad.GetCpuID();
        public static string USER_NAME = "wows-poll";
        public static string PASSWORD = "wows-poll";

        public static string TOPIC_PUSH_SERVER = "wows/client/push/server/" + CLIENT_ID;
        public static string TOPIC_PUSH_REAL = "wows/client/push/real/" + CLIENT_ID;
        public static string TOPIC_POLL_REAL = "wows/client/poll/real/" + CLIENT_ID;

        /// <summary>
        /// 连接
        /// </summary>
        public static MqttClient Init()
        {
            string[] url = MQ_URL.Split(':');
            MqttClient client = new MqttClient(url[0], int.Parse(url[1]), false, null, null, MqttSslProtocols.None);
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

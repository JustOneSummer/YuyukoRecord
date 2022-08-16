using Newtonsoft.Json;
using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Threading;
using uPLibrary.Networking.M2Mqtt;
using YuyukoRecord.config;
using YuyukoRecord.game;
using YuyukoRecord.game.data;
using YuyukoRecord.local;
using YuyukoRecord.mq;
using YuyukoRecord.table;
using YuyukoRecord.utils;

namespace YuyukoRecord
{
    public partial class Yuyuko : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private const string VERSION = "1.2.5";
        private static GameData GAME_DATA = null;
        private Dispatcher Dispatcher = Dispatcher.CurrentDispatcher;
        private MqttClient mqttClient = null;
        private string MENU_DATA = null;
        private string MENU_SHIP_NAME = null;
        private string GAME_SERVER = null;
        public Yuyuko()
        {
            //1、设置窗体的双缓冲
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();

            InitializeComponent();

            //2、利用反射设置DataGridView的双缓冲
            Type dgvType = dataGridViewOne.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dataGridViewOne, true, null);
        }

        private void Yuyuko_Load(object sender, EventArgs e)
        {
            labelMyOne.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            log.Info("正在运行的 .net framework 信息：" + RuntimeInformation.FrameworkDescription);
            log.Info("windows version = " + Program.OS_VERSION);
            log.Info("初始化 版本=" + VERSION);
            TableUtils.Load(dataGridViewOne);
            log.Info("加载pr信息...");
            PrCache.Http();
            log.Info("加载ship信息...");
            ShipCache.Http();
            log.Info("加载配置信息...");
            AppConfigUtils.LoadInit();
            ClientMqInit();
            string v = SourcesLoad.LoadGamePath();
            if (string.IsNullOrEmpty(v))
            {
                //弹窗提示选择游戏安装路径
                SetGamePathToolStripMenuItem_Click(sender, null);
            }
            CheckUpdate();
            buttonLoadServer_Click(sender, null);
            if (!string.IsNullOrEmpty(SourcesLoad.LoadGamePath()))
            {
                //开始监听文件
                FileEvent();
            }
        }

        private void FileEvent()
        {
            string path = SourcesLoad.LoadGamePath() + "replays";
            if (!Directory.Exists(path))
            {
                log.Error("replays 文件夹不存在！");
                return;
            }
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = path;
            /*监视LastAcceSS和LastWrite时间的更改以及文件或目录的重命名*/
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite |
                   NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.Filter = "tempArenaInfo.json";

            watcher.Created += (s, e) => Dispatcher.Invoke(new Action(() =>
              {
                  //指定当文件被更改、创建或删除时要做的事
                  Console.WriteLine("file:" + e.FullPath + "" + e.ChangeType);
                  if (WatcherChangeTypes.Created == e.ChangeType)
                  {
                      labelDataStatus.Text = "数据加载中...";
                      Thread.Sleep(500);
                      //加载对局信息
                      LoadGameInfo();
                  }

              }));
            watcher.Deleted += (s, e) => Dispatcher.Invoke(new Action(() =>
              {
                  if (WatcherChangeTypes.Deleted == e.ChangeType)
                  {
                      labelDataStatus.Text = "等待开始游戏...";
                      contextMenuStrip1.Enabled = false;
                  }
              }));
            //开始监视
            watcher.EnableRaisingEvents = true;
            LoadGameInfo();
        }

        public void LoadGameInfo()
        {
            GAME_DATA = null;
            this.dataGridViewOne.Rows.Clear();
            //读取内容,渲染
            GameTempArenaInfo gameTempArenaInfo = GameTempArenaInfo.LoadJson();
            if (gameTempArenaInfo == null)
            {
                return;
            }
            labelDataStatus.Text = "开始渲染数据...";
            if(!string.IsNullOrEmpty(gameTempArenaInfo.DateTime)){
                DateTime dateTime = DateTime.ParseExact(gameTempArenaInfo.DateTime, "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                labelGameTime.Text = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                labelGameTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            GAME_DATA = GameData.ToData(gameTempArenaInfo);
            //这里判断是推送到mqtt还是本地化计算
            if (AppConfigUtils.Instance.MqttServer)
            {
                Publish(MqttUtils.TOPIC_PUSH_REAL, 1, GAME_DATA.GameTempArenaInfo.TempArenaInfoJson);
            }
            else
            {
                //本地化计算
                ThreadPool.QueueUserWorkItem(state=> LocalService.LoadGameInfo(GAME_SERVER, GAME_DATA));
            }
            //排序
            GAME_DATA.One.Sort();
            GAME_DATA.Two.Sort();
            TableUtils.LoadGame(dataGridViewOne, GAME_DATA);
        }


        /// <summary>
        /// 检测更新
        /// </summary>
        public void CheckUpdate()
        {
            try
            {
                int localV = int.Parse(VERSION.Replace(".", ""));
                string newV = SourcesLoad.GetVersion();
                log.Info("检查版本 新版本=" + newV);
                int ver = int.Parse(newV.Replace(".", ""));
                if (ver > localV)
                {
                    if (ver - localV >= 5 && localV > 2)
                    {
                        MessageBox.Show("版本过低！！！\r\n请去网站更新最新版本\r\nhttp://v.wows.shinoaki.com");
                        //System.Diagnostics.Process.GetProcessById(System.Diagnostics.Process.GetCurrentProcess().Id).Kill();
                    }
                    else
                    {
                        MessageBox.Show("发现新版本，请去网站更新最新版本\r\nhttp://v.wows.shinoaki.com");
                    }
                }
            }
            catch (Exception e)
            {
                log.Error("请求版本信息出错！", e);
                MessageBox.Show("请求版本信息出错！如果开了代理请关闭全局代理...");
            }
        }


        /// <summary>
        /// 设置游戏路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetGamePathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigGamePath home = new ConfigGamePath();
            home.ShowDialog();
        }

        /// <summary>
        /// 打开文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReplayPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string paht = @"" + SourcesLoad.LoadGamePath() + "replays";
            System.Diagnostics.Process.Start("explorer.exe", paht);
        }

        private void ExeLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string paht = @"" + System.Environment.CurrentDirectory + "\\logs";
            System.Diagnostics.Process.Start("explorer.exe", paht);
        }

        private void InfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Author author = new Author();
            author.ShowDialog();
        }

        private void DefaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Width = 1500;
            Height = 1010;
            TableUtils.Hw(dataGridViewOne);
        }

        private void buttonLoadServer_Click(object sender, EventArgs e)
        {
            string v = SourcesLoad.ServerInfo();
            GAME_SERVER = v;
            if ("asia".Equals(v))
            {
                labelServer.Text = "亚服";
                Publish(MqttUtils.TOPIC_PUSH_SERVER, 1, GAME_SERVER);
            }
            else if ("cn".Equals(v))
            {
                labelServer.Text = "国服";
                Publish(MqttUtils.TOPIC_PUSH_SERVER, 1, GAME_SERVER);
            }
            else if ("eu".Equals(v))
            {
                labelServer.Text = "欧服";
                Publish(MqttUtils.TOPIC_PUSH_SERVER, 1, GAME_SERVER);
            }
            else if ("na".Equals(v))
            {
                labelServer.Text = "美服";
                Publish(MqttUtils.TOPIC_PUSH_SERVER, 1, GAME_SERVER);
            }
            else if ("ru".Equals(v))
            {
                labelServer.Text = "俄服";
                Publish(MqttUtils.TOPIC_PUSH_SERVER, 1, GAME_SERVER);
            }
            else
            {
                return;
            }

            if (e != null)
            {
                MessageBox.Show("刷新成功!");
            }
        }

        public void Subscribe()
        {
            string[] topic = new string[] { MqttUtils.TOPIC_POLL_REAL };
            byte[] qos = new byte[] { 2 };
            log.Info("订阅数据=" + MqttUtils.TOPIC_POLL_REAL);
            mqttClient.Subscribe(topic, qos);
        }

        public void Publish(string topic, byte qos, string data)
        {
            if (!AppConfigUtils.Instance.MqttServer)
            {
                return ;
            }
            try
            {
                log.Info("topic=" + topic + " 推送数据:" + data);
                byte[] dataBytes = GzipUtils.Compress(Encoding.UTF8.GetBytes(data));
                mqttClient.Publish(topic, dataBytes, qos, false);
            }
            catch (Exception e)
            {
                log.Error(topic + " 推送数据异常!data+" + data + " ,error=" + e.Message);
            }
        }

        public void ClientMqInit()
        {
            if (!AppConfigUtils.Instance.MqttServer)
            {
                log.Info("非mqtt模式启动...");
                return ;
            }
            else
            {
                log.Info("mqtt模式启动...");
            }
            mqttClient = MqttUtils.Init();
            /*           mqttClient.ConnectionClosed += (s, e) => this.Dispatcher.Invoke(new Action(() =>
                        {

                        }));*/
            log.Info("初始化mq客户端 id=" + MqttUtils.CLIENT_ID);
            mqttClient.MqttMsgPublishReceived += (s, e) => Dispatcher.Invoke(new Action(() =>
              {
                  try
                  {
                      // handle message received 
                      string data = Encoding.UTF8.GetString(GzipUtils.Decompress(e.Message));
                      log.Info("收到服务器推送数据:" + data);
                      if (data.Contains("dateTime"))
                      {
                          labelDataStatus.Text = "收到数据...";
                      }
                      else
                      {
                          GameUser gameUser = JsonConvert.DeserializeObject<GameUser>(data);
                          LocalService.Put(gameUser);
                      }
                  }
                  catch (Exception ex)
                  {
                      log.Error("处理订阅数据异常!" + e.Topic + " err=" + ex);
                  }
              }));
            Subscribe();
        }

        public static string SpOne(int pr, int battle, double wins)
        {
            return battle + "场  " + wins.ToString("0.00") + "%  " + pr + "pr";
        }

        public static string SpTwo(int pr, int battle, double wins)
        {
            return pr + "pr  " + wins.ToString("0.00") + "%  " + battle + "场";
        }

        private void dataGridViewOne_SelectionChanged(object sender, EventArgs e)
        {
            dataGridViewOne.ClearSelection();
        }

        private void ReLoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReLoadToolStripMenuItem.Enabled = false;
            buttonLoadServer_Click(sender, null);
            LoadGameInfo();
        }

        private void Yuyuko_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (mqttClient != null)
            {
                try
                {
                    mqttClient.Disconnect();
                }
                catch (Exception ex)
                {
                    log.Error("mqtt退出异常!" + ex);
                }
            }
        }

        private void UpdateSorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateFile update = new UpdateFile();
            update.ShowDialog();
        }

        private void dataGridViewOne_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                {
                    string v = null;
                    if (e.ColumnIndex <= 7)
                    {
                        //获取友方数据
                        v = dataGridViewOne.Rows[e.RowIndex].Cells[0].Value.ToString();
                        MENU_SHIP_NAME = dataGridViewOne.Rows[e.RowIndex].Cells[3].Value.ToString();
                    }
                    else if (e.ColumnIndex >= 8)
                    {
                        //获取敌方
                        v = dataGridViewOne.Rows[e.RowIndex].Cells[13].Value.ToString();
                        MENU_SHIP_NAME = dataGridViewOne.Rows[e.RowIndex].Cells[10].Value.ToString();
                    }
                    else
                    {
                        MENU_SHIP_NAME = null;
                    }
                    log.Info("右键内容=" + v);
                    //剔除公会信息 开始查找
                    int index = v.IndexOf("]");
                    MENU_DATA = v;
                    if (index >= 0)
                    {
                        MENU_DATA = v.Substring(index + 1);
                    }
                }
            }
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GameUser user in GAME_DATA.GameUserList)
                {
                    if (user.UserName.Equals(MENU_DATA))
                    {
                        //复制战绩
                        StringBuilder builder = new StringBuilder();
                        if (string.IsNullOrEmpty(user.ClanTag))
                        {
                            builder.Append("[").Append(user.ClanTag).Append("]");
                        }
                        builder.Append(user.UserName).Append(TableTemplate.RN);
                        if (user.Hide)
                        {
                            builder.Append("该用户隐藏了战绩");
                        }
                        else
                        {
                            builder.Append(user.Pvp.Battles).Append("场").Append(TableTemplate.RN);
                            builder.Append("胜率:").Append(user.Pvp.Wins).Append("%").Append(TableTemplate.RN);
                            builder.Append("PR:").Append(user.Pvp.Pr).Append(TableTemplate.RN);
                            builder.Append(MENU_SHIP_NAME).Append(TableTemplate.RN);
                            builder.Append(user.Ship.Battles).Append("场").Append(TableTemplate.RN);
                            builder.Append("胜率:").Append(user.Ship.Wins).Append("%").Append(TableTemplate.RN);
                            builder.Append("场均:").Append(user.Ship.Damage).Append(TableTemplate.RN);
                            builder.Append("PR(评分)=").Append(user.Ship.Pr).Append(TableTemplate.RN);
                        }
                        string data = builder.ToString();
                        log.Info("复制内容:" + data);
                        Clipboard.SetText(data);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("复制到粘贴板异常! " + ex);
            }
        }

        private void OpenViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GameUser user in GAME_DATA.GameUserList)
                {
                    if (user.UserName.Equals(MENU_DATA))
                    {
                        //去浏览器看战绩
                        System.Diagnostics.Process.Start("https://wows.mgaia.top/#/player?server=" + GAME_SERVER + "&accountId=" + user.AccountId);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("打开浏览器异常! " + ex);
            }
        }

        private void ImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigImage image = new ConfigImage();
            image.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (mqttClient != null && !mqttClient.IsConnected)
            {
                byte status = MqttUtils.Connect(mqttClient);
                log.Info("mqtt 重连状态..." + status);
                Subscribe();
            }
            while (true)
            {
                GameUser gameUser = LocalService.Poll();
                if (gameUser != null)
                {
                    if (gameUser.Pvp == null)
                    {
                        gameUser.Pvp = new GamePlayerInfo();
                    }
                    if (gameUser.Ship == null)
                    {
                        gameUser.Ship = new GamePlayerInfo();
                    }
                    TableUtils.LoadGame(dataGridViewOne, gameUser);
                    bool d = true;
                    foreach (GameUser g in GAME_DATA.GameUserList)
                    {
                        if (g.UserName.Equals(gameUser.UserName))
                        {
                            d = false;
                        }
                    }
                    if (d)
                    {
                        GAME_DATA.GameUserList.Add(gameUser);
                    }
                    if (GAME_DATA.GameUserList.Count >= GAME_DATA.GameTempArenaInfo.Vehicles.Count)
                    {
                        GAME_DATA.Process();
                        GameAvg avgOne = GAME_DATA.AvgOne;
                        GameAvg avgTwo = GAME_DATA.AvgTwo;
                        labelMyOne.Text = SpOne(avgOne.AvgPr(), avgOne.Battle, avgOne.AvgWins());
                        labelMyTwo.Text = SpTwo(avgTwo.AvgPr(), avgTwo.Battle, avgTwo.AvgWins());
                        labelDataStatus.Text = "计算完成...";
                        contextMenuStrip1.Enabled = true;
                        ReLoadToolStripMenuItem.Enabled = true;
                    }
                }
                else
                {
                    return;
                }
            }
        }

        private void labelMyOne_DoubleClick(object sender, EventArgs e)
        {
            log.Info("复制内容我方:" + this.labelMyOne.Text);
            Clipboard.SetText("我方队伍综合评分:"+this.labelMyOne.Text);
        }

        private void labelMyTwo_DoubleClick(object sender, EventArgs e)
        {
            log.Info("复制内容敌方:" + this.labelMyOne.Text);
            Clipboard.SetText("敌方队伍综合评分:" + this.labelMyOne.Text);
        }
    }
}

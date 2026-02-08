using RedisClearApp.Common;
using RedisClearApp.EasyORM;
using RedisClearApp.Ext;
using RedisClearApp.Helper;
using RedisClearApp.Model;
using System.Diagnostics;
using WinformLib;

namespace RedisClearApp
{
    public partial class MainForm : Form
    {
        EasyCrud easy = DB.easydb;
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.SetCommon(new FormSettings
            {
                TitleText = "RedisClear"
            });
            this.MinimizeBox = false;
            this.TaskRun(() =>
            {
                CheckConn();
            });
        }

        /// <summary>
        /// 清空Redis
        /// </summary>
        public void StartFlush()
        {
            timer1.Stop();
            timer1.Interval = CommonDto.timerInterval;
            timer1.Start();
            // 立即执行一次 timer1_Tick 事件的逻辑
            timer1_Tick(this, EventArgs.Empty);
            Debug.WriteLine($"【{DateTime.Now}】清空redis");
        }

        public void ChangeCheck()
        {
            if (checkBox2.Checked)
            {
                checkBox2.Checked = false;
                checkBox2.Checked = true;
            }
        }

        private async void CheckConn()
        {
            var data = easy.GetList<RedisInfo>();
            int err = 0;
            int all = data.Count;
            int okCount = 0;
            foreach (var item in data)
            {
                var isOK = await RedisEasyHelper.TryConnectAsync(item.Conn);
                if (!isOK)
                {
                    err++;
                }
            }
            SetTips(err == 0 ? "ReidsClear:全部初始化成功" : "ReidsClear:部分初始化成功", err == 0);
            this.UISafeInvoke(() =>
            {
                //更新到标题
                okCount = all - err;
                if (all == 0)
                {
                    return;
                }
                this.Text = $"RedisClear({okCount}/{all})";
            });
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = checkBox1.Checked;
        }

        private void SetTips(string v1, bool v2 = true)
        {
            this.UISafeInvoke(() =>
            {
                label1.Text = v1;
                label1.ForeColor = v2 ? Color.Blue : Color.Red;

            });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ConfigForm form = new ConfigForm(ChangeCheck);
            this.ShowOnlyOne(form);
        }

        /// <summary>
        /// 定期清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                StartFlush();
            }
            else
            {
                SetTips("取消定清成功！");
                timer1.Stop();
            }
        }

        /// <summary>
        /// 定时器方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void timer1_Tick(object sender, EventArgs e)
        {
            var data = await easy.GetListAsync<RedisInfo>();
            if (!data.Any())
            {
                SetTips("请先配置Redis连接！",false);
                this.Text = $"RedisClear";
                return;
            }
            int err = 0;
            int all = data.Count;
            int okCount = 0;
            foreach (var item in data)
            {
                var isOK = await RedisEasyHelper.FLUSHALL(item.Conn);
                if (!isOK)
                {
                    err++;
                }
            }
            //计算下次清空时间:
            var next = DateTime.Now.AddMilliseconds(timer1.Interval);
            var show = $"(Next:{next.ToString("HH:mm:ss")})";
            SetTips(err == 0 ? $"全部定清成功{show}" : $"部分定清成功{show}", err == 0);

            //更新到标题
            okCount = all - err;
            this.Text = $"RedisClear({okCount}/{all})";
        }

        /// <summary>
        /// 立即清空
        /// </summary>
        private async void button1_Click(object sender, EventArgs e)
        {
            var data = await easy.GetListAsync<RedisInfo>();
            if (!data.Any())
            {
                SetTips("请先配置Redis连接！", false);
                this.Text = $"RedisClear";
                return;
            }
            int err = 0;
            int all = data.Count;
            int okCount = 0;
            foreach (var item in data)
            {
                var isOK = await RedisEasyHelper.FLUSHALL(item.Conn);
                if (!isOK)
                {
                    err++;
                }
            }
            SetTips(err == 0 ? "立即清空:全部清空成功！" : "立即清空:部分清空成功！", err == 0);
            this.UISafeInvoke(() =>
            {
                //更新到标题
                okCount = all - err;
                this.Text = $"RedisClear({okCount}/{all})";
            });
        }
    }
}

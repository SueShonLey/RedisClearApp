using RedisClearApp.Common;
using RedisClearApp.EasyORM;
using RedisClearApp.Ext;
using RedisClearApp.Helper;
using RedisClearApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinformLib;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace RedisClearApp
{
    public partial class ConfigForm : Form
    {
        EasyCrud easy = DB.easydb;
        Action _ChangeCheck;
        public ConfigForm(Action ChangeCheck)
        {
            InitializeComponent();
            _ChangeCheck = ChangeCheck;
        }



        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            var hour = Convert.ToInt32(numericUpDown2.Value);
            var min = Convert.ToInt32(numericUpDown3.Value);
            var second = Convert.ToInt32(numericUpDown4.Value);
            CommonDto.timerInterval = hour * 3600000 + min * 60000 + second * 1000;
            _ChangeCheck();
        }

        /// <summary>
        /// 链接字符串管理查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void textBox1_TextChanged(object sender, EventArgs e)
        {
            var queryText = textBox1.Text.Trim();
            var list = await easy.GetFreeSql()
                                .Select<RedisInfo>()
                                .Where(x => x.Conn.Contains(queryText) || x.Remark.Contains(queryText))
                                .ToListAsync();
            var updateList = new List<RedisInfo>();
            foreach (var item in list)
            {
                var testresult = await RedisEasyHelper.TryConnectAsync(item.Conn) ? 1 : 0;
                if (item.Issucess != testresult)
                {
                    updateList.Add(item);
                }
            }
            await easy.UpdateAsync(updateList);
            if (!list.Any())
            {
                dataGridView1.Rows.Clear();
                return;
            }
            Debug.WriteLine(list.Count);
            dataGridView1.SetCommonWithCell<RedisInfo>(new ControlExt.DataDisplayEntityCell<RedisInfo>
            {
                DataList = list,
                HeadtextList = new List<(System.Linq.Expressions.Expression<Func<RedisInfo, object>> fields, string name, int width)>
                {
                    (x=> x.Ip,"IP地址",130),
                    (x=> x.Port,"端口",70),
                    (x=> x.Remark,"备注",160),
                },
                ButtonList = new List<(string ButtonName, string titile, int Width)>
                {
                    ("清空","操作1",60),
                    ("删除","操作2",60),
                },
                RowAction = (info, cell) =>
                {
                    var style = cell.DefaultCellStyle;
                    style.ForeColor = Color.Red;
                    style.Font = new Font("宋体", 10f, FontStyle.Regular);
                    if (info.Issucess == 1)
                    {
                        style.ForeColor = Color.FromArgb(0, 170, 17);
                    }
                },
                ColumnAction = (col) =>
                {
                    if (col.Name.Equals("Remark"))
                    {
                        col.ReadOnly = false;
                    }
                }
            });
        }

        /// <summary>
        /// 新增
        /// </summary>
        private async void button2_Click(object sender, EventArgs e)
        {
            var result = this.SetCustomizeForms(new CustomizeFormsExtentions.CustomizeFormInput
            {
                FormTitle = "新增Redis连接",
                inputs = new List<CustomizeFormsExtentions.CustomizeValueInput>
                {
                    new CustomizeFormsExtentions.CustomizeValueInput
                    {
                        Label="IP地址",
                        DefaultValue = "127.0.0.1"
                    },
                    new CustomizeFormsExtentions.CustomizeValueInput
                    {
                        Label="端口号",
                        DefaultValue = "6379"
                    },
                    new CustomizeFormsExtentions.CustomizeValueInput
                    {
                        Label="密码"
                    },
                    new CustomizeFormsExtentions.CustomizeValueInput
                    {
                        Label="备注"
                    }
                }
            });
            if (result.Any())
            {
                var ip = result["IP地址"];
                var port = result["端口号"];
                var pwd = result["密码"];
                var remark = result["备注"];
                if (string.IsNullOrEmpty(ip) || string.IsNullOrEmpty(port))
                {
                    this.PopUpTips("必须填写IP地址和端口号");
                    button2_Click(sender, e);
                    return;
                }
                if (!int.TryParse(result["端口号"], out var portInt))
                {
                    this.PopUpTips("端口号必须为数字");
                    button2_Click(sender, e);
                    return;
                }
                // 重复性校验
                if (await easy.AnyAsync<RedisInfo>(x => x.Ip.Equals(ip) && x.Port == portInt))
                {
                    this.PopUpTips("IP地址和端口号重复，请检查！");
                    button2_Click(sender, e);
                    return;
                }
                //入库
                var conn = RedisEasyHelper.GetRedisString(ip, portInt, pwd);
                var info = new RedisInfo
                {
                    Ip = ip,
                    Port = portInt,
                    Password = pwd,
                    Remark = remark,
                    Conn = conn,
                    Issucess = await RedisEasyHelper.TryConnectAsync(conn) ? 1 : 0
                };
                await easy.InsertAsync(info);
                _ChangeCheck();
                textBox1_TextChanged(sender, e);
            }

        }

        /// <summary>
        /// 点击清空或者删除
        /// </summary>
        private async void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var isdelete = dataGridView1.GetCommonByButton<RedisInfo>("删除", e);
            var isclear = dataGridView1.GetCommonByButton<RedisInfo>("清空", e);
            bool hasOperea = false;//是否触发操作
            if (isdelete != null)
            {
                await easy.DeleteByExpAsync<RedisInfo>(x => x.Id.Equals(isdelete.Id));
                hasOperea = true;
            }
            if (isclear != null)
            {
                await RedisEasyHelper.FLUSHALL(isclear.Conn);
                hasOperea = true;
            }
            if (hasOperea)
            {
                textBox1_TextChanged(sender, e);
            }
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            textBox1_TextChanged(sender, e);
        }

        /// <summary>
        /// 保存备注
        /// </summary>
        private void button4_Click(object sender, EventArgs e)
        {
            var editData = dataGridView1.GetCommon<RedisInfo>(new List<(System.Linq.Expressions.Expression<Func<RedisInfo, object>> fields, string name)>
            {
                (x=>x.Id,"Id"),
                (x=>x.Remark,"备注"),
            });
            foreach (var item in editData)
            {
                easy.UpdateSetWhere<RedisInfo>(x => x.Remark, item.Remark, x => x.Id == item.Id);
            }
        }
    }
}

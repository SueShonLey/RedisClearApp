using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WinformLib.DataGridViewExtentions;

namespace RedisClearApp.Ext
{
    public static class ControlExt
    {
        /// <summary>
        /// 渲染DataGridView（可控制Cell/UI，加强版）
        /// </summary>
        public static void SetCommonWithCell<T>(this DataGridView dataGridView, DataDisplayEntityCell<T> input) where T : class, new()
        {
            // 先渲染数据
            dataGridView.SetCommon(input.DataList, input.HeadtextList, input.ButtonList.Select(x => x.ButtonName).ToList());
            dataGridView.ReadOnly = false;
            // 单独处理UI
            foreach (DataGridViewRow item in dataGridView.Rows)
            {
                // 行操作
                if (input.RowAction != null)
                {
                    input.RowAction(item.Tag as T, item);
                }

                //单元格操作
                foreach (DataGridViewCell cell in item.Cells)
                {
                    if (input.CellAction != null)
                    {
                        input.CellAction(item.Tag as T, cell.OwningColumn, cell);
                    }

                }
            }
            //列操作
            foreach (DataGridViewColumn item in dataGridView.Columns)
            {
                if (input.ColumnAction != null)
                {
                    input.ColumnAction(item);
                }
            }
            // 处理按钮宽度
            foreach (var item in input.ButtonList)
            {
                dataGridView.Columns[item.ButtonName].HeaderText = item.TitileName;
                dataGridView.Columns[item.ButtonName].Width = item.Width;
            }
        }

        /// <summary>
        /// UI设置入参Dto
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class DataDisplayEntityCell<T>
        {
            /// <summary>
            /// 数据列表
            /// </summary>
            public List<T> DataList { get; set; } = new List<T>();

            /// <summary>
            /// 字段、标题名称及宽度
            /// </summary>
            public List<(Expression<Func<T, object>> Feild, string TitileName, int Width)> HeadtextList { get; set; } = new List<(Expression<Func<T, object>> Feild, string TitileName, int width)>();

            /// <summary>
            /// 按钮名称、标题名称及宽度
            /// </summary>
            public List<(string ButtonName, string TitileName, int Width)> ButtonList { get; set; } = new List<(string ButtonName, string TitileName, int Width)>();

            /// <summary>
            /// 行样式样式委托（实体、实体对应的行）
            /// 示例：if (user.Name.Equals("李四"))row.DefaultCellStyle.ForeColor =  Color.Red;
            /// </summary>
            public Action<T, DataGridViewRow>? RowAction { get; set; } = null;

            /// <summary>
            /// 列按钮委托（列）
            /// 示例：if (col.Name.Equals("Name"))col.ReadOnly = false;
            /// </summary>
            public Action<DataGridViewColumn>? ColumnAction { get; set; } = null;

            /// <summary>
            /// 单元格样式委托（实体、当前列、通过实体和当前列筛选得到的单元格）
            /// 示例：if(user.Name.Equals("张三") && col.Name.Equals("Name"))cell.Style.BackColor = Color.Yellow;
            /// </summary>
            public Action<T, DataGridViewColumn, DataGridViewCell>? CellAction { get; set; } = null;

        }

        public static Form ShowOnlyOne(this Form currentForm, Form targetForm, bool inheritUI = true)
        {
            if (targetForm == null)
            {
                throw new ArgumentNullException(nameof(targetForm), "目标窗体实例不能为空！");
            }

            if (targetForm.IsDisposed)
            {
                throw new ObjectDisposedException(nameof(targetForm), "目标窗体实例已被释放，无法重复显示！");
            }

            Form existingForm = Application.OpenForms
                                .Cast<Form>() // 先转为强类型IEnumerable<Form>
                                .Where(f => f.GetType() == targetForm.GetType()) // 运行时判断类型一致
                                .FirstOrDefault();

            if (existingForm != null && existingForm != targetForm)
            {
                // 关闭传入的新实例，复用已打开的实例
                targetForm.Dispose();
                targetForm = existingForm;
            }

            // 继承UI样式（仅在窗体首次显示前执行）
            if (inheritUI && !targetForm.Visible)
            {
                targetForm.StartPosition = FormStartPosition.CenterScreen;
                targetForm.Icon = currentForm.Icon;
                targetForm.Font = currentForm.Font;
                targetForm.MaximizeBox = currentForm.MaximizeBox;
                targetForm.FormBorderStyle = currentForm.FormBorderStyle;
            }

            // 恢复最小化+置顶激活
            if (targetForm.WindowState == FormWindowState.Minimized)
            {
                targetForm.WindowState = FormWindowState.Normal;
            }
            targetForm.BringToFront();
            targetForm.Activate();

            // 显示窗体
            targetForm.Show();

            return targetForm;
        }

    }
}

namespace RedisClearApp
{
    partial class ConfigForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
            button4 = new Button();
            button3 = new Button();
            button2 = new Button();
            textBox1 = new TextBox();
            label5 = new Label();
            dataGridView1 = new DataGridView();
            button1 = new Button();
            groupBox2 = new GroupBox();
            label4 = new Label();
            numericUpDown4 = new NumericUpDown();
            numericUpDown2 = new NumericUpDown();
            label3 = new Label();
            label2 = new Label();
            numericUpDown3 = new NumericUpDown();
            label7 = new Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(button4);
            groupBox1.Controls.Add(button3);
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(dataGridView1);
            groupBox1.Location = new Point(8, 83);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(567, 357);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "连接字符串管理";
            // 
            // button4
            // 
            button4.Location = new Point(457, 26);
            button4.Name = "button4";
            button4.Size = new Size(104, 37);
            button4.TabIndex = 6;
            button4.Text = "保存备注";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button3
            // 
            button3.Location = new Point(285, 26);
            button3.Name = "button3";
            button3.Size = new Size(60, 37);
            button3.TabIndex = 5;
            button3.Text = "查询";
            button3.UseVisualStyleBackColor = true;
            button3.Click += textBox1_TextChanged;
            // 
            // button2
            // 
            button2.Location = new Point(370, 26);
            button2.Name = "button2";
            button2.Size = new Size(62, 37);
            button2.TabIndex = 4;
            button2.Text = "新增";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(62, 30);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(207, 27);
            textBox1.TabIndex = 2;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 33);
            label5.Name = "label5";
            label5.Size = new Size(54, 20);
            label5.TabIndex = 1;
            label5.Text = "检索：";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(9, 68);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new Size(552, 281);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // button1
            // 
            button1.Location = new Point(458, 26);
            button1.Name = "button1";
            button1.Size = new Size(102, 38);
            button1.TabIndex = 8;
            button1.Text = "设置定时";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(button1);
            groupBox2.Controls.Add(numericUpDown4);
            groupBox2.Controls.Add(numericUpDown2);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(numericUpDown3);
            groupBox2.Controls.Add(label7);
            groupBox2.Location = new Point(9, 1);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(566, 81);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "定时管理";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(407, 35);
            label4.Name = "label4";
            label4.Size = new Size(24, 20);
            label4.TabIndex = 7;
            label4.Text = "秒";
            // 
            // numericUpDown4
            // 
            numericUpDown4.Location = new Point(343, 31);
            numericUpDown4.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown4.Name = "numericUpDown4";
            numericUpDown4.Size = new Size(59, 27);
            numericUpDown4.TabIndex = 6;
            numericUpDown4.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // numericUpDown2
            // 
            numericUpDown2.Location = new Point(68, 31);
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(59, 27);
            numericUpDown2.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(265, 34);
            label3.Name = "label3";
            label3.Size = new Size(24, 20);
            label3.TabIndex = 5;
            label3.Text = "分";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(132, 34);
            label2.Name = "label2";
            label2.Size = new Size(24, 20);
            label2.TabIndex = 3;
            label2.Text = "时";
            // 
            // numericUpDown3
            // 
            numericUpDown3.Location = new Point(202, 30);
            numericUpDown3.Name = "numericUpDown3";
            numericUpDown3.Size = new Size(59, 27);
            numericUpDown3.TabIndex = 4;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(8, 33);
            label7.Name = "label7";
            label7.Size = new Size(54, 20);
            label7.TabIndex = 9;
            label7.Text = "定时：";
            // 
            // ConfigForm
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(581, 444);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            MaximizeBox = false;
            Name = "ConfigForm";
            Text = "配置管理";
            Load += ConfigForm_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Button button1;
        private Label label4;
        private NumericUpDown numericUpDown4;
        private Label label3;
        private NumericUpDown numericUpDown3;
        private Label label2;
        private NumericUpDown numericUpDown2;
        private Button button2;
        private TextBox textBox1;
        private Label label5;
        private DataGridView dataGridView1;
        private Button button3;
        private Button button4;
        private Label label7;
    }
}
namespace u_net
{
    partial class F_請求条件設定
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
            請求締日 = new TextBox();
            請求締日_ラベル = new Label();
            表示方法 = new GroupBox();
            表示方法Button3 = new RadioButton();
            表示方法Button2 = new RadioButton();
            表示方法Button1 = new RadioButton();
            OKボタン = new Button();
            キャンセルボタン = new Button();
            請求締日選択ボタン = new Button();
            顧客名_ラベル = new Label();
            顧客検索ボタン = new Button();
            締日分類 = new GroupBox();
            RoHS対応Button2 = new RadioButton();
            RoHS対応Button1 = new RadioButton();
            顧客コード = new TextBox();
            顧客名 = new TextBox();
            groupBox1 = new GroupBox();
            表示方法.SuspendLayout();
            締日分類.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // 請求締日
            // 
            請求締日.BackColor = Color.White;
            請求締日.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            請求締日.ImeMode = ImeMode.Disable;
            請求締日.Location = new Point(121, 100);
            請求締日.Name = "請求締日";
            請求締日.Size = new Size(102, 20);
            請求締日.TabIndex = 3;
            // 
            // 請求締日_ラベル
            // 
            請求締日_ラベル.AllowDrop = true;
            請求締日_ラベル.AutoEllipsis = true;
            請求締日_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            請求締日_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            請求締日_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            請求締日_ラベル.Location = new Point(13, 100);
            請求締日_ラベル.Margin = new Padding(0);
            請求締日_ラベル.Name = "請求締日_ラベル";
            請求締日_ラベル.Size = new Size(98, 27);
            請求締日_ラベル.TabIndex = 2;
            請求締日_ラベル.Text = "請求締日(&D)";
            請求締日_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 表示方法
            // 
            表示方法.Controls.Add(表示方法Button3);
            表示方法.Controls.Add(表示方法Button2);
            表示方法.Controls.Add(表示方法Button1);
            表示方法.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            表示方法.Location = new Point(13, 172);
            表示方法.Margin = new Padding(3, 4, 3, 4);
            表示方法.Name = "表示方法";
            表示方法.Padding = new Padding(3, 4, 3, 4);
            表示方法.Size = new Size(439, 67);
            表示方法.TabIndex = 8;
            表示方法.TabStop = false;
            表示方法.Text = "表示方法";
            // 
            // 表示方法Button3
            // 
            表示方法Button3.AutoSize = true;
            表示方法Button3.Location = new Point(285, 29);
            表示方法Button3.Margin = new Padding(3, 4, 3, 4);
            表示方法Button3.Name = "表示方法Button3";
            表示方法Button3.Size = new Size(131, 16);
            表示方法Button3.TabIndex = 204;
            表示方法Button3.TabStop = true;
            表示方法Button3.Text = "売上がある顧客のみ";
            表示方法Button3.UseVisualStyleBackColor = true;
            // 
            // 表示方法Button2
            // 
            表示方法Button2.AutoSize = true;
            表示方法Button2.Location = new Point(113, 29);
            表示方法Button2.Margin = new Padding(3, 4, 3, 4);
            表示方法Button2.Name = "表示方法Button2";
            表示方法Button2.Size = new Size(143, 16);
            表示方法Button2.TabIndex = 2;
            表示方法Button2.TabStop = true;
            表示方法Button2.Text = "売掛残がある顧客のみ";
            表示方法Button2.UseVisualStyleBackColor = true;
            // 
            // 表示方法Button1
            // 
            表示方法Button1.AutoSize = true;
            表示方法Button1.Location = new Point(26, 29);
            表示方法Button1.Margin = new Padding(3, 4, 3, 4);
            表示方法Button1.Name = "表示方法Button1";
            表示方法Button1.Size = new Size(59, 16);
            表示方法Button1.TabIndex = 1;
            表示方法Button1.TabStop = true;
            表示方法Button1.Text = "全一覧";
            表示方法Button1.UseVisualStyleBackColor = true;
            // 
            // OKボタン
            // 
            OKボタン.Location = new Point(229, 293);
            OKボタン.Margin = new Padding(3, 4, 3, 4);
            OKボタン.Name = "OKボタン";
            OKボタン.Size = new Size(117, 32);
            OKボタン.TabIndex = 17;
            OKボタン.Text = "OK";
            OKボタン.UseVisualStyleBackColor = true;
            OKボタン.Click += OKボタン_Click;
            // 
            // キャンセルボタン
            // 
            キャンセルボタン.Location = new Point(353, 293);
            キャンセルボタン.Margin = new Padding(3, 4, 3, 4);
            キャンセルボタン.Name = "キャンセルボタン";
            キャンセルボタン.Size = new Size(117, 32);
            キャンセルボタン.TabIndex = 18;
            キャンセルボタン.Text = "キャンセル";
            キャンセルボタン.UseVisualStyleBackColor = true;
            キャンセルボタン.MouseClick += キャンセルボタン_MouseClick;
            // 
            // 請求締日選択ボタン
            // 
            請求締日選択ボタン.Location = new Point(225, 100);
            請求締日選択ボタン.Margin = new Padding(5, 5, 5, 5);
            請求締日選択ボタン.Name = "請求締日選択ボタン";
            請求締日選択ボタン.Size = new Size(24, 28);
            請求締日選択ボタン.TabIndex = 4;
            請求締日選択ボタン.Text = "▼";
            請求締日選択ボタン.UseVisualStyleBackColor = true;
            請求締日選択ボタン.Click += 請求締日選択ボタン_Click;
            // 
            // 顧客名_ラベル
            // 
            顧客名_ラベル.AllowDrop = true;
            顧客名_ラベル.AutoEllipsis = true;
            顧客名_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            顧客名_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            顧客名_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            顧客名_ラベル.Location = new Point(13, 135);
            顧客名_ラベル.Margin = new Padding(0);
            顧客名_ラベル.Name = "顧客名_ラベル";
            顧客名_ラベル.Size = new Size(109, 28);
            顧客名_ラベル.TabIndex = 5;
            顧客名_ラベル.Text = "顧客名(&C)";
            顧客名_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 顧客検索ボタン
            // 
            顧客検索ボタン.Location = new Point(197, 135);
            顧客検索ボタン.Margin = new Padding(5, 5, 5, 5);
            顧客検索ボタン.Name = "顧客検索ボタン";
            顧客検索ボタン.Size = new Size(24, 28);
            顧客検索ボタン.TabIndex = 7;
            顧客検索ボタン.Text = "▼";
            顧客検索ボタン.UseVisualStyleBackColor = true;
            顧客検索ボタン.Click += 顧客検索ボタン_Click;
            // 
            // 締日分類
            // 
            締日分類.Controls.Add(RoHS対応Button2);
            締日分類.Controls.Add(RoHS対応Button1);
            締日分類.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            締日分類.Location = new Point(13, 29);
            締日分類.Margin = new Padding(3, 4, 3, 4);
            締日分類.Name = "締日分類";
            締日分類.Padding = new Padding(3, 4, 3, 4);
            締日分類.Size = new Size(439, 67);
            締日分類.TabIndex = 1;
            締日分類.TabStop = false;
            締日分類.Text = "締日分類";
            // 
            // RoHS対応Button2
            // 
            RoHS対応Button2.AutoSize = true;
            RoHS対応Button2.Location = new Point(115, 29);
            RoHS対応Button2.Margin = new Padding(3, 4, 3, 4);
            RoHS対応Button2.Name = "RoHS対応Button2";
            RoHS対応Button2.Size = new Size(71, 16);
            RoHS対応Button2.TabIndex = 2;
            RoHS対応Button2.TabStop = true;
            RoHS対応Button2.Text = "指定締日";
            RoHS対応Button2.UseVisualStyleBackColor = true;
            // 
            // RoHS対応Button1
            // 
            RoHS対応Button1.AutoSize = true;
            RoHS対応Button1.Location = new Point(27, 29);
            RoHS対応Button1.Margin = new Padding(3, 4, 3, 4);
            RoHS対応Button1.Name = "RoHS対応Button1";
            RoHS対応Button1.Size = new Size(71, 16);
            RoHS対応Button1.TabIndex = 1;
            RoHS対応Button1.TabStop = true;
            RoHS対応Button1.Text = "月次締日";
            RoHS対応Button1.UseVisualStyleBackColor = true;
            // 
            // 顧客コード
            // 
            顧客コード.BackColor = Color.White;
            顧客コード.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            顧客コード.ImeMode = ImeMode.Disable;
            顧客コード.Location = new Point(121, 136);
            顧客コード.Name = "顧客コード";
            顧客コード.Size = new Size(74, 20);
            顧客コード.TabIndex = 21017;
            顧客コード.KeyDown += 顧客コード_KeyDown;
            // 
            // 顧客名
            // 
            顧客名.BackColor = Color.White;
            顧客名.Enabled = false;
            顧客名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            顧客名.ImeMode = ImeMode.NoControl;
            顧客名.Location = new Point(222, 136);
            顧客名.Name = "顧客名";
            顧客名.ReadOnly = true;
            顧客名.Size = new Size(219, 20);
            顧客名.TabIndex = 21018;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(表示方法);
            groupBox1.Controls.Add(締日分類);
            groupBox1.Controls.Add(顧客名);
            groupBox1.Controls.Add(請求締日_ラベル);
            groupBox1.Controls.Add(顧客コード);
            groupBox1.Controls.Add(請求締日);
            groupBox1.Controls.Add(請求締日選択ボタン);
            groupBox1.Controls.Add(顧客検索ボタン);
            groupBox1.Controls.Add(顧客名_ラベル);
            groupBox1.Location = new Point(7, -4);
            groupBox1.Margin = new Padding(3, 4, 3, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 4, 3, 4);
            groupBox1.Size = new Size(463, 284);
            groupBox1.TabIndex = 21019;
            groupBox1.TabStop = false;
            // 
            // F_請求条件設定
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(477, 340);
            Controls.Add(groupBox1);
            Controls.Add(キャンセルボタン);
            Controls.Add(OKボタン);
            Margin = new Padding(3, 4, 3, 4);
            Name = "F_請求条件設定";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "請求処理 - 抽出";
            Load += Form_Load;
            KeyDown += F_請求条件設定_KeyDown;
            表示方法.ResumeLayout(false);
            表示方法.PerformLayout();
            締日分類.ResumeLayout(false);
            締日分類.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ComboBox 更新者名;
        private Label label9;
        private TextBox 請求締日;
        private Label 請求締日_ラベル;
        private Label 顧客名_ラベル;
        private GroupBox 表示方法;
        private RadioButton 表示方法Button3;
        private RadioButton 表示方法Button2;
        private RadioButton 表示方法Button1;
        private Button OKボタン;
        private Button キャンセルボタン;
        private Button 請求締日選択ボタン;
        private Label 更新者名_ラベル;
        private Button 顧客検索ボタン;
        private GroupBox 締日分類;
        private RadioButton RoHS対応Button2;
        private RadioButton RoHS対応Button1;
        private TextBox 顧客コード;
        private TextBox 顧客名;
        private GroupBox groupBox1;
    }
}
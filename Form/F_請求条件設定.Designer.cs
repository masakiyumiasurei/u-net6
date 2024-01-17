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
            削除指定Button3 = new RadioButton();
            削除指定Button2 = new RadioButton();
            削除指定Button1 = new RadioButton();
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
            請求締日.Location = new Point(106, 75);
            請求締日.Margin = new Padding(3, 2, 3, 2);
            請求締日.Name = "請求締日";
            請求締日.Size = new Size(90, 20);
            請求締日.TabIndex = 3;
            // 
            // 請求締日_ラベル
            // 
            請求締日_ラベル.AllowDrop = true;
            請求締日_ラベル.AutoEllipsis = true;
            請求締日_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            請求締日_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            請求締日_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            請求締日_ラベル.Location = new Point(11, 75);
            請求締日_ラベル.Margin = new Padding(0);
            請求締日_ラベル.Name = "請求締日_ラベル";
            請求締日_ラベル.Size = new Size(95, 20);
            請求締日_ラベル.TabIndex = 2;
            請求締日_ラベル.Text = "請求締日(&D)";
            請求締日_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 表示方法
            // 
            表示方法.Controls.Add(削除指定Button3);
            表示方法.Controls.Add(削除指定Button2);
            表示方法.Controls.Add(削除指定Button1);
            表示方法.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            表示方法.Location = new Point(11, 129);
            表示方法.Name = "表示方法";
            表示方法.Size = new Size(384, 50);
            表示方法.TabIndex = 8;
            表示方法.TabStop = false;
            表示方法.Text = "表示方法";
            // 
            // 削除指定Button3
            // 
            削除指定Button3.AutoSize = true;
            削除指定Button3.Location = new Point(249, 22);
            削除指定Button3.Name = "削除指定Button3";
            削除指定Button3.Size = new Size(131, 16);
            削除指定Button3.TabIndex = 204;
            削除指定Button3.TabStop = true;
            削除指定Button3.Text = "売上がある顧客のみ";
            削除指定Button3.UseVisualStyleBackColor = true;
            // 
            // 削除指定Button2
            // 
            削除指定Button2.AutoSize = true;
            削除指定Button2.Location = new Point(99, 22);
            削除指定Button2.Name = "削除指定Button2";
            削除指定Button2.Size = new Size(143, 16);
            削除指定Button2.TabIndex = 2;
            削除指定Button2.TabStop = true;
            削除指定Button2.Text = "売掛残がある顧客のみ";
            削除指定Button2.UseVisualStyleBackColor = true;
            // 
            // 削除指定Button1
            // 
            削除指定Button1.AutoSize = true;
            削除指定Button1.Location = new Point(23, 22);
            削除指定Button1.Name = "削除指定Button1";
            削除指定Button1.Size = new Size(59, 16);
            削除指定Button1.TabIndex = 1;
            削除指定Button1.TabStop = true;
            削除指定Button1.Text = "全一覧";
            削除指定Button1.UseVisualStyleBackColor = true;
            // 
            // OKボタン
            // 
            OKボタン.Location = new Point(200, 220);
            OKボタン.Name = "OKボタン";
            OKボタン.Size = new Size(102, 24);
            OKボタン.TabIndex = 17;
            OKボタン.Text = "OK";
            OKボタン.UseVisualStyleBackColor = true;
            OKボタン.Click += OKボタン_Click;
            // 
            // キャンセルボタン
            // 
            キャンセルボタン.Location = new Point(309, 220);
            キャンセルボタン.Name = "キャンセルボタン";
            キャンセルボタン.Size = new Size(102, 24);
            キャンセルボタン.TabIndex = 18;
            キャンセルボタン.Text = "キャンセル";
            キャンセルボタン.UseVisualStyleBackColor = true;
            キャンセルボタン.MouseClick += キャンセルボタン_MouseClick;
            // 
            // 請求締日選択ボタン
            // 
            請求締日選択ボタン.Location = new Point(197, 75);
            請求締日選択ボタン.Margin = new Padding(4);
            請求締日選択ボタン.Name = "請求締日選択ボタン";
            請求締日選択ボタン.Size = new Size(21, 21);
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
            顧客名_ラベル.Location = new Point(11, 101);
            顧客名_ラベル.Margin = new Padding(0);
            顧客名_ラベル.Name = "顧客名_ラベル";
            顧客名_ラベル.Size = new Size(95, 21);
            顧客名_ラベル.TabIndex = 5;
            顧客名_ラベル.Text = "顧客名(&C)";
            顧客名_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 顧客検索ボタン
            // 
            顧客検索ボタン.Location = new Point(172, 101);
            顧客検索ボタン.Margin = new Padding(4);
            顧客検索ボタン.Name = "顧客検索ボタン";
            顧客検索ボタン.Size = new Size(21, 21);
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
            締日分類.Location = new Point(11, 22);
            締日分類.Name = "締日分類";
            締日分類.Size = new Size(384, 50);
            締日分類.TabIndex = 1;
            締日分類.TabStop = false;
            締日分類.Text = "締日分類";
            // 
            // RoHS対応Button2
            // 
            RoHS対応Button2.AutoSize = true;
            RoHS対応Button2.Location = new Point(101, 22);
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
            RoHS対応Button1.Location = new Point(24, 22);
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
            顧客コード.Location = new Point(106, 102);
            顧客コード.Margin = new Padding(3, 2, 3, 2);
            顧客コード.Name = "顧客コード";
            顧客コード.Size = new Size(65, 20);
            顧客コード.TabIndex = 21017;
            // 
            // 顧客名
            // 
            顧客名.BackColor = Color.White;
            顧客名.Enabled = false;
            顧客名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            顧客名.ImeMode = ImeMode.NoControl;
            顧客名.Location = new Point(194, 102);
            顧客名.Margin = new Padding(3, 2, 3, 2);
            顧客名.Name = "顧客名";
            顧客名.ReadOnly = true;
            顧客名.Size = new Size(192, 20);
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
            groupBox1.Location = new Point(6, -3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(405, 213);
            groupBox1.TabIndex = 21019;
            groupBox1.TabStop = false;
            // 
            // F_請求条件設定
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(417, 255);
            Controls.Add(groupBox1);
            Controls.Add(キャンセルボタン);
            Controls.Add(OKボタン);
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
        private RadioButton 削除指定Button3;
        private RadioButton 削除指定Button2;
        private RadioButton 削除指定Button1;
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
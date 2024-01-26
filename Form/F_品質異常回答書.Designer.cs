namespace u_net
{
    partial class F_品質異常回答書
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
            処置日 = new TextBox();
            処置日_ラベル = new Label();
            登録ボタン = new Button();
            閉じるボタン = new Button();
            処置日選択ボタン = new Button();
            groupBox1 = new GroupBox();
            label1 = new Label();
            処置方法コード = new ComboBox();
            処置方法_ラベル = new Label();
            参照文書コード = new TextBox();
            文書コード_ラベル = new Label();
            処置内容 = new TextBox();
            処置内容_ラベル = new Label();
            処置担当者 = new TextBox();
            処置担当者_ラベル = new Label();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            groupBox1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // 処置日
            // 
            処置日.BackColor = Color.White;
            処置日.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            処置日.ImeMode = ImeMode.Disable;
            処置日.Location = new Point(107, 42);
            処置日.Margin = new Padding(3, 2, 3, 2);
            処置日.Name = "処置日";
            処置日.Size = new Size(102, 20);
            処置日.TabIndex = 4;
            処置日.DoubleClick += 処置日_DoubleClick;
            処置日.Enter += 処置日_Enter;
            処置日.KeyPress += 処置日_KeyPress;
            処置日.Leave += 処置日_Leave;
            処置日.Validating += 処置日_Validating;
            // 
            // 処置日_ラベル
            // 
            処置日_ラベル.AllowDrop = true;
            処置日_ラベル.AutoEllipsis = true;
            処置日_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            処置日_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            処置日_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            処置日_ラベル.Location = new Point(11, 42);
            処置日_ラベル.Margin = new Padding(0);
            処置日_ラベル.Name = "処置日_ラベル";
            処置日_ラベル.Size = new Size(95, 19);
            処置日_ラベル.TabIndex = 3;
            処置日_ラベル.Text = "処置日(&D)";
            処置日_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 登録ボタン
            // 
            登録ボタン.Location = new Point(221, 525);
            登録ボタン.Name = "登録ボタン";
            登録ボタン.Size = new Size(102, 24);
            登録ボタン.TabIndex = 10;
            登録ボタン.Text = "登録(&R)";
            登録ボタン.UseVisualStyleBackColor = true;
            登録ボタン.Click += 登録ボタン_Click;
            // 
            // 閉じるボタン
            // 
            閉じるボタン.Location = new Point(330, 525);
            閉じるボタン.Name = "閉じるボタン";
            閉じるボタン.Size = new Size(102, 24);
            閉じるボタン.TabIndex = 11;
            閉じるボタン.Text = "閉じる(&C)";
            閉じるボタン.UseVisualStyleBackColor = true;
            閉じるボタン.Click += 閉じるボタン_Click;
            // 
            // 処置日選択ボタン
            // 
            処置日選択ボタン.Location = new Point(212, 42);
            処置日選択ボタン.Margin = new Padding(4);
            処置日選択ボタン.Name = "処置日選択ボタン";
            処置日選択ボタン.Size = new Size(21, 21);
            処置日選択ボタン.TabIndex = 21016;
            処置日選択ボタン.TabStop = false;
            処置日選択ボタン.Text = "▼";
            処置日選択ボタン.UseVisualStyleBackColor = true;
            処置日選択ボタン.Click += 処置日選択ボタン_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(処置方法コード);
            groupBox1.Controls.Add(処置方法_ラベル);
            groupBox1.Controls.Add(参照文書コード);
            groupBox1.Controls.Add(文書コード_ラベル);
            groupBox1.Controls.Add(処置内容);
            groupBox1.Controls.Add(処置内容_ラベル);
            groupBox1.Controls.Add(処置担当者);
            groupBox1.Controls.Add(処置日選択ボタン);
            groupBox1.Controls.Add(処置日_ラベル);
            groupBox1.Controls.Add(処置担当者_ラベル);
            groupBox1.Controls.Add(処置日);
            groupBox1.Location = new Point(6, 7);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(425, 509);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AllowDrop = true;
            label1.AutoEllipsis = true;
            label1.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ActiveCaptionText;
            label1.ImageAlign = ContentAlignment.MiddleLeft;
            label1.Location = new Point(107, 479);
            label1.Margin = new Padding(0);
            label1.Name = "label1";
            label1.Size = new Size(307, 19);
            label1.TabIndex = 21017;
            label1.Text = "※関連する是正・予防処置報告書";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 処置方法コード
            // 
            処置方法コード.BackColor = Color.White;
            処置方法コード.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            処置方法コード.FormattingEnabled = true;
            処置方法コード.ImeMode = ImeMode.Disable;
            処置方法コード.Location = new Point(107, 17);
            処置方法コード.Name = "処置方法コード";
            処置方法コード.Size = new Size(146, 21);
            処置方法コード.TabIndex = 2;
            処置方法コード.Enter += 処置方法コード_Enter;
            処置方法コード.Leave += 処置方法コード_Leave;
            // 
            // 処置方法_ラベル
            // 
            処置方法_ラベル.AllowDrop = true;
            処置方法_ラベル.AutoEllipsis = true;
            処置方法_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            処置方法_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            処置方法_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            処置方法_ラベル.Location = new Point(11, 18);
            処置方法_ラベル.Margin = new Padding(0);
            処置方法_ラベル.Name = "処置方法_ラベル";
            処置方法_ラベル.Size = new Size(95, 19);
            処置方法_ラベル.TabIndex = 1;
            処置方法_ラベル.Text = "処置方法(&H)";
            処置方法_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 参照文書コード
            // 
            参照文書コード.BackColor = Color.White;
            参照文書コード.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            参照文書コード.ImeMode = ImeMode.Disable;
            参照文書コード.Location = new Point(107, 455);
            参照文書コード.Margin = new Padding(3, 2, 3, 2);
            参照文書コード.Name = "参照文書コード";
            参照文書コード.Size = new Size(126, 20);
            参照文書コード.TabIndex = 10;
            参照文書コード.TextChanged += 参照文書コード_TextChanged;
            参照文書コード.Enter += 参照文書コード_Enter;
            参照文書コード.KeyDown += 参照文書コード_KeyDown;
            参照文書コード.Leave += 参照文書コード_Leave;
            // 
            // 文書コード_ラベル
            // 
            文書コード_ラベル.AllowDrop = true;
            文書コード_ラベル.AutoEllipsis = true;
            文書コード_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            文書コード_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            文書コード_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            文書コード_ラベル.Location = new Point(11, 456);
            文書コード_ラベル.Margin = new Padding(0);
            文書コード_ラベル.Name = "文書コード_ラベル";
            文書コード_ラベル.Size = new Size(95, 19);
            文書コード_ラベル.TabIndex = 9;
            文書コード_ラベル.Text = "文書コード(&C)";
            文書コード_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 処置内容
            // 
            処置内容.BackColor = SystemColors.Window;
            処置内容.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            処置内容.ImeMode = ImeMode.Hiragana;
            処置内容.Location = new Point(11, 108);
            処置内容.Margin = new Padding(3, 2, 3, 2);
            処置内容.Multiline = true;
            処置内容.Name = "処置内容";
            処置内容.Size = new Size(403, 337);
            処置内容.TabIndex = 8;
            処置内容.TextChanged += 処置内容_TextChanged;
            処置内容.DoubleClick += 処置内容_DoubleClick;
            処置内容.Enter += 処置内容_Enter;
            処置内容.Leave += 処置内容_Leave;
            処置内容.Validating += 処置内容_Validating;
            // 
            // 処置内容_ラベル
            // 
            処置内容_ラベル.AllowDrop = true;
            処置内容_ラベル.AutoEllipsis = true;
            処置内容_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            処置内容_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            処置内容_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            処置内容_ラベル.Location = new Point(11, 90);
            処置内容_ラベル.Margin = new Padding(0);
            処置内容_ラベル.Name = "処置内容_ラベル";
            処置内容_ラベル.Size = new Size(403, 19);
            処置内容_ラベル.TabIndex = 7;
            処置内容_ラベル.Text = "処置内容(&N)";
            処置内容_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 処置担当者
            // 
            処置担当者.BackColor = Color.White;
            処置担当者.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            処置担当者.ImeMode = ImeMode.Hiragana;
            処置担当者.Location = new Point(107, 67);
            処置担当者.Margin = new Padding(3, 2, 3, 2);
            処置担当者.Name = "処置担当者";
            処置担当者.Size = new Size(307, 20);
            処置担当者.TabIndex = 6;
            処置担当者.Enter += 処置担当者_Enter;
            処置担当者.Leave += 処置担当者_Leave;
            // 
            // 処置担当者_ラベル
            // 
            処置担当者_ラベル.AllowDrop = true;
            処置担当者_ラベル.AutoEllipsis = true;
            処置担当者_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            処置担当者_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            処置担当者_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            処置担当者_ラベル.Location = new Point(11, 67);
            処置担当者_ラベル.Margin = new Padding(0);
            処置担当者_ラベル.Name = "処置担当者_ラベル";
            処置担当者_ラベル.Size = new Size(95, 19);
            処置担当者_ラベル.TabIndex = 5;
            処置担当者_ラベル.Text = "処置担当者(&M)";
            処置担当者_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 556);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 19, 0);
            statusStrip1.Size = new Size(441, 22);
            statusStrip1.TabIndex = 10196;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(89, 17);
            toolStripStatusLabel1.Text = "各種項目の説明";
            // 
            // F_品質異常回答書
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(441, 578);
            Controls.Add(statusStrip1);
            Controls.Add(groupBox1);
            Controls.Add(閉じるボタン);
            Controls.Add(登録ボタン);
            Name = "F_品質異常回答書";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "環境回答書";
            Load += Form_Load;
            KeyDown += F_品質異常回答書_KeyDown;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox 更新者名;
        private Label label9;
        private Label 型番_ラベル;
        private TextBox 型番;
        private TextBox 品名;
        private Label 処置日_ラベル;
        private Label label2;
        private TextBox 更新日終了;
        private GroupBox groupBox3;
        private RadioButton 確定指定Button3;
        private RadioButton 確定指定Button2;
        private RadioButton 確定指定Button1;
        private GroupBox groupBox4;
        private RadioButton 承認指定button3;
        private RadioButton 承認指定button2;
        private RadioButton 承認指定button1;
        private Button 登録ボタン;
        private Button 閉じるボタン;
        private Button 更新日開始選択ボタン;
        private Label 更新者名_ラベル;
        private TextBox 更新日開始;
        private Button 処置日選択ボタン;
        private GroupBox groupBox1;
        private Label 処置担当者_ラベル;
        private TextBox 処置担当者;
        private Label 処置内容_ラベル;
        private TextBox 参照文書コード;
        private Label 文書コード_ラベル;
        private TextBox 処置内容;
        private TextBox 処置日;
        private StatusStrip statusStrip1;
        internal ToolStripStatusLabel toolStripStatusLabel1;
        private GroupBox groupBox2;
        private RadioButton 廃止指定Button3;
        private RadioButton 廃止指定Button2;
        private RadioButton 廃止指定Button1;
        private RadioButton RoHS対応Button3;
        private RadioButton RoHS対応Button2;
        private RadioButton RoHS対応Button1;
        private Label 非含有証明書_ラベル;
        private ComboBox 処置方法コード;
        private Label 処置方法_ラベル;
        private Label label1;
    }
}
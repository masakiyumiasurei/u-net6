namespace u_net
{
    partial class F_是正予防処置回答_環境
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
            回答者_ラベル = new Label();
            OKボタン = new Button();
            キャンセルボタン = new Button();
            groupBox1 = new GroupBox();
            label2 = new Label();
            修正緩和処置 = new TextBox();
            label1 = new Label();
            予防処置_ラベル = new Label();
            是正処置_ラベル = new Label();
            影響範囲_ラベル = new Label();
            原因_ラベル = new Label();
            予防処置 = new TextBox();
            是正処置 = new TextBox();
            影響範囲 = new TextBox();
            原因 = new TextBox();
            回答者名 = new TextBox();
            回答者コード = new ComboBox();
            状況 = new TextBox();
            状況_ラベル = new Label();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            文書コード = new TextBox();
            版数 = new TextBox();
            groupBox1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // 回答者_ラベル
            // 
            回答者_ラベル.AllowDrop = true;
            回答者_ラベル.AutoEllipsis = true;
            回答者_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            回答者_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            回答者_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            回答者_ラベル.Location = new Point(11, 17);
            回答者_ラベル.Margin = new Padding(0);
            回答者_ラベル.Name = "回答者_ラベル";
            回答者_ラベル.Size = new Size(95, 19);
            回答者_ラベル.TabIndex = 1;
            回答者_ラベル.Text = "回答者";
            回答者_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // OKボタン
            // 
            OKボタン.Location = new Point(261, 561);
            OKボタン.Name = "OKボタン";
            OKボタン.Size = new Size(102, 24);
            OKボタン.TabIndex = 15;
            OKボタン.Text = "OK(&O)";
            OKボタン.UseVisualStyleBackColor = true;
            OKボタン.Click += 登録ボタン_Click;
            // 
            // キャンセルボタン
            // 
            キャンセルボタン.Location = new Point(370, 561);
            キャンセルボタン.Name = "キャンセルボタン";
            キャンセルボタン.Size = new Size(102, 24);
            キャンセルボタン.TabIndex = 16;
            キャンセルボタン.Text = "キャンセル(&X)";
            キャンセルボタン.UseVisualStyleBackColor = true;
            キャンセルボタン.Click += 閉じるボタン_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(修正緩和処置);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(予防処置_ラベル);
            groupBox1.Controls.Add(是正処置_ラベル);
            groupBox1.Controls.Add(影響範囲_ラベル);
            groupBox1.Controls.Add(原因_ラベル);
            groupBox1.Controls.Add(予防処置);
            groupBox1.Controls.Add(是正処置);
            groupBox1.Controls.Add(影響範囲);
            groupBox1.Controls.Add(原因);
            groupBox1.Controls.Add(回答者名);
            groupBox1.Controls.Add(回答者コード);
            groupBox1.Controls.Add(状況);
            groupBox1.Controls.Add(状況_ラベル);
            groupBox1.Controls.Add(回答者_ラベル);
            groupBox1.Location = new Point(6, 7);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(466, 544);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            // 
            // label2
            // 
            label2.AllowDrop = true;
            label2.AutoEllipsis = true;
            label2.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ActiveCaptionText;
            label2.ImageAlign = ContentAlignment.MiddleLeft;
            label2.Location = new Point(11, 258);
            label2.Margin = new Padding(0);
            label2.Name = "label2";
            label2.Size = new Size(95, 31);
            label2.TabIndex = 9;
            label2.Text = "修正・緩和\r\n処置";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 修正緩和処置
            // 
            修正緩和処置.BackColor = SystemColors.Window;
            修正緩和処置.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            修正緩和処置.ImeMode = ImeMode.Hiragana;
            修正緩和処置.Location = new Point(107, 258);
            修正緩和処置.Margin = new Padding(3, 2, 3, 2);
            修正緩和処置.Multiline = true;
            修正緩和処置.Name = "修正緩和処置";
            修正緩和処置.Size = new Size(353, 85);
            修正緩和処置.TabIndex = 10;
            修正緩和処置.TextChanged += 修正緩和処置_TextChanged;
            修正緩和処置.DoubleClick += 修正緩和処置_DoubleClick;
            修正緩和処置.Enter += 修正緩和処置_Enter;
            修正緩和処置.Leave += 修正緩和処置_Leave;
            修正緩和処置.Validating += 修正緩和処置_Validating;
            修正緩和処置.Validated += 修正緩和処置_Validated;
            // 
            // label1
            // 
            label1.AllowDrop = true;
            label1.AutoEllipsis = true;
            label1.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ActiveCaptionText;
            label1.ImageAlign = ContentAlignment.MiddleLeft;
            label1.Location = new Point(22, 517);
            label1.Margin = new Padding(0);
            label1.Name = "label1";
            label1.Size = new Size(438, 19);
            label1.TabIndex = 16;
            label1.Text = "※対策記述後に必ず原因との整合性を確認し、真因追求を行ってください。";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 予防処置_ラベル
            // 
            予防処置_ラベル.AllowDrop = true;
            予防処置_ラベル.AutoEllipsis = true;
            予防処置_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            予防処置_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            予防処置_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            予防処置_ラベル.Location = new Point(11, 431);
            予防処置_ラベル.Margin = new Padding(0);
            予防処置_ラベル.Name = "予防処置_ラベル";
            予防処置_ラベル.Size = new Size(95, 19);
            予防処置_ラベル.TabIndex = 13;
            予防処置_ラベル.Text = "予防処置";
            予防処置_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 是正処置_ラベル
            // 
            是正処置_ラベル.AllowDrop = true;
            是正処置_ラベル.AutoEllipsis = true;
            是正処置_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            是正処置_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            是正処置_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            是正処置_ラベル.Location = new Point(11, 347);
            是正処置_ラベル.Margin = new Padding(0);
            是正処置_ラベル.Name = "是正処置_ラベル";
            是正処置_ラベル.Size = new Size(95, 19);
            是正処置_ラベル.TabIndex = 11;
            是正処置_ラベル.Text = "是正処置";
            是正処置_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 影響範囲_ラベル
            // 
            影響範囲_ラベル.AllowDrop = true;
            影響範囲_ラベル.AutoEllipsis = true;
            影響範囲_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            影響範囲_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            影響範囲_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            影響範囲_ラベル.Location = new Point(11, 215);
            影響範囲_ラベル.Margin = new Padding(0);
            影響範囲_ラベル.Name = "影響範囲_ラベル";
            影響範囲_ラベル.Size = new Size(95, 19);
            影響範囲_ラベル.TabIndex = 7;
            影響範囲_ラベル.Text = "影響範囲";
            影響範囲_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 原因_ラベル
            // 
            原因_ラベル.AllowDrop = true;
            原因_ラベル.AutoEllipsis = true;
            原因_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            原因_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            原因_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            原因_ラベル.Location = new Point(11, 82);
            原因_ラベル.Margin = new Padding(0);
            原因_ラベル.Name = "原因_ラベル";
            原因_ラベル.Size = new Size(95, 19);
            原因_ラベル.TabIndex = 5;
            原因_ラベル.Text = "原因";
            原因_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 予防処置
            // 
            予防処置.BackColor = SystemColors.Window;
            予防処置.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            予防処置.ImeMode = ImeMode.Hiragana;
            予防処置.Location = new Point(107, 431);
            予防処置.Margin = new Padding(3, 2, 3, 2);
            予防処置.Multiline = true;
            予防処置.Name = "予防処置";
            予防処置.Size = new Size(353, 80);
            予防処置.TabIndex = 14;
            予防処置.TextChanged += 予防処置_TextChanged;
            予防処置.DoubleClick += 予防処置_DoubleClick;
            予防処置.Enter += 予防処置_Enter;
            予防処置.Leave += 予防処置_Leave;
            予防処置.Validating += 予防処置_Validating;
            予防処置.Validated += 予防処置_Validated;
            // 
            // 是正処置
            // 
            是正処置.BackColor = SystemColors.Window;
            是正処置.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            是正処置.ImeMode = ImeMode.Hiragana;
            是正処置.Location = new Point(107, 347);
            是正処置.Margin = new Padding(3, 2, 3, 2);
            是正処置.Multiline = true;
            是正処置.Name = "是正処置";
            是正処置.Size = new Size(353, 80);
            是正処置.TabIndex = 12;
            是正処置.TextChanged += 是正処置_TextChanged;
            是正処置.DoubleClick += 是正処置_DoubleClick;
            是正処置.Enter += 是正処置_Enter;
            是正処置.Leave += 是正処置_Leave;
            是正処置.Validating += 是正処置_Validating;
            是正処置.Validated += 是正処置_Validated;
            // 
            // 影響範囲
            // 
            影響範囲.BackColor = SystemColors.Window;
            影響範囲.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            影響範囲.ImeMode = ImeMode.Hiragana;
            影響範囲.Location = new Point(107, 215);
            影響範囲.Margin = new Padding(3, 2, 3, 2);
            影響範囲.Multiline = true;
            影響範囲.Name = "影響範囲";
            影響範囲.Size = new Size(353, 39);
            影響範囲.TabIndex = 8;
            影響範囲.TextChanged += 影響範囲_TextChanged;
            影響範囲.DoubleClick += 影響範囲_DoubleClick;
            影響範囲.Enter += 影響範囲_Enter;
            影響範囲.Leave += 影響範囲_Leave;
            影響範囲.Validating += 影響範囲_Validating;
            影響範囲.Validated += 影響範囲_Validated;
            // 
            // 原因
            // 
            原因.BackColor = SystemColors.Window;
            原因.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            原因.ImeMode = ImeMode.Hiragana;
            原因.Location = new Point(107, 82);
            原因.Margin = new Padding(3, 2, 3, 2);
            原因.Multiline = true;
            原因.Name = "原因";
            原因.Size = new Size(353, 130);
            原因.TabIndex = 6;
            原因.TextChanged += 原因_TextChanged;
            原因.DoubleClick += 原因_DoubleClick;
            原因.Enter += 原因_Enter;
            原因.Leave += 原因_Leave;
            原因.Validating += 原因_Validating;
            原因.Validated += 原因_Validated;
            // 
            // 回答者名
            // 
            回答者名.BackColor = Color.White;
            回答者名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            回答者名.ImeMode = ImeMode.NoControl;
            回答者名.Location = new Point(189, 16);
            回答者名.Margin = new Padding(3, 2, 3, 2);
            回答者名.Name = "回答者名";
            回答者名.ReadOnly = true;
            回答者名.Size = new Size(131, 20);
            回答者名.TabIndex = 15;
            回答者名.TabStop = false;
            // 
            // 回答者コード
            // 
            回答者コード.BackColor = Color.White;
            回答者コード.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            回答者コード.FormattingEnabled = true;
            回答者コード.ImeMode = ImeMode.Disable;
            回答者コード.Location = new Point(107, 15);
            回答者コード.Name = "回答者コード";
            回答者コード.Size = new Size(80, 21);
            回答者コード.TabIndex = 2;
            回答者コード.SelectedIndexChanged += 回答者コード_SelectedIndexChanged;
            回答者コード.Enter += 回答者コード_Enter;
            回答者コード.Leave += 回答者コード_Leave;
            回答者コード.Validating += 回答者コード_Validating;
            回答者コード.Validated += 回答者コード_Validated;
            // 
            // 状況
            // 
            状況.BackColor = SystemColors.Window;
            状況.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            状況.ImeMode = ImeMode.Hiragana;
            状況.Location = new Point(107, 38);
            状況.Margin = new Padding(3, 2, 3, 2);
            状況.Multiline = true;
            状況.Name = "状況";
            状況.Size = new Size(353, 41);
            状況.TabIndex = 4;
            状況.TextChanged += 状況_TextChanged;
            状況.DoubleClick += 状況_DoubleClick;
            状況.Enter += 状況_Enter;
            状況.Leave += 状況_Leave;
            状況.Validating += 状況_Validating;
            状況.Validated += 状況_Validated;
            // 
            // 状況_ラベル
            // 
            状況_ラベル.AllowDrop = true;
            状況_ラベル.AutoEllipsis = true;
            状況_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            状況_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            状況_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            状況_ラベル.Location = new Point(11, 38);
            状況_ラベル.Margin = new Padding(0);
            状況_ラベル.Name = "状況_ラベル";
            状況_ラベル.Size = new Size(95, 19);
            状況_ラベル.TabIndex = 3;
            状況_ラベル.Text = "状況";
            状況_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 600);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 19, 0);
            statusStrip1.Size = new Size(480, 22);
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
            // 文書コード
            // 
            文書コード.BackColor = Color.White;
            文書コード.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            文書コード.ImeMode = ImeMode.NoControl;
            文書コード.Location = new Point(12, 564);
            文書コード.Margin = new Padding(3, 2, 3, 2);
            文書コード.Name = "文書コード";
            文書コード.ReadOnly = true;
            文書コード.Size = new Size(13, 20);
            文書コード.TabIndex = 17;
            文書コード.TabStop = false;
            文書コード.Visible = false;
            // 
            // 版数
            // 
            版数.BackColor = Color.White;
            版数.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            版数.ImeMode = ImeMode.NoControl;
            版数.Location = new Point(31, 564);
            版数.Margin = new Padding(3, 2, 3, 2);
            版数.Name = "版数";
            版数.ReadOnly = true;
            版数.Size = new Size(13, 20);
            版数.TabIndex = 10197;
            版数.TabStop = false;
            版数.Visible = false;
            // 
            // F_是正予防処置回答_環境
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(480, 622);
            Controls.Add(版数);
            Controls.Add(文書コード);
            Controls.Add(statusStrip1);
            Controls.Add(groupBox1);
            Controls.Add(キャンセルボタン);
            Controls.Add(OKボタン);
            Name = "F_是正予防処置回答_環境";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "是正・予防処置回答（環境）";
            Load += Form_Load;
            KeyDown += F_是正予防処置回答_環境_KeyDown;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox 回答者コード;
        private Label label9;
        private Label 型番_ラベル;
        private TextBox 回答者名;
        private TextBox 品名;
        private Label 回答者_ラベル;
        private Label 影響範囲_ラベル;
        private TextBox 更新日終了;
        private GroupBox groupBox3;
        private RadioButton 確定指定Button3;
        private RadioButton 確定指定Button2;
        private RadioButton 確定指定Button1;
        private GroupBox groupBox4;
        private RadioButton 承認指定button3;
        private RadioButton 承認指定button2;
        private RadioButton 承認指定button1;
        private Button OKボタン;
        private Button キャンセルボタン;
        private Button 更新日開始選択ボタン;
        private Label 更新者名_ラベル;
        private TextBox 更新日開始;
        private GroupBox groupBox1;
        private Label 状況_ラベル;
        private TextBox 状況;
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
        private ComboBox 非含有証明書;
        private TextBox 原因;
        private Label 予防処置_ラベル;
        private Label 是正処置_ラベル;
        private Label 原因_ラベル;
        private TextBox 予防処置;
        private TextBox 是正処置;
        private TextBox 影響範囲;
        private Label label1;
        private TextBox 文書コード;
        private TextBox 版数;
        private Label label2;
        private TextBox 修正緩和処置;
    }
}
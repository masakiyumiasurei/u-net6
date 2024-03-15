namespace u_net
{
    partial class F_文書ズーム
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
            components = new System.ComponentModel.Container();
            対象 = new TextBox();
            対象_ラベル = new Label();
            キャンセルボタン = new Button();
            コモンダイアログ = new Button();
            印刷ボタン = new Button();
            テキスト = new TextBox();
            groupBox1 = new GroupBox();
            文字縮小ボタン = new Button();
            文字拡大ボタン = new Button();
            文字サイズ = new TextBox();
            OKボタン = new Button();
            toolTip1 = new ToolTip(components);
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // 対象
            // 
            対象.BackColor = Color.White;
            対象.Enabled = false;
            対象.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            対象.ImeMode = ImeMode.NoControl;
            対象.Location = new Point(58, 31);
            対象.Margin = new Padding(3, 2, 3, 2);
            対象.Name = "対象";
            対象.ReadOnly = true;
            対象.Size = new Size(204, 20);
            対象.TabIndex = 4;
            対象.TabStop = false;
            // 
            // 対象_ラベル
            // 
            対象_ラベル.AllowDrop = true;
            対象_ラベル.AutoEllipsis = true;
            対象_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            対象_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            対象_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            対象_ラベル.Location = new Point(13, 31);
            対象_ラベル.Margin = new Padding(0);
            対象_ラベル.Name = "対象_ラベル";
            対象_ラベル.Size = new Size(42, 19);
            対象_ラベル.TabIndex = 1;
            対象_ラベル.Text = "対象";
            対象_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // キャンセルボタン
            // 
            キャンセルボタン.Location = new Point(321, 8);
            キャンセルボタン.Name = "キャンセルボタン";
            キャンセルボタン.Size = new Size(102, 24);
            キャンセルボタン.TabIndex = 9;
            キャンセルボタン.Text = "キャンセル";
            キャンセルボタン.UseVisualStyleBackColor = true;
            キャンセルボタン.MouseClick += キャンセルボタン_MouseClick;
            // 
            // コモンダイアログ
            // 
            コモンダイアログ.Location = new Point(11, 8);
            コモンダイアログ.Name = "コモンダイアログ";
            コモンダイアログ.Size = new Size(24, 24);
            コモンダイアログ.TabIndex = 5;
            コモンダイアログ.TabStop = false;
            コモンダイアログ.UseVisualStyleBackColor = true;
            コモンダイアログ.Visible = false;
            // 
            // 印刷ボタン
            // 
            印刷ボタン.Location = new Point(104, 8);
            印刷ボタン.Name = "印刷ボタン";
            印刷ボタン.Size = new Size(102, 24);
            印刷ボタン.TabIndex = 3;
            印刷ボタン.Text = "印刷プレビュー";
            印刷ボタン.UseVisualStyleBackColor = true;
            印刷ボタン.Click += 印刷ボタン_Click;
            // 
            // テキスト
            // 
            テキスト.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            テキスト.BackColor = SystemColors.Window;
            テキスト.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            テキスト.ImeMode = ImeMode.Hiragana;
            テキスト.Location = new Point(13, 56);
            テキスト.Margin = new Padding(3, 2, 3, 2);
            テキスト.Multiline = true;
            テキスト.Name = "テキスト";
            テキスト.Size = new Size(384, 397);
            テキスト.TabIndex = 1;
            テキスト.TextChanged += テキスト_TextChanged;
            テキスト.Enter += テキスト_Enter;
            テキスト.KeyDown += テキスト_KeyDown;
            テキスト.Leave += テキスト_Leave;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(文字縮小ボタン);
            groupBox1.Controls.Add(文字拡大ボタン);
            groupBox1.Controls.Add(文字サイズ);
            groupBox1.Controls.Add(テキスト);
            groupBox1.Controls.Add(対象_ラベル);
            groupBox1.Controls.Add(対象);
            groupBox1.Location = new Point(11, 38);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(411, 463);
            groupBox1.TabIndex = 70;
            groupBox1.TabStop = false;
            // 
            // 文字縮小ボタン
            // 
            文字縮小ボタン.Location = new Point(373, 28);
            文字縮小ボタン.Name = "文字縮小ボタン";
            文字縮小ボタン.Size = new Size(23, 23);
            文字縮小ボタン.TabIndex = 7;
            文字縮小ボタン.Text = "－";
            toolTip1.SetToolTip(文字縮小ボタン, "文字の縮小");
            文字縮小ボタン.UseVisualStyleBackColor = true;
            文字縮小ボタン.Click += 文字縮小ボタン_Click;
            // 
            // 文字拡大ボタン
            // 
            文字拡大ボタン.Location = new Point(347, 28);
            文字拡大ボタン.Name = "文字拡大ボタン";
            文字拡大ボタン.Size = new Size(23, 23);
            文字拡大ボタン.TabIndex = 6;
            文字拡大ボタン.Text = "＋";
            toolTip1.SetToolTip(文字拡大ボタン, "文字の拡大");
            文字拡大ボタン.UseVisualStyleBackColor = true;
            文字拡大ボタン.Click += 文字拡大ボタン_Click;
            // 
            // 文字サイズ
            // 
            文字サイズ.BackColor = Color.White;
            文字サイズ.Enabled = false;
            文字サイズ.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            文字サイズ.ImeMode = ImeMode.Disable;
            文字サイズ.Location = new Point(307, 30);
            文字サイズ.Margin = new Padding(3, 2, 3, 2);
            文字サイズ.Name = "文字サイズ";
            文字サイズ.ReadOnly = true;
            文字サイズ.Size = new Size(34, 20);
            文字サイズ.TabIndex = 8;
            文字サイズ.Validated += 文字サイズ_Validated;
            // 
            // OKボタン
            // 
            OKボタン.Location = new Point(212, 8);
            OKボタン.Name = "OKボタン";
            OKボタン.Size = new Size(102, 24);
            OKボタン.TabIndex = 2;
            OKボタン.Text = "OK";
            OKボタン.UseVisualStyleBackColor = true;
            OKボタン.Click += OKボタン_Click;
            // 
            // F_文書ズーム
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(433, 510);
            Controls.Add(groupBox1);
            Controls.Add(印刷ボタン);
            Controls.Add(コモンダイアログ);
            Controls.Add(キャンセルボタン);
            Controls.Add(OKボタン);
            Name = "F_文書ズーム";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "文書ズーム";
            Load += Form_Load;
            KeyDown += Form_KeyDown;
            Resize += F_文書ズーム_Resize;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Label label9;
        private TextBox 対象;
        private Label 対象_ラベル;
        private Button キャンセルボタン;
        private Button コモンダイアログ;
        private Button 印刷ボタン;
        public TextBox テキスト;
        private GroupBox groupBox1;
        private Button 文字拡大ボタン;
        private TextBox 文字サイズ;
        private Button OKボタン;
        private Button 文字縮小ボタン;
        private ToolTip toolTip1;
    }
}
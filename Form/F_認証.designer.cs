
namespace u_net
{
    partial class F_認証
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_認証));
            ユーザー名 = new ComboBox();
            パスワード = new TextBox();
            パスワードラベル = new Label();
            ユーザー名ラベル = new Label();
            画面項目パネル = new Panel();
            パスワード強制変更ボタン = new Button();
            パスワード変更フレーム = new GroupBox();
            確認入力 = new TextBox();
            確認入力ラベル = new Label();
            新しいパスワードラベル = new Label();
            新パスワード = new TextBox();
            パスワード変更コメントラベル = new Label();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            OKボタン = new Button();
            キャンセルボタン = new Button();
            画面項目パネル.SuspendLayout();
            パスワード変更フレーム.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // ユーザー名
            // 
            ユーザー名.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ユーザー名.AutoCompleteSource = AutoCompleteSource.ListItems;
            ユーザー名.Font = new Font("BIZ UDPゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            ユーザー名.FormattingEnabled = true;
            ユーザー名.ImeMode = ImeMode.NoControl;
            ユーザー名.Location = new Point(140, 8);
            ユーザー名.Margin = new Padding(4, 3, 4, 3);
            ユーザー名.Name = "ユーザー名";
            ユーザー名.Size = new Size(185, 21);
            ユーザー名.TabIndex = 0;
            ユーザー名.SelectedIndexChanged += ユーザー名_SelectedIndexChanged;
            ユーザー名.Enter += ユーザー名_Enter;
            ユーザー名.Leave += ユーザー名_Leave;
            ユーザー名.Validating += ユーザー名_Validating;
            // 
            // パスワード
            // 
            パスワード.BackColor = Color.White;
            パスワード.Font = new Font("BIZ UDゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            パスワード.ImeMode = ImeMode.Off;
            パスワード.Location = new Point(140, 35);
            パスワード.Margin = new Padding(3, 2, 3, 2);
            パスワード.Multiline = true;
            パスワード.Name = "パスワード";
            パスワード.PasswordChar = '*';
            パスワード.Size = new Size(185, 20);
            パスワード.TabIndex = 1;
            パスワード.TextChanged += パスワード_TextChanged;
            パスワード.Enter += パスワード_Enter;
            パスワード.Leave += パスワード_Leave;
            // 
            // パスワードラベル
            // 
            パスワードラベル.AllowDrop = true;
            パスワードラベル.Font = new Font("BIZ UDゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            パスワードラベル.ForeColor = SystemColors.ActiveCaptionText;
            パスワードラベル.ImageAlign = ContentAlignment.MiddleLeft;
            パスワードラベル.Location = new Point(10, 37);
            パスワードラベル.Margin = new Padding(0);
            パスワードラベル.Name = "パスワードラベル";
            パスワードラベル.Size = new Size(100, 17);
            パスワードラベル.TabIndex = 10002;
            パスワードラベル.Text = "パスワード(&P)";
            パスワードラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ユーザー名ラベル
            // 
            ユーザー名ラベル.AllowDrop = true;
            ユーザー名ラベル.Font = new Font("BIZ UDゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            ユーザー名ラベル.ForeColor = SystemColors.ActiveCaptionText;
            ユーザー名ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            ユーザー名ラベル.Location = new Point(10, 10);
            ユーザー名ラベル.Margin = new Padding(0);
            ユーザー名ラベル.Name = "ユーザー名ラベル";
            ユーザー名ラベル.Size = new Size(100, 17);
            ユーザー名ラベル.TabIndex = 10001;
            ユーザー名ラベル.Text = "ユーザー名(&U)";
            ユーザー名ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 画面項目パネル
            // 
            画面項目パネル.BackColor = SystemColors.ButtonFace;
            画面項目パネル.BorderStyle = BorderStyle.Fixed3D;
            画面項目パネル.Controls.Add(パスワード強制変更ボタン);
            画面項目パネル.Controls.Add(パスワード変更フレーム);
            画面項目パネル.Controls.Add(ユーザー名);
            画面項目パネル.Controls.Add(ユーザー名ラベル);
            画面項目パネル.Controls.Add(パスワード);
            画面項目パネル.Controls.Add(パスワードラベル);
            画面項目パネル.Location = new Point(30, 10);
            画面項目パネル.Name = "画面項目パネル";
            画面項目パネル.Size = new Size(417, 189);
            画面項目パネル.TabIndex = 0;
            // 
            // パスワード強制変更ボタン
            // 
            パスワード強制変更ボタン.BackColor = SystemColors.ButtonFace;
            パスワード強制変更ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            パスワード強制変更ボタン.Image = (Image)resources.GetObject("パスワード強制変更ボタン.Image");
            パスワード強制変更ボタン.Location = new Point(330, 35);
            パスワード強制変更ボタン.Margin = new Padding(3, 2, 3, 2);
            パスワード強制変更ボタン.Name = "パスワード強制変更ボタン";
            パスワード強制変更ボタン.Size = new Size(20, 20);
            パスワード強制変更ボタン.TabIndex = 7;
            パスワード強制変更ボタン.UseVisualStyleBackColor = false;
            パスワード強制変更ボタン.Click += パスワード強制変更ボタン_Click;
            // 
            // パスワード変更フレーム
            // 
            パスワード変更フレーム.Controls.Add(確認入力);
            パスワード変更フレーム.Controls.Add(確認入力ラベル);
            パスワード変更フレーム.Controls.Add(新しいパスワードラベル);
            パスワード変更フレーム.Controls.Add(新パスワード);
            パスワード変更フレーム.Controls.Add(パスワード変更コメントラベル);
            パスワード変更フレーム.Font = new Font("BIZ UDPゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            パスワード変更フレーム.Location = new Point(10, 73);
            パスワード変更フレーム.Name = "パスワード変更フレーム";
            パスワード変更フレーム.Size = new Size(370, 100);
            パスワード変更フレーム.TabIndex = 6;
            パスワード変更フレーム.TabStop = false;
            パスワード変更フレーム.Text = "パスワードの変更(&A)";
            // 
            // 確認入力
            // 
            確認入力.BackColor = Color.White;
            確認入力.Font = new Font("BIZ UDゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            確認入力.ImeMode = ImeMode.Off;
            確認入力.Location = new Point(170, 68);
            確認入力.Margin = new Padding(3, 2, 3, 2);
            確認入力.Multiline = true;
            確認入力.Name = "確認入力";
            確認入力.PasswordChar = '*';
            確認入力.Size = new Size(185, 20);
            確認入力.TabIndex = 3;
            確認入力.TextChanged += 確認入力_TextChanged;
            確認入力.Enter += 確認入力_Enter;
            確認入力.Leave += 確認入力_Leave;
            // 
            // 確認入力ラベル
            // 
            確認入力ラベル.AllowDrop = true;
            確認入力ラベル.Font = new Font("BIZ UDゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            確認入力ラベル.ForeColor = SystemColors.ActiveCaptionText;
            確認入力ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            確認入力ラベル.Location = new Point(20, 70);
            確認入力ラベル.Margin = new Padding(0);
            確認入力ラベル.Name = "確認入力ラベル";
            確認入力ラベル.Size = new Size(140, 17);
            確認入力ラベル.TabIndex = 10005;
            確認入力ラベル.Text = "確認入力(&C)";
            確認入力ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 新しいパスワードラベル
            // 
            新しいパスワードラベル.AllowDrop = true;
            新しいパスワードラベル.Font = new Font("BIZ UDゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            新しいパスワードラベル.ForeColor = SystemColors.ActiveCaptionText;
            新しいパスワードラベル.ImageAlign = ContentAlignment.MiddleLeft;
            新しいパスワードラベル.Location = new Point(20, 45);
            新しいパスワードラベル.Margin = new Padding(0);
            新しいパスワードラベル.Name = "新しいパスワードラベル";
            新しいパスワードラベル.Size = new Size(140, 17);
            新しいパスワードラベル.TabIndex = 10004;
            新しいパスワードラベル.Text = "新しいパスワード(&N)";
            新しいパスワードラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 新パスワード
            // 
            新パスワード.BackColor = Color.White;
            新パスワード.Font = new Font("BIZ UDゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            新パスワード.ImeMode = ImeMode.Off;
            新パスワード.Location = new Point(170, 43);
            新パスワード.Margin = new Padding(3, 2, 3, 2);
            新パスワード.Multiline = true;
            新パスワード.Name = "新パスワード";
            新パスワード.PasswordChar = '*';
            新パスワード.Size = new Size(185, 20);
            新パスワード.TabIndex = 2;
            新パスワード.TextChanged += 新パスワード_TextChanged;
            新パスワード.Enter += 新パスワード_Enter;
            新パスワード.Leave += 新パスワード_Leave;
            新パスワード.Validating += 新パスワード_Validating;
            // 
            // パスワード変更コメントラベル
            // 
            パスワード変更コメントラベル.AllowDrop = true;
            パスワード変更コメントラベル.Font = new Font("BIZ UDゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            パスワード変更コメントラベル.ForeColor = SystemColors.ActiveCaptionText;
            パスワード変更コメントラベル.ImageAlign = ContentAlignment.MiddleLeft;
            パスワード変更コメントラベル.Location = new Point(20, 20);
            パスワード変更コメントラベル.Margin = new Padding(0);
            パスワード変更コメントラベル.Name = "パスワード変更コメントラベル";
            パスワード変更コメントラベル.Size = new Size(330, 17);
            パスワード変更コメントラベル.TabIndex = 11002;
            パスワード変更コメントラベル.Text = "パスワードを変更したい場合は入力してください。";
            パスワード変更コメントラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = SystemColors.ButtonFace;
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, toolStripStatusLabel2 });
            statusStrip1.Location = new Point(0, 237);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 19, 0);
            statusStrip1.Size = new Size(484, 22);
            statusStrip1.TabIndex = 12000;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Font = new Font("Yu Gothic UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(0, 17);
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.Font = new Font("Yu Gothic UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new Size(82, 17);
            toolStripStatusLabel2.Text = "各種項目の説明";
            // 
            // OKボタン
            // 
            OKボタン.BackColor = SystemColors.ButtonFace;
            OKボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            OKボタン.Location = new Point(293, 214);
            OKボタン.Margin = new Padding(3, 2, 3, 2);
            OKボタン.Name = "OKボタン";
            OKボタン.Size = new Size(70, 20);
            OKボタン.TabIndex = 4;
            OKボタン.Text = "OK";
            OKボタン.UseVisualStyleBackColor = false;
            OKボタン.Click += OKボタン_Click;
            // 
            // キャンセルボタン
            // 
            キャンセルボタン.BackColor = SystemColors.ButtonFace;
            キャンセルボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            キャンセルボタン.Location = new Point(374, 214);
            キャンセルボタン.Margin = new Padding(3, 2, 3, 2);
            キャンセルボタン.Name = "キャンセルボタン";
            キャンセルボタン.Size = new Size(70, 20);
            キャンセルボタン.TabIndex = 5;
            キャンセルボタン.Text = "キャンセル";
            キャンセルボタン.UseVisualStyleBackColor = false;
            キャンセルボタン.Click += キャンセルボタン_Click;
            // 
            // F_認証
            // 
            AutoScaleDimensions = new SizeF(8F, 12F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(484, 259);
            Controls.Add(キャンセルボタン);
            Controls.Add(OKボタン);
            Controls.Add(statusStrip1);
            Controls.Add(画面項目パネル);
            Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Margin = new Padding(3, 2, 3, 2);
            Name = "F_認証";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "認証";
            FormClosing += Form_FormClosing;
            Load += Form_Load;
            KeyDown += Form_KeyDown;
            画面項目パネル.ResumeLayout(false);
            画面項目パネル.PerformLayout();
            パスワード変更フレーム.ResumeLayout(false);
            パスワード変更フレーム.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox ユーザー名;
        private TextBox パスワード;
        private Label パスワードラベル;
        private Label ユーザー名ラベル;
        private Panel 画面項目パネル;
        private GroupBox パスワード変更フレーム;
        private Label パスワード変更コメントラベル;
        private Label 確認入力ラベル;
        private Label 新しいパスワードラベル;
        private TextBox 新パスワード;
        private TextBox 確認入力;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private Button OKボタン;
        private Button キャンセルボタン;
        private Button パスワード強制変更ボタン;
    }
}


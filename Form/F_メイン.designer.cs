
namespace u_net
{
    partial class F_メイン
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_メイン));
            コマンド終了 = new Button();
            panel1 = new Panel();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            tabControl1 = new TabControl();
            販売 = new TabPage();
            label23 = new Label();
            ActiveDate = new TextBox();
            無効日時 = new TextBox();
            無効者名 = new TextBox();
            無効者コード = new TextBox();
            更新者名 = new TextBox();
            更新者コード = new TextBox();
            更新日時 = new TextBox();
            作成者名 = new TextBox();
            作成者コード = new TextBox();
            支払 = new TabPage();
            label33 = new Label();
            ログインユーザー名 = new TextBox();
            ログインボタン = new Button();
            日付 = new TextBox();
            statusStrip1.SuspendLayout();
            tabControl1.SuspendLayout();
            販売.SuspendLayout();
            支払.SuspendLayout();
            SuspendLayout();
            // 
            // コマンド終了
            // 
            コマンド終了.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド終了.ForeColor = Color.Red;
            コマンド終了.Location = new Point(795, 5);
            コマンド終了.Margin = new Padding(3, 2, 3, 2);
            コマンド終了.Name = "コマンド終了";
            コマンド終了.Size = new Size(70, 20);
            コマンド終了.TabIndex = 1021;
            コマンド終了.TabStop = false;
            コマンド終了.Text = "終了";
            コマンド終了.UseVisualStyleBackColor = true;
            コマンド終了.Click += コマンド終了_Click;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaption;
            panel1.Location = new Point(0, 1);
            panel1.Margin = new Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(876, 42);
            panel1.TabIndex = 81;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, toolStripStatusLabel2 });
            statusStrip1.Location = new Point(0, 646);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 19, 0);
            statusStrip1.Size = new Size(876, 25);
            statusStrip1.TabIndex = 10001;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(0, 20);
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new Size(111, 20);
            toolStripStatusLabel2.Text = "各種項目の説明";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(販売);
            tabControl1.Controls.Add(支払);
            tabControl1.Location = new Point(0, 49);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(866, 526);
            tabControl1.TabIndex = 10002;
            // 
            // 販売
            // 
            販売.BackColor = SystemColors.Control;
            販売.Controls.Add(label23);
            販売.Controls.Add(ActiveDate);
            販売.Controls.Add(無効日時);
            販売.Controls.Add(無効者名);
            販売.Controls.Add(無効者コード);
            販売.Controls.Add(更新者名);
            販売.Controls.Add(更新者コード);
            販売.Controls.Add(更新日時);
            販売.Controls.Add(作成者名);
            販売.Controls.Add(作成者コード);
            販売.Location = new Point(4, 29);
            販売.Name = "販売";
            販売.Padding = new Padding(3);
            販売.Size = new Size(858, 493);
            販売.TabIndex = 0;
            販売.Text = "販売";
            // 
            // label23
            // 
            label23.AllowDrop = true;
            label23.AutoEllipsis = true;
            label23.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label23.ForeColor = SystemColors.ActiveCaptionText;
            label23.ImageAlign = ContentAlignment.MiddleLeft;
            label23.Location = new Point(538, 195);
            label23.Margin = new Padding(0);
            label23.Name = "label23";
            label23.Size = new Size(276, 21);
            label23.TabIndex = 10006;
            label23.Text = "※項目名が赤色の欄は入力必須欄です";
            label23.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ActiveDate
            // 
            ActiveDate.BackColor = SystemColors.Control;
            ActiveDate.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            ActiveDate.Location = new Point(626, 163);
            ActiveDate.Margin = new Padding(3, 2, 3, 2);
            ActiveDate.Multiline = true;
            ActiveDate.Name = "ActiveDate";
            ActiveDate.ReadOnly = true;
            ActiveDate.Size = new Size(188, 20);
            ActiveDate.TabIndex = 196;
            // 
            // 無効日時
            // 
            無効日時.BackColor = SystemColors.Control;
            無効日時.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            無効日時.Location = new Point(626, 113);
            無効日時.Margin = new Padding(3, 2, 3, 2);
            無効日時.Multiline = true;
            無効日時.Name = "無効日時";
            無効日時.ReadOnly = true;
            無効日時.Size = new Size(188, 20);
            無効日時.TabIndex = 194;
            // 
            // 無効者名
            // 
            無効者名.BackColor = SystemColors.Control;
            無効者名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            無効者名.Location = new Point(672, 138);
            無効者名.Margin = new Padding(3, 2, 3, 2);
            無効者名.Multiline = true;
            無効者名.Name = "無効者名";
            無効者名.ReadOnly = true;
            無効者名.Size = new Size(142, 20);
            無効者名.TabIndex = 192;
            // 
            // 無効者コード
            // 
            無効者コード.BackColor = SystemColors.Control;
            無効者コード.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            無効者コード.Location = new Point(626, 138);
            無効者コード.Margin = new Padding(3, 2, 3, 2);
            無効者コード.Multiline = true;
            無効者コード.Name = "無効者コード";
            無効者コード.ReadOnly = true;
            無効者コード.Size = new Size(47, 20);
            無効者コード.TabIndex = 191;
            // 
            // 更新者名
            // 
            更新者名.BackColor = SystemColors.Control;
            更新者名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            更新者名.Location = new Point(672, 90);
            更新者名.Margin = new Padding(3, 2, 3, 2);
            更新者名.Multiline = true;
            更新者名.Name = "更新者名";
            更新者名.ReadOnly = true;
            更新者名.Size = new Size(142, 20);
            更新者名.TabIndex = 186;
            // 
            // 更新者コード
            // 
            更新者コード.BackColor = SystemColors.Control;
            更新者コード.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            更新者コード.Location = new Point(626, 90);
            更新者コード.Margin = new Padding(3, 2, 3, 2);
            更新者コード.Multiline = true;
            更新者コード.Name = "更新者コード";
            更新者コード.ReadOnly = true;
            更新者コード.Size = new Size(47, 20);
            更新者コード.TabIndex = 185;
            // 
            // 更新日時
            // 
            更新日時.BackColor = SystemColors.Control;
            更新日時.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            更新日時.Location = new Point(626, 67);
            更新日時.Margin = new Padding(3, 2, 3, 2);
            更新日時.Multiline = true;
            更新日時.Name = "更新日時";
            更新日時.ReadOnly = true;
            更新日時.Size = new Size(188, 20);
            更新日時.TabIndex = 184;
            // 
            // 作成者名
            // 
            作成者名.BackColor = SystemColors.Control;
            作成者名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            作成者名.Location = new Point(672, 41);
            作成者名.Margin = new Padding(3, 2, 3, 2);
            作成者名.Multiline = true;
            作成者名.Name = "作成者名";
            作成者名.ReadOnly = true;
            作成者名.Size = new Size(142, 20);
            作成者名.TabIndex = 183;
            // 
            // 作成者コード
            // 
            作成者コード.BackColor = SystemColors.Control;
            作成者コード.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            作成者コード.Location = new Point(626, 41);
            作成者コード.Margin = new Padding(3, 2, 3, 2);
            作成者コード.Multiline = true;
            作成者コード.Name = "作成者コード";
            作成者コード.ReadOnly = true;
            作成者コード.Size = new Size(47, 20);
            作成者コード.TabIndex = 182;
            // 
            // 支払
            // 
            支払.BackColor = SystemColors.Control;
            支払.Controls.Add(label33);
            支払.Location = new Point(4, 29);
            支払.Name = "支払";
            支払.Padding = new Padding(3);
            支払.Size = new Size(858, 493);
            支払.TabIndex = 1;
            支払.Text = "支払";
            // 
            // label33
            // 
            label33.AllowDrop = true;
            label33.AutoEllipsis = true;
            label33.Font = new Font("BIZ UDPゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label33.ForeColor = SystemColors.ActiveCaptionText;
            label33.ImageAlign = ContentAlignment.MiddleLeft;
            label33.Location = new Point(29, 594);
            label33.Margin = new Padding(0);
            label33.Name = "label33";
            label33.Size = new Size(97, 23);
            label33.TabIndex = 10026;
            label33.Text = "相殺有無";
            label33.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ログインユーザー名
            // 
            ログインユーザー名.BackColor = Color.Black;
            ログインユーザー名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            ログインユーザー名.ForeColor = Color.White;
            ログインユーザー名.Location = new Point(310, 576);
            ログインユーザー名.Margin = new Padding(3, 2, 3, 2);
            ログインユーザー名.Multiline = true;
            ログインユーザー名.Name = "ログインユーザー名";
            ログインユーザー名.ReadOnly = true;
            ログインユーザー名.Size = new Size(188, 23);
            ログインユーザー名.TabIndex = 181;
            // 
            // ログインボタン
            // 
            ログインボタン.Font = new Font("Yu Gothic UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            ログインボタン.ForeColor = Color.Black;
            ログインボタン.Location = new Point(520, 577);
            ログインボタン.Name = "ログインボタン";
            ログインボタン.Size = new Size(142, 45);
            ログインボタン.TabIndex = 10003;
            ログインボタン.Text = "ログアウト";
            ログインボタン.UseVisualStyleBackColor = true;
            // 
            // 日付
            // 
            日付.BackColor = Color.Black;
            日付.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            日付.ForeColor = Color.White;
            日付.Location = new Point(310, 602);
            日付.Margin = new Padding(3, 2, 3, 2);
            日付.Multiline = true;
            日付.Name = "日付";
            日付.ReadOnly = true;
            日付.Size = new Size(188, 23);
            日付.TabIndex = 10004;
            // 
            // F_メイン
            // 
            BackColor = SystemColors.Control;
            ClientSize = new Size(876, 671);
            Controls.Add(日付);
            Controls.Add(ログインボタン);
            Controls.Add(tabControl1);
            Controls.Add(statusStrip1);
            Controls.Add(コマンド終了);
            Controls.Add(panel1);
            Controls.Add(ログインユーザー名);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Name = "F_メイン";
            Text = " メイン";
            FormClosing += Form_Unload;
            Load += Form_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            tabControl1.ResumeLayout(false);
            販売.ResumeLayout(false);
            販売.PerformLayout();
            支払.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CalendarColumn calendarColumn1;
        private CalendarColumn calendarColumn2;
        private CalendarColumn calendarColumn3;
        private System.Windows.Forms.Button コマンド終了;


        private CalendarColumn 登録日;
        private System.Windows.Forms.Panel panel1;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private TabControl tabControl1;
        private TabPage 支払;
        private Label label33;
        private TextBox Contact3PhoneNumber;
        private TextBox Contact2PhoneNumber;
        private TabPage 販売;
        private Label label23;
        private TextBox ActiveDate;
        private TextBox 無効日時;
        private TextBox 無効者名;
        private TextBox 無効者コード;
        private TextBox 更新者名;
        private TextBox 更新者コード;
        private TextBox 更新日時;
        private TextBox 作成者名;
        private TextBox 作成者コード;
        private TextBox ログインユーザー名;
        private Button ログインボタン;
        private TextBox 日付;
    }
}


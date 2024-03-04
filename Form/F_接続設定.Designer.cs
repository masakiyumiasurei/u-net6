namespace u_net
{
    partial class F_接続設定
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
            閉じる = new Button();
            label1 = new Label();
            DBName = new TextBox();
            label2 = new Label();
            ServerName = new TextBox();
            label3 = new Label();
            Login = new TextBox();
            パスワード = new Label();
            Password = new TextBox();
            接続文字列 = new TextBox();
            接続運用ボタン = new Button();
            セキュリティ設定ボタン = new Button();
            サウンドテストボタン = new Button();
            進捗状況テストボタン = new Button();
            接続設定ボタン = new Button();
            接続テストボタン = new Button();
            接続テスト2ボタン = new Button();
            label18 = new Label();
            バージョンアップボタン = new Button();
            管理者用バージョンアップボタン = new Button();
            サーバー日時更新ボタン = new Button();
            toolTip1 = new ToolTip(components);
            test = new TextBox();
            SuspendLayout();
            // 
            // 閉じる
            // 
            閉じる.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            閉じる.Location = new Point(573, 549);
            閉じる.Name = "閉じる";
            閉じる.Size = new Size(114, 23);
            閉じる.TabIndex = 211;
            閉じる.Text = "閉じる(&X)";
            閉じる.UseVisualStyleBackColor = true;
            閉じる.Click += 閉じる_Click;
            // 
            // label1
            // 
            label1.AllowDrop = true;
            label1.AutoEllipsis = true;
            label1.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ActiveCaptionText;
            label1.ImageAlign = ContentAlignment.MiddleLeft;
            label1.Location = new Point(18, 56);
            label1.Margin = new Padding(0);
            label1.Name = "label1";
            label1.Size = new Size(100, 17);
            label1.TabIndex = 213;
            label1.Text = "DB名";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // DBName
            // 
            DBName.BackColor = Color.White;
            DBName.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            DBName.Location = new Point(147, 55);
            DBName.Margin = new Padding(3, 2, 3, 2);
            DBName.Name = "DBName";
            DBName.ReadOnly = true;
            DBName.Size = new Size(275, 20);
            DBName.TabIndex = 1;
            // 
            // label2
            // 
            label2.AllowDrop = true;
            label2.AutoEllipsis = true;
            label2.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ActiveCaptionText;
            label2.ImageAlign = ContentAlignment.MiddleLeft;
            label2.Location = new Point(18, 27);
            label2.Margin = new Padding(0);
            label2.Name = "label2";
            label2.Size = new Size(100, 17);
            label2.TabIndex = 215;
            label2.Text = "サーバー名";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ServerName
            // 
            ServerName.BackColor = Color.White;
            ServerName.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            ServerName.Location = new Point(147, 26);
            ServerName.Margin = new Padding(3, 2, 3, 2);
            ServerName.Name = "ServerName";
            ServerName.ReadOnly = true;
            ServerName.Size = new Size(275, 20);
            ServerName.TabIndex = 214;
            // 
            // label3
            // 
            label3.AllowDrop = true;
            label3.AutoEllipsis = true;
            label3.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.ActiveCaptionText;
            label3.ImageAlign = ContentAlignment.MiddleLeft;
            label3.Location = new Point(18, 84);
            label3.Margin = new Padding(0);
            label3.Name = "label3";
            label3.Size = new Size(100, 17);
            label3.TabIndex = 217;
            label3.Text = "ログイン情報";
            label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Login
            // 
            Login.BackColor = Color.White;
            Login.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Login.Location = new Point(147, 83);
            Login.Margin = new Padding(3, 2, 3, 2);
            Login.Name = "Login";
            Login.ReadOnly = true;
            Login.Size = new Size(275, 20);
            Login.TabIndex = 216;
            // 
            // パスワード
            // 
            パスワード.AllowDrop = true;
            パスワード.AutoEllipsis = true;
            パスワード.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            パスワード.ForeColor = SystemColors.ActiveCaptionText;
            パスワード.ImageAlign = ContentAlignment.MiddleLeft;
            パスワード.Location = new Point(18, 111);
            パスワード.Margin = new Padding(0);
            パスワード.Name = "パスワード";
            パスワード.Size = new Size(115, 17);
            パスワード.TabIndex = 219;
            パスワード.Text = "現在のバージョン";
            パスワード.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Password
            // 
            Password.BackColor = Color.White;
            Password.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Password.Location = new Point(147, 110);
            Password.Margin = new Padding(3, 2, 3, 2);
            Password.Name = "Password";
            Password.ReadOnly = true;
            Password.Size = new Size(275, 20);
            Password.TabIndex = 218;
            // 
            // 接続文字列
            // 
            接続文字列.BackColor = Color.White;
            接続文字列.Enabled = false;
            接続文字列.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            接続文字列.Location = new Point(450, 26);
            接続文字列.Margin = new Padding(3, 2, 3, 2);
            接続文字列.Multiline = true;
            接続文字列.Name = "接続文字列";
            接続文字列.Size = new Size(237, 76);
            接続文字列.TabIndex = 240;
            接続文字列.Visible = false;
            // 
            // 接続運用ボタン
            // 
            接続運用ボタン.Location = new Point(18, 416);
            接続運用ボタン.Name = "接続運用ボタン";
            接続運用ボタン.Size = new Size(184, 33);
            接続運用ボタン.TabIndex = 244;
            接続運用ボタン.Text = "運用サーバーへ接続(&M)";
            接続運用ボタン.UseVisualStyleBackColor = true;
            接続運用ボタン.Click += 接続運用ボタン_Click;
            // 
            // セキュリティ設定ボタン
            // 
            セキュリティ設定ボタン.Location = new Point(208, 416);
            セキュリティ設定ボタン.Name = "セキュリティ設定ボタン";
            セキュリティ設定ボタン.Size = new Size(184, 33);
            セキュリティ設定ボタン.TabIndex = 245;
            セキュリティ設定ボタン.Text = "セキュリティ設定(&E)";
            セキュリティ設定ボタン.UseVisualStyleBackColor = true;
            // 
            // サウンドテストボタン
            // 
            サウンドテストボタン.Location = new Point(398, 416);
            サウンドテストボタン.Name = "サウンドテストボタン";
            サウンドテストボタン.Size = new Size(184, 33);
            サウンドテストボタン.TabIndex = 246;
            サウンドテストボタン.Text = "サウンドテスト(&S)";
            サウンドテストボタン.UseVisualStyleBackColor = true;
            サウンドテストボタン.Click += サウンドテストボタン_Click;
            // 
            // 進捗状況テストボタン
            // 
            進捗状況テストボタン.Location = new Point(398, 455);
            進捗状況テストボタン.Name = "進捗状況テストボタン";
            進捗状況テストボタン.Size = new Size(184, 33);
            進捗状況テストボタン.TabIndex = 249;
            進捗状況テストボタン.Text = "進捗状況テスト(&P)";
            進捗状況テストボタン.UseVisualStyleBackColor = true;
            進捗状況テストボタン.Click += 進捗状況テストボタン_Click;
            // 
            // 接続設定ボタン
            // 
            接続設定ボタン.Location = new Point(208, 455);
            接続設定ボタン.Name = "接続設定ボタン";
            接続設定ボタン.Size = new Size(184, 33);
            接続設定ボタン.TabIndex = 248;
            接続設定ボタン.Text = "接続設定(&T)";
            接続設定ボタン.UseVisualStyleBackColor = true;
            接続設定ボタン.Click += 接続設定ボタン_Click;
            // 
            // 接続テストボタン
            // 
            接続テストボタン.Location = new Point(18, 455);
            接続テストボタン.Name = "接続テストボタン";
            接続テストボタン.Size = new Size(184, 33);
            接続テストボタン.TabIndex = 247;
            接続テストボタン.Text = "テストサーバー接続(&T)";
            接続テストボタン.UseVisualStyleBackColor = true;
            接続テストボタン.Click += 接続テストボタン_Click;
            // 
            // 接続テスト2ボタン
            // 
            接続テスト2ボタン.Location = new Point(18, 494);
            接続テスト2ボタン.Name = "接続テスト2ボタン";
            接続テスト2ボタン.Size = new Size(184, 33);
            接続テスト2ボタン.TabIndex = 250;
            接続テスト2ボタン.Text = "テスト2サーバーへ接続";
            接続テスト2ボタン.UseVisualStyleBackColor = true;
            接続テスト2ボタン.Click += 接続テスト2ボタン_Click;
            // 
            // label18
            // 
            label18.AllowDrop = true;
            label18.AutoEllipsis = true;
            label18.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label18.ForeColor = SystemColors.ActiveCaptionText;
            label18.ImageAlign = ContentAlignment.MiddleLeft;
            label18.Location = new Point(208, 502);
            label18.Margin = new Padding(0);
            label18.Name = "label18";
            label18.Size = new Size(374, 17);
            label18.TabIndex = 251;
            label18.Text = "※テスト2サーバーへ接続した場合、接続先情報は無効です。";
            label18.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // バージョンアップボタン
            // 
            バージョンアップボタン.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            バージョンアップボタン.Location = new Point(392, 312);
            バージョンアップボタン.Name = "バージョンアップボタン";
            バージョンアップボタン.Size = new Size(190, 23);
            バージョンアップボタン.TabIndex = 252;
            バージョンアップボタン.Text = "バージョンアップ(&U)";
            toolTip1.SetToolTip(バージョンアップボタン, "クライアントバージョンアップ");
            バージョンアップボタン.UseVisualStyleBackColor = true;
            バージョンアップボタン.Click += バージョンアップボタン_Click;
            // 
            // 管理者用バージョンアップボタン
            // 
            管理者用バージョンアップボタン.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            管理者用バージョンアップボタン.Location = new Point(392, 338);
            管理者用バージョンアップボタン.Name = "管理者用バージョンアップボタン";
            管理者用バージョンアップボタン.Size = new Size(190, 23);
            管理者用バージョンアップボタン.TabIndex = 253;
            管理者用バージョンアップボタン.Text = "管理者用バージョンアップ(&A)";
            toolTip1.SetToolTip(管理者用バージョンアップボタン, "管理者専用バージョンアップ");
            管理者用バージョンアップボタン.UseVisualStyleBackColor = true;
            管理者用バージョンアップボタン.Click += 管理者用バージョンアップボタン_Click;
            // 
            // サーバー日時更新ボタン
            // 
            サーバー日時更新ボタン.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            サーバー日時更新ボタン.Location = new Point(392, 367);
            サーバー日時更新ボタン.Name = "サーバー日時更新ボタン";
            サーバー日時更新ボタン.Size = new Size(190, 23);
            サーバー日時更新ボタン.TabIndex = 254;
            サーバー日時更新ボタン.Text = "現在の日時を取得(&N)";
            toolTip1.SetToolTip(サーバー日時更新ボタン, "サーバー日時更新");
            サーバー日時更新ボタン.UseVisualStyleBackColor = true;
            サーバー日時更新ボタン.Click += サーバー日時更新ボタン_Click;
            // 
            // test
            // 
            test.BackColor = Color.White;
            test.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            test.Location = new Point(590, 463);
            test.Margin = new Padding(3, 2, 3, 2);
            test.Name = "test";
            test.ReadOnly = true;
            test.Size = new Size(106, 20);
            test.TabIndex = 255;
            // 
            // F_接続設定
            // 
            ClientSize = new Size(708, 586);
            Controls.Add(test);
            Controls.Add(サーバー日時更新ボタン);
            Controls.Add(管理者用バージョンアップボタン);
            Controls.Add(バージョンアップボタン);
            Controls.Add(label18);
            Controls.Add(接続テスト2ボタン);
            Controls.Add(進捗状況テストボタン);
            Controls.Add(接続設定ボタン);
            Controls.Add(接続テストボタン);
            Controls.Add(サウンドテストボタン);
            Controls.Add(セキュリティ設定ボタン);
            Controls.Add(接続運用ボタン);
            Controls.Add(接続文字列);
            Controls.Add(パスワード);
            Controls.Add(Password);
            Controls.Add(label3);
            Controls.Add(Login);
            Controls.Add(label2);
            Controls.Add(ServerName);
            Controls.Add(label1);
            Controls.Add(DBName);
            Controls.Add(閉じる);
            Name = "F_接続設定";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "接続設定";
            Load += Form_Load;
            KeyDown += Form_KeyDown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button 閉じる;
        private Label label1;
        private TextBox DBName;
        private Label label2;
        private TextBox ServerName;
        private Label label3;
        private TextBox Login;
        private Label パスワード;
        private TextBox Password;
        private TextBox 接続文字列;
        private Button 接続運用ボタン;
        private Button セキュリティ設定ボタン;
        private Button サウンドテストボタン;
        private Button 進捗状況テストボタン;
        private Button 接続設定ボタン;
        private Button 接続テストボタン;
        private Button 接続テスト2ボタン;
        private Label label18;
        private Button バージョンアップボタン;
        private Button 管理者用バージョンアップボタン;
        private Button サーバー日時更新ボタン;
        private ToolTip toolTip1;
        private TextBox test;
    }
}
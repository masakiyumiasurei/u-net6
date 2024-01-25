namespace u_net
{
    partial class F_出荷管理_抽出
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
            出荷コード開始 = new TextBox();
            出荷コード_ラベル = new Label();
            抽出ボタン = new Button();
            キャンセルボタン = new Button();
            出荷予定日開始選択ボタン = new Button();
            出荷予定日開始 = new TextBox();
            出荷予定日終了 = new TextBox();
            出荷予定日終了選択ボタン = new Button();
            label1 = new Label();
            シリアル番号指定 = new GroupBox();
            シリアル番号終了 = new TextBox();
            label7 = new Label();
            シリアル番号開始 = new TextBox();
            シリアル番号_ラベル = new Label();
            シリアル番号指定3 = new RadioButton();
            シリアル番号指定2 = new RadioButton();
            シリアル番号指定1 = new RadioButton();
            発送先名_ラベル = new Label();
            出荷コード終了 = new TextBox();
            出荷予定日_ラベル = new Label();
            label3 = new Label();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            label4 = new Label();
            受注コード終了 = new TextBox();
            受注コード開始 = new TextBox();
            label5 = new Label();
            型番 = new TextBox();
            型番_ラベル = new Label();
            型番詳細 = new TextBox();
            label6 = new Label();
            発送先名 = new TextBox();
            顧客コード_ラベル = new Label();
            顧客コード = new TextBox();
            顧客コード検索ボタン = new Button();
            顧客名_ラベル = new Label();
            顧客名 = new TextBox();
            作業終了指定 = new GroupBox();
            作業終了日終了選択ボタン = new Button();
            作業終了日開始選択ボタン = new Button();
            作業終了日終了 = new TextBox();
            label2 = new Label();
            作業終了日開始 = new TextBox();
            作業終了日_ラベル = new Label();
            作業終了指定3 = new RadioButton();
            作業終了指定2 = new RadioButton();
            作業終了指定1 = new RadioButton();
            シリアル番号指定.SuspendLayout();
            statusStrip1.SuspendLayout();
            作業終了指定.SuspendLayout();
            SuspendLayout();
            // 
            // 出荷コード開始
            // 
            出荷コード開始.BackColor = Color.White;
            出荷コード開始.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            出荷コード開始.ImeMode = ImeMode.Disable;
            出荷コード開始.Location = new Point(119, 17);
            出荷コード開始.Margin = new Padding(3, 2, 3, 2);
            出荷コード開始.Name = "出荷コード開始";
            出荷コード開始.Size = new Size(102, 20);
            出荷コード開始.TabIndex = 2;
            出荷コード開始.Leave += 出荷コード開始_Leave;
            // 
            // 出荷コード_ラベル
            // 
            出荷コード_ラベル.AllowDrop = true;
            出荷コード_ラベル.AutoEllipsis = true;
            出荷コード_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            出荷コード_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            出荷コード_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            出荷コード_ラベル.Location = new Point(17, 17);
            出荷コード_ラベル.Margin = new Padding(0);
            出荷コード_ラベル.Name = "出荷コード_ラベル";
            出荷コード_ラベル.Size = new Size(95, 19);
            出荷コード_ラベル.TabIndex = 1;
            出荷コード_ラベル.Text = "出荷コード(&C)";
            出荷コード_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 抽出ボタン
            // 
            抽出ボタン.Location = new Point(190, 440);
            抽出ボタン.Name = "抽出ボタン";
            抽出ボタン.Size = new Size(102, 24);
            抽出ボタン.TabIndex = 20;
            抽出ボタン.Text = "抽出(&F)";
            抽出ボタン.UseVisualStyleBackColor = true;
            抽出ボタン.Click += 抽出ボタン_Click;
            // 
            // キャンセルボタン
            // 
            キャンセルボタン.Location = new Point(299, 440);
            キャンセルボタン.Name = "キャンセルボタン";
            キャンセルボタン.Size = new Size(102, 24);
            キャンセルボタン.TabIndex = 21;
            キャンセルボタン.Text = "キャンセル(&X)";
            キャンセルボタン.UseVisualStyleBackColor = true;
            キャンセルボタン.MouseClick += キャンセルボタン_MouseClick;
            // 
            // 出荷予定日開始選択ボタン
            // 
            出荷予定日開始選択ボタン.Location = new Point(223, 41);
            出荷予定日開始選択ボタン.Margin = new Padding(4, 4, 4, 4);
            出荷予定日開始選択ボタン.Name = "出荷予定日開始選択ボタン";
            出荷予定日開始選択ボタン.Size = new Size(21, 21);
            出荷予定日開始選択ボタン.TabIndex = 21008;
            出荷予定日開始選択ボタン.TabStop = false;
            出荷予定日開始選択ボタン.Text = "▼";
            出荷予定日開始選択ボタン.UseVisualStyleBackColor = true;
            出荷予定日開始選択ボタン.Click += 出荷予定日開始選択ボタン_Click;
            // 
            // 出荷予定日開始
            // 
            出荷予定日開始.BackColor = Color.White;
            出荷予定日開始.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            出荷予定日開始.ImeMode = ImeMode.Disable;
            出荷予定日開始.Location = new Point(119, 42);
            出荷予定日開始.Margin = new Padding(3, 2, 3, 2);
            出荷予定日開始.Name = "出荷予定日開始";
            出荷予定日開始.Size = new Size(102, 20);
            出荷予定日開始.TabIndex = 5;
            出荷予定日開始.DoubleClick += 出荷予定日開始_DoubleClick;
            出荷予定日開始.Enter += 出荷予定日開始_Enter;
            出荷予定日開始.KeyPress += 出荷予定日開始_KeyPress;
            出荷予定日開始.Leave += 出荷予定日開始_Leave;
            // 
            // 出荷予定日終了
            // 
            出荷予定日終了.BackColor = Color.White;
            出荷予定日終了.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            出荷予定日終了.ImeMode = ImeMode.Disable;
            出荷予定日終了.Location = new Point(266, 42);
            出荷予定日終了.Margin = new Padding(3, 2, 3, 2);
            出荷予定日終了.Name = "出荷予定日終了";
            出荷予定日終了.Size = new Size(102, 20);
            出荷予定日終了.TabIndex = 6;
            出荷予定日終了.DoubleClick += 出荷予定日終了_DoubleClick;
            出荷予定日終了.Enter += 出荷予定日終了_Enter;
            出荷予定日終了.KeyPress += 出荷予定日終了_KeyPress;
            出荷予定日終了.Leave += 出荷予定日終了_Leave;
            // 
            // 出荷予定日終了選択ボタン
            // 
            出荷予定日終了選択ボタン.Location = new Point(370, 41);
            出荷予定日終了選択ボタン.Margin = new Padding(4, 4, 4, 4);
            出荷予定日終了選択ボタン.Name = "出荷予定日終了選択ボタン";
            出荷予定日終了選択ボタン.Size = new Size(21, 21);
            出荷予定日終了選択ボタン.TabIndex = 21016;
            出荷予定日終了選択ボタン.TabStop = false;
            出荷予定日終了選択ボタン.Text = "▼";
            出荷予定日終了選択ボタン.UseVisualStyleBackColor = true;
            出荷予定日終了選択ボタン.Click += 出荷予定日終了選択ボタン_Click;
            // 
            // label1
            // 
            label1.AllowDrop = true;
            label1.AutoEllipsis = true;
            label1.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ActiveCaptionText;
            label1.ImageAlign = ContentAlignment.MiddleLeft;
            label1.Location = new Point(221, 16);
            label1.Margin = new Padding(0);
            label1.Name = "label1";
            label1.Size = new Size(21, 21);
            label1.TabIndex = 21017;
            label1.Text = "～";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // シリアル番号指定
            // 
            シリアル番号指定.Controls.Add(シリアル番号終了);
            シリアル番号指定.Controls.Add(label7);
            シリアル番号指定.Controls.Add(シリアル番号開始);
            シリアル番号指定.Controls.Add(シリアル番号_ラベル);
            シリアル番号指定.Controls.Add(シリアル番号指定3);
            シリアル番号指定.Controls.Add(シリアル番号指定2);
            シリアル番号指定.Controls.Add(シリアル番号指定1);
            シリアル番号指定.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            シリアル番号指定.Location = new Point(17, 139);
            シリアル番号指定.Name = "シリアル番号指定";
            シリアル番号指定.Size = new Size(384, 106);
            シリアル番号指定.TabIndex = 14;
            シリアル番号指定.TabStop = false;
            シリアル番号指定.Text = "シリアル番号指定(&S)";
            シリアル番号指定.Validated += シリアル番号指定_Validated;
            // 
            // シリアル番号終了
            // 
            シリアル番号終了.BackColor = Color.White;
            シリアル番号終了.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            シリアル番号終了.ImeMode = ImeMode.Disable;
            シリアル番号終了.Location = new Point(260, 40);
            シリアル番号終了.Margin = new Padding(3, 2, 3, 2);
            シリアル番号終了.Name = "シリアル番号終了";
            シリアル番号終了.Size = new Size(102, 20);
            シリアル番号終了.TabIndex = 3;
            シリアル番号終了.Leave += シリアル番号終了_Leave;
            シリアル番号終了.Validated += シリアル番号終了_Validated;
            // 
            // label7
            // 
            label7.AllowDrop = true;
            label7.AutoEllipsis = true;
            label7.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label7.ForeColor = SystemColors.ActiveCaptionText;
            label7.ImageAlign = ContentAlignment.MiddleLeft;
            label7.Location = new Point(238, 39);
            label7.Margin = new Padding(0);
            label7.Name = "label7";
            label7.Size = new Size(21, 21);
            label7.TabIndex = 21024;
            label7.Text = "～";
            label7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // シリアル番号開始
            // 
            シリアル番号開始.BackColor = Color.White;
            シリアル番号開始.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            シリアル番号開始.ImeMode = ImeMode.Disable;
            シリアル番号開始.Location = new Point(135, 40);
            シリアル番号開始.Margin = new Padding(3, 2, 3, 2);
            シリアル番号開始.Name = "シリアル番号開始";
            シリアル番号開始.Size = new Size(102, 20);
            シリアル番号開始.TabIndex = 2;
            シリアル番号開始.Leave += シリアル番号開始_Leave;
            シリアル番号開始.Validated += シリアル番号開始_Validated;
            // 
            // シリアル番号_ラベル
            // 
            シリアル番号_ラベル.AllowDrop = true;
            シリアル番号_ラベル.AutoEllipsis = true;
            シリアル番号_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            シリアル番号_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            シリアル番号_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            シリアル番号_ラベル.Location = new Point(28, 40);
            シリアル番号_ラベル.Margin = new Padding(0);
            シリアル番号_ラベル.Name = "シリアル番号_ラベル";
            シリアル番号_ラベル.Size = new Size(95, 19);
            シリアル番号_ラベル.TabIndex = 21023;
            シリアル番号_ラベル.Text = "シリアル番号";
            シリアル番号_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // シリアル番号指定3
            // 
            シリアル番号指定3.AutoSize = true;
            シリアル番号指定3.Location = new Point(12, 82);
            シリアル番号指定3.Name = "シリアル番号指定3";
            シリアル番号指定3.Size = new Size(83, 16);
            シリアル番号指定3.TabIndex = 4;
            シリアル番号指定3.TabStop = true;
            シリアル番号指定3.Text = "指定しない";
            シリアル番号指定3.UseVisualStyleBackColor = true;
            // 
            // シリアル番号指定2
            // 
            シリアル番号指定2.AutoSize = true;
            シリアル番号指定2.Location = new Point(12, 60);
            シリアル番号指定2.Name = "シリアル番号指定2";
            シリアル番号指定2.Size = new Size(119, 16);
            シリアル番号指定2.TabIndex = 4;
            シリアル番号指定2.TabStop = true;
            シリアル番号指定2.Text = "シリアル番号無し";
            シリアル番号指定2.UseVisualStyleBackColor = true;
            // 
            // シリアル番号指定1
            // 
            シリアル番号指定1.AutoSize = true;
            シリアル番号指定1.Location = new Point(12, 22);
            シリアル番号指定1.Name = "シリアル番号指定1";
            シリアル番号指定1.Size = new Size(119, 16);
            シリアル番号指定1.TabIndex = 1;
            シリアル番号指定1.TabStop = true;
            シリアル番号指定1.Text = "シリアル番号有り";
            シリアル番号指定1.UseVisualStyleBackColor = true;
            // 
            // 発送先名_ラベル
            // 
            発送先名_ラベル.AllowDrop = true;
            発送先名_ラベル.AutoEllipsis = true;
            発送先名_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            発送先名_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            発送先名_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            発送先名_ラベル.Location = new Point(17, 250);
            発送先名_ラベル.Margin = new Padding(0);
            発送先名_ラベル.Name = "発送先名_ラベル";
            発送先名_ラベル.Size = new Size(95, 21);
            発送先名_ラベル.TabIndex = 15;
            発送先名_ラベル.Text = "発送先名(&H)";
            発送先名_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 出荷コード終了
            // 
            出荷コード終了.BackColor = Color.White;
            出荷コード終了.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            出荷コード終了.ImeMode = ImeMode.Disable;
            出荷コード終了.Location = new Point(243, 17);
            出荷コード終了.Margin = new Padding(3, 2, 3, 2);
            出荷コード終了.Name = "出荷コード終了";
            出荷コード終了.Size = new Size(102, 20);
            出荷コード終了.TabIndex = 3;
            出荷コード終了.Leave += 出荷コード終了_Leave;
            // 
            // 出荷予定日_ラベル
            // 
            出荷予定日_ラベル.AllowDrop = true;
            出荷予定日_ラベル.AutoEllipsis = true;
            出荷予定日_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            出荷予定日_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            出荷予定日_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            出荷予定日_ラベル.Location = new Point(17, 43);
            出荷予定日_ラベル.Margin = new Padding(0);
            出荷予定日_ラベル.Name = "出荷予定日_ラベル";
            出荷予定日_ラベル.Size = new Size(95, 19);
            出荷予定日_ラベル.TabIndex = 4;
            出荷予定日_ラベル.Text = "出荷予定日(&D)";
            出荷予定日_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            label3.AllowDrop = true;
            label3.AutoEllipsis = true;
            label3.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.ActiveCaptionText;
            label3.ImageAlign = ContentAlignment.MiddleLeft;
            label3.Location = new Point(244, 42);
            label3.Margin = new Padding(0);
            label3.Name = "label3";
            label3.Size = new Size(21, 21);
            label3.TabIndex = 21018;
            label3.Text = "～";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 473);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 19, 0);
            statusStrip1.Size = new Size(410, 22);
            statusStrip1.TabIndex = 21019;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(89, 17);
            toolStripStatusLabel1.Text = "各種項目の説明";
            // 
            // label4
            // 
            label4.AllowDrop = true;
            label4.AutoEllipsis = true;
            label4.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label4.ForeColor = SystemColors.ActiveCaptionText;
            label4.ImageAlign = ContentAlignment.MiddleLeft;
            label4.Location = new Point(221, 65);
            label4.Margin = new Padding(0);
            label4.Name = "label4";
            label4.Size = new Size(21, 21);
            label4.TabIndex = 21022;
            label4.Text = "～";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // 受注コード終了
            // 
            受注コード終了.BackColor = Color.White;
            受注コード終了.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            受注コード終了.ImeMode = ImeMode.Disable;
            受注コード終了.Location = new Point(243, 66);
            受注コード終了.Margin = new Padding(3, 2, 3, 2);
            受注コード終了.Name = "受注コード終了";
            受注コード終了.Size = new Size(102, 20);
            受注コード終了.TabIndex = 9;
            受注コード終了.KeyDown += 受注コード終了_KeyDown;
            受注コード終了.Leave += 受注コード終了_Leave;
            // 
            // 受注コード開始
            // 
            受注コード開始.BackColor = Color.White;
            受注コード開始.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            受注コード開始.ImeMode = ImeMode.Disable;
            受注コード開始.Location = new Point(119, 66);
            受注コード開始.Margin = new Padding(3, 2, 3, 2);
            受注コード開始.Name = "受注コード開始";
            受注コード開始.Size = new Size(102, 20);
            受注コード開始.TabIndex = 8;
            受注コード開始.KeyDown += 受注コード開始_KeyDown;
            受注コード開始.Leave += 受注コード開始_Leave;
            // 
            // label5
            // 
            label5.AllowDrop = true;
            label5.AutoEllipsis = true;
            label5.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label5.ForeColor = SystemColors.ActiveCaptionText;
            label5.ImageAlign = ContentAlignment.MiddleLeft;
            label5.Location = new Point(17, 67);
            label5.Margin = new Padding(0);
            label5.Name = "label5";
            label5.Size = new Size(95, 19);
            label5.TabIndex = 7;
            label5.Text = "受注コード(&O)";
            label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 型番
            // 
            型番.BackColor = Color.White;
            型番.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            型番.ImeMode = ImeMode.Off;
            型番.Location = new Point(119, 90);
            型番.Margin = new Padding(3, 2, 3, 2);
            型番.Name = "型番";
            型番.Size = new Size(282, 20);
            型番.TabIndex = 11;
            型番.TextChanged += 型番_TextChanged;
            型番.Enter += 型番_Enter;
            型番.Leave += 型番_Leave;
            型番.Validating += 型番_Validating;
            // 
            // 型番_ラベル
            // 
            型番_ラベル.AllowDrop = true;
            型番_ラベル.AutoEllipsis = true;
            型番_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            型番_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            型番_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            型番_ラベル.Location = new Point(17, 91);
            型番_ラベル.Margin = new Padding(0);
            型番_ラベル.Name = "型番_ラベル";
            型番_ラベル.Size = new Size(95, 19);
            型番_ラベル.TabIndex = 10;
            型番_ラベル.Text = "型番(&M)";
            型番_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 型番詳細
            // 
            型番詳細.BackColor = Color.White;
            型番詳細.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            型番詳細.ImeMode = ImeMode.Off;
            型番詳細.Location = new Point(119, 114);
            型番詳細.Margin = new Padding(3, 2, 3, 2);
            型番詳細.Name = "型番詳細";
            型番詳細.Size = new Size(282, 20);
            型番詳細.TabIndex = 13;
            型番詳細.TextChanged += 型番詳細_TextChanged;
            型番詳細.Enter += 型番詳細_Enter;
            型番詳細.Leave += 型番詳細_Leave;
            // 
            // label6
            // 
            label6.AllowDrop = true;
            label6.AutoEllipsis = true;
            label6.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label6.ForeColor = SystemColors.ActiveCaptionText;
            label6.ImageAlign = ContentAlignment.MiddleLeft;
            label6.Location = new Point(17, 115);
            label6.Margin = new Padding(0);
            label6.Name = "label6";
            label6.Size = new Size(95, 19);
            label6.TabIndex = 12;
            label6.Text = "型番詳細(&T)";
            label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 発送先名
            // 
            発送先名.BackColor = Color.White;
            発送先名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            発送先名.ImeMode = ImeMode.Hiragana;
            発送先名.Location = new Point(119, 250);
            発送先名.Margin = new Padding(3, 2, 3, 2);
            発送先名.Name = "発送先名";
            発送先名.Size = new Size(282, 20);
            発送先名.TabIndex = 16;
            発送先名.Enter += 発送先名_Enter;
            発送先名.Leave += 発送先名_Leave;
            // 
            // 顧客コード_ラベル
            // 
            顧客コード_ラベル.AllowDrop = true;
            顧客コード_ラベル.AutoEllipsis = true;
            顧客コード_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            顧客コード_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            顧客コード_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            顧客コード_ラベル.Location = new Point(17, 274);
            顧客コード_ラベル.Margin = new Padding(0);
            顧客コード_ラベル.Name = "顧客コード_ラベル";
            顧客コード_ラベル.Size = new Size(95, 19);
            顧客コード_ラベル.TabIndex = 17;
            顧客コード_ラベル.Text = "顧客コード(&U)";
            顧客コード_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 顧客コード
            // 
            顧客コード.BackColor = Color.White;
            顧客コード.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            顧客コード.ImeMode = ImeMode.Disable;
            顧客コード.Location = new Point(119, 274);
            顧客コード.Margin = new Padding(3, 2, 3, 2);
            顧客コード.Name = "顧客コード";
            顧客コード.Size = new Size(102, 20);
            顧客コード.TabIndex = 18;
            顧客コード.TextChanged += 顧客コード_TextChanged;
            顧客コード.DoubleClick += 顧客コード_DoubleClick;
            顧客コード.Enter += 顧客コード_Enter;
            顧客コード.Leave += 顧客コード_Leave;
            顧客コード.Validating += 顧客コード_Validating;
            // 
            // 顧客コード検索ボタン
            // 
            顧客コード検索ボタン.Location = new Point(223, 273);
            顧客コード検索ボタン.Margin = new Padding(4, 4, 4, 4);
            顧客コード検索ボタン.Name = "顧客コード検索ボタン";
            顧客コード検索ボタン.Size = new Size(21, 21);
            顧客コード検索ボタン.TabIndex = 21025;
            顧客コード検索ボタン.TabStop = false;
            顧客コード検索ボタン.Text = "▼";
            顧客コード検索ボタン.UseVisualStyleBackColor = true;
            顧客コード検索ボタン.Click += 顧客コード検索ボタン_Click;
            // 
            // 顧客名_ラベル
            // 
            顧客名_ラベル.AllowDrop = true;
            顧客名_ラベル.AutoEllipsis = true;
            顧客名_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            顧客名_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            顧客名_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            顧客名_ラベル.Location = new Point(17, 300);
            顧客名_ラベル.Margin = new Padding(0);
            顧客名_ラベル.Name = "顧客名_ラベル";
            顧客名_ラベル.Size = new Size(95, 19);
            顧客名_ラベル.TabIndex = 19;
            顧客名_ラベル.Text = "顧客名";
            顧客名_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 顧客名
            // 
            顧客名.BackColor = Color.White;
            顧客名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            顧客名.ImeMode = ImeMode.NoControl;
            顧客名.Location = new Point(119, 299);
            顧客名.Margin = new Padding(3, 2, 3, 2);
            顧客名.Name = "顧客名";
            顧客名.ReadOnly = true;
            顧客名.Size = new Size(282, 20);
            顧客名.TabIndex = 21027;
            顧客名.TabStop = false;
            顧客名.Validated += 顧客名_Validated;
            // 
            // 作業終了指定
            // 
            作業終了指定.Controls.Add(作業終了日終了選択ボタン);
            作業終了指定.Controls.Add(作業終了日開始選択ボタン);
            作業終了指定.Controls.Add(作業終了日終了);
            作業終了指定.Controls.Add(label2);
            作業終了指定.Controls.Add(作業終了日開始);
            作業終了指定.Controls.Add(作業終了日_ラベル);
            作業終了指定.Controls.Add(作業終了指定3);
            作業終了指定.Controls.Add(作業終了指定2);
            作業終了指定.Controls.Add(作業終了指定1);
            作業終了指定.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            作業終了指定.Location = new Point(17, 328);
            作業終了指定.Name = "作業終了指定";
            作業終了指定.Size = new Size(384, 106);
            作業終了指定.TabIndex = 19;
            作業終了指定.TabStop = false;
            作業終了指定.Text = "作業終了指定(&E)";
            // 
            // 作業終了日終了選択ボタン
            // 
            作業終了日終了選択ボタン.Location = new Point(356, 39);
            作業終了日終了選択ボタン.Margin = new Padding(4, 4, 4, 4);
            作業終了日終了選択ボタン.Name = "作業終了日終了選択ボタン";
            作業終了日終了選択ボタン.Size = new Size(21, 21);
            作業終了日終了選択ボタン.TabIndex = 21028;
            作業終了日終了選択ボタン.TabStop = false;
            作業終了日終了選択ボタン.Text = "▼";
            作業終了日終了選択ボタン.UseVisualStyleBackColor = true;
            作業終了日終了選択ボタン.Click += 作業終了日終了選択ボタン_Click;
            // 
            // 作業終了日開始選択ボタン
            // 
            作業終了日開始選択ボタン.Location = new Point(211, 40);
            作業終了日開始選択ボタン.Margin = new Padding(4, 4, 4, 4);
            作業終了日開始選択ボタン.Name = "作業終了日開始選択ボタン";
            作業終了日開始選択ボタン.Size = new Size(21, 21);
            作業終了日開始選択ボタン.TabIndex = 21028;
            作業終了日開始選択ボタン.TabStop = false;
            作業終了日開始選択ボタン.Text = "▼";
            作業終了日開始選択ボタン.UseVisualStyleBackColor = true;
            作業終了日開始選択ボタン.Click += 作業終了日開始選択ボタン_Click;
            // 
            // 作業終了日終了
            // 
            作業終了日終了.BackColor = Color.White;
            作業終了日終了.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            作業終了日終了.ImeMode = ImeMode.Disable;
            作業終了日終了.Location = new Point(252, 40);
            作業終了日終了.Margin = new Padding(3, 2, 3, 2);
            作業終了日終了.Name = "作業終了日終了";
            作業終了日終了.Size = new Size(102, 20);
            作業終了日終了.TabIndex = 3;
            作業終了日終了.DoubleClick += 作業終了日終了_DoubleClick;
            作業終了日終了.Enter += 作業終了日終了_Enter;
            作業終了日終了.KeyPress += 作業終了日終了_KeyPress;
            作業終了日終了.Leave += 作業終了日終了_Leave;
            作業終了日終了.Validated += 作業終了日終了_Validated;
            // 
            // label2
            // 
            label2.AllowDrop = true;
            label2.AutoEllipsis = true;
            label2.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ActiveCaptionText;
            label2.ImageAlign = ContentAlignment.MiddleLeft;
            label2.Location = new Point(230, 39);
            label2.Margin = new Padding(0);
            label2.Name = "label2";
            label2.Size = new Size(21, 21);
            label2.TabIndex = 21024;
            label2.Text = "～";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // 作業終了日開始
            // 
            作業終了日開始.BackColor = Color.White;
            作業終了日開始.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            作業終了日開始.ImeMode = ImeMode.Disable;
            作業終了日開始.Location = new Point(107, 40);
            作業終了日開始.Margin = new Padding(3, 2, 3, 2);
            作業終了日開始.Name = "作業終了日開始";
            作業終了日開始.Size = new Size(102, 20);
            作業終了日開始.TabIndex = 2;
            作業終了日開始.DoubleClick += 作業終了日開始_DoubleClick;
            作業終了日開始.Enter += 作業終了日開始_Enter;
            作業終了日開始.KeyPress += 作業終了日開始_KeyPress;
            作業終了日開始.Leave += 作業終了日開始_Leave;
            作業終了日開始.Validated += 作業終了日開始_Validated;
            // 
            // 作業終了日_ラベル
            // 
            作業終了日_ラベル.AllowDrop = true;
            作業終了日_ラベル.AutoEllipsis = true;
            作業終了日_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            作業終了日_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            作業終了日_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            作業終了日_ラベル.Location = new Point(28, 40);
            作業終了日_ラベル.Margin = new Padding(0);
            作業終了日_ラベル.Name = "作業終了日_ラベル";
            作業終了日_ラベル.Size = new Size(82, 19);
            作業終了日_ラベル.TabIndex = 21023;
            作業終了日_ラベル.Text = "作業終了日";
            作業終了日_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 作業終了指定3
            // 
            作業終了指定3.AutoSize = true;
            作業終了指定3.Location = new Point(12, 82);
            作業終了指定3.Name = "作業終了指定3";
            作業終了指定3.Size = new Size(83, 16);
            作業終了指定3.TabIndex = 4;
            作業終了指定3.TabStop = true;
            作業終了指定3.Text = "指定しない";
            作業終了指定3.UseVisualStyleBackColor = true;
            // 
            // 作業終了指定2
            // 
            作業終了指定2.AutoSize = true;
            作業終了指定2.Location = new Point(12, 60);
            作業終了指定2.Name = "作業終了指定2";
            作業終了指定2.Size = new Size(143, 16);
            作業終了指定2.TabIndex = 4;
            作業終了指定2.TabStop = true;
            作業終了指定2.Text = "作業を終了していない";
            作業終了指定2.UseVisualStyleBackColor = true;
            // 
            // 作業終了指定1
            // 
            作業終了指定1.AutoSize = true;
            作業終了指定1.Location = new Point(12, 22);
            作業終了指定1.Name = "作業終了指定1";
            作業終了指定1.Size = new Size(131, 16);
            作業終了指定1.TabIndex = 1;
            作業終了指定1.TabStop = true;
            作業終了指定1.Text = "作業を終了している";
            作業終了指定1.UseVisualStyleBackColor = true;
            // 
            // F_出荷管理_抽出
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(410, 495);
            Controls.Add(作業終了指定);
            Controls.Add(顧客名);
            Controls.Add(顧客名_ラベル);
            Controls.Add(顧客コード検索ボタン);
            Controls.Add(顧客コード);
            Controls.Add(顧客コード_ラベル);
            Controls.Add(発送先名);
            Controls.Add(label6);
            Controls.Add(型番詳細);
            Controls.Add(型番_ラベル);
            Controls.Add(型番);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(受注コード終了);
            Controls.Add(受注コード開始);
            Controls.Add(statusStrip1);
            Controls.Add(label3);
            Controls.Add(出荷予定日_ラベル);
            Controls.Add(出荷コード終了);
            Controls.Add(発送先名_ラベル);
            Controls.Add(シリアル番号指定);
            Controls.Add(label1);
            Controls.Add(出荷予定日終了選択ボタン);
            Controls.Add(出荷予定日終了);
            Controls.Add(出荷予定日開始);
            Controls.Add(出荷予定日開始選択ボタン);
            Controls.Add(キャンセルボタン);
            Controls.Add(抽出ボタン);
            Controls.Add(出荷コード開始);
            Controls.Add(出荷コード_ラベル);
            KeyPreview = true;
            Name = "F_出荷管理_抽出";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "出荷管理 - 抽出";
            Load += Form_Load;
            KeyDown += F_出荷管理_抽出_KeyDown;
            シリアル番号指定.ResumeLayout(false);
            シリアル番号指定.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            作業終了指定.ResumeLayout(false);
            作業終了指定.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox 更新者名;
        private Label label9;
        private Label 型番_ラベル;
        private TextBox 型番;
        private TextBox 品名;
        private Label 出荷コード_ラベル;
        private TextBox 出荷予定日終了;
        private GroupBox groupBox3;
        private RadioButton 確定指定Button3;
        private RadioButton 確定指定Button2;
        private RadioButton 確定指定Button1;
        private GroupBox groupBox4;
        private RadioButton 承認指定button3;
        private RadioButton 承認指定button2;
        private RadioButton 承認指定button1;
        private GroupBox groupBox5;
        private RadioButton 削除指定Button3;
        private RadioButton 削除指定Button2;
        private RadioButton 削除指定Button1;
        private Button 抽出ボタン;
        private Button キャンセルボタン;
        private Button 出荷予定日開始選択ボタン;
        private Label 更新者名_ラベル;
        private TextBox 出荷予定日開始;
        private Button 出荷予定日終了選択ボタン;
        private Label label1;
        private GroupBox シリアル番号指定;
        private RadioButton シリアル番号指定3;
        private RadioButton シリアル番号指定2;
        private RadioButton シリアル番号指定1;
        private Label 発送先名_ラベル;
        private TextBox 出荷コード開始;
        private TextBox 出荷コード終了;
        private Label 出荷予定日_ラベル;
        private Label label3;
        private StatusStrip statusStrip1;
        internal ToolStripStatusLabel toolStripStatusLabel1;
        private Label label4;
        private TextBox 受注コード終了;
        private TextBox 受注コード開始;
        private Label label5;
        private TextBox 型番詳細;
        private Label label6;
        private TextBox シリアル番号開始;
        private Label シリアル番号_ラベル;
        private TextBox シリアル番号終了;
        private Label label7;
        private TextBox 発送先名;
        private Label 顧客コード_ラベル;
        private TextBox 顧客コード;
        private Button 顧客コード検索ボタン;
        private Label 顧客名_ラベル;
        private TextBox 顧客名;
        private GroupBox 作業終了指定;
        private TextBox 作業終了日終了;
        private Label label2;
        private TextBox 作業終了日開始;
        private Label 作業終了日_ラベル;
        private RadioButton 作業終了指定3;
        private RadioButton 作業終了指定2;
        private RadioButton 作業終了指定1;
        private Button 作業終了日終了選択ボタン;
        private Button 作業終了日開始選択ボタン;
    }
}
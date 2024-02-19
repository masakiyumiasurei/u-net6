namespace u_net
{
    partial class F_受注管理
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_受注管理));
            panel1 = new Panel();
            コマンド入力 = new Button();
            button1 = new Button();
            コマンド生産 = new Button();
            コマンド受注 = new Button();
            コマンド顧客 = new Button();
            コマンド出力 = new Button();
            コマンド更新 = new Button();
            コマンド全表示 = new Button();
            コマンド初期化 = new Button();
            コマンド検索 = new Button();
            コマンド抽出 = new Button();
            コマンド終了 = new Button();
            有効件数 = new TextBox();
            表示件数ラベル = new Label();
            label2 = new Label();
            dataGridView1 = new DataGridView();
            panel2 = new Panel();
            label1 = new Label();
            税込合計金額 = new TextBox();
            label3 = new Label();
            合計金額 = new TextBox();
            合計金額ラベル = new Label();
            合計数量 = new TextBox();
            合計数量ラベル = new Label();
            初期表示ボタン = new Button();
            本日受注分ボタン = new Button();
            前日受注分ボタン = new Button();
            前ページボタン = new Button();
            次ページボタン = new Button();
            履歴トグル = new CheckBox();
            panel4 = new Panel();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel2.SuspendLayout();
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaption;
            panel1.Controls.Add(コマンド入力);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(コマンド生産);
            panel1.Controls.Add(コマンド受注);
            panel1.Controls.Add(コマンド顧客);
            panel1.Controls.Add(コマンド出力);
            panel1.Controls.Add(コマンド更新);
            panel1.Controls.Add(コマンド全表示);
            panel1.Controls.Add(コマンド初期化);
            panel1.Controls.Add(コマンド検索);
            panel1.Controls.Add(コマンド抽出);
            panel1.Controls.Add(コマンド終了);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(4, 2, 4, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(1032, 32);
            panel1.TabIndex = 83;
            // 
            // コマンド入力
            // 
            コマンド入力.Enabled = false;
            コマンド入力.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド入力.ForeColor = Color.Blue;
            コマンド入力.ImageAlign = ContentAlignment.BottomLeft;
            コマンド入力.Location = new Point(578, 4);
            コマンド入力.Margin = new Padding(0, 2, 0, 2);
            コマンド入力.Name = "コマンド入力";
            コマンド入力.Size = new Size(70, 22);
            コマンド入力.TabIndex = 13;
            コマンド入力.TabStop = false;
            コマンド入力.Text = "入力";
            コマンド入力.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Enabled = false;
            button1.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            button1.ForeColor = Color.Blue;
            button1.ImageAlign = ContentAlignment.BottomLeft;
            button1.Location = new Point(498, 4);
            button1.Margin = new Padding(0, 2, 0, 2);
            button1.Name = "button1";
            button1.Size = new Size(70, 22);
            button1.TabIndex = 12;
            button1.TabStop = false;
            button1.UseVisualStyleBackColor = true;
            // 
            // コマンド生産
            // 
            コマンド生産.Enabled = false;
            コマンド生産.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド生産.ForeColor = Color.Blue;
            コマンド生産.ImageAlign = ContentAlignment.BottomLeft;
            コマンド生産.Location = new Point(429, 4);
            コマンド生産.Margin = new Padding(0, 2, 0, 2);
            コマンド生産.Name = "コマンド生産";
            コマンド生産.Size = new Size(70, 22);
            コマンド生産.TabIndex = 11;
            コマンド生産.TabStop = false;
            コマンド生産.Text = "生産";
            コマンド生産.UseVisualStyleBackColor = true;
            // 
            // コマンド受注
            // 
            コマンド受注.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド受注.ForeColor = Color.Blue;
            コマンド受注.ImageAlign = ContentAlignment.BottomLeft;
            コマンド受注.Location = new Point(291, 4);
            コマンド受注.Margin = new Padding(0, 2, 0, 2);
            コマンド受注.Name = "コマンド受注";
            コマンド受注.Size = new Size(70, 22);
            コマンド受注.TabIndex = 1;
            コマンド受注.TabStop = false;
            コマンド受注.Text = "受注";
            コマンド受注.UseVisualStyleBackColor = true;
            コマンド受注.Click += コマンド受注_Click;
            // 
            // コマンド顧客
            // 
            コマンド顧客.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド顧客.ForeColor = Color.Blue;
            コマンド顧客.ImageAlign = ContentAlignment.BottomLeft;
            コマンド顧客.Location = new Point(360, 4);
            コマンド顧客.Margin = new Padding(0, 2, 0, 2);
            コマンド顧客.Name = "コマンド顧客";
            コマンド顧客.Size = new Size(70, 22);
            コマンド顧客.TabIndex = 6;
            コマンド顧客.TabStop = false;
            コマンド顧客.Text = "顧客";
            コマンド顧客.UseVisualStyleBackColor = true;
            コマンド顧客.Click += コマンド顧客_Click;
            // 
            // コマンド出力
            // 
            コマンド出力.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド出力.ForeColor = Color.Blue;
            コマンド出力.ImageAlign = ContentAlignment.BottomLeft;
            コマンド出力.Location = new Point(647, 4);
            コマンド出力.Margin = new Padding(0, 2, 0, 2);
            コマンド出力.Name = "コマンド出力";
            コマンド出力.Size = new Size(70, 22);
            コマンド出力.TabIndex = 11;
            コマンド出力.TabStop = false;
            コマンド出力.Text = "出力";
            コマンド出力.UseVisualStyleBackColor = true;
            コマンド出力.Click += コマンド出力_Click;
            // 
            // コマンド更新
            // 
            コマンド更新.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド更新.ForeColor = Color.Blue;
            コマンド更新.ImageAlign = ContentAlignment.BottomLeft;
            コマンド更新.Location = new Point(715, 4);
            コマンド更新.Margin = new Padding(0, 2, 0, 2);
            コマンド更新.Name = "コマンド更新";
            コマンド更新.Size = new Size(70, 22);
            コマンド更新.TabIndex = 10;
            コマンド更新.TabStop = false;
            コマンド更新.Text = "更新";
            コマンド更新.UseVisualStyleBackColor = true;
            コマンド更新.Click += コマンド更新_Click;
            // 
            // コマンド全表示
            // 
            コマンド全表示.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド全表示.ForeColor = Color.Blue;
            コマンド全表示.ImageAlign = ContentAlignment.BottomLeft;
            コマンド全表示.Location = new Point(207, 4);
            コマンド全表示.Margin = new Padding(0, 2, 0, 2);
            コマンド全表示.Name = "コマンド全表示";
            コマンド全表示.Size = new Size(70, 22);
            コマンド全表示.TabIndex = 5;
            コマンド全表示.TabStop = false;
            コマンド全表示.Text = "全表示";
            コマンド全表示.UseVisualStyleBackColor = true;
            コマンド全表示.Click += コマンド全表示_Click;
            // 
            // コマンド初期化
            // 
            コマンド初期化.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド初期化.ForeColor = Color.Blue;
            コマンド初期化.ImageAlign = ContentAlignment.BottomLeft;
            コマンド初期化.Location = new Point(139, 4);
            コマンド初期化.Margin = new Padding(0, 2, 0, 2);
            コマンド初期化.Name = "コマンド初期化";
            コマンド初期化.Size = new Size(70, 22);
            コマンド初期化.TabIndex = 4;
            コマンド初期化.TabStop = false;
            コマンド初期化.Text = "初期化";
            コマンド初期化.UseVisualStyleBackColor = true;
            コマンド初期化.Click += コマンド初期化_Click;
            // 
            // コマンド検索
            // 
            コマンド検索.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド検索.ForeColor = Color.Blue;
            コマンド検索.ImageAlign = ContentAlignment.BottomLeft;
            コマンド検索.Location = new Point(71, 4);
            コマンド検索.Margin = new Padding(0, 2, 0, 2);
            コマンド検索.Name = "コマンド検索";
            コマンド検索.Size = new Size(70, 22);
            コマンド検索.TabIndex = 3;
            コマンド検索.TabStop = false;
            コマンド検索.Text = "検索";
            コマンド検索.UseVisualStyleBackColor = true;
            コマンド検索.Click += コマンド検索_Click;
            // 
            // コマンド抽出
            // 
            コマンド抽出.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド抽出.ForeColor = Color.Blue;
            コマンド抽出.ImageAlign = ContentAlignment.BottomLeft;
            コマンド抽出.Location = new Point(3, 4);
            コマンド抽出.Margin = new Padding(0, 2, 0, 2);
            コマンド抽出.Name = "コマンド抽出";
            コマンド抽出.Size = new Size(70, 22);
            コマンド抽出.TabIndex = 2;
            コマンド抽出.TabStop = false;
            コマンド抽出.Text = "抽出";
            コマンド抽出.UseVisualStyleBackColor = true;
            コマンド抽出.Click += コマンド抽出_Click;
            // 
            // コマンド終了
            // 
            コマンド終了.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド終了.ForeColor = Color.Red;
            コマンド終了.Location = new Point(783, 4);
            コマンド終了.Margin = new Padding(0, 2, 0, 2);
            コマンド終了.Name = "コマンド終了";
            コマンド終了.Size = new Size(70, 22);
            コマンド終了.TabIndex = 0;
            コマンド終了.TabStop = false;
            コマンド終了.Text = "終了";
            コマンド終了.UseVisualStyleBackColor = true;
            コマンド終了.Click += コマンド終了_Click;
            // 
            // 有効件数
            // 
            有効件数.Font = new Font("BIZ UDPゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            有効件数.Location = new Point(72, 7);
            有効件数.Margin = new Padding(3, 2, 3, 2);
            有効件数.Name = "有効件数";
            有効件数.Size = new Size(88, 20);
            有効件数.TabIndex = 0;
            // 
            // 表示件数ラベル
            // 
            表示件数ラベル.AutoSize = true;
            表示件数ラベル.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            表示件数ラベル.Location = new Point(6, 9);
            表示件数ラベル.Name = "表示件数ラベル";
            表示件数ラベル.Size = new Size(53, 12);
            表示件数ラベル.TabIndex = 85;
            表示件数ラベル.Text = "表示件数";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(164, 9);
            label2.Name = "label2";
            label2.Size = new Size(17, 12);
            label2.TabIndex = 86;
            label2.Text = "件";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 60);
            dataGridView1.Margin = new Padding(3, 2, 3, 2);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ShowCellErrors = false;
            dataGridView1.Size = new Size(1032, 419);
            dataGridView1.TabIndex = 87;
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
            dataGridView1.CellFormatting += dataGridView1_CellFormatting;
            dataGridView1.CellPainting += DataGridView1_CellPainting;
            dataGridView1.RowPostPaint += dataGridView1_RowPostPaint;
            dataGridView1.Sorted += dataGridView1_Sorted;
            dataGridView1.KeyDown += dataGridView1_KeyDown;
            // 
            // panel2
            // 
            panel2.Controls.Add(label1);
            panel2.Controls.Add(税込合計金額);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(合計金額);
            panel2.Controls.Add(合計金額ラベル);
            panel2.Controls.Add(合計数量);
            panel2.Controls.Add(合計数量ラベル);
            panel2.Controls.Add(有効件数);
            panel2.Controls.Add(表示件数ラベル);
            panel2.Controls.Add(label2);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 479);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(1032, 34);
            panel2.TabIndex = 88;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(776, 11);
            label1.Name = "label1";
            label1.Size = new Size(236, 12);
            label1.TabIndex = 93;
            label1.Text = "※合計金額に未承認データは含まれません。";
            // 
            // 税込合計金額
            // 
            税込合計金額.Font = new Font("BIZ UDPゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            税込合計金額.Location = new Point(637, 7);
            税込合計金額.Margin = new Padding(3, 2, 3, 2);
            税込合計金額.Name = "税込合計金額";
            税込合計金額.Size = new Size(133, 20);
            税込合計金額.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(552, 9);
            label3.Name = "label3";
            label3.Size = new Size(77, 12);
            label3.TabIndex = 91;
            label3.Text = "税込合計金額";
            // 
            // 合計金額
            // 
            合計金額.Font = new Font("BIZ UDPゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            合計金額.Location = new Point(436, 7);
            合計金額.Margin = new Padding(3, 2, 3, 2);
            合計金額.Name = "合計金額";
            合計金額.Size = new Size(88, 20);
            合計金額.TabIndex = 2;
            // 
            // 合計金額ラベル
            // 
            合計金額ラベル.AutoSize = true;
            合計金額ラベル.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            合計金額ラベル.Location = new Point(375, 9);
            合計金額ラベル.Name = "合計金額ラベル";
            合計金額ラベル.Size = new Size(53, 12);
            合計金額ラベル.TabIndex = 89;
            合計金額ラベル.Text = "合計金額";
            // 
            // 合計数量
            // 
            合計数量.Font = new Font("BIZ UDPゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            合計数量.Location = new Point(268, 7);
            合計数量.Margin = new Padding(3, 2, 3, 2);
            合計数量.Name = "合計数量";
            合計数量.Size = new Size(88, 20);
            合計数量.TabIndex = 1;
            // 
            // 合計数量ラベル
            // 
            合計数量ラベル.AutoSize = true;
            合計数量ラベル.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            合計数量ラベル.Location = new Point(207, 9);
            合計数量ラベル.Name = "合計数量ラベル";
            合計数量ラベル.Size = new Size(53, 12);
            合計数量ラベル.TabIndex = 87;
            合計数量ラベル.Text = "合計数量";
            // 
            // 初期表示ボタン
            // 
            初期表示ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            初期表示ボタン.Location = new Point(10, 4);
            初期表示ボタン.Margin = new Padding(3, 2, 3, 2);
            初期表示ボタン.Name = "初期表示ボタン";
            初期表示ボタン.Size = new Size(75, 22);
            初期表示ボタン.TabIndex = 1;
            初期表示ボタン.Text = "受注残";
            初期表示ボタン.UseVisualStyleBackColor = true;
            初期表示ボタン.Click += 初期表示ボタン_Click;
            // 
            // 本日受注分ボタン
            // 
            本日受注分ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            本日受注分ボタン.Location = new Point(81, 4);
            本日受注分ボタン.Margin = new Padding(3, 2, 3, 2);
            本日受注分ボタン.Name = "本日受注分ボタン";
            本日受注分ボタン.Size = new Size(75, 22);
            本日受注分ボタン.TabIndex = 2;
            本日受注分ボタン.Text = "本日受注分";
            本日受注分ボタン.UseVisualStyleBackColor = true;
            本日受注分ボタン.Click += 本日受注分ボタン_Click;
            // 
            // 前日受注分ボタン
            // 
            前日受注分ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            前日受注分ボタン.Location = new Point(155, 4);
            前日受注分ボタン.Margin = new Padding(3, 2, 3, 2);
            前日受注分ボタン.Name = "前日受注分ボタン";
            前日受注分ボタン.Size = new Size(75, 22);
            前日受注分ボタン.TabIndex = 3;
            前日受注分ボタン.Text = "前日受注分";
            前日受注分ボタン.UseVisualStyleBackColor = true;
            前日受注分ボタン.Click += 前日受注分ボタン_Click;
            // 
            // 前ページボタン
            // 
            前ページボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            前ページボタン.Location = new Point(269, 4);
            前ページボタン.Margin = new Padding(3, 2, 3, 2);
            前ページボタン.Name = "前ページボタン";
            前ページボタン.Size = new Size(47, 22);
            前ページボタン.TabIndex = 4;
            前ページボタン.Text = "↑";
            前ページボタン.UseVisualStyleBackColor = true;
            前ページボタン.Click += 前ページボタン_Click;
            // 
            // 次ページボタン
            // 
            次ページボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            次ページボタン.Location = new Point(312, 4);
            次ページボタン.Margin = new Padding(3, 2, 3, 2);
            次ページボタン.Name = "次ページボタン";
            次ページボタン.Size = new Size(47, 22);
            次ページボタン.TabIndex = 5;
            次ページボタン.Text = "↓";
            次ページボタン.UseVisualStyleBackColor = true;
            次ページボタン.Click += 次ページボタン_Click;
            // 
            // 履歴トグル
            // 
            履歴トグル.Appearance = Appearance.Button;
            履歴トグル.AutoSize = true;
            履歴トグル.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            履歴トグル.Location = new Point(366, 4);
            履歴トグル.Name = "履歴トグル";
            履歴トグル.Size = new Size(72, 22);
            履歴トグル.TabIndex = 10015;
            履歴トグル.Text = "履歴モード";
            履歴トグル.UseVisualStyleBackColor = true;
            履歴トグル.CheckedChanged += 履歴トグル_CheckedChanged;
            // 
            // panel4
            // 
            panel4.Controls.Add(前日受注分ボタン);
            panel4.Controls.Add(履歴トグル);
            panel4.Controls.Add(初期表示ボタン);
            panel4.Controls.Add(次ページボタン);
            panel4.Controls.Add(本日受注分ボタン);
            panel4.Controls.Add(前ページボタン);
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(0, 32);
            panel4.Margin = new Padding(3, 2, 3, 2);
            panel4.Name = "panel4";
            panel4.Size = new Size(1032, 28);
            panel4.TabIndex = 10016;
            // 
            // F_受注管理
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1032, 513);
            Controls.Add(dataGridView1);
            Controls.Add(panel4);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            Name = "F_受注管理";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "受注管理";
            FormClosed += Form_Unload;
            Load += Form_Load;
            SizeChanged += Form_Resize;
            KeyDown += F_受注管理_KeyDown;
            Resize += Form_Resize;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Panel panel1;
        private Button コマンド生産;
        private Button コマンド受注;
        private Button コマンド顧客;
        private Button コマンド出力;
        private Button コマンド更新;
        private Button コマンド全表示;
        private Button コマンド初期化;
        private Button コマンド抽出;
        private Button コマンド終了;
        private TextBox 有効件数;
        private Label 表示件数ラベル;
        private Label label2;


        public DataGridView dataGridView1;

        private Panel panel2;
        private Button コマンド検索;
        private Button 初期表示ボタン;
        private Button 本日受注分ボタン;
        private Button 前日受注分ボタン;
        private Button 前ページボタン;
        private Button 次ページボタン;
        private Label label1;
        private TextBox 税込合計金額;
        private Label label3;
        private TextBox 合計金額;
        private Label 合計金額ラベル;
        private TextBox 合計数量;
        private Label 合計数量ラベル;
        private Button コマンド入力;
        private Button button1;
        private CheckBox 履歴トグル;
        private Panel panel3;
        private Panel panel4;
    }
}
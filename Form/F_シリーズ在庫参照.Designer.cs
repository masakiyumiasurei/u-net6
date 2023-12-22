namespace u_net
{
    partial class F_シリーズ在庫参照
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_シリーズ在庫参照));
            panel1 = new Panel();
            コマンド再計算 = new Button();
            コマンド締め = new Button();
            コマンドシリーズ = new Button();
            コマンド保守 = new Button();
            コマンド補正 = new Button();
            コマンド入出力 = new Button();
            コマンド更新 = new Button();
            コマンド初期化 = new Button();
            コマンド検索 = new Button();
            コマンド抽出 = new Button();
            コマンド印刷 = new Button();
            コマンド終了 = new Button();
            表示件数 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            dataGridView1 = new DataGridView();
            panel2 = new Panel();
            シリーズコード = new ComboBox();
            現在日 = new TextBox();
            メーカーコード_ラベル = new Label();
            今日の日付 = new TextBox();
            label3 = new Label();
            label4 = new Label();
            シリーズ名 = new TextBox();
            現在日戻るボタン = new Button();
            現在日選択ボタン = new Button();
            現在日進むボタン = new Button();
            toolTip1 = new ToolTip(components);
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaption;
            panel1.Controls.Add(コマンド再計算);
            panel1.Controls.Add(コマンド締め);
            panel1.Controls.Add(コマンドシリーズ);
            panel1.Controls.Add(コマンド保守);
            panel1.Controls.Add(コマンド補正);
            panel1.Controls.Add(コマンド入出力);
            panel1.Controls.Add(コマンド更新);
            panel1.Controls.Add(コマンド初期化);
            panel1.Controls.Add(コマンド検索);
            panel1.Controls.Add(コマンド抽出);
            panel1.Controls.Add(コマンド印刷);
            panel1.Controls.Add(コマンド終了);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(4, 2, 4, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(1035, 32);
            panel1.TabIndex = 83;
            // 
            // コマンド再計算
            // 
            コマンド再計算.Enabled = false;
            コマンド再計算.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド再計算.ForeColor = Color.Blue;
            コマンド再計算.ImageAlign = ContentAlignment.BottomLeft;
            コマンド再計算.Location = new Point(496, 4);
            コマンド再計算.Margin = new Padding(0, 2, 0, 2);
            コマンド再計算.Name = "コマンド再計算";
            コマンド再計算.Size = new Size(70, 22);
            コマンド再計算.TabIndex = 11;
            コマンド再計算.Text = "再計算";
            コマンド再計算.UseVisualStyleBackColor = true;
            コマンド再計算.Click += コマンド再計算_Click;
            // 
            // コマンド締め
            // 
            コマンド締め.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド締め.ForeColor = Color.Blue;
            コマンド締め.ImageAlign = ContentAlignment.BottomLeft;
            コマンド締め.Location = new Point(428, 4);
            コマンド締め.Margin = new Padding(0, 2, 0, 2);
            コマンド締め.Name = "コマンド締め";
            コマンド締め.Size = new Size(70, 22);
            コマンド締め.TabIndex = 10;
            コマンド締め.Text = "締め";
            toolTip1.SetToolTip(コマンド締め, "在庫締め処理");
            コマンド締め.UseVisualStyleBackColor = true;
            コマンド締め.Click += コマンド締め_Click;
            // 
            // コマンドシリーズ
            // 
            コマンドシリーズ.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンドシリーズ.ForeColor = Color.Blue;
            コマンドシリーズ.ImageAlign = ContentAlignment.BottomLeft;
            コマンドシリーズ.Location = new Point(291, 4);
            コマンドシリーズ.Margin = new Padding(0, 2, 0, 2);
            コマンドシリーズ.Name = "コマンドシリーズ";
            コマンドシリーズ.Size = new Size(70, 22);
            コマンドシリーズ.TabIndex = 9;
            コマンドシリーズ.Text = "シリーズ";
            toolTip1.SetToolTip(コマンドシリーズ, "選択しているシリーズに関する情報を表示します。");
            コマンドシリーズ.UseVisualStyleBackColor = true;
            コマンドシリーズ.Click += コマンドシリーズ_Click;
            // 
            // コマンド保守
            // 
            コマンド保守.Enabled = false;
            コマンド保守.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド保守.ForeColor = Color.Blue;
            コマンド保守.ImageAlign = ContentAlignment.BottomLeft;
            コマンド保守.Location = new Point(715, 4);
            コマンド保守.Margin = new Padding(0, 2, 0, 2);
            コマンド保守.Name = "コマンド保守";
            コマンド保守.Size = new Size(70, 22);
            コマンド保守.TabIndex = 6;
            コマンド保守.Text = "保守";
            コマンド保守.UseVisualStyleBackColor = true;
            // 
            // コマンド補正
            // 
            コマンド補正.AutoSize = true;
            コマンド補正.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド補正.ForeColor = Color.Blue;
            コマンド補正.ImageAlign = ContentAlignment.BottomLeft;
            コマンド補正.Location = new Point(360, 4);
            コマンド補正.Margin = new Padding(0, 2, 0, 2);
            コマンド補正.Name = "コマンド補正";
            コマンド補正.Size = new Size(70, 22);
            コマンド補正.TabIndex = 8;
            コマンド補正.Text = "補正";
            toolTip1.SetToolTip(コマンド補正, "選択行の在庫数量に対して補正値を追加します。");
            コマンド補正.UseVisualStyleBackColor = true;
            コマンド補正.Click += コマンド補正_Click;
            // 
            // コマンド入出力
            // 
            コマンド入出力.Enabled = false;
            コマンド入出力.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド入出力.ForeColor = Color.Blue;
            コマンド入出力.ImageAlign = ContentAlignment.BottomLeft;
            コマンド入出力.Location = new Point(647, 4);
            コマンド入出力.Margin = new Padding(0, 2, 0, 2);
            コマンド入出力.Name = "コマンド入出力";
            コマンド入出力.Size = new Size(70, 22);
            コマンド入出力.TabIndex = 7;
            コマンド入出力.Text = "入出力";
            コマンド入出力.UseVisualStyleBackColor = true;
            // 
            // コマンド更新
            // 
            コマンド更新.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド更新.ForeColor = Color.Blue;
            コマンド更新.ImageAlign = ContentAlignment.BottomLeft;
            コマンド更新.Location = new Point(207, 4);
            コマンド更新.Margin = new Padding(0, 2, 0, 2);
            コマンド更新.Name = "コマンド更新";
            コマンド更新.Size = new Size(70, 22);
            コマンド更新.TabIndex = 5;
            コマンド更新.Text = "表示更新";
            コマンド更新.UseVisualStyleBackColor = true;
            コマンド更新.Click += コマンド更新_Click;
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
            コマンド初期化.Text = "初期化";
            toolTip1.SetToolTip(コマンド初期化, "抽出初期化");
            コマンド初期化.UseVisualStyleBackColor = true;
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
            コマンド検索.Text = "検索";
            toolTip1.SetToolTip(コマンド検索, "コード検索");
            コマンド検索.UseVisualStyleBackColor = true;
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
            コマンド抽出.Text = "抽出";
            toolTip1.SetToolTip(コマンド抽出, "抽出設定");
            コマンド抽出.UseVisualStyleBackColor = true;
            // 
            // コマンド印刷
            // 
            コマンド印刷.Enabled = false;
            コマンド印刷.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド印刷.ForeColor = Color.Blue;
            コマンド印刷.ImageAlign = ContentAlignment.BottomLeft;
            コマンド印刷.Location = new Point(579, 4);
            コマンド印刷.Margin = new Padding(0, 2, 0, 2);
            コマンド印刷.Name = "コマンド印刷";
            コマンド印刷.Size = new Size(70, 22);
            コマンド印刷.TabIndex = 1;
            コマンド印刷.TabStop = false;
            コマンド印刷.Text = "印刷";
            コマンド印刷.UseVisualStyleBackColor = true;
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
            コマンド終了.Text = "終了";
            toolTip1.SetToolTip(コマンド終了, "終了");
            コマンド終了.UseVisualStyleBackColor = true;
            コマンド終了.Click += コマンド終了_Click;
            // 
            // 表示件数
            // 
            表示件数.Location = new Point(72, 4);
            表示件数.Margin = new Padding(3, 2, 3, 2);
            表示件数.Name = "表示件数";
            表示件数.Size = new Size(88, 23);
            表示件数.TabIndex = 84;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 6);
            label1.Name = "label1";
            label1.Size = new Size(55, 15);
            label1.TabIndex = 85;
            label1.Text = "表示件数";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(164, 6);
            label2.Name = "label2";
            label2.Size = new Size(19, 15);
            label2.TabIndex = 86;
            label2.Text = "件";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(0, 61);
            dataGridView1.Margin = new Padding(3, 2, 3, 2);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new Size(1300, 535);
            dataGridView1.TabIndex = 87;
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
            dataGridView1.CellPainting += DataGridView1_CellPainting;
            dataGridView1.Sorted += dataGridView1_Sorted;
            // 
            // panel2
            // 
            panel2.Controls.Add(表示件数);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(label2);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 600);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(1035, 27);
            panel2.TabIndex = 88;
            // 
            // シリーズコード
            // 
            シリーズコード.BackColor = Color.White;
            シリーズコード.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            シリーズコード.FormattingEnabled = true;
            シリーズコード.ImeMode = ImeMode.Off;
            シリーズコード.Location = new Point(681, 35);
            シリーズコード.Name = "シリーズコード";
            シリーズコード.Size = new Size(139, 21);
            シリーズコード.TabIndex = 6;
            シリーズコード.Visible = false;
            シリーズコード.KeyDown += シリーズコード_KeyDown;
            // 
            // 現在日
            // 
            現在日.BackColor = Color.White;
            現在日.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            現在日.Location = new Point(72, 35);
            現在日.Margin = new Padding(3, 2, 3, 2);
            現在日.Name = "現在日";
            現在日.Size = new Size(139, 20);
            現在日.TabIndex = 2;
            現在日.Validated += 現在日_Validated;
            // 
            // メーカーコード_ラベル
            // 
            メーカーコード_ラベル.AllowDrop = true;
            メーカーコード_ラベル.AutoEllipsis = true;
            メーカーコード_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            メーカーコード_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            メーカーコード_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            メーカーコード_ラベル.Location = new Point(5, 35);
            メーカーコード_ラベル.Margin = new Padding(0);
            メーカーコード_ラベル.Name = "メーカーコード_ラベル";
            メーカーコード_ラベル.Size = new Size(102, 17);
            メーカーコード_ラベル.TabIndex = 1;
            メーカーコード_ラベル.Text = "現在日(&P)";
            メーカーコード_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 今日の日付
            // 
            今日の日付.BackColor = Color.White;
            今日の日付.Enabled = false;
            今日の日付.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            今日の日付.Location = new Point(427, 35);
            今日の日付.Margin = new Padding(3, 2, 3, 2);
            今日の日付.Name = "今日の日付";
            今日の日付.Size = new Size(139, 20);
            今日の日付.TabIndex = 4;
            // 
            // label3
            // 
            label3.AllowDrop = true;
            label3.AutoEllipsis = true;
            label3.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.ActiveCaptionText;
            label3.ImageAlign = ContentAlignment.MiddleLeft;
            label3.Location = new Point(358, 35);
            label3.Margin = new Padding(0);
            label3.Name = "label3";
            label3.Size = new Size(102, 17);
            label3.TabIndex = 94;
            label3.Text = "今日の日付";
            label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            label4.AllowDrop = true;
            label4.AutoEllipsis = true;
            label4.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label4.ForeColor = SystemColors.ActiveCaptionText;
            label4.ImageAlign = ContentAlignment.MiddleLeft;
            label4.Location = new Point(604, 35);
            label4.Margin = new Padding(0);
            label4.Name = "label4";
            label4.Size = new Size(102, 17);
            label4.TabIndex = 5;
            label4.Text = "今日の日付";
            label4.TextAlign = ContentAlignment.MiddleLeft;
            label4.Visible = false;
            // 
            // シリーズ名
            // 
            シリーズ名.BackColor = Color.White;
            シリーズ名.Enabled = false;
            シリーズ名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            シリーズ名.Location = new Point(826, 35);
            シリーズ名.Margin = new Padding(3, 2, 3, 2);
            シリーズ名.Name = "シリーズ名";
            シリーズ名.Size = new Size(139, 20);
            シリーズ名.TabIndex = 7;
            シリーズ名.Visible = false;
            // 
            // 現在日戻るボタン
            // 
            現在日戻るボタン.Font = new Font("BIZ UDPゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point);
            現在日戻るボタン.ForeColor = Color.Black;
            現在日戻るボタン.ImageAlign = ContentAlignment.BottomLeft;
            現在日戻るボタン.Location = new Point(214, 35);
            現在日戻るボタン.Margin = new Padding(0, 2, 0, 2);
            現在日戻るボタン.Name = "現在日戻るボタン";
            現在日戻るボタン.Size = new Size(25, 22);
            現在日戻るボタン.TabIndex = 12;
            現在日戻るボタン.TabStop = false;
            現在日戻るボタン.Text = "◀";
            toolTip1.SetToolTip(現在日戻るボタン, "現在日戻る");
            現在日戻るボタン.UseVisualStyleBackColor = true;
            現在日戻るボタン.Click += 現在日戻るボタン_Click;
            // 
            // 現在日選択ボタン
            // 
            現在日選択ボタン.Font = new Font("BIZ UDPゴシック", 6F, FontStyle.Regular, GraphicsUnit.Point);
            現在日選択ボタン.ForeColor = Color.Black;
            現在日選択ボタン.ImageAlign = ContentAlignment.BottomLeft;
            現在日選択ボタン.Location = new Point(238, 35);
            現在日選択ボタン.Margin = new Padding(0, 2, 0, 2);
            現在日選択ボタン.Name = "現在日選択ボタン";
            現在日選択ボタン.Size = new Size(25, 22);
            現在日選択ボタン.TabIndex = 95;
            現在日選択ボタン.TabStop = false;
            現在日選択ボタン.Text = "▼";
            toolTip1.SetToolTip(現在日選択ボタン, "カレンダー");
            現在日選択ボタン.UseVisualStyleBackColor = true;
            現在日選択ボタン.Click += 現在日選択ボタン_Click;
            // 
            // 現在日進むボタン
            // 
            現在日進むボタン.Font = new Font("BIZ UDPゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point);
            現在日進むボタン.ForeColor = Color.Black;
            現在日進むボタン.ImageAlign = ContentAlignment.BottomLeft;
            現在日進むボタン.Location = new Point(262, 35);
            現在日進むボタン.Margin = new Padding(0, 2, 0, 2);
            現在日進むボタン.Name = "現在日進むボタン";
            現在日進むボタン.Size = new Size(25, 22);
            現在日進むボタン.TabIndex = 96;
            現在日進むボタン.TabStop = false;
            現在日進むボタン.Text = "▶";
            toolTip1.SetToolTip(現在日進むボタン, "現在日進む");
            現在日進むボタン.UseVisualStyleBackColor = true;
            現在日進むボタン.Click += 現在日進むボタン_Click;
            // 
            // F_シリーズ在庫参照
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1035, 627);
            Controls.Add(現在日進むボタン);
            Controls.Add(現在日選択ボタン);
            Controls.Add(現在日戻るボタン);
            Controls.Add(シリーズコード);
            Controls.Add(シリーズ名);
            Controls.Add(label4);
            Controls.Add(今日の日付);
            Controls.Add(label3);
            Controls.Add(現在日);
            Controls.Add(メーカーコード_ラベル);
            Controls.Add(panel2);
            Controls.Add(dataGridView1);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "F_シリーズ在庫参照";
            Text = "シリーズ在庫参照";
            FormClosing += F_シリーズ在庫参照_FormClosing;
            Load += Form_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel panel1;
        private Button コマンド再計算;
        private Button コマンド締め;
        private Button コマンドシリーズ;
        private Button コマンド補正;
        private Button コマンド入出力;
        private Button コマンド保守;
        private Button コマンド更新;
        private Button コマンド初期化;
        private Button コマンド抽出;
        private Button コマンド印刷;
        private Button コマンド終了;
        private TextBox 表示件数;
        private Label label1;
        private Label label2;
        private DataGridViewTextBoxColumn 受注明細コードDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 受注コードDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 受注版数DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn detailNumberDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 明細番号DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 受注区分コードDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 製造先コードDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn シリアル番号付加DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 型番DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 入力仕様DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 単価DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 原価DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn シリアル番号1DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn シリアル番号2DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 数量DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 単位コードDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn measureNameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn customerSerialNumberFromDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn customerSerialNumberToDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn settingSheetDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn inspectionReportDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn specificationDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn parameterSheetDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn ynEnterInvoiceDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn ynEnterStateOfDeliveryDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn ynSendInvoiceFaxDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 順序DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 在庫締めコードDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn orderDateDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn storeDateDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 製造予定日DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 製造予定更新日DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 製造開始日DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 製造終了日DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 出荷予定日DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 出荷開始日DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 出荷終了日DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn productionPlanningDateDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn productionBeginDateDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn productionEndDateDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn productionApprovedDateDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn productionApproverCodeDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn20;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn21;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn22;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn23;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn24;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn25;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn26;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn27;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn28;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn29;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn30;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn31;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn32;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn33;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn34;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn35;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn36;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn37;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn38;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn39;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn40;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn41;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn42;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn43;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn44;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn45;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn46;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn47;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn48;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn49;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn50;


        private DataGridViewTextBoxColumn 型式番号DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 型式名DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 定価DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 機能DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 構成番号DataGridViewTextBoxColumn;

        //private newDataSetTableAdapters.Vシリーズ在庫参照TableAdapter vシリーズ在庫参照TableAdapter;

        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn 商品コードDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 基本型式名DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn シリーズ名DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 在庫管理DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 在庫数量DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 在庫下限数量DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 更新日時DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 更新者名DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 廃止DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 削除DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn ユニDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 構成DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn51;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn52;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn53;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn54;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn55;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn56;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn57;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn58;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn59;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn60;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn61;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn62;
        //private newDataSetTableAdapters.Qシリーズ在庫参照TableAdapter qシリーズ在庫参照TableAdapter;
        private Panel panel2;
        private Button コマンド検索;
        private ComboBox シリーズコード;
        private TextBox 現在日;
        private Label メーカーコード_ラベル;
        private TextBox 今日の日付;
        private Label label3;
        private Label label4;
        private TextBox シリーズ名;
        private Button 現在日戻るボタン;
        private Button 現在日選択ボタン;
        private Button 現在日進むボタン;
        private ToolTip toolTip1;
    }
}
namespace u_net
{
    partial class F_売掛一覧
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_売掛一覧));
            panel1 = new Panel();
            コマンド詳細 = new Button();
            コマンド売掛資料 = new Button();
            コマンド顧客 = new Button();
            コマンド入金 = new Button();
            コマンド入出力 = new Button();
            コマンド検収 = new Button();
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
            dataGridView2 = new DataGridView();
            label4 = new Label();
            label3 = new Label();
            売掛年月 = new ComboBox();
            完了指定 = new GroupBox();
            radioButton3 = new RadioButton();
            radioButton2 = new RadioButton();
            radioButton1 = new RadioButton();
            toolTip1 = new ToolTip(components);
            panel3 = new Panel();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            完了指定.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaption;
            panel1.Controls.Add(コマンド詳細);
            panel1.Controls.Add(コマンド売掛資料);
            panel1.Controls.Add(コマンド顧客);
            panel1.Controls.Add(コマンド入金);
            panel1.Controls.Add(コマンド入出力);
            panel1.Controls.Add(コマンド検収);
            panel1.Controls.Add(コマンド更新);
            panel1.Controls.Add(コマンド初期化);
            panel1.Controls.Add(コマンド検索);
            panel1.Controls.Add(コマンド抽出);
            panel1.Controls.Add(コマンド印刷);
            panel1.Controls.Add(コマンド終了);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(5, 3, 5, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(1081, 43);
            panel1.TabIndex = 83;
            // 
            // コマンド詳細
            // 
            コマンド詳細.Enabled = false;
            コマンド詳細.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド詳細.ForeColor = Color.Blue;
            コマンド詳細.ImageAlign = ContentAlignment.BottomLeft;
            コマンド詳細.Location = new Point(411, 5);
            コマンド詳細.Margin = new Padding(0, 3, 0, 3);
            コマンド詳細.Name = "コマンド詳細";
            コマンド詳細.Size = new Size(80, 29);
            コマンド詳細.TabIndex = 6;
            コマンド詳細.Text = "詳細";
            toolTip1.SetToolTip(コマンド詳細, "詳細参照");
            コマンド詳細.UseVisualStyleBackColor = true;
            コマンド詳細.Click += コマンド詳細_Click;
            // 
            // コマンド売掛資料
            // 
            コマンド売掛資料.Enabled = false;
            コマンド売掛資料.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド売掛資料.ForeColor = Color.Blue;
            コマンド売掛資料.ImageAlign = ContentAlignment.BottomLeft;
            コマンド売掛資料.Location = new Point(567, 5);
            コマンド売掛資料.Margin = new Padding(0, 3, 0, 3);
            コマンド売掛資料.Name = "コマンド売掛資料";
            コマンド売掛資料.Size = new Size(80, 29);
            コマンド売掛資料.TabIndex = 10;
            コマンド売掛資料.Text = "売掛資料P";
            toolTip1.SetToolTip(コマンド売掛資料, "全データ表示");
            コマンド売掛資料.UseVisualStyleBackColor = true;
            コマンド売掛資料.Click += コマンド売掛資料_Click;
            // 
            // コマンド顧客
            // 
            コマンド顧客.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド顧客.ForeColor = Color.Blue;
            コマンド顧客.ImageAlign = ContentAlignment.BottomLeft;
            コマンド顧客.Location = new Point(489, 5);
            コマンド顧客.Margin = new Padding(0, 3, 0, 3);
            コマンド顧客.Name = "コマンド顧客";
            コマンド顧客.Size = new Size(80, 29);
            コマンド顧客.TabIndex = 5;
            コマンド顧客.Text = "顧客";
            toolTip1.SetToolTip(コマンド顧客, "顧客参照");
            コマンド顧客.UseVisualStyleBackColor = true;
            コマンド顧客.Click += コマンド顧客_Click;
            // 
            // コマンド入金
            // 
            コマンド入金.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド入金.ForeColor = Color.Blue;
            コマンド入金.ImageAlign = ContentAlignment.BottomLeft;
            コマンド入金.Location = new Point(333, 5);
            コマンド入金.Margin = new Padding(0, 3, 0, 3);
            コマンド入金.Name = "コマンド入金";
            コマンド入金.Size = new Size(80, 29);
            コマンド入金.TabIndex = 4;
            コマンド入金.Text = "入金";
            toolTip1.SetToolTip(コマンド入金, "入金入力");
            コマンド入金.UseVisualStyleBackColor = true;
            コマンド入金.Click += コマンド入金_Click;
            // 
            // コマンド入出力
            // 
            コマンド入出力.Enabled = false;
            コマンド入出力.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド入出力.ForeColor = Color.Blue;
            コマンド入出力.ImageAlign = ContentAlignment.BottomLeft;
            コマンド入出力.Location = new Point(817, 5);
            コマンド入出力.Margin = new Padding(0, 3, 0, 3);
            コマンド入出力.Name = "コマンド入出力";
            コマンド入出力.Size = new Size(80, 29);
            コマンド入出力.TabIndex = 9;
            コマンド入出力.Text = "入出力";
            コマンド入出力.UseVisualStyleBackColor = true;
            コマンド入出力.Click += コマンド入出力_Click;
            // 
            // コマンド検収
            // 
            コマンド検収.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド検収.ForeColor = Color.Blue;
            コマンド検収.ImageAlign = ContentAlignment.BottomLeft;
            コマンド検収.Location = new Point(739, 5);
            コマンド検収.Margin = new Padding(0, 3, 0, 3);
            コマンド検収.Name = "コマンド検収";
            コマンド検収.Size = new Size(80, 29);
            コマンド検収.TabIndex = 7;
            コマンド検収.Text = "検収通知";
            toolTip1.SetToolTip(コマンド検収, "検収通知書の到達状態を設定します。");
            コマンド検収.UseVisualStyleBackColor = true;
            コマンド検収.Click += コマンド検収_Click;
            // 
            // コマンド更新
            // 
            コマンド更新.Enabled = false;
            コマンド更新.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド更新.ForeColor = Color.Blue;
            コマンド更新.ImageAlign = ContentAlignment.BottomLeft;
            コマンド更新.Location = new Point(237, 5);
            コマンド更新.Margin = new Padding(0, 3, 0, 3);
            コマンド更新.Name = "コマンド更新";
            コマンド更新.Size = new Size(80, 29);
            コマンド更新.TabIndex = 3;
            コマンド更新.Text = "更新";
            toolTip1.SetToolTip(コマンド更新, "最新の情報に更新");
            コマンド更新.UseVisualStyleBackColor = true;
            コマンド更新.Click += コマンド更新_Click;
            // 
            // コマンド初期化
            // 
            コマンド初期化.Enabled = false;
            コマンド初期化.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド初期化.ForeColor = Color.Blue;
            コマンド初期化.ImageAlign = ContentAlignment.BottomLeft;
            コマンド初期化.Location = new Point(159, 5);
            コマンド初期化.Margin = new Padding(0, 3, 0, 3);
            コマンド初期化.Name = "コマンド初期化";
            コマンド初期化.Size = new Size(80, 29);
            コマンド初期化.TabIndex = 2;
            コマンド初期化.Text = "初期化";
            toolTip1.SetToolTip(コマンド初期化, "抽出初期化");
            コマンド初期化.UseVisualStyleBackColor = true;
            コマンド初期化.Click += コマンド初期化_Click;
            // 
            // コマンド検索
            // 
            コマンド検索.Enabled = false;
            コマンド検索.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド検索.ForeColor = Color.Blue;
            コマンド検索.ImageAlign = ContentAlignment.BottomLeft;
            コマンド検索.Location = new Point(81, 5);
            コマンド検索.Margin = new Padding(0, 3, 0, 3);
            コマンド検索.Name = "コマンド検索";
            コマンド検索.Size = new Size(80, 29);
            コマンド検索.TabIndex = 1;
            コマンド検索.Text = "検索";
            toolTip1.SetToolTip(コマンド検索, "コード検索");
            コマンド検索.UseVisualStyleBackColor = true;
            コマンド検索.Click += コマンド検索_Click;
            // 
            // コマンド抽出
            // 
            コマンド抽出.Enabled = false;
            コマンド抽出.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド抽出.ForeColor = Color.Blue;
            コマンド抽出.ImageAlign = ContentAlignment.BottomLeft;
            コマンド抽出.Location = new Point(3, 5);
            コマンド抽出.Margin = new Padding(0, 3, 0, 3);
            コマンド抽出.Name = "コマンド抽出";
            コマンド抽出.Size = new Size(80, 29);
            コマンド抽出.TabIndex = 0;
            コマンド抽出.Text = "抽出";
            toolTip1.SetToolTip(コマンド抽出, "抽出設定");
            コマンド抽出.UseVisualStyleBackColor = true;
            コマンド抽出.Click += コマンド抽出_Click;
            // 
            // コマンド印刷
            // 
            コマンド印刷.Enabled = false;
            コマンド印刷.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド印刷.ForeColor = Color.Blue;
            コマンド印刷.ImageAlign = ContentAlignment.BottomLeft;
            コマンド印刷.Location = new Point(662, 5);
            コマンド印刷.Margin = new Padding(0, 3, 0, 3);
            コマンド印刷.Name = "コマンド印刷";
            コマンド印刷.Size = new Size(80, 29);
            コマンド印刷.TabIndex = 8;
            コマンド印刷.TabStop = false;
            コマンド印刷.Text = "印刷P";
            toolTip1.SetToolTip(コマンド印刷, "印刷プレビュー");
            コマンド印刷.UseVisualStyleBackColor = true;
            コマンド印刷.Click += コマンド印刷_Click;
            // 
            // コマンド終了
            // 
            コマンド終了.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド終了.ForeColor = Color.Red;
            コマンド終了.Location = new Point(895, 5);
            コマンド終了.Margin = new Padding(0, 3, 0, 3);
            コマンド終了.Name = "コマンド終了";
            コマンド終了.Size = new Size(80, 29);
            コマンド終了.TabIndex = 11;
            コマンド終了.Text = "終了";
            toolTip1.SetToolTip(コマンド終了, "終了");
            コマンド終了.UseVisualStyleBackColor = true;
            コマンド終了.Click += コマンド終了_Click;
            // 
            // 表示件数
            // 
            表示件数.Location = new Point(90, 6);
            表示件数.Name = "表示件数";
            表示件数.Size = new Size(100, 27);
            表示件数.TabIndex = 0;
            表示件数.TextAlign = HorizontalAlignment.Right;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("BIZ UDPゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(15, 9);
            label1.Name = "label1";
            label1.Size = new Size(63, 14);
            label1.TabIndex = 85;
            label1.Text = "表示件数";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("BIZ UDPゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(195, 9);
            label2.Name = "label2";
            label2.Size = new Size(21, 14);
            label2.TabIndex = 86;
            label2.Text = "件";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 141);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new Size(1081, 281);
            dataGridView1.TabIndex = 87;
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
            dataGridView1.CellPainting += DataGridView1_CellPainting;
            dataGridView1.Sorted += dataGridView1_Sorted;
            dataGridView1.KeyDown += Form_KeyDown;
            // 
            // panel2
            // 
            panel2.Controls.Add(dataGridView2);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(表示件数);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(label2);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 422);
            panel2.Name = "panel2";
            panel2.Size = new Size(1081, 97);
            panel2.TabIndex = 88;
            // 
            // dataGridView2
            // 
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.AllowUserToDeleteRows = false;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(470, 6);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.ReadOnly = true;
            dataGridView2.RowTemplate.Height = 29;
            dataGridView2.Size = new Size(601, 75);
            dataGridView2.TabIndex = 91;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("BIZ UDPゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(429, 6);
            label4.Name = "label4";
            label4.Size = new Size(35, 14);
            label4.TabIndex = 87;
            label4.Text = "合計";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("BIZ UDPゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(35, 18);
            label3.Name = "label3";
            label3.Size = new Size(91, 14);
            label3.TabIndex = 87;
            label3.Text = "売掛年月(&M)";
            // 
            // 売掛年月
            // 
            売掛年月.DropDownWidth = 170;
            売掛年月.FormattingEnabled = true;
            売掛年月.ImeMode = ImeMode.Disable;
            売掛年月.Location = new Point(146, 13);
            売掛年月.Margin = new Padding(3, 4, 3, 4);
            売掛年月.Name = "売掛年月";
            売掛年月.Size = new Size(138, 28);
            売掛年月.TabIndex = 0;
            売掛年月.DrawItem += 売掛年月_DrawItem;
            売掛年月.SelectedIndexChanged += 売掛年月_SelectedIndexChanged;
            売掛年月.KeyDown += 売掛年月_KeyDown;
            売掛年月.KeyPress += 売掛年月_KeyPress;
            // 
            // 完了指定
            // 
            完了指定.Controls.Add(radioButton3);
            完了指定.Controls.Add(radioButton2);
            完了指定.Controls.Add(radioButton1);
            完了指定.Font = new Font("BIZ UDPゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            完了指定.Location = new Point(344, 13);
            完了指定.Margin = new Padding(3, 4, 3, 4);
            完了指定.Name = "完了指定";
            完了指定.Padding = new Padding(3, 4, 3, 4);
            完了指定.Size = new Size(631, 72);
            完了指定.TabIndex = 90;
            完了指定.TabStop = false;
            完了指定.Text = "完了指定(&C)";
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Location = new Point(464, 27);
            radioButton3.Margin = new Padding(3, 4, 3, 4);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(118, 18);
            radioButton3.TabIndex = 2;
            radioButton3.TabStop = true;
            radioButton3.Text = "指定しない(&N)";
            radioButton3.UseVisualStyleBackColor = true;
            radioButton3.CheckedChanged += radioButton3_CheckedChanged;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(249, 27);
            radioButton2.Margin = new Padding(3, 4, 3, 4);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(158, 18);
            radioButton2.TabIndex = 1;
            radioButton2.TabStop = true;
            radioButton2.Text = "完了している売掛(&E)";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += radioButton2_CheckedChanged;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(32, 27);
            radioButton1.Margin = new Padding(3, 4, 3, 4);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(175, 18);
            radioButton1.TabIndex = 0;
            radioButton1.TabStop = true;
            radioButton1.Text = "完了していない売掛(&U)";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.CheckedChanged += radioButton1_CheckedChanged;
            // 
            // panel3
            // 
            panel3.Controls.Add(売掛年月);
            panel3.Controls.Add(完了指定);
            panel3.Controls.Add(label3);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 43);
            panel3.Name = "panel3";
            panel3.Size = new Size(1081, 98);
            panel3.TabIndex = 91;
            // 
            // F_売掛一覧
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1081, 519);
            Controls.Add(dataGridView1);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "F_売掛一覧";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "売掛一覧";
            FormClosing += F_売掛一覧_FormClosing;
            Load += Form_Load;
            KeyDown += Form_KeyDown;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            完了指定.ResumeLayout(false);
            完了指定.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Panel panel1;
        private Button コマンド売掛資料;
        private Button コマンド顧客;
        private Button コマンド入金;
        private Button コマンド入出力;
        private Button コマンド検収;
        private Button コマンド更新;
        private Button コマンド初期化;
        private Button コマンド抽出;
        private Button コマンド印刷;
        private Button コマンド終了;
        public TextBox 表示件数;
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

        //private newDataSetTableAdapters.V入庫管理TableAdapter v入庫管理TableAdapter;

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
        //private newDataSetTableAdapters.Q入庫管理TableAdapter q入庫管理TableAdapter;
        private Panel panel2;
        private Button コマンド検索;
        private Button コマンド詳細;
        private Label label3;
        private Label label4;
        private ComboBox 売掛年月;
        private GroupBox 完了指定;
        private RadioButton radioButton3;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private DataGridView dataGridView2;
        private ToolTip toolTip1;
        private Panel panel3;
    }
}
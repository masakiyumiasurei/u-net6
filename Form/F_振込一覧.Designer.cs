namespace u_net
{
    partial class F_振込一覧
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_振込一覧));
            panel1 = new Panel();
            コマンド入出力 = new Button();
            コマンドF8 = new Button();
            コマンド支払通知 = new Button();
            コマンド支払先 = new Button();
            コマンド締切 = new Button();
            コマンド印刷 = new Button();
            コマンド保守 = new Button();
            コマンド初期化 = new Button();
            コマンド検索 = new Button();
            コマンド抽出 = new Button();
            コマンド更新 = new Button();
            コマンド終了 = new Button();
            表示件数 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            dataGridView1 = new DataGridView();
            panel2 = new Panel();
            toolTip1 = new ToolTip(components);
            支払年月_ラベル = new Label();
            支払年月 = new ComboBox();
            月度_ラベル = new Label();
            締切_ラベル = new Label();
            締切 = new TextBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaption;
            panel1.Controls.Add(コマンド入出力);
            panel1.Controls.Add(コマンドF8);
            panel1.Controls.Add(コマンド支払通知);
            panel1.Controls.Add(コマンド支払先);
            panel1.Controls.Add(コマンド締切);
            panel1.Controls.Add(コマンド印刷);
            panel1.Controls.Add(コマンド保守);
            panel1.Controls.Add(コマンド初期化);
            panel1.Controls.Add(コマンド検索);
            panel1.Controls.Add(コマンド抽出);
            panel1.Controls.Add(コマンド更新);
            panel1.Controls.Add(コマンド終了);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(4, 2, 4, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(1054, 32);
            panel1.TabIndex = 83;
            // 
            // コマンド入出力
            // 
            コマンド入出力.Enabled = false;
            コマンド入出力.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド入出力.ForeColor = Color.Blue;
            コマンド入出力.ImageAlign = ContentAlignment.BottomLeft;
            コマンド入出力.Location = new Point(713, 4);
            コマンド入出力.Margin = new Padding(0, 2, 0, 2);
            コマンド入出力.Name = "コマンド入出力";
            コマンド入出力.Size = new Size(70, 22);
            コマンド入出力.TabIndex = 12;
            コマンド入出力.TabStop = false;
            コマンド入出力.Text = "入出力";
            コマンド入出力.UseVisualStyleBackColor = true;
            コマンド入出力.Click += コマンド入出力_Click;
            // 
            // コマンドF8
            // 
            コマンドF8.Enabled = false;
            コマンドF8.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンドF8.ForeColor = Color.Blue;
            コマンドF8.ImageAlign = ContentAlignment.BottomLeft;
            コマンドF8.Location = new Point(496, 4);
            コマンドF8.Margin = new Padding(0, 2, 0, 2);
            コマンドF8.Name = "コマンドF8";
            コマンドF8.Size = new Size(70, 22);
            コマンドF8.TabIndex = 11;
            コマンドF8.TabStop = false;
            コマンドF8.UseVisualStyleBackColor = true;
            // 
            // コマンド支払通知
            // 
            コマンド支払通知.Enabled = false;
            コマンド支払通知.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド支払通知.ForeColor = Color.Blue;
            コマンド支払通知.ImageAlign = ContentAlignment.BottomLeft;
            コマンド支払通知.Location = new Point(428, 4);
            コマンド支払通知.Margin = new Padding(0, 2, 0, 2);
            コマンド支払通知.Name = "コマンド支払通知";
            コマンド支払通知.Size = new Size(70, 22);
            コマンド支払通知.TabIndex = 10;
            コマンド支払通知.TabStop = false;
            コマンド支払通知.Text = "支払通知";
            toolTip1.SetToolTip(コマンド支払通知, "支払通知");
            コマンド支払通知.UseVisualStyleBackColor = true;
            コマンド支払通知.Click += コマンド支払通知_Click_1;
            // 
            // コマンド支払先
            // 
            コマンド支払先.Enabled = false;
            コマンド支払先.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド支払先.ForeColor = Color.Blue;
            コマンド支払先.ImageAlign = ContentAlignment.BottomLeft;
            コマンド支払先.Location = new Point(291, 4);
            コマンド支払先.Margin = new Padding(0, 2, 0, 2);
            コマンド支払先.Name = "コマンド支払先";
            コマンド支払先.Size = new Size(70, 22);
            コマンド支払先.TabIndex = 9;
            コマンド支払先.TabStop = false;
            コマンド支払先.Text = "支払先";
            toolTip1.SetToolTip(コマンド支払先, "支払先の参照");
            コマンド支払先.UseVisualStyleBackColor = true;
            コマンド支払先.Click += コマンド支払先_Click;
            // 
            // コマンド締切
            // 
            コマンド締切.Enabled = false;
            コマンド締切.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド締切.ForeColor = Color.Blue;
            コマンド締切.ImageAlign = ContentAlignment.BottomLeft;
            コマンド締切.Location = new Point(360, 4);
            コマンド締切.Margin = new Padding(0, 2, 0, 2);
            コマンド締切.Name = "コマンド締切";
            コマンド締切.Size = new Size(70, 22);
            コマンド締切.TabIndex = 8;
            コマンド締切.TabStop = false;
            コマンド締切.Text = "締切";
            toolTip1.SetToolTip(コマンド締切, "締切の設定");
            コマンド締切.UseVisualStyleBackColor = true;
            コマンド締切.Click += コマンド締切_Click;
            // 
            // コマンド印刷
            // 
            コマンド印刷.Enabled = false;
            コマンド印刷.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド印刷.ForeColor = Color.Blue;
            コマンド印刷.ImageAlign = ContentAlignment.BottomLeft;
            コマンド印刷.Location = new Point(577, 4);
            コマンド印刷.Margin = new Padding(0, 2, 0, 2);
            コマンド印刷.Name = "コマンド印刷";
            コマンド印刷.Size = new Size(70, 22);
            コマンド印刷.TabIndex = 7;
            コマンド印刷.Text = "印刷";
            toolTip1.SetToolTip(コマンド印刷, "表示データの印刷");
            コマンド印刷.UseVisualStyleBackColor = true;
            コマンド印刷.Click += コマンド印刷_Click;
            // 
            // コマンド保守
            // 
            コマンド保守.Enabled = false;
            コマンド保守.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド保守.ForeColor = Color.Blue;
            コマンド保守.ImageAlign = ContentAlignment.BottomLeft;
            コマンド保守.Location = new Point(645, 4);
            コマンド保守.Margin = new Padding(0, 2, 0, 2);
            コマンド保守.Name = "コマンド保守";
            コマンド保守.Size = new Size(70, 22);
            コマンド保守.TabIndex = 6;
            コマンド保守.TabStop = false;
            コマンド保守.Text = "保守";
            toolTip1.SetToolTip(コマンド保守, "保守");
            コマンド保守.UseVisualStyleBackColor = true;
            コマンド保守.Click += コマンド保守_Click;
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
            toolTip1.SetToolTip(コマンド初期化, "抽出初期化");
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
            toolTip1.SetToolTip(コマンド検索, "コード検索");
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
            toolTip1.SetToolTip(コマンド抽出, "抽出設定");
            コマンド抽出.UseVisualStyleBackColor = true;
            コマンド抽出.Click += コマンド抽出_Click;
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
            コマンド更新.TabIndex = 1;
            コマンド更新.TabStop = false;
            コマンド更新.Text = "更新";
            toolTip1.SetToolTip(コマンド更新, "最新の情報に更新");
            コマンド更新.UseVisualStyleBackColor = true;
            コマンド更新.Click += コマンド更新_Click;
            // 
            // コマンド終了
            // 
            コマンド終了.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド終了.ForeColor = Color.Red;
            コマンド終了.Location = new Point(781, 4);
            コマンド終了.Margin = new Padding(0, 2, 0, 2);
            コマンド終了.Name = "コマンド終了";
            コマンド終了.Size = new Size(70, 22);
            コマンド終了.TabIndex = 0;
            コマンド終了.TabStop = false;
            コマンド終了.Text = "終了";
            toolTip1.SetToolTip(コマンド終了, "終了");
            コマンド終了.UseVisualStyleBackColor = true;
            コマンド終了.Click += コマンド終了_Click;
            // 
            // 表示件数
            // 
            表示件数.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            表示件数.Location = new Point(72, 4);
            表示件数.Margin = new Padding(3, 2, 3, 2);
            表示件数.Name = "表示件数";
            表示件数.Size = new Size(88, 20);
            表示件数.TabIndex = 84;
            表示件数.TabStop = false;
            表示件数.TextAlign = HorizontalAlignment.Right;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(7, 8);
            label1.Name = "label1";
            label1.Size = new Size(53, 12);
            label1.TabIndex = 85;
            label1.Text = "表示件数";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(163, 8);
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
            dataGridView1.Location = new Point(0, 75);
            dataGridView1.Margin = new Padding(3, 2, 3, 2);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new Size(1054, 440);
            dataGridView1.TabIndex = 1;
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
            dataGridView1.CellPainting += DataGridView1_CellPainting;
            dataGridView1.Sorted += dataGridView1_Sorted;
            dataGridView1.KeyDown += dataGridView1_KeyDown;
            // 
            // panel2
            // 
            panel2.Controls.Add(表示件数);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(label2);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 515);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(1054, 27);
            panel2.TabIndex = 88;
            // 
            // 支払年月_ラベル
            // 
            支払年月_ラベル.AllowDrop = true;
            支払年月_ラベル.AutoEllipsis = true;
            支払年月_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            支払年月_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            支払年月_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            支払年月_ラベル.Location = new Point(9, 41);
            支払年月_ラベル.Margin = new Padding(0);
            支払年月_ラベル.Name = "支払年月_ラベル";
            支払年月_ラベル.Size = new Size(75, 20);
            支払年月_ラベル.TabIndex = 1;
            支払年月_ラベル.Text = "支払年月(&M)";
            支払年月_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 支払年月
            // 
            支払年月.BackColor = SystemColors.Window;
            支払年月.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            支払年月.FormattingEnabled = true;
            支払年月.ImeMode = ImeMode.Disable;
            支払年月.Location = new Point(87, 41);
            支払年月.Name = "支払年月";
            支払年月.Size = new Size(102, 21);
            支払年月.TabIndex = 2;
            支払年月.SelectedIndexChanged += 集計年月_SelectedIndexChanged;
            // 
            // 月度_ラベル
            // 
            月度_ラベル.AllowDrop = true;
            月度_ラベル.AutoEllipsis = true;
            月度_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            月度_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            月度_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            月度_ラベル.Location = new Point(192, 41);
            月度_ラベル.Margin = new Padding(0);
            月度_ラベル.Name = "月度_ラベル";
            月度_ラベル.Size = new Size(56, 20);
            月度_ラベル.TabIndex = 89;
            月度_ラベル.Text = "月";
            月度_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 締切_ラベル
            // 
            締切_ラベル.AllowDrop = true;
            締切_ラベル.AutoEllipsis = true;
            締切_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            締切_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            締切_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            締切_ラベル.Location = new Point(268, 41);
            締切_ラベル.Margin = new Padding(0);
            締切_ラベル.Name = "締切_ラベル";
            締切_ラベル.Size = new Size(56, 20);
            締切_ラベル.TabIndex = 3;
            締切_ラベル.Text = "締切(&C)";
            締切_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 締切
            // 
            締切.BackColor = Color.White;
            締切.Enabled = false;
            締切.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            締切.ImeMode = ImeMode.NoControl;
            締切.Location = new Point(325, 41);
            締切.Margin = new Padding(3, 2, 3, 2);
            締切.Name = "締切";
            締切.ReadOnly = true;
            締切.Size = new Size(17, 20);
            締切.TabIndex = 91;
            締切.TabStop = false;
            // 
            // F_振込一覧
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1054, 542);
            Controls.Add(締切);
            Controls.Add(締切_ラベル);
            Controls.Add(月度_ラベル);
            Controls.Add(支払年月);
            Controls.Add(支払年月_ラベル);
            Controls.Add(panel2);
            Controls.Add(dataGridView1);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            Name = "F_振込一覧";
            Text = "支払一覧";
            Load += Form_Load;
            KeyDown += Form_KeyDown;
            Resize += Form_Resize;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel panel1;
        private Button コマンドF8;
        private Button コマンド支払通知;
        private Button コマンド支払先;
        private Button コマンド締切;
        private Button コマンド印刷;
        private Button コマンド保守;
        private Button コマンド初期化;
        private Button コマンド抽出;
        private Button コマンド更新;
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

        //private newDataSetTableAdapters.Vメーカー管理TableAdapter vメーカー管理TableAdapter;

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
        //private newDataSetTableAdapters.Qメーカー管理TableAdapter qメーカー管理TableAdapter;
        private Panel panel2;
        private Button コマンド検索;
        private Button コマンド入出力;
        private ToolTip toolTip1;
        private Label 支払年月_ラベル;
        internal ComboBox 支払年月;
        private Label 月度_ラベル;
        private Label 締切_ラベル;
        private TextBox 締切;
    }
}
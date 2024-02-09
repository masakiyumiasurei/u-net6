namespace u_net
{
    partial class F_ファックス管理
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_ファックス管理));
            panel1 = new Panel();
            コマンド更新 = new Button();
            command = new Button();
            コマンド再送 = new Button();
            コマンド参照 = new Button();
            コマンド表示 = new Button();
            コマンド非表示 = new Button();
            コマンド入出力 = new Button();
            コマンド初期化 = new Button();
            コマンド検索 = new Button();
            コマンド抽出 = new Button();
            コマンド印刷 = new Button();
            コマンド終了 = new Button();
            dataGridView1 = new DataGridView();
            panel3 = new Panel();
            label3 = new Label();
            toolTip1 = new ToolTip(components);
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            panel2 = new Panel();
            ボタン = new Button();
            表示件数 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel3.SuspendLayout();
            statusStrip1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaption;
            panel1.Controls.Add(コマンド更新);
            panel1.Controls.Add(command);
            panel1.Controls.Add(コマンド再送);
            panel1.Controls.Add(コマンド参照);
            panel1.Controls.Add(コマンド表示);
            panel1.Controls.Add(コマンド非表示);
            panel1.Controls.Add(コマンド入出力);
            panel1.Controls.Add(コマンド初期化);
            panel1.Controls.Add(コマンド検索);
            panel1.Controls.Add(コマンド抽出);
            panel1.Controls.Add(コマンド印刷);
            panel1.Controls.Add(コマンド終了);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(5, 3, 5, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(1205, 43);
            panel1.TabIndex = 83;
            // 
            // コマンド更新
            // 
            コマンド更新.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド更新.ForeColor = Color.Blue;
            コマンド更新.ImageAlign = ContentAlignment.BottomLeft;
            コマンド更新.Location = new Point(815, 5);
            コマンド更新.Margin = new Padding(0, 3, 0, 3);
            コマンド更新.Name = "コマンド更新";
            コマンド更新.Size = new Size(80, 29);
            コマンド更新.TabIndex = 10;
            コマンド更新.TabStop = false;
            コマンド更新.Text = "更新";
            toolTip1.SetToolTip(コマンド更新, "最新の情報に更新");
            コマンド更新.UseVisualStyleBackColor = true;
            コマンド更新.Click += コマンド更新_Click;
            // 
            // command
            // 
            command.Enabled = false;
            command.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            command.ForeColor = Color.Blue;
            command.ImageAlign = ContentAlignment.BottomLeft;
            command.Location = new Point(567, 5);
            command.Margin = new Padding(0, 3, 0, 3);
            command.Name = "command";
            command.Size = new Size(80, 29);
            command.TabIndex = 11;
            command.TabStop = false;
            command.UseVisualStyleBackColor = true;
            // 
            // コマンド再送
            // 
            コマンド再送.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド再送.ForeColor = Color.Blue;
            コマンド再送.ImageAlign = ContentAlignment.BottomLeft;
            コマンド再送.Location = new Point(489, 5);
            コマンド再送.Margin = new Padding(0, 3, 0, 3);
            コマンド再送.Name = "コマンド再送";
            コマンド再送.Size = new Size(80, 29);
            コマンド再送.TabIndex = 1;
            コマンド再送.TabStop = false;
            コマンド再送.Text = "再送";
            toolTip1.SetToolTip(コマンド再送, "選択データのFAX再送");
            コマンド再送.UseVisualStyleBackColor = true;
            コマンド再送.Click += コマンド再送_Click;
            // 
            // コマンド参照
            // 
            コマンド参照.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド参照.ForeColor = Color.Blue;
            コマンド参照.ImageAlign = ContentAlignment.BottomLeft;
            コマンド参照.Location = new Point(333, 5);
            コマンド参照.Margin = new Padding(0, 3, 0, 3);
            コマンド参照.Name = "コマンド参照";
            コマンド参照.Size = new Size(80, 29);
            コマンド参照.TabIndex = 7;
            コマンド参照.TabStop = false;
            コマンド参照.Text = "送信元";
            toolTip1.SetToolTip(コマンド参照, "送信元データの参照");
            コマンド参照.UseVisualStyleBackColor = true;
            コマンド参照.Click += コマンド参照_Click;
            コマンド参照.Enter += コマンド参照_Enter;
            コマンド参照.Leave += コマンド参照_Leave;
            // 
            // コマンド表示
            // 
            コマンド表示.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド表示.ForeColor = Color.Blue;
            コマンド表示.ImageAlign = ContentAlignment.BottomLeft;
            コマンド表示.Location = new Point(411, 5);
            コマンド表示.Margin = new Padding(0, 3, 0, 3);
            コマンド表示.Name = "コマンド表示";
            コマンド表示.Size = new Size(80, 29);
            コマンド表示.TabIndex = 9;
            コマンド表示.TabStop = false;
            コマンド表示.Text = "送信文書";
            toolTip1.SetToolTip(コマンド表示, "送信文書のプレビュー");
            コマンド表示.UseVisualStyleBackColor = true;
            コマンド表示.Click += コマンド表示_Click;
            コマンド表示.Enter += コマンド表示_Enter;
            コマンド表示.Leave += コマンド表示_Leave;
            // 
            // コマンド非表示
            // 
            コマンド非表示.Enabled = false;
            コマンド非表示.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド非表示.ForeColor = Color.Blue;
            コマンド非表示.ImageAlign = ContentAlignment.BottomLeft;
            コマンド非表示.Location = new Point(659, 5);
            コマンド非表示.Margin = new Padding(0, 3, 0, 3);
            コマンド非表示.Name = "コマンド非表示";
            コマンド非表示.Size = new Size(80, 29);
            コマンド非表示.TabIndex = 8;
            コマンド非表示.TabStop = false;
            コマンド非表示.Text = "非表示";
            toolTip1.SetToolTip(コマンド非表示, "ウィンドウ非表示");
            コマンド非表示.UseVisualStyleBackColor = true;
            コマンド非表示.Click += コマンド非表示_Click;
            // 
            // コマンド入出力
            // 
            コマンド入出力.Enabled = false;
            コマンド入出力.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド入出力.ForeColor = Color.Blue;
            コマンド入出力.ImageAlign = ContentAlignment.BottomLeft;
            コマンド入出力.Location = new Point(737, 5);
            コマンド入出力.Margin = new Padding(0, 3, 0, 3);
            コマンド入出力.Name = "コマンド入出力";
            コマンド入出力.Size = new Size(80, 29);
            コマンド入出力.TabIndex = 11;
            コマンド入出力.TabStop = false;
            コマンド入出力.Text = "入出力";
            コマンド入出力.UseVisualStyleBackColor = true;
            // 
            // コマンド初期化
            // 
            コマンド初期化.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド初期化.ForeColor = Color.Blue;
            コマンド初期化.ImageAlign = ContentAlignment.BottomLeft;
            コマンド初期化.Location = new Point(159, 5);
            コマンド初期化.Margin = new Padding(0, 3, 0, 3);
            コマンド初期化.Name = "コマンド初期化";
            コマンド初期化.Size = new Size(80, 29);
            コマンド初期化.TabIndex = 4;
            コマンド初期化.TabStop = false;
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
            コマンド抽出.Location = new Point(3, 5);
            コマンド抽出.Margin = new Padding(0, 3, 0, 3);
            コマンド抽出.Name = "コマンド抽出";
            コマンド抽出.Size = new Size(80, 29);
            コマンド抽出.TabIndex = 2;
            コマンド抽出.TabStop = false;
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
            コマンド印刷.Location = new Point(237, 5);
            コマンド印刷.Margin = new Padding(0, 3, 0, 3);
            コマンド印刷.Name = "コマンド印刷";
            コマンド印刷.Size = new Size(80, 29);
            コマンド印刷.TabIndex = 5;
            コマンド印刷.TabStop = false;
            コマンド印刷.Text = "印刷";
            コマンド印刷.UseVisualStyleBackColor = true;
            // 
            // コマンド終了
            // 
            コマンド終了.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド終了.ForeColor = Color.Red;
            コマンド終了.Location = new Point(893, 5);
            コマンド終了.Margin = new Padding(0, 3, 0, 3);
            コマンド終了.Name = "コマンド終了";
            コマンド終了.Size = new Size(80, 29);
            コマンド終了.TabIndex = 0;
            コマンド終了.TabStop = false;
            コマンド終了.Text = "終了";
            toolTip1.SetToolTip(コマンド終了, "終了");
            コマンド終了.UseVisualStyleBackColor = true;
            コマンド終了.Click += コマンド終了_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 79);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new Size(1205, 584);
            dataGridView1.TabIndex = 87;
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
            // 
            // panel3
            // 
            panel3.Controls.Add(label3);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 43);
            panel3.Name = "panel3";
            panel3.Size = new Size(1205, 36);
            panel3.TabIndex = 89;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(18, 9);
            label3.Name = "label3";
            label3.Size = new Size(466, 12);
            label3.TabIndex = 88;
            label3.Text = "※FAX送信中の状態で本ウィンドウを閉じるには[非表示]コマンドを実行してください。";
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 641);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 22, 0);
            statusStrip1.Size = new Size(1205, 22);
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
            // panel2
            // 
            panel2.Controls.Add(ボタン);
            panel2.Controls.Add(表示件数);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(label2);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 605);
            panel2.Name = "panel2";
            panel2.Size = new Size(1205, 36);
            panel2.TabIndex = 10197;
            // 
            // ボタン
            // 
            ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            ボタン.Location = new Point(1025, 0);
            ボタン.Margin = new Padding(3, 4, 3, 4);
            ボタン.Name = "ボタン";
            ボタン.Size = new Size(87, 35);
            ボタン.TabIndex = 87;
            ボタン.Text = "ログ出力";
            ボタン.UseVisualStyleBackColor = true;
            ボタン.Visible = false;
            // 
            // 表示件数
            // 
            表示件数.Location = new Point(79, 3);
            表示件数.Name = "表示件数";
            表示件数.Size = new Size(100, 27);
            表示件数.TabIndex = 0;
            表示件数.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(10, 11);
            label1.Name = "label1";
            label1.Size = new Size(53, 12);
            label1.TabIndex = 85;
            label1.Text = "表示件数";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(186, 11);
            label2.Name = "label2";
            label2.Size = new Size(17, 12);
            label2.TabIndex = 86;
            label2.Text = "件";
            // 
            // F_ファックス管理
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1205, 663);
            Controls.Add(panel2);
            Controls.Add(statusStrip1);
            Controls.Add(dataGridView1);
            Controls.Add(panel3);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "F_ファックス管理";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ファックス管理";
            FormClosed += F_ファックス管理_FormClosed;
            Load += Form_Load;
            KeyDown += Form_KeyDown;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel panel1;
        private Button command;
        private Button コマンド再送;
        private Button コマンド参照;
        private Button コマンド表示;
        private Button コマンド非表示;
        private Button コマンド入出力;
        private Button コマンド初期化;
        private Button コマンド抽出;
        private Button コマンド印刷;
        private Button コマンド終了;
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
        private Button コマンド検索;
        private Button コマンド更新;
        private Panel panel3;
        private Label label3;
        private ToolTip toolTip1;
        private StatusStrip statusStrip1;
        internal ToolStripStatusLabel toolStripStatusLabel1;
        private Panel panel2;
        private Button ボタン;
        private TextBox 表示件数;
        private Label label1;
        private Label label2;
    }
}
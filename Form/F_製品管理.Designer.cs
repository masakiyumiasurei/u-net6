﻿namespace u_net
{
    partial class F_製品管理
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_製品管理));
            panel1 = new Panel();
            コマンドF11 = new Button();
            コマンド指導書変更有り = new Button();
            コマンド指導書設定 = new Button();
            コマンド製品 = new Button();
            コマンド材料費 = new Button();
            コマンド参照用 = new Button();
            コマンドF10 = new Button();
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
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaption;
            panel1.Controls.Add(コマンドF11);
            panel1.Controls.Add(コマンド指導書変更有り);
            panel1.Controls.Add(コマンド指導書設定);
            panel1.Controls.Add(コマンド製品);
            panel1.Controls.Add(コマンド材料費);
            panel1.Controls.Add(コマンド参照用);
            panel1.Controls.Add(コマンドF10);
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
            // コマンドF11
            // 
            コマンドF11.Enabled = false;
            コマンドF11.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンドF11.ForeColor = Color.Blue;
            コマンドF11.ImageAlign = ContentAlignment.BottomLeft;
            コマンドF11.Location = new Point(713, 4);
            コマンドF11.Margin = new Padding(0, 2, 0, 2);
            コマンドF11.Name = "コマンドF11";
            コマンドF11.Size = new Size(70, 22);
            コマンドF11.TabIndex = 12;
            コマンドF11.TabStop = false;
            コマンドF11.UseVisualStyleBackColor = true;
            // 
            // コマンド指導書変更有り
            // 
            コマンド指導書変更有り.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド指導書変更有り.ForeColor = Color.Blue;
            コマンド指導書変更有り.ImageAlign = ContentAlignment.BottomLeft;
            コマンド指導書変更有り.Location = new Point(496, 4);
            コマンド指導書変更有り.Margin = new Padding(0, 2, 0, 2);
            コマンド指導書変更有り.Name = "コマンド指導書変更有り";
            コマンド指導書変更有り.Size = new Size(70, 22);
            コマンド指導書変更有り.TabIndex = 11;
            コマンド指導書変更有り.TabStop = false;
            コマンド指導書変更有り.Text = "指変更有";
            toolTip1.SetToolTip(コマンド指導書変更有り, "指導書変更有りデータの抽出");
            コマンド指導書変更有り.UseVisualStyleBackColor = true;
            コマンド指導書変更有り.Click += コマンド指導書変更有り_Click;
            // 
            // コマンド指導書設定
            // 
            コマンド指導書設定.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド指導書設定.ForeColor = Color.Blue;
            コマンド指導書設定.ImageAlign = ContentAlignment.BottomLeft;
            コマンド指導書設定.Location = new Point(428, 4);
            コマンド指導書設定.Margin = new Padding(0, 2, 0, 2);
            コマンド指導書設定.Name = "コマンド指導書設定";
            コマンド指導書設定.Size = new Size(70, 22);
            コマンド指導書設定.TabIndex = 10;
            コマンド指導書設定.TabStop = false;
            コマンド指導書設定.Text = "指導設定";
            toolTip1.SetToolTip(コマンド指導書設定, "指導書変更の設定");
            コマンド指導書設定.UseVisualStyleBackColor = true;
            コマンド指導書設定.Click += コマンド指導書設定_Click;
            // 
            // コマンド製品
            // 
            コマンド製品.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド製品.ForeColor = Color.Blue;
            コマンド製品.ImageAlign = ContentAlignment.BottomLeft;
            コマンド製品.Location = new Point(291, 4);
            コマンド製品.Margin = new Padding(0, 2, 0, 2);
            コマンド製品.Name = "コマンド製品";
            コマンド製品.Size = new Size(70, 22);
            コマンド製品.TabIndex = 9;
            コマンド製品.TabStop = false;
            コマンド製品.Text = "製品";
            toolTip1.SetToolTip(コマンド製品, "製品の参照");
            コマンド製品.UseVisualStyleBackColor = true;
            コマンド製品.Click += コマンド製品_Click;
            // 
            // コマンド材料費
            // 
            コマンド材料費.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド材料費.ForeColor = Color.Blue;
            コマンド材料費.ImageAlign = ContentAlignment.BottomLeft;
            コマンド材料費.Location = new Point(360, 4);
            コマンド材料費.Margin = new Padding(0, 2, 0, 2);
            コマンド材料費.Name = "コマンド材料費";
            コマンド材料費.Size = new Size(70, 22);
            コマンド材料費.TabIndex = 8;
            コマンド材料費.TabStop = false;
            コマンド材料費.Text = "材料費";
            toolTip1.SetToolTip(コマンド材料費, "材料費の参照");
            コマンド材料費.UseVisualStyleBackColor = true;
            コマンド材料費.Click += コマンド材料費_Click;
            // 
            // コマンド参照用
            // 
            コマンド参照用.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド参照用.ForeColor = Color.Blue;
            コマンド参照用.ImageAlign = ContentAlignment.BottomLeft;
            コマンド参照用.Location = new Point(577, 4);
            コマンド参照用.Margin = new Padding(0, 2, 0, 2);
            コマンド参照用.Name = "コマンド参照用";
            コマンド参照用.Size = new Size(70, 22);
            コマンド参照用.TabIndex = 7;
            コマンド参照用.TabStop = false;
            コマンド参照用.Text = "参照用";
            コマンド参照用.UseVisualStyleBackColor = true;
            コマンド参照用.Click += コマンド参照用_Click;
            // 
            // コマンドF10
            // 
            コマンドF10.Enabled = false;
            コマンドF10.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンドF10.ForeColor = Color.Blue;
            コマンドF10.ImageAlign = ContentAlignment.BottomLeft;
            コマンドF10.Location = new Point(645, 4);
            コマンドF10.Margin = new Padding(0, 2, 0, 2);
            コマンドF10.Name = "コマンドF10";
            コマンドF10.Size = new Size(70, 22);
            コマンドF10.TabIndex = 6;
            コマンドF10.TabStop = false;
            コマンドF10.UseVisualStyleBackColor = true;
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
            toolTip1.SetToolTip(コマンド初期化, "抽出条件の初期化");
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
            toolTip1.SetToolTip(コマンド検索, "コードによる検索");
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
            toolTip1.SetToolTip(コマンド抽出, "抽出条件の設定");
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
            label2.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(163, 8);
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
            dataGridView1.Location = new Point(0, 32);
            dataGridView1.Margin = new Padding(3, 2, 3, 2);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new Size(1054, 438);
            dataGridView1.TabIndex = 87;
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
            dataGridView1.CellPainting += DataGridView1_CellPainting;
            dataGridView1.KeyDown += dataGridView1_KeyDown;
            dataGridView1.KeyUp += dataGridView1_KeyUp;
            // 
            // panel2
            // 
            panel2.Controls.Add(表示件数);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(label2);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 470);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(1054, 27);
            panel2.TabIndex = 88;
            // 
            // F_製品管理
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1054, 497);
            Controls.Add(dataGridView1);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            Name = "F_製品管理";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "製品管理";
            FormClosing += F_製品管理_FormClosing;
            Load += Form_Load;
            KeyDown += Form_KeyDown;
            KeyUp += F_製品管理_KeyUp;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Panel panel1;
        private Button コマンド指導書変更有り;
        private Button コマンド指導書設定;
        private Button コマンド製品;
        private Button コマンド材料費;
        private Button コマンド参照用;
        private Button コマンドF10;
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
        private Button コマンドF11;
        private ToolTip toolTip1;
    }
}
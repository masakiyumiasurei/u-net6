﻿namespace u_net
{
    partial class F_入出庫履歴
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_入出庫履歴));
            panel1 = new Panel();
            button4 = new Button();
            button3 = new Button();
            コマンドメーカー = new Button();
            コマンドメール = new Button();
            コマンド更新 = new Button();
            コマンド入出力 = new Button();
            コマンド印刷 = new Button();
            コマンド全表示 = new Button();
            コマンド初期化 = new Button();
            コマンド抽出 = new Button();
            コマンドF9 = new Button();
            コマンド終了 = new Button();
            出庫数合計 = new TextBox();
            label1 = new Label();
            dataGridView1 = new DataGridView();
            日付 = new DataGridViewTextBoxColumn();
            購買コード = new DataGridViewTextBoxColumn();
            シリーズ名 = new DataGridViewTextBoxColumn();
            備考 = new DataGridViewTextBoxColumn();
            入出庫コード = new DataGridViewTextBoxColumn();
            入庫数量 = new DataGridViewTextBoxColumn();
            出庫数量 = new DataGridViewTextBoxColumn();
            panel2 = new Panel();
            入庫数合計 = new TextBox();
            label2 = new Label();
            部品コード = new ComboBox();
            品名 = new TextBox();
            部品コード_ラベル = new Label();
            型番 = new TextBox();
            toolTip1 = new ToolTip(components);
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaption;
            panel1.Controls.Add(button4);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(コマンドメーカー);
            panel1.Controls.Add(コマンドメール);
            panel1.Controls.Add(コマンド更新);
            panel1.Controls.Add(コマンド入出力);
            panel1.Controls.Add(コマンド印刷);
            panel1.Controls.Add(コマンド全表示);
            panel1.Controls.Add(コマンド初期化);
            panel1.Controls.Add(コマンド抽出);
            panel1.Controls.Add(コマンドF9);
            panel1.Controls.Add(コマンド終了);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(4, 2, 4, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(1054, 32);
            panel1.TabIndex = 83;
            // 
            // button4
            // 
            button4.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            button4.ForeColor = Color.Blue;
            button4.ImageAlign = ContentAlignment.BottomLeft;
            button4.Location = new Point(496, 4);
            button4.Margin = new Padding(0, 2, 0, 2);
            button4.Name = "button4";
            button4.Size = new Size(70, 22);
            button4.TabIndex = 11;
            button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            button3.ForeColor = Color.Blue;
            button3.ImageAlign = ContentAlignment.BottomLeft;
            button3.Location = new Point(428, 4);
            button3.Margin = new Padding(0, 2, 0, 2);
            button3.Name = "button3";
            button3.Size = new Size(70, 22);
            button3.TabIndex = 10;
            button3.UseVisualStyleBackColor = true;
            // 
            // コマンドメーカー
            // 
            コマンドメーカー.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンドメーカー.ForeColor = Color.Blue;
            コマンドメーカー.ImageAlign = ContentAlignment.BottomLeft;
            コマンドメーカー.Location = new Point(291, 4);
            コマンドメーカー.Margin = new Padding(0, 2, 0, 2);
            コマンドメーカー.Name = "コマンドメーカー";
            コマンドメーカー.Size = new Size(70, 22);
            コマンドメーカー.TabIndex = 9;
            コマンドメーカー.UseVisualStyleBackColor = true;
            // 
            // コマンドメール
            // 
            コマンドメール.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンドメール.ForeColor = Color.Blue;
            コマンドメール.ImageAlign = ContentAlignment.BottomLeft;
            コマンドメール.Location = new Point(360, 4);
            コマンドメール.Margin = new Padding(0, 2, 0, 2);
            コマンドメール.Name = "コマンドメール";
            コマンドメール.Size = new Size(70, 22);
            コマンドメール.TabIndex = 8;
            コマンドメール.UseVisualStyleBackColor = true;
            // 
            // コマンド更新
            // 
            コマンド更新.Enabled = false;
            コマンド更新.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド更新.ImageAlign = ContentAlignment.BottomLeft;
            コマンド更新.Location = new Point(647, 4);
            コマンド更新.Margin = new Padding(0, 2, 0, 2);
            コマンド更新.Name = "コマンド更新";
            コマンド更新.Size = new Size(70, 22);
            コマンド更新.TabIndex = 7;
            コマンド更新.Text = "更新";
            toolTip1.SetToolTip(コマンド更新, "表示更新");
            コマンド更新.UseVisualStyleBackColor = true;
            // 
            // コマンド入出力
            // 
            コマンド入出力.Enabled = false;
            コマンド入出力.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド入出力.ImageAlign = ContentAlignment.BottomLeft;
            コマンド入出力.Location = new Point(715, 4);
            コマンド入出力.Margin = new Padding(0, 2, 0, 2);
            コマンド入出力.Name = "コマンド入出力";
            コマンド入出力.Size = new Size(70, 22);
            コマンド入出力.TabIndex = 6;
            コマンド入出力.UseVisualStyleBackColor = true;
            // 
            // コマンド印刷
            // 
            コマンド印刷.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド印刷.ForeColor = Color.Blue;
            コマンド印刷.ImageAlign = ContentAlignment.BottomLeft;
            コマンド印刷.Location = new Point(207, 4);
            コマンド印刷.Margin = new Padding(0, 2, 0, 2);
            コマンド印刷.Name = "コマンド印刷";
            コマンド印刷.Size = new Size(70, 22);
            コマンド印刷.TabIndex = 5;
            コマンド印刷.Text = "印刷";
            toolTip1.SetToolTip(コマンド印刷, "印刷");
            コマンド印刷.UseVisualStyleBackColor = true;
            // 
            // コマンド全表示
            // 
            コマンド全表示.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド全表示.ForeColor = Color.Blue;
            コマンド全表示.ImageAlign = ContentAlignment.BottomLeft;
            コマンド全表示.Location = new Point(139, 4);
            コマンド全表示.Margin = new Padding(0, 2, 0, 2);
            コマンド全表示.Name = "コマンド全表示";
            コマンド全表示.Size = new Size(70, 22);
            コマンド全表示.TabIndex = 4;
            コマンド全表示.Text = "全表示";
            toolTip1.SetToolTip(コマンド全表示, "全データ表示");
            コマンド全表示.UseVisualStyleBackColor = true;
            // 
            // コマンド初期化
            // 
            コマンド初期化.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド初期化.ForeColor = Color.Blue;
            コマンド初期化.ImageAlign = ContentAlignment.BottomLeft;
            コマンド初期化.Location = new Point(71, 4);
            コマンド初期化.Margin = new Padding(0, 2, 0, 2);
            コマンド初期化.Name = "コマンド初期化";
            コマンド初期化.Size = new Size(70, 22);
            コマンド初期化.TabIndex = 3;
            コマンド初期化.Text = "初期化";
            toolTip1.SetToolTip(コマンド初期化, "抽出初期化");
            コマンド初期化.UseVisualStyleBackColor = true;
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
            // コマンドF9
            // 
            コマンドF9.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンドF9.ForeColor = Color.Blue;
            コマンドF9.ImageAlign = ContentAlignment.BottomLeft;
            コマンドF9.Location = new Point(579, 4);
            コマンドF9.Margin = new Padding(0, 2, 0, 2);
            コマンドF9.Name = "コマンドF9";
            コマンドF9.Size = new Size(70, 22);
            コマンドF9.TabIndex = 1;
            コマンドF9.UseVisualStyleBackColor = true;
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
            // 出庫数合計
            // 
            出庫数合計.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            出庫数合計.Location = new Point(960, 23);
            出庫数合計.Margin = new Padding(3, 2, 3, 2);
            出庫数合計.Name = "出庫数合計";
            出庫数合計.Size = new Size(88, 19);
            出庫数合計.TabIndex = 84;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(960, 5);
            label1.Name = "label1";
            label1.Size = new Size(65, 12);
            label1.TabIndex = 85;
            label1.Text = "出庫数合計";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { 日付, 購買コード, シリーズ名, 備考, 入出庫コード, 入庫数量, 出庫数量 });
            dataGridView1.Location = new Point(0, 86);
            dataGridView1.Margin = new Padding(3, 2, 3, 2);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new Size(1054, 340);
            dataGridView1.TabIndex = 87;
            // 
            // 日付
            // 
            日付.HeaderText = "日付";
            日付.Name = "日付";
            日付.ReadOnly = true;
            // 
            // 購買コード
            // 
            購買コード.HeaderText = "購買コード";
            購買コード.Name = "購買コード";
            購買コード.ReadOnly = true;
            // 
            // シリーズ名
            // 
            シリーズ名.HeaderText = "シリーズ名";
            シリーズ名.Name = "シリーズ名";
            シリーズ名.ReadOnly = true;
            // 
            // 備考
            // 
            備考.HeaderText = "備考";
            備考.Name = "備考";
            備考.ReadOnly = true;
            // 
            // 入出庫コード
            // 
            入出庫コード.HeaderText = "入出庫コード";
            入出庫コード.Name = "入出庫コード";
            入出庫コード.ReadOnly = true;
            // 
            // 入庫数量
            // 
            入庫数量.HeaderText = "入庫数量";
            入庫数量.Name = "入庫数量";
            入庫数量.ReadOnly = true;
            // 
            // 出庫数量
            // 
            出庫数量.HeaderText = "出庫数量";
            出庫数量.Name = "出庫数量";
            出庫数量.ReadOnly = true;
            // 
            // panel2
            // 
            panel2.Controls.Add(入庫数合計);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(出庫数合計);
            panel2.Controls.Add(label1);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 431);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(1054, 55);
            panel2.TabIndex = 88;
            // 
            // 入庫数合計
            // 
            入庫数合計.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            入庫数合計.Location = new Point(871, 23);
            入庫数合計.Margin = new Padding(3, 2, 3, 2);
            入庫数合計.Name = "入庫数合計";
            入庫数合計.Size = new Size(88, 19);
            入庫数合計.TabIndex = 86;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(871, 5);
            label2.Name = "label2";
            label2.Size = new Size(65, 12);
            label2.TabIndex = 87;
            label2.Text = "入庫数合計";
            // 
            // 部品コード
            // 
            部品コード.BackColor = Color.White;
            部品コード.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            部品コード.FormattingEnabled = true;
            部品コード.ImeMode = ImeMode.Off;
            部品コード.Location = new Point(95, 37);
            部品コード.Name = "部品コード";
            部品コード.Size = new Size(139, 21);
            部品コード.TabIndex = 2;
            部品コード.DrawItem += 部品コード_DrawItem;
            部品コード.SelectedIndexChanged += 部品コード_SelectedIndexChanged;
            部品コード.TextChanged += 部品コード_TextChanged;
            部品コード.KeyDown += 部品コード_KeyDown;
            // 
            // 品名
            // 
            品名.BackColor = Color.White;
            品名.Enabled = false;
            品名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            品名.Location = new Point(240, 38);
            品名.Margin = new Padding(3, 2, 3, 2);
            品名.Name = "品名";
            品名.Size = new Size(466, 20);
            品名.TabIndex = 91;
            // 
            // 部品コード_ラベル
            // 
            部品コード_ラベル.AllowDrop = true;
            部品コード_ラベル.AutoEllipsis = true;
            部品コード_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            部品コード_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            部品コード_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            部品コード_ラベル.Location = new Point(3, 38);
            部品コード_ラベル.Margin = new Padding(0);
            部品コード_ラベル.Name = "部品コード_ラベル";
            部品コード_ラベル.Size = new Size(102, 20);
            部品コード_ラベル.TabIndex = 1;
            部品コード_ラベル.Text = "部品コード(&C)";
            部品コード_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 型番
            // 
            型番.BackColor = Color.White;
            型番.Enabled = false;
            型番.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            型番.Location = new Point(240, 62);
            型番.Margin = new Padding(3, 2, 3, 2);
            型番.Name = "型番";
            型番.Size = new Size(466, 20);
            型番.TabIndex = 92;
            // 
            // F_入出庫履歴
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1054, 486);
            Controls.Add(部品コード);
            Controls.Add(品名);
            Controls.Add(部品コード_ラベル);
            Controls.Add(型番);
            Controls.Add(panel2);
            Controls.Add(dataGridView1);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            Name = "F_入出庫履歴";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "入出庫履歴";
            FormClosing += F_入出庫履歴_FormClosing;
            Load += Form_Load;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel panel1;
        private Button button4;
        private Button button3;
        private Button コマンドメーカー;
        private Button コマンドメール;
        private Button コマンド更新;
        private Button コマンド入出力;
        private Button コマンド印刷;
        private Button コマンド全表示;
        private Button コマンド抽出;
        private Button コマンドF9;
        private Button コマンド終了;
        private TextBox 出庫数合計;
        private Label label1;
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

        //private newDataSetTableAdapters.V入出庫履歴TableAdapter v入出庫履歴TableAdapter;

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
        //private newDataSetTableAdapters.Q入出庫履歴TableAdapter q入出庫履歴TableAdapter;
        private Panel panel2;
        private Button コマンド初期化;
        private TextBox 入庫数合計;
        private Label label2;
        private DataGridViewTextBoxColumn 日付;
        private DataGridViewTextBoxColumn 購買コード;
        private DataGridViewTextBoxColumn シリーズ名;
        private DataGridViewTextBoxColumn 備考;
        private DataGridViewTextBoxColumn 入出庫コード;
        private DataGridViewTextBoxColumn 入庫数量;
        private DataGridViewTextBoxColumn 出庫数量;
        private ComboBox 部品コード;
        private TextBox 品名;
        private Label 部品コード_ラベル;
        private TextBox 型番;
        private ToolTip toolTip1;
    }
}
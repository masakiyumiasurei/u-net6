﻿namespace u_net
{
    partial class F_承認管理
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_承認管理));
            panel1 = new Panel();
            button4 = new Button();
            button3 = new Button();
            コマンド受注 = new Button();
            コマンド顧客 = new Button();
            コマンド更新 = new Button();
            コマンド確認 = new Button();
            コマンド取消 = new Button();
            コマンド削除 = new Button();
            コマンド修正 = new Button();
            コマンド新規 = new Button();
            コマンド複写 = new Button();
            コマンド終了 = new Button();
            表示件数 = new TextBox();
            表示件数ラベル = new Label();
            label2 = new Label();
            dataGridView1 = new DataGridView();
            newDataSet = new newDataSet();
            panel2 = new Panel();
            本日登録分ボタン = new Button();
            前日登録分ボタン = new Button();
            検索ボタン = new Button();
            履歴トグル = new Button();
            受注コードボタン = new Button();
            版数ボタン = new Button();
            登録日ボタン = new Button();
            承認依頼者名ボタン = new Button();
            出荷予定日ボタン = new Button();
            受注納期ボタン = new Button();
            顧客コードボタン = new Button();
            承認者名ボタン = new Button();
            承認日ボタン = new Button();
            コード = new TextBox();
            版数 = new TextBox();
            登録日 = new TextBox();
            承認依頼者名 = new TextBox();
            出荷予定日 = new TextBox();
            受注納期 = new TextBox();
            顧客コード = new TextBox();
            承認者名 = new TextBox();
            承認日 = new TextBox();
            承認者コード = new TextBox();
            無効日 = new TextBox();
            label4 = new Label();
            承認情報 = new ComboBox();
            検索コードラベル = new Label();
            検索コード = new TextBox();
            前ページボタン = new Button();
            次ページボタン = new Button();
            抽出表示ボタン = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)newDataSet).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaption;
            panel1.Controls.Add(button4);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(コマンド受注);
            panel1.Controls.Add(コマンド顧客);
            panel1.Controls.Add(コマンド更新);
            panel1.Controls.Add(コマンド確認);
            panel1.Controls.Add(コマンド取消);
            panel1.Controls.Add(コマンド削除);
            panel1.Controls.Add(コマンド修正);
            panel1.Controls.Add(コマンド新規);
            panel1.Controls.Add(コマンド複写);
            panel1.Controls.Add(コマンド終了);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(4, 2, 4, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(882, 32);
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
            button3.ForeColor = SystemColors.ControlText;
            button3.ImageAlign = ContentAlignment.BottomLeft;
            button3.Location = new Point(428, 4);
            button3.Margin = new Padding(0, 2, 0, 2);
            button3.Name = "button3";
            button3.Size = new Size(70, 22);
            button3.TabIndex = 7;
            button3.UseVisualStyleBackColor = true;
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
            コマンド受注.Text = "受注";
            コマンド受注.UseVisualStyleBackColor = true;
            コマンド受注.Click += コマンド商品_Click;
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
            コマンド顧客.Text = "顧客";
            コマンド顧客.UseVisualStyleBackColor = true;
            // 
            // コマンド更新
            // 
            コマンド更新.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド更新.ForeColor = Color.Blue;
            コマンド更新.ImageAlign = ContentAlignment.BottomLeft;
            コマンド更新.Location = new Point(647, 4);
            コマンド更新.Margin = new Padding(0, 2, 0, 2);
            コマンド更新.Name = "コマンド更新";
            コマンド更新.Size = new Size(70, 22);
            コマンド更新.TabIndex = 10;
            コマンド更新.Text = "更新";
            コマンド更新.UseVisualStyleBackColor = true;
            コマンド更新.Click += コマンド保守_Click;
            // 
            // コマンド確認
            // 
            コマンド確認.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド確認.ForeColor = SystemColors.ControlText;
            コマンド確認.ImageAlign = ContentAlignment.BottomLeft;
            コマンド確認.Location = new Point(715, 4);
            コマンド確認.Margin = new Padding(0, 2, 0, 2);
            コマンド確認.Name = "コマンド確認";
            コマンド確認.Size = new Size(70, 22);
            コマンド確認.TabIndex = 11;
            コマンド確認.Text = "確認";
            コマンド確認.UseVisualStyleBackColor = true;
            コマンド確認.Click += コマンド更新_Click;
            // 
            // コマンド取消
            // 
            コマンド取消.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド取消.ForeColor = SystemColors.ControlText;
            コマンド取消.ImageAlign = ContentAlignment.BottomLeft;
            コマンド取消.Location = new Point(207, 4);
            コマンド取消.Margin = new Padding(0, 2, 0, 2);
            コマンド取消.Name = "コマンド取消";
            コマンド取消.Size = new Size(70, 22);
            コマンド取消.TabIndex = 5;
            コマンド取消.Text = "取消";
            コマンド取消.UseVisualStyleBackColor = true;
            コマンド取消.Click += コマンド全表示_Click;
            // 
            // コマンド削除
            // 
            コマンド削除.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド削除.ForeColor = SystemColors.ControlText;
            コマンド削除.ImageAlign = ContentAlignment.BottomLeft;
            コマンド削除.Location = new Point(139, 4);
            コマンド削除.Margin = new Padding(0, 2, 0, 2);
            コマンド削除.Name = "コマンド削除";
            コマンド削除.Size = new Size(70, 22);
            コマンド削除.TabIndex = 4;
            コマンド削除.Text = "削除";
            コマンド削除.UseVisualStyleBackColor = true;
            コマンド削除.Click += コマンド初期化_Click;
            // 
            // コマンド修正
            // 
            コマンド修正.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド修正.ForeColor = SystemColors.ControlText;
            コマンド修正.ImageAlign = ContentAlignment.BottomLeft;
            コマンド修正.Location = new Point(71, 4);
            コマンド修正.Margin = new Padding(0, 2, 0, 2);
            コマンド修正.Name = "コマンド修正";
            コマンド修正.Size = new Size(70, 22);
            コマンド修正.TabIndex = 3;
            コマンド修正.Text = "修正";
            コマンド修正.UseVisualStyleBackColor = true;
            コマンド修正.Click += コマンド検索_Click;
            // 
            // コマンド新規
            // 
            コマンド新規.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド新規.ForeColor = SystemColors.ControlText;
            コマンド新規.ImageAlign = ContentAlignment.BottomLeft;
            コマンド新規.Location = new Point(3, 4);
            コマンド新規.Margin = new Padding(0, 2, 0, 2);
            コマンド新規.Name = "コマンド新規";
            コマンド新規.Size = new Size(70, 22);
            コマンド新規.TabIndex = 2;
            コマンド新規.Text = "新規";
            コマンド新規.UseVisualStyleBackColor = true;
            コマンド新規.Click += コマンド抽出_Click;
            // 
            // コマンド複写
            // 
            コマンド複写.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド複写.ImageAlign = ContentAlignment.BottomLeft;
            コマンド複写.Location = new Point(579, 4);
            コマンド複写.Margin = new Padding(0, 2, 0, 2);
            コマンド複写.Name = "コマンド複写";
            コマンド複写.Size = new Size(70, 22);
            コマンド複写.TabIndex = 9;
            コマンド複写.Text = "複写";
            コマンド複写.UseVisualStyleBackColor = true;
            コマンド複写.Click += コマンド入出力_Click;
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
            コマンド終了.UseVisualStyleBackColor = true;
            コマンド終了.Click += コマンド終了_Click;
            // 
            // 表示件数
            // 
            表示件数.Location = new Point(72, 4);
            表示件数.Margin = new Padding(3, 2, 3, 2);
            表示件数.Name = "表示件数";
            表示件数.Size = new Size(88, 23);
            表示件数.TabIndex = 0;
            // 
            // 表示件数ラベル
            // 
            表示件数ラベル.AutoSize = true;
            表示件数ラベル.Font = new Font("BIZ UDPゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            表示件数ラベル.Location = new Point(6, 6);
            表示件数ラベル.Name = "表示件数ラベル";
            表示件数ラベル.Size = new Size(63, 14);
            表示件数ラベル.TabIndex = 85;
            表示件数ラベル.Text = "表示件数";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("BIZ UDPゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(164, 6);
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
            dataGridView1.Location = new Point(0, 65);
            dataGridView1.Margin = new Padding(3, 2, 3, 2);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new Size(1054, 358);
            dataGridView1.TabIndex = 87;
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
            dataGridView1.CellPainting += DataGridView1_CellPainting;
            dataGridView1.Sorted += dataGridView1_Sorted;
            dataGridView1.KeyDown += dataGridView1_KeyDown;
            // 
            // newDataSet
            // 
            newDataSet.DataSetName = "newDataSet";
            newDataSet.Namespace = "http://tempuri.org/newDataSet.xsd";
            newDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // panel2
            // 
            panel2.Controls.Add(抽出表示ボタン);
            panel2.Controls.Add(次ページボタン);
            panel2.Controls.Add(前ページボタン);
            panel2.Controls.Add(表示件数);
            panel2.Controls.Add(表示件数ラベル);
            panel2.Controls.Add(label2);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 421);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(882, 28);
            panel2.TabIndex = 88;
            // 
            // 本日登録分ボタン
            // 
            本日登録分ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            本日登録分ボタン.Location = new Point(244, 38);
            本日登録分ボタン.Margin = new Padding(3, 2, 3, 2);
            本日登録分ボタン.Name = "本日登録分ボタン";
            本日登録分ボタン.Size = new Size(75, 22);
            本日登録分ボタン.TabIndex = 3;
            本日登録分ボタン.Text = "本日変更分";
            本日登録分ボタン.UseVisualStyleBackColor = true;
            // 
            // 前日登録分ボタン
            // 
            前日登録分ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            前日登録分ボタン.Location = new Point(317, 38);
            前日登録分ボタン.Margin = new Padding(3, 2, 3, 2);
            前日登録分ボタン.Name = "前日登録分ボタン";
            前日登録分ボタン.Size = new Size(75, 22);
            前日登録分ボタン.TabIndex = 4;
            前日登録分ボタン.Text = "前日変更分";
            前日登録分ボタン.UseVisualStyleBackColor = true;
            // 
            // 検索ボタン
            // 
            検索ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            検索ボタン.Location = new Point(565, 38);
            検索ボタン.Margin = new Padding(3, 2, 3, 2);
            検索ボタン.Name = "検索ボタン";
            検索ボタン.Size = new Size(47, 22);
            検索ボタン.TabIndex = 2;
            検索ボタン.Text = "検索";
            検索ボタン.UseVisualStyleBackColor = true;
            // 
            // 履歴トグル
            // 
            履歴トグル.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            履歴トグル.Location = new Point(609, 38);
            履歴トグル.Margin = new Padding(3, 2, 3, 2);
            履歴トグル.Name = "履歴トグル";
            履歴トグル.Size = new Size(75, 22);
            履歴トグル.TabIndex = 5;
            履歴トグル.Text = "履歴モード";
            履歴トグル.UseVisualStyleBackColor = true;
            // 
            // 受注コードボタン
            // 
            受注コードボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            受注コードボタン.Location = new Point(0, 66);
            受注コードボタン.Margin = new Padding(3, 2, 3, 2);
            受注コードボタン.Name = "受注コードボタン";
            受注コードボタン.Size = new Size(75, 26);
            受注コードボタン.TabIndex = 0;
            受注コードボタン.Text = "受注コード";
            受注コードボタン.UseVisualStyleBackColor = true;
            // 
            // 版数ボタン
            // 
            版数ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            版数ボタン.Location = new Point(73, 66);
            版数ボタン.Margin = new Padding(3, 2, 3, 2);
            版数ボタン.Name = "版数ボタン";
            版数ボタン.Size = new Size(36, 26);
            版数ボタン.TabIndex = 7;
            版数ボタン.Text = "版";
            版数ボタン.UseVisualStyleBackColor = true;
            // 
            // 登録日ボタン
            // 
            登録日ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            登録日ボタン.Location = new Point(107, 66);
            登録日ボタン.Margin = new Padding(3, 2, 3, 2);
            登録日ボタン.Name = "登録日ボタン";
            登録日ボタン.Size = new Size(76, 26);
            登録日ボタン.TabIndex = 1;
            登録日ボタン.Text = "登録日";
            登録日ボタン.UseVisualStyleBackColor = true;
            // 
            // 承認依頼者名ボタン
            // 
            承認依頼者名ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            承認依頼者名ボタン.Location = new Point(178, 66);
            承認依頼者名ボタン.Margin = new Padding(3, 2, 3, 2);
            承認依頼者名ボタン.Name = "承認依頼者名ボタン";
            承認依頼者名ボタン.Size = new Size(85, 26);
            承認依頼者名ボタン.TabIndex = 3;
            承認依頼者名ボタン.Text = "承認依頼者名";
            承認依頼者名ボタン.UseVisualStyleBackColor = true;
            // 
            // 出荷予定日ボタン
            // 
            出荷予定日ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            出荷予定日ボタン.Location = new Point(260, 66);
            出荷予定日ボタン.Margin = new Padding(3, 2, 3, 2);
            出荷予定日ボタン.Name = "出荷予定日ボタン";
            出荷予定日ボタン.Size = new Size(89, 26);
            出荷予定日ボタン.TabIndex = 8;
            出荷予定日ボタン.Text = "出荷予定日";
            出荷予定日ボタン.UseVisualStyleBackColor = true;
            // 
            // 受注納期ボタン
            // 
            受注納期ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            受注納期ボタン.Location = new Point(347, 66);
            受注納期ボタン.Margin = new Padding(3, 2, 3, 2);
            受注納期ボタン.Name = "受注納期ボタン";
            受注納期ボタン.Size = new Size(94, 26);
            受注納期ボタン.TabIndex = 2;
            受注納期ボタン.Text = "受注納期";
            受注納期ボタン.UseVisualStyleBackColor = true;
            // 
            // 顧客コードボタン
            // 
            顧客コードボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            顧客コードボタン.Location = new Point(439, 66);
            顧客コードボタン.Margin = new Padding(3, 2, 3, 2);
            顧客コードボタン.Name = "顧客コードボタン";
            顧客コードボタン.Size = new Size(88, 26);
            顧客コードボタン.TabIndex = 6;
            顧客コードボタン.Text = "顧客コード";
            顧客コードボタン.UseVisualStyleBackColor = true;
            // 
            // 承認者名ボタン
            // 
            承認者名ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            承認者名ボタン.Location = new Point(525, 66);
            承認者名ボタン.Margin = new Padding(3, 2, 3, 2);
            承認者名ボタン.Name = "承認者名ボタン";
            承認者名ボタン.Size = new Size(75, 26);
            承認者名ボタン.TabIndex = 5;
            承認者名ボタン.Text = "承認者名";
            承認者名ボタン.UseVisualStyleBackColor = true;
            // 
            // 承認日ボタン
            // 
            承認日ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            承認日ボタン.Location = new Point(598, 66);
            承認日ボタン.Margin = new Padding(3, 2, 3, 2);
            承認日ボタン.Name = "承認日ボタン";
            承認日ボタン.Size = new Size(75, 26);
            承認日ボタン.TabIndex = 4;
            承認日ボタン.Text = "承認日";
            承認日ボタン.UseVisualStyleBackColor = true;
            // 
            // コード
            // 
            コード.Enabled = false;
            コード.Font = new Font("BIZ UDPゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            コード.Location = new Point(0, 93);
            コード.Name = "コード";
            コード.Size = new Size(75, 21);
            コード.TabIndex = 0;
            // 
            // 版数
            // 
            版数.Enabled = false;
            版数.Font = new Font("BIZ UDPゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            版数.Location = new Point(73, 93);
            版数.Name = "版数";
            版数.Size = new Size(36, 21);
            版数.TabIndex = 7;
            // 
            // 登録日
            // 
            登録日.Enabled = false;
            登録日.Font = new Font("BIZ UDPゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            登録日.Location = new Point(108, 93);
            登録日.Name = "登録日";
            登録日.Size = new Size(74, 21);
            登録日.TabIndex = 1;
            // 
            // 承認依頼者名
            // 
            承認依頼者名.Enabled = false;
            承認依頼者名.Font = new Font("BIZ UDPゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            承認依頼者名.Location = new Point(178, 93);
            承認依頼者名.Name = "承認依頼者名";
            承認依頼者名.Size = new Size(84, 21);
            承認依頼者名.TabIndex = 6;
            // 
            // 出荷予定日
            // 
            出荷予定日.Enabled = false;
            出荷予定日.Font = new Font("BIZ UDPゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            出荷予定日.Location = new Point(260, 93);
            出荷予定日.Name = "出荷予定日";
            出荷予定日.Size = new Size(89, 21);
            出荷予定日.TabIndex = 3;
            // 
            // 受注納期
            // 
            受注納期.Enabled = false;
            受注納期.Font = new Font("BIZ UDPゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            受注納期.Location = new Point(347, 93);
            受注納期.Name = "受注納期";
            受注納期.Size = new Size(94, 21);
            受注納期.TabIndex = 2;
            // 
            // 顧客コード
            // 
            顧客コード.Enabled = false;
            顧客コード.Font = new Font("BIZ UDPゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            顧客コード.Location = new Point(439, 93);
            顧客コード.Name = "顧客コード";
            顧客コード.Size = new Size(88, 21);
            顧客コード.TabIndex = 5;
            // 
            // 承認者名
            // 
            承認者名.Enabled = false;
            承認者名.Font = new Font("BIZ UDPゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            承認者名.Location = new Point(525, 93);
            承認者名.Name = "承認者名";
            承認者名.Size = new Size(75, 21);
            承認者名.TabIndex = 8;
            // 
            // 承認日
            // 
            承認日.Enabled = false;
            承認日.Font = new Font("BIZ UDPゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            承認日.Location = new Point(598, 93);
            承認日.Name = "承認日";
            承認日.Size = new Size(75, 21);
            承認日.TabIndex = 10;
            // 
            // 承認者コード
            // 
            承認者コード.Enabled = false;
            承認者コード.Font = new Font("BIZ UDPゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            承認者コード.Location = new Point(673, 93);
            承認者コード.Name = "承認者コード";
            承認者コード.Size = new Size(18, 21);
            承認者コード.TabIndex = 9;
            // 
            // 無効日
            // 
            無効日.Enabled = false;
            無効日.Location = new Point(0, 93);
            無効日.Name = "無効日";
            無効日.Size = new Size(691, 23);
            無効日.TabIndex = 0;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(6, 42);
            label4.Name = "label4";
            label4.Size = new Size(53, 12);
            label4.TabIndex = 94;
            label4.Text = "承認情報";
            // 
            // 承認情報
            // 
            承認情報.FormattingEnabled = true;
            承認情報.Location = new Point(67, 38);
            承認情報.Name = "承認情報";
            承認情報.Size = new Size(168, 23);
            承認情報.TabIndex = 6;
            // 
            // 検索コードラベル
            // 
            検索コードラベル.AutoSize = true;
            検索コードラベル.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            検索コードラベル.Location = new Point(410, 42);
            検索コードラベル.Name = "検索コードラベル";
            検索コードラベル.Size = new Size(62, 12);
            検索コードラベル.TabIndex = 96;
            検索コードラベル.Text = "検索コード";
            // 
            // 検索コード
            // 
            検索コード.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            検索コード.Location = new Point(471, 39);
            検索コード.Margin = new Padding(3, 2, 3, 2);
            検索コード.Name = "検索コード";
            検索コード.Size = new Size(88, 19);
            検索コード.TabIndex = 1;
            // 
            // 前ページボタン
            // 
            前ページボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            前ページボタン.Location = new Point(528, 4);
            前ページボタン.Name = "前ページボタン";
            前ページボタン.Size = new Size(46, 23);
            前ページボタン.TabIndex = 87;
            前ページボタン.Text = "↑";
            前ページボタン.UseVisualStyleBackColor = true;
            // 
            // 次ページボタン
            // 
            次ページボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            次ページボタン.Location = new Point(580, 4);
            次ページボタン.Name = "次ページボタン";
            次ページボタン.Size = new Size(45, 23);
            次ページボタン.TabIndex = 88;
            次ページボタン.Text = "↓";
            次ページボタン.UseVisualStyleBackColor = true;
            // 
            // 抽出表示ボタン
            // 
            抽出表示ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            抽出表示ボタン.ForeColor = Color.Red;
            抽出表示ボタン.Location = new Point(631, 4);
            抽出表示ボタン.Name = "抽出表示ボタン";
            抽出表示ボタン.Size = new Size(75, 23);
            抽出表示ボタン.TabIndex = 89;
            抽出表示ボタン.Text = "抽出表示";
            抽出表示ボタン.UseVisualStyleBackColor = true;
            // 
            // F_承認管理
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(882, 449);
            Controls.Add(検索コード);
            Controls.Add(検索コードラベル);
            Controls.Add(承認情報);
            Controls.Add(label4);
            Controls.Add(無効日);
            Controls.Add(承認者コード);
            Controls.Add(承認日);
            Controls.Add(承認者名);
            Controls.Add(顧客コード);
            Controls.Add(受注納期);
            Controls.Add(出荷予定日);
            Controls.Add(承認依頼者名);
            Controls.Add(登録日);
            Controls.Add(版数);
            Controls.Add(コード);
            Controls.Add(承認日ボタン);
            Controls.Add(承認者名ボタン);
            Controls.Add(顧客コードボタン);
            Controls.Add(受注納期ボタン);
            Controls.Add(出荷予定日ボタン);
            Controls.Add(承認依頼者名ボタン);
            Controls.Add(登録日ボタン);
            Controls.Add(版数ボタン);
            Controls.Add(受注コードボタン);
            Controls.Add(履歴トグル);
            Controls.Add(検索ボタン);
            Controls.Add(前日登録分ボタン);
            Controls.Add(本日登録分ボタン);
            Controls.Add(panel2);
            Controls.Add(dataGridView1);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            Name = "F_承認管理";
            Text = "承認管理";
            Load += Form_Load;
            SizeChanged += Form_Resize;
            KeyDown += Form_KeyDown;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)newDataSet).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel panel1;
        private Button button4;
        private Button button3;
        private Button コマンド受注;
        private Button コマンド顧客;
        private Button コマンド更新;
        private Button コマンド確認;
        private Button コマンド取消;
        private Button コマンド削除;
        private Button コマンド新規;
        private Button コマンド複写;
        private Button コマンド終了;
        private TextBox 表示件数;
        private Label 表示件数ラベル;
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

        private newDataSetTableAdapters.V商品管理TableAdapter v商品管理TableAdapter;

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
        private newDataSet newDataSet;
        private Panel panel2;
        private Button コマンド修正;
        private Button 本日登録分ボタン;
        private Button 前日登録分ボタン;
        private Button 検索ボタン;
        private Button 履歴トグル;
        private Button 受注コードボタン;
        private Button 版数ボタン;
        private Button 登録日ボタン;
        private Button 承認依頼者名ボタン;
        private Button 出荷予定日ボタン;
        private Button 受注納期ボタン;
        private Button 顧客コードボタン;
        private Button 承認者名ボタン;
        private Button 承認日ボタン;
        private TextBox コード;
        private TextBox 版数;
        private TextBox 登録日;
        private TextBox 承認依頼者名;
        private TextBox 出荷予定日;
        private TextBox 受注納期;
        private TextBox 顧客コード;
        private TextBox 承認者名;
        private TextBox 承認日;
        private TextBox 承認者コード;
        private TextBox 無効日;
        private Label label4;
        private ComboBox 承認情報;
        private Label 検索コードラベル;
        private TextBox 検索コード;
        private Button 抽出表示ボタン;
        private Button 次ページボタン;
        private Button 前ページボタン;
    }
}
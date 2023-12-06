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
            button4 = new Button();
            コマンド生産 = new Button();
            コマンド受注 = new Button();
            コマンド顧客 = new Button();
            コマンド出力 = new Button();
            コマンド更新 = new Button();
            コマンド全表示 = new Button();
            コマンド初期化 = new Button();
            コマンド検索 = new Button();
            コマンド抽出 = new Button();
            コマンド入力 = new Button();
            コマンド終了 = new Button();
            表示件数 = new TextBox();
            表示件数ラベル = new Label();
            label2 = new Label();
            dataGridView1 = new DataGridView();
            newDataSet = new newDataSet();
            panel2 = new Panel();
            初期表示ボタン = new Button();
            本日受注分ボタン = new Button();
            前日受注分ボタン = new Button();
            前ページボタン = new Button();
            次ページボタン = new Button();
            履歴トグル = new Button();
            受注コードボタン = new Button();
            版数ボタン = new Button();
            受注日ボタン = new Button();
            出荷予定日ボタン = new Button();
            受注納期ボタン = new Button();
            注文番号ボタン = new Button();
            顧客名ボタン = new Button();
            自社担当者名ボタン = new Button();
            受注金額ボタン = new Button();
            進捗状況ボタン = new Button();
            受注コード = new TextBox();
            受注版数 = new TextBox();
            受注日 = new TextBox();
            出荷予定日 = new TextBox();
            受注納期 = new TextBox();
            注文番号 = new TextBox();
            顧客名 = new TextBox();
            自社担当者名 = new TextBox();
            税込受注金額 = new TextBox();
            確定 = new TextBox();
            承認 = new TextBox();
            出荷完了日 = new TextBox();
            完了承認者コード = new TextBox();
            無効日 = new TextBox();
            合計数量ラベル = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            合計金額ラベル = new Label();
            textBox3 = new TextBox();
            label3 = new Label();
            label1 = new Label();
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
            panel1.Controls.Add(コマンド生産);
            panel1.Controls.Add(コマンド受注);
            panel1.Controls.Add(コマンド顧客);
            panel1.Controls.Add(コマンド出力);
            panel1.Controls.Add(コマンド更新);
            panel1.Controls.Add(コマンド全表示);
            panel1.Controls.Add(コマンド初期化);
            panel1.Controls.Add(コマンド検索);
            panel1.Controls.Add(コマンド抽出);
            panel1.Controls.Add(コマンド入力);
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
            // コマンド生産
            // 
            コマンド生産.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド生産.ForeColor = SystemColors.ControlText;
            コマンド生産.ImageAlign = ContentAlignment.BottomLeft;
            コマンド生産.Location = new Point(428, 4);
            コマンド生産.Margin = new Padding(0, 2, 0, 2);
            コマンド生産.Name = "コマンド生産";
            コマンド生産.Size = new Size(70, 22);
            コマンド生産.TabIndex = 7;
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
            コマンド出力.Text = "出力";
            コマンド出力.UseVisualStyleBackColor = true;
            コマンド出力.Click += コマンド保守_Click;
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
            コマンド抽出.Text = "抽出";
            コマンド抽出.UseVisualStyleBackColor = true;
            コマンド抽出.Click += コマンド抽出_Click;
            // 
            // コマンド入力
            // 
            コマンド入力.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド入力.ImageAlign = ContentAlignment.BottomLeft;
            コマンド入力.Location = new Point(579, 4);
            コマンド入力.Margin = new Padding(0, 2, 0, 2);
            コマンド入力.Name = "コマンド入力";
            コマンド入力.Size = new Size(70, 22);
            コマンド入力.TabIndex = 9;
            コマンド入力.Text = "入力";
            コマンド入力.UseVisualStyleBackColor = true;
            コマンド入力.Click += コマンド入出力_Click;
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
            表示件数ラベル.Location = new Point(6, 6);
            表示件数ラベル.Name = "表示件数ラベル";
            表示件数ラベル.Size = new Size(55, 15);
            表示件数ラベル.TabIndex = 85;
            表示件数ラベル.Text = "表示件数";
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
            panel2.Controls.Add(label1);
            panel2.Controls.Add(textBox3);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(textBox2);
            panel2.Controls.Add(合計金額ラベル);
            panel2.Controls.Add(textBox1);
            panel2.Controls.Add(合計数量ラベル);
            panel2.Controls.Add(表示件数);
            panel2.Controls.Add(表示件数ラベル);
            panel2.Controls.Add(label2);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 421);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(1054, 28);
            panel2.TabIndex = 88;
            // 
            // 初期表示ボタン
            // 
            初期表示ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            初期表示ボタン.Location = new Point(0, 38);
            初期表示ボタン.Margin = new Padding(3, 2, 3, 2);
            初期表示ボタン.Name = "初期表示ボタン";
            初期表示ボタン.Size = new Size(75, 22);
            初期表示ボタン.TabIndex = 1;
            初期表示ボタン.Text = "受注残";
            初期表示ボタン.UseVisualStyleBackColor = true;
            // 
            // 本日受注分ボタン
            // 
            本日受注分ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            本日受注分ボタン.Location = new Point(71, 38);
            本日受注分ボタン.Margin = new Padding(3, 2, 3, 2);
            本日受注分ボタン.Name = "本日受注分ボタン";
            本日受注分ボタン.Size = new Size(75, 22);
            本日受注分ボタン.TabIndex = 2;
            本日受注分ボタン.Text = "本日受注分";
            本日受注分ボタン.UseVisualStyleBackColor = true;
            // 
            // 前日受注分ボタン
            // 
            前日受注分ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            前日受注分ボタン.Location = new Point(144, 38);
            前日受注分ボタン.Margin = new Padding(3, 2, 3, 2);
            前日受注分ボタン.Name = "前日受注分ボタン";
            前日受注分ボタン.Size = new Size(75, 22);
            前日受注分ボタン.TabIndex = 3;
            前日受注分ボタン.Text = "前日受注分";
            前日受注分ボタン.UseVisualStyleBackColor = true;
            // 
            // 前ページボタン
            // 
            前ページボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            前ページボタン.Location = new Point(258, 38);
            前ページボタン.Margin = new Padding(3, 2, 3, 2);
            前ページボタン.Name = "前ページボタン";
            前ページボタン.Size = new Size(47, 22);
            前ページボタン.TabIndex = 4;
            前ページボタン.Text = "↑";
            前ページボタン.UseVisualStyleBackColor = true;
            // 
            // 次ページボタン
            // 
            次ページボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            次ページボタン.Location = new Point(302, 38);
            次ページボタン.Margin = new Padding(3, 2, 3, 2);
            次ページボタン.Name = "次ページボタン";
            次ページボタン.Size = new Size(47, 22);
            次ページボタン.TabIndex = 5;
            次ページボタン.Text = "↓";
            次ページボタン.UseVisualStyleBackColor = true;
            // 
            // 履歴トグル
            // 
            履歴トグル.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            履歴トグル.Location = new Point(346, 38);
            履歴トグル.Margin = new Padding(3, 2, 3, 2);
            履歴トグル.Name = "履歴トグル";
            履歴トグル.Size = new Size(75, 22);
            履歴トグル.TabIndex = 6;
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
            版数ボタン.Size = new Size(44, 26);
            版数ボタン.TabIndex = 8;
            版数ボタン.Text = "版";
            版数ボタン.UseVisualStyleBackColor = true;
            // 
            // 受注日ボタン
            // 
            受注日ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            受注日ボタン.Location = new Point(115, 66);
            受注日ボタン.Margin = new Padding(3, 2, 3, 2);
            受注日ボタン.Name = "受注日ボタン";
            受注日ボタン.Size = new Size(75, 26);
            受注日ボタン.TabIndex = 1;
            受注日ボタン.Text = "受注日";
            受注日ボタン.UseVisualStyleBackColor = true;
            // 
            // 出荷予定日ボタン
            // 
            出荷予定日ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            出荷予定日ボタン.Location = new Point(188, 66);
            出荷予定日ボタン.Margin = new Padding(3, 2, 3, 2);
            出荷予定日ボタン.Name = "出荷予定日ボタン";
            出荷予定日ボタン.Size = new Size(75, 26);
            出荷予定日ボタン.TabIndex = 3;
            出荷予定日ボタン.Text = "出荷予定日";
            出荷予定日ボタン.UseVisualStyleBackColor = true;
            // 
            // 受注納期ボタン
            // 
            受注納期ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            受注納期ボタン.Location = new Point(260, 66);
            受注納期ボタン.Margin = new Padding(3, 2, 3, 2);
            受注納期ボタン.Name = "受注納期ボタン";
            受注納期ボタン.Size = new Size(89, 26);
            受注納期ボタン.TabIndex = 2;
            受注納期ボタン.Text = "受注納期";
            受注納期ボタン.UseVisualStyleBackColor = true;
            // 
            // 注文番号ボタン
            // 
            注文番号ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            注文番号ボタン.Location = new Point(347, 66);
            注文番号ボタン.Margin = new Padding(3, 2, 3, 2);
            注文番号ボタン.Name = "注文番号ボタン";
            注文番号ボタン.Size = new Size(139, 26);
            注文番号ボタン.TabIndex = 4;
            注文番号ボタン.Text = "注文番号";
            注文番号ボタン.UseVisualStyleBackColor = true;
            // 
            // 顧客名ボタン
            // 
            顧客名ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            顧客名ボタン.Location = new Point(484, 66);
            顧客名ボタン.Margin = new Padding(3, 2, 3, 2);
            顧客名ボタン.Name = "顧客名ボタン";
            顧客名ボタン.Size = new Size(251, 26);
            顧客名ボタン.TabIndex = 5;
            顧客名ボタン.Text = "顧　　客　　名";
            顧客名ボタン.UseVisualStyleBackColor = true;
            // 
            // 自社担当者名ボタン
            // 
            自社担当者名ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            自社担当者名ボタン.Location = new Point(733, 66);
            自社担当者名ボタン.Margin = new Padding(3, 2, 3, 2);
            自社担当者名ボタン.Name = "自社担当者名ボタン";
            自社担当者名ボタン.Size = new Size(75, 26);
            自社担当者名ボタン.TabIndex = 7;
            自社担当者名ボタン.Text = "担当者名";
            自社担当者名ボタン.UseVisualStyleBackColor = true;
            // 
            // 受注金額ボタン
            // 
            受注金額ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            受注金額ボタン.Location = new Point(806, 66);
            受注金額ボタン.Margin = new Padding(3, 2, 3, 2);
            受注金額ボタン.Name = "受注金額ボタン";
            受注金額ボタン.Size = new Size(75, 26);
            受注金額ボタン.TabIndex = 9;
            受注金額ボタン.Text = "受注金額";
            受注金額ボタン.UseVisualStyleBackColor = true;
            // 
            // 進捗状況ボタン
            // 
            進捗状況ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            進捗状況ボタン.Location = new Point(879, 66);
            進捗状況ボタン.Margin = new Padding(3, 2, 3, 2);
            進捗状況ボタン.Name = "進捗状況ボタン";
            進捗状況ボタン.Size = new Size(75, 26);
            進捗状況ボタン.TabIndex = 6;
            進捗状況ボタン.Text = "進捗";
            進捗状況ボタン.UseVisualStyleBackColor = true;
            // 
            // 受注コード
            // 
            受注コード.Enabled = false;
            受注コード.Font = new Font("BIZ UDPゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            受注コード.Location = new Point(0, 93);
            受注コード.Name = "受注コード";
            受注コード.Size = new Size(75, 21);
            受注コード.TabIndex = 0;
            // 
            // 受注版数
            // 
            受注版数.Enabled = false;
            受注版数.Font = new Font("BIZ UDPゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            受注版数.Location = new Point(73, 93);
            受注版数.Name = "受注版数";
            受注版数.Size = new Size(44, 21);
            受注版数.TabIndex = 9;
            // 
            // 受注日
            // 
            受注日.Enabled = false;
            受注日.Font = new Font("BIZ UDPゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            受注日.Location = new Point(115, 93);
            受注日.Name = "受注日";
            受注日.Size = new Size(75, 21);
            受注日.TabIndex = 1;
            // 
            // 出荷予定日
            // 
            出荷予定日.Enabled = false;
            出荷予定日.Font = new Font("BIZ UDPゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            出荷予定日.Location = new Point(188, 93);
            出荷予定日.Name = "出荷予定日";
            出荷予定日.Size = new Size(75, 21);
            出荷予定日.TabIndex = 3;
            // 
            // 受注納期
            // 
            受注納期.Enabled = false;
            受注納期.Font = new Font("BIZ UDPゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            受注納期.Location = new Point(260, 93);
            受注納期.Name = "受注納期";
            受注納期.Size = new Size(89, 21);
            受注納期.TabIndex = 2;
            // 
            // 注文番号
            // 
            注文番号.Enabled = false;
            注文番号.Font = new Font("BIZ UDPゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            注文番号.Location = new Point(347, 93);
            注文番号.Name = "注文番号";
            注文番号.Size = new Size(139, 21);
            注文番号.TabIndex = 4;
            // 
            // 顧客名
            // 
            顧客名.Enabled = false;
            顧客名.Font = new Font("BIZ UDPゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            顧客名.Location = new Point(484, 93);
            顧客名.Name = "顧客名";
            顧客名.Size = new Size(251, 21);
            顧客名.TabIndex = 5;
            // 
            // 自社担当者名
            // 
            自社担当者名.Enabled = false;
            自社担当者名.Font = new Font("BIZ UDPゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            自社担当者名.Location = new Point(733, 93);
            自社担当者名.Name = "自社担当者名";
            自社担当者名.Size = new Size(75, 21);
            自社担当者名.TabIndex = 8;
            // 
            // 税込受注金額
            // 
            税込受注金額.Enabled = false;
            税込受注金額.Font = new Font("BIZ UDPゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            税込受注金額.Location = new Point(806, 93);
            税込受注金額.Name = "税込受注金額";
            税込受注金額.Size = new Size(75, 21);
            税込受注金額.TabIndex = 13;
            // 
            // 確定
            // 
            確定.Enabled = false;
            確定.Font = new Font("BIZ UDPゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            確定.Location = new Point(879, 93);
            確定.Name = "確定";
            確定.Size = new Size(18, 21);
            確定.TabIndex = 14;
            // 
            // 承認
            // 
            承認.Enabled = false;
            承認.Font = new Font("BIZ UDPゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            承認.Location = new Point(898, 93);
            承認.Name = "承認";
            承認.Size = new Size(18, 21);
            承認.TabIndex = 10;
            // 
            // 出荷完了日
            // 
            出荷完了日.Enabled = false;
            出荷完了日.Font = new Font("BIZ UDPゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            出荷完了日.Location = new Point(917, 93);
            出荷完了日.Name = "出荷完了日";
            出荷完了日.Size = new Size(18, 21);
            出荷完了日.TabIndex = 11;
            // 
            // 完了承認者コード
            // 
            完了承認者コード.Enabled = false;
            完了承認者コード.Font = new Font("BIZ UDPゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            完了承認者コード.Location = new Point(936, 93);
            完了承認者コード.Name = "完了承認者コード";
            完了承認者コード.Size = new Size(18, 21);
            完了承認者コード.TabIndex = 12;
            // 
            // 無効日
            // 
            無効日.Enabled = false;
            無効日.Location = new Point(-9, 91);
            無効日.Name = "無効日";
            無効日.Size = new Size(954, 23);
            無効日.TabIndex = 0;
            // 
            // 合計数量ラベル
            // 
            合計数量ラベル.AutoSize = true;
            合計数量ラベル.Location = new Point(207, 6);
            合計数量ラベル.Name = "合計数量ラベル";
            合計数量ラベル.Size = new Size(55, 15);
            合計数量ラベル.TabIndex = 87;
            合計数量ラベル.Text = "合計数量";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(268, 4);
            textBox1.Margin = new Padding(3, 2, 3, 2);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(88, 23);
            textBox1.TabIndex = 1;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(436, 4);
            textBox2.Margin = new Padding(3, 2, 3, 2);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(88, 23);
            textBox2.TabIndex = 2;
            // 
            // 合計金額ラベル
            // 
            合計金額ラベル.AutoSize = true;
            合計金額ラベル.Location = new Point(375, 6);
            合計金額ラベル.Name = "合計金額ラベル";
            合計金額ラベル.Size = new Size(55, 15);
            合計金額ラベル.TabIndex = 89;
            合計金額ラベル.Text = "合計金額";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(637, 4);
            textBox3.Margin = new Padding(3, 2, 3, 2);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(88, 23);
            textBox3.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(552, 6);
            label3.Name = "label3";
            label3.Size = new Size(79, 15);
            label3.TabIndex = 91;
            label3.Text = "税込合計金額";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(751, 6);
            label1.Name = "label1";
            label1.Size = new Size(216, 15);
            label1.TabIndex = 93;
            label1.Text = "※合計金額に未承認データは含まれません。";
            // 
            // F_受注管理
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1054, 449);
            Controls.Add(無効日);
            Controls.Add(完了承認者コード);
            Controls.Add(出荷完了日);
            Controls.Add(承認);
            Controls.Add(確定);
            Controls.Add(税込受注金額);
            Controls.Add(自社担当者名);
            Controls.Add(顧客名);
            Controls.Add(注文番号);
            Controls.Add(受注納期);
            Controls.Add(出荷予定日);
            Controls.Add(受注日);
            Controls.Add(受注版数);
            Controls.Add(受注コード);
            Controls.Add(進捗状況ボタン);
            Controls.Add(受注金額ボタン);
            Controls.Add(自社担当者名ボタン);
            Controls.Add(顧客名ボタン);
            Controls.Add(注文番号ボタン);
            Controls.Add(受注納期ボタン);
            Controls.Add(出荷予定日ボタン);
            Controls.Add(受注日ボタン);
            Controls.Add(版数ボタン);
            Controls.Add(受注コードボタン);
            Controls.Add(履歴トグル);
            Controls.Add(次ページボタン);
            Controls.Add(前ページボタン);
            Controls.Add(前日受注分ボタン);
            Controls.Add(本日受注分ボタン);
            Controls.Add(初期表示ボタン);
            Controls.Add(panel2);
            Controls.Add(dataGridView1);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            Name = "F_受注管理";
            Text = "受注管理";
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
        private Button コマンド生産;
        private Button コマンド受注;
        private Button コマンド顧客;
        private Button コマンド出力;
        private Button コマンド更新;
        private Button コマンド全表示;
        private Button コマンド初期化;
        private Button コマンド抽出;
        private Button コマンド入力;
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
        private Button コマンド検索;
        private Button 初期表示ボタン;
        private Button 本日受注分ボタン;
        private Button 前日受注分ボタン;
        private Button 前ページボタン;
        private Button 次ページボタン;
        private Button 履歴トグル;
        private Button 受注コードボタン;
        private Button 版数ボタン;
        private Button 受注日ボタン;
        private Button 出荷予定日ボタン;
        private Button 受注納期ボタン;
        private Button 注文番号ボタン;
        private Button 顧客名ボタン;
        private Button 自社担当者名ボタン;
        private Button 受注金額ボタン;
        private Button 進捗状況ボタン;
        private TextBox 受注コード;
        private TextBox 受注版数;
        private TextBox 受注日;
        private TextBox 出荷予定日;
        private TextBox 受注納期;
        private TextBox 注文番号;
        private TextBox 顧客名;
        private TextBox 自社担当者名;
        private TextBox 税込受注金額;
        private TextBox 確定;
        private TextBox 承認;
        private TextBox 出荷完了日;
        private TextBox 完了承認者コード;
        private TextBox 無効日;
        private Label label1;
        private TextBox textBox3;
        private Label label3;
        private TextBox textBox2;
        private Label 合計金額ラベル;
        private TextBox textBox1;
        private Label 合計数量ラベル;
    }
}
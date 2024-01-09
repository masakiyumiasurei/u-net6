namespace u_net
{
    partial class F_出荷管理旧
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_出荷管理旧));
            panel1 = new Panel();
            コマンド印刷 = new Button();
            コマンド現品票全印刷 = new Button();
            コマンド現品票印刷 = new Button();
            コマンド受注 = new Button();
            コマンド現品票 = new Button();
            コマンド出力 = new Button();
            コマンドF11 = new Button();
            コマンドF4 = new Button();
            コマンド更新 = new Button();
            コマンド検索 = new Button();
            コマンド抽出 = new Button();
            コマンド終了 = new Button();
            有効件数 = new TextBox();
            表示件数ラベル = new Label();
            label2 = new Label();
            dataGridView1 = new DataGridView();
            newDataSet = new newDataSet();
            panel2 = new Panel();
            合計金額 = new TextBox();
            合計金額ラベル = new Label();
            合計数量 = new TextBox();
            合計数量ラベル = new Label();
            初期表示ボタン = new Button();
            本日出荷分ボタン = new Button();
            前日出荷分ボタン = new Button();
            履歴トグル = new CheckBox();
            toolTip1 = new ToolTip(components);
            最終製品検査記録プレビューボタン = new Button();
            翌日出荷分ボタン = new Button();
            注釈_ラベル = new Label();
            最終製品検査記録印刷ボタン = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)newDataSet).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaption;
            panel1.Controls.Add(コマンド印刷);
            panel1.Controls.Add(コマンド現品票全印刷);
            panel1.Controls.Add(コマンド現品票印刷);
            panel1.Controls.Add(コマンド受注);
            panel1.Controls.Add(コマンド現品票);
            panel1.Controls.Add(コマンド出力);
            panel1.Controls.Add(コマンドF11);
            panel1.Controls.Add(コマンドF4);
            panel1.Controls.Add(コマンド更新);
            panel1.Controls.Add(コマンド検索);
            panel1.Controls.Add(コマンド抽出);
            panel1.Controls.Add(コマンド終了);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(4, 2, 4, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(1035, 32);
            panel1.TabIndex = 83;
            // 
            // コマンド印刷
            // 
            コマンド印刷.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド印刷.ForeColor = Color.Blue;
            コマンド印刷.ImageAlign = ContentAlignment.BottomLeft;
            コマンド印刷.Location = new Point(578, 4);
            コマンド印刷.Margin = new Padding(0, 2, 0, 2);
            コマンド印刷.Name = "コマンド印刷";
            コマンド印刷.Size = new Size(70, 22);
            コマンド印刷.TabIndex = 13;
            コマンド印刷.TabStop = false;
            コマンド印刷.Text = "印刷PV";
            toolTip1.SetToolTip(コマンド印刷, "出荷管理表の印刷プレビュー");
            コマンド印刷.UseVisualStyleBackColor = true;
            コマンド印刷.Click += コマンド印刷_Click;
            // 
            // コマンド現品票全印刷
            // 
            コマンド現品票全印刷.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド現品票全印刷.ForeColor = Color.Blue;
            コマンド現品票全印刷.ImageAlign = ContentAlignment.BottomLeft;
            コマンド現品票全印刷.Location = new Point(498, 4);
            コマンド現品票全印刷.Margin = new Padding(0, 2, 0, 2);
            コマンド現品票全印刷.Name = "コマンド現品票全印刷";
            コマンド現品票全印刷.Size = new Size(70, 22);
            コマンド現品票全印刷.TabIndex = 12;
            コマンド現品票全印刷.TabStop = false;
            コマンド現品票全印刷.Text = "現品全印";
            toolTip1.SetToolTip(コマンド現品票全印刷, "全現品票の印刷");
            コマンド現品票全印刷.UseVisualStyleBackColor = true;
            コマンド現品票全印刷.Click += コマンド現品票全印刷_Click;
            // 
            // コマンド現品票印刷
            // 
            コマンド現品票印刷.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド現品票印刷.ForeColor = Color.Blue;
            コマンド現品票印刷.ImageAlign = ContentAlignment.BottomLeft;
            コマンド現品票印刷.Location = new Point(429, 4);
            コマンド現品票印刷.Margin = new Padding(0, 2, 0, 2);
            コマンド現品票印刷.Name = "コマンド現品票印刷";
            コマンド現品票印刷.Size = new Size(70, 22);
            コマンド現品票印刷.TabIndex = 11;
            コマンド現品票印刷.TabStop = false;
            コマンド現品票印刷.Text = "現品票P";
            toolTip1.SetToolTip(コマンド現品票印刷, "出荷現品票の直接印刷");
            コマンド現品票印刷.UseVisualStyleBackColor = true;
            コマンド現品票印刷.Click += コマンド現品票印刷_Click;
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
            コマンド受注.Text = "受注参照";
            toolTip1.SetToolTip(コマンド受注, "受注データの参照");
            コマンド受注.UseVisualStyleBackColor = true;
            コマンド受注.Click += コマンド受注_Click;
            // 
            // コマンド現品票
            // 
            コマンド現品票.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド現品票.ForeColor = Color.Blue;
            コマンド現品票.ImageAlign = ContentAlignment.BottomLeft;
            コマンド現品票.Location = new Point(360, 4);
            コマンド現品票.Margin = new Padding(0, 2, 0, 2);
            コマンド現品票.Name = "コマンド現品票";
            コマンド現品票.Size = new Size(70, 22);
            コマンド現品票.TabIndex = 6;
            コマンド現品票.TabStop = false;
            コマンド現品票.Text = "現品票";
            toolTip1.SetToolTip(コマンド現品票, "出荷現品票を開く");
            コマンド現品票.UseVisualStyleBackColor = true;
            コマンド現品票.Click += コマンド現品票_Click;
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
            toolTip1.SetToolTip(コマンド出力, "表示データの出力");
            コマンド出力.UseVisualStyleBackColor = true;
            コマンド出力.Click += コマンド出力_Click;
            // 
            // コマンドF11
            // 
            コマンドF11.Enabled = false;
            コマンドF11.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンドF11.ForeColor = Color.Blue;
            コマンドF11.ImageAlign = ContentAlignment.BottomLeft;
            コマンドF11.Location = new Point(715, 4);
            コマンドF11.Margin = new Padding(0, 2, 0, 2);
            コマンドF11.Name = "コマンドF11";
            コマンドF11.Size = new Size(70, 22);
            コマンドF11.TabIndex = 10;
            コマンドF11.TabStop = false;
            コマンドF11.UseVisualStyleBackColor = true;
            // 
            // コマンドF4
            // 
            コマンドF4.Enabled = false;
            コマンドF4.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンドF4.ForeColor = Color.Blue;
            コマンドF4.ImageAlign = ContentAlignment.BottomLeft;
            コマンドF4.Location = new Point(207, 4);
            コマンドF4.Margin = new Padding(0, 2, 0, 2);
            コマンドF4.Name = "コマンドF4";
            コマンドF4.Size = new Size(70, 22);
            コマンドF4.TabIndex = 5;
            コマンドF4.TabStop = false;
            コマンドF4.UseVisualStyleBackColor = true;
            // 
            // コマンド更新
            // 
            コマンド更新.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド更新.ForeColor = Color.Blue;
            コマンド更新.ImageAlign = ContentAlignment.BottomLeft;
            コマンド更新.Location = new Point(139, 4);
            コマンド更新.Margin = new Padding(0, 2, 0, 2);
            コマンド更新.Name = "コマンド更新";
            コマンド更新.Size = new Size(70, 22);
            コマンド更新.TabIndex = 4;
            コマンド更新.TabStop = false;
            コマンド更新.Text = "更新";
            toolTip1.SetToolTip(コマンド更新, "最新の情報に更新");
            コマンド更新.UseVisualStyleBackColor = true;
            コマンド更新.Click += コマンド更新_Click;
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
            toolTip1.SetToolTip(コマンド抽出, "抽出条件設定");
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
            toolTip1.SetToolTip(コマンド終了, "終了");
            コマンド終了.UseVisualStyleBackColor = true;
            コマンド終了.Click += コマンド終了_Click;
            // 
            // 有効件数
            // 
            有効件数.Location = new Point(72, 2);
            有効件数.Margin = new Padding(3, 2, 3, 2);
            有効件数.Name = "有効件数";
            有効件数.Size = new Size(88, 23);
            有効件数.TabIndex = 0;
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
            dataGridView1.Location = new Point(6, 65);
            dataGridView1.Margin = new Padding(3, 2, 3, 2);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new Size(1005, 422);
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
            panel2.Controls.Add(合計金額);
            panel2.Controls.Add(合計金額ラベル);
            panel2.Controls.Add(合計数量);
            panel2.Controls.Add(合計数量ラベル);
            panel2.Controls.Add(有効件数);
            panel2.Controls.Add(表示件数ラベル);
            panel2.Controls.Add(label2);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 487);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(1035, 28);
            panel2.TabIndex = 88;
            // 
            // 合計金額
            // 
            合計金額.Location = new Point(436, 2);
            合計金額.Margin = new Padding(3, 2, 3, 2);
            合計金額.Name = "合計金額";
            合計金額.Size = new Size(88, 23);
            合計金額.TabIndex = 2;
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
            // 合計数量
            // 
            合計数量.Location = new Point(268, 2);
            合計数量.Margin = new Padding(3, 2, 3, 2);
            合計数量.Name = "合計数量";
            合計数量.Size = new Size(88, 23);
            合計数量.TabIndex = 1;
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
            // 初期表示ボタン
            // 
            初期表示ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            初期表示ボタン.Location = new Point(6, 39);
            初期表示ボタン.Margin = new Padding(3, 2, 3, 2);
            初期表示ボタン.Name = "初期表示ボタン";
            初期表示ボタン.Size = new Size(75, 22);
            初期表示ボタン.TabIndex = 1;
            初期表示ボタン.TabStop = false;
            初期表示ボタン.Text = "出荷残";
            初期表示ボタン.UseVisualStyleBackColor = true;
            初期表示ボタン.Click += 初期表示ボタン_Click;
            // 
            // 本日出荷分ボタン
            // 
            本日出荷分ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            本日出荷分ボタン.Location = new Point(156, 39);
            本日出荷分ボタン.Margin = new Padding(3, 2, 3, 2);
            本日出荷分ボタン.Name = "本日出荷分ボタン";
            本日出荷分ボタン.Size = new Size(75, 22);
            本日出荷分ボタン.TabIndex = 2;
            本日出荷分ボタン.TabStop = false;
            本日出荷分ボタン.Text = "本日出荷分";
            本日出荷分ボタン.UseVisualStyleBackColor = true;
            本日出荷分ボタン.Click += 本日出荷分ボタン_Click;
            // 
            // 前日出荷分ボタン
            // 
            前日出荷分ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            前日出荷分ボタン.Location = new Point(81, 39);
            前日出荷分ボタン.Margin = new Padding(3, 2, 3, 2);
            前日出荷分ボタン.Name = "前日出荷分ボタン";
            前日出荷分ボタン.Size = new Size(75, 22);
            前日出荷分ボタン.TabIndex = 3;
            前日出荷分ボタン.TabStop = false;
            前日出荷分ボタン.Text = "前日出荷分";
            前日出荷分ボタン.UseVisualStyleBackColor = true;
            前日出荷分ボタン.Click += 前日出荷分ボタン_Click;
            // 
            // 履歴トグル
            // 
            履歴トグル.Appearance = Appearance.Button;
            履歴トグル.AutoSize = true;
            履歴トグル.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            履歴トグル.Location = new Point(336, 39);
            履歴トグル.Name = "履歴トグル";
            履歴トグル.Size = new Size(72, 22);
            履歴トグル.TabIndex = 10015;
            履歴トグル.Text = "履歴モード";
            toolTip1.SetToolTip(履歴トグル, "履歴モードのON/OFF");
            履歴トグル.UseVisualStyleBackColor = true;
            履歴トグル.CheckedChanged += 履歴トグル_CheckedChanged;
            // 
            // 最終製品検査記録プレビューボタン
            // 
            最終製品検査記録プレビューボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            最終製品検査記録プレビューボタン.Image = (Image)resources.GetObject("最終製品検査記録プレビューボタン.Image");
            最終製品検査記録プレビューボタン.Location = new Point(783, 39);
            最終製品検査記録プレビューボタン.Margin = new Padding(3, 2, 3, 2);
            最終製品検査記録プレビューボタン.Name = "最終製品検査記録プレビューボタン";
            最終製品検査記録プレビューボタン.Size = new Size(34, 22);
            最終製品検査記録プレビューボタン.TabIndex = 10018;
            最終製品検査記録プレビューボタン.TabStop = false;
            toolTip1.SetToolTip(最終製品検査記録プレビューボタン, "最終製品検査・試験記録のプレビュー");
            最終製品検査記録プレビューボタン.UseVisualStyleBackColor = true;
            最終製品検査記録プレビューボタン.Click += 最終製品検査記録プレビューボタン_Click;
            // 
            // 翌日出荷分ボタン
            // 
            翌日出荷分ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            翌日出荷分ボタン.Location = new Point(231, 39);
            翌日出荷分ボタン.Margin = new Padding(3, 2, 3, 2);
            翌日出荷分ボタン.Name = "翌日出荷分ボタン";
            翌日出荷分ボタン.Size = new Size(75, 22);
            翌日出荷分ボタン.TabIndex = 10016;
            翌日出荷分ボタン.TabStop = false;
            翌日出荷分ボタン.Text = "翌日出荷分";
            翌日出荷分ボタン.UseVisualStyleBackColor = true;
            翌日出荷分ボタン.Click += 翌日出荷分ボタン_Click;
            // 
            // 注釈_ラベル
            // 
            注釈_ラベル.Font = new Font("Yu Gothic UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            注釈_ラベル.ForeColor = Color.Red;
            注釈_ラベル.Location = new Point(414, 39);
            注釈_ラベル.Name = "注釈_ラベル";
            注釈_ラベル.Size = new Size(391, 22);
            注釈_ラベル.TabIndex = 10017;
            注釈_ラベル.Text = "受注日が2010年2月22日以降の出荷データは[出荷管理]を参照してください！！";
            注釈_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 最終製品検査記録印刷ボタン
            // 
            最終製品検査記録印刷ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            最終製品検査記録印刷ボタン.Image = (Image)resources.GetObject("最終製品検査記録印刷ボタン.Image");
            最終製品検査記録印刷ボタン.Location = new Point(819, 39);
            最終製品検査記録印刷ボタン.Margin = new Padding(3, 2, 3, 2);
            最終製品検査記録印刷ボタン.Name = "最終製品検査記録印刷ボタン";
            最終製品検査記録印刷ボタン.Size = new Size(34, 22);
            最終製品検査記録印刷ボタン.TabIndex = 10019;
            最終製品検査記録印刷ボタン.TabStop = false;
            最終製品検査記録印刷ボタン.UseVisualStyleBackColor = true;
            最終製品検査記録印刷ボタン.Click += 最終製品検査記録印刷ボタン_Click;
            // 
            // F_出荷管理旧
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1035, 515);
            Controls.Add(最終製品検査記録印刷ボタン);
            Controls.Add(最終製品検査記録プレビューボタン);
            Controls.Add(注釈_ラベル);
            Controls.Add(翌日出荷分ボタン);
            Controls.Add(履歴トグル);
            Controls.Add(前日出荷分ボタン);
            Controls.Add(本日出荷分ボタン);
            Controls.Add(初期表示ボタン);
            Controls.Add(panel2);
            Controls.Add(dataGridView1);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            Name = "F_出荷管理旧";
            Text = "受注管理";
            FormClosed += Form_Unload;
            Load += Form_Load;
            SizeChanged += Form_Resize;
            Resize += Form_Resize;
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
        private Button コマンド現品票印刷;
        private Button コマンド受注;
        private Button コマンド現品票;
        private Button コマンド出力;
        private Button コマンドF11;
        private Button コマンドF4;
        private Button コマンド更新;
        private Button コマンド抽出;
        private Button コマンド終了;
        private TextBox 有効件数;
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
        private Button 本日出荷分ボタン;
        private Button 前日出荷分ボタン;
        private TextBox 合計金額;
        private Label 合計金額ラベル;
        private TextBox 合計数量;
        private Label 合計数量ラベル;
        private Button コマンド印刷;
        private Button コマンド現品票全印刷;
        private CheckBox 履歴トグル;
        private ToolTip toolTip1;
        private Button 翌日出荷分ボタン;
        private Label 注釈_ラベル;
        private Button 最終製品検査記録プレビューボタン;
        private Button 最終製品検査記録印刷ボタン;
    }
}
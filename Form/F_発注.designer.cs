
namespace u_net
{
    partial class F_発注
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_発注));
            コマンド終了 = new Button();
            コマンド登録 = new Button();
            panel1 = new Panel();
            コマンド新規 = new Button();
            コマンド購買 = new Button();
            コマンド部品 = new Button();
            コマンド送信 = new Button();
            コマンド発注書 = new Button();
            コマンド確定 = new Button();
            コマンド承認 = new Button();
            コマンド削除 = new Button();
            コマンド複写 = new Button();
            コマンド読込 = new Button();
            発注コードラベル = new Label();
            版数_ラベル = new Label();
            発注コード = new ComboBox();
            発注日選択ボタン = new Button();
            改版ボタン = new Button();
            購買コード = new TextBox();
            発注版数 = new ComboBox();
            購買コード_ラベル = new Label();
            シリーズ名 = new TextBox();
            シリーズ名_ラベル = new Label();
            ロット番号_ラベル = new Label();
            ロット番号 = new TextBox();
            発注日_ラベル = new Label();
            発注日 = new TextBox();
            発注者_ラベル = new Label();
            発注者コード = new ComboBox();
            発注者名 = new TextBox();
            仕入先コード = new TextBox();
            仕入先_ラベル = new Label();
            仕入先選択ボタン = new Button();
            仕入先名 = new TextBox();
            仕入先担当者名 = new TextBox();
            担当者名_ラベル = new Label();
            摘要_ラベル = new Label();
            摘要 = new TextBox();
            備考 = new TextBox();
            備考_ラベル = new Label();
            在庫管理_ラベル = new Label();
            注釈1_ラベル = new Label();
            注釈2_ラベル = new Label();
            振込不要_ラベル = new Label();
            登録日時_ラベル = new Label();
            登録日時 = new TextBox();
            仕入先電話番号__ラベル = new Label();
            仕入先電話番号 = new TextBox();
            仕入先ファックス番号 = new TextBox();
            仕入先ファックス番号_ラベル = new Label();
            在庫管理 = new CheckBox();
            NoCredit = new CheckBox();
            登録者_ラベル = new Label();
            登録者コード = new TextBox();
            登録者名 = new TextBox();
            確定_ラベル = new Label();
            確定 = new TextBox();
            承認 = new TextBox();
            承認_ラベル = new Label();
            送信 = new TextBox();
            送信_ラベル = new Label();
            入庫状況表示 = new TextBox();
            入庫_ラベル = new Label();
            削除 = new TextBox();
            削除_ラベル = new Label();
            注釈3_ラベル = new Label();
            SupplierSendMethodCode = new TextBox();
            SupplierSendMethodName = new TextBox();
            確定日時 = new TextBox();
            承認日時 = new TextBox();
            承認者コード = new TextBox();
            承認者名 = new TextBox();
            テストコマンド = new Button();
            無効日時 = new TextBox();
            無効者コード = new TextBox();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            入庫状況 = new TextBox();
            発注明細1 = new MultiRowDesigner.発注明細();
            panel1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // コマンド終了
            // 
            コマンド終了.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド終了.ForeColor = Color.Red;
            コマンド終了.Location = new Point(795, 5);
            コマンド終了.Margin = new Padding(3, 2, 3, 2);
            コマンド終了.Name = "コマンド終了";
            コマンド終了.Size = new Size(70, 20);
            コマンド終了.TabIndex = 1021;
            コマンド終了.TabStop = false;
            コマンド終了.Text = "終了";
            コマンド終了.UseVisualStyleBackColor = true;
            コマンド終了.Click += コマンド終了_Click;
            // 
            // コマンド登録
            // 
            コマンド登録.Enabled = false;
            コマンド登録.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド登録.ForeColor = Color.Blue;
            コマンド登録.ImageAlign = ContentAlignment.BottomLeft;
            コマンド登録.Location = new Point(725, 5);
            コマンド登録.Margin = new Padding(3, 2, 3, 2);
            コマンド登録.Name = "コマンド登録";
            コマンド登録.Size = new Size(70, 20);
            コマンド登録.TabIndex = 1020;
            コマンド登録.TabStop = false;
            コマンド登録.Text = "登録";
            コマンド登録.UseVisualStyleBackColor = true;
            コマンド登録.Click += コマンド登録_Click;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaption;
            panel1.Controls.Add(コマンド新規);
            panel1.Location = new Point(0, 1);
            panel1.Margin = new Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(1036, 42);
            panel1.TabIndex = 81;
            // 
            // コマンド新規
            // 
            コマンド新規.BackColor = SystemColors.Control;
            コマンド新規.Enabled = false;
            コマンド新規.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド新規.ForeColor = Color.Blue;
            コマンド新規.ImageAlign = ContentAlignment.BottomLeft;
            コマンド新規.Location = new Point(5, 5);
            コマンド新規.Margin = new Padding(3, 2, 3, 2);
            コマンド新規.Name = "コマンド新規";
            コマンド新規.Size = new Size(70, 20);
            コマンド新規.TabIndex = 1002;
            コマンド新規.TabStop = false;
            コマンド新規.Text = "新規";
            コマンド新規.UseVisualStyleBackColor = true;
            コマンド新規.Click += コマンド新規_Click;
            // 
            // コマンド購買
            // 
            コマンド購買.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド購買.ForeColor = Color.Blue;
            コマンド購買.ImageAlign = ContentAlignment.BottomLeft;
            コマンド購買.Location = new Point(510, 5);
            コマンド購買.Margin = new Padding(3, 2, 3, 2);
            コマンド購買.Name = "コマンド購買";
            コマンド購買.Size = new Size(70, 20);
            コマンド購買.TabIndex = 1011;
            コマンド購買.TabStop = false;
            コマンド購買.Text = "購買参照";
            コマンド購買.UseVisualStyleBackColor = true;
            コマンド購買.Click += コマンド購買_Click;
            // 
            // コマンド部品
            // 
            コマンド部品.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド部品.ForeColor = Color.Blue;
            コマンド部品.ImageAlign = ContentAlignment.BottomLeft;
            コマンド部品.Location = new Point(440, 5);
            コマンド部品.Margin = new Padding(3, 2, 3, 2);
            コマンド部品.Name = "コマンド部品";
            コマンド部品.Size = new Size(70, 20);
            コマンド部品.TabIndex = 1010;
            コマンド部品.TabStop = false;
            コマンド部品.Text = "部品参照";
            コマンド部品.UseVisualStyleBackColor = true;
            コマンド部品.Click += コマンド部品_Click;
            // 
            // コマンド送信
            // 
            コマンド送信.Enabled = false;
            コマンド送信.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド送信.ForeColor = Color.Blue;
            コマンド送信.ImageAlign = ContentAlignment.BottomLeft;
            コマンド送信.Location = new Point(370, 5);
            コマンド送信.Margin = new Padding(3, 2, 3, 2);
            コマンド送信.Name = "コマンド送信";
            コマンド送信.Size = new Size(70, 20);
            コマンド送信.TabIndex = 1009;
            コマンド送信.TabStop = false;
            コマンド送信.Text = "送信";
            コマンド送信.UseVisualStyleBackColor = true;
            コマンド送信.Click += コマンド送信_Click;
            // 
            // コマンド発注書
            // 
            コマンド発注書.Enabled = false;
            コマンド発注書.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド発注書.ForeColor = Color.Blue;
            コマンド発注書.ImageAlign = ContentAlignment.BottomLeft;
            コマンド発注書.Location = new Point(300, 5);
            コマンド発注書.Margin = new Padding(3, 2, 3, 2);
            コマンド発注書.Name = "コマンド発注書";
            コマンド発注書.Size = new Size(70, 20);
            コマンド発注書.TabIndex = 1008;
            コマンド発注書.TabStop = false;
            コマンド発注書.Text = "発注書";
            コマンド発注書.UseVisualStyleBackColor = true;
            コマンド発注書.Click += コマンド発注書_Click;
            // 
            // コマンド確定
            // 
            コマンド確定.Enabled = false;
            コマンド確定.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド確定.ForeColor = Color.Blue;
            コマンド確定.ImageAlign = ContentAlignment.BottomLeft;
            コマンド確定.Location = new Point(655, 5);
            コマンド確定.Margin = new Padding(3, 2, 3, 2);
            コマンド確定.Name = "コマンド確定";
            コマンド確定.Size = new Size(70, 20);
            コマンド確定.TabIndex = 1007;
            コマンド確定.TabStop = false;
            コマンド確定.Text = "確定";
            コマンド確定.UseVisualStyleBackColor = true;
            コマンド確定.Click += コマンド確定_Click;
            // 
            // コマンド承認
            // 
            コマンド承認.Enabled = false;
            コマンド承認.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド承認.ForeColor = Color.Blue;
            コマンド承認.ImageAlign = ContentAlignment.BottomLeft;
            コマンド承認.Location = new Point(585, 5);
            コマンド承認.Margin = new Padding(3, 2, 3, 2);
            コマンド承認.Name = "コマンド承認";
            コマンド承認.Size = new Size(70, 20);
            コマンド承認.TabIndex = 1006;
            コマンド承認.TabStop = false;
            コマンド承認.Text = "承認";
            コマンド承認.UseVisualStyleBackColor = true;
            コマンド承認.Click += コマンド承認_Click;
            // 
            // コマンド削除
            // 
            コマンド削除.Enabled = false;
            コマンド削除.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド削除.ForeColor = Color.Red;
            コマンド削除.ImageAlign = ContentAlignment.BottomLeft;
            コマンド削除.Location = new Point(215, 5);
            コマンド削除.Margin = new Padding(3, 2, 3, 2);
            コマンド削除.Name = "コマンド削除";
            コマンド削除.Size = new Size(70, 20);
            コマンド削除.TabIndex = 1005;
            コマンド削除.TabStop = false;
            コマンド削除.Text = "削除";
            コマンド削除.UseVisualStyleBackColor = true;
            コマンド削除.Click += コマンド削除_Click;
            // 
            // コマンド複写
            // 
            コマンド複写.Enabled = false;
            コマンド複写.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド複写.ForeColor = Color.Blue;
            コマンド複写.ImageAlign = ContentAlignment.BottomLeft;
            コマンド複写.Location = new Point(145, 5);
            コマンド複写.Margin = new Padding(3, 2, 3, 2);
            コマンド複写.Name = "コマンド複写";
            コマンド複写.Size = new Size(70, 20);
            コマンド複写.TabIndex = 1004;
            コマンド複写.TabStop = false;
            コマンド複写.Text = "複写";
            コマンド複写.UseVisualStyleBackColor = true;
            コマンド複写.Click += コマンド複写_Click;
            // 
            // コマンド読込
            // 
            コマンド読込.Enabled = false;
            コマンド読込.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド読込.ForeColor = Color.Blue;
            コマンド読込.ImageAlign = ContentAlignment.BottomLeft;
            コマンド読込.Location = new Point(75, 5);
            コマンド読込.Margin = new Padding(3, 2, 3, 2);
            コマンド読込.Name = "コマンド読込";
            コマンド読込.Size = new Size(70, 20);
            コマンド読込.TabIndex = 1003;
            コマンド読込.TabStop = false;
            コマンド読込.Text = "読込";
            コマンド読込.UseVisualStyleBackColor = true;
            コマンド読込.Click += コマンド読込_Click;
            // 
            // 発注コードラベル
            // 
            発注コードラベル.AllowDrop = true;
            発注コードラベル.AutoEllipsis = true;
            発注コードラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            発注コードラベル.ForeColor = SystemColors.ActiveCaptionText;
            発注コードラベル.ImageAlign = ContentAlignment.MiddleLeft;
            発注コードラベル.Location = new Point(10, 50);
            発注コードラベル.Margin = new Padding(0);
            発注コードラベル.Name = "発注コードラベル";
            発注コードラベル.Size = new Size(100, 17);
            発注コードラベル.TabIndex = 1;
            発注コードラベル.Text = "発注コード(&C)";
            発注コードラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 版数_ラベル
            // 
            版数_ラベル.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            版数_ラベル.Location = new Point(253, 50);
            版数_ラベル.Name = "版数_ラベル";
            版数_ラベル.Size = new Size(54, 23);
            版数_ラベル.TabIndex = 3;
            版数_ラベル.Text = "版数(&E)";
            // 
            // 発注コード
            // 
            発注コード.BackColor = Color.FromArgb(255, 255, 153);
            発注コード.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            発注コード.FormattingEnabled = true;
            発注コード.ImeMode = ImeMode.Disable;
            発注コード.Location = new Point(108, 48);
            発注コード.Name = "発注コード";
            発注コード.Size = new Size(139, 21);
            発注コード.TabIndex = 2;
            発注コード.SelectedIndexChanged += 発注コード_SelectedIndexChanged;
            発注コード.TextChanged += 発注コード_TextChanged;
            発注コード.Enter += 発注コード_Enter;
            発注コード.KeyDown += 発注コード_KeyDown;
            発注コード.Leave += 発注コード_Leave;
            発注コード.Validating += 発注コード_Validating;
            発注コード.Validated += 発注コード_Validated;
            // 
            // 発注日選択ボタン
            // 
            発注日選択ボタン.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            発注日選択ボタン.Location = new Point(228, 71);
            発注日選択ボタン.Margin = new Padding(4);
            発注日選択ボタン.Name = "発注日選択ボタン";
            発注日選択ボタン.Size = new Size(20, 20);
            発注日選択ボタン.TabIndex = 10005;
            発注日選択ボタン.TabStop = false;
            発注日選択ボタン.Text = "▼";
            発注日選択ボタン.UseVisualStyleBackColor = true;
            発注日選択ボタン.Click += 発注日選択ボタン_Click;
            // 
            // 改版ボタン
            // 
            改版ボタン.Enabled = false;
            改版ボタン.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            改版ボタン.Location = new Point(296, 70);
            改版ボタン.Margin = new Padding(4);
            改版ボタン.Name = "改版ボタン";
            改版ボタン.RightToLeft = RightToLeft.Yes;
            改版ボタン.Size = new Size(80, 23);
            改版ボタン.TabIndex = 21;
            改版ボタン.Text = "改版(&R)";
            改版ボタン.UseVisualStyleBackColor = true;
            改版ボタン.Click += 改版ボタン_Click;
            // 
            // 購買コード
            // 
            購買コード.BackColor = SystemColors.Window;
            購買コード.Enabled = false;
            購買コード.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            購買コード.ImeMode = ImeMode.NoControl;
            購買コード.Location = new Point(444, 48);
            購買コード.Margin = new Padding(3, 2, 3, 2);
            購買コード.Name = "購買コード";
            購買コード.ReadOnly = true;
            購買コード.Size = new Size(193, 20);
            購買コード.TabIndex = 10016;
            購買コード.TabStop = false;
            // 
            // 発注版数
            // 
            発注版数.BackColor = Color.FromArgb(255, 255, 153);
            発注版数.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            発注版数.FormattingEnabled = true;
            発注版数.ImeMode = ImeMode.Disable;
            発注版数.Location = new Point(301, 48);
            発注版数.Name = "発注版数";
            発注版数.Size = new Size(54, 21);
            発注版数.TabIndex = 4;
            発注版数.Enter += 発注版数_Enter;
            発注版数.Leave += 発注版数_Leave;
            発注版数.Validating += 発注版数_Validating;
            発注版数.Validated += 発注版数_Validated;
            // 
            // 購買コード_ラベル
            // 
            購買コード_ラベル.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            購買コード_ラベル.Location = new Point(383, 50);
            購買コード_ラベル.Name = "購買コード_ラベル";
            購買コード_ラベル.Size = new Size(57, 23);
            購買コード_ラベル.TabIndex = 10064;
            購買コード_ラベル.Text = "購買コード";
            // 
            // シリーズ名
            // 
            シリーズ名.BackColor = SystemColors.Window;
            シリーズ名.Enabled = false;
            シリーズ名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            シリーズ名.ImeMode = ImeMode.NoControl;
            シリーズ名.Location = new Point(444, 71);
            シリーズ名.Margin = new Padding(3, 2, 3, 2);
            シリーズ名.Name = "シリーズ名";
            シリーズ名.ReadOnly = true;
            シリーズ名.Size = new Size(193, 20);
            シリーズ名.TabIndex = 10065;
            シリーズ名.TabStop = false;
            // 
            // シリーズ名_ラベル
            // 
            シリーズ名_ラベル.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            シリーズ名_ラベル.Location = new Point(383, 73);
            シリーズ名_ラベル.Name = "シリーズ名_ラベル";
            シリーズ名_ラベル.Size = new Size(57, 23);
            シリーズ名_ラベル.TabIndex = 10066;
            シリーズ名_ラベル.Text = "シリーズ名";
            // 
            // ロット番号_ラベル
            // 
            ロット番号_ラベル.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            ロット番号_ラベル.Location = new Point(383, 96);
            ロット番号_ラベル.Name = "ロット番号_ラベル";
            ロット番号_ラベル.Size = new Size(57, 23);
            ロット番号_ラベル.TabIndex = 10067;
            ロット番号_ラベル.Text = "ロット番号";
            // 
            // ロット番号
            // 
            ロット番号.BackColor = SystemColors.Window;
            ロット番号.Enabled = false;
            ロット番号.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            ロット番号.ImeMode = ImeMode.NoControl;
            ロット番号.Location = new Point(444, 94);
            ロット番号.Margin = new Padding(3, 2, 3, 2);
            ロット番号.Name = "ロット番号";
            ロット番号.ReadOnly = true;
            ロット番号.Size = new Size(193, 20);
            ロット番号.TabIndex = 10068;
            ロット番号.TabStop = false;
            // 
            // 発注日_ラベル
            // 
            発注日_ラベル.AllowDrop = true;
            発注日_ラベル.AutoEllipsis = true;
            発注日_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            発注日_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            発注日_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            発注日_ラベル.Location = new Point(10, 73);
            発注日_ラベル.Margin = new Padding(0);
            発注日_ラベル.Name = "発注日_ラベル";
            発注日_ラベル.Size = new Size(100, 17);
            発注日_ラベル.TabIndex = 5;
            発注日_ラベル.Text = "発注日(&D)";
            発注日_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 発注日
            // 
            発注日.BackColor = Color.White;
            発注日.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            発注日.ImeMode = ImeMode.Disable;
            発注日.Location = new Point(108, 71);
            発注日.Margin = new Padding(3, 2, 3, 2);
            発注日.Name = "発注日";
            発注日.Size = new Size(116, 20);
            発注日.TabIndex = 6;
            発注日.TextChanged += 発注日_TextChanged;
            発注日.Enter += 発注日_Enter;
            発注日.KeyPress += 発注日_KeyPress;
            発注日.Leave += 発注日_Leave;
            発注日.Validating += 発注日_Validating;
            発注日.Validated += 発注日_Validated;
            // 
            // 発注者_ラベル
            // 
            発注者_ラベル.AllowDrop = true;
            発注者_ラベル.AutoEllipsis = true;
            発注者_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            発注者_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            発注者_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            発注者_ラベル.Location = new Point(10, 96);
            発注者_ラベル.Margin = new Padding(0);
            発注者_ラベル.Name = "発注者_ラベル";
            発注者_ラベル.Size = new Size(100, 17);
            発注者_ラベル.TabIndex = 7;
            発注者_ラベル.Text = "発注者(&N)";
            発注者_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 発注者コード
            // 
            発注者コード.BackColor = Color.White;
            発注者コード.Enabled = false;
            発注者コード.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            発注者コード.FormattingEnabled = true;
            発注者コード.ImeMode = ImeMode.Disable;
            発注者コード.Location = new Point(108, 93);
            発注者コード.MaxDropDownItems = 9;
            発注者コード.Name = "発注者コード";
            発注者コード.Size = new Size(61, 21);
            発注者コード.TabIndex = 8;
            発注者コード.DrawItem += 発注者コード_DrawItem;
            発注者コード.TextChanged += 発注者コード_TextChanged;
            発注者コード.Enter += 発注者コード_Enter;
            発注者コード.Validating += 発注者コード_Validating;
            発注者コード.Validated += 発注者コード_Validated;
            // 
            // 発注者名
            // 
            発注者名.BackColor = SystemColors.Window;
            発注者名.Enabled = false;
            発注者名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            発注者名.ImeMode = ImeMode.NoControl;
            発注者名.Location = new Point(175, 94);
            発注者名.Margin = new Padding(3, 2, 3, 2);
            発注者名.Name = "発注者名";
            発注者名.Size = new Size(202, 20);
            発注者名.TabIndex = 10069;
            発注者名.TabStop = false;
            発注者名.Visible = false;
            // 
            // 仕入先コード
            // 
            仕入先コード.BackColor = Color.White;
            仕入先コード.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            仕入先コード.ImeMode = ImeMode.Disable;
            仕入先コード.Location = new Point(108, 117);
            仕入先コード.Margin = new Padding(3, 2, 3, 2);
            仕入先コード.Name = "仕入先コード";
            仕入先コード.Size = new Size(116, 20);
            仕入先コード.TabIndex = 10;
            仕入先コード.TextChanged += 仕入先コード_TextChanged;
            仕入先コード.DoubleClick += 仕入先コード_DoubleClick;
            仕入先コード.Enter += 仕入先コード_Enter;
            仕入先コード.KeyDown += 仕入先コード_KeyDown;
            仕入先コード.KeyPress += 仕入先コード_KeyPress;
            仕入先コード.Leave += 仕入先コード_Leave;
            仕入先コード.Validating += 仕入先コード_Validating;
            仕入先コード.Validated += 仕入先コード_Validated;
            // 
            // 仕入先_ラベル
            // 
            仕入先_ラベル.AllowDrop = true;
            仕入先_ラベル.AutoEllipsis = true;
            仕入先_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            仕入先_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            仕入先_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            仕入先_ラベル.Location = new Point(10, 120);
            仕入先_ラベル.Margin = new Padding(0);
            仕入先_ラベル.Name = "仕入先_ラベル";
            仕入先_ラベル.Size = new Size(100, 17);
            仕入先_ラベル.TabIndex = 9;
            仕入先_ラベル.Text = "仕入先(&S)";
            仕入先_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 仕入先選択ボタン
            // 
            仕入先選択ボタン.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            仕入先選択ボタン.Location = new Point(228, 117);
            仕入先選択ボタン.Margin = new Padding(4);
            仕入先選択ボタン.Name = "仕入先選択ボタン";
            仕入先選択ボタン.Size = new Size(20, 20);
            仕入先選択ボタン.TabIndex = 10070;
            仕入先選択ボタン.TabStop = false;
            仕入先選択ボタン.Text = "▼";
            仕入先選択ボタン.UseVisualStyleBackColor = true;
            仕入先選択ボタン.Click += 仕入先選択ボタン_Click;
            // 
            // 仕入先名
            // 
            仕入先名.BackColor = SystemColors.Window;
            仕入先名.Enabled = false;
            仕入先名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            仕入先名.ImeMode = ImeMode.NoControl;
            仕入先名.Location = new Point(253, 117);
            仕入先名.Margin = new Padding(3, 2, 3, 2);
            仕入先名.Name = "仕入先名";
            仕入先名.Size = new Size(384, 20);
            仕入先名.TabIndex = 10071;
            // 
            // 仕入先担当者名
            // 
            仕入先担当者名.BackColor = Color.White;
            仕入先担当者名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            仕入先担当者名.ImeMode = ImeMode.Hiragana;
            仕入先担当者名.Location = new Point(108, 142);
            仕入先担当者名.Margin = new Padding(3, 2, 3, 2);
            仕入先担当者名.Name = "仕入先担当者名";
            仕入先担当者名.Size = new Size(199, 20);
            仕入先担当者名.TabIndex = 12;
            仕入先担当者名.TextChanged += 仕入先担当者名_TextChanged;
            仕入先担当者名.Enter += 仕入先担当者名_Enter;
            仕入先担当者名.Leave += 仕入先担当者名_Leave;
            仕入先担当者名.Validating += 仕入先担当者名_Validating;
            // 
            // 担当者名_ラベル
            // 
            担当者名_ラベル.AllowDrop = true;
            担当者名_ラベル.AutoEllipsis = true;
            担当者名_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            担当者名_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            担当者名_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            担当者名_ラベル.Location = new Point(10, 144);
            担当者名_ラベル.Margin = new Padding(0);
            担当者名_ラベル.Name = "担当者名_ラベル";
            担当者名_ラベル.Size = new Size(100, 17);
            担当者名_ラベル.TabIndex = 11;
            担当者名_ラベル.Text = "担当者名(&P)";
            担当者名_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 摘要_ラベル
            // 
            摘要_ラベル.AllowDrop = true;
            摘要_ラベル.AutoEllipsis = true;
            摘要_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            摘要_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            摘要_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            摘要_ラベル.Location = new Point(10, 168);
            摘要_ラベル.Margin = new Padding(0);
            摘要_ラベル.Name = "摘要_ラベル";
            摘要_ラベル.Size = new Size(95, 71);
            摘要_ラベル.TabIndex = 13;
            摘要_ラベル.Text = "摘要(&N)";
            // 
            // 摘要
            // 
            摘要.BackColor = SystemColors.Window;
            摘要.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            摘要.ImeMode = ImeMode.Hiragana;
            摘要.Location = new Point(108, 165);
            摘要.Margin = new Padding(3, 2, 3, 2);
            摘要.Multiline = true;
            摘要.Name = "摘要";
            摘要.Size = new Size(720, 84);
            摘要.TabIndex = 14;
            摘要.TextChanged += 摘要_TextChanged;
            摘要.Enter += 摘要_Enter;
            摘要.Leave += 摘要_Leave;
            摘要.Validating += 摘要_Validating;
            摘要.Validated += 摘要_Validated;
            // 
            // 備考
            // 
            備考.BackColor = SystemColors.Window;
            備考.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            備考.ImeMode = ImeMode.Hiragana;
            備考.Location = new Point(108, 255);
            備考.Margin = new Padding(3, 2, 3, 2);
            備考.Multiline = true;
            備考.Name = "備考";
            備考.Size = new Size(720, 44);
            備考.TabIndex = 16;
            備考.TextChanged += 備考_TextChanged;
            備考.DoubleClick += 備考_DoubleClick;
            備考.Enter += 備考_Enter;
            備考.Leave += 備考_Leave;
            備考.Validating += 備考_Validating;
            備考.Validated += 備考_Validated;
            // 
            // 備考_ラベル
            // 
            備考_ラベル.AllowDrop = true;
            備考_ラベル.AutoEllipsis = true;
            備考_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            備考_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            備考_ラベル.ImageAlign = ContentAlignment.TopLeft;
            備考_ラベル.Location = new Point(10, 255);
            備考_ラベル.Margin = new Padding(0);
            備考_ラベル.Name = "備考_ラベル";
            備考_ラベル.Size = new Size(95, 43);
            備考_ラベル.TabIndex = 15;
            備考_ラベル.Text = "備考(&O)";
            // 
            // 在庫管理_ラベル
            // 
            在庫管理_ラベル.AllowDrop = true;
            在庫管理_ラベル.AutoEllipsis = true;
            在庫管理_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            在庫管理_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            在庫管理_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            在庫管理_ラベル.Location = new Point(10, 306);
            在庫管理_ラベル.Margin = new Padding(0);
            在庫管理_ラベル.Name = "在庫管理_ラベル";
            在庫管理_ラベル.Size = new Size(95, 17);
            在庫管理_ラベル.TabIndex = 17;
            在庫管理_ラベル.Text = "在庫管理(&M)";
            在庫管理_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 注釈1_ラベル
            // 
            注釈1_ラベル.AllowDrop = true;
            注釈1_ラベル.AutoEllipsis = true;
            注釈1_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            注釈1_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            注釈1_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            注釈1_ラベル.Location = new Point(129, 306);
            注釈1_ラベル.Margin = new Padding(0);
            注釈1_ラベル.Name = "注釈1_ラベル";
            注釈1_ラベル.Size = new Size(430, 17);
            注釈1_ラベル.TabIndex = 10080;
            注釈1_ラベル.Text = "※在庫管理を必要とするときのみチェックを入れてください。";
            注釈1_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 注釈2_ラベル
            // 
            注釈2_ラベル.AllowDrop = true;
            注釈2_ラベル.AutoEllipsis = true;
            注釈2_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            注釈2_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            注釈2_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            注釈2_ラベル.Location = new Point(129, 326);
            注釈2_ラベル.Margin = new Padding(0);
            注釈2_ラベル.Name = "注釈2_ラベル";
            注釈2_ラベル.Size = new Size(430, 17);
            注釈2_ラベル.TabIndex = 10082;
            注釈2_ラベル.Text = "※振込を行う必要が無いときのみチェックを入れてください。";
            注釈2_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 振込不要_ラベル
            // 
            振込不要_ラベル.AllowDrop = true;
            振込不要_ラベル.AutoEllipsis = true;
            振込不要_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            振込不要_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            振込不要_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            振込不要_ラベル.Location = new Point(10, 326);
            振込不要_ラベル.Margin = new Padding(0);
            振込不要_ラベル.Name = "振込不要_ラベル";
            振込不要_ラベル.Size = new Size(95, 17);
            振込不要_ラベル.TabIndex = 19;
            振込不要_ラベル.Text = "振込不要(&T)";
            振込不要_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 登録日時_ラベル
            // 
            登録日時_ラベル.AllowDrop = true;
            登録日時_ラベル.AutoEllipsis = true;
            登録日時_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            登録日時_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            登録日時_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            登録日時_ラベル.Location = new Point(698, 50);
            登録日時_ラベル.Margin = new Padding(0);
            登録日時_ラベル.Name = "登録日時_ラベル";
            登録日時_ラベル.Size = new Size(64, 17);
            登録日時_ラベル.TabIndex = 10083;
            登録日時_ラベル.Text = "登録日時";
            登録日時_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 登録日時
            // 
            登録日時.BackColor = SystemColors.Window;
            登録日時.Enabled = false;
            登録日時.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            登録日時.ImeMode = ImeMode.NoControl;
            登録日時.Location = new Point(769, 48);
            登録日時.Margin = new Padding(3, 2, 3, 2);
            登録日時.Name = "登録日時";
            登録日時.ReadOnly = true;
            登録日時.Size = new Size(240, 20);
            登録日時.TabIndex = 10085;
            登録日時.TabStop = false;
            // 
            // 仕入先電話番号__ラベル
            // 
            仕入先電話番号__ラベル.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            仕入先電話番号__ラベル.Location = new Point(313, 145);
            仕入先電話番号__ラベル.Name = "仕入先電話番号__ラベル";
            仕入先電話番号__ラベル.Size = new Size(28, 16);
            仕入先電話番号__ラベル.TabIndex = 10088;
            仕入先電話番号__ラベル.Text = "TEL:";
            // 
            // 仕入先電話番号
            // 
            仕入先電話番号.BackColor = SystemColors.Window;
            仕入先電話番号.Enabled = false;
            仕入先電話番号.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            仕入先電話番号.ImeMode = ImeMode.NoControl;
            仕入先電話番号.Location = new Point(341, 142);
            仕入先電話番号.Margin = new Padding(3, 2, 3, 2);
            仕入先電話番号.Name = "仕入先電話番号";
            仕入先電話番号.ReadOnly = true;
            仕入先電話番号.Size = new Size(129, 20);
            仕入先電話番号.TabIndex = 10089;
            仕入先電話番号.TabStop = false;
            // 
            // 仕入先ファックス番号
            // 
            仕入先ファックス番号.BackColor = SystemColors.Window;
            仕入先ファックス番号.Enabled = false;
            仕入先ファックス番号.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            仕入先ファックス番号.ImeMode = ImeMode.NoControl;
            仕入先ファックス番号.Location = new Point(507, 142);
            仕入先ファックス番号.Margin = new Padding(3, 2, 3, 2);
            仕入先ファックス番号.Name = "仕入先ファックス番号";
            仕入先ファックス番号.ReadOnly = true;
            仕入先ファックス番号.Size = new Size(129, 20);
            仕入先ファックス番号.TabIndex = 10091;
            // 
            // 仕入先ファックス番号_ラベル
            // 
            仕入先ファックス番号_ラベル.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            仕入先ファックス番号_ラベル.Location = new Point(474, 145);
            仕入先ファックス番号_ラベル.Name = "仕入先ファックス番号_ラベル";
            仕入先ファックス番号_ラベル.Size = new Size(34, 16);
            仕入先ファックス番号_ラベル.TabIndex = 10090;
            仕入先ファックス番号_ラベル.Text = "FAX:";
            仕入先ファックス番号_ラベル.TextAlign = ContentAlignment.TopRight;
            // 
            // 在庫管理
            // 
            在庫管理.AutoSize = true;
            在庫管理.Location = new Point(108, 309);
            在庫管理.Name = "在庫管理";
            在庫管理.Size = new Size(15, 14);
            在庫管理.TabIndex = 18;
            在庫管理.UseVisualStyleBackColor = true;
            在庫管理.CheckedChanged += 在庫管理_CheckedChanged;
            在庫管理.Enter += 在庫管理_Enter;
            在庫管理.Leave += 在庫管理_Leave;
            在庫管理.Validating += 在庫管理_Validating;
            // 
            // NoCredit
            // 
            NoCredit.AutoSize = true;
            NoCredit.Location = new Point(108, 329);
            NoCredit.Name = "NoCredit";
            NoCredit.Size = new Size(15, 14);
            NoCredit.TabIndex = 20;
            NoCredit.UseVisualStyleBackColor = true;
            NoCredit.Enter += NoCredit_Enter;
            NoCredit.Leave += NoCredit_Leave;
            NoCredit.Validating += NoCredit_Validating;
            NoCredit.Validated += NoCredit_Validated;
            // 
            // 登録者_ラベル
            // 
            登録者_ラベル.AllowDrop = true;
            登録者_ラベル.AutoEllipsis = true;
            登録者_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            登録者_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            登録者_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            登録者_ラベル.Location = new Point(698, 73);
            登録者_ラベル.Margin = new Padding(0);
            登録者_ラベル.Name = "登録者_ラベル";
            登録者_ラベル.Size = new Size(64, 17);
            登録者_ラベル.TabIndex = 10092;
            登録者_ラベル.Text = "登録者";
            登録者_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 登録者コード
            // 
            登録者コード.BackColor = SystemColors.Window;
            登録者コード.Enabled = false;
            登録者コード.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            登録者コード.ImeMode = ImeMode.NoControl;
            登録者コード.Location = new Point(769, 71);
            登録者コード.Margin = new Padding(3, 2, 3, 2);
            登録者コード.Name = "登録者コード";
            登録者コード.ReadOnly = true;
            登録者コード.Size = new Size(40, 20);
            登録者コード.TabIndex = 10093;
            登録者コード.TabStop = false;
            // 
            // 登録者名
            // 
            登録者名.BackColor = SystemColors.Window;
            登録者名.Enabled = false;
            登録者名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            登録者名.ImeMode = ImeMode.NoControl;
            登録者名.Location = new Point(807, 71);
            登録者名.Margin = new Padding(3, 2, 3, 2);
            登録者名.Name = "登録者名";
            登録者名.ReadOnly = true;
            登録者名.Size = new Size(202, 20);
            登録者名.TabIndex = 10094;
            登録者名.TabStop = false;
            // 
            // 確定_ラベル
            // 
            確定_ラベル.AllowDrop = true;
            確定_ラベル.AutoEllipsis = true;
            確定_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            確定_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            確定_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            確定_ラベル.Location = new Point(698, 96);
            確定_ラベル.Margin = new Padding(0);
            確定_ラベル.Name = "確定_ラベル";
            確定_ラベル.Size = new Size(31, 17);
            確定_ラベル.TabIndex = 10095;
            確定_ラベル.Text = "確定";
            確定_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 確定
            // 
            確定.BackColor = SystemColors.Window;
            確定.Enabled = false;
            確定.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            確定.ImeMode = ImeMode.NoControl;
            確定.Location = new Point(732, 94);
            確定.Margin = new Padding(3, 2, 3, 2);
            確定.Name = "確定";
            確定.ReadOnly = true;
            確定.Size = new Size(20, 20);
            確定.TabIndex = 10096;
            確定.TabStop = false;
            // 
            // 承認
            // 
            承認.BackColor = SystemColors.Window;
            承認.Enabled = false;
            承認.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            承認.ImeMode = ImeMode.NoControl;
            承認.Location = new Point(795, 94);
            承認.Margin = new Padding(3, 2, 3, 2);
            承認.Name = "承認";
            承認.ReadOnly = true;
            承認.Size = new Size(20, 20);
            承認.TabIndex = 10098;
            承認.TabStop = false;
            // 
            // 承認_ラベル
            // 
            承認_ラベル.AllowDrop = true;
            承認_ラベル.AutoEllipsis = true;
            承認_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            承認_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            承認_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            承認_ラベル.Location = new Point(761, 96);
            承認_ラベル.Margin = new Padding(0);
            承認_ラベル.Name = "承認_ラベル";
            承認_ラベル.Size = new Size(31, 17);
            承認_ラベル.TabIndex = 10097;
            承認_ラベル.Text = "承認";
            承認_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 送信
            // 
            送信.BackColor = SystemColors.Window;
            送信.Enabled = false;
            送信.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            送信.ImeMode = ImeMode.NoControl;
            送信.Location = new Point(860, 94);
            送信.Margin = new Padding(3, 2, 3, 2);
            送信.Name = "送信";
            送信.ReadOnly = true;
            送信.Size = new Size(20, 20);
            送信.TabIndex = 10100;
            送信.TabStop = false;
            // 
            // 送信_ラベル
            // 
            送信_ラベル.AllowDrop = true;
            送信_ラベル.AutoEllipsis = true;
            送信_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            送信_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            送信_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            送信_ラベル.Location = new Point(824, 96);
            送信_ラベル.Margin = new Padding(0);
            送信_ラベル.Name = "送信_ラベル";
            送信_ラベル.Size = new Size(35, 17);
            送信_ラベル.TabIndex = 10099;
            送信_ラベル.Text = "送信";
            送信_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 入庫状況表示
            // 
            入庫状況表示.BackColor = SystemColors.Window;
            入庫状況表示.Enabled = false;
            入庫状況表示.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            入庫状況表示.ImeMode = ImeMode.NoControl;
            入庫状況表示.Location = new Point(923, 94);
            入庫状況表示.Margin = new Padding(3, 2, 3, 2);
            入庫状況表示.Name = "入庫状況表示";
            入庫状況表示.ReadOnly = true;
            入庫状況表示.Size = new Size(20, 20);
            入庫状況表示.TabIndex = 10102;
            入庫状況表示.TabStop = false;
            // 
            // 入庫_ラベル
            // 
            入庫_ラベル.AllowDrop = true;
            入庫_ラベル.AutoEllipsis = true;
            入庫_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            入庫_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            入庫_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            入庫_ラベル.Location = new Point(889, 96);
            入庫_ラベル.Margin = new Padding(0);
            入庫_ラベル.Name = "入庫_ラベル";
            入庫_ラベル.Size = new Size(31, 17);
            入庫_ラベル.TabIndex = 10101;
            入庫_ラベル.Text = "入庫";
            入庫_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 削除
            // 
            削除.BackColor = SystemColors.Window;
            削除.Enabled = false;
            削除.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            削除.ImeMode = ImeMode.NoControl;
            削除.Location = new Point(989, 94);
            削除.Margin = new Padding(3, 2, 3, 2);
            削除.Name = "削除";
            削除.ReadOnly = true;
            削除.Size = new Size(20, 20);
            削除.TabIndex = 10104;
            削除.TabStop = false;
            // 
            // 削除_ラベル
            // 
            削除_ラベル.AllowDrop = true;
            削除_ラベル.AutoEllipsis = true;
            削除_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            削除_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            削除_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            削除_ラベル.Location = new Point(955, 96);
            削除_ラベル.Margin = new Padding(0);
            削除_ラベル.Name = "削除_ラベル";
            削除_ラベル.Size = new Size(31, 17);
            削除_ラベル.TabIndex = 10103;
            削除_ラベル.Text = "削除";
            削除_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 注釈3_ラベル
            // 
            注釈3_ラベル.AllowDrop = true;
            注釈3_ラベル.AutoEllipsis = true;
            注釈3_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            注釈3_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            注釈3_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            注釈3_ラベル.Location = new Point(649, 120);
            注釈3_ラベル.Margin = new Padding(0);
            注釈3_ラベル.Name = "注釈3_ラベル";
            注釈3_ラベル.Size = new Size(384, 41);
            注釈3_ラベル.TabIndex = 10105;
            注釈3_ラベル.Text = "※U-netからFAXを送信する場合、「020」や「090」等、“0**0”    で始まる特殊な番号の一部への送信はできません。";
            注釈3_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // SupplierSendMethodCode
            // 
            SupplierSendMethodCode.BackColor = Color.White;
            SupplierSendMethodCode.Enabled = false;
            SupplierSendMethodCode.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            SupplierSendMethodCode.ImeMode = ImeMode.NoControl;
            SupplierSendMethodCode.Location = new Point(850, 224);
            SupplierSendMethodCode.Margin = new Padding(3, 2, 3, 2);
            SupplierSendMethodCode.Name = "SupplierSendMethodCode";
            SupplierSendMethodCode.Size = new Size(52, 20);
            SupplierSendMethodCode.TabIndex = 10108;
            SupplierSendMethodCode.TabStop = false;
            SupplierSendMethodCode.Visible = false;
            // 
            // SupplierSendMethodName
            // 
            SupplierSendMethodName.BackColor = Color.White;
            SupplierSendMethodName.Enabled = false;
            SupplierSendMethodName.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            SupplierSendMethodName.ImeMode = ImeMode.NoControl;
            SupplierSendMethodName.Location = new Point(913, 224);
            SupplierSendMethodName.Margin = new Padding(3, 2, 3, 2);
            SupplierSendMethodName.Name = "SupplierSendMethodName";
            SupplierSendMethodName.Size = new Size(104, 20);
            SupplierSendMethodName.TabIndex = 10109;
            SupplierSendMethodName.TabStop = false;
            SupplierSendMethodName.Visible = false;
            // 
            // 確定日時
            // 
            確定日時.BackColor = SystemColors.Control;
            確定日時.Enabled = false;
            確定日時.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            確定日時.ImeMode = ImeMode.NoControl;
            確定日時.Location = new Point(836, 165);
            確定日時.Margin = new Padding(3, 2, 3, 2);
            確定日時.Name = "確定日時";
            確定日時.Size = new Size(138, 20);
            確定日時.TabIndex = 10110;
            確定日時.TabStop = false;
            確定日時.Visible = false;
            // 
            // 承認日時
            // 
            承認日時.BackColor = SystemColors.Control;
            承認日時.Enabled = false;
            承認日時.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            承認日時.ImeMode = ImeMode.NoControl;
            承認日時.Location = new Point(836, 191);
            承認日時.Margin = new Padding(3, 2, 3, 2);
            承認日時.Name = "承認日時";
            承認日時.Size = new Size(44, 20);
            承認日時.TabIndex = 10111;
            承認日時.TabStop = false;
            承認日時.Visible = false;
            // 
            // 承認者コード
            // 
            承認者コード.BackColor = SystemColors.Control;
            承認者コード.Enabled = false;
            承認者コード.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            承認者コード.ImeMode = ImeMode.NoControl;
            承認者コード.Location = new Point(883, 191);
            承認者コード.Margin = new Padding(3, 2, 3, 2);
            承認者コード.Name = "承認者コード";
            承認者コード.Size = new Size(40, 20);
            承認者コード.TabIndex = 10112;
            承認者コード.TabStop = false;
            承認者コード.Visible = false;
            // 
            // 承認者名
            // 
            承認者名.BackColor = SystemColors.Control;
            承認者名.Enabled = false;
            承認者名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            承認者名.ImeMode = ImeMode.NoControl;
            承認者名.Location = new Point(926, 191);
            承認者名.Margin = new Padding(3, 2, 3, 2);
            承認者名.Name = "承認者名";
            承認者名.Size = new Size(48, 20);
            承認者名.TabIndex = 10113;
            承認者名.TabStop = false;
            承認者名.Visible = false;
            // 
            // テストコマンド
            // 
            テストコマンド.Location = new Point(906, 304);
            テストコマンド.Margin = new Padding(4);
            テストコマンド.Name = "テストコマンド";
            テストコマンド.RightToLeft = RightToLeft.Yes;
            テストコマンド.Size = new Size(80, 23);
            テストコマンド.TabIndex = 22;
            テストコマンド.Text = "承認者テスト";
            テストコマンド.UseVisualStyleBackColor = true;
            テストコマンド.Visible = false;
            テストコマンド.Click += テストコマンド_Click;
            // 
            // 無効日時
            // 
            無効日時.BackColor = SystemColors.Control;
            無効日時.Enabled = false;
            無効日時.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            無効日時.ImeMode = ImeMode.NoControl;
            無効日時.Location = new Point(836, 255);
            無効日時.Margin = new Padding(3, 2, 3, 2);
            無効日時.Name = "無効日時";
            無効日時.Size = new Size(138, 20);
            無効日時.TabIndex = 10114;
            無効日時.TabStop = false;
            無効日時.Visible = false;
            // 
            // 無効者コード
            // 
            無効者コード.BackColor = SystemColors.Control;
            無効者コード.Enabled = false;
            無効者コード.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            無効者コード.ImeMode = ImeMode.NoControl;
            無効者コード.Location = new Point(836, 279);
            無効者コード.Margin = new Padding(3, 2, 3, 2);
            無効者コード.Name = "無効者コード";
            無効者コード.Size = new Size(44, 20);
            無効者コード.TabIndex = 10115;
            無効者コード.TabStop = false;
            無効者コード.Visible = false;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 707);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 19, 0);
            statusStrip1.Size = new Size(1035, 22);
            statusStrip1.TabIndex = 10117;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(89, 17);
            toolStripStatusLabel1.Text = "各種項目の説明";
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new Size(111, 20);
            toolStripStatusLabel2.Text = "各種項目の説明";
            // 
            // 入庫状況
            // 
            入庫状況.BackColor = SystemColors.Window;
            入庫状況.Enabled = false;
            入庫状況.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            入庫状況.ImeMode = ImeMode.NoControl;
            入庫状況.Location = new Point(1004, 144);
            入庫状況.Margin = new Padding(3, 2, 3, 2);
            入庫状況.Name = "入庫状況";
            入庫状況.ReadOnly = true;
            入庫状況.Size = new Size(20, 20);
            入庫状況.TabIndex = 10118;
            入庫状況.TabStop = false;
            // 
            // 発注明細1
            // 
            発注明細1.AutoScroll = true;
            発注明細1.Location = new Point(5, 361);
            発注明細1.Margin = new Padding(4, 5, 4, 5);
            発注明細1.Name = "発注明細1";
            発注明細1.Size = new Size(1584, 338);
            発注明細1.TabIndex = 10119;
            発注明細1.Resize += Form_Resize;
            // 
            // F_発注
            // 
            BackColor = SystemColors.Control;
            ClientSize = new Size(1035, 729);
            Controls.Add(発注明細1);
            Controls.Add(入庫状況);
            Controls.Add(statusStrip1);
            Controls.Add(無効者コード);
            Controls.Add(無効日時);
            Controls.Add(テストコマンド);
            Controls.Add(承認者名);
            Controls.Add(承認者コード);
            Controls.Add(承認日時);
            Controls.Add(確定日時);
            Controls.Add(SupplierSendMethodName);
            Controls.Add(SupplierSendMethodCode);
            Controls.Add(注釈3_ラベル);
            Controls.Add(削除);
            Controls.Add(削除_ラベル);
            Controls.Add(入庫状況表示);
            Controls.Add(入庫_ラベル);
            Controls.Add(送信);
            Controls.Add(送信_ラベル);
            Controls.Add(承認);
            Controls.Add(承認_ラベル);
            Controls.Add(確定);
            Controls.Add(確定_ラベル);
            Controls.Add(登録者名);
            Controls.Add(登録者コード);
            Controls.Add(登録者_ラベル);
            Controls.Add(NoCredit);
            Controls.Add(在庫管理);
            Controls.Add(仕入先ファックス番号);
            Controls.Add(仕入先ファックス番号_ラベル);
            Controls.Add(仕入先電話番号);
            Controls.Add(仕入先電話番号__ラベル);
            Controls.Add(登録日時);
            Controls.Add(登録日時_ラベル);
            Controls.Add(注釈2_ラベル);
            Controls.Add(振込不要_ラベル);
            Controls.Add(注釈1_ラベル);
            Controls.Add(在庫管理_ラベル);
            Controls.Add(備考);
            Controls.Add(備考_ラベル);
            Controls.Add(摘要);
            Controls.Add(摘要_ラベル);
            Controls.Add(仕入先担当者名);
            Controls.Add(担当者名_ラベル);
            Controls.Add(仕入先名);
            Controls.Add(仕入先選択ボタン);
            Controls.Add(仕入先コード);
            Controls.Add(仕入先_ラベル);
            Controls.Add(発注者名);
            Controls.Add(発注者コード);
            Controls.Add(発注者_ラベル);
            Controls.Add(発注日);
            Controls.Add(発注日_ラベル);
            Controls.Add(ロット番号);
            Controls.Add(ロット番号_ラベル);
            Controls.Add(シリーズ名_ラベル);
            Controls.Add(シリーズ名);
            Controls.Add(購買コード_ラベル);
            Controls.Add(発注版数);
            Controls.Add(購買コード);
            Controls.Add(改版ボタン);
            Controls.Add(発注日選択ボタン);
            Controls.Add(発注コード);
            Controls.Add(コマンド終了);
            Controls.Add(コマンド登録);
            Controls.Add(コマンド購買);
            Controls.Add(コマンド部品);
            Controls.Add(コマンド送信);
            Controls.Add(コマンド発注書);
            Controls.Add(コマンド確定);
            Controls.Add(コマンド承認);
            Controls.Add(コマンド削除);
            Controls.Add(コマンド複写);
            Controls.Add(コマンド読込);
            Controls.Add(発注コードラベル);
            Controls.Add(版数_ラベル);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            ImeMode = ImeMode.Off;
            KeyPreview = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "F_発注";
            Text = " 発注";
            FormClosing += Form_Unload;
            Load += Form_Load;
            KeyDown += Form_KeyDown;
            panel1.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion


        internal System.Windows.Forms.Button コマンド終了;
        internal System.Windows.Forms.Button コマンド登録;

        internal System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Button コマンド新規;
        internal System.Windows.Forms.Button コマンド購買;
        internal System.Windows.Forms.Button コマンド部品;
        internal System.Windows.Forms.Button コマンド送信;
        internal System.Windows.Forms.Button コマンド発注書;
        internal System.Windows.Forms.Button コマンド確定;
        internal System.Windows.Forms.Button コマンド承認;
        internal System.Windows.Forms.Button コマンド削除;
        internal System.Windows.Forms.Button コマンド複写;
        internal System.Windows.Forms.Button コマンド読込;

        internal System.Windows.Forms.Label 発注コードラベル;

        internal Label 版数_ラベル;
        internal ComboBox 発注コード;
        internal Button 発注日選択ボタン;
        internal Button 改版ボタン;
        internal TextBox 購買コード;
        internal ComboBox 発注版数;
        internal Label 購買コード_ラベル;
        internal TextBox シリーズ名;
        internal Label シリーズ名_ラベル;
        internal Label ロット番号_ラベル;
        internal TextBox ロット番号;
        internal Label 発注日_ラベル;
        internal TextBox 発注日;
        internal Label 発注者_ラベル;
        internal ComboBox 発注者コード;
        internal TextBox 発注者名;
        internal TextBox 仕入先コード;
        internal Label 仕入先_ラベル;
        internal Button 仕入先選択ボタン;
        internal TextBox 仕入先名;
        internal TextBox 仕入先担当者名;
        internal Label 担当者名_ラベル;
        internal Label 摘要_ラベル;
        internal TextBox 摘要;
        internal TextBox 備考;
        internal Label 備考_ラベル;
        internal Label 在庫管理_ラベル;
        internal Label 注釈1_ラベル;
        internal Label 注釈2_ラベル;
        internal Label 振込不要_ラベル;
        internal Label 登録日時_ラベル;
        internal TextBox 登録日時;
        internal Label 仕入先電話番号__ラベル;
        internal TextBox 仕入先電話番号;
        internal TextBox 仕入先ファックス番号;
        internal Label 仕入先ファックス番号_ラベル;
        internal CheckBox 在庫管理;
        internal CheckBox NoCredit;
        internal Label 登録者_ラベル;
        internal TextBox 登録者コード;
        internal TextBox 登録者名;
        internal Label 確定_ラベル;
        internal TextBox 確定;
        internal TextBox 承認;
        internal Label 承認_ラベル;
        internal TextBox 送信;
        internal Label 送信_ラベル;
        internal TextBox 入庫状況表示;
        internal Label 入庫_ラベル;
        internal TextBox 削除;
        internal Label 削除_ラベル;
        internal Label 注釈3_ラベル;
        internal TextBox SupplierSendMethodCode;
        internal TextBox SupplierSendMethodName;
        internal TextBox 確定日時;
        internal TextBox 承認日時;
        internal TextBox 承認者コード;
        internal TextBox 承認者名;
        internal Button テストコマンド;
        internal TextBox 無効日時;
        internal TextBox 無効者コード;

        internal StatusStrip statusStrip1;
        internal ToolStripStatusLabel toolStripStatusLabel1;
        //internal ToolStripStatusLabel toolStripStatusLabel1;
        internal ToolStripStatusLabel toolStripStatusLabel2;
        internal TextBox 入庫状況;
        //internal MultiRowDesigner.発注明細 発注明細1;
        private MultiRowDesigner.発注明細 発注明細1;
    }
}


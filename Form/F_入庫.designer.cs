
namespace u_net
{
    partial class F_入庫
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_入庫));
            コマンド終了 = new Button();
            コマンド登録 = new Button();
            panel1 = new Panel();
            コマンド新規 = new Button();
            コマンド注文書 = new Button();
            コマンド仕入先 = new Button();
            コマンド発注 = new Button();
            コマンド確定 = new Button();
            コマンド承認 = new Button();
            コマンド削除 = new Button();
            コマンド複写 = new Button();
            コマンド修正 = new Button();
            コマンド = new Button();
            摘要 = new TextBox();
            摘要_ラベル = new Label();
            入庫者名 = new TextBox();
            入庫者コード = new ComboBox();
            入庫者コード_ラベル = new Label();
            入庫日 = new TextBox();
            入庫日_ラベル = new Label();
            SupplierCloseDay = new TextBox();
            締日_ラベル = new Label();
            仕入先名_ラベル = new Label();
            仕入先名 = new TextBox();
            仕入先コード_ラベル = new Label();
            仕入先コード = new TextBox();
            入庫日選択ボタン = new Button();
            入庫コード = new ComboBox();
            入庫コードラベル = new Label();
            仕入先2_ラベル = new Label();
            購買コード = new TextBox();
            購買コード_ラベル = new Label();
            担当者名_ラベル = new Label();
            仕入先担当者名 = new TextBox();
            窓口電話番号_ラベル = new Label();
            仕入先窓口電話番号 = new TextBox();
            発注コード = new ComboBox();
            発注コード_ラベル = new Label();
            版数_ラベル = new Label();
            発注版数 = new TextBox();
            集計年月 = new ComboBox();
            集計年月_ラベル = new Label();
            支払年月 = new ComboBox();
            支払年月_ラベル = new Label();
            TaxRate = new TextBox();
            消費税率_ラベル = new Label();
            ロット番号_ラベル = new Label();
            シリーズ名_ラベル = new Label();
            シリーズ名 = new TextBox();
            ロット番号1 = new TextBox();
            ロット番号2 = new TextBox();
            label1 = new Label();
            label5 = new Label();
            棚卸コード = new TextBox();
            label6 = new Label();
            削除 = new TextBox();
            確定 = new TextBox();
            label7 = new Label();
            label8 = new Label();
            登録者コード = new TextBox();
            label9 = new Label();
            登録日時 = new TextBox();
            登録者名 = new TextBox();
            確定日時 = new TextBox();
            確定者コード = new TextBox();
            無効者コード = new TextBox();
            無効日時 = new TextBox();
            入庫明細1 = new MultiRowDesigner.入庫明細();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            買掛区分コード設定 = new ComboBox();
            toolTip1 = new ToolTip(components);
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
            コマンド新規.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド新規.ForeColor = Color.Blue;
            コマンド新規.ImageAlign = ContentAlignment.BottomLeft;
            コマンド新規.Location = new Point(5, 5);
            コマンド新規.Margin = new Padding(3, 2, 3, 2);
            コマンド新規.Name = "コマンド新規";
            コマンド新規.Size = new Size(70, 20);
            コマンド新規.TabIndex = 1002;
            コマンド新規.TabStop = false;
            コマンド新規.Text = "新規";
            コマンド新規.UseVisualStyleBackColor = false;
            コマンド新規.Click += コマンド新規_Click;
            // 
            // コマンド注文書
            // 
            コマンド注文書.Enabled = false;
            コマンド注文書.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド注文書.ForeColor = Color.Blue;
            コマンド注文書.ImageAlign = ContentAlignment.BottomLeft;
            コマンド注文書.Location = new Point(440, 5);
            コマンド注文書.Margin = new Padding(3, 2, 3, 2);
            コマンド注文書.Name = "コマンド注文書";
            コマンド注文書.Size = new Size(70, 20);
            コマンド注文書.TabIndex = 1010;
            コマンド注文書.TabStop = false;
            コマンド注文書.UseVisualStyleBackColor = true;
            // 
            // コマンド仕入先
            // 
            コマンド仕入先.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド仕入先.ForeColor = Color.Blue;
            コマンド仕入先.ImageAlign = ContentAlignment.BottomLeft;
            コマンド仕入先.Location = new Point(370, 5);
            コマンド仕入先.Margin = new Padding(3, 2, 3, 2);
            コマンド仕入先.Name = "コマンド仕入先";
            コマンド仕入先.Size = new Size(70, 20);
            コマンド仕入先.TabIndex = 1009;
            コマンド仕入先.TabStop = false;
            コマンド仕入先.Text = "仕入先";
            toolTip1.SetToolTip(コマンド仕入先, "仕入先参照");
            コマンド仕入先.UseVisualStyleBackColor = true;
            コマンド仕入先.Click += コマンド仕入先_Click;
            // 
            // コマンド発注
            // 
            コマンド発注.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド発注.ForeColor = Color.Blue;
            コマンド発注.ImageAlign = ContentAlignment.BottomLeft;
            コマンド発注.Location = new Point(300, 5);
            コマンド発注.Margin = new Padding(3, 2, 3, 2);
            コマンド発注.Name = "コマンド発注";
            コマンド発注.Size = new Size(70, 20);
            コマンド発注.TabIndex = 1008;
            コマンド発注.TabStop = false;
            コマンド発注.Text = "発注参照";
            toolTip1.SetToolTip(コマンド発注, "対象発注データの参照");
            コマンド発注.UseVisualStyleBackColor = true;
            コマンド発注.Click += コマンド発注_Click;
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
            // コマンド修正
            // 
            コマンド修正.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド修正.ForeColor = Color.Blue;
            コマンド修正.ImageAlign = ContentAlignment.BottomLeft;
            コマンド修正.Location = new Point(75, 5);
            コマンド修正.Margin = new Padding(3, 2, 3, 2);
            コマンド修正.Name = "コマンド修正";
            コマンド修正.Size = new Size(70, 20);
            コマンド修正.TabIndex = 1003;
            コマンド修正.TabStop = false;
            コマンド修正.Text = "修正";
            コマンド修正.UseVisualStyleBackColor = true;
            コマンド修正.Click += コマンド修正_Click;
            // 
            // コマンド
            // 
            コマンド.Enabled = false;
            コマンド.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド.ForeColor = Color.Blue;
            コマンド.ImageAlign = ContentAlignment.BottomLeft;
            コマンド.Location = new Point(510, 5);
            コマンド.Margin = new Padding(3, 2, 3, 2);
            コマンド.Name = "コマンド";
            コマンド.Size = new Size(70, 20);
            コマンド.TabIndex = 1011;
            コマンド.TabStop = false;
            コマンド.UseVisualStyleBackColor = true;
            // 
            // 摘要
            // 
            摘要.BackColor = SystemColors.Window;
            摘要.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            摘要.ImeMode = ImeMode.Hiragana;
            摘要.Location = new Point(107, 196);
            摘要.Margin = new Padding(3, 2, 3, 2);
            摘要.Multiline = true;
            摘要.Name = "摘要";
            摘要.Size = new Size(246, 45);
            摘要.TabIndex = 14;
            摘要.TextChanged += 摘要_TextChanged;
            摘要.Validating += 摘要_Validating;
            摘要.Validated += 摘要_Validated;
            // 
            // 摘要_ラベル
            // 
            摘要_ラベル.AllowDrop = true;
            摘要_ラベル.AutoEllipsis = true;
            摘要_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            摘要_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            摘要_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            摘要_ラベル.Location = new Point(9, 199);
            摘要_ラベル.Margin = new Padding(0);
            摘要_ラベル.Name = "摘要_ラベル";
            摘要_ラベル.Size = new Size(95, 42);
            摘要_ラベル.TabIndex = 13;
            摘要_ラベル.Text = "摘要(&N)";
            // 
            // 入庫者名
            // 
            入庫者名.BackColor = SystemColors.Window;
            入庫者名.Enabled = false;
            入庫者名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            入庫者名.ImeMode = ImeMode.NoControl;
            入庫者名.Location = new Point(178, 94);
            入庫者名.Margin = new Padding(3, 2, 3, 2);
            入庫者名.Name = "入庫者名";
            入庫者名.Size = new Size(175, 20);
            入庫者名.TabIndex = 10129;
            入庫者名.TabStop = false;
            // 
            // 入庫者コード
            // 
            入庫者コード.BackColor = Color.White;
            入庫者コード.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            入庫者コード.FormattingEnabled = true;
            入庫者コード.ImeMode = ImeMode.Disable;
            入庫者コード.Location = new Point(107, 93);
            入庫者コード.MaxDropDownItems = 9;
            入庫者コード.Name = "入庫者コード";
            入庫者コード.Size = new Size(65, 21);
            入庫者コード.TabIndex = 6;
            入庫者コード.DrawItem += 入庫者コード_DrawItem;
            入庫者コード.SelectedIndexChanged += 入庫者コード_SelectedIndexChanged;
            入庫者コード.TextChanged += 入庫者コード_TextChanged;
            入庫者コード.Validating += 入庫者コード_Validating;
            // 
            // 入庫者コード_ラベル
            // 
            入庫者コード_ラベル.AllowDrop = true;
            入庫者コード_ラベル.AutoEllipsis = true;
            入庫者コード_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            入庫者コード_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            入庫者コード_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            入庫者コード_ラベル.Location = new Point(9, 96);
            入庫者コード_ラベル.Margin = new Padding(0);
            入庫者コード_ラベル.Name = "入庫者コード_ラベル";
            入庫者コード_ラベル.Size = new Size(95, 19);
            入庫者コード_ラベル.TabIndex = 5;
            入庫者コード_ラベル.Text = "入庫者(&M)";
            入庫者コード_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 入庫日
            // 
            入庫日.BackColor = Color.White;
            入庫日.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            入庫日.ImeMode = ImeMode.Disable;
            入庫日.Location = new Point(107, 71);
            入庫日.Margin = new Padding(3, 2, 3, 2);
            入庫日.Name = "入庫日";
            入庫日.Size = new Size(116, 20);
            入庫日.TabIndex = 4;
            入庫日.TextChanged += 入庫日_TextChanged;
            入庫日.KeyPress += 入庫日_KeyPress;
            入庫日.Validating += 入庫日_Validating;
            入庫日.Validated += 入庫日_Validated;
            // 
            // 入庫日_ラベル
            // 
            入庫日_ラベル.AllowDrop = true;
            入庫日_ラベル.AutoEllipsis = true;
            入庫日_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            入庫日_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            入庫日_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            入庫日_ラベル.Location = new Point(9, 73);
            入庫日_ラベル.Margin = new Padding(0);
            入庫日_ラベル.Name = "入庫日_ラベル";
            入庫日_ラベル.Size = new Size(95, 17);
            入庫日_ラベル.TabIndex = 3;
            入庫日_ラベル.Text = "入庫日(&D)";
            入庫日_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // SupplierCloseDay
            // 
            SupplierCloseDay.BackColor = SystemColors.Window;
            SupplierCloseDay.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            SupplierCloseDay.ImeMode = ImeMode.NoControl;
            SupplierCloseDay.Location = new Point(453, 94);
            SupplierCloseDay.Margin = new Padding(3, 2, 3, 2);
            SupplierCloseDay.Name = "SupplierCloseDay";
            SupplierCloseDay.Size = new Size(85, 20);
            SupplierCloseDay.TabIndex = 10128;
            SupplierCloseDay.TabStop = false;
            // 
            // 締日_ラベル
            // 
            締日_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            締日_ラベル.Location = new Point(365, 96);
            締日_ラベル.Name = "締日_ラベル";
            締日_ラベル.Size = new Size(88, 19);
            締日_ラベル.TabIndex = 10127;
            締日_ラベル.Text = "締日";
            締日_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 仕入先名_ラベル
            // 
            仕入先名_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            仕入先名_ラベル.Location = new Point(365, 73);
            仕入先名_ラベル.Name = "仕入先名_ラベル";
            仕入先名_ラベル.Size = new Size(88, 19);
            仕入先名_ラベル.TabIndex = 10126;
            仕入先名_ラベル.Text = "仕入先名";
            仕入先名_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 仕入先名
            // 
            仕入先名.BackColor = SystemColors.Window;
            仕入先名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            仕入先名.ImeMode = ImeMode.Hiragana;
            仕入先名.Location = new Point(453, 71);
            仕入先名.Margin = new Padding(3, 2, 3, 2);
            仕入先名.Name = "仕入先名";
            仕入先名.Size = new Size(306, 20);
            仕入先名.TabIndex = 10125;
            仕入先名.TabStop = false;
            // 
            // 仕入先コード_ラベル
            // 
            仕入先コード_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            仕入先コード_ラベル.Location = new Point(365, 50);
            仕入先コード_ラベル.Name = "仕入先コード_ラベル";
            仕入先コード_ラベル.Size = new Size(88, 19);
            仕入先コード_ラベル.TabIndex = 10124;
            仕入先コード_ラベル.Text = "仕入先コード";
            仕入先コード_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 仕入先コード
            // 
            仕入先コード.BackColor = SystemColors.Window;
            仕入先コード.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            仕入先コード.ImeMode = ImeMode.NoControl;
            仕入先コード.Location = new Point(453, 48);
            仕入先コード.Margin = new Padding(3, 2, 3, 2);
            仕入先コード.Name = "仕入先コード";
            仕入先コード.Size = new Size(202, 20);
            仕入先コード.TabIndex = 10123;
            仕入先コード.TabStop = false;
            // 
            // 入庫日選択ボタン
            // 
            入庫日選択ボタン.Font = new Font("Yu Gothic UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            入庫日選択ボタン.Location = new Point(224, 70);
            入庫日選択ボタン.Margin = new Padding(4);
            入庫日選択ボタン.Name = "入庫日選択ボタン";
            入庫日選択ボタン.Size = new Size(22, 22);
            入庫日選択ボタン.TabIndex = 10122;
            入庫日選択ボタン.TabStop = false;
            入庫日選択ボタン.Text = "▼";
            入庫日選択ボタン.TextAlign = ContentAlignment.TopCenter;
            入庫日選択ボタン.UseVisualStyleBackColor = true;
            入庫日選択ボタン.Click += 入庫日選択ボタン_Click;
            // 
            // 入庫コード
            // 
            入庫コード.BackColor = Color.FromArgb(255, 255, 153);
            入庫コード.Enabled = false;
            入庫コード.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            入庫コード.FormattingEnabled = true;
            入庫コード.ImeMode = ImeMode.Disable;
            入庫コード.Location = new Point(107, 48);
            入庫コード.Name = "入庫コード";
            入庫コード.Size = new Size(139, 21);
            入庫コード.TabIndex = 2;
            入庫コード.SelectedIndexChanged += 入庫コード_SelectedIndexChanged;
            入庫コード.TextChanged += 入庫コード_TextChanged;
            入庫コード.KeyDown += 入庫コード_KeyDown;
            入庫コード.KeyPress += 入庫コード_KeyPress;
            入庫コード.Validating += 入庫コード_Validating;
            入庫コード.Validated += 入庫コード_Validated;
            // 
            // 入庫コードラベル
            // 
            入庫コードラベル.AllowDrop = true;
            入庫コードラベル.AutoEllipsis = true;
            入庫コードラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            入庫コードラベル.ForeColor = SystemColors.ActiveCaptionText;
            入庫コードラベル.ImageAlign = ContentAlignment.MiddleLeft;
            入庫コードラベル.Location = new Point(9, 50);
            入庫コードラベル.Margin = new Padding(0);
            入庫コードラベル.Name = "入庫コードラベル";
            入庫コードラベル.Size = new Size(95, 19);
            入庫コードラベル.TabIndex = 1;
            入庫コードラベル.Text = "入庫コード(&C)";
            入庫コードラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 仕入先2_ラベル
            // 
            仕入先2_ラベル.AllowDrop = true;
            仕入先2_ラベル.AutoEllipsis = true;
            仕入先2_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            仕入先2_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            仕入先2_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            仕入先2_ラベル.Location = new Point(14, 50);
            仕入先2_ラベル.Margin = new Padding(0);
            仕入先2_ラベル.Name = "仕入先2_ラベル";
            仕入先2_ラベル.Size = new Size(132, 17);
            仕入先2_ラベル.TabIndex = 10121;
            仕入先2_ラベル.Text = "仕入先2";
            仕入先2_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 購買コード
            // 
            購買コード.BackColor = SystemColors.Window;
            購買コード.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            購買コード.ImeMode = ImeMode.NoControl;
            購買コード.Location = new Point(453, 169);
            購買コード.Margin = new Padding(3, 2, 3, 2);
            購買コード.Name = "購買コード";
            購買コード.Size = new Size(202, 20);
            購買コード.TabIndex = 10145;
            購買コード.TabStop = false;
            // 
            // 購買コード_ラベル
            // 
            購買コード_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            購買コード_ラベル.Location = new Point(365, 173);
            購買コード_ラベル.Name = "購買コード_ラベル";
            購買コード_ラベル.Size = new Size(88, 19);
            購買コード_ラベル.TabIndex = 10144;
            購買コード_ラベル.Text = "購買コード";
            購買コード_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 担当者名_ラベル
            // 
            担当者名_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            担当者名_ラベル.Location = new Point(365, 147);
            担当者名_ラベル.Name = "担当者名_ラベル";
            担当者名_ラベル.Size = new Size(88, 19);
            担当者名_ラベル.TabIndex = 10143;
            担当者名_ラベル.Text = "担当者名";
            担当者名_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 仕入先担当者名
            // 
            仕入先担当者名.BackColor = SystemColors.Window;
            仕入先担当者名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            仕入先担当者名.ImeMode = ImeMode.Hiragana;
            仕入先担当者名.Location = new Point(453, 145);
            仕入先担当者名.Margin = new Padding(3, 2, 3, 2);
            仕入先担当者名.Name = "仕入先担当者名";
            仕入先担当者名.Size = new Size(306, 20);
            仕入先担当者名.TabIndex = 10142;
            仕入先担当者名.TabStop = false;
            // 
            // 窓口電話番号_ラベル
            // 
            窓口電話番号_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            窓口電話番号_ラベル.Location = new Point(365, 121);
            窓口電話番号_ラベル.Name = "窓口電話番号_ラベル";
            窓口電話番号_ラベル.Size = new Size(88, 19);
            窓口電話番号_ラベル.TabIndex = 10141;
            窓口電話番号_ラベル.Text = "窓口電話番号";
            窓口電話番号_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 仕入先窓口電話番号
            // 
            仕入先窓口電話番号.BackColor = SystemColors.Window;
            仕入先窓口電話番号.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            仕入先窓口電話番号.ImeMode = ImeMode.NoControl;
            仕入先窓口電話番号.Location = new Point(453, 119);
            仕入先窓口電話番号.Margin = new Padding(3, 2, 3, 2);
            仕入先窓口電話番号.Name = "仕入先窓口電話番号";
            仕入先窓口電話番号.Size = new Size(202, 20);
            仕入先窓口電話番号.TabIndex = 10140;
            仕入先窓口電話番号.TabStop = false;
            // 
            // 発注コード
            // 
            発注コード.BackColor = Color.White;
            発注コード.DrawMode = DrawMode.OwnerDrawFixed;
            発注コード.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            発注コード.FormattingEnabled = true;
            発注コード.ImeMode = ImeMode.Disable;
            発注コード.Location = new Point(107, 118);
            発注コード.MaxDropDownItems = 9;
            発注コード.Name = "発注コード";
            発注コード.Size = new Size(139, 21);
            発注コード.TabIndex = 8;
            発注コード.DrawItem += 発注コード_DrawItem;
            発注コード.SelectedIndexChanged += 発注コード_SelectedIndexChanged;
            発注コード.TextChanged += 発注コード_TextChanged;
            発注コード.Enter += 発注コード_Enter;
            発注コード.KeyDown += 発注コード_KeyDown;
            発注コード.KeyPress += 発注コード_KeyPress;
            発注コード.Leave += 発注コード_Leave;
            発注コード.Validating += 発注コード_Validating;
            // 
            // 発注コード_ラベル
            // 
            発注コード_ラベル.AllowDrop = true;
            発注コード_ラベル.AutoEllipsis = true;
            発注コード_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            発注コード_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            発注コード_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            発注コード_ラベル.Location = new Point(9, 121);
            発注コード_ラベル.Margin = new Padding(0);
            発注コード_ラベル.Name = "発注コード_ラベル";
            発注コード_ラベル.Size = new Size(95, 17);
            発注コード_ラベル.TabIndex = 7;
            発注コード_ラベル.Text = "発注コード(&O)";
            発注コード_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 版数_ラベル
            // 
            版数_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            版数_ラベル.Location = new Point(252, 121);
            版数_ラベル.Name = "版数_ラベル";
            版数_ラベル.Size = new Size(34, 17);
            版数_ラベル.TabIndex = 10149;
            版数_ラベル.Text = "版数";
            版数_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 発注版数
            // 
            発注版数.BackColor = SystemColors.Window;
            発注版数.Enabled = false;
            発注版数.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            発注版数.ImeMode = ImeMode.NoControl;
            発注版数.Location = new Point(287, 118);
            発注版数.Margin = new Padding(3, 2, 3, 2);
            発注版数.Name = "発注版数";
            発注版数.Size = new Size(46, 20);
            発注版数.TabIndex = 10148;
            発注版数.TabStop = false;
            // 
            // 集計年月
            // 
            集計年月.BackColor = Color.White;
            集計年月.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            集計年月.FormattingEnabled = true;
            集計年月.ImeMode = ImeMode.Disable;
            集計年月.Location = new Point(107, 144);
            集計年月.MaxDropDownItems = 9;
            集計年月.Name = "集計年月";
            集計年月.Size = new Size(139, 21);
            集計年月.TabIndex = 10;
            集計年月.SelectedIndexChanged += 集計年月_SelectedIndexChanged;
            集計年月.TextChanged += 集計年月_TextChanged;
            集計年月.Enter += 集計年月_Enter;
            集計年月.Validating += 集計年月_Validating;
            // 
            // 集計年月_ラベル
            // 
            集計年月_ラベル.AllowDrop = true;
            集計年月_ラベル.AutoEllipsis = true;
            集計年月_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            集計年月_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            集計年月_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            集計年月_ラベル.Location = new Point(9, 147);
            集計年月_ラベル.Margin = new Padding(0);
            集計年月_ラベル.Name = "集計年月_ラベル";
            集計年月_ラベル.Size = new Size(95, 20);
            集計年月_ラベル.TabIndex = 9;
            集計年月_ラベル.Text = "集計年月(&A)";
            集計年月_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 支払年月
            // 
            支払年月.BackColor = Color.White;
            支払年月.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            支払年月.FormattingEnabled = true;
            支払年月.ImeMode = ImeMode.Disable;
            支払年月.Location = new Point(107, 170);
            支払年月.MaxDropDownItems = 9;
            支払年月.Name = "支払年月";
            支払年月.Size = new Size(139, 21);
            支払年月.TabIndex = 12;
            支払年月.SelectedIndexChanged += 支払年月_SelectedIndexChanged;
            支払年月.TextChanged += 支払年月_TextChanged;
            支払年月.Enter += 支払年月_Enter;
            支払年月.Validating += 支払年月_Validating;
            // 
            // 支払年月_ラベル
            // 
            支払年月_ラベル.AllowDrop = true;
            支払年月_ラベル.AutoEllipsis = true;
            支払年月_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            支払年月_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            支払年月_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            支払年月_ラベル.Location = new Point(9, 171);
            支払年月_ラベル.Margin = new Padding(0);
            支払年月_ラベル.Name = "支払年月_ラベル";
            支払年月_ラベル.Size = new Size(95, 18);
            支払年月_ラベル.TabIndex = 11;
            支払年月_ラベル.Text = "支払年月(&P)";
            支払年月_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TaxRate
            // 
            TaxRate.BackColor = SystemColors.Window;
            TaxRate.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            TaxRate.ImeMode = ImeMode.Disable;
            TaxRate.Location = new Point(107, 246);
            TaxRate.Margin = new Padding(3, 2, 3, 2);
            TaxRate.Name = "TaxRate";
            TaxRate.Size = new Size(116, 20);
            TaxRate.TabIndex = 16;
            TaxRate.TextAlign = HorizontalAlignment.Right;
            // 
            // 消費税率_ラベル
            // 
            消費税率_ラベル.AllowDrop = true;
            消費税率_ラベル.AutoEllipsis = true;
            消費税率_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            消費税率_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            消費税率_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            消費税率_ラベル.Location = new Point(9, 246);
            消費税率_ラベル.Margin = new Padding(0);
            消費税率_ラベル.Name = "消費税率_ラベル";
            消費税率_ラベル.Size = new Size(95, 19);
            消費税率_ラベル.TabIndex = 15;
            消費税率_ラベル.Text = "消費税率(&T)";
            消費税率_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ロット番号_ラベル
            // 
            ロット番号_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            ロット番号_ラベル.Location = new Point(365, 222);
            ロット番号_ラベル.Name = "ロット番号_ラベル";
            ロット番号_ラベル.Size = new Size(88, 19);
            ロット番号_ラベル.TabIndex = 10151;
            ロット番号_ラベル.Text = "ロット番号";
            ロット番号_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // シリーズ名_ラベル
            // 
            シリーズ名_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            シリーズ名_ラベル.Location = new Point(365, 197);
            シリーズ名_ラベル.Name = "シリーズ名_ラベル";
            シリーズ名_ラベル.Size = new Size(88, 19);
            シリーズ名_ラベル.TabIndex = 10150;
            シリーズ名_ラベル.Text = "シリーズ名";
            シリーズ名_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // シリーズ名
            // 
            シリーズ名.BackColor = SystemColors.Window;
            シリーズ名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            シリーズ名.ImeMode = ImeMode.Hiragana;
            シリーズ名.Location = new Point(453, 197);
            シリーズ名.Margin = new Padding(3, 2, 3, 2);
            シリーズ名.Name = "シリーズ名";
            シリーズ名.Size = new Size(202, 20);
            シリーズ名.TabIndex = 10152;
            シリーズ名.TabStop = false;
            // 
            // ロット番号1
            // 
            ロット番号1.BackColor = SystemColors.Window;
            ロット番号1.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            ロット番号1.ImeMode = ImeMode.NoControl;
            ロット番号1.Location = new Point(453, 222);
            ロット番号1.Margin = new Padding(3, 2, 3, 2);
            ロット番号1.Name = "ロット番号1";
            ロット番号1.Size = new Size(85, 20);
            ロット番号1.TabIndex = 10153;
            ロット番号1.TabStop = false;
            // 
            // ロット番号2
            // 
            ロット番号2.BackColor = SystemColors.Window;
            ロット番号2.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            ロット番号2.ImeMode = ImeMode.NoControl;
            ロット番号2.Location = new Point(570, 222);
            ロット番号2.Margin = new Padding(3, 2, 3, 2);
            ロット番号2.Name = "ロット番号2";
            ロット番号2.Size = new Size(85, 20);
            ロット番号2.TabIndex = 10154;
            ロット番号2.TabStop = false;
            // 
            // label1
            // 
            label1.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(539, 223);
            label1.Name = "label1";
            label1.Size = new Size(30, 20);
            label1.TabIndex = 10155;
            label1.Text = "～";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(770, 147);
            label5.Name = "label5";
            label5.Size = new Size(72, 17);
            label5.TabIndex = 10165;
            label5.Text = "棚卸コード";
            label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 棚卸コード
            // 
            棚卸コード.BackColor = SystemColors.Window;
            棚卸コード.Enabled = false;
            棚卸コード.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            棚卸コード.ImeMode = ImeMode.NoControl;
            棚卸コード.Location = new Point(858, 145);
            棚卸コード.Margin = new Padding(3, 2, 3, 2);
            棚卸コード.Name = "棚卸コード";
            棚卸コード.Size = new Size(161, 20);
            棚卸コード.TabIndex = 10164;
            棚卸コード.TabStop = false;
            // 
            // label6
            // 
            label6.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(770, 121);
            label6.Name = "label6";
            label6.Size = new Size(88, 17);
            label6.TabIndex = 10163;
            label6.Text = "削除";
            label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 削除
            // 
            削除.BackColor = SystemColors.Window;
            削除.Enabled = false;
            削除.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            削除.ImeMode = ImeMode.NoControl;
            削除.Location = new Point(858, 119);
            削除.Margin = new Padding(3, 2, 3, 2);
            削除.Name = "削除";
            削除.Size = new Size(30, 20);
            削除.TabIndex = 10162;
            削除.TabStop = false;
            // 
            // 確定
            // 
            確定.BackColor = SystemColors.Window;
            確定.Enabled = false;
            確定.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            確定.ImeMode = ImeMode.NoControl;
            確定.Location = new Point(858, 94);
            確定.Margin = new Padding(3, 2, 3, 2);
            確定.Name = "確定";
            確定.Size = new Size(30, 20);
            確定.TabIndex = 10161;
            確定.TabStop = false;
            // 
            // label7
            // 
            label7.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Location = new Point(770, 96);
            label7.Name = "label7";
            label7.Size = new Size(72, 17);
            label7.TabIndex = 10160;
            label7.Text = "確定";
            label7.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            label8.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label8.Location = new Point(770, 73);
            label8.Name = "label8";
            label8.Size = new Size(72, 17);
            label8.TabIndex = 10159;
            label8.Text = "登録者";
            label8.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 登録者コード
            // 
            登録者コード.BackColor = SystemColors.Window;
            登録者コード.Enabled = false;
            登録者コード.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            登録者コード.ImeMode = ImeMode.NoControl;
            登録者コード.Location = new Point(858, 71);
            登録者コード.Margin = new Padding(3, 2, 3, 2);
            登録者コード.Name = "登録者コード";
            登録者コード.Size = new Size(40, 20);
            登録者コード.TabIndex = 10158;
            登録者コード.TabStop = false;
            // 
            // label9
            // 
            label9.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label9.Location = new Point(770, 50);
            label9.Name = "label9";
            label9.Size = new Size(72, 17);
            label9.TabIndex = 10157;
            label9.Text = "登録日時";
            label9.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 登録日時
            // 
            登録日時.BackColor = SystemColors.Window;
            登録日時.Enabled = false;
            登録日時.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            登録日時.ImeMode = ImeMode.NoControl;
            登録日時.Location = new Point(858, 48);
            登録日時.Margin = new Padding(3, 2, 3, 2);
            登録日時.Name = "登録日時";
            登録日時.Size = new Size(161, 20);
            登録日時.TabIndex = 10156;
            登録日時.TabStop = false;
            // 
            // 登録者名
            // 
            登録者名.BackColor = SystemColors.Window;
            登録者名.Enabled = false;
            登録者名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            登録者名.ImeMode = ImeMode.NoControl;
            登録者名.Location = new Point(897, 71);
            登録者名.Margin = new Padding(3, 2, 3, 2);
            登録者名.Name = "登録者名";
            登録者名.Size = new Size(122, 20);
            登録者名.TabIndex = 10166;
            登録者名.TabStop = false;
            // 
            // 確定日時
            // 
            確定日時.BackColor = SystemColors.Control;
            確定日時.Enabled = false;
            確定日時.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            確定日時.ImeMode = ImeMode.NoControl;
            確定日時.Location = new Point(770, 173);
            確定日時.Margin = new Padding(3, 2, 3, 2);
            確定日時.Name = "確定日時";
            確定日時.Size = new Size(40, 20);
            確定日時.TabIndex = 10167;
            確定日時.TabStop = false;
            確定日時.Visible = false;
            // 
            // 確定者コード
            // 
            確定者コード.BackColor = SystemColors.Control;
            確定者コード.Enabled = false;
            確定者コード.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            確定者コード.ImeMode = ImeMode.NoControl;
            確定者コード.Location = new Point(815, 173);
            確定者コード.Margin = new Padding(3, 2, 3, 2);
            確定者コード.Name = "確定者コード";
            確定者コード.Size = new Size(40, 20);
            確定者コード.TabIndex = 10168;
            確定者コード.TabStop = false;
            確定者コード.Visible = false;
            // 
            // 無効者コード
            // 
            無効者コード.BackColor = SystemColors.Control;
            無効者コード.Enabled = false;
            無効者コード.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            無効者コード.ImeMode = ImeMode.NoControl;
            無効者コード.Location = new Point(815, 199);
            無効者コード.Margin = new Padding(3, 2, 3, 2);
            無効者コード.Name = "無効者コード";
            無効者コード.Size = new Size(40, 20);
            無効者コード.TabIndex = 10170;
            無効者コード.TabStop = false;
            無効者コード.Visible = false;
            // 
            // 無効日時
            // 
            無効日時.BackColor = SystemColors.Control;
            無効日時.Enabled = false;
            無効日時.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            無効日時.ImeMode = ImeMode.NoControl;
            無効日時.Location = new Point(770, 199);
            無効日時.Margin = new Padding(3, 2, 3, 2);
            無効日時.Name = "無効日時";
            無効日時.Size = new Size(40, 20);
            無効日時.TabIndex = 10169;
            無効日時.TabStop = false;
            無効日時.Visible = false;
            // 
            // 入庫明細1
            // 
            入庫明細1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            入庫明細1.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            入庫明細1.Location = new Point(9, 275);
            入庫明細1.Margin = new Padding(4, 4, 4, 4);
            入庫明細1.Name = "入庫明細1";
            入庫明細1.Size = new Size(1027, 326);
            入庫明細1.TabIndex = 10171;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, toolStripStatusLabel2 });
            statusStrip1.Location = new Point(0, 605);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 19, 0);
            statusStrip1.Size = new Size(1036, 22);
            statusStrip1.TabIndex = 10172;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(0, 17);
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new Size(89, 17);
            toolStripStatusLabel2.Text = "各種項目の説明";
            // 
            // 買掛区分コード設定
            // 
            買掛区分コード設定.BackColor = Color.White;
            買掛区分コード設定.DrawMode = DrawMode.OwnerDrawFixed;
            買掛区分コード設定.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            買掛区分コード設定.FormattingEnabled = true;
            買掛区分コード設定.ImeMode = ImeMode.Disable;
            買掛区分コード設定.Location = new Point(560, 338);
            買掛区分コード設定.MaxDropDownItems = 9;
            買掛区分コード設定.Name = "買掛区分コード設定";
            買掛区分コード設定.Size = new Size(140, 21);
            買掛区分コード設定.TabIndex = 10173;
            買掛区分コード設定.DrawItem += 買掛区分コード設定_DrawItem;
            買掛区分コード設定.SelectedIndexChanged += 買掛区分コード設定_SelectedIndexChanged;
            買掛区分コード設定.Enter += 買掛区分コード設定_Enter;
            買掛区分コード設定.Leave += 買掛区分コード設定_Leave;
            // 
            // F_入庫
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = SystemColors.Control;
            ClientSize = new Size(1036, 627);
            Controls.Add(買掛区分コード設定);
            Controls.Add(statusStrip1);
            Controls.Add(入庫明細1);
            Controls.Add(無効者コード);
            Controls.Add(無効日時);
            Controls.Add(確定者コード);
            Controls.Add(確定日時);
            Controls.Add(登録者名);
            Controls.Add(label5);
            Controls.Add(棚卸コード);
            Controls.Add(label6);
            Controls.Add(削除);
            Controls.Add(確定);
            Controls.Add(label7);
            Controls.Add(label8);
            Controls.Add(登録者コード);
            Controls.Add(label9);
            Controls.Add(登録日時);
            Controls.Add(label1);
            Controls.Add(ロット番号2);
            Controls.Add(ロット番号1);
            Controls.Add(シリーズ名);
            Controls.Add(ロット番号_ラベル);
            Controls.Add(シリーズ名_ラベル);
            Controls.Add(TaxRate);
            Controls.Add(消費税率_ラベル);
            Controls.Add(支払年月);
            Controls.Add(支払年月_ラベル);
            Controls.Add(集計年月);
            Controls.Add(集計年月_ラベル);
            Controls.Add(版数_ラベル);
            Controls.Add(発注版数);
            Controls.Add(発注コード);
            Controls.Add(発注コード_ラベル);
            Controls.Add(購買コード);
            Controls.Add(購買コード_ラベル);
            Controls.Add(担当者名_ラベル);
            Controls.Add(仕入先担当者名);
            Controls.Add(窓口電話番号_ラベル);
            Controls.Add(仕入先窓口電話番号);
            Controls.Add(摘要);
            Controls.Add(摘要_ラベル);
            Controls.Add(入庫者名);
            Controls.Add(入庫者コード);
            Controls.Add(入庫者コード_ラベル);
            Controls.Add(入庫日);
            Controls.Add(入庫日_ラベル);
            Controls.Add(SupplierCloseDay);
            Controls.Add(締日_ラベル);
            Controls.Add(仕入先名_ラベル);
            Controls.Add(仕入先名);
            Controls.Add(仕入先コード_ラベル);
            Controls.Add(仕入先コード);
            Controls.Add(入庫日選択ボタン);
            Controls.Add(入庫コード);
            Controls.Add(入庫コードラベル);
            Controls.Add(仕入先2_ラベル);
            Controls.Add(コマンド終了);
            Controls.Add(コマンド登録);
            Controls.Add(コマンド);
            Controls.Add(コマンド注文書);
            Controls.Add(コマンド仕入先);
            Controls.Add(コマンド発注);
            Controls.Add(コマンド確定);
            Controls.Add(コマンド承認);
            Controls.Add(コマンド削除);
            Controls.Add(コマンド複写);
            Controls.Add(コマンド修正);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            ImeMode = ImeMode.Off;
            KeyPreview = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "F_入庫";
            StartPosition = FormStartPosition.CenterScreen;
            Text = " 入庫（発注）";
            FormClosing += Form_Unload;
            Load += Form_Load;
            Shown += F_入庫_Shown;
            KeyDown += Form_KeyDown;
            Resize += Form_Resize;
            panel1.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion


        private CalendarColumn calendarColumn1;


        private CalendarColumn calendarColumn2;
        private CalendarColumn calendarColumn3;
        private System.Windows.Forms.Button コマンド終了;
        private System.Windows.Forms.Button コマンド登録;
        private System.Windows.Forms.DataGridViewTextBoxColumn 日誌IDDataGridViewTextBoxColumn;

        private CalendarColumn 登録日;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button コマンド新規;
        private System.Windows.Forms.Button コマンド注文書;
        private System.Windows.Forms.Button コマンド仕入先;
        private System.Windows.Forms.Button コマンド発注;
        private System.Windows.Forms.Button コマンド確定;
        private System.Windows.Forms.Button コマンド承認;
        private System.Windows.Forms.Button コマンド削除;
        private System.Windows.Forms.Button コマンド複写;
        private System.Windows.Forms.Button コマンド修正;
       

       
        private Button コマンド;
        private TextBox 摘要;
        private Label 摘要_ラベル;
        private TextBox 入庫者名;
        private ComboBox 入庫者コード;
        private Label 入庫者コード_ラベル;
        private TextBox 入庫日;
        private Label 入庫日_ラベル;
        private TextBox SupplierCloseDay;
        private Label 締日_ラベル;
        private Label 仕入先名_ラベル;
        private TextBox 仕入先名;
        private Label 仕入先コード_ラベル;
        private TextBox 仕入先コード;
        private Button 入庫日選択ボタン;
        internal ComboBox 入庫コード;
        private Label 入庫コードラベル;
        private Label 仕入先2_ラベル;
        private TextBox 購買コード;
        private Label 購買コード_ラベル;
        private Label 担当者名_ラベル;
        private TextBox 仕入先担当者名;
        private Label 窓口電話番号_ラベル;
        private TextBox 仕入先窓口電話番号;
        private ComboBox 発注コード;
        private Label 発注コード_ラベル;
        private Label 版数_ラベル;
        private TextBox 発注版数;
        private ComboBox 集計年月;
        private Label 集計年月_ラベル;
        private ComboBox 支払年月;
        private Label 支払年月_ラベル;
        private TextBox TaxRate;
        private Label 消費税率_ラベル;
        private Label ロット番号_ラベル;
        private Label シリーズ名_ラベル;
        private TextBox シリーズ名;
        private TextBox ロット番号1;
        private TextBox ロット番号2;
        private Label label1;
        private Label label5;
        private TextBox 棚卸コード;
        private Label label6;
        private TextBox 削除;
        private TextBox 確定;
        private Label label7;
        private Label label8;
        private TextBox 登録者コード;
        private Label label9;
        private TextBox 登録日時;
        private TextBox 登録者名;
        private TextBox 確定日時;
        private TextBox 確定者コード;
        private TextBox 無効者コード;
        private TextBox 無効日時;
        private MultiRowDesigner.入庫明細 入庫明細1;
        private StatusStrip statusStrip1;
        internal ToolStripStatusLabel toolStripStatusLabel1;
        internal ToolStripStatusLabel toolStripStatusLabel2;
        private ComboBox 買掛区分コード設定;
        private ToolTip toolTip1;
    }
}


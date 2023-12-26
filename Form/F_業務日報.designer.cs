
namespace u_net
{
    partial class F_業務日報
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
            コマンド終了 = new Button();
            コマンド登録 = new Button();
            panel1 = new Panel();
            コマンド新規 = new Button();
            コマンドF7 = new Button();
            コマンド詳細 = new Button();
            コマンド社員 = new Button();
            コマンド確定 = new Button();
            コマンド承認 = new Button();
            コマンド削除 = new Button();
            コマンド複写 = new Button();
            コマンド読込 = new Button();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn8 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn9 = new DataGridViewTextBoxColumn();
            日誌IDDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn10 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn11 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn12 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn13 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn14 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn15 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn16 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn17 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn18 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn19 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn20 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn21 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn22 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn23 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn24 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn25 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn26 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn27 = new DataGridViewTextBoxColumn();
            コマンドF8 = new Button();
            確定_ラベル = new Label();
            氏名_ラベル = new Label();
            日付 = new ComboBox();
            日付_ラベル = new Label();
            仕入先2_ラベル = new Label();
            日報コード_ラベル = new Label();
            登録日_ラベル = new Label();
            日報コード = new TextBox();
            確定コード = new TextBox();
            確定者コード = new TextBox();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            label5 = new Label();
            日付選択ボタン = new Button();
            社員コード = new ComboBox();
            日付旧 = new TextBox();
            社員コード旧 = new TextBox();
            登録日 = new TextBox();
            登録者名_ラベル = new Label();
            登録者コード = new TextBox();
            登録者名 = new TextBox();
            確定日時 = new TextBox();
            確定者名 = new TextBox();
            状況 = new ListBox();
            業務日報明細実績1 = new MultiRowDesigner.業務日報明細実績();
            業務日報明細予定1 = new MultiRowDesigner.業務日報明細予定();
            label1 = new Label();
            本日の一言 = new TextBox();
            toolTip1 = new ToolTip(components);
            ログインユーザーボタン = new Button();
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
            toolTip1.SetToolTip(コマンド終了, "終了");
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
            toolTip1.SetToolTip(コマンド登録, "登録");
            コマンド登録.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaption;
            panel1.Controls.Add(コマンド新規);
            panel1.Location = new Point(0, 1);
            panel1.Margin = new Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(889, 42);
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
            コマンド新規.UseVisualStyleBackColor = false;
            // 
            // コマンドF7
            // 
            コマンドF7.Enabled = false;
            コマンドF7.Font = new Font("BIZ UDPゴシック", 8F, FontStyle.Regular, GraphicsUnit.Point);
            コマンドF7.ForeColor = Color.Blue;
            コマンドF7.ImageAlign = ContentAlignment.BottomLeft;
            コマンドF7.Location = new Point(440, 5);
            コマンドF7.Margin = new Padding(3, 2, 3, 2);
            コマンドF7.Name = "コマンドF7";
            コマンドF7.Size = new Size(70, 20);
            コマンドF7.TabIndex = 1010;
            コマンドF7.TabStop = false;
            コマンドF7.UseVisualStyleBackColor = true;
            // 
            // コマンド詳細
            // 
            コマンド詳細.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド詳細.ForeColor = Color.Blue;
            コマンド詳細.ImageAlign = ContentAlignment.BottomLeft;
            コマンド詳細.Location = new Point(370, 5);
            コマンド詳細.Margin = new Padding(3, 2, 3, 2);
            コマンド詳細.Name = "コマンド詳細";
            コマンド詳細.Size = new Size(70, 20);
            コマンド詳細.TabIndex = 1009;
            コマンド詳細.TabStop = false;
            コマンド詳細.Text = "詳細";
            コマンド詳細.UseVisualStyleBackColor = true;
            コマンド詳細.Click += コマンド詳細_Click;
            // 
            // コマンド社員
            // 
            コマンド社員.Enabled = false;
            コマンド社員.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド社員.ForeColor = Color.Blue;
            コマンド社員.ImageAlign = ContentAlignment.BottomLeft;
            コマンド社員.Location = new Point(300, 5);
            コマンド社員.Margin = new Padding(3, 2, 3, 2);
            コマンド社員.Name = "コマンド社員";
            コマンド社員.Size = new Size(70, 20);
            コマンド社員.TabIndex = 1008;
            コマンド社員.TabStop = false;
            コマンド社員.Text = "社員";
            コマンド社員.UseVisualStyleBackColor = true;
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
            toolTip1.SetToolTip(コマンド確定, "確定登録");
            コマンド確定.UseVisualStyleBackColor = true;
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
            toolTip1.SetToolTip(コマンド承認, "表示データを承認します。");
            コマンド承認.UseVisualStyleBackColor = true;
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
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.DataPropertyName = "商品コード";
            dataGridViewTextBoxColumn1.HeaderText = "商品コード";
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.DataPropertyName = "Revision";
            dataGridViewTextBoxColumn2.HeaderText = "Revision";
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.DataPropertyName = "明細番号";
            dataGridViewTextBoxColumn3.HeaderText = "明細番号";
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.DataPropertyName = "型式番号";
            dataGridViewTextBoxColumn4.HeaderText = "型式番号";
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewTextBoxColumn5.DataPropertyName = "型式名";
            dataGridViewTextBoxColumn5.HeaderText = "型式名";
            dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewTextBoxColumn6.DataPropertyName = "定価";
            dataGridViewTextBoxColumn6.HeaderText = "定価";
            dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewTextBoxColumn7
            // 
            dataGridViewTextBoxColumn7.DataPropertyName = "原価";
            dataGridViewTextBoxColumn7.HeaderText = "原価";
            dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewTextBoxColumn8
            // 
            dataGridViewTextBoxColumn8.DataPropertyName = "機能";
            dataGridViewTextBoxColumn8.HeaderText = "機能";
            dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            // 
            // dataGridViewTextBoxColumn9
            // 
            dataGridViewTextBoxColumn9.DataPropertyName = "構成番号";
            dataGridViewTextBoxColumn9.HeaderText = "構成番号";
            dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            // 
            // 日誌IDDataGridViewTextBoxColumn
            // 
            日誌IDDataGridViewTextBoxColumn.Name = "日誌IDDataGridViewTextBoxColumn";
            // 
            // dataGridViewTextBoxColumn10
            // 
            dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            // 
            // dataGridViewTextBoxColumn11
            // 
            dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            // 
            // dataGridViewTextBoxColumn12
            // 
            dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            // 
            // dataGridViewTextBoxColumn13
            // 
            dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            // 
            // dataGridViewTextBoxColumn14
            // 
            dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            // 
            // dataGridViewTextBoxColumn15
            // 
            dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            // 
            // dataGridViewTextBoxColumn16
            // 
            dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            // 
            // dataGridViewTextBoxColumn17
            // 
            dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
            // 
            // dataGridViewTextBoxColumn18
            // 
            dataGridViewTextBoxColumn18.Name = "dataGridViewTextBoxColumn18";
            // 
            // dataGridViewTextBoxColumn19
            // 
            dataGridViewTextBoxColumn19.Name = "dataGridViewTextBoxColumn19";
            // 
            // dataGridViewTextBoxColumn20
            // 
            dataGridViewTextBoxColumn20.Name = "dataGridViewTextBoxColumn20";
            // 
            // dataGridViewTextBoxColumn21
            // 
            dataGridViewTextBoxColumn21.Name = "dataGridViewTextBoxColumn21";
            // 
            // dataGridViewTextBoxColumn22
            // 
            dataGridViewTextBoxColumn22.Name = "dataGridViewTextBoxColumn22";
            // 
            // dataGridViewTextBoxColumn23
            // 
            dataGridViewTextBoxColumn23.Name = "dataGridViewTextBoxColumn23";
            // 
            // dataGridViewTextBoxColumn24
            // 
            dataGridViewTextBoxColumn24.Name = "dataGridViewTextBoxColumn24";
            // 
            // dataGridViewTextBoxColumn25
            // 
            dataGridViewTextBoxColumn25.Name = "dataGridViewTextBoxColumn25";
            // 
            // dataGridViewTextBoxColumn26
            // 
            dataGridViewTextBoxColumn26.Name = "dataGridViewTextBoxColumn26";
            // 
            // dataGridViewTextBoxColumn27
            // 
            dataGridViewTextBoxColumn27.Name = "dataGridViewTextBoxColumn27";
            // 
            // コマンドF8
            // 
            コマンドF8.Enabled = false;
            コマンドF8.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンドF8.ForeColor = Color.Blue;
            コマンドF8.ImageAlign = ContentAlignment.BottomLeft;
            コマンドF8.Location = new Point(510, 5);
            コマンドF8.Margin = new Padding(3, 2, 3, 2);
            コマンドF8.Name = "コマンドF8";
            コマンドF8.Size = new Size(70, 20);
            コマンドF8.TabIndex = 1011;
            コマンドF8.TabStop = false;
            コマンドF8.UseVisualStyleBackColor = true;
            // 
            // 確定_ラベル
            // 
            確定_ラベル.AllowDrop = true;
            確定_ラベル.AutoEllipsis = true;
            確定_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            確定_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            確定_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            確定_ラベル.Location = new Point(9, 99);
            確定_ラベル.Margin = new Padding(0);
            確定_ラベル.Name = "確定_ラベル";
            確定_ラベル.Size = new Size(100, 17);
            確定_ラベル.TabIndex = 5;
            確定_ラベル.Text = "確定";
            確定_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 氏名_ラベル
            // 
            氏名_ラベル.AllowDrop = true;
            氏名_ラベル.AutoEllipsis = true;
            氏名_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            氏名_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            氏名_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            氏名_ラベル.Location = new Point(9, 72);
            氏名_ラベル.Margin = new Padding(0);
            氏名_ラベル.Name = "氏名_ラベル";
            氏名_ラベル.Size = new Size(100, 21);
            氏名_ラベル.TabIndex = 3;
            氏名_ラベル.Text = "氏名(&N)";
            氏名_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 日付
            // 
            日付.BackColor = SystemColors.Window;
            日付.DropDownHeight = 81;
            日付.DropDownWidth = 122;
            日付.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            日付.FormattingEnabled = true;
            日付.ImeMode = ImeMode.Disable;
            日付.IntegralHeight = false;
            日付.Location = new Point(122, 48);
            日付.Name = "日付";
            日付.Size = new Size(136, 21);
            日付.TabIndex = 2;
            日付.Enter += 日付_Enter;
            日付.Leave += 日付_Leave;
            // 
            // 日付_ラベル
            // 
            日付_ラベル.AllowDrop = true;
            日付_ラベル.AutoEllipsis = true;
            日付_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            日付_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            日付_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            日付_ラベル.Location = new Point(9, 47);
            日付_ラベル.Margin = new Padding(0);
            日付_ラベル.Name = "日付_ラベル";
            日付_ラベル.Size = new Size(108, 21);
            日付_ラベル.TabIndex = 1;
            日付_ラベル.Text = "日付(&D)";
            日付_ラベル.TextAlign = ContentAlignment.MiddleLeft;
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
            // 日報コード_ラベル
            // 
            日報コード_ラベル.AllowDrop = true;
            日報コード_ラベル.AutoEllipsis = true;
            日報コード_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            日報コード_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            日報コード_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            日報コード_ラベル.Location = new Point(9, 124);
            日報コード_ラベル.Margin = new Padding(0);
            日報コード_ラベル.Name = "日報コード_ラベル";
            日報コード_ラベル.Size = new Size(100, 20);
            日報コード_ラベル.TabIndex = 6;
            日報コード_ラベル.Text = "日報コード";
            日報コード_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            日報コード_ラベル.Visible = false;
            // 
            // 登録日_ラベル
            // 
            登録日_ラベル.AllowDrop = true;
            登録日_ラベル.AutoEllipsis = true;
            登録日_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            登録日_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            登録日_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            登録日_ラベル.Location = new Point(9, 150);
            登録日_ラベル.Margin = new Padding(0);
            登録日_ラベル.Name = "登録日_ラベル";
            登録日_ラベル.Size = new Size(100, 20);
            登録日_ラベル.TabIndex = 8;
            登録日_ラベル.Text = "登録日";
            登録日_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            登録日_ラベル.Visible = false;
            // 
            // 日報コード
            // 
            日報コード.BackColor = Color.White;
            日報コード.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            日報コード.ImeMode = ImeMode.NoControl;
            日報コード.Location = new Point(122, 123);
            日報コード.Margin = new Padding(3, 2, 3, 2);
            日報コード.Name = "日報コード";
            日報コード.ReadOnly = true;
            日報コード.Size = new Size(136, 20);
            日報コード.TabIndex = 7;
            日報コード.Visible = false;
            // 
            // 確定コード
            // 
            確定コード.BackColor = Color.Black;
            確定コード.BorderStyle = BorderStyle.FixedSingle;
            確定コード.Enabled = false;
            確定コード.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            確定コード.ImeMode = ImeMode.NoControl;
            確定コード.Location = new Point(122, 98);
            確定コード.Margin = new Padding(3, 2, 3, 2);
            確定コード.Name = "確定コード";
            確定コード.Size = new Size(20, 20);
            確定コード.TabIndex = 10179;
            確定コード.TabStop = false;
            // 
            // 確定者コード
            // 
            確定者コード.BackColor = SystemColors.Control;
            確定者コード.Enabled = false;
            確定者コード.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            確定者コード.ImeMode = ImeMode.NoControl;
            確定者コード.Location = new Point(265, 120);
            確定者コード.Margin = new Padding(3, 2, 3, 2);
            確定者コード.Name = "確定者コード";
            確定者コード.ReadOnly = true;
            確定者コード.Size = new Size(20, 20);
            確定者コード.TabIndex = 10182;
            確定者コード.TabStop = false;
            確定者コード.Visible = false;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 620);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 19, 0);
            statusStrip1.Size = new Size(888, 22);
            statusStrip1.TabIndex = 10195;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(89, 17);
            toolStripStatusLabel1.Text = "各種項目の説明";
            // 
            // label5
            // 
            label5.AllowDrop = true;
            label5.AutoEllipsis = true;
            label5.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label5.ForeColor = Color.Red;
            label5.ImageAlign = ContentAlignment.MiddleLeft;
            label5.Location = new Point(216, 205);
            label5.Margin = new Padding(0);
            label5.Name = "label5";
            label5.Size = new Size(444, 20);
            label5.TabIndex = 10196;
            label5.Text = "※この画面で参照できる日報データは2002年10月7日から2005年3月9日までです。";
            label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 日付選択ボタン
            // 
            日付選択ボタン.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            日付選択ボタン.Location = new Point(261, 49);
            日付選択ボタン.Margin = new Padding(4);
            日付選択ボタン.Name = "日付選択ボタン";
            日付選択ボタン.Size = new Size(21, 21);
            日付選択ボタン.TabIndex = 21009;
            日付選択ボタン.TabStop = false;
            日付選択ボタン.Text = "▼";
            toolTip1.SetToolTip(日付選択ボタン, "カレンダー");
            日付選択ボタン.UseVisualStyleBackColor = true;
            日付選択ボタン.Click += 日付選択ボタン_Click;
            // 
            // 社員コード
            // 
            社員コード.BackColor = SystemColors.Window;
            社員コード.DropDownHeight = 81;
            社員コード.DropDownWidth = 122;
            社員コード.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            社員コード.FormattingEnabled = true;
            社員コード.ImeMode = ImeMode.Disable;
            社員コード.IntegralHeight = false;
            社員コード.Location = new Point(122, 72);
            社員コード.Name = "社員コード";
            社員コード.Size = new Size(136, 21);
            社員コード.TabIndex = 4;
            社員コード.Enter += 社員コード_Enter;
            社員コード.Leave += 社員コード_Leave;
            // 
            // 日付旧
            // 
            日付旧.BackColor = SystemColors.Control;
            日付旧.Enabled = false;
            日付旧.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            日付旧.ImeMode = ImeMode.NoControl;
            日付旧.Location = new Point(148, 95);
            日付旧.Margin = new Padding(3, 2, 3, 2);
            日付旧.Name = "日付旧";
            日付旧.ReadOnly = true;
            日付旧.Size = new Size(46, 20);
            日付旧.TabIndex = 21010;
            日付旧.TabStop = false;
            日付旧.Visible = false;
            // 
            // 社員コード旧
            // 
            社員コード旧.BackColor = SystemColors.Control;
            社員コード旧.Enabled = false;
            社員コード旧.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            社員コード旧.ImeMode = ImeMode.NoControl;
            社員コード旧.Location = new Point(200, 95);
            社員コード旧.Margin = new Padding(3, 2, 3, 2);
            社員コード旧.Name = "社員コード旧";
            社員コード旧.ReadOnly = true;
            社員コード旧.Size = new Size(46, 20);
            社員コード旧.TabIndex = 21011;
            社員コード旧.TabStop = false;
            社員コード旧.Visible = false;
            // 
            // 登録日
            // 
            登録日.BackColor = Color.White;
            登録日.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            登録日.ImeMode = ImeMode.NoControl;
            登録日.Location = new Point(122, 150);
            登録日.Margin = new Padding(3, 2, 3, 2);
            登録日.Name = "登録日";
            登録日.ReadOnly = true;
            登録日.Size = new Size(136, 20);
            登録日.TabIndex = 9;
            登録日.Visible = false;
            // 
            // 登録者名_ラベル
            // 
            登録者名_ラベル.AllowDrop = true;
            登録者名_ラベル.AutoEllipsis = true;
            登録者名_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            登録者名_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            登録者名_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            登録者名_ラベル.Location = new Point(9, 177);
            登録者名_ラベル.Margin = new Padding(0);
            登録者名_ラベル.Name = "登録者名_ラベル";
            登録者名_ラベル.Size = new Size(100, 20);
            登録者名_ラベル.TabIndex = 10;
            登録者名_ラベル.Text = "登録者名";
            登録者名_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            登録者名_ラベル.Visible = false;
            // 
            // 登録者コード
            // 
            登録者コード.BackColor = Color.White;
            登録者コード.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            登録者コード.ImeMode = ImeMode.NoControl;
            登録者コード.Location = new Point(122, 178);
            登録者コード.Margin = new Padding(3, 2, 3, 2);
            登録者コード.Name = "登録者コード";
            登録者コード.ReadOnly = true;
            登録者コード.Size = new Size(43, 20);
            登録者コード.TabIndex = 11;
            登録者コード.Visible = false;
            // 
            // 登録者名
            // 
            登録者名.BackColor = Color.White;
            登録者名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            登録者名.ImeMode = ImeMode.NoControl;
            登録者名.Location = new Point(164, 178);
            登録者名.Margin = new Padding(3, 2, 3, 2);
            登録者名.Name = "登録者名";
            登録者名.ReadOnly = true;
            登録者名.Size = new Size(94, 20);
            登録者名.TabIndex = 12;
            登録者名.Visible = false;
            // 
            // 確定日時
            // 
            確定日時.BackColor = SystemColors.Control;
            確定日時.Enabled = false;
            確定日時.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            確定日時.ImeMode = ImeMode.NoControl;
            確定日時.Location = new Point(265, 97);
            確定日時.Margin = new Padding(3, 2, 3, 2);
            確定日時.Name = "確定日時";
            確定日時.ReadOnly = true;
            確定日時.Size = new Size(20, 20);
            確定日時.TabIndex = 21012;
            確定日時.TabStop = false;
            確定日時.Visible = false;
            // 
            // 確定者名
            // 
            確定者名.BackColor = SystemColors.Control;
            確定者名.Enabled = false;
            確定者名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            確定者名.ImeMode = ImeMode.NoControl;
            確定者名.Location = new Point(265, 144);
            確定者名.Margin = new Padding(3, 2, 3, 2);
            確定者名.Name = "確定者名";
            確定者名.ReadOnly = true;
            確定者名.Size = new Size(20, 20);
            確定者名.TabIndex = 21013;
            確定者名.TabStop = false;
            確定者名.Visible = false;
            // 
            // 状況
            // 
            状況.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            状況.FormattingEnabled = true;
            状況.Location = new Point(331, 50);
            状況.Name = "状況";
            状況.Size = new Size(534, 147);
            状況.TabIndex = 13;
            状況.Enter += 状況_Enter;
            状況.Leave += 状況_Leave;
            // 
            // 業務日報明細実績1
            // 
            業務日報明細実績1.Location = new Point(9, 229);
            業務日報明細実績1.Margin = new Padding(4);
            業務日報明細実績1.Name = "業務日報明細実績1";
            業務日報明細実績1.Size = new Size(856, 221);
            業務日報明細実績1.TabIndex = 14;
            // 
            // 業務日報明細予定1
            // 
            業務日報明細予定1.Location = new Point(9, 450);
            業務日報明細予定1.Margin = new Padding(4);
            業務日報明細予定1.Name = "業務日報明細予定1";
            業務日報明細予定1.Size = new Size(856, 79);
            業務日報明細予定1.TabIndex = 15;
            // 
            // label1
            // 
            label1.AllowDrop = true;
            label1.AutoEllipsis = true;
            label1.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ActiveCaptionText;
            label1.ImageAlign = ContentAlignment.MiddleLeft;
            label1.Location = new Point(9, 543);
            label1.Margin = new Padding(0);
            label1.Name = "label1";
            label1.Size = new Size(100, 21);
            label1.TabIndex = 16;
            label1.Text = "本日の一言(&W)";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 本日の一言
            // 
            本日の一言.BackColor = SystemColors.Window;
            本日の一言.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            本日の一言.ImeMode = ImeMode.Hiragana;
            本日の一言.Location = new Point(122, 543);
            本日の一言.Margin = new Padding(3, 2, 3, 2);
            本日の一言.Multiline = true;
            本日の一言.Name = "本日の一言";
            本日の一言.Size = new Size(743, 34);
            本日の一言.TabIndex = 17;
            // 
            // ログインユーザーボタン
            // 
            ログインユーザーボタン.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            ログインユーザーボタン.Location = new Point(261, 72);
            ログインユーザーボタン.Margin = new Padding(4);
            ログインユーザーボタン.Name = "ログインユーザーボタン";
            ログインユーザーボタン.Size = new Size(35, 21);
            ログインユーザーボタン.TabIndex = 21014;
            ログインユーザーボタン.TabStop = false;
            ログインユーザーボタン.Text = "me";
            toolTip1.SetToolTip(ログインユーザーボタン, "現在のユーザーに設定");
            ログインユーザーボタン.UseVisualStyleBackColor = true;
            // 
            // F_業務日報
            // 
            BackColor = SystemColors.Control;
            ClientSize = new Size(888, 642);
            Controls.Add(ログインユーザーボタン);
            Controls.Add(本日の一言);
            Controls.Add(label1);
            Controls.Add(業務日報明細予定1);
            Controls.Add(業務日報明細実績1);
            Controls.Add(状況);
            Controls.Add(確定者名);
            Controls.Add(確定日時);
            Controls.Add(登録者名);
            Controls.Add(登録者コード);
            Controls.Add(登録者名_ラベル);
            Controls.Add(登録日);
            Controls.Add(社員コード旧);
            Controls.Add(日付旧);
            Controls.Add(社員コード);
            Controls.Add(日付選択ボタン);
            Controls.Add(label5);
            Controls.Add(statusStrip1);
            Controls.Add(確定者コード);
            Controls.Add(確定コード);
            Controls.Add(日報コード);
            Controls.Add(登録日_ラベル);
            Controls.Add(日報コード_ラベル);
            Controls.Add(確定_ラベル);
            Controls.Add(氏名_ラベル);
            Controls.Add(日付);
            Controls.Add(日付_ラベル);
            Controls.Add(仕入先2_ラベル);
            Controls.Add(コマンド終了);
            Controls.Add(コマンド登録);
            Controls.Add(コマンドF8);
            Controls.Add(コマンドF7);
            Controls.Add(コマンド詳細);
            Controls.Add(コマンド社員);
            Controls.Add(コマンド確定);
            Controls.Add(コマンド承認);
            Controls.Add(コマンド削除);
            Controls.Add(コマンド複写);
            Controls.Add(コマンド読込);
            Controls.Add(panel1);
            ImeMode = ImeMode.Off;
            KeyPreview = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "F_業務日報";
            Text = "業務日報";
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

        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn20;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn21;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn22;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn23;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn24;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn25;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn26;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn27;

        private System.Windows.Forms.Button コマンド終了;
        private System.Windows.Forms.Button コマンド登録;
        private System.Windows.Forms.DataGridViewTextBoxColumn 日誌IDDataGridViewTextBoxColumn;

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button コマンド新規;
        private System.Windows.Forms.Button コマンドF7;
        private System.Windows.Forms.Button コマンド詳細;
        private System.Windows.Forms.Button コマンド社員;
        private System.Windows.Forms.Button コマンド確定;
        private System.Windows.Forms.Button コマンド承認;
        private System.Windows.Forms.Button コマンド削除;
        private System.Windows.Forms.Button コマンド複写;
        private System.Windows.Forms.Button コマンド読込;

        private Button コマンドF8;
        private Label 確定_ラベル;
        private Label 氏名_ラベル;
        private ComboBox 日付;
        private Label 日付_ラベル;
        private Label 仕入先2_ラベル;
        private Label 日報コード_ラベル;
        private Label 登録日_ラベル;
        private TextBox 日報コード;
        private TextBox 確定コード;
        private TextBox 確定者コード;
        private StatusStrip statusStrip1;
        internal ToolStripStatusLabel toolStripStatusLabel1;
        private Label label5;
        private Button 日付選択ボタン;
        private ComboBox 社員コード;
        private TextBox 日付旧;
        private TextBox 社員コード旧;
        private TextBox 登録日;
        private Label 登録者名_ラベル;
        private TextBox 登録者コード;
        private TextBox 登録者名;
        private TextBox 確定日時;
        private TextBox 確定者名;
        private ListBox 状況;
        private MultiRowDesigner.業務日報明細実績 業務日報明細実績1;
        private MultiRowDesigner.業務日報明細予定 業務日報明細予定1;
        private Label label1;
        private TextBox 本日の一言;
        private ToolTip toolTip1;
        private Button ログインユーザーボタン;
    }
}


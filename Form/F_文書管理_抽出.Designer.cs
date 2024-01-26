namespace u_net
{
    partial class F_文書管理_抽出
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
            文書名_ラベル = new Label();
            文書名 = new TextBox();
            文書コード開始 = new TextBox();
            文書コード_ラベル = new Label();
            確定指定 = new GroupBox();
            確定指定Button3 = new RadioButton();
            確定指定Button2 = new RadioButton();
            確定指定Button1 = new RadioButton();
            承認指定 = new GroupBox();
            承認指定button3 = new RadioButton();
            承認指定button2 = new RadioButton();
            承認指定button1 = new RadioButton();
            完了承認指定 = new GroupBox();
            完了承認指定Button3 = new RadioButton();
            完了承認指定Button2 = new RadioButton();
            完了承認指定Button1 = new RadioButton();
            抽出ボタン = new Button();
            キャンセルボタン = new Button();
            確定日開始選択ボタン = new Button();
            件名_ラベル = new Label();
            確定日開始 = new TextBox();
            確定日終了 = new TextBox();
            確定日終了選択ボタン = new Button();
            label1 = new Label();
            label3 = new Label();
            期限日終了選択ボタン = new Button();
            期限日終了 = new TextBox();
            期限日開始 = new TextBox();
            期限日開始選択ボタン = new Button();
            文書コード終了 = new TextBox();
            件名 = new TextBox();
            発信者名 = new TextBox();
            発信者名_ラベル = new Label();
            確定日_ラベル = new Label();
            期限日_ラベル = new Label();
            削除指定 = new GroupBox();
            削除指定Button3 = new RadioButton();
            削除指定Button2 = new RadioButton();
            削除指定Button1 = new RadioButton();
            label2 = new Label();
            確定指定.SuspendLayout();
            承認指定.SuspendLayout();
            完了承認指定.SuspendLayout();
            削除指定.SuspendLayout();
            SuspendLayout();
            // 
            // 文書名_ラベル
            // 
            文書名_ラベル.AllowDrop = true;
            文書名_ラベル.AutoEllipsis = true;
            文書名_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            文書名_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            文書名_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            文書名_ラベル.Location = new Point(17, 39);
            文書名_ラベル.Margin = new Padding(0);
            文書名_ラベル.Name = "文書名_ラベル";
            文書名_ラベル.Size = new Size(95, 19);
            文書名_ラベル.TabIndex = 4;
            文書名_ラベル.Text = "文書名(&N)";
            文書名_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 文書名
            // 
            文書名.BackColor = Color.White;
            文書名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            文書名.ImeMode = ImeMode.Hiragana;
            文書名.Location = new Point(119, 39);
            文書名.Margin = new Padding(3, 2, 3, 2);
            文書名.Name = "文書名";
            文書名.Size = new Size(277, 20);
            文書名.TabIndex = 5;
            // 
            // 文書コード開始
            // 
            文書コード開始.BackColor = Color.White;
            文書コード開始.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            文書コード開始.ImeMode = ImeMode.Disable;
            文書コード開始.Location = new Point(119, 17);
            文書コード開始.Margin = new Padding(3, 2, 3, 2);
            文書コード開始.Name = "文書コード開始";
            文書コード開始.Size = new Size(122, 20);
            文書コード開始.TabIndex = 2;
            文書コード開始.KeyDown += 文書コード開始_KeyDown;
            // 
            // 文書コード_ラベル
            // 
            文書コード_ラベル.AllowDrop = true;
            文書コード_ラベル.AutoEllipsis = true;
            文書コード_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            文書コード_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            文書コード_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            文書コード_ラベル.Location = new Point(17, 17);
            文書コード_ラベル.Margin = new Padding(0);
            文書コード_ラベル.Name = "文書コード_ラベル";
            文書コード_ラベル.Size = new Size(95, 19);
            文書コード_ラベル.TabIndex = 1;
            文書コード_ラベル.Text = "文書コード(&C)";
            文書コード_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 確定指定
            // 
            確定指定.Controls.Add(確定指定Button3);
            確定指定.Controls.Add(確定指定Button2);
            確定指定.Controls.Add(確定指定Button1);
            確定指定.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            確定指定.Location = new Point(17, 163);
            確定指定.Name = "確定指定";
            確定指定.Size = new Size(374, 50);
            確定指定.TabIndex = 14;
            確定指定.TabStop = false;
            確定指定.Text = "確定指定(&D)";
            // 
            // 確定指定Button3
            // 
            確定指定Button3.AutoSize = true;
            確定指定Button3.Location = new Point(249, 22);
            確定指定Button3.Name = "確定指定Button3";
            確定指定Button3.Size = new Size(83, 16);
            確定指定Button3.TabIndex = 204;
            確定指定Button3.TabStop = true;
            確定指定Button3.Text = "指定しない";
            確定指定Button3.UseVisualStyleBackColor = true;
            // 
            // 確定指定Button2
            // 
            確定指定Button2.AutoSize = true;
            確定指定Button2.Location = new Point(129, 22);
            確定指定Button2.Name = "確定指定Button2";
            確定指定Button2.Size = new Size(95, 16);
            確定指定Button2.TabIndex = 2;
            確定指定Button2.TabStop = true;
            確定指定Button2.Text = "確定している";
            確定指定Button2.UseVisualStyleBackColor = true;
            // 
            // 確定指定Button1
            // 
            確定指定Button1.AutoSize = true;
            確定指定Button1.Location = new Point(6, 22);
            確定指定Button1.Name = "確定指定Button1";
            確定指定Button1.Size = new Size(107, 16);
            確定指定Button1.TabIndex = 1;
            確定指定Button1.TabStop = true;
            確定指定Button1.Text = "確定していない";
            確定指定Button1.UseVisualStyleBackColor = true;
            // 
            // 承認指定
            // 
            承認指定.Controls.Add(承認指定button3);
            承認指定.Controls.Add(承認指定button2);
            承認指定.Controls.Add(承認指定button1);
            承認指定.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            承認指定.Location = new Point(17, 227);
            承認指定.Name = "承認指定";
            承認指定.Size = new Size(374, 50);
            承認指定.TabIndex = 15;
            承認指定.TabStop = false;
            承認指定.Text = "承認指定(&A)";
            // 
            // 承認指定button3
            // 
            承認指定button3.AutoSize = true;
            承認指定button3.Location = new Point(249, 22);
            承認指定button3.Name = "承認指定button3";
            承認指定button3.Size = new Size(83, 16);
            承認指定button3.TabIndex = 204;
            承認指定button3.TabStop = true;
            承認指定button3.Text = "指定しない";
            承認指定button3.UseVisualStyleBackColor = true;
            // 
            // 承認指定button2
            // 
            承認指定button2.AutoSize = true;
            承認指定button2.Location = new Point(129, 22);
            承認指定button2.Name = "承認指定button2";
            承認指定button2.Size = new Size(95, 16);
            承認指定button2.TabIndex = 2;
            承認指定button2.TabStop = true;
            承認指定button2.Text = "承認している";
            承認指定button2.UseVisualStyleBackColor = true;
            // 
            // 承認指定button1
            // 
            承認指定button1.AutoSize = true;
            承認指定button1.Location = new Point(6, 22);
            承認指定button1.Name = "承認指定button1";
            承認指定button1.Size = new Size(107, 16);
            承認指定button1.TabIndex = 1;
            承認指定button1.TabStop = true;
            承認指定button1.Text = "承認していない";
            承認指定button1.UseVisualStyleBackColor = true;
            // 
            // 完了承認指定
            // 
            完了承認指定.Controls.Add(完了承認指定Button3);
            完了承認指定.Controls.Add(完了承認指定Button2);
            完了承認指定.Controls.Add(完了承認指定Button1);
            完了承認指定.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            完了承認指定.Location = new Point(17, 292);
            完了承認指定.Name = "完了承認指定";
            完了承認指定.Size = new Size(374, 50);
            完了承認指定.TabIndex = 16;
            完了承認指定.TabStop = false;
            完了承認指定.Text = "完了承認指定(&O)";
            // 
            // 完了承認指定Button3
            // 
            完了承認指定Button3.AutoSize = true;
            完了承認指定Button3.Location = new Point(249, 22);
            完了承認指定Button3.Name = "完了承認指定Button3";
            完了承認指定Button3.Size = new Size(83, 16);
            完了承認指定Button3.TabIndex = 204;
            完了承認指定Button3.TabStop = true;
            完了承認指定Button3.Text = "指定しない";
            完了承認指定Button3.UseVisualStyleBackColor = true;
            // 
            // 完了承認指定Button2
            // 
            完了承認指定Button2.AutoSize = true;
            完了承認指定Button2.Location = new Point(129, 22);
            完了承認指定Button2.Name = "完了承認指定Button2";
            完了承認指定Button2.Size = new Size(95, 16);
            完了承認指定Button2.TabIndex = 2;
            完了承認指定Button2.TabStop = true;
            完了承認指定Button2.Text = "承認している";
            完了承認指定Button2.UseVisualStyleBackColor = true;
            // 
            // 完了承認指定Button1
            // 
            完了承認指定Button1.AutoSize = true;
            完了承認指定Button1.Location = new Point(6, 22);
            完了承認指定Button1.Name = "完了承認指定Button1";
            完了承認指定Button1.Size = new Size(107, 16);
            完了承認指定Button1.TabIndex = 1;
            完了承認指定Button1.TabStop = true;
            完了承認指定Button1.Text = "承認していない";
            完了承認指定Button1.UseVisualStyleBackColor = true;
            // 
            // 抽出ボタン
            // 
            抽出ボタン.Location = new Point(180, 415);
            抽出ボタン.Name = "抽出ボタン";
            抽出ボタン.Size = new Size(102, 24);
            抽出ボタン.TabIndex = 17;
            抽出ボタン.Text = "抽出(&O)";
            抽出ボタン.UseVisualStyleBackColor = true;
            抽出ボタン.Click += 抽出ボタン_Click;
            // 
            // キャンセルボタン
            // 
            キャンセルボタン.Location = new Point(289, 415);
            キャンセルボタン.Name = "キャンセルボタン";
            キャンセルボタン.Size = new Size(102, 24);
            キャンセルボタン.TabIndex = 18;
            キャンセルボタン.Text = "キャンセル(&X)";
            キャンセルボタン.UseVisualStyleBackColor = true;
            キャンセルボタン.MouseClick += キャンセルボタン_MouseClick;
            // 
            // 確定日開始選択ボタン
            // 
            確定日開始選択ボタン.Location = new Point(223, 108);
            確定日開始選択ボタン.Margin = new Padding(4);
            確定日開始選択ボタン.Name = "確定日開始選択ボタン";
            確定日開始選択ボタン.Size = new Size(21, 21);
            確定日開始選択ボタン.TabIndex = 21008;
            確定日開始選択ボタン.TabStop = false;
            確定日開始選択ボタン.Text = "▼";
            確定日開始選択ボタン.UseVisualStyleBackColor = true;
            確定日開始選択ボタン.Click += 確定日開始選択_Click;
            // 
            // 件名_ラベル
            // 
            件名_ラベル.AllowDrop = true;
            件名_ラベル.AutoEllipsis = true;
            件名_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            件名_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            件名_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            件名_ラベル.Location = new Point(17, 62);
            件名_ラベル.Margin = new Padding(0);
            件名_ラベル.Name = "件名_ラベル";
            件名_ラベル.Size = new Size(95, 21);
            件名_ラベル.TabIndex = 6;
            件名_ラベル.Text = "件名(&T)";
            件名_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 確定日開始
            // 
            確定日開始.BackColor = Color.White;
            確定日開始.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            確定日開始.ImeMode = ImeMode.Disable;
            確定日開始.Location = new Point(119, 109);
            確定日開始.Margin = new Padding(3, 2, 3, 2);
            確定日開始.Name = "確定日開始";
            確定日開始.Size = new Size(102, 20);
            確定日開始.TabIndex = 11;
            確定日開始.DoubleClick += 確定日開始_DoubleClick;
            確定日開始.KeyPress += 確定日開始_KeyPress;
            確定日開始.Leave += 確定日開始_Leave;
            // 
            // 確定日終了
            // 
            確定日終了.BackColor = Color.White;
            確定日終了.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            確定日終了.ImeMode = ImeMode.Disable;
            確定日終了.Location = new Point(271, 109);
            確定日終了.Margin = new Padding(3, 2, 3, 2);
            確定日終了.Name = "確定日終了";
            確定日終了.Size = new Size(102, 20);
            確定日終了.TabIndex = 12;
            確定日終了.DoubleClick += 確定日終了_DoubleClick;
            確定日終了.KeyPress += 確定日終了_KeyPress;
            確定日終了.Leave += 確定日終了_Leave;
            // 
            // 確定日終了選択ボタン
            // 
            確定日終了選択ボタン.Location = new Point(375, 108);
            確定日終了選択ボタン.Margin = new Padding(4);
            確定日終了選択ボタン.Name = "確定日終了選択ボタン";
            確定日終了選択ボタン.Size = new Size(21, 21);
            確定日終了選択ボタン.TabIndex = 21016;
            確定日終了選択ボタン.TabStop = false;
            確定日終了選択ボタン.Text = "▼";
            確定日終了選択ボタン.UseVisualStyleBackColor = true;
            確定日終了選択ボタン.Click += 確定日終了選択_Click;
            // 
            // label1
            // 
            label1.AllowDrop = true;
            label1.AutoEllipsis = true;
            label1.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ActiveCaptionText;
            label1.ImageAlign = ContentAlignment.MiddleLeft;
            label1.Location = new Point(245, 109);
            label1.Margin = new Padding(0);
            label1.Name = "label1";
            label1.Size = new Size(21, 21);
            label1.TabIndex = 21017;
            label1.Text = "～";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.AllowDrop = true;
            label3.AutoEllipsis = true;
            label3.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.ActiveCaptionText;
            label3.ImageAlign = ContentAlignment.MiddleLeft;
            label3.Location = new Point(245, 132);
            label3.Margin = new Padding(0);
            label3.Name = "label3";
            label3.Size = new Size(21, 21);
            label3.TabIndex = 21023;
            label3.Text = "～";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // 期限日終了選択ボタン
            // 
            期限日終了選択ボタン.Location = new Point(375, 131);
            期限日終了選択ボタン.Margin = new Padding(4);
            期限日終了選択ボタン.Name = "期限日終了選択ボタン";
            期限日終了選択ボタン.Size = new Size(21, 21);
            期限日終了選択ボタン.TabIndex = 21022;
            期限日終了選択ボタン.TabStop = false;
            期限日終了選択ボタン.Text = "▼";
            期限日終了選択ボタン.UseVisualStyleBackColor = true;
            期限日終了選択ボタン.Click += 期限日終了選択ボタン_Click;
            // 
            // 期限日終了
            // 
            期限日終了.BackColor = Color.White;
            期限日終了.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            期限日終了.ImeMode = ImeMode.Disable;
            期限日終了.Location = new Point(271, 132);
            期限日終了.Margin = new Padding(3, 2, 3, 2);
            期限日終了.Name = "期限日終了";
            期限日終了.Size = new Size(102, 20);
            期限日終了.TabIndex = 15;
            期限日終了.DoubleClick += 期限日終了_DoubleClick;
            期限日終了.KeyPress += 期限日終了_KeyPress;
            期限日終了.Leave += 期限日終了_Leave;
            // 
            // 期限日開始
            // 
            期限日開始.BackColor = Color.White;
            期限日開始.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            期限日開始.ImeMode = ImeMode.Disable;
            期限日開始.Location = new Point(119, 132);
            期限日開始.Margin = new Padding(3, 2, 3, 2);
            期限日開始.Name = "期限日開始";
            期限日開始.Size = new Size(102, 20);
            期限日開始.TabIndex = 14;
            期限日開始.DoubleClick += 期限日開始_DoubleClick;
            期限日開始.KeyPress += 期限日開始_KeyPress;
            期限日開始.Leave += 期限日開始_Leave;
            // 
            // 期限日開始選択ボタン
            // 
            期限日開始選択ボタン.Location = new Point(223, 131);
            期限日開始選択ボタン.Margin = new Padding(4);
            期限日開始選択ボタン.Name = "期限日開始選択ボタン";
            期限日開始選択ボタン.Size = new Size(21, 21);
            期限日開始選択ボタン.TabIndex = 21021;
            期限日開始選択ボタン.TabStop = false;
            期限日開始選択ボタン.Text = "▼";
            期限日開始選択ボタン.UseVisualStyleBackColor = true;
            期限日開始選択ボタン.Click += 期限日開始選択ボタン_Click;
            // 
            // 文書コード終了
            // 
            文書コード終了.BackColor = Color.White;
            文書コード終了.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            文書コード終了.ImeMode = ImeMode.Disable;
            文書コード終了.Location = new Point(271, 17);
            文書コード終了.Margin = new Padding(3, 2, 3, 2);
            文書コード終了.Name = "文書コード終了";
            文書コード終了.Size = new Size(125, 20);
            文書コード終了.TabIndex = 3;
            文書コード終了.KeyDown += 文書コード終了_KeyDown;
            // 
            // 件名
            // 
            件名.BackColor = Color.White;
            件名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            件名.ImeMode = ImeMode.Hiragana;
            件名.Location = new Point(119, 62);
            件名.Margin = new Padding(3, 2, 3, 2);
            件名.Name = "件名";
            件名.Size = new Size(277, 20);
            件名.TabIndex = 7;
            // 
            // 発信者名
            // 
            発信者名.BackColor = Color.White;
            発信者名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            発信者名.ImeMode = ImeMode.Hiragana;
            発信者名.Location = new Point(119, 85);
            発信者名.Margin = new Padding(3, 2, 3, 2);
            発信者名.Name = "発信者名";
            発信者名.Size = new Size(277, 20);
            発信者名.TabIndex = 9;
            // 
            // 発信者名_ラベル
            // 
            発信者名_ラベル.AllowDrop = true;
            発信者名_ラベル.AutoEllipsis = true;
            発信者名_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            発信者名_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            発信者名_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            発信者名_ラベル.Location = new Point(17, 85);
            発信者名_ラベル.Margin = new Padding(0);
            発信者名_ラベル.Name = "発信者名_ラベル";
            発信者名_ラベル.Size = new Size(95, 21);
            発信者名_ラベル.TabIndex = 8;
            発信者名_ラベル.Text = "発信者名(&F)";
            発信者名_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 確定日_ラベル
            // 
            確定日_ラベル.AllowDrop = true;
            確定日_ラベル.AutoEllipsis = true;
            確定日_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            確定日_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            確定日_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            確定日_ラベル.Location = new Point(17, 109);
            確定日_ラベル.Margin = new Padding(0);
            確定日_ラベル.Name = "確定日_ラベル";
            確定日_ラベル.Size = new Size(95, 21);
            確定日_ラベル.TabIndex = 10;
            確定日_ラベル.Text = "確定日(&S)";
            確定日_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 期限日_ラベル
            // 
            期限日_ラベル.AllowDrop = true;
            期限日_ラベル.AutoEllipsis = true;
            期限日_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            期限日_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            期限日_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            期限日_ラベル.Location = new Point(17, 132);
            期限日_ラベル.Margin = new Padding(0);
            期限日_ラベル.Name = "期限日_ラベル";
            期限日_ラベル.Size = new Size(95, 21);
            期限日_ラベル.TabIndex = 13;
            期限日_ラベル.Text = "期限日(&L)";
            期限日_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 削除指定
            // 
            削除指定.Controls.Add(削除指定Button3);
            削除指定.Controls.Add(削除指定Button2);
            削除指定.Controls.Add(削除指定Button1);
            削除指定.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            削除指定.Location = new Point(17, 355);
            削除指定.Name = "削除指定";
            削除指定.Size = new Size(374, 50);
            削除指定.TabIndex = 205;
            削除指定.TabStop = false;
            削除指定.Text = "削除指定(&E)";
            // 
            // 削除指定Button3
            // 
            削除指定Button3.AutoSize = true;
            削除指定Button3.Location = new Point(249, 22);
            削除指定Button3.Name = "削除指定Button3";
            削除指定Button3.Size = new Size(83, 16);
            削除指定Button3.TabIndex = 204;
            削除指定Button3.TabStop = true;
            削除指定Button3.Text = "指定しない";
            削除指定Button3.UseVisualStyleBackColor = true;
            // 
            // 削除指定Button2
            // 
            削除指定Button2.AutoSize = true;
            削除指定Button2.Location = new Point(129, 22);
            削除指定Button2.Name = "削除指定Button2";
            削除指定Button2.Size = new Size(95, 16);
            削除指定Button2.TabIndex = 2;
            削除指定Button2.TabStop = true;
            削除指定Button2.Text = "削除している";
            削除指定Button2.UseVisualStyleBackColor = true;
            // 
            // 削除指定Button1
            // 
            削除指定Button1.AutoSize = true;
            削除指定Button1.Location = new Point(6, 22);
            削除指定Button1.Name = "削除指定Button1";
            削除指定Button1.Size = new Size(107, 16);
            削除指定Button1.TabIndex = 1;
            削除指定Button1.TabStop = true;
            削除指定Button1.Text = "削除していない";
            削除指定Button1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AllowDrop = true;
            label2.AutoEllipsis = true;
            label2.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ActiveCaptionText;
            label2.ImageAlign = ContentAlignment.MiddleLeft;
            label2.Location = new Point(245, 16);
            label2.Margin = new Padding(0);
            label2.Name = "label2";
            label2.Size = new Size(21, 21);
            label2.TabIndex = 21024;
            label2.Text = "～";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // F_文書管理_抽出
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(406, 451);
            Controls.Add(label2);
            Controls.Add(削除指定);
            Controls.Add(期限日_ラベル);
            Controls.Add(確定日_ラベル);
            Controls.Add(発信者名_ラベル);
            Controls.Add(発信者名);
            Controls.Add(件名);
            Controls.Add(文書コード終了);
            Controls.Add(label3);
            Controls.Add(期限日終了選択ボタン);
            Controls.Add(期限日終了);
            Controls.Add(期限日開始);
            Controls.Add(期限日開始選択ボタン);
            Controls.Add(label1);
            Controls.Add(確定日終了選択ボタン);
            Controls.Add(確定日終了);
            Controls.Add(確定日開始);
            Controls.Add(件名_ラベル);
            Controls.Add(確定日開始選択ボタン);
            Controls.Add(キャンセルボタン);
            Controls.Add(抽出ボタン);
            Controls.Add(完了承認指定);
            Controls.Add(承認指定);
            Controls.Add(確定指定);
            Controls.Add(文書名_ラベル);
            Controls.Add(文書名);
            Controls.Add(文書コード開始);
            Controls.Add(文書コード_ラベル);
            Name = "F_文書管理_抽出";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "文書管理 - 抽出";
            Load += Form_Load;
            KeyDown += F_文書管理_抽出_KeyDown;
            確定指定.ResumeLayout(false);
            確定指定.PerformLayout();
            承認指定.ResumeLayout(false);
            承認指定.PerformLayout();
            完了承認指定.ResumeLayout(false);
            完了承認指定.PerformLayout();
            削除指定.ResumeLayout(false);
            削除指定.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label9;
        private Label 文書名_ラベル;
        private TextBox 文書名;
        private TextBox 文書コード開始;
        private Label 文書コード_ラベル;
        private Label 件名_ラベル;
        private TextBox 確定日終了;
        private GroupBox 確定指定;
        private RadioButton 確定指定Button3;
        private RadioButton 確定指定Button2;
        private RadioButton 確定指定Button1;
        private GroupBox 承認指定;
        private RadioButton 承認指定button3;
        private RadioButton 承認指定button2;
        private RadioButton 承認指定button1;
        private GroupBox 完了承認指定;
        private RadioButton 完了承認指定Button3;
        private RadioButton 完了承認指定Button2;
        private RadioButton 完了承認指定Button1;
        private Button 抽出ボタン;
        private Button キャンセルボタン;
        private Button 確定日開始選択ボタン;
        private TextBox 確定日開始;
        private Button 確定日終了選択ボタン;
        private Label label1;
        private Label label3;
        private Button 期限日終了選択ボタン;
        private TextBox 期限日終了;
        private TextBox 期限日開始;
        private Button 期限日開始選択ボタン;
        private TextBox 文書コード終了;
        private TextBox 件名;
        private TextBox 発信者名;
        private Label 発信者名_ラベル;
        private Label 確定日_ラベル;
        private Label 期限日_ラベル;
        private GroupBox 削除指定;
        private RadioButton 削除指定Button3;
        private RadioButton 削除指定Button2;
        private RadioButton 削除指定Button1;
        private Label label2;
    }
}
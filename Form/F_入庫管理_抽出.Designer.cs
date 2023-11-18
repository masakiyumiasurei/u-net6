namespace u_net
{
    partial class F_入庫管理_抽出
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
            this.削除指定 = new System.Windows.Forms.GroupBox();
            this.DeletedButton3 = new System.Windows.Forms.RadioButton();
            this.DeletedButton2 = new System.Windows.Forms.RadioButton();
            this.DeletedButton1 = new System.Windows.Forms.RadioButton();
            this.抽出ボタン = new System.Windows.Forms.Button();
            this.キャンセルボタン = new System.Windows.Forms.Button();
            this.棚卸指定 = new System.Windows.Forms.GroupBox();
            this.InventoryButton3 = new System.Windows.Forms.RadioButton();
            this.InventoryButton2 = new System.Windows.Forms.RadioButton();
            this.InventoryButton1 = new System.Windows.Forms.RadioButton();
            this.確定指定 = new System.Windows.Forms.GroupBox();
            this.ConfirmButton3 = new System.Windows.Forms.RadioButton();
            this.ConfirmButton2 = new System.Windows.Forms.RadioButton();
            this.ConfirmButton1 = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.入庫者名 = new System.Windows.Forms.TextBox();
            this.仕入先名 = new System.Windows.Forms.TextBox();
            this.仕入先コード = new System.Windows.Forms.TextBox();
            this.集計年月 = new System.Windows.Forms.ComboBox();
            this.支払年月 = new System.Windows.Forms.ComboBox();
            this.発注コード = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.入庫日開始選択ボタン = new System.Windows.Forms.Button();
            this.入庫日終了選択ボタン = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.入庫日終了 = new System.Windows.Forms.TextBox();
            this.入庫日開始 = new System.Windows.Forms.TextBox();
            this.仕入先コード検索ボタン = new System.Windows.Forms.Button();
            this.削除指定.SuspendLayout();
            this.棚卸指定.SuspendLayout();
            this.確定指定.SuspendLayout();
            this.SuspendLayout();
            // 
            // 削除指定
            // 
            this.削除指定.Controls.Add(this.DeletedButton3);
            this.削除指定.Controls.Add(this.DeletedButton2);
            this.削除指定.Controls.Add(this.DeletedButton1);
            this.削除指定.Location = new System.Drawing.Point(27, 452);
            this.削除指定.Name = "削除指定";
            this.削除指定.Size = new System.Drawing.Size(525, 63);
            this.削除指定.TabIndex = 18;
            this.削除指定.TabStop = false;
            this.削除指定.Text = "削除指定(&D)";
            // 
            // DeletedButton3
            // 
            this.DeletedButton3.AutoSize = true;
            this.DeletedButton3.Location = new System.Drawing.Point(347, 22);
            this.DeletedButton3.Name = "DeletedButton3";
            this.DeletedButton3.Size = new System.Drawing.Size(77, 19);
            this.DeletedButton3.TabIndex = 204;
            this.DeletedButton3.TabStop = true;
            this.DeletedButton3.Text = "指定しない";
            this.DeletedButton3.UseVisualStyleBackColor = true;
            // 
            // DeletedButton2
            // 
            this.DeletedButton2.AutoSize = true;
            this.DeletedButton2.Location = new System.Drawing.Point(195, 22);
            this.DeletedButton2.Name = "DeletedButton2";
            this.DeletedButton2.Size = new System.Drawing.Size(85, 19);
            this.DeletedButton2.TabIndex = 2;
            this.DeletedButton2.TabStop = true;
            this.DeletedButton2.Text = "削除している";
            this.DeletedButton2.UseVisualStyleBackColor = true;
            // 
            // DeletedButton1
            // 
            this.DeletedButton1.AutoSize = true;
            this.DeletedButton1.Location = new System.Drawing.Point(30, 22);
            this.DeletedButton1.Name = "DeletedButton1";
            this.DeletedButton1.Size = new System.Drawing.Size(96, 19);
            this.DeletedButton1.TabIndex = 1;
            this.DeletedButton1.TabStop = true;
            this.DeletedButton1.Text = "削除していない";
            this.DeletedButton1.UseVisualStyleBackColor = true;
            // 
            // 抽出ボタン
            // 
            this.抽出ボタン.Location = new System.Drawing.Point(251, 545);
            this.抽出ボタン.Name = "抽出ボタン";
            this.抽出ボタン.Size = new System.Drawing.Size(114, 23);
            this.抽出ボタン.TabIndex = 24;
            this.抽出ボタン.Text = "抽出(&O)";
            this.抽出ボタン.UseVisualStyleBackColor = true;
            this.抽出ボタン.Click += new System.EventHandler(this.抽出ボタン_Click);
            // 
            // キャンセルボタン
            // 
            this.キャンセルボタン.Location = new System.Drawing.Point(374, 545);
            this.キャンセルボタン.Name = "キャンセルボタン";
            this.キャンセルボタン.Size = new System.Drawing.Size(114, 23);
            this.キャンセルボタン.TabIndex = 25;
            this.キャンセルボタン.Text = "キャンセル(&X)";
            this.キャンセルボタン.UseVisualStyleBackColor = true;
            this.キャンセルボタン.MouseClick += new System.Windows.Forms.MouseEventHandler(this.キャンセルボタン_MouseClick);
            // 
            // 棚卸指定
            // 
            this.棚卸指定.Controls.Add(this.InventoryButton3);
            this.棚卸指定.Controls.Add(this.InventoryButton2);
            this.棚卸指定.Controls.Add(this.InventoryButton1);
            this.棚卸指定.Location = new System.Drawing.Point(27, 371);
            this.棚卸指定.Name = "棚卸指定";
            this.棚卸指定.Size = new System.Drawing.Size(525, 63);
            this.棚卸指定.TabIndex = 17;
            this.棚卸指定.TabStop = false;
            this.棚卸指定.Text = "棚卸指定(&C)";
            // 
            // InventoryButton3
            // 
            this.InventoryButton3.AutoSize = true;
            this.InventoryButton3.Location = new System.Drawing.Point(347, 22);
            this.InventoryButton3.Name = "InventoryButton3";
            this.InventoryButton3.Size = new System.Drawing.Size(77, 19);
            this.InventoryButton3.TabIndex = 204;
            this.InventoryButton3.TabStop = true;
            this.InventoryButton3.Text = "指定しない";
            this.InventoryButton3.UseVisualStyleBackColor = true;
            // 
            // InventoryButton2
            // 
            this.InventoryButton2.AutoSize = true;
            this.InventoryButton2.Location = new System.Drawing.Point(195, 22);
            this.InventoryButton2.Name = "InventoryButton2";
            this.InventoryButton2.Size = new System.Drawing.Size(85, 19);
            this.InventoryButton2.TabIndex = 2;
            this.InventoryButton2.TabStop = true;
            this.InventoryButton2.Text = "棚卸している";
            this.InventoryButton2.UseVisualStyleBackColor = true;
            // 
            // InventoryButton1
            // 
            this.InventoryButton1.AutoSize = true;
            this.InventoryButton1.Location = new System.Drawing.Point(30, 22);
            this.InventoryButton1.Name = "InventoryButton1";
            this.InventoryButton1.Size = new System.Drawing.Size(96, 19);
            this.InventoryButton1.TabIndex = 1;
            this.InventoryButton1.TabStop = true;
            this.InventoryButton1.Text = "棚卸していない";
            this.InventoryButton1.UseVisualStyleBackColor = true;
            // 
            // 確定指定
            // 
            this.確定指定.Controls.Add(this.ConfirmButton3);
            this.確定指定.Controls.Add(this.ConfirmButton2);
            this.確定指定.Controls.Add(this.ConfirmButton1);
            this.確定指定.Location = new System.Drawing.Point(27, 291);
            this.確定指定.Name = "確定指定";
            this.確定指定.Size = new System.Drawing.Size(525, 63);
            this.確定指定.TabIndex = 16;
            this.確定指定.TabStop = false;
            this.確定指定.Text = "確定指定(&K)";
            // 
            // ConfirmButton3
            // 
            this.ConfirmButton3.AutoSize = true;
            this.ConfirmButton3.Location = new System.Drawing.Point(347, 22);
            this.ConfirmButton3.Name = "ConfirmButton3";
            this.ConfirmButton3.Size = new System.Drawing.Size(77, 19);
            this.ConfirmButton3.TabIndex = 204;
            this.ConfirmButton3.TabStop = true;
            this.ConfirmButton3.Text = "指定しない";
            this.ConfirmButton3.UseVisualStyleBackColor = true;
            // 
            // ConfirmButton2
            // 
            this.ConfirmButton2.AutoSize = true;
            this.ConfirmButton2.Location = new System.Drawing.Point(195, 22);
            this.ConfirmButton2.Name = "ConfirmButton2";
            this.ConfirmButton2.Size = new System.Drawing.Size(85, 19);
            this.ConfirmButton2.TabIndex = 2;
            this.ConfirmButton2.TabStop = true;
            this.ConfirmButton2.Text = "確定している";
            this.ConfirmButton2.UseVisualStyleBackColor = true;
            // 
            // ConfirmButton1
            // 
            this.ConfirmButton1.AutoSize = true;
            this.ConfirmButton1.Location = new System.Drawing.Point(30, 22);
            this.ConfirmButton1.Name = "ConfirmButton1";
            this.ConfirmButton1.Size = new System.Drawing.Size(96, 19);
            this.ConfirmButton1.TabIndex = 1;
            this.ConfirmButton1.TabStop = true;
            this.ConfirmButton1.Text = "確定していない";
            this.ConfirmButton1.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AllowDrop = true;
            this.label5.AutoEllipsis = true;
            this.label5.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label5.Location = new System.Drawing.Point(27, 23);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 17);
            this.label5.TabIndex = 1;
            this.label5.Text = "入庫日(&D)";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AllowDrop = true;
            this.label2.AutoEllipsis = true;
            this.label2.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point(27, 64);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "入庫者名(&E)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AllowDrop = true;
            this.label4.AutoEllipsis = true;
            this.label4.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Location = new System.Drawing.Point(25, 240);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(151, 17);
            this.label4.TabIndex = 14;
            this.label4.Text = "仕入先名(&N)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AllowDrop = true;
            this.label6.AutoEllipsis = true;
            this.label6.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label6.Location = new System.Drawing.Point(25, 102);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(151, 17);
            this.label6.TabIndex = 6;
            this.label6.Text = "集計年月(&A)";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.AllowDrop = true;
            this.label8.AutoEllipsis = true;
            this.label8.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label8.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label8.Location = new System.Drawing.Point(25, 140);
            this.label8.Margin = new System.Windows.Forms.Padding(0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(151, 17);
            this.label8.TabIndex = 8;
            this.label8.Text = "支払年月(&P)";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.AllowDrop = true;
            this.label11.AutoEllipsis = true;
            this.label11.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label11.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label11.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label11.Location = new System.Drawing.Point(25, 206);
            this.label11.Margin = new System.Windows.Forms.Padding(0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(151, 17);
            this.label11.TabIndex = 12;
            this.label11.Text = "仕入先コード(&S)";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // 入庫者名
            // 
            this.入庫者名.BackColor = System.Drawing.Color.White;
            this.入庫者名.Font = new System.Drawing.Font("BIZ UDゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.入庫者名.Location = new System.Drawing.Point(170, 64);
            this.入庫者名.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.入庫者名.Name = "入庫者名";
            this.入庫者名.Size = new System.Drawing.Size(413, 20);
            this.入庫者名.TabIndex = 5;
            // 
            // 仕入先名
            // 
            this.仕入先名.BackColor = System.Drawing.Color.White;
            this.仕入先名.Font = new System.Drawing.Font("BIZ UDゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.仕入先名.Location = new System.Drawing.Point(168, 240);
            this.仕入先名.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.仕入先名.Name = "仕入先名";
            this.仕入先名.Size = new System.Drawing.Size(413, 20);
            this.仕入先名.TabIndex = 15;
            this.仕入先名.TextChanged += new System.EventHandler(this.仕入先名_TextChanged);
            // 
            // 仕入先コード
            // 
            this.仕入先コード.BackColor = System.Drawing.Color.White;
            this.仕入先コード.Font = new System.Drawing.Font("BIZ UDゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.仕入先コード.Location = new System.Drawing.Point(168, 206);
            this.仕入先コード.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.仕入先コード.Name = "仕入先コード";
            this.仕入先コード.Size = new System.Drawing.Size(139, 20);
            this.仕入先コード.TabIndex = 13;
            this.仕入先コード.DoubleClick += new System.EventHandler(this.仕入先コード_DoubleClick);
            this.仕入先コード.KeyDown += new System.Windows.Forms.KeyEventHandler(this.仕入先コード_KeyDown);
            this.仕入先コード.Validated += new System.EventHandler(this.仕入先コード_Validated);
            // 
            // 集計年月
            // 
            this.集計年月.BackColor = System.Drawing.Color.White;
            this.集計年月.DisplayMember = "分類コード";
            this.集計年月.Font = new System.Drawing.Font("BIZ UDゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.集計年月.FormattingEnabled = true;
            this.集計年月.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.集計年月.Location = new System.Drawing.Point(168, 102);
            this.集計年月.Name = "集計年月";
            this.集計年月.Size = new System.Drawing.Size(139, 21);
            this.集計年月.TabIndex = 7;
            this.集計年月.ValueMember = "分類コード";
            // 
            // 支払年月
            // 
            this.支払年月.BackColor = System.Drawing.Color.White;
            this.支払年月.DisplayMember = "分類コード";
            this.支払年月.Font = new System.Drawing.Font("BIZ UDゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.支払年月.FormattingEnabled = true;
            this.支払年月.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.支払年月.Location = new System.Drawing.Point(168, 140);
            this.支払年月.Name = "支払年月";
            this.支払年月.Size = new System.Drawing.Size(139, 21);
            this.支払年月.TabIndex = 9;
            this.支払年月.ValueMember = "分類コード";
            // 
            // 発注コード
            // 
            this.発注コード.BackColor = System.Drawing.Color.White;
            this.発注コード.Font = new System.Drawing.Font("BIZ UDゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.発注コード.Location = new System.Drawing.Point(168, 173);
            this.発注コード.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.発注コード.Name = "発注コード";
            this.発注コード.Size = new System.Drawing.Size(139, 20);
            this.発注コード.TabIndex = 11;
            // 
            // label9
            // 
            this.label9.AllowDrop = true;
            this.label9.AutoEllipsis = true;
            this.label9.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label9.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label9.Location = new System.Drawing.Point(25, 173);
            this.label9.Margin = new System.Windows.Forms.Padding(0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(151, 17);
            this.label9.TabIndex = 10;
            this.label9.Text = "発注コード(&H)";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // 入庫日開始選択ボタン
            // 
            this.入庫日開始選択ボタン.Location = new System.Drawing.Point(332, 23);
            this.入庫日開始選択ボタン.Margin = new System.Windows.Forms.Padding(4);
            this.入庫日開始選択ボタン.Name = "入庫日開始選択ボタン";
            this.入庫日開始選択ボタン.Size = new System.Drawing.Size(29, 23);
            this.入庫日開始選択ボタン.TabIndex = 21007;
            this.入庫日開始選択ボタン.Text = "▼";
            this.入庫日開始選択ボタン.UseVisualStyleBackColor = true;
            this.入庫日開始選択ボタン.Click += new System.EventHandler(this.入庫日開始選択ボタン_Click);
            // 
            // 入庫日終了選択ボタン
            // 
            this.入庫日終了選択ボタン.Location = new System.Drawing.Point(554, 23);
            this.入庫日終了選択ボタン.Margin = new System.Windows.Forms.Padding(4);
            this.入庫日終了選択ボタン.Name = "入庫日終了選択ボタン";
            this.入庫日終了選択ボタン.Size = new System.Drawing.Size(29, 23);
            this.入庫日終了選択ボタン.TabIndex = 21006;
            this.入庫日終了選択ボタン.Text = "▼";
            this.入庫日終了選択ボタン.UseVisualStyleBackColor = true;
            this.入庫日終了選択ボタン.Click += new System.EventHandler(this.入庫日終了選択ボタン_Click);
            // 
            // label1
            // 
            this.label1.AllowDrop = true;
            this.label1.AutoEllipsis = true;
            this.label1.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(365, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 16);
            this.label1.TabIndex = 21005;
            this.label1.Text = "～";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // 入庫日終了
            // 
            this.入庫日終了.BackColor = System.Drawing.Color.White;
            this.入庫日終了.Font = new System.Drawing.Font("BIZ UDゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.入庫日終了.Location = new System.Drawing.Point(392, 23);
            this.入庫日終了.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.入庫日終了.Multiline = true;
            this.入庫日終了.Name = "入庫日終了";
            this.入庫日終了.Size = new System.Drawing.Size(160, 23);
            this.入庫日終了.TabIndex = 3;
            this.入庫日終了.DoubleClick += new System.EventHandler(this.入庫日終了_DoubleClick);
            this.入庫日終了.Leave += new System.EventHandler(this.入庫日終了_Leave);
            // 
            // 入庫日開始
            // 
            this.入庫日開始.BackColor = System.Drawing.Color.White;
            this.入庫日開始.Font = new System.Drawing.Font("BIZ UDゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.入庫日開始.Location = new System.Drawing.Point(170, 22);
            this.入庫日開始.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.入庫日開始.Multiline = true;
            this.入庫日開始.Name = "入庫日開始";
            this.入庫日開始.Size = new System.Drawing.Size(160, 23);
            this.入庫日開始.TabIndex = 2;
            this.入庫日開始.DoubleClick += new System.EventHandler(this.入庫日開始_DoubleClick);
            this.入庫日開始.Leave += new System.EventHandler(this.入庫日開始_Leave);
            // 
            // 仕入先コード検索ボタン
            // 
            this.仕入先コード検索ボタン.Location = new System.Drawing.Point(314, 204);
            this.仕入先コード検索ボタン.Margin = new System.Windows.Forms.Padding(4);
            this.仕入先コード検索ボタン.Name = "仕入先コード検索ボタン";
            this.仕入先コード検索ボタン.Size = new System.Drawing.Size(29, 23);
            this.仕入先コード検索ボタン.TabIndex = 21008;
            this.仕入先コード検索ボタン.Text = "▼";
            this.仕入先コード検索ボタン.UseVisualStyleBackColor = true;
            this.仕入先コード検索ボタン.Click += new System.EventHandler(this.仕入先コード検索ボタン_Click);
            // 
            // F_入庫管理_抽出
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 596);
            this.Controls.Add(this.仕入先コード検索ボタン);
            this.Controls.Add(this.入庫日開始選択ボタン);
            this.Controls.Add(this.入庫日終了選択ボタン);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.入庫日終了);
            this.Controls.Add(this.入庫日開始);
            this.Controls.Add(this.発注コード);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.支払年月);
            this.Controls.Add(this.集計年月);
            this.Controls.Add(this.仕入先コード);
            this.Controls.Add(this.仕入先名);
            this.Controls.Add(this.入庫者名);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.確定指定);
            this.Controls.Add(this.棚卸指定);
            this.Controls.Add(this.キャンセルボタン);
            this.Controls.Add(this.抽出ボタン);
            this.Controls.Add(this.削除指定);
            this.Controls.Add(this.label5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "F_入庫管理_抽出";
            this.Text = "F_入庫管理_抽出";
            this.Load += new System.EventHandler(this.Form_Load);
            this.削除指定.ResumeLayout(false);
            this.削除指定.PerformLayout();
            this.棚卸指定.ResumeLayout(false);
            this.棚卸指定.PerformLayout();
            this.確定指定.ResumeLayout(false);
            this.確定指定.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private GroupBox 削除指定;
        private RadioButton DeletedButton3;
        private RadioButton DeletedButton2;
        private RadioButton DeletedButton1;
        private Button 抽出ボタン;
        private Button キャンセルボタン;
        private GroupBox 棚卸指定;
        private RadioButton InventoryButton3;
        private RadioButton InventoryButton2;
        private RadioButton InventoryButton1;
        private GroupBox 確定指定;
        private RadioButton ConfirmButton3;
        private RadioButton ConfirmButton2;
        private RadioButton ConfirmButton1;
        private Label label5;
        private Label label2;
        private Label label4;
        private Label label6;
        private Label label8;
        private Label label11;
        private TextBox 入庫者名;
        private TextBox 仕入先名;
        private TextBox 仕入先コード;
        private ComboBox 集計年月;
        private ComboBox 支払年月;
        private TextBox 発注コード;
        private Label label9;
        private Button 入庫日開始選択ボタン;
        private Button 入庫日終了選択ボタン;
        private Label label1;
        private TextBox 入庫日終了;
        private TextBox 入庫日開始;
        private Button 仕入先コード検索ボタン;
    }
}
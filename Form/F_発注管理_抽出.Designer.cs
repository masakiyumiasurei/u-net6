namespace u_net
{
    partial class F_発注管理_抽出
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
            削除指定 = new GroupBox();
            DeletedButton3 = new RadioButton();
            DeletedButton2 = new RadioButton();
            DeletedButton1 = new RadioButton();
            抽出ボタン = new Button();
            キャンセルボタン = new Button();
            入庫状況指定 = new GroupBox();
            InventoryButton3 = new RadioButton();
            InventoryButton2 = new RadioButton();
            InventoryButton1 = new RadioButton();
            購買指定 = new GroupBox();
            購買データ抽出指定3 = new RadioButton();
            購買データ抽出指定2 = new RadioButton();
            購買データ抽出指定1 = new RadioButton();
            label5 = new Label();
            label2 = new Label();
            label4 = new Label();
            label11 = new Label();
            発注者名 = new ComboBox();
            発注コード開始 = new TextBox();
            label9 = new Label();
            発注日開始選択ボタン = new Button();
            発注日終了選択ボタン = new Button();
            label1 = new Label();
            発注日終了 = new TextBox();
            発注日開始 = new TextBox();
            label3 = new Label();
            発注コード終了 = new TextBox();
            購買コード終了 = new TextBox();
            label7 = new Label();
            購買コード開始 = new TextBox();
            仕入先名 = new ComboBox();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            削除指定.SuspendLayout();
            入庫状況指定.SuspendLayout();
            購買指定.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // 削除指定
            // 
            削除指定.Controls.Add(DeletedButton3);
            削除指定.Controls.Add(DeletedButton2);
            削除指定.Controls.Add(DeletedButton1);
            削除指定.Location = new Point(31, 373);
            削除指定.Margin = new Padding(3, 4, 3, 4);
            削除指定.Name = "削除指定";
            削除指定.Padding = new Padding(3, 4, 3, 4);
            削除指定.Size = new Size(600, 84);
            削除指定.TabIndex = 18;
            削除指定.TabStop = false;
            削除指定.Text = "削除指定(&R)";
            // 
            // DeletedButton3
            // 
            DeletedButton3.AutoSize = true;
            DeletedButton3.Location = new Point(397, 29);
            DeletedButton3.Margin = new Padding(3, 4, 3, 4);
            DeletedButton3.Name = "DeletedButton3";
            DeletedButton3.Size = new Size(92, 24);
            DeletedButton3.TabIndex = 204;
            DeletedButton3.TabStop = true;
            DeletedButton3.Text = "指定しない";
            DeletedButton3.UseVisualStyleBackColor = true;
            // 
            // DeletedButton2
            // 
            DeletedButton2.AutoSize = true;
            DeletedButton2.Location = new Point(223, 29);
            DeletedButton2.Margin = new Padding(3, 4, 3, 4);
            DeletedButton2.Name = "DeletedButton2";
            DeletedButton2.Size = new Size(131, 24);
            DeletedButton2.TabIndex = 2;
            DeletedButton2.TabStop = true;
            DeletedButton2.Text = "削除している発注";
            DeletedButton2.UseVisualStyleBackColor = true;
            // 
            // DeletedButton1
            // 
            DeletedButton1.AutoSize = true;
            DeletedButton1.Location = new Point(34, 29);
            DeletedButton1.Margin = new Padding(3, 4, 3, 4);
            DeletedButton1.Name = "DeletedButton1";
            DeletedButton1.Size = new Size(145, 24);
            DeletedButton1.TabIndex = 1;
            DeletedButton1.TabStop = true;
            DeletedButton1.Text = "削除していない発注";
            DeletedButton1.UseVisualStyleBackColor = true;
            // 
            // 抽出ボタン
            // 
            抽出ボタン.Location = new Point(352, 465);
            抽出ボタン.Margin = new Padding(3, 4, 3, 4);
            抽出ボタン.Name = "抽出ボタン";
            抽出ボタン.Size = new Size(130, 31);
            抽出ボタン.TabIndex = 24;
            抽出ボタン.Text = "抽出";
            抽出ボタン.UseVisualStyleBackColor = true;
            抽出ボタン.Click += 抽出ボタン_Click;
            // 
            // キャンセルボタン
            // 
            キャンセルボタン.Location = new Point(492, 465);
            キャンセルボタン.Margin = new Padding(3, 4, 3, 4);
            キャンセルボタン.Name = "キャンセルボタン";
            キャンセルボタン.Size = new Size(130, 31);
            キャンセルボタン.TabIndex = 25;
            キャンセルボタン.Text = "キャンセル";
            キャンセルボタン.UseVisualStyleBackColor = true;
            キャンセルボタン.MouseClick += キャンセルボタン_MouseClick;
            // 
            // 入庫状況指定
            // 
            入庫状況指定.Controls.Add(InventoryButton3);
            入庫状況指定.Controls.Add(InventoryButton2);
            入庫状況指定.Controls.Add(InventoryButton1);
            入庫状況指定.Location = new Point(31, 282);
            入庫状況指定.Margin = new Padding(3, 4, 3, 4);
            入庫状況指定.Name = "入庫状況指定";
            入庫状況指定.Padding = new Padding(3, 4, 3, 4);
            入庫状況指定.Size = new Size(600, 84);
            入庫状況指定.TabIndex = 17;
            入庫状況指定.TabStop = false;
            入庫状況指定.Text = "入庫状況指定(&E)";
            // 
            // InventoryButton3
            // 
            InventoryButton3.AutoSize = true;
            InventoryButton3.Location = new Point(397, 29);
            InventoryButton3.Margin = new Padding(3, 4, 3, 4);
            InventoryButton3.Name = "InventoryButton3";
            InventoryButton3.Size = new Size(92, 24);
            InventoryButton3.TabIndex = 204;
            InventoryButton3.TabStop = true;
            InventoryButton3.Text = "指定しない";
            InventoryButton3.UseVisualStyleBackColor = true;
            // 
            // InventoryButton2
            // 
            InventoryButton2.AutoSize = true;
            InventoryButton2.Location = new Point(223, 29);
            InventoryButton2.Margin = new Padding(3, 4, 3, 4);
            InventoryButton2.Name = "InventoryButton2";
            InventoryButton2.Size = new Size(115, 24);
            InventoryButton2.TabIndex = 2;
            InventoryButton2.TabStop = true;
            InventoryButton2.Text = "入庫済み発注";
            InventoryButton2.UseVisualStyleBackColor = true;
            // 
            // InventoryButton1
            // 
            InventoryButton1.AutoSize = true;
            InventoryButton1.Location = new Point(34, 29);
            InventoryButton1.Margin = new Padding(3, 4, 3, 4);
            InventoryButton1.Name = "InventoryButton1";
            InventoryButton1.Size = new Size(102, 24);
            InventoryButton1.TabIndex = 1;
            InventoryButton1.TabStop = true;
            InventoryButton1.Text = "未入庫発注";
            InventoryButton1.UseVisualStyleBackColor = true;
            // 
            // 購買指定
            // 
            購買指定.Controls.Add(購買データ抽出指定3);
            購買指定.Controls.Add(購買データ抽出指定2);
            購買指定.Controls.Add(購買データ抽出指定1);
            購買指定.Location = new Point(31, 190);
            購買指定.Margin = new Padding(3, 4, 3, 4);
            購買指定.Name = "購買指定";
            購買指定.Padding = new Padding(3, 4, 3, 4);
            購買指定.Size = new Size(600, 84);
            購買指定.TabIndex = 16;
            購買指定.TabStop = false;
            購買指定.Text = "購買指定(&P)";
            // 
            // 購買データ抽出指定3
            // 
            購買データ抽出指定3.AutoSize = true;
            購買データ抽出指定3.Location = new Point(397, 29);
            購買データ抽出指定3.Margin = new Padding(3, 4, 3, 4);
            購買データ抽出指定3.Name = "購買データ抽出指定3";
            購買データ抽出指定3.Size = new Size(92, 24);
            購買データ抽出指定3.TabIndex = 204;
            購買データ抽出指定3.TabStop = true;
            購買データ抽出指定3.Text = "指定しない";
            購買データ抽出指定3.UseVisualStyleBackColor = true;
            // 
            // 購買データ抽出指定2
            // 
            購買データ抽出指定2.AutoSize = true;
            購買データ抽出指定2.Location = new Point(223, 29);
            購買データ抽出指定2.Margin = new Padding(3, 4, 3, 4);
            購買データ抽出指定2.Name = "購買データ抽出指定2";
            購買データ抽出指定2.Size = new Size(129, 24);
            購買データ抽出指定2.TabIndex = 2;
            購買データ抽出指定2.TabStop = true;
            購買データ抽出指定2.Text = "購買対象の発注";
            購買データ抽出指定2.UseVisualStyleBackColor = true;
            // 
            // 購買データ抽出指定1
            // 
            購買データ抽出指定1.AutoSize = true;
            購買データ抽出指定1.Location = new Point(34, 29);
            購買データ抽出指定1.Margin = new Padding(3, 4, 3, 4);
            購買データ抽出指定1.Name = "購買データ抽出指定1";
            購買データ抽出指定1.Size = new Size(154, 24);
            購買データ抽出指定1.TabIndex = 1;
            購買データ抽出指定1.TabStop = true;
            購買データ抽出指定1.Text = "購買対象でない発注";
            購買データ抽出指定1.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.AllowDrop = true;
            label5.AutoEllipsis = true;
            label5.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label5.ForeColor = SystemColors.ActiveCaptionText;
            label5.ImageAlign = ContentAlignment.MiddleLeft;
            label5.Location = new Point(31, 48);
            label5.Margin = new Padding(0);
            label5.Name = "label5";
            label5.Size = new Size(99, 23);
            label5.TabIndex = 3;
            label5.Text = "発注日(&D)";
            label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            label2.AllowDrop = true;
            label2.AutoEllipsis = true;
            label2.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ActiveCaptionText;
            label2.ImageAlign = ContentAlignment.MiddleLeft;
            label2.Location = new Point(31, 85);
            label2.Margin = new Padding(0);
            label2.Name = "label2";
            label2.Size = new Size(99, 23);
            label2.TabIndex = 6;
            label2.Text = "発注者名(&N)";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            label4.AllowDrop = true;
            label4.AutoEllipsis = true;
            label4.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label4.ForeColor = SystemColors.ActiveCaptionText;
            label4.ImageAlign = ContentAlignment.MiddleLeft;
            label4.Location = new Point(31, 155);
            label4.Margin = new Padding(0);
            label4.Name = "label4";
            label4.Size = new Size(114, 23);
            label4.TabIndex = 14;
            label4.Text = "仕入先名(&S)";
            label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            label11.AllowDrop = true;
            label11.AutoEllipsis = true;
            label11.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label11.ForeColor = SystemColors.ActiveCaptionText;
            label11.ImageAlign = ContentAlignment.MiddleLeft;
            label11.Location = new Point(29, 122);
            label11.Margin = new Padding(0);
            label11.Name = "label11";
            label11.Size = new Size(90, 23);
            label11.TabIndex = 12;
            label11.Text = "購買コード(&K)";
            label11.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 発注者名
            // 
            発注者名.BackColor = Color.White;
            発注者名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            発注者名.FormattingEnabled = true;
            発注者名.ImeMode = ImeMode.Off;
            発注者名.Location = new Point(148, 87);
            発注者名.Margin = new Padding(3, 4, 3, 4);
            発注者名.Name = "発注者名";
            発注者名.Size = new Size(221, 21);
            発注者名.TabIndex = 7;
            発注者名.Enter += 発注者名_Enter;
            // 
            // 発注コード開始
            // 
            発注コード開始.BackColor = Color.White;
            発注コード開始.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            発注コード開始.Location = new Point(148, 12);
            発注コード開始.Name = "発注コード開始";
            発注コード開始.Size = new Size(184, 20);
            発注コード開始.TabIndex = 1;
            発注コード開始.Enter += 発注コード開始_Enter;
            発注コード開始.KeyDown += 発注コード_KeyDown;
            // 
            // label9
            // 
            label9.AllowDrop = true;
            label9.AutoEllipsis = true;
            label9.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label9.ForeColor = SystemColors.ActiveCaptionText;
            label9.ImageAlign = ContentAlignment.MiddleLeft;
            label9.Location = new Point(29, 14);
            label9.Margin = new Padding(0);
            label9.Name = "label9";
            label9.Size = new Size(101, 23);
            label9.TabIndex = 0;
            label9.Text = "発注コード(&C)";
            label9.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 発注日開始選択ボタン
            // 
            発注日開始選択ボタン.Font = new Font("Yu Gothic UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            発注日開始選択ボタン.Location = new Point(307, 47);
            発注日開始選択ボタン.Margin = new Padding(5);
            発注日開始選択ボタン.Name = "発注日開始選択ボタン";
            発注日開始選択ボタン.Size = new Size(25, 25);
            発注日開始選択ボタン.TabIndex = 21007;
            発注日開始選択ボタン.Text = "▼";
            発注日開始選択ボタン.UseVisualStyleBackColor = true;
            発注日開始選択ボタン.Click += 発注日開始選択ボタン_Click;
            // 
            // 発注日終了選択ボタン
            // 
            発注日終了選択ボタン.Font = new Font("Yu Gothic UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            発注日終了選択ボタン.Location = new Point(524, 45);
            発注日終了選択ボタン.Margin = new Padding(5);
            発注日終了選択ボタン.Name = "発注日終了選択ボタン";
            発注日終了選択ボタン.Size = new Size(25, 25);
            発注日終了選択ボタン.TabIndex = 21006;
            発注日終了選択ボタン.Text = "▼";
            発注日終了選択ボタン.UseVisualStyleBackColor = true;
            発注日終了選択ボタン.Click += 発注日終了選択ボタン_Click;
            // 
            // label1
            // 
            label1.AllowDrop = true;
            label1.AutoEllipsis = true;
            label1.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ActiveCaptionText;
            label1.ImageAlign = ContentAlignment.MiddleLeft;
            label1.Location = new Point(337, 49);
            label1.Margin = new Padding(0);
            label1.Name = "label1";
            label1.Size = new Size(21, 21);
            label1.TabIndex = 21005;
            label1.Text = "～";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 発注日終了
            // 
            発注日終了.BackColor = Color.White;
            発注日終了.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            発注日終了.Location = new Point(361, 47);
            発注日終了.Multiline = true;
            発注日終了.Name = "発注日終了";
            発注日終了.Size = new Size(159, 23);
            発注日終了.TabIndex = 5;
            発注日終了.DoubleClick += 発注日終了_DoubleClick;
            発注日終了.Enter += 発注日終了_Enter;
            発注日終了.Leave += 発注日終了_Leave;
            // 
            // 発注日開始
            // 
            発注日開始.BackColor = Color.White;
            発注日開始.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            発注日開始.Location = new Point(148, 47);
            発注日開始.Multiline = true;
            発注日開始.Name = "発注日開始";
            発注日開始.Size = new Size(156, 23);
            発注日開始.TabIndex = 4;
            発注日開始.DoubleClick += 発注日開始_DoubleClick;
            発注日開始.Enter += 発注日開始_Enter;
            発注日開始.Leave += 発注日開始_Leave;
            // 
            // label3
            // 
            label3.AllowDrop = true;
            label3.AutoEllipsis = true;
            label3.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.ActiveCaptionText;
            label3.ImageAlign = ContentAlignment.MiddleLeft;
            label3.Location = new Point(337, 14);
            label3.Margin = new Padding(0);
            label3.Name = "label3";
            label3.Size = new Size(21, 21);
            label3.TabIndex = 21009;
            label3.Text = "～";
            label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 発注コード終了
            // 
            発注コード終了.BackColor = Color.White;
            発注コード終了.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            発注コード終了.Location = new Point(361, 12);
            発注コード終了.Name = "発注コード終了";
            発注コード終了.Size = new Size(184, 20);
            発注コード終了.TabIndex = 2;
            発注コード終了.Enter += 発注コード終了_Enter;
            発注コード終了.KeyDown += 発注コード_KeyDown;
            // 
            // 購買コード終了
            // 
            購買コード終了.BackColor = Color.White;
            購買コード終了.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            購買コード終了.Location = new Point(361, 122);
            購買コード終了.Name = "購買コード終了";
            購買コード終了.Size = new Size(184, 20);
            購買コード終了.TabIndex = 21011;
            購買コード終了.Enter += 購買コード終了_Enter;
            購買コード終了.KeyDown += 購買コード終了_KeyDown;
            購買コード終了.Validating += 購買コード終了_Validating;
            // 
            // label7
            // 
            label7.AllowDrop = true;
            label7.AutoEllipsis = true;
            label7.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label7.ForeColor = SystemColors.ActiveCaptionText;
            label7.ImageAlign = ContentAlignment.MiddleLeft;
            label7.Location = new Point(337, 124);
            label7.Margin = new Padding(0);
            label7.Name = "label7";
            label7.Size = new Size(21, 21);
            label7.TabIndex = 21012;
            label7.Text = "～";
            label7.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 購買コード開始
            // 
            購買コード開始.BackColor = Color.White;
            購買コード開始.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            購買コード開始.Location = new Point(148, 122);
            購買コード開始.Name = "購買コード開始";
            購買コード開始.Size = new Size(184, 20);
            購買コード開始.TabIndex = 21010;
            購買コード開始.Enter += 購買コード開始_Enter;
            購買コード開始.KeyDown += 購買コード開始_KeyDown;
            購買コード開始.Validating += 購買コード開始_Validating;
            // 
            // 仕入先名
            // 
            仕入先名.BackColor = Color.White;
            仕入先名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            仕入先名.FormattingEnabled = true;
            仕入先名.ImeMode = ImeMode.Off;
            仕入先名.Location = new Point(148, 157);
            仕入先名.Margin = new Padding(3, 4, 3, 4);
            仕入先名.Name = "仕入先名";
            仕入先名.Size = new Size(397, 21);
            仕入先名.TabIndex = 21013;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, toolStripStatusLabel2 });
            statusStrip1.Location = new Point(0, 531);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 19, 0);
            statusStrip1.Size = new Size(674, 25);
            statusStrip1.TabIndex = 21014;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(0, 20);
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new Size(111, 20);
            toolStripStatusLabel2.Text = "各種項目の説明";
            // 
            // F_発注管理_抽出
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(674, 556);
            Controls.Add(statusStrip1);
            Controls.Add(仕入先名);
            Controls.Add(購買コード終了);
            Controls.Add(label7);
            Controls.Add(購買コード開始);
            Controls.Add(発注コード終了);
            Controls.Add(label3);
            Controls.Add(発注日開始選択ボタン);
            Controls.Add(発注日終了選択ボタン);
            Controls.Add(label1);
            Controls.Add(発注日終了);
            Controls.Add(発注日開始);
            Controls.Add(発注コード開始);
            Controls.Add(label9);
            Controls.Add(発注者名);
            Controls.Add(label11);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(購買指定);
            Controls.Add(入庫状況指定);
            Controls.Add(キャンセルボタン);
            Controls.Add(抽出ボタン);
            Controls.Add(削除指定);
            Controls.Add(label5);
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "F_発注管理_抽出";
            Text = "発注管理 - 抽出";
            FormClosing += F_発注管理_抽出_FormClosing;
            Load += Form_Load;
            削除指定.ResumeLayout(false);
            削除指定.PerformLayout();
            入庫状況指定.ResumeLayout(false);
            入庫状況指定.PerformLayout();
            購買指定.ResumeLayout(false);
            購買指定.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private GroupBox 削除指定;
        private RadioButton DeletedButton3;
        private RadioButton DeletedButton2;
        private RadioButton DeletedButton1;
        private Button 抽出ボタン;
        private Button キャンセルボタン;
        private GroupBox 入庫状況指定;
        private RadioButton InventoryButton3;
        private RadioButton InventoryButton2;
        private RadioButton InventoryButton1;
        private GroupBox 購買指定;
        private RadioButton 購買データ抽出指定3;
        private RadioButton 購買データ抽出指定2;
        private RadioButton 購買データ抽出指定1;
        private Label label5;
        private Label label2;
        private Label label4;
        private Label label6;
        private Label label8;
        private Label label11;
        private TextBox 入庫者名;

        private TextBox 仕入先コード;
        private ComboBox 発注者名;
        private ComboBox 支払年月;
        private TextBox 発注コード開始;
        private Label label9;
        private Button 発注日開始選択ボタン;
        private Button 発注日終了選択ボタン;
        private Label label1;
        private TextBox 発注日終了;
        private TextBox 発注日開始;
        private Button 仕入先コード検索ボタン;
        private Label label3;
        private TextBox 発注コード終了;
        private TextBox 購買コード終了;
        private Label label7;
        private TextBox 購買コード開始;
        private ComboBox 仕入先名;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel toolStripStatusLabel2;
    }
}
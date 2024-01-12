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
            components = new System.ComponentModel.Container();
            削除指定 = new GroupBox();
            DeletedButton3 = new RadioButton();
            DeletedButton2 = new RadioButton();
            DeletedButton1 = new RadioButton();
            抽出ボタン = new Button();
            キャンセルボタン = new Button();
            棚卸指定 = new GroupBox();
            InventoryButton3 = new RadioButton();
            InventoryButton2 = new RadioButton();
            InventoryButton1 = new RadioButton();
            確定指定 = new GroupBox();
            ConfirmButton3 = new RadioButton();
            ConfirmButton2 = new RadioButton();
            ConfirmButton1 = new RadioButton();
            label5 = new Label();
            label2 = new Label();
            label4 = new Label();
            label6 = new Label();
            label8 = new Label();
            label11 = new Label();
            入庫者名 = new TextBox();
            仕入先名 = new TextBox();
            仕入先コード = new TextBox();
            集計年月 = new ComboBox();
            支払年月 = new ComboBox();
            発注コード = new TextBox();
            label9 = new Label();
            入庫日開始選択ボタン = new Button();
            入庫日終了選択ボタン = new Button();
            label1 = new Label();
            入庫日終了 = new TextBox();
            入庫日開始 = new TextBox();
            仕入先コード検索ボタン = new Button();
            toolTip1 = new ToolTip(components);
            削除指定.SuspendLayout();
            棚卸指定.SuspendLayout();
            確定指定.SuspendLayout();
            SuspendLayout();
            // 
            // 削除指定
            // 
            削除指定.Controls.Add(DeletedButton3);
            削除指定.Controls.Add(DeletedButton2);
            削除指定.Controls.Add(DeletedButton1);
            削除指定.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            削除指定.Location = new Point(27, 452);
            削除指定.Name = "削除指定";
            削除指定.Size = new Size(525, 63);
            削除指定.TabIndex = 18;
            削除指定.TabStop = false;
            削除指定.Text = "削除指定(&D)";
            // 
            // DeletedButton3
            // 
            DeletedButton3.AutoSize = true;
            DeletedButton3.Location = new Point(347, 22);
            DeletedButton3.Name = "DeletedButton3";
            DeletedButton3.Size = new Size(83, 16);
            DeletedButton3.TabIndex = 204;
            DeletedButton3.TabStop = true;
            DeletedButton3.Text = "指定しない";
            DeletedButton3.UseVisualStyleBackColor = true;
            // 
            // DeletedButton2
            // 
            DeletedButton2.AutoSize = true;
            DeletedButton2.Location = new Point(195, 22);
            DeletedButton2.Name = "DeletedButton2";
            DeletedButton2.Size = new Size(95, 16);
            DeletedButton2.TabIndex = 2;
            DeletedButton2.TabStop = true;
            DeletedButton2.Text = "削除している";
            DeletedButton2.UseVisualStyleBackColor = true;
            // 
            // DeletedButton1
            // 
            DeletedButton1.AutoSize = true;
            DeletedButton1.Location = new Point(30, 22);
            DeletedButton1.Name = "DeletedButton1";
            DeletedButton1.Size = new Size(107, 16);
            DeletedButton1.TabIndex = 1;
            DeletedButton1.TabStop = true;
            DeletedButton1.Text = "削除していない";
            DeletedButton1.UseVisualStyleBackColor = true;
            // 
            // 抽出ボタン
            // 
            抽出ボタン.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            抽出ボタン.Location = new Point(251, 545);
            抽出ボタン.Name = "抽出ボタン";
            抽出ボタン.Size = new Size(114, 23);
            抽出ボタン.TabIndex = 24;
            抽出ボタン.Text = "抽出(&O)";
            抽出ボタン.UseVisualStyleBackColor = true;
            抽出ボタン.Click += 抽出ボタン_Click;
            // 
            // キャンセルボタン
            // 
            キャンセルボタン.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            キャンセルボタン.Location = new Point(374, 545);
            キャンセルボタン.Name = "キャンセルボタン";
            キャンセルボタン.Size = new Size(114, 23);
            キャンセルボタン.TabIndex = 25;
            キャンセルボタン.Text = "キャンセル(&X)";
            キャンセルボタン.UseVisualStyleBackColor = true;
            キャンセルボタン.MouseClick += キャンセルボタン_MouseClick;
            // 
            // 棚卸指定
            // 
            棚卸指定.Controls.Add(InventoryButton3);
            棚卸指定.Controls.Add(InventoryButton2);
            棚卸指定.Controls.Add(InventoryButton1);
            棚卸指定.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            棚卸指定.Location = new Point(27, 371);
            棚卸指定.Name = "棚卸指定";
            棚卸指定.Size = new Size(525, 63);
            棚卸指定.TabIndex = 17;
            棚卸指定.TabStop = false;
            棚卸指定.Text = "棚卸指定(&C)";
            // 
            // InventoryButton3
            // 
            InventoryButton3.AutoSize = true;
            InventoryButton3.Location = new Point(347, 22);
            InventoryButton3.Name = "InventoryButton3";
            InventoryButton3.Size = new Size(83, 16);
            InventoryButton3.TabIndex = 204;
            InventoryButton3.TabStop = true;
            InventoryButton3.Text = "指定しない";
            InventoryButton3.UseVisualStyleBackColor = true;
            // 
            // InventoryButton2
            // 
            InventoryButton2.AutoSize = true;
            InventoryButton2.Location = new Point(195, 22);
            InventoryButton2.Name = "InventoryButton2";
            InventoryButton2.Size = new Size(95, 16);
            InventoryButton2.TabIndex = 2;
            InventoryButton2.TabStop = true;
            InventoryButton2.Text = "棚卸している";
            InventoryButton2.UseVisualStyleBackColor = true;
            // 
            // InventoryButton1
            // 
            InventoryButton1.AutoSize = true;
            InventoryButton1.Location = new Point(30, 22);
            InventoryButton1.Name = "InventoryButton1";
            InventoryButton1.Size = new Size(107, 16);
            InventoryButton1.TabIndex = 1;
            InventoryButton1.TabStop = true;
            InventoryButton1.Text = "棚卸していない";
            InventoryButton1.UseVisualStyleBackColor = true;
            // 
            // 確定指定
            // 
            確定指定.Controls.Add(ConfirmButton3);
            確定指定.Controls.Add(ConfirmButton2);
            確定指定.Controls.Add(ConfirmButton1);
            確定指定.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            確定指定.Location = new Point(27, 291);
            確定指定.Name = "確定指定";
            確定指定.Size = new Size(525, 63);
            確定指定.TabIndex = 16;
            確定指定.TabStop = false;
            確定指定.Text = "確定指定(&K)";
            // 
            // ConfirmButton3
            // 
            ConfirmButton3.AutoSize = true;
            ConfirmButton3.Location = new Point(347, 22);
            ConfirmButton3.Name = "ConfirmButton3";
            ConfirmButton3.Size = new Size(83, 16);
            ConfirmButton3.TabIndex = 204;
            ConfirmButton3.TabStop = true;
            ConfirmButton3.Text = "指定しない";
            ConfirmButton3.UseVisualStyleBackColor = true;
            // 
            // ConfirmButton2
            // 
            ConfirmButton2.AutoSize = true;
            ConfirmButton2.Location = new Point(195, 22);
            ConfirmButton2.Name = "ConfirmButton2";
            ConfirmButton2.Size = new Size(95, 16);
            ConfirmButton2.TabIndex = 2;
            ConfirmButton2.TabStop = true;
            ConfirmButton2.Text = "確定している";
            ConfirmButton2.UseVisualStyleBackColor = true;
            // 
            // ConfirmButton1
            // 
            ConfirmButton1.AutoSize = true;
            ConfirmButton1.Location = new Point(30, 22);
            ConfirmButton1.Name = "ConfirmButton1";
            ConfirmButton1.Size = new Size(107, 16);
            ConfirmButton1.TabIndex = 1;
            ConfirmButton1.TabStop = true;
            ConfirmButton1.Text = "確定していない";
            ConfirmButton1.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.AllowDrop = true;
            label5.AutoEllipsis = true;
            label5.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label5.ForeColor = SystemColors.ActiveCaptionText;
            label5.ImageAlign = ContentAlignment.MiddleLeft;
            label5.Location = new Point(25, 23);
            label5.Margin = new Padding(0);
            label5.Name = "label5";
            label5.Size = new Size(87, 20);
            label5.TabIndex = 1;
            label5.Text = "入庫日(&D)";
            label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            label2.AllowDrop = true;
            label2.AutoEllipsis = true;
            label2.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ActiveCaptionText;
            label2.ImageAlign = ContentAlignment.MiddleLeft;
            label2.Location = new Point(25, 64);
            label2.Margin = new Padding(0);
            label2.Name = "label2";
            label2.Size = new Size(151, 20);
            label2.TabIndex = 4;
            label2.Text = "入庫者名(&E)";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            label4.AllowDrop = true;
            label4.AutoEllipsis = true;
            label4.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label4.ForeColor = SystemColors.ActiveCaptionText;
            label4.ImageAlign = ContentAlignment.MiddleLeft;
            label4.Location = new Point(25, 239);
            label4.Margin = new Padding(0);
            label4.Name = "label4";
            label4.Size = new Size(151, 20);
            label4.TabIndex = 14;
            label4.Text = "仕入先名(&N)";
            label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            label6.AllowDrop = true;
            label6.AutoEllipsis = true;
            label6.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label6.ForeColor = SystemColors.ActiveCaptionText;
            label6.ImageAlign = ContentAlignment.MiddleLeft;
            label6.Location = new Point(25, 102);
            label6.Margin = new Padding(0);
            label6.Name = "label6";
            label6.Size = new Size(151, 20);
            label6.TabIndex = 6;
            label6.Text = "集計年月(&A)";
            label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            label8.AllowDrop = true;
            label8.AutoEllipsis = true;
            label8.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label8.ForeColor = SystemColors.ActiveCaptionText;
            label8.ImageAlign = ContentAlignment.MiddleLeft;
            label8.Location = new Point(27, 140);
            label8.Margin = new Padding(0);
            label8.Name = "label8";
            label8.Size = new Size(151, 20);
            label8.TabIndex = 8;
            label8.Text = "支払年月(&P)";
            label8.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            label11.AllowDrop = true;
            label11.AutoEllipsis = true;
            label11.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label11.ForeColor = SystemColors.ActiveCaptionText;
            label11.ImageAlign = ContentAlignment.MiddleLeft;
            label11.Location = new Point(25, 206);
            label11.Margin = new Padding(0);
            label11.Name = "label11";
            label11.Size = new Size(151, 20);
            label11.TabIndex = 12;
            label11.Text = "仕入先コード(&S)";
            label11.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 入庫者名
            // 
            入庫者名.BackColor = Color.White;
            入庫者名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            入庫者名.ImeMode = ImeMode.Hiragana;
            入庫者名.Location = new Point(168, 65);
            入庫者名.Margin = new Padding(3, 2, 3, 2);
            入庫者名.Name = "入庫者名";
            入庫者名.Size = new Size(413, 20);
            入庫者名.TabIndex = 5;
            // 
            // 仕入先名
            // 
            仕入先名.BackColor = Color.White;
            仕入先名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            仕入先名.ImeMode = ImeMode.Hiragana;
            仕入先名.Location = new Point(168, 240);
            仕入先名.Margin = new Padding(3, 2, 3, 2);
            仕入先名.Name = "仕入先名";
            仕入先名.Size = new Size(413, 20);
            仕入先名.TabIndex = 15;
            仕入先名.TextChanged += 仕入先名_TextChanged;
            // 
            // 仕入先コード
            // 
            仕入先コード.BackColor = Color.White;
            仕入先コード.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            仕入先コード.ImeMode = ImeMode.Disable;
            仕入先コード.Location = new Point(168, 206);
            仕入先コード.Margin = new Padding(3, 2, 3, 2);
            仕入先コード.Name = "仕入先コード";
            仕入先コード.Size = new Size(139, 20);
            仕入先コード.TabIndex = 13;
            仕入先コード.DoubleClick += 仕入先コード_DoubleClick;
            仕入先コード.KeyDown += 仕入先コード_KeyDown;
            仕入先コード.Validated += 仕入先コード_Validated;
            // 
            // 集計年月
            // 
            集計年月.BackColor = Color.White;
            集計年月.DisplayMember = "分類コード";
            集計年月.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            集計年月.FormattingEnabled = true;
            集計年月.ImeMode = ImeMode.Off;
            集計年月.Location = new Point(168, 102);
            集計年月.Name = "集計年月";
            集計年月.Size = new Size(139, 21);
            集計年月.TabIndex = 7;
            集計年月.ValueMember = "分類コード";
            // 
            // 支払年月
            // 
            支払年月.BackColor = Color.White;
            支払年月.DisplayMember = "分類コード";
            支払年月.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            支払年月.FormattingEnabled = true;
            支払年月.ImeMode = ImeMode.Off;
            支払年月.Location = new Point(168, 140);
            支払年月.Name = "支払年月";
            支払年月.Size = new Size(139, 21);
            支払年月.TabIndex = 9;
            支払年月.ValueMember = "分類コード";
            // 
            // 発注コード
            // 
            発注コード.BackColor = Color.White;
            発注コード.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            発注コード.ImeMode = ImeMode.Disable;
            発注コード.Location = new Point(168, 173);
            発注コード.Margin = new Padding(3, 2, 3, 2);
            発注コード.Name = "発注コード";
            発注コード.Size = new Size(139, 20);
            発注コード.TabIndex = 11;
            // 
            // label9
            // 
            label9.AllowDrop = true;
            label9.AutoEllipsis = true;
            label9.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label9.ForeColor = SystemColors.ActiveCaptionText;
            label9.ImageAlign = ContentAlignment.MiddleLeft;
            label9.Location = new Point(25, 172);
            label9.Margin = new Padding(0);
            label9.Name = "label9";
            label9.Size = new Size(151, 20);
            label9.TabIndex = 10;
            label9.Text = "発注コード(&H)";
            label9.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 入庫日開始選択ボタン
            // 
            入庫日開始選択ボタン.Location = new Point(331, 22);
            入庫日開始選択ボタン.Margin = new Padding(4);
            入庫日開始選択ボタン.Name = "入庫日開始選択ボタン";
            入庫日開始選択ボタン.Size = new Size(29, 21);
            入庫日開始選択ボタン.TabIndex = 21007;
            入庫日開始選択ボタン.Text = "▼";
            入庫日開始選択ボタン.UseVisualStyleBackColor = true;
            入庫日開始選択ボタン.Click += 入庫日開始選択ボタン_Click;
            // 
            // 入庫日終了選択ボタン
            // 
            入庫日終了選択ボタン.Location = new Point(554, 22);
            入庫日終了選択ボタン.Margin = new Padding(4);
            入庫日終了選択ボタン.Name = "入庫日終了選択ボタン";
            入庫日終了選択ボタン.Size = new Size(29, 21);
            入庫日終了選択ボタン.TabIndex = 21006;
            入庫日終了選択ボタン.Text = "▼";
            入庫日終了選択ボタン.UseVisualStyleBackColor = true;
            入庫日終了選択ボタン.Click += 入庫日終了選択ボタン_Click;
            // 
            // label1
            // 
            label1.AllowDrop = true;
            label1.AutoEllipsis = true;
            label1.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ActiveCaptionText;
            label1.ImageAlign = ContentAlignment.MiddleLeft;
            label1.Location = new Point(362, 23);
            label1.Margin = new Padding(0);
            label1.Name = "label1";
            label1.Size = new Size(27, 20);
            label1.TabIndex = 21005;
            label1.Text = "～";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // 入庫日終了
            // 
            入庫日終了.BackColor = Color.White;
            入庫日終了.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            入庫日終了.ImeMode = ImeMode.Disable;
            入庫日終了.Location = new Point(392, 22);
            入庫日終了.Margin = new Padding(3, 2, 3, 2);
            入庫日終了.Name = "入庫日終了";
            入庫日終了.Size = new Size(160, 20);
            入庫日終了.TabIndex = 3;
            入庫日終了.DoubleClick += 入庫日終了_DoubleClick;
            入庫日終了.Leave += 入庫日終了_Leave;
            // 
            // 入庫日開始
            // 
            入庫日開始.BackColor = Color.White;
            入庫日開始.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            入庫日開始.ImeMode = ImeMode.Disable;
            入庫日開始.Location = new Point(168, 22);
            入庫日開始.Margin = new Padding(3, 2, 3, 2);
            入庫日開始.Name = "入庫日開始";
            入庫日開始.Size = new Size(160, 20);
            入庫日開始.TabIndex = 2;
            入庫日開始.DoubleClick += 入庫日開始_DoubleClick;
            入庫日開始.Leave += 入庫日開始_Leave;
            // 
            // 仕入先コード検索ボタン
            // 
            仕入先コード検索ボタン.Location = new Point(309, 206);
            仕入先コード検索ボタン.Margin = new Padding(4);
            仕入先コード検索ボタン.Name = "仕入先コード検索ボタン";
            仕入先コード検索ボタン.Size = new Size(29, 21);
            仕入先コード検索ボタン.TabIndex = 21008;
            仕入先コード検索ボタン.Text = "▼";
            toolTip1.SetToolTip(仕入先コード検索ボタン, "仕入先選択");
            仕入先コード検索ボタン.UseVisualStyleBackColor = true;
            仕入先コード検索ボタン.Click += 仕入先コード検索ボタン_Click;
            // 
            // F_入庫管理_抽出
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(607, 596);
            Controls.Add(仕入先コード検索ボタン);
            Controls.Add(入庫日開始選択ボタン);
            Controls.Add(入庫日終了選択ボタン);
            Controls.Add(label1);
            Controls.Add(入庫日終了);
            Controls.Add(入庫日開始);
            Controls.Add(発注コード);
            Controls.Add(label9);
            Controls.Add(支払年月);
            Controls.Add(集計年月);
            Controls.Add(仕入先コード);
            Controls.Add(仕入先名);
            Controls.Add(入庫者名);
            Controls.Add(label11);
            Controls.Add(label8);
            Controls.Add(label6);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(確定指定);
            Controls.Add(棚卸指定);
            Controls.Add(キャンセルボタン);
            Controls.Add(抽出ボタン);
            Controls.Add(削除指定);
            Controls.Add(label5);
            KeyPreview = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "F_入庫管理_抽出";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "入庫管理 - 抽出";
            FormClosing += F_入庫管理_抽出_FormClosing;
            Load += Form_Load;
            KeyDown += F_入庫管理_抽出_KeyDown;
            削除指定.ResumeLayout(false);
            削除指定.PerformLayout();
            棚卸指定.ResumeLayout(false);
            棚卸指定.PerformLayout();
            確定指定.ResumeLayout(false);
            確定指定.PerformLayout();
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
        private ToolTip toolTip1;
    }
}
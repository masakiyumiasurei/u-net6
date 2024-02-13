namespace u_net
{
    partial class F_見積管理_抽出
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
            label7 = new Label();
            顧客名 = new TextBox();
            見積日開始 = new TextBox();
            出荷予定日1ラベル = new Label();
            label1 = new Label();
            件名 = new TextBox();
            確定指定 = new GroupBox();
            承認指定button3 = new RadioButton();
            承認指定button2 = new RadioButton();
            承認指定button1 = new RadioButton();
            抽出ボタン = new Button();
            キャンセルボタン = new Button();
            見積日開始選択ボタン = new Button();
            見積日終了選択ボタン = new Button();
            出荷予定日2ラベル = new Label();
            見積日終了 = new TextBox();
            label3 = new Label();
            顧客コード選択ボタン = new Button();
            顧客コード = new TextBox();
            label5 = new Label();
            担当者名 = new ComboBox();
            削除指定 = new GroupBox();
            削除指定Button3 = new RadioButton();
            削除指定Button2 = new RadioButton();
            削除指定Button1 = new RadioButton();
            確定指定Button1 = new RadioButton();
            確定指定Button2 = new RadioButton();
            確定指定Button3 = new RadioButton();
            承認指定 = new GroupBox();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            確定指定.SuspendLayout();
            削除指定.SuspendLayout();
            承認指定.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // label7
            // 
            label7.AllowDrop = true;
            label7.AutoEllipsis = true;
            label7.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label7.ForeColor = SystemColors.ActiveCaptionText;
            label7.ImageAlign = ContentAlignment.MiddleLeft;
            label7.Location = new Point(14, 85);
            label7.Margin = new Padding(0);
            label7.Name = "label7";
            label7.Size = new Size(85, 20);
            label7.TabIndex = 8;
            label7.Text = "顧客名(&U)";
            label7.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 顧客名
            // 
            顧客名.BackColor = Color.White;
            顧客名.Font = new Font("BIZ UDPゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            顧客名.ImeMode = ImeMode.Hiragana;
            顧客名.Location = new Point(105, 84);
            顧客名.Margin = new Padding(3, 2, 3, 2);
            顧客名.Name = "顧客名";
            顧客名.Size = new Size(293, 21);
            顧客名.TabIndex = 9;
            顧客名.TextChanged += 顧客名_TextChanged;
            // 
            // 見積日開始
            // 
            見積日開始.BackColor = Color.White;
            見積日開始.Font = new Font("BIZ UDPゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            見積日開始.ImeMode = ImeMode.Disable;
            見積日開始.Location = new Point(105, 9);
            見積日開始.Margin = new Padding(3, 2, 3, 2);
            見積日開始.Name = "見積日開始";
            見積日開始.Size = new Size(83, 21);
            見積日開始.TabIndex = 2;
            見積日開始.TextChanged += 見積日開始_Leave;
            見積日開始.Enter += 見積日開始_Enter;
            見積日開始.KeyPress += 見積日開始_KeyPress;
            見積日開始.Leave += 見積日開始_Leave;
            見積日開始.MouseDoubleClick += 見積日開始_MouseDoubleClick;
            // 
            // 出荷予定日1ラベル
            // 
            出荷予定日1ラベル.AllowDrop = true;
            出荷予定日1ラベル.AutoEllipsis = true;
            出荷予定日1ラベル.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            出荷予定日1ラベル.ForeColor = SystemColors.ActiveCaptionText;
            出荷予定日1ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            出荷予定日1ラベル.Location = new Point(14, 9);
            出荷予定日1ラベル.Margin = new Padding(0);
            出荷予定日1ラベル.Name = "出荷予定日1ラベル";
            出荷予定日1ラベル.Size = new Size(85, 21);
            出荷予定日1ラベル.TabIndex = 1;
            出荷予定日1ラベル.Text = "見積日(&D)";
            出荷予定日1ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            label1.AllowDrop = true;
            label1.AutoEllipsis = true;
            label1.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ActiveCaptionText;
            label1.ImageAlign = ContentAlignment.MiddleLeft;
            label1.Location = new Point(14, 109);
            label1.Margin = new Padding(0);
            label1.Name = "label1";
            label1.Size = new Size(85, 20);
            label1.TabIndex = 10;
            label1.Text = "件名(&T)";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 件名
            // 
            件名.BackColor = Color.White;
            件名.Font = new Font("BIZ UDPゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            件名.ImeMode = ImeMode.Hiragana;
            件名.Location = new Point(105, 109);
            件名.Margin = new Padding(3, 2, 3, 2);
            件名.Name = "件名";
            件名.Size = new Size(293, 21);
            件名.TabIndex = 11;
            // 
            // 確定指定
            // 
            確定指定.Controls.Add(承認指定button3);
            確定指定.Controls.Add(承認指定button2);
            確定指定.Controls.Add(承認指定button1);
            確定指定.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            確定指定.Location = new Point(14, 142);
            確定指定.Name = "確定指定";
            確定指定.Size = new Size(384, 63);
            確定指定.TabIndex = 12;
            確定指定.TabStop = false;
            確定指定.Text = "確定指定(&E)";
            // 
            // 承認指定button3
            // 
            承認指定button3.AutoSize = true;
            承認指定button3.Location = new Point(274, 22);
            承認指定button3.Name = "承認指定button3";
            承認指定button3.Size = new Size(81, 16);
            承認指定button3.TabIndex = 3;
            承認指定button3.TabStop = true;
            承認指定button3.Text = "指定しない";
            承認指定button3.UseVisualStyleBackColor = true;
            // 
            // 承認指定button2
            // 
            承認指定button2.AutoSize = true;
            承認指定button2.Location = new Point(160, 22);
            承認指定button2.Name = "承認指定button2";
            承認指定button2.Size = new Size(93, 16);
            承認指定button2.TabIndex = 2;
            承認指定button2.TabStop = true;
            承認指定button2.Text = "確定している";
            承認指定button2.UseVisualStyleBackColor = true;
            // 
            // 承認指定button1
            // 
            承認指定button1.AutoSize = true;
            承認指定button1.Location = new Point(30, 22);
            承認指定button1.Name = "承認指定button1";
            承認指定button1.Size = new Size(105, 16);
            承認指定button1.TabIndex = 1;
            承認指定button1.TabStop = true;
            承認指定button1.Text = "確定していない";
            承認指定button1.UseVisualStyleBackColor = true;
            // 
            // 抽出ボタン
            // 
            抽出ボタン.Font = new Font("BIZ UDPゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            抽出ボタン.Location = new Point(189, 363);
            抽出ボタン.Name = "抽出ボタン";
            抽出ボタン.Size = new Size(100, 24);
            抽出ボタン.TabIndex = 15;
            抽出ボタン.Text = "抽出(&O)";
            抽出ボタン.UseVisualStyleBackColor = true;
            抽出ボタン.Click += 抽出ボタン_Click;
            // 
            // キャンセルボタン
            // 
            キャンセルボタン.Font = new Font("BIZ UDPゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            キャンセルボタン.Location = new Point(298, 363);
            キャンセルボタン.Name = "キャンセルボタン";
            キャンセルボタン.Size = new Size(100, 24);
            キャンセルボタン.TabIndex = 16;
            キャンセルボタン.Text = "キャンセル(&X)";
            キャンセルボタン.UseVisualStyleBackColor = true;
            キャンセルボタン.MouseClick += キャンセルボタン_MouseClick;
            // 
            // 見積日開始選択ボタン
            // 
            見積日開始選択ボタン.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            見積日開始選択ボタン.Location = new Point(189, 8);
            見積日開始選択ボタン.Margin = new Padding(4, 3, 4, 3);
            見積日開始選択ボタン.Name = "見積日開始選択ボタン";
            見積日開始選択ボタン.Size = new Size(22, 22);
            見積日開始選択ボタン.TabIndex = 1;
            見積日開始選択ボタン.TabStop = false;
            見積日開始選択ボタン.Text = "▼";
            見積日開始選択ボタン.UseVisualStyleBackColor = true;
            見積日開始選択ボタン.Click += 見積日開始選択ボタン_Click;
            // 
            // 見積日終了選択ボタン
            // 
            見積日終了選択ボタン.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            見積日終了選択ボタン.Location = new Point(317, 8);
            見積日終了選択ボタン.Margin = new Padding(4, 3, 4, 3);
            見積日終了選択ボタン.Name = "見積日終了選択ボタン";
            見積日終了選択ボタン.Size = new Size(22, 22);
            見積日終了選択ボタン.TabIndex = 3;
            見積日終了選択ボタン.TabStop = false;
            見積日終了選択ボタン.Text = "▼";
            見積日終了選択ボタン.UseVisualStyleBackColor = true;
            見積日終了選択ボタン.Click += 見積日終了選択ボタン_Click;
            // 
            // 出荷予定日2ラベル
            // 
            出荷予定日2ラベル.AllowDrop = true;
            出荷予定日2ラベル.AutoEllipsis = true;
            出荷予定日2ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            出荷予定日2ラベル.ForeColor = SystemColors.ActiveCaptionText;
            出荷予定日2ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            出荷予定日2ラベル.Location = new Point(212, 10);
            出荷予定日2ラベル.Margin = new Padding(0);
            出荷予定日2ラベル.Name = "出荷予定日2ラベル";
            出荷予定日2ラベル.Size = new Size(21, 17);
            出荷予定日2ラベル.TabIndex = 10012;
            出荷予定日2ラベル.Text = "～";
            出荷予定日2ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 見積日終了
            // 
            見積日終了.BackColor = Color.White;
            見積日終了.Font = new Font("BIZ UDPゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            見積日終了.ImeMode = ImeMode.Disable;
            見積日終了.Location = new Point(233, 9);
            見積日終了.Margin = new Padding(3, 2, 3, 2);
            見積日終了.Name = "見積日終了";
            見積日終了.Size = new Size(83, 21);
            見積日終了.TabIndex = 3;
            見積日終了.TextChanged += 見積日終了_Leave;
            見積日終了.Enter += 見積日終了_Enter;
            見積日終了.KeyPress += 見積日終了_KeyPress;
            見積日終了.Leave += 見積日終了_Leave;
            見積日終了.MouseDoubleClick += 見積日終了_MouseDoubleClick;
            // 
            // label3
            // 
            label3.AllowDrop = true;
            label3.AutoEllipsis = true;
            label3.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.ActiveCaptionText;
            label3.ImageAlign = ContentAlignment.MiddleLeft;
            label3.Location = new Point(14, 34);
            label3.Margin = new Padding(0);
            label3.Name = "label3";
            label3.Size = new Size(85, 20);
            label3.TabIndex = 4;
            label3.Text = "担当者名(&N)";
            label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 顧客コード選択ボタン
            // 
            顧客コード選択ボタン.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            顧客コード選択ボタン.Location = new Point(219, 59);
            顧客コード選択ボタン.Margin = new Padding(4, 3, 4, 3);
            顧客コード選択ボタン.Name = "顧客コード選択ボタン";
            顧客コード選択ボタン.Size = new Size(22, 22);
            顧客コード選択ボタン.TabIndex = 9;
            顧客コード選択ボタン.TabStop = false;
            顧客コード選択ボタン.Text = "▼";
            顧客コード選択ボタン.UseVisualStyleBackColor = true;
            顧客コード選択ボタン.Click += 顧客コード選択ボタン_Click;
            // 
            // 顧客コード
            // 
            顧客コード.BackColor = Color.White;
            顧客コード.Font = new Font("BIZ UDPゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            顧客コード.ImeMode = ImeMode.Disable;
            顧客コード.Location = new Point(105, 59);
            顧客コード.Margin = new Padding(3, 2, 3, 2);
            顧客コード.Name = "顧客コード";
            顧客コード.Size = new Size(113, 21);
            顧客コード.TabIndex = 7;
            顧客コード.Validated += 顧客コード_Validated;
            // 
            // label5
            // 
            label5.AllowDrop = true;
            label5.AutoEllipsis = true;
            label5.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label5.ForeColor = SystemColors.ActiveCaptionText;
            label5.ImageAlign = ContentAlignment.MiddleLeft;
            label5.Location = new Point(14, 58);
            label5.Margin = new Padding(0);
            label5.Name = "label5";
            label5.Size = new Size(88, 20);
            label5.TabIndex = 6;
            label5.Text = "顧客コード(&C)";
            label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 担当者名
            // 
            担当者名.Font = new Font("BIZ UDPゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            担当者名.FormattingEnabled = true;
            担当者名.ImeMode = ImeMode.Disable;
            担当者名.Location = new Point(105, 34);
            担当者名.Name = "担当者名";
            担当者名.Size = new Size(162, 21);
            担当者名.TabIndex = 5;
            担当者名.Enter += 担当者名_Enter;
            担当者名.Leave += 担当者名_Leave;
            // 
            // 削除指定
            // 
            削除指定.Controls.Add(削除指定Button3);
            削除指定.Controls.Add(削除指定Button2);
            削除指定.Controls.Add(削除指定Button1);
            削除指定.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            削除指定.Location = new Point(14, 289);
            削除指定.Name = "削除指定";
            削除指定.Size = new Size(384, 63);
            削除指定.TabIndex = 14;
            削除指定.TabStop = false;
            削除指定.Text = "削除指定(&R)";
            // 
            // 削除指定Button3
            // 
            削除指定Button3.AutoSize = true;
            削除指定Button3.Location = new Point(274, 22);
            削除指定Button3.Name = "削除指定Button3";
            削除指定Button3.Size = new Size(81, 16);
            削除指定Button3.TabIndex = 3;
            削除指定Button3.TabStop = true;
            削除指定Button3.Text = "指定しない";
            削除指定Button3.UseVisualStyleBackColor = true;
            // 
            // 削除指定Button2
            // 
            削除指定Button2.AutoSize = true;
            削除指定Button2.Location = new Point(158, 22);
            削除指定Button2.Name = "削除指定Button2";
            削除指定Button2.Size = new Size(93, 16);
            削除指定Button2.TabIndex = 2;
            削除指定Button2.TabStop = true;
            削除指定Button2.Text = "削除している";
            削除指定Button2.UseVisualStyleBackColor = true;
            // 
            // 削除指定Button1
            // 
            削除指定Button1.AutoSize = true;
            削除指定Button1.Location = new Point(30, 22);
            削除指定Button1.Name = "削除指定Button1";
            削除指定Button1.Size = new Size(105, 16);
            削除指定Button1.TabIndex = 1;
            削除指定Button1.TabStop = true;
            削除指定Button1.Text = "削除していない";
            削除指定Button1.UseVisualStyleBackColor = true;
            // 
            // 確定指定Button1
            // 
            確定指定Button1.AutoSize = true;
            確定指定Button1.Location = new Point(30, 22);
            確定指定Button1.Name = "確定指定Button1";
            確定指定Button1.Size = new Size(105, 16);
            確定指定Button1.TabIndex = 1;
            確定指定Button1.TabStop = true;
            確定指定Button1.Text = "承認していない";
            確定指定Button1.UseVisualStyleBackColor = true;
            // 
            // 確定指定Button2
            // 
            確定指定Button2.AutoSize = true;
            確定指定Button2.Location = new Point(158, 22);
            確定指定Button2.Name = "確定指定Button2";
            確定指定Button2.Size = new Size(93, 16);
            確定指定Button2.TabIndex = 2;
            確定指定Button2.TabStop = true;
            確定指定Button2.Text = "承認している";
            確定指定Button2.UseVisualStyleBackColor = true;
            // 
            // 確定指定Button3
            // 
            確定指定Button3.AutoSize = true;
            確定指定Button3.Location = new Point(274, 22);
            確定指定Button3.Name = "確定指定Button3";
            確定指定Button3.Size = new Size(81, 16);
            確定指定Button3.TabIndex = 3;
            確定指定Button3.TabStop = true;
            確定指定Button3.Text = "指定しない";
            確定指定Button3.UseVisualStyleBackColor = true;
            // 
            // 承認指定
            // 
            承認指定.Controls.Add(確定指定Button3);
            承認指定.Controls.Add(確定指定Button2);
            承認指定.Controls.Add(確定指定Button1);
            承認指定.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            承認指定.Location = new Point(14, 215);
            承認指定.Name = "承認指定";
            承認指定.Size = new Size(384, 63);
            承認指定.TabIndex = 13;
            承認指定.TabStop = false;
            承認指定.Text = "承認指定(&A)";
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 408);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 19, 0);
            statusStrip1.Size = new Size(420, 22);
            statusStrip1.TabIndex = 10196;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(89, 17);
            toolStripStatusLabel1.Text = "各種項目の説明";
            // 
            // F_見積管理_抽出
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(420, 430);
            Controls.Add(statusStrip1);
            Controls.Add(削除指定);
            Controls.Add(担当者名);
            Controls.Add(顧客コード選択ボタン);
            Controls.Add(顧客コード);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(見積日終了);
            Controls.Add(出荷予定日2ラベル);
            Controls.Add(見積日終了選択ボタン);
            Controls.Add(見積日開始選択ボタン);
            Controls.Add(キャンセルボタン);
            Controls.Add(抽出ボタン);
            Controls.Add(確定指定);
            Controls.Add(承認指定);
            Controls.Add(label1);
            Controls.Add(件名);
            Controls.Add(label7);
            Controls.Add(顧客名);
            Controls.Add(見積日開始);
            Controls.Add(出荷予定日1ラベル);
            Name = "F_見積管理_抽出";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "見積管理 - 抽出";
            Load += Form_Load;
            KeyDown += Form_KeyDown;
            確定指定.ResumeLayout(false);
            確定指定.PerformLayout();
            削除指定.ResumeLayout(false);
            削除指定.PerformLayout();
            承認指定.ResumeLayout(false);
            承認指定.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox 更新者名;
        private Label label9;
        private Label label7;
        private TextBox 顧客名;
        private TextBox 見積日開始;
        private Label 出荷予定日1ラベル;
        private Label label1;
        private TextBox 件名;
        private TextBox 更新日終了;
        private GroupBox 確定指定;
        private RadioButton 承認指定button3;
        private RadioButton 承認指定button2;
        private RadioButton 承認指定button1;
        private Button 抽出ボタン;
        private Button キャンセルボタン;
        private Button 見積日開始選択ボタン;
        private Button 見積日終了選択ボタン;
        private Label 出荷予定日2ラベル;
        private TextBox 見積日終了;
        private Label label3;
        private Button 顧客コード選択ボタン;
        private TextBox 顧客コード;
        private Label label5;
        private ComboBox 担当者名;
        private GroupBox 削除指定;
        private RadioButton 削除指定Button3;
        private RadioButton 削除指定Button2;
        private RadioButton 削除指定Button1;
        private RadioButton 確定指定Button1;
        private RadioButton 確定指定Button2;
        private RadioButton 確定指定Button3;
        private GroupBox 承認指定;
        private StatusStrip statusStrip1;
        internal ToolStripStatusLabel toolStripStatusLabel1;
    }
}
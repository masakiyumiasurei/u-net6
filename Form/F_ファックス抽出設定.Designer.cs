namespace u_net
{
    partial class F_ファックス抽出設定
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
            送信日開始 = new TextBox();
            送信日_ラベル = new Label();
            抽出ボタン = new Button();
            キャンセルボタン = new Button();
            groupBox1 = new GroupBox();
            送信日終了選択 = new Button();
            送信日終了 = new TextBox();
            label1 = new Label();
            送信日開始選択 = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // 送信日開始
            // 
            送信日開始.BackColor = Color.White;
            送信日開始.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            送信日開始.ImeMode = ImeMode.Disable;
            送信日開始.Location = new Point(115, 15);
            送信日開始.Margin = new Padding(3, 2, 3, 2);
            送信日開始.Name = "送信日開始";
            送信日開始.Size = new Size(102, 20);
            送信日開始.TabIndex = 2;
            送信日開始.Click += 送信日開始_Click;
            送信日開始.DoubleClick += 送信日開始_DoubleClick;
            // 
            // 送信日_ラベル
            // 
            送信日_ラベル.AllowDrop = true;
            送信日_ラベル.AutoEllipsis = true;
            送信日_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            送信日_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            送信日_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            送信日_ラベル.Location = new Point(10, 15);
            送信日_ラベル.Margin = new Padding(0);
            送信日_ラベル.Name = "送信日_ラベル";
            送信日_ラベル.Size = new Size(102, 20);
            送信日_ラベル.TabIndex = 1;
            送信日_ラベル.Text = "送信日(&D)";
            送信日_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 抽出ボタン
            // 
            抽出ボタン.Location = new Point(200, 171);
            抽出ボタン.Name = "抽出ボタン";
            抽出ボタン.Size = new Size(102, 24);
            抽出ボタン.TabIndex = 3;
            抽出ボタン.Text = "抽出(&O)";
            抽出ボタン.UseVisualStyleBackColor = true;
            抽出ボタン.Click += 抽出ボタン_Click;
            // 
            // キャンセルボタン
            // 
            キャンセルボタン.Location = new Point(308, 171);
            キャンセルボタン.Name = "キャンセルボタン";
            キャンセルボタン.Size = new Size(102, 24);
            キャンセルボタン.TabIndex = 4;
            キャンセルボタン.Text = "キャンセル(&X)";
            キャンセルボタン.UseVisualStyleBackColor = true;
            キャンセルボタン.MouseClick += キャンセルボタン_MouseClick;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(送信日終了選択);
            groupBox1.Controls.Add(送信日終了);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(送信日開始選択);
            groupBox1.Controls.Add(送信日_ラベル);
            groupBox1.Controls.Add(送信日開始);
            groupBox1.Location = new Point(9, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(401, 165);
            groupBox1.TabIndex = 19;
            groupBox1.TabStop = false;
            // 
            // 送信日終了選択
            // 
            送信日終了選択.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            送信日終了選択.Location = new Point(372, 16);
            送信日終了選択.Margin = new Padding(4);
            送信日終了選択.Name = "送信日終了選択";
            送信日終了選択.Size = new Size(20, 20);
            送信日終了選択.TabIndex = 21019;
            送信日終了選択.TabStop = false;
            送信日終了選択.Text = "▼";
            送信日終了選択.UseVisualStyleBackColor = true;
            送信日終了選択.Click += 送信日終了選択_Click;
            // 
            // 送信日終了
            // 
            送信日終了.BackColor = Color.White;
            送信日終了.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            送信日終了.ImeMode = ImeMode.Disable;
            送信日終了.Location = new Point(267, 15);
            送信日終了.Margin = new Padding(3, 2, 3, 2);
            送信日終了.Name = "送信日終了";
            送信日終了.Size = new Size(102, 20);
            送信日終了.TabIndex = 2;
            送信日終了.Click += 送信日終了_Click;
            送信日終了.DoubleClick += 送信日終了_DoubleClick;
            // 
            // label1
            // 
            label1.AllowDrop = true;
            label1.AutoEllipsis = true;
            label1.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ActiveCaptionText;
            label1.ImageAlign = ContentAlignment.MiddleLeft;
            label1.Location = new Point(241, 15);
            label1.Margin = new Padding(0);
            label1.Name = "label1";
            label1.Size = new Size(21, 21);
            label1.TabIndex = 21018;
            label1.Text = "～";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // 送信日開始選択
            // 
            送信日開始選択.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            送信日開始選択.Location = new Point(220, 15);
            送信日開始選択.Margin = new Padding(4);
            送信日開始選択.Name = "送信日開始選択";
            送信日開始選択.Size = new Size(20, 20);
            送信日開始選択.TabIndex = 10206;
            送信日開始選択.TabStop = false;
            送信日開始選択.Text = "▼";
            送信日開始選択.UseVisualStyleBackColor = true;
            送信日開始選択.Click += 送信日開始選択_Click;
            // 
            // F_ファックス抽出設定
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(420, 202);
            Controls.Add(groupBox1);
            Controls.Add(キャンセルボタン);
            Controls.Add(抽出ボタン);
            Name = "F_ファックス抽出設定";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ファックス抽出設定";
            Load += Form_Load;
            KeyDown += F_ファックス抽出設定_KeyDown;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Label label9;
        private Label 型番_ラベル;
        private TextBox 型番;
        private TextBox 品名;
        private Label 送信日_ラベル;
        private Button 抽出ボタン;
        private Button キャンセルボタン;
        private GroupBox groupBox1;
        internal Button 送信日開始選択;
        internal Button 送信日終了選択;
        private TextBox 送信日開始;
        private TextBox 送信日終了;
        private Label label1;
    }
}
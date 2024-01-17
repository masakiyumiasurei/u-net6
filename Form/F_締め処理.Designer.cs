namespace u_net
{
    partial class F_締め処理
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
            開始ボタン = new Button();
            キャンセルボタン = new Button();
            groupBox1 = new GroupBox();
            請求書様式_ラベル = new Label();
            印刷 = new CheckBox();
            label5 = new Label();
            締め処理 = new CheckBox();
            請求書様式 = new ComboBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // 開始ボタン
            // 
            開始ボタン.Location = new Point(125, 210);
            開始ボタン.Name = "開始ボタン";
            開始ボタン.Size = new Size(102, 24);
            開始ボタン.TabIndex = 5;
            開始ボタン.Text = "開始";
            開始ボタン.UseVisualStyleBackColor = true;
            開始ボタン.Click += 開始ボタン_Click;
            // 
            // キャンセルボタン
            // 
            キャンセルボタン.Location = new Point(234, 210);
            キャンセルボタン.Name = "キャンセルボタン";
            キャンセルボタン.Size = new Size(102, 24);
            キャンセルボタン.TabIndex = 6;
            キャンセルボタン.Text = "キャンセル";
            キャンセルボタン.UseVisualStyleBackColor = true;
            キャンセルボタン.MouseClick += キャンセルボタン_MouseClick;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(請求書様式_ラベル);
            groupBox1.Controls.Add(印刷);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(締め処理);
            groupBox1.Controls.Add(請求書様式);
            groupBox1.Location = new Point(6, -3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(330, 203);
            groupBox1.TabIndex = 21019;
            groupBox1.TabStop = false;
            // 
            // 請求書様式_ラベル
            // 
            請求書様式_ラベル.AllowDrop = true;
            請求書様式_ラベル.AutoEllipsis = true;
            請求書様式_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            請求書様式_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            請求書様式_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            請求書様式_ラベル.Location = new Point(11, 155);
            請求書様式_ラベル.Margin = new Padding(0);
            請求書様式_ラベル.Name = "請求書様式_ラベル";
            請求書様式_ラベル.Size = new Size(92, 20);
            請求書様式_ラベル.TabIndex = 3;
            請求書様式_ラベル.Text = "請求書様式(&F)";
            請求書様式_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 印刷
            // 
            印刷.AutoSize = true;
            印刷.Cursor = Cursors.SizeAll;
            印刷.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            印刷.Location = new Point(18, 94);
            印刷.Name = "印刷";
            印刷.Size = new Size(78, 17);
            印刷.TabIndex = 2;
            印刷.Text = "印刷する";
            印刷.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.AllowDrop = true;
            label5.AutoEllipsis = true;
            label5.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label5.ForeColor = SystemColors.ActiveCaptionText;
            label5.ImageAlign = ContentAlignment.MiddleLeft;
            label5.Location = new Point(11, 19);
            label5.Margin = new Padding(0);
            label5.Name = "label5";
            label5.Size = new Size(308, 20);
            label5.TabIndex = 10197;
            label5.Text = "締め処理を実行し、請求書を印刷します。";
            label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 締め処理
            // 
            締め処理.AutoSize = true;
            締め処理.Cursor = Cursors.SizeAll;
            締め処理.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            締め処理.Location = new Point(18, 65);
            締め処理.Name = "締め処理";
            締め処理.Size = new Size(104, 17);
            締め処理.TabIndex = 1;
            締め処理.Text = "締め処理する";
            締め処理.UseVisualStyleBackColor = true;
            // 
            // 請求書様式
            // 
            請求書様式.BackColor = Color.White;
            請求書様式.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            請求書様式.FormattingEnabled = true;
            請求書様式.ImeMode = ImeMode.NoControl;
            請求書様式.Location = new Point(106, 154);
            請求書様式.Name = "請求書様式";
            請求書様式.Size = new Size(183, 21);
            請求書様式.TabIndex = 4;
            // 
            // F_締め処理
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(347, 255);
            Controls.Add(groupBox1);
            Controls.Add(キャンセルボタン);
            Controls.Add(開始ボタン);
            Name = "F_締め処理";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "締め処理";
            Load += Form_Load;
            KeyDown += F_締め処理_KeyDown;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ComboBox 請求書様式;
        private Label label9;
        private Button 開始ボタン;
        private Button キャンセルボタン;
        private Label 更新者名_ラベル;
        private GroupBox groupBox1;
        private CheckBox 締め処理;
        private CheckBox 印刷;
        private Label label5;
        private Label 請求書様式_ラベル;
    }
}
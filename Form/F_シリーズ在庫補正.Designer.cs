namespace u_net
{
    partial class F_シリーズ在庫補正
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_シリーズ在庫補正));
            キャンセルボタン = new Button();
            補正実行ボタン = new Button();
            label1 = new Label();
            今日の日付 = new TextBox();
            label2 = new Label();
            シリーズコード = new TextBox();
            label3 = new Label();
            確認日 = new TextBox();
            シリーズ名 = new TextBox();
            label4 = new Label();
            label5 = new Label();
            在庫数量 = new TextBox();
            label6 = new Label();
            label7 = new Label();
            補正数量 = new TextBox();
            補正数量増加ボタン = new Button();
            補正数量減少ボタン = new Button();
            SuspendLayout();
            // 
            // キャンセルボタン
            // 
            キャンセルボタン.Location = new Point(360, 245);
            キャンセルボタン.Name = "キャンセルボタン";
            キャンセルボタン.Size = new Size(102, 24);
            キャンセルボタン.TabIndex = 3;
            キャンセルボタン.Text = "キャンセル(&X)";
            キャンセルボタン.UseVisualStyleBackColor = true;
            キャンセルボタン.MouseClick += キャンセルボタン_MouseClick;
            // 
            // 補正実行ボタン
            // 
            補正実行ボタン.Location = new Point(251, 245);
            補正実行ボタン.Name = "補正実行ボタン";
            補正実行ボタン.Size = new Size(102, 24);
            補正実行ボタン.TabIndex = 2;
            補正実行ボタン.Text = "補正実行(&O)";
            補正実行ボタン.UseVisualStyleBackColor = true;
            補正実行ボタン.Click += 補正実行ボタン_Click;
            // 
            // label1
            // 
            label1.AllowDrop = true;
            label1.AutoEllipsis = true;
            label1.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ActiveCaptionText;
            label1.ImageAlign = ContentAlignment.MiddleLeft;
            label1.Location = new Point(9, 15);
            label1.Margin = new Padding(0);
            label1.Name = "label1";
            label1.Size = new Size(102, 20);
            label1.TabIndex = 213;
            label1.Text = "今日の日付";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 今日の日付
            // 
            今日の日付.BackColor = Color.White;
            今日の日付.Enabled = false;
            今日の日付.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            今日の日付.ImeMode = ImeMode.NoControl;
            今日の日付.Location = new Point(119, 14);
            今日の日付.Margin = new Padding(3, 2, 3, 2);
            今日の日付.Name = "今日の日付";
            今日の日付.Size = new Size(160, 20);
            今日の日付.TabIndex = 1;
            今日の日付.TabStop = false;
            // 
            // label2
            // 
            label2.AllowDrop = true;
            label2.AutoEllipsis = true;
            label2.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ActiveCaptionText;
            label2.ImageAlign = ContentAlignment.MiddleLeft;
            label2.Location = new Point(9, 40);
            label2.Margin = new Padding(0);
            label2.Name = "label2";
            label2.Size = new Size(102, 20);
            label2.TabIndex = 215;
            label2.Text = "シリーズコード";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // シリーズコード
            // 
            シリーズコード.BackColor = Color.White;
            シリーズコード.Enabled = false;
            シリーズコード.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            シリーズコード.Location = new Point(119, 40);
            シリーズコード.Margin = new Padding(3, 2, 3, 2);
            シリーズコード.Name = "シリーズコード";
            シリーズコード.Size = new Size(160, 20);
            シリーズコード.TabIndex = 2;
            シリーズコード.TabStop = false;
            // 
            // label3
            // 
            label3.AllowDrop = true;
            label3.AutoEllipsis = true;
            label3.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.ActiveCaptionText;
            label3.ImageAlign = ContentAlignment.MiddleLeft;
            label3.Location = new Point(9, 92);
            label3.Margin = new Padding(0);
            label3.Name = "label3";
            label3.Size = new Size(102, 20);
            label3.TabIndex = 217;
            label3.Text = "確認日";
            label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 確認日
            // 
            確認日.BackColor = Color.White;
            確認日.Enabled = false;
            確認日.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            確認日.Location = new Point(119, 92);
            確認日.Margin = new Padding(3, 2, 3, 2);
            確認日.Name = "確認日";
            確認日.Size = new Size(160, 20);
            確認日.TabIndex = 4;
            確認日.TabStop = false;
            // 
            // シリーズ名
            // 
            シリーズ名.BackColor = Color.White;
            シリーズ名.Enabled = false;
            シリーズ名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            シリーズ名.Location = new Point(119, 66);
            シリーズ名.Margin = new Padding(3, 2, 3, 2);
            シリーズ名.Name = "シリーズ名";
            シリーズ名.Size = new Size(343, 20);
            シリーズ名.TabIndex = 3;
            シリーズ名.TabStop = false;
            // 
            // label4
            // 
            label4.AllowDrop = true;
            label4.AutoEllipsis = true;
            label4.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label4.ForeColor = SystemColors.ActiveCaptionText;
            label4.ImageAlign = ContentAlignment.MiddleLeft;
            label4.Location = new Point(9, 65);
            label4.Margin = new Padding(0);
            label4.Name = "label4";
            label4.Size = new Size(102, 20);
            label4.TabIndex = 217;
            label4.Text = "シリーズ名";
            label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            label5.AllowDrop = true;
            label5.AutoEllipsis = true;
            label5.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label5.ForeColor = SystemColors.ActiveCaptionText;
            label5.ImageAlign = ContentAlignment.MiddleLeft;
            label5.Location = new Point(9, 118);
            label5.Margin = new Padding(0);
            label5.Name = "label5";
            label5.Size = new Size(102, 20);
            label5.TabIndex = 219;
            label5.Text = "在庫数量";
            label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 在庫数量
            // 
            在庫数量.BackColor = Color.White;
            在庫数量.Enabled = false;
            在庫数量.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            在庫数量.Location = new Point(119, 118);
            在庫数量.Margin = new Padding(3, 2, 3, 2);
            在庫数量.Name = "在庫数量";
            在庫数量.Size = new Size(160, 20);
            在庫数量.TabIndex = 5;
            在庫数量.TabStop = false;
            // 
            // label6
            // 
            label6.AllowDrop = true;
            label6.AutoEllipsis = true;
            label6.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label6.ForeColor = SystemColors.ActiveCaptionText;
            label6.ImageAlign = ContentAlignment.MiddleLeft;
            label6.Location = new Point(9, 159);
            label6.Margin = new Padding(0);
            label6.Name = "label6";
            label6.Size = new Size(494, 20);
            label6.TabIndex = 220;
            label6.Text = "在庫数量に加算する補正数量を入力してください。";
            label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            label7.AllowDrop = true;
            label7.AutoEllipsis = true;
            label7.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label7.ForeColor = SystemColors.ActiveCaptionText;
            label7.ImageAlign = ContentAlignment.MiddleLeft;
            label7.Location = new Point(9, 195);
            label7.Margin = new Padding(0);
            label7.Name = "label7";
            label7.Size = new Size(102, 20);
            label7.TabIndex = 222;
            label7.Text = "補正数量";
            label7.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 補正数量
            // 
            補正数量.BackColor = Color.White;
            補正数量.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            補正数量.ImeMode = ImeMode.Disable;
            補正数量.Location = new Point(119, 195);
            補正数量.Margin = new Padding(3, 2, 3, 2);
            補正数量.Name = "補正数量";
            補正数量.Size = new Size(160, 20);
            補正数量.TabIndex = 1;
            // 
            // 補正数量増加ボタン
            // 
            補正数量増加ボタン.Font = new Font("Yu Gothic UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            補正数量増加ボタン.Image = (Image)resources.GetObject("補正数量増加ボタン.Image");
            補正数量増加ボタン.Location = new Point(283, 194);
            補正数量増加ボタン.Name = "補正数量増加ボタン";
            補正数量増加ボタン.Size = new Size(22, 22);
            補正数量増加ボタン.TabIndex = 223;
            補正数量増加ボタン.TabStop = false;
            補正数量増加ボタン.UseVisualStyleBackColor = true;
            補正数量増加ボタン.Click += 補正数量増加ボタン_Click;
            // 
            // 補正数量減少ボタン
            // 
            補正数量減少ボタン.Font = new Font("Yu Gothic UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            補正数量減少ボタン.Image = (Image)resources.GetObject("補正数量減少ボタン.Image");
            補正数量減少ボタン.Location = new Point(307, 194);
            補正数量減少ボタン.Name = "補正数量減少ボタン";
            補正数量減少ボタン.Size = new Size(22, 22);
            補正数量減少ボタン.TabIndex = 224;
            補正数量減少ボタン.TabStop = false;
            補正数量減少ボタン.UseVisualStyleBackColor = true;
            補正数量減少ボタン.Click += 補正数量減少ボタン_Click;
            // 
            // F_シリーズ在庫補正
            // 
            ClientSize = new Size(470, 283);
            Controls.Add(補正数量減少ボタン);
            Controls.Add(補正数量増加ボタン);
            Controls.Add(label7);
            Controls.Add(補正数量);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(在庫数量);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(シリーズ名);
            Controls.Add(確認日);
            Controls.Add(label2);
            Controls.Add(シリーズコード);
            Controls.Add(label1);
            Controls.Add(今日の日付);
            Controls.Add(キャンセルボタン);
            Controls.Add(補正実行ボタン);
            KeyPreview = true;
            Name = "F_シリーズ在庫補正";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "シリーズ在庫補正";
            FormClosing += F_シリーズ在庫補正_FormClosing;
            Load += Form_Load;
            KeyDown += F_シリーズ在庫補正_KeyDown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button キャンセルボタン;
        private Button 補正実行ボタン;
        private TextBox シリーズコード;
        private TextBox シリーズ名;
        private TextBox 確認日;
        private TextBox 在庫数量;
        private Label label1;
        private TextBox 今日の日付;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private TextBox 補正数量;
        private Button 補正数量増加ボタン;
        private Button 補正数量減少ボタン;
    }
}
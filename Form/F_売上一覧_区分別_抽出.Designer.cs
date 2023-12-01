namespace u_net
{
    partial class F_売上一覧_区分別_抽出
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
            this.抽出ボタン = new System.Windows.Forms.Button();
            this.キャンセルボタン = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.売上地区コード = new System.Windows.Forms.ComboBox();
            this.担当者コード = new System.Windows.Forms.ComboBox();
            this.売上地区名 = new System.Windows.Forms.TextBox();
            this.担当者名 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // 抽出ボタン
            // 
            this.抽出ボタン.Location = new System.Drawing.Point(276, 270);
            this.抽出ボタン.Name = "抽出ボタン";
            this.抽出ボタン.Size = new System.Drawing.Size(114, 23);
            this.抽出ボタン.TabIndex = 24;
            this.抽出ボタン.Text = "抽出(&O)";
            this.抽出ボタン.UseVisualStyleBackColor = true;
            this.抽出ボタン.Click += new System.EventHandler(this.抽出ボタン_Click);
            // 
            // キャンセルボタン
            // 
            this.キャンセルボタン.Location = new System.Drawing.Point(399, 270);
            this.キャンセルボタン.Name = "キャンセルボタン";
            this.キャンセルボタン.Size = new System.Drawing.Size(114, 23);
            this.キャンセルボタン.TabIndex = 25;
            this.キャンセルボタン.Text = "キャンセル(&X)";
            this.キャンセルボタン.UseVisualStyleBackColor = true;
            this.キャンセルボタン.MouseClick += new System.Windows.Forms.MouseEventHandler(this.キャンセルボタン_MouseClick);
            // 
            // label6
            // 
            this.label6.AllowDrop = true;
            this.label6.AutoEllipsis = true;
            this.label6.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label6.Location = new System.Drawing.Point(23, 27);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(151, 17);
            this.label6.TabIndex = 6;
            this.label6.Text = "売上地区コード(&A)";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.AllowDrop = true;
            this.label8.AutoEllipsis = true;
            this.label8.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label8.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label8.Location = new System.Drawing.Point(23, 65);
            this.label8.Margin = new System.Windows.Forms.Padding(0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(151, 17);
            this.label8.TabIndex = 8;
            this.label8.Text = "担当者コード(&S)";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // 売上地区コード
            // 
            this.売上地区コード.BackColor = System.Drawing.Color.White;
            this.売上地区コード.DisplayMember = "分類コード";
            this.売上地区コード.Font = new System.Drawing.Font("BIZ UDゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.売上地区コード.FormattingEnabled = true;
            this.売上地区コード.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.売上地区コード.Location = new System.Drawing.Point(166, 27);
            this.売上地区コード.Name = "売上地区コード";
            this.売上地区コード.Size = new System.Drawing.Size(139, 21);
            this.売上地区コード.TabIndex = 7;
            this.売上地区コード.ValueMember = "分類コード";
            this.売上地区コード.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.売上地区コード_DrawItem);
            this.売上地区コード.SelectedIndexChanged += new System.EventHandler(this.売上地区コード_SelectedIndexChanged);
            this.売上地区コード.TextChanged += new System.EventHandler(this.売上地区コード_TextChanged);
            // 
            // 担当者コード
            // 
            this.担当者コード.BackColor = System.Drawing.Color.White;
            this.担当者コード.DisplayMember = "分類コード";
            this.担当者コード.Font = new System.Drawing.Font("BIZ UDゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.担当者コード.FormattingEnabled = true;
            this.担当者コード.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.担当者コード.Location = new System.Drawing.Point(166, 65);
            this.担当者コード.Name = "担当者コード";
            this.担当者コード.Size = new System.Drawing.Size(139, 21);
            this.担当者コード.TabIndex = 9;
            this.担当者コード.ValueMember = "分類コード";
            this.担当者コード.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.担当者コード_DrawItem);
            this.担当者コード.SelectedIndexChanged += new System.EventHandler(this.担当者コード_SelectedIndexChanged);
            this.担当者コード.TextChanged += new System.EventHandler(this.担当者コード_TextChanged);
            // 
            // 売上地区名
            // 
            this.売上地区名.BackColor = System.Drawing.Color.White;
            this.売上地区名.Enabled = false;
            this.売上地区名.Font = new System.Drawing.Font("BIZ UDゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.売上地区名.Location = new System.Drawing.Point(311, 27);
            this.売上地区名.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.売上地区名.Name = "売上地区名";
            this.売上地区名.Size = new System.Drawing.Size(177, 20);
            this.売上地区名.TabIndex = 11;
            // 
            // 担当者名
            // 
            this.担当者名.BackColor = System.Drawing.Color.White;
            this.担当者名.Enabled = false;
            this.担当者名.Font = new System.Drawing.Font("BIZ UDゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.担当者名.Location = new System.Drawing.Point(311, 64);
            this.担当者名.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.担当者名.Name = "担当者名";
            this.担当者名.Size = new System.Drawing.Size(177, 20);
            this.担当者名.TabIndex = 26;
            // 
            // F_売上一覧_区分別_抽出
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 336);
            this.Controls.Add(this.担当者名);
            this.Controls.Add(this.売上地区名);
            this.Controls.Add(this.担当者コード);
            this.Controls.Add(this.売上地区コード);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.キャンセルボタン);
            this.Controls.Add(this.抽出ボタン);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "F_売上一覧_区分別_抽出";
            this.Text = "F_売上一覧_区分別_抽出";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.F_売上一覧_区分別_抽出_FormClosing);
            this.Load += new System.EventHandler(this.Form_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.F_売上一覧_区分別_抽出_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button 抽出ボタン;
        private Button キャンセルボタン;
        private Label label6;
        private Label label8;
        private ComboBox 売上地区コード;
        private ComboBox 担当者コード;
        private TextBox 売上地区名;
        private TextBox 担当者名;
    }
}
namespace u_net
{
    partial class F_社員管理_抽出
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
            氏名 = new TextBox();
            社員区分 = new GroupBox();
            社員区分Button3 = new RadioButton();
            社員区分Button2 = new RadioButton();
            社員区分Button1 = new RadioButton();
            退社指定 = new GroupBox();
            退社指定Button3 = new RadioButton();
            退社指定Button2 = new RadioButton();
            退社指定Button1 = new RadioButton();
            抽出ボタン = new Button();
            キャンセルボタン = new Button();
            社員区分.SuspendLayout();
            退社指定.SuspendLayout();
            SuspendLayout();
            // 
            // label7
            // 
            label7.AllowDrop = true;
            label7.AutoEllipsis = true;
            label7.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label7.ForeColor = SystemColors.ActiveCaptionText;
            label7.ImageAlign = ContentAlignment.MiddleLeft;
            label7.Location = new Point(27, 120);
            label7.Margin = new Padding(0);
            label7.Name = "label7";
            label7.Size = new Size(85, 17);
            label7.TabIndex = 3;
            label7.Text = "氏名(&N)";
            label7.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 氏名
            // 
            氏名.BackColor = Color.White;
            氏名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            氏名.ImeMode = ImeMode.Hiragana;
            氏名.Location = new Point(119, 113);
            氏名.Margin = new Padding(3, 2, 3, 2);
            氏名.Multiline = true;
            氏名.Name = "氏名";
            氏名.Size = new Size(413, 23);
            氏名.TabIndex = 4;
            氏名.Validated += 氏名_Validated;
            // 
            // 社員区分
            // 
            社員区分.Controls.Add(社員区分Button3);
            社員区分.Controls.Add(社員区分Button2);
            社員区分.Controls.Add(社員区分Button1);
            社員区分.Location = new Point(26, 33);
            社員区分.Name = "社員区分";
            社員区分.Size = new Size(525, 63);
            社員区分.TabIndex = 205;
            社員区分.TabStop = false;
            社員区分.Text = "社員区分(&G)";
            // 
            // 社員区分Button3
            // 
            社員区分Button3.AutoSize = true;
            社員区分Button3.Location = new Point(347, 22);
            社員区分Button3.Name = "社員区分Button3";
            社員区分Button3.Size = new Size(77, 19);
            社員区分Button3.TabIndex = 204;
            社員区分Button3.TabStop = true;
            社員区分Button3.Text = "指定しない";
            社員区分Button3.UseVisualStyleBackColor = true;
            // 
            // 社員区分Button2
            // 
            社員区分Button2.AutoSize = true;
            社員区分Button2.Location = new Point(195, 22);
            社員区分Button2.Name = "社員区分Button2";
            社員区分Button2.Size = new Size(51, 19);
            社員区分Button2.TabIndex = 2;
            社員区分Button2.TabStop = true;
            社員区分Button2.Text = "パート";
            社員区分Button2.UseVisualStyleBackColor = true;
            // 
            // 社員区分Button1
            // 
            社員区分Button1.AutoSize = true;
            社員区分Button1.Location = new Point(30, 22);
            社員区分Button1.Name = "社員区分Button1";
            社員区分Button1.Size = new Size(61, 19);
            社員区分Button1.TabIndex = 1;
            社員区分Button1.TabStop = true;
            社員区分Button1.Text = "正社員";
            社員区分Button1.UseVisualStyleBackColor = true;
            // 
            // 退社指定
            // 
            退社指定.Controls.Add(退社指定Button3);
            退社指定.Controls.Add(退社指定Button2);
            退社指定.Controls.Add(退社指定Button1);
            退社指定.Location = new Point(26, 160);
            退社指定.Name = "退社指定";
            退社指定.Size = new Size(525, 63);
            退社指定.TabIndex = 207;
            退社指定.TabStop = false;
            退社指定.Text = "退社指定(&R)";
            // 
            // 退社指定Button3
            // 
            退社指定Button3.AutoSize = true;
            退社指定Button3.Location = new Point(347, 22);
            退社指定Button3.Name = "退社指定Button3";
            退社指定Button3.Size = new Size(77, 19);
            退社指定Button3.TabIndex = 204;
            退社指定Button3.TabStop = true;
            退社指定Button3.Text = "指定しない";
            退社指定Button3.UseVisualStyleBackColor = true;
            // 
            // 退社指定Button2
            // 
            退社指定Button2.AutoSize = true;
            退社指定Button2.Location = new Point(195, 22);
            退社指定Button2.Name = "退社指定Button2";
            退社指定Button2.Size = new Size(85, 19);
            退社指定Button2.TabIndex = 2;
            退社指定Button2.TabStop = true;
            退社指定Button2.Text = "退社している";
            退社指定Button2.UseVisualStyleBackColor = true;
            // 
            // 退社指定Button1
            // 
            退社指定Button1.AutoSize = true;
            退社指定Button1.Location = new Point(30, 22);
            退社指定Button1.Name = "退社指定Button1";
            退社指定Button1.Size = new Size(96, 19);
            退社指定Button1.TabIndex = 1;
            退社指定Button1.TabStop = true;
            退社指定Button1.Text = "退社していない";
            退社指定Button1.UseVisualStyleBackColor = true;
            // 
            // 抽出ボタン
            // 
            抽出ボタン.Location = new Point(314, 369);
            抽出ボタン.Name = "抽出ボタン";
            抽出ボタン.Size = new Size(114, 23);
            抽出ボタン.TabIndex = 208;
            抽出ボタン.Text = "抽出";
            抽出ボタン.UseVisualStyleBackColor = true;
            抽出ボタン.Click += 抽出ボタン_Click;
            // 
            // キャンセルボタン
            // 
            キャンセルボタン.Location = new Point(433, 369);
            キャンセルボタン.Name = "キャンセルボタン";
            キャンセルボタン.Size = new Size(114, 23);
            キャンセルボタン.TabIndex = 209;
            キャンセルボタン.Text = "キャンセル";
            キャンセルボタン.UseVisualStyleBackColor = true;
            キャンセルボタン.MouseClick += キャンセルボタン_MouseClick;
            // 
            // F_社員管理_抽出
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(577, 410);
            Controls.Add(キャンセルボタン);
            Controls.Add(抽出ボタン);
            Controls.Add(退社指定);
            Controls.Add(社員区分);
            Controls.Add(label7);
            Controls.Add(氏名);
            Name = "F_社員管理_抽出";
            Text = "社員管理_抽出";
            Load += Form_Load;
            社員区分.ResumeLayout(false);
            社員区分.PerformLayout();
            退社指定.ResumeLayout(false);
            退社指定.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox 更新者名;
        private Label label9;
        private Label label7;
        private TextBox 氏名;
        private Label label2;
        private TextBox 更新日終了;
        private GroupBox 社員区分;
        private RadioButton 社員区分Button3;
        private RadioButton 社員区分Button2;
        private RadioButton 社員区分Button1;
        private GroupBox 退社指定;
        private RadioButton 退社指定Button3;
        private RadioButton 退社指定Button2;
        private RadioButton 退社指定Button1;
        private Button 抽出ボタン;
        private Button キャンセルボタン;
    }
}
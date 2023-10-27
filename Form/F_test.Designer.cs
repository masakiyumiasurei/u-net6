namespace u_net
{
    partial class F_test
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
            this.日付選択 = new System.Windows.Forms.Button();
            this.日付 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.顧客コード = new System.Windows.Forms.TextBox();
            this.顧客選択ボタン = new System.Windows.Forms.Button();
            this.SuspendLayout();
            日付選択 = new Button();
            日付 = new TextBox();
            label1 = new Label();
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // 日付選択
            // 
            日付選択.Location = new Point(358, 289);
            日付選択.Margin = new Padding(4);
            日付選択.Name = "日付選択";
            日付選択.Size = new Size(29, 23);
            日付選択.TabIndex = 0;
            日付選択.Text = "▼";
            日付選択.UseVisualStyleBackColor = true;
            日付選択.Click += 日付選択_Click;
            // 
            // 日付
            // 
            日付.Location = new Point(232, 289);
            日付.Name = "日付";
            日付.Size = new Size(126, 23);
            日付.TabIndex = 10002;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(195, 293);
            label1.Name = "label1";
            label1.Size = new Size(31, 15);
            label1.TabIndex = 10003;
            label1.Text = "日付";
            // 
            // button1
            // 
            button1.Location = new Point(374, 139);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 10004;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(156, 381);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 15);
            this.label2.TabIndex = 10006;
            this.label2.Text = "顧客コード";
            // 
            // 顧客コード
            // 
            this.顧客コード.Location = new System.Drawing.Point(217, 377);
            this.顧客コード.Name = "顧客コード";
            this.顧客コード.Size = new System.Drawing.Size(126, 23);
            this.顧客コード.TabIndex = 10005;
            // 
            // 顧客選択ボタン
            // 
            this.顧客選択ボタン.Location = new System.Drawing.Point(343, 377);
            this.顧客選択ボタン.Margin = new System.Windows.Forms.Padding(4);
            this.顧客選択ボタン.Name = "顧客選択ボタン";
            this.顧客選択ボタン.Size = new System.Drawing.Size(29, 23);
            this.顧客選択ボタン.TabIndex = 10004;
            this.顧客選択ボタン.Text = "▼";
            this.顧客選択ボタン.UseVisualStyleBackColor = true;
            this.顧客選択ボタン.Click += new System.EventHandler(this.顧客選択ボタン_Click);
            // 
            // button2
            // 
            button2.Location = new Point(374, 179);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 10005;
            button2.Text = "button2";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // F_test
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(934, 562);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(日付);
            Controls.Add(日付選択);
            Margin = new Padding(4);
            Name = "F_test";
            Text = "main";
            ResumeLayout(false);
            PerformLayout();
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 562);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.顧客コード);
            this.Controls.Add(this.顧客選択ボタン);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.日付);
            this.Controls.Add(this.日付選択);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "F_test";
            this.Text = "main";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button 日付選択;
        private TextBox 日付;
        private Label label1;
        private Button button1;
        private Button button2;
        private Label label2;
        private TextBox 顧客コード;
        private Button 顧客選択ボタン;
    }
}
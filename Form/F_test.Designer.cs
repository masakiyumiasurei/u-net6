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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_test));
            this.日付選択 = new System.Windows.Forms.Button();
            this.日付 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.顧客コード = new System.Windows.Forms.TextBox();
            this.顧客選択ボタン = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // 日付選択
            // 
            this.日付選択.Location = new System.Drawing.Point(358, 289);
            this.日付選択.Margin = new System.Windows.Forms.Padding(4);
            this.日付選択.Name = "日付選択";
            this.日付選択.Size = new System.Drawing.Size(29, 23);
            this.日付選択.TabIndex = 0;
            this.日付選択.Text = "▼";
            this.日付選択.UseVisualStyleBackColor = true;
            this.日付選択.Click += new System.EventHandler(this.日付選択_Click);
            // 
            // 日付
            // 
            this.日付.Location = new System.Drawing.Point(232, 289);
            this.日付.Name = "日付";
            this.日付.Size = new System.Drawing.Size(126, 23);
            this.日付.TabIndex = 10002;
            this.日付.KeyDown += new System.Windows.Forms.KeyEventHandler(this.日付_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(195, 293);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 15);
            this.label1.TabIndex = 10003;
            this.label1.Text = "日付";
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
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(374, 139);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10004;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(374, 179);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 10005;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(170, 170);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 10007;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(147, 199);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(126, 23);
            this.textBox1.TabIndex = 10008;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(45, 241);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(126, 23);
            this.textBox2.TabIndex = 10009;
            // 
            // F_test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 562);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.顧客コード);
            this.Controls.Add(this.顧客選択ボタン);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.日付);
            this.Controls.Add(this.日付選択);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
        private Button button3;
        private TextBox textBox1;
        private TextBox textBox2;
    }
}
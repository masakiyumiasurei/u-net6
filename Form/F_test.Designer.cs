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
            // F_test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 562);
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
    }
}
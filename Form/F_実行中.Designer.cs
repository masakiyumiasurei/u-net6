namespace u_net
{
    partial class F_実行中
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
            this.処理内容 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // 処理内容
            // 
            this.処理内容.BackColor = System.Drawing.Color.Black;
            this.処理内容.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.処理内容.Enabled = false;
            this.処理内容.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.処理内容.ForeColor = System.Drawing.Color.White;
            this.処理内容.Location = new System.Drawing.Point(-3, 23);
            this.処理内容.Name = "処理内容";
            this.処理内容.Size = new System.Drawing.Size(208, 16);
            this.処理内容.TabIndex = 0;
            this.処理内容.Text = "登録中...";
            this.処理内容.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // F_実行中
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(205, 67);
            this.Controls.Add(this.処理内容);
            this.Name = "F_実行中";
            this.Text = "F_実行中";
            this.Load += new System.EventHandler(this.F_実行中_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox 処理内容;
    }
}
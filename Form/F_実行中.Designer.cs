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
            処理内容 = new TextBox();
            SuspendLayout();
            // 
            // 処理内容
            // 
            処理内容.BackColor = Color.Black;
            処理内容.BorderStyle = BorderStyle.None;
            処理内容.Enabled = false;
            処理内容.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold, GraphicsUnit.Point);
            処理内容.ForeColor = Color.White;
            処理内容.Location = new Point(-3, 23);
            処理内容.Name = "処理内容";
            処理内容.Size = new Size(208, 16);
            処理内容.TabIndex = 0;
            処理内容.Text = "登録中...";
            処理内容.TextAlign = HorizontalAlignment.Center;
            // 
            // F_実行中
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(205, 67);
            Controls.Add(処理内容);
            Name = "F_実行中";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "F_実行中";
            Load += F_実行中_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox 処理内容;
    }
}
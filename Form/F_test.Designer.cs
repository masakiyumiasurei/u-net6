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
            this.テスト = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // テスト
            // 
            this.テスト.Location = new System.Drawing.Point(577, 329);
            this.テスト.Name = "テスト";
            this.テスト.Size = new System.Drawing.Size(93, 43);
            this.テスト.TabIndex = 0;
            this.テスト.Text = "button1";
            this.テスト.UseVisualStyleBackColor = true;
            this.テスト.Click += new System.EventHandler(this.テスト_Click);
            // 
            // F_main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.テスト);
            this.Name = "F_main";
            this.Text = "main";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button テスト;
    }
}
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
            テスト = new Button();
            SuspendLayout();
            // 
            // テスト
            // 
            テスト.Location = new Point(265, 92);
            テスト.Margin = new Padding(4, 5, 4, 5);
            テスト.Name = "テスト";
            テスト.Size = new Size(124, 72);
            テスト.TabIndex = 0;
            テスト.Text = "button1";
            テスト.UseVisualStyleBackColor = true;
            テスト.Click += テスト_Click;
            // 
            // F_test
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1067, 750);
            Controls.Add(テスト);
            Margin = new Padding(4, 5, 4, 5);
            Name = "F_test";
            Text = "main";
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button テスト;
    }
}
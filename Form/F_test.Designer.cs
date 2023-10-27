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
        }

        #endregion

        private System.Windows.Forms.Button 日付選択;
        private TextBox 日付;
        private Label label1;
        private Button button1;
        private Button button2;
    }
}
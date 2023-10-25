namespace u_net
{
    partial class F_カレンダー
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private TextBox 日付;
        private Button btnOK;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        private void InitializeComponent()
        {
            this.日付 = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.cmdPrevYear = new System.Windows.Forms.Button();
            this.cmdNextYear = new System.Windows.Forms.Button();
            this.cmdPrevMonth = new System.Windows.Forms.Button();
            this.cmdNextMonth = new System.Windows.Forms.Button();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.d00 = new System.Windows.Forms.Button();
            this.d01 = new System.Windows.Forms.Button();
            this.d02 = new System.Windows.Forms.Button();
            this.d03 = new System.Windows.Forms.Button();
            this.d04 = new System.Windows.Forms.Button();
            this.d05 = new System.Windows.Forms.Button();
            this.d06 = new System.Windows.Forms.Button();
            this.d16 = new System.Windows.Forms.Button();
            this.d15 = new System.Windows.Forms.Button();
            this.d14 = new System.Windows.Forms.Button();
            this.d13 = new System.Windows.Forms.Button();
            this.d12 = new System.Windows.Forms.Button();
            this.d11 = new System.Windows.Forms.Button();
            this.d10 = new System.Windows.Forms.Button();
            this.d26 = new System.Windows.Forms.Button();
            this.d25 = new System.Windows.Forms.Button();
            this.d24 = new System.Windows.Forms.Button();
            this.d23 = new System.Windows.Forms.Button();
            this.d22 = new System.Windows.Forms.Button();
            this.d21 = new System.Windows.Forms.Button();
            this.d20 = new System.Windows.Forms.Button();
            this.d36 = new System.Windows.Forms.Button();
            this.d35 = new System.Windows.Forms.Button();
            this.d34 = new System.Windows.Forms.Button();
            this.d33 = new System.Windows.Forms.Button();
            this.d32 = new System.Windows.Forms.Button();
            this.d31 = new System.Windows.Forms.Button();
            this.d30 = new System.Windows.Forms.Button();
            this.d46 = new System.Windows.Forms.Button();
            this.d45 = new System.Windows.Forms.Button();
            this.d44 = new System.Windows.Forms.Button();
            this.d43 = new System.Windows.Forms.Button();
            this.d42 = new System.Windows.Forms.Button();
            this.d41 = new System.Windows.Forms.Button();
            this.d40 = new System.Windows.Forms.Button();
            this.d56 = new System.Windows.Forms.Button();
            this.d55 = new System.Windows.Forms.Button();
            this.d54 = new System.Windows.Forms.Button();
            this.d53 = new System.Windows.Forms.Button();
            this.d52 = new System.Windows.Forms.Button();
            this.d51 = new System.Windows.Forms.Button();
            this.d50 = new System.Windows.Forms.Button();
            this.SetTodayButton = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cboMonth = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // 日付
            // 
            this.日付.Location = new System.Drawing.Point(87, 134);
            this.日付.Name = "日付";
            this.日付.Size = new System.Drawing.Size(123, 23);
            this.日付.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(29, 48);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(88, 49);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "button1";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // cmdPrevYear
            // 
            this.cmdPrevYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmdPrevYear.Location = new System.Drawing.Point(0, 0);
            this.cmdPrevYear.Name = "cmdPrevYear";
            this.cmdPrevYear.Size = new System.Drawing.Size(25, 25);
            this.cmdPrevYear.TabIndex = 0;
            this.cmdPrevYear.Text = "-";
            this.cmdPrevYear.UseVisualStyleBackColor = true;
            this.cmdPrevYear.Click += new System.EventHandler(this.cmdPrevYear_Click);
            // 
            // cmdNextYear
            // 
            this.cmdNextYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmdNextYear.Location = new System.Drawing.Point(105, 0);
            this.cmdNextYear.Name = "cmdNextYear";
            this.cmdNextYear.Size = new System.Drawing.Size(25, 25);
            this.cmdNextYear.TabIndex = 1;
            this.cmdNextYear.Text = "+";
            this.cmdNextYear.UseVisualStyleBackColor = true;
            this.cmdNextYear.Click += new System.EventHandler(this.cmdNextYear_Click);
            // 
            // cmdPrevMonth
            // 
            this.cmdPrevMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmdPrevMonth.Location = new System.Drawing.Point(149, 0);
            this.cmdPrevMonth.Name = "cmdPrevMonth";
            this.cmdPrevMonth.Size = new System.Drawing.Size(25, 25);
            this.cmdPrevMonth.TabIndex = 2;
            this.cmdPrevMonth.Text = "-";
            this.cmdPrevMonth.UseVisualStyleBackColor = true;
            this.cmdPrevMonth.Click += new System.EventHandler(this.cmdPrevMonth_Click);
            // 
            // cmdNextMonth
            // 
            this.cmdNextMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmdNextMonth.Location = new System.Drawing.Point(255, 0);
            this.cmdNextMonth.Name = "cmdNextMonth";
            this.cmdNextMonth.Size = new System.Drawing.Size(25, 25);
            this.cmdNextMonth.TabIndex = 3;
            this.cmdNextMonth.Text = "+";
            this.cmdNextMonth.UseVisualStyleBackColor = true;
            this.cmdNextMonth.Click += new System.EventHandler(this.cmdNextMonth_Click);
            // 
            // txtYear
            // 
            this.txtYear.Font = new System.Drawing.Font("ＭＳ ゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtYear.Location = new System.Drawing.Point(25, 0);
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(80, 23);
            this.txtYear.TabIndex = 4;
            this.txtYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtYear.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtYear_KeyPress);
            this.txtYear.Validating += new System.ComponentModel.CancelEventHandler(this.txtYear_Validating);
            this.txtYear.Validated += new System.EventHandler(this.txtYear_Validated);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(10, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "日";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(50, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "月";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(89, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "火";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(131, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "水";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(170, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "木";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(211, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(22, 17);
            this.label6.TabIndex = 11;
            this.label6.Text = "金";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.ForeColor = System.Drawing.Color.Blue;
            this.label7.Location = new System.Drawing.Point(250, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(22, 17);
            this.label7.TabIndex = 12;
            this.label7.Text = "土";
            // 
            // d00
            // 
            this.d00.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.d00.Location = new System.Drawing.Point(0, 50);
            this.d00.Name = "d00";
            this.d00.Size = new System.Drawing.Size(40, 30);
            this.d00.TabIndex = 13;
            this.d00.Text = "1";
            this.d00.UseVisualStyleBackColor = true;
            this.d00.Click += new System.EventHandler(this.d00_Click);
            // 
            // d01
            // 
            this.d01.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.d01.Location = new System.Drawing.Point(40, 50);
            this.d01.Name = "d01";
            this.d01.Size = new System.Drawing.Size(40, 30);
            this.d01.TabIndex = 14;
            this.d01.Text = "1";
            this.d01.UseVisualStyleBackColor = true;
            this.d01.Click += new System.EventHandler(this.d01_Click);
            // 
            // d02
            // 
            this.d02.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.d02.Location = new System.Drawing.Point(80, 50);
            this.d02.Name = "d02";
            this.d02.Size = new System.Drawing.Size(40, 30);
            this.d02.TabIndex = 15;
            this.d02.Text = "1";
            this.d02.UseVisualStyleBackColor = true;
            this.d02.Click += new System.EventHandler(this.d02_Click);
            // 
            // d03
            // 
            this.d03.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.d03.Location = new System.Drawing.Point(120, 50);
            this.d03.Name = "d03";
            this.d03.Size = new System.Drawing.Size(40, 30);
            this.d03.TabIndex = 16;
            this.d03.Text = "1";
            this.d03.UseVisualStyleBackColor = true;
            this.d03.Click += new System.EventHandler(this.d03_Click);
            // 
            // d04
            // 
            this.d04.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.d04.Location = new System.Drawing.Point(160, 50);
            this.d04.Name = "d04";
            this.d04.Size = new System.Drawing.Size(40, 30);
            this.d04.TabIndex = 17;
            this.d04.Text = "1";
            this.d04.UseVisualStyleBackColor = true;
            this.d04.Click += new System.EventHandler(this.d04_Click);
            // 
            // d05
            // 
            this.d05.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.d05.Location = new System.Drawing.Point(200, 50);
            this.d05.Name = "d05";
            this.d05.Size = new System.Drawing.Size(40, 30);
            this.d05.TabIndex = 18;
            this.d05.Text = "1";
            this.d05.UseVisualStyleBackColor = true;
            this.d05.Click += new System.EventHandler(this.d05_Click);
            // 
            // d06
            // 
            this.d06.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.d06.Location = new System.Drawing.Point(240, 50);
            this.d06.Name = "d06";
            this.d06.Size = new System.Drawing.Size(40, 30);
            this.d06.TabIndex = 19;
            this.d06.Text = "1";
            this.d06.UseVisualStyleBackColor = true;
            this.d06.Click += new System.EventHandler(this.d06_Click);
            // 
            // d16
            // 
            this.d16.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.d16.Location = new System.Drawing.Point(240, 80);
            this.d16.Name = "d16";
            this.d16.Size = new System.Drawing.Size(40, 30);
            this.d16.TabIndex = 26;
            this.d16.Text = "1";
            this.d16.UseVisualStyleBackColor = true;
            this.d16.Click += new System.EventHandler(this.d16_Click);
            // 
            // d15
            // 
            this.d15.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.d15.Location = new System.Drawing.Point(200, 80);
            this.d15.Name = "d15";
            this.d15.Size = new System.Drawing.Size(40, 30);
            this.d15.TabIndex = 25;
            this.d15.Text = "1";
            this.d15.UseVisualStyleBackColor = true;
            this.d15.Click += new System.EventHandler(this.d15_Click);
            // 
            // d14
            // 
            this.d14.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.d14.Location = new System.Drawing.Point(160, 80);
            this.d14.Name = "d14";
            this.d14.Size = new System.Drawing.Size(40, 30);
            this.d14.TabIndex = 24;
            this.d14.Text = "1";
            this.d14.UseVisualStyleBackColor = true;
            this.d14.Click += new System.EventHandler(this.d14_Click);
            // 
            // d13
            // 
            this.d13.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.d13.Location = new System.Drawing.Point(120, 80);
            this.d13.Name = "d13";
            this.d13.Size = new System.Drawing.Size(40, 30);
            this.d13.TabIndex = 23;
            this.d13.Text = "1";
            this.d13.UseVisualStyleBackColor = true;
            this.d13.Click += new System.EventHandler(this.d13_Click);
            // 
            // d12
            // 
            this.d12.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.d12.Location = new System.Drawing.Point(80, 80);
            this.d12.Name = "d12";
            this.d12.Size = new System.Drawing.Size(40, 30);
            this.d12.TabIndex = 22;
            this.d12.Text = "1";
            this.d12.UseVisualStyleBackColor = true;
            this.d12.Click += new System.EventHandler(this.d12_Click);
            // 
            // d11
            // 
            this.d11.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.d11.Location = new System.Drawing.Point(40, 80);
            this.d11.Name = "d11";
            this.d11.Size = new System.Drawing.Size(40, 30);
            this.d11.TabIndex = 21;
            this.d11.Text = "1";
            this.d11.UseVisualStyleBackColor = true;
            this.d11.Click += new System.EventHandler(this.d11_Click);
            // 
            // d10
            // 
            this.d10.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.d10.Location = new System.Drawing.Point(0, 80);
            this.d10.Name = "d10";
            this.d10.Size = new System.Drawing.Size(40, 30);
            this.d10.TabIndex = 20;
            this.d10.Text = "1";
            this.d10.UseVisualStyleBackColor = true;
            this.d10.Click += new System.EventHandler(this.d10_Click);
            // 
            // d26
            // 
            this.d26.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.d26.Location = new System.Drawing.Point(240, 110);
            this.d26.Name = "d26";
            this.d26.Size = new System.Drawing.Size(40, 30);
            this.d26.TabIndex = 33;
            this.d26.Text = "1";
            this.d26.UseVisualStyleBackColor = true;
            this.d26.Click += new System.EventHandler(this.d26_Click);
            // 
            // d25
            // 
            this.d25.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.d25.Location = new System.Drawing.Point(200, 110);
            this.d25.Name = "d25";
            this.d25.Size = new System.Drawing.Size(40, 30);
            this.d25.TabIndex = 32;
            this.d25.Text = "1";
            this.d25.UseVisualStyleBackColor = true;
            this.d25.Click += new System.EventHandler(this.d25_Click);
            // 
            // d24
            // 
            this.d24.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.d24.Location = new System.Drawing.Point(160, 110);
            this.d24.Name = "d24";
            this.d24.Size = new System.Drawing.Size(40, 30);
            this.d24.TabIndex = 31;
            this.d24.Text = "1";
            this.d24.UseVisualStyleBackColor = true;
            this.d24.Click += new System.EventHandler(this.d24_Click);
            // 
            // d23
            // 
            this.d23.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.d23.Location = new System.Drawing.Point(120, 110);
            this.d23.Name = "d23";
            this.d23.Size = new System.Drawing.Size(40, 30);
            this.d23.TabIndex = 30;
            this.d23.Text = "1";
            this.d23.UseVisualStyleBackColor = true;
            this.d23.Click += new System.EventHandler(this.d23_Click);
            // 
            // d22
            // 
            this.d22.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.d22.Location = new System.Drawing.Point(80, 110);
            this.d22.Name = "d22";
            this.d22.Size = new System.Drawing.Size(40, 30);
            this.d22.TabIndex = 29;
            this.d22.Text = "1";
            this.d22.UseVisualStyleBackColor = true;
            this.d22.Click += new System.EventHandler(this.d22_Click);
            // 
            // d21
            // 
            this.d21.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.d21.Location = new System.Drawing.Point(40, 110);
            this.d21.Name = "d21";
            this.d21.Size = new System.Drawing.Size(40, 30);
            this.d21.TabIndex = 28;
            this.d21.Text = "1";
            this.d21.UseVisualStyleBackColor = true;
            this.d21.Click += new System.EventHandler(this.d21_Click);
            // 
            // d20
            // 
            this.d20.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.d20.Location = new System.Drawing.Point(0, 110);
            this.d20.Name = "d20";
            this.d20.Size = new System.Drawing.Size(40, 30);
            this.d20.TabIndex = 27;
            this.d20.Text = "1";
            this.d20.UseVisualStyleBackColor = true;
            this.d20.Click += new System.EventHandler(this.d20_Click);
            // 
            // d36
            // 
            this.d36.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.d36.Location = new System.Drawing.Point(240, 140);
            this.d36.Name = "d36";
            this.d36.Size = new System.Drawing.Size(40, 30);
            this.d36.TabIndex = 40;
            this.d36.Text = "1";
            this.d36.UseVisualStyleBackColor = true;
            this.d36.Click += new System.EventHandler(this.d36_Click);
            // 
            // d35
            // 
            this.d35.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.d35.Location = new System.Drawing.Point(200, 140);
            this.d35.Name = "d35";
            this.d35.Size = new System.Drawing.Size(40, 30);
            this.d35.TabIndex = 39;
            this.d35.Text = "1";
            this.d35.UseVisualStyleBackColor = true;
            this.d35.Click += new System.EventHandler(this.d35_Click);
            // 
            // d34
            // 
            this.d34.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.d34.Location = new System.Drawing.Point(160, 140);
            this.d34.Name = "d34";
            this.d34.Size = new System.Drawing.Size(40, 30);
            this.d34.TabIndex = 38;
            this.d34.Text = "1";
            this.d34.UseVisualStyleBackColor = true;
            this.d34.Click += new System.EventHandler(this.d34_Click);
            // 
            // d33
            // 
            this.d33.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.d33.Location = new System.Drawing.Point(120, 140);
            this.d33.Name = "d33";
            this.d33.Size = new System.Drawing.Size(40, 30);
            this.d33.TabIndex = 37;
            this.d33.Text = "1";
            this.d33.UseVisualStyleBackColor = true;
            this.d33.Click += new System.EventHandler(this.d33_Click);
            // 
            // d32
            // 
            this.d32.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.d32.Location = new System.Drawing.Point(80, 140);
            this.d32.Name = "d32";
            this.d32.Size = new System.Drawing.Size(40, 30);
            this.d32.TabIndex = 36;
            this.d32.Text = "1";
            this.d32.UseVisualStyleBackColor = true;
            this.d32.Click += new System.EventHandler(this.d32_Click);
            // 
            // d31
            // 
            this.d31.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.d31.Location = new System.Drawing.Point(40, 140);
            this.d31.Name = "d31";
            this.d31.Size = new System.Drawing.Size(40, 30);
            this.d31.TabIndex = 35;
            this.d31.Text = "1";
            this.d31.UseVisualStyleBackColor = true;
            this.d31.Click += new System.EventHandler(this.d31_Click);
            // 
            // d30
            // 
            this.d30.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.d30.Location = new System.Drawing.Point(0, 140);
            this.d30.Name = "d30";
            this.d30.Size = new System.Drawing.Size(40, 30);
            this.d30.TabIndex = 34;
            this.d30.Text = "1";
            this.d30.UseVisualStyleBackColor = true;
            this.d30.Click += new System.EventHandler(this.d30_Click);
            // 
            // d46
            // 
            this.d46.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.d46.Location = new System.Drawing.Point(240, 170);
            this.d46.Name = "d46";
            this.d46.Size = new System.Drawing.Size(40, 30);
            this.d46.TabIndex = 47;
            this.d46.Text = "1";
            this.d46.UseVisualStyleBackColor = true;
            this.d46.Click += new System.EventHandler(this.d46_Click);
            // 
            // d45
            // 
            this.d45.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.d45.Location = new System.Drawing.Point(200, 170);
            this.d45.Name = "d45";
            this.d45.Size = new System.Drawing.Size(40, 30);
            this.d45.TabIndex = 46;
            this.d45.Text = "1";
            this.d45.UseVisualStyleBackColor = true;
            this.d45.Click += new System.EventHandler(this.d45_Click);
            // 
            // d44
            // 
            this.d44.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.d44.Location = new System.Drawing.Point(160, 170);
            this.d44.Name = "d44";
            this.d44.Size = new System.Drawing.Size(40, 30);
            this.d44.TabIndex = 45;
            this.d44.Text = "1";
            this.d44.UseVisualStyleBackColor = true;
            this.d44.Click += new System.EventHandler(this.d44_Click);
            // 
            // d43
            // 
            this.d43.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.d43.Location = new System.Drawing.Point(120, 170);
            this.d43.Name = "d43";
            this.d43.Size = new System.Drawing.Size(40, 30);
            this.d43.TabIndex = 44;
            this.d43.Text = "1";
            this.d43.UseVisualStyleBackColor = true;
            this.d43.Click += new System.EventHandler(this.d43_Click);
            // 
            // d42
            // 
            this.d42.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.d42.Location = new System.Drawing.Point(80, 170);
            this.d42.Name = "d42";
            this.d42.Size = new System.Drawing.Size(40, 30);
            this.d42.TabIndex = 43;
            this.d42.Text = "1";
            this.d42.UseVisualStyleBackColor = true;
            this.d42.Click += new System.EventHandler(this.d42_Click);
            // 
            // d41
            // 
            this.d41.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.d41.Location = new System.Drawing.Point(40, 170);
            this.d41.Name = "d41";
            this.d41.Size = new System.Drawing.Size(40, 30);
            this.d41.TabIndex = 42;
            this.d41.Text = "1";
            this.d41.UseVisualStyleBackColor = true;
            this.d41.Click += new System.EventHandler(this.d41_Click);
            // 
            // d40
            // 
            this.d40.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.d40.Location = new System.Drawing.Point(0, 170);
            this.d40.Name = "d40";
            this.d40.Size = new System.Drawing.Size(40, 30);
            this.d40.TabIndex = 41;
            this.d40.Text = "1";
            this.d40.UseVisualStyleBackColor = true;
            this.d40.Click += new System.EventHandler(this.d40_Click);
            // 
            // d56
            // 
            this.d56.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.d56.Location = new System.Drawing.Point(240, 200);
            this.d56.Name = "d56";
            this.d56.Size = new System.Drawing.Size(40, 30);
            this.d56.TabIndex = 54;
            this.d56.Text = "1";
            this.d56.UseVisualStyleBackColor = true;
            this.d56.Click += new System.EventHandler(this.d56_Click);
            // 
            // d55
            // 
            this.d55.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.d55.Location = new System.Drawing.Point(200, 200);
            this.d55.Name = "d55";
            this.d55.Size = new System.Drawing.Size(40, 30);
            this.d55.TabIndex = 53;
            this.d55.Text = "1";
            this.d55.UseVisualStyleBackColor = true;
            this.d55.Click += new System.EventHandler(this.d55_Click);
            // 
            // d54
            // 
            this.d54.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.d54.Location = new System.Drawing.Point(160, 200);
            this.d54.Name = "d54";
            this.d54.Size = new System.Drawing.Size(40, 30);
            this.d54.TabIndex = 52;
            this.d54.Text = "1";
            this.d54.UseVisualStyleBackColor = true;
            this.d54.Click += new System.EventHandler(this.d54_Click);
            // 
            // d53
            // 
            this.d53.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.d53.Location = new System.Drawing.Point(120, 200);
            this.d53.Name = "d53";
            this.d53.Size = new System.Drawing.Size(40, 30);
            this.d53.TabIndex = 51;
            this.d53.Text = "1";
            this.d53.UseVisualStyleBackColor = true;
            this.d53.Click += new System.EventHandler(this.d53_Click);
            // 
            // d52
            // 
            this.d52.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.d52.Location = new System.Drawing.Point(80, 200);
            this.d52.Name = "d52";
            this.d52.Size = new System.Drawing.Size(40, 30);
            this.d52.TabIndex = 50;
            this.d52.Text = "1";
            this.d52.UseVisualStyleBackColor = true;
            this.d52.Click += new System.EventHandler(this.d52_Click);
            // 
            // d51
            // 
            this.d51.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.d51.Location = new System.Drawing.Point(40, 200);
            this.d51.Name = "d51";
            this.d51.Size = new System.Drawing.Size(40, 30);
            this.d51.TabIndex = 49;
            this.d51.Text = "1";
            this.d51.UseVisualStyleBackColor = true;
            this.d51.Click += new System.EventHandler(this.d51_Click);
            // 
            // d50
            // 
            this.d50.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.d50.Location = new System.Drawing.Point(0, 200);
            this.d50.Name = "d50";
            this.d50.Size = new System.Drawing.Size(40, 30);
            this.d50.TabIndex = 48;
            this.d50.Text = "1";
            this.d50.UseVisualStyleBackColor = true;
            this.d50.Click += new System.EventHandler(this.d50_Click);
            // 
            // SetTodayButton
            // 
            this.SetTodayButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SetTodayButton.Location = new System.Drawing.Point(0, 235);
            this.SetTodayButton.Name = "SetTodayButton";
            this.SetTodayButton.Size = new System.Drawing.Size(135, 25);
            this.SetTodayButton.TabIndex = 55;
            this.SetTodayButton.Text = "今日を設定";
            this.SetTodayButton.UseVisualStyleBackColor = true;
            this.SetTodayButton.Click += new System.EventHandler(this.SetTodayButton_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmdCancel.Location = new System.Drawing.Point(145, 235);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(135, 25);
            this.cmdCancel.TabIndex = 56;
            this.cmdCancel.Text = "キャンセル";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cboMonth
            // 
            this.cboMonth.Font = new System.Drawing.Font("ＭＳ ゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cboMonth.FormattingEnabled = true;
            this.cboMonth.Location = new System.Drawing.Point(175, 0);
            this.cboMonth.Name = "cboMonth";
            this.cboMonth.Size = new System.Drawing.Size(80, 24);
            this.cboMonth.TabIndex = 57;
            this.cboMonth.SelectedIndexChanged += new System.EventHandler(this.cboMonth_SelectedIndexChanged);
            // 
            // F_カレンダー
            // 
            this.ClientSize = new System.Drawing.Size(281, 266);
            this.Controls.Add(this.cboMonth);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.SetTodayButton);
            this.Controls.Add(this.d56);
            this.Controls.Add(this.d55);
            this.Controls.Add(this.d54);
            this.Controls.Add(this.d53);
            this.Controls.Add(this.d52);
            this.Controls.Add(this.d51);
            this.Controls.Add(this.d50);
            this.Controls.Add(this.d46);
            this.Controls.Add(this.d45);
            this.Controls.Add(this.d44);
            this.Controls.Add(this.d43);
            this.Controls.Add(this.d42);
            this.Controls.Add(this.d41);
            this.Controls.Add(this.d40);
            this.Controls.Add(this.d36);
            this.Controls.Add(this.d35);
            this.Controls.Add(this.d34);
            this.Controls.Add(this.d33);
            this.Controls.Add(this.d32);
            this.Controls.Add(this.d31);
            this.Controls.Add(this.d30);
            this.Controls.Add(this.d26);
            this.Controls.Add(this.d25);
            this.Controls.Add(this.d24);
            this.Controls.Add(this.d23);
            this.Controls.Add(this.d22);
            this.Controls.Add(this.d21);
            this.Controls.Add(this.d20);
            this.Controls.Add(this.d16);
            this.Controls.Add(this.d15);
            this.Controls.Add(this.d14);
            this.Controls.Add(this.d13);
            this.Controls.Add(this.d12);
            this.Controls.Add(this.d11);
            this.Controls.Add(this.d10);
            this.Controls.Add(this.d06);
            this.Controls.Add(this.d05);
            this.Controls.Add(this.d04);
            this.Controls.Add(this.d03);
            this.Controls.Add(this.d02);
            this.Controls.Add(this.d01);
            this.Controls.Add(this.d00);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtYear);
            this.Controls.Add(this.cmdNextMonth);
            this.Controls.Add(this.cmdPrevMonth);
            this.Controls.Add(this.cmdNextYear);
            this.Controls.Add(this.cmdPrevYear);
            this.Name = "F_カレンダー";
            this.Load += new System.EventHandler(this.Form_Open);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button cmdPrevYear;
        private Button cmdNextYear;
        private Button cmdPrevMonth;
        private Button cmdNextMonth;
        private TextBox txtYear;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Button d00;
        private Button d01;
        private Button d02;
        private Button d03;
        private Button d04;
        private Button d05;
        private Button d06;
        private Button d16;
        private Button d15;
        private Button d14;
        private Button d13;
        private Button d12;
        private Button d11;
        private Button d10;
        private Button d26;
        private Button d25;
        private Button d24;
        private Button d23;
        private Button d22;
        private Button d21;
        private Button d20;
        private Button d36;
        private Button d35;
        private Button d34;
        private Button d33;
        private Button d32;
        private Button d31;
        private Button d30;
        private Button d46;
        private Button d45;
        private Button d44;
        private Button d43;
        private Button d42;
        private Button d41;
        private Button d40;
        private Button d56;
        private Button d55;
        private Button d54;
        private Button d53;
        private Button d52;
        private Button d51;
        private Button d50;
        private Button SetTodayButton;
        private Button cmdCancel;
        private ComboBox cboMonth;
    }
}
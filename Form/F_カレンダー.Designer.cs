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
            components = new System.ComponentModel.Container();
            日付 = new TextBox();
            btnOK = new Button();
            cmdPrevYear = new Button();
            cmdNextYear = new Button();
            cmdPrevMonth = new Button();
            cmdNextMonth = new Button();
            txtYear = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            d00 = new Button();
            d01 = new Button();
            d02 = new Button();
            d03 = new Button();
            d04 = new Button();
            d05 = new Button();
            d06 = new Button();
            d16 = new Button();
            d15 = new Button();
            d14 = new Button();
            d13 = new Button();
            d12 = new Button();
            d11 = new Button();
            d10 = new Button();
            d26 = new Button();
            d25 = new Button();
            d24 = new Button();
            d23 = new Button();
            d22 = new Button();
            d21 = new Button();
            d20 = new Button();
            d36 = new Button();
            d35 = new Button();
            d34 = new Button();
            d33 = new Button();
            d32 = new Button();
            d31 = new Button();
            d30 = new Button();
            d46 = new Button();
            d45 = new Button();
            d44 = new Button();
            d43 = new Button();
            d42 = new Button();
            d41 = new Button();
            d40 = new Button();
            d56 = new Button();
            d55 = new Button();
            d54 = new Button();
            d53 = new Button();
            d52 = new Button();
            d51 = new Button();
            d50 = new Button();
            SetTodayButton = new Button();
            cmdCancel = new Button();
            cboMonth = new ComboBox();
            toolTip1 = new ToolTip(components);
            SuspendLayout();
            // 
            // 日付
            // 
            日付.Location = new Point(87, 134);
            日付.Name = "日付";
            日付.Size = new Size(123, 23);
            日付.TabIndex = 0;
            // 
            // btnOK
            // 
            btnOK.Location = new Point(29, 48);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(88, 49);
            btnOK.TabIndex = 1;
            btnOK.Text = "button1";
            btnOK.UseVisualStyleBackColor = true;
            // 
            // cmdPrevYear
            // 
            cmdPrevYear.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point);
            cmdPrevYear.Location = new Point(0, 0);
            cmdPrevYear.Name = "cmdPrevYear";
            cmdPrevYear.Size = new Size(25, 25);
            cmdPrevYear.TabIndex = 0;
            cmdPrevYear.Text = "-";
            toolTip1.SetToolTip(cmdPrevYear, "年数を減らします");
            cmdPrevYear.UseVisualStyleBackColor = true;
            cmdPrevYear.Click += cmdPrevYear_Click;
            // 
            // cmdNextYear
            // 
            cmdNextYear.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point);
            cmdNextYear.Location = new Point(105, 0);
            cmdNextYear.Name = "cmdNextYear";
            cmdNextYear.Size = new Size(25, 25);
            cmdNextYear.TabIndex = 1;
            cmdNextYear.Text = "+";
            toolTip1.SetToolTip(cmdNextYear, "年数を増やします");
            cmdNextYear.UseVisualStyleBackColor = true;
            cmdNextYear.Click += cmdNextYear_Click;
            // 
            // cmdPrevMonth
            // 
            cmdPrevMonth.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point);
            cmdPrevMonth.Location = new Point(149, 0);
            cmdPrevMonth.Name = "cmdPrevMonth";
            cmdPrevMonth.Size = new Size(25, 25);
            cmdPrevMonth.TabIndex = 2;
            cmdPrevMonth.Text = "-";
            toolTip1.SetToolTip(cmdPrevMonth, "月数を減らします");
            cmdPrevMonth.UseVisualStyleBackColor = true;
            cmdPrevMonth.Click += cmdPrevMonth_Click;
            // 
            // cmdNextMonth
            // 
            cmdNextMonth.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point);
            cmdNextMonth.Location = new Point(255, 0);
            cmdNextMonth.Name = "cmdNextMonth";
            cmdNextMonth.Size = new Size(25, 25);
            cmdNextMonth.TabIndex = 3;
            cmdNextMonth.Text = "+";
            toolTip1.SetToolTip(cmdNextMonth, "月数を増やします");
            cmdNextMonth.UseVisualStyleBackColor = true;
            cmdNextMonth.Click += cmdNextMonth_Click;
            // 
            // txtYear
            // 
            txtYear.Font = new Font("ＭＳ ゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtYear.Location = new Point(25, 0);
            txtYear.Name = "txtYear";
            txtYear.Size = new Size(80, 23);
            txtYear.TabIndex = 4;
            txtYear.TextAlign = HorizontalAlignment.Center;
            toolTip1.SetToolTip(txtYear, "年");
            txtYear.KeyPress += txtYear_KeyPress;
            txtYear.Validating += txtYear_Validating;
            txtYear.Validated += txtYear_Validated;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.Red;
            label1.Location = new Point(10, 30);
            label1.Name = "label1";
            label1.Size = new Size(22, 17);
            label1.TabIndex = 6;
            label1.Text = "日";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(50, 30);
            label2.Name = "label2";
            label2.Size = new Size(22, 17);
            label2.TabIndex = 7;
            label2.Text = "月";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(89, 30);
            label3.Name = "label3";
            label3.Size = new Size(22, 17);
            label3.TabIndex = 8;
            label3.Text = "火";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(131, 30);
            label4.Name = "label4";
            label4.Size = new Size(22, 17);
            label4.TabIndex = 9;
            label4.Text = "水";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(170, 30);
            label5.Name = "label5";
            label5.Size = new Size(22, 17);
            label5.TabIndex = 10;
            label5.Text = "木";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(211, 30);
            label6.Name = "label6";
            label6.Size = new Size(22, 17);
            label6.TabIndex = 11;
            label6.Text = "金";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label7.ForeColor = Color.Blue;
            label7.Location = new Point(250, 30);
            label7.Name = "label7";
            label7.Size = new Size(22, 17);
            label7.TabIndex = 12;
            label7.Text = "土";
            // 
            // d00
            // 
            d00.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            d00.Location = new Point(0, 50);
            d00.Name = "d00";
            d00.Size = new Size(40, 30);
            d00.TabIndex = 13;
            d00.Text = "1";
            d00.UseVisualStyleBackColor = true;
            d00.Click += d00_Click;
            // 
            // d01
            // 
            d01.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            d01.Location = new Point(40, 50);
            d01.Name = "d01";
            d01.Size = new Size(40, 30);
            d01.TabIndex = 14;
            d01.Text = "1";
            d01.UseVisualStyleBackColor = true;
            d01.Click += d01_Click;
            // 
            // d02
            // 
            d02.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            d02.Location = new Point(80, 50);
            d02.Name = "d02";
            d02.Size = new Size(40, 30);
            d02.TabIndex = 15;
            d02.Text = "1";
            d02.UseVisualStyleBackColor = true;
            d02.Click += d02_Click;
            // 
            // d03
            // 
            d03.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            d03.Location = new Point(120, 50);
            d03.Name = "d03";
            d03.Size = new Size(40, 30);
            d03.TabIndex = 16;
            d03.Text = "1";
            d03.UseVisualStyleBackColor = true;
            d03.Click += d03_Click;
            // 
            // d04
            // 
            d04.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            d04.Location = new Point(160, 50);
            d04.Name = "d04";
            d04.Size = new Size(40, 30);
            d04.TabIndex = 17;
            d04.Text = "1";
            d04.UseVisualStyleBackColor = true;
            d04.Click += d04_Click;
            // 
            // d05
            // 
            d05.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            d05.Location = new Point(200, 50);
            d05.Name = "d05";
            d05.Size = new Size(40, 30);
            d05.TabIndex = 18;
            d05.Text = "1";
            d05.UseVisualStyleBackColor = true;
            d05.Click += d05_Click;
            // 
            // d06
            // 
            d06.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            d06.Location = new Point(240, 50);
            d06.Name = "d06";
            d06.Size = new Size(40, 30);
            d06.TabIndex = 19;
            d06.Text = "1";
            d06.UseVisualStyleBackColor = true;
            d06.Click += d06_Click;
            // 
            // d16
            // 
            d16.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            d16.Location = new Point(240, 80);
            d16.Name = "d16";
            d16.Size = new Size(40, 30);
            d16.TabIndex = 26;
            d16.Text = "1";
            d16.UseVisualStyleBackColor = true;
            d16.Click += d16_Click;
            // 
            // d15
            // 
            d15.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            d15.Location = new Point(200, 80);
            d15.Name = "d15";
            d15.Size = new Size(40, 30);
            d15.TabIndex = 25;
            d15.Text = "1";
            d15.UseVisualStyleBackColor = true;
            d15.Click += d15_Click;
            // 
            // d14
            // 
            d14.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            d14.Location = new Point(160, 80);
            d14.Name = "d14";
            d14.Size = new Size(40, 30);
            d14.TabIndex = 24;
            d14.Text = "1";
            d14.UseVisualStyleBackColor = true;
            d14.Click += d14_Click;
            // 
            // d13
            // 
            d13.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            d13.Location = new Point(120, 80);
            d13.Name = "d13";
            d13.Size = new Size(40, 30);
            d13.TabIndex = 23;
            d13.Text = "1";
            d13.UseVisualStyleBackColor = true;
            d13.Click += d13_Click;
            // 
            // d12
            // 
            d12.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            d12.Location = new Point(80, 80);
            d12.Name = "d12";
            d12.Size = new Size(40, 30);
            d12.TabIndex = 22;
            d12.Text = "1";
            d12.UseVisualStyleBackColor = true;
            d12.Click += d12_Click;
            // 
            // d11
            // 
            d11.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            d11.Location = new Point(40, 80);
            d11.Name = "d11";
            d11.Size = new Size(40, 30);
            d11.TabIndex = 21;
            d11.Text = "1";
            d11.UseVisualStyleBackColor = true;
            d11.Click += d11_Click;
            // 
            // d10
            // 
            d10.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            d10.Location = new Point(0, 80);
            d10.Name = "d10";
            d10.Size = new Size(40, 30);
            d10.TabIndex = 20;
            d10.Text = "1";
            d10.UseVisualStyleBackColor = true;
            d10.Click += d10_Click;
            // 
            // d26
            // 
            d26.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            d26.Location = new Point(240, 110);
            d26.Name = "d26";
            d26.Size = new Size(40, 30);
            d26.TabIndex = 33;
            d26.Text = "1";
            d26.UseVisualStyleBackColor = true;
            d26.Click += d26_Click;
            // 
            // d25
            // 
            d25.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            d25.Location = new Point(200, 110);
            d25.Name = "d25";
            d25.Size = new Size(40, 30);
            d25.TabIndex = 32;
            d25.Text = "1";
            d25.UseVisualStyleBackColor = true;
            d25.Click += d25_Click;
            // 
            // d24
            // 
            d24.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            d24.Location = new Point(160, 110);
            d24.Name = "d24";
            d24.Size = new Size(40, 30);
            d24.TabIndex = 31;
            d24.Text = "1";
            d24.UseVisualStyleBackColor = true;
            d24.Click += d24_Click;
            // 
            // d23
            // 
            d23.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            d23.Location = new Point(120, 110);
            d23.Name = "d23";
            d23.Size = new Size(40, 30);
            d23.TabIndex = 30;
            d23.Text = "1";
            d23.UseVisualStyleBackColor = true;
            d23.Click += d23_Click;
            // 
            // d22
            // 
            d22.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            d22.Location = new Point(80, 110);
            d22.Name = "d22";
            d22.Size = new Size(40, 30);
            d22.TabIndex = 29;
            d22.Text = "1";
            d22.UseVisualStyleBackColor = true;
            d22.Click += d22_Click;
            // 
            // d21
            // 
            d21.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            d21.Location = new Point(40, 110);
            d21.Name = "d21";
            d21.Size = new Size(40, 30);
            d21.TabIndex = 28;
            d21.Text = "1";
            d21.UseVisualStyleBackColor = true;
            d21.Click += d21_Click;
            // 
            // d20
            // 
            d20.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            d20.Location = new Point(0, 110);
            d20.Name = "d20";
            d20.Size = new Size(40, 30);
            d20.TabIndex = 27;
            d20.Text = "1";
            d20.UseVisualStyleBackColor = true;
            d20.Click += d20_Click;
            // 
            // d36
            // 
            d36.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            d36.Location = new Point(240, 140);
            d36.Name = "d36";
            d36.Size = new Size(40, 30);
            d36.TabIndex = 40;
            d36.Text = "1";
            d36.UseVisualStyleBackColor = true;
            d36.Click += d36_Click;
            // 
            // d35
            // 
            d35.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            d35.Location = new Point(200, 140);
            d35.Name = "d35";
            d35.Size = new Size(40, 30);
            d35.TabIndex = 39;
            d35.Text = "1";
            d35.UseVisualStyleBackColor = true;
            d35.Click += d35_Click;
            // 
            // d34
            // 
            d34.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            d34.Location = new Point(160, 140);
            d34.Name = "d34";
            d34.Size = new Size(40, 30);
            d34.TabIndex = 38;
            d34.Text = "1";
            d34.UseVisualStyleBackColor = true;
            d34.Click += d34_Click;
            // 
            // d33
            // 
            d33.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            d33.Location = new Point(120, 140);
            d33.Name = "d33";
            d33.Size = new Size(40, 30);
            d33.TabIndex = 37;
            d33.Text = "1";
            d33.UseVisualStyleBackColor = true;
            d33.Click += d33_Click;
            // 
            // d32
            // 
            d32.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            d32.Location = new Point(80, 140);
            d32.Name = "d32";
            d32.Size = new Size(40, 30);
            d32.TabIndex = 36;
            d32.Text = "1";
            d32.UseVisualStyleBackColor = true;
            d32.Click += d32_Click;
            // 
            // d31
            // 
            d31.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            d31.Location = new Point(40, 140);
            d31.Name = "d31";
            d31.Size = new Size(40, 30);
            d31.TabIndex = 35;
            d31.Text = "1";
            d31.UseVisualStyleBackColor = true;
            d31.Click += d31_Click;
            // 
            // d30
            // 
            d30.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            d30.Location = new Point(0, 140);
            d30.Name = "d30";
            d30.Size = new Size(40, 30);
            d30.TabIndex = 34;
            d30.Text = "1";
            d30.UseVisualStyleBackColor = true;
            d30.Click += d30_Click;
            // 
            // d46
            // 
            d46.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            d46.Location = new Point(240, 170);
            d46.Name = "d46";
            d46.Size = new Size(40, 30);
            d46.TabIndex = 47;
            d46.Text = "1";
            d46.UseVisualStyleBackColor = true;
            d46.Click += d46_Click;
            // 
            // d45
            // 
            d45.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            d45.Location = new Point(200, 170);
            d45.Name = "d45";
            d45.Size = new Size(40, 30);
            d45.TabIndex = 46;
            d45.Text = "1";
            d45.UseVisualStyleBackColor = true;
            d45.Click += d45_Click;
            // 
            // d44
            // 
            d44.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            d44.Location = new Point(160, 170);
            d44.Name = "d44";
            d44.Size = new Size(40, 30);
            d44.TabIndex = 45;
            d44.Text = "1";
            d44.UseVisualStyleBackColor = true;
            d44.Click += d44_Click;
            // 
            // d43
            // 
            d43.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            d43.Location = new Point(120, 170);
            d43.Name = "d43";
            d43.Size = new Size(40, 30);
            d43.TabIndex = 44;
            d43.Text = "1";
            d43.UseVisualStyleBackColor = true;
            d43.Click += d43_Click;
            // 
            // d42
            // 
            d42.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            d42.Location = new Point(80, 170);
            d42.Name = "d42";
            d42.Size = new Size(40, 30);
            d42.TabIndex = 43;
            d42.Text = "1";
            d42.UseVisualStyleBackColor = true;
            d42.Click += d42_Click;
            // 
            // d41
            // 
            d41.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            d41.Location = new Point(40, 170);
            d41.Name = "d41";
            d41.Size = new Size(40, 30);
            d41.TabIndex = 42;
            d41.Text = "1";
            d41.UseVisualStyleBackColor = true;
            d41.Click += d41_Click;
            // 
            // d40
            // 
            d40.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            d40.Location = new Point(0, 170);
            d40.Name = "d40";
            d40.Size = new Size(40, 30);
            d40.TabIndex = 41;
            d40.Text = "1";
            d40.UseVisualStyleBackColor = true;
            d40.Click += d40_Click;
            // 
            // d56
            // 
            d56.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            d56.Location = new Point(240, 200);
            d56.Name = "d56";
            d56.Size = new Size(40, 30);
            d56.TabIndex = 54;
            d56.Text = "1";
            d56.UseVisualStyleBackColor = true;
            d56.Click += d56_Click;
            // 
            // d55
            // 
            d55.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            d55.Location = new Point(200, 200);
            d55.Name = "d55";
            d55.Size = new Size(40, 30);
            d55.TabIndex = 53;
            d55.Text = "1";
            d55.UseVisualStyleBackColor = true;
            d55.Click += d55_Click;
            // 
            // d54
            // 
            d54.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            d54.Location = new Point(160, 200);
            d54.Name = "d54";
            d54.Size = new Size(40, 30);
            d54.TabIndex = 52;
            d54.Text = "1";
            d54.UseVisualStyleBackColor = true;
            d54.Click += d54_Click;
            // 
            // d53
            // 
            d53.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            d53.Location = new Point(120, 200);
            d53.Name = "d53";
            d53.Size = new Size(40, 30);
            d53.TabIndex = 51;
            d53.Text = "1";
            d53.UseVisualStyleBackColor = true;
            d53.Click += d53_Click;
            // 
            // d52
            // 
            d52.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            d52.Location = new Point(80, 200);
            d52.Name = "d52";
            d52.Size = new Size(40, 30);
            d52.TabIndex = 50;
            d52.Text = "1";
            d52.UseVisualStyleBackColor = true;
            d52.Click += d52_Click;
            // 
            // d51
            // 
            d51.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            d51.Location = new Point(40, 200);
            d51.Name = "d51";
            d51.Size = new Size(40, 30);
            d51.TabIndex = 49;
            d51.Text = "1";
            d51.UseVisualStyleBackColor = true;
            d51.Click += d51_Click;
            // 
            // d50
            // 
            d50.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            d50.Location = new Point(0, 200);
            d50.Name = "d50";
            d50.Size = new Size(40, 30);
            d50.TabIndex = 48;
            d50.Text = "1";
            d50.UseVisualStyleBackColor = true;
            d50.Click += d50_Click;
            // 
            // SetTodayButton
            // 
            SetTodayButton.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point);
            SetTodayButton.Location = new Point(0, 235);
            SetTodayButton.Name = "SetTodayButton";
            SetTodayButton.Size = new Size(135, 25);
            SetTodayButton.TabIndex = 55;
            SetTodayButton.Text = "今日を設定";
            SetTodayButton.UseVisualStyleBackColor = true;
            SetTodayButton.Click += SetTodayButton_Click;
            // 
            // cmdCancel
            // 
            cmdCancel.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point);
            cmdCancel.Location = new Point(145, 235);
            cmdCancel.Name = "cmdCancel";
            cmdCancel.Size = new Size(135, 25);
            cmdCancel.TabIndex = 56;
            cmdCancel.Text = "キャンセル";
            cmdCancel.UseVisualStyleBackColor = true;
            cmdCancel.Click += cmdCancel_Click;
            // 
            // cboMonth
            // 
            cboMonth.Font = new Font("ＭＳ ゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point);
            cboMonth.FormattingEnabled = true;
            cboMonth.Location = new Point(175, 0);
            cboMonth.Name = "cboMonth";
            cboMonth.Size = new Size(80, 24);
            cboMonth.TabIndex = 57;
            toolTip1.SetToolTip(cboMonth, "月");
            cboMonth.SelectedIndexChanged += cboMonth_SelectedIndexChanged;
            // 
            // F_カレンダー
            // 
            ClientSize = new Size(281, 266);
            Controls.Add(cboMonth);
            Controls.Add(cmdCancel);
            Controls.Add(SetTodayButton);
            Controls.Add(d56);
            Controls.Add(d55);
            Controls.Add(d54);
            Controls.Add(d53);
            Controls.Add(d52);
            Controls.Add(d51);
            Controls.Add(d50);
            Controls.Add(d46);
            Controls.Add(d45);
            Controls.Add(d44);
            Controls.Add(d43);
            Controls.Add(d42);
            Controls.Add(d41);
            Controls.Add(d40);
            Controls.Add(d36);
            Controls.Add(d35);
            Controls.Add(d34);
            Controls.Add(d33);
            Controls.Add(d32);
            Controls.Add(d31);
            Controls.Add(d30);
            Controls.Add(d26);
            Controls.Add(d25);
            Controls.Add(d24);
            Controls.Add(d23);
            Controls.Add(d22);
            Controls.Add(d21);
            Controls.Add(d20);
            Controls.Add(d16);
            Controls.Add(d15);
            Controls.Add(d14);
            Controls.Add(d13);
            Controls.Add(d12);
            Controls.Add(d11);
            Controls.Add(d10);
            Controls.Add(d06);
            Controls.Add(d05);
            Controls.Add(d04);
            Controls.Add(d03);
            Controls.Add(d02);
            Controls.Add(d01);
            Controls.Add(d00);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtYear);
            Controls.Add(cmdNextMonth);
            Controls.Add(cmdPrevMonth);
            Controls.Add(cmdNextYear);
            Controls.Add(cmdPrevYear);
            Name = "F_カレンダー";
            StartPosition = FormStartPosition.CenterScreen;
            FormClosing += F_カレンダー_FormClosing;
            Load += Form_Open;
            ResumeLayout(false);
            PerformLayout();
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
        private ToolTip toolTip1;
    }
}
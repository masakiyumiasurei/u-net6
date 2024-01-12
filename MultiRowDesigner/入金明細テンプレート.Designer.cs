namespace MultiRowDesigner
{          
    [System.ComponentModel.ToolboxItem(true)]
    partial class 入金明細テンプレート
    {
        /// <summary> 
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region MultiRow Template Designer generated code

		/// <summary> 
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
        private void InitializeComponent()
        {
            GrapeCity.Win.MultiRow.CellStyle cellStyle9 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle10 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle11 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle12 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle13 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle14 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle15 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border1 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle6 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border2 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle7 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border3 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle8 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border4 = new GrapeCity.Win.MultiRow.Border();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.入金明細番号ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.削除ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.領収書出力ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.入金区分ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.備考コードボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.備考ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.入金金額ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.明細番号 = new GrapeCity.Win.MultiRow.RowHeaderCell();
            this.明細削除ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.領収証出力コード = new GrapeCity.Win.MultiRow.ComboBoxCell();
            this.入金区分コード = new GrapeCity.Win.MultiRow.ComboBoxCell();
            this.備考コード = new GrapeCity.Win.MultiRow.ComboBoxCell();
            this.備考 = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.入金金額 = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.入金コード = new GrapeCity.Win.MultiRow.TextBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.明細番号);
            this.Row.Cells.Add(this.明細削除ボタン);
            this.Row.Cells.Add(this.領収証出力コード);
            this.Row.Cells.Add(this.入金区分コード);
            this.Row.Cells.Add(this.備考コード);
            this.Row.Cells.Add(this.備考);
            this.Row.Cells.Add(this.入金金額);
            this.Row.Cells.Add(this.入金コード);
            this.Row.Height = 17;
            this.Row.Width = 707;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.入金明細番号ボタン);
            this.columnHeaderSection1.Cells.Add(this.削除ボタン);
            this.columnHeaderSection1.Cells.Add(this.領収書出力ボタン);
            this.columnHeaderSection1.Cells.Add(this.入金区分ボタン);
            this.columnHeaderSection1.Cells.Add(this.備考コードボタン);
            this.columnHeaderSection1.Cells.Add(this.備考ボタン);
            this.columnHeaderSection1.Cells.Add(this.入金金額ボタン);
            this.columnHeaderSection1.Height = 21;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 707;
            // 
            // 入金明細番号ボタン
            // 
            this.入金明細番号ボタン.Location = new System.Drawing.Point(20, -1);
            this.入金明細番号ボタン.Name = "入金明細番号ボタン";
            this.入金明細番号ボタン.Size = new System.Drawing.Size(27, 21);
            cellStyle9.BackColor = System.Drawing.SystemColors.Control;
            cellStyle9.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            cellStyle9.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle9.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle9.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.入金明細番号ボタン.Style = cellStyle9;
            this.入金明細番号ボタン.TabIndex = 0;
            this.入金明細番号ボタン.TabStop = false;
            this.入金明細番号ボタン.Value = "No";
            // 
            // 削除ボタン
            // 
            this.削除ボタン.Location = new System.Drawing.Point(0, 0);
            this.削除ボタン.Name = "削除ボタン";
            this.削除ボタン.Size = new System.Drawing.Size(20, 20);
            cellStyle10.BackColor = System.Drawing.SystemColors.Control;
            cellStyle10.Font = new System.Drawing.Font("BIZ UDPゴシック", 8F);
            cellStyle10.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle10.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle10.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.削除ボタン.Style = cellStyle10;
            this.削除ボタン.TabIndex = 1;
            this.削除ボタン.TabStop = false;
            // 
            // 領収書出力ボタン
            // 
            this.領収書出力ボタン.Location = new System.Drawing.Point(47, 0);
            this.領収書出力ボタン.Name = "領収書出力ボタン";
            this.領収書出力ボタン.Size = new System.Drawing.Size(82, 21);
            cellStyle11.BackColor = System.Drawing.SystemColors.Control;
            cellStyle11.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            cellStyle11.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle11.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle11.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.領収書出力ボタン.Style = cellStyle11;
            this.領収書出力ボタン.TabIndex = 2;
            this.領収書出力ボタン.TabStop = false;
            this.領収書出力ボタン.Value = "領収書出力";
            // 
            // 入金区分ボタン
            // 
            this.入金区分ボタン.Location = new System.Drawing.Point(129, 0);
            this.入金区分ボタン.Name = "入金区分ボタン";
            this.入金区分ボタン.Size = new System.Drawing.Size(102, 21);
            cellStyle12.BackColor = System.Drawing.SystemColors.Control;
            cellStyle12.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            cellStyle12.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle12.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle12.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.入金区分ボタン.Style = cellStyle12;
            this.入金区分ボタン.TabIndex = 3;
            this.入金区分ボタン.TabStop = false;
            this.入金区分ボタン.Value = "入金区分";
            // 
            // 備考コードボタン
            // 
            this.備考コードボタン.Location = new System.Drawing.Point(231, 0);
            this.備考コードボタン.Name = "備考コードボタン";
            this.備考コードボタン.Size = new System.Drawing.Size(68, 21);
            cellStyle13.BackColor = System.Drawing.SystemColors.Control;
            cellStyle13.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            cellStyle13.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle13.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle13.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.備考コードボタン.Style = cellStyle13;
            this.備考コードボタン.TabIndex = 4;
            this.備考コードボタン.TabStop = false;
            this.備考コードボタン.Value = "備考コード";
            // 
            // 備考ボタン
            // 
            this.備考ボタン.Location = new System.Drawing.Point(299, 0);
            this.備考ボタン.Name = "備考ボタン";
            this.備考ボタン.Size = new System.Drawing.Size(296, 21);
            cellStyle14.BackColor = System.Drawing.SystemColors.Control;
            cellStyle14.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            cellStyle14.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle14.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle14.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.備考ボタン.Style = cellStyle14;
            this.備考ボタン.TabIndex = 5;
            this.備考ボタン.TabStop = false;
            this.備考ボタン.Value = "備　　考";
            // 
            // 入金金額ボタン
            // 
            this.入金金額ボタン.Location = new System.Drawing.Point(595, -1);
            this.入金金額ボタン.Name = "入金金額ボタン";
            this.入金金額ボタン.Size = new System.Drawing.Size(112, 21);
            cellStyle15.BackColor = System.Drawing.SystemColors.Control;
            cellStyle15.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            cellStyle15.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle15.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle15.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.入金金額ボタン.Style = cellStyle15;
            this.入金金額ボタン.TabIndex = 6;
            this.入金金額ボタン.TabStop = false;
            this.入金金額ボタン.Value = "入金金額";
            // 
            // 明細番号
            // 
            this.明細番号.DataField = "明細番号";
            this.明細番号.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.明細番号.Location = new System.Drawing.Point(20, 0);
            this.明細番号.Name = "明細番号";
            this.明細番号.ShowIndicator = false;
            this.明細番号.ShowRowError = false;
            this.明細番号.Size = new System.Drawing.Size(27, 17);
            cellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle1.Border = border1;
            cellStyle1.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            this.明細番号.Style = cellStyle1;
            this.明細番号.TabIndex = 0;
            this.明細番号.TabStop = false;
            this.明細番号.ValueFormat = "%1%";
            // 
            // 明細削除ボタン
            // 
            this.明細削除ボタン.ButtonCommand = GrapeCity.Win.MultiRow.RowActionButtonCommands.Remove;
            this.明細削除ボタン.Location = new System.Drawing.Point(0, 0);
            this.明細削除ボタン.Name = "明細削除ボタン";
            this.明細削除ボタン.Size = new System.Drawing.Size(20, 17);
            cellStyle2.BackColor = System.Drawing.Color.White;
            cellStyle2.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            cellStyle2.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle2.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.明細削除ボタン.Style = cellStyle2;
            this.明細削除ボタン.TabIndex = 1;
            this.明細削除ボタン.TabStop = false;
            this.明細削除ボタン.Value = "×";
            // 
            // 領収証出力コード
            // 
            this.領収証出力コード.DataField = "領収証出力コード";
            this.領収証出力コード.Location = new System.Drawing.Point(47, 0);
            this.領収証出力コード.Name = "領収証出力コード";
            this.領収証出力コード.Size = new System.Drawing.Size(82, 17);
            cellStyle3.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            cellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.領収証出力コード.Style = cellStyle3;
            this.領収証出力コード.TabIndex = 1;
            // 
            // 入金区分コード
            // 
            this.入金区分コード.DataField = "入金区分コード";
            this.入金区分コード.Location = new System.Drawing.Point(129, 0);
            this.入金区分コード.Name = "入金区分コード";
            this.入金区分コード.Size = new System.Drawing.Size(102, 17);
            cellStyle4.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            cellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.入金区分コード.Style = cellStyle4;
            this.入金区分コード.TabIndex = 2;
            // 
            // 備考コード
            // 
            this.備考コード.DataField = "備考コード";
            this.備考コード.Location = new System.Drawing.Point(231, 0);
            this.備考コード.Name = "備考コード";
            this.備考コード.Size = new System.Drawing.Size(68, 17);
            cellStyle5.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            cellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.備考コード.Style = cellStyle5;
            this.備考コード.TabIndex = 3;
            // 
            // 備考
            // 
            this.備考.DataField = "備考";
            this.備考.Location = new System.Drawing.Point(299, 0);
            this.備考.Name = "備考";
            this.備考.Size = new System.Drawing.Size(296, 17);
            border2.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle6.Border = border2;
            cellStyle6.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            cellStyle6.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.備考.Style = cellStyle6;
            this.備考.TabIndex = 4;
            // 
            // 入金金額
            // 
            this.入金金額.DataField = "入金金額";
            this.入金金額.Location = new System.Drawing.Point(595, 0);
            this.入金金額.Name = "入金金額";
            this.入金金額.Size = new System.Drawing.Size(112, 17);
            border3.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle7.Border = border3;
            cellStyle7.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            cellStyle7.Format = "N0";
            cellStyle7.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.入金金額.Style = cellStyle7;
            this.入金金額.TabIndex = 5;
            // 
            // 入金コード
            // 
            this.入金コード.DataField = "入金コード";
            this.入金コード.Enabled = false;
            this.入金コード.Location = new System.Drawing.Point(645, 0);
            this.入金コード.Name = "入金コード";
            this.入金コード.ReadOnly = true;
            this.入金コード.Size = new System.Drawing.Size(10, 17);
            border4.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle8.Border = border4;
            cellStyle8.Font = new System.Drawing.Font("BIZ UDPゴシック", 10F);
            cellStyle8.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.入金コード.Style = cellStyle8;
            this.入金コード.TabIndex = 6;
            this.入金コード.TabStop = false;
            this.入金コード.Visible = false;
            // 
            // 入金明細テンプレート
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 38;
            this.Width = 707;

        }
        

        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private GrapeCity.Win.MultiRow.RowHeaderCell 明細番号;
        private GrapeCity.Win.MultiRow.ButtonCell 入金明細番号ボタン;
        private GrapeCity.Win.MultiRow.ButtonCell 削除ボタン;
        private GrapeCity.Win.MultiRow.ButtonCell 領収書出力ボタン;
        private GrapeCity.Win.MultiRow.ButtonCell 入金区分ボタン;
        private GrapeCity.Win.MultiRow.ButtonCell 備考コードボタン;
        private GrapeCity.Win.MultiRow.ButtonCell 備考ボタン;
        private GrapeCity.Win.MultiRow.ButtonCell 入金金額ボタン;
        private GrapeCity.Win.MultiRow.ButtonCell 明細削除ボタン;
        private GrapeCity.Win.MultiRow.ComboBoxCell 領収証出力コード;
        private GrapeCity.Win.MultiRow.ComboBoxCell 入金区分コード;
        private GrapeCity.Win.MultiRow.ComboBoxCell 備考コード;
        private GrapeCity.Win.MultiRow.TextBoxCell 備考;
        private GrapeCity.Win.MultiRow.TextBoxCell 入金金額;
        private GrapeCity.Win.MultiRow.TextBoxCell 入金コード;
    }
}

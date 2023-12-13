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
            GrapeCity.Win.MultiRow.CellStyle cellStyle15 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border4 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle22 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle23 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle24 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle25 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle26 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle27 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle28 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle16 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle17 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle18 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle19 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle20 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border5 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle21 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border6 = new GrapeCity.Win.MultiRow.Border();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.明細番号 = new GrapeCity.Win.MultiRow.RowHeaderCell();
            this.入金明細番号ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.削除ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.領収書出力ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.入金区分ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.備考コードボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.備考ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.入金金額ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.明細削除ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.領収証出力コード = new GrapeCity.Win.MultiRow.ComboBoxCell();
            this.入金区分コード = new GrapeCity.Win.MultiRow.ComboBoxCell();
            this.備考コード = new GrapeCity.Win.MultiRow.ComboBoxCell();
            this.備考 = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.入金金額 = new GrapeCity.Win.MultiRow.TextBoxCell();
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
            // 明細番号
            // 
            this.明細番号.DataField = "明細番号";
            this.明細番号.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.明細番号.Location = new System.Drawing.Point(20, 0);
            this.明細番号.Name = "明細番号";
            this.明細番号.ShowIndicator = false;
            this.明細番号.ShowRowError = false;
            this.明細番号.Size = new System.Drawing.Size(27, 17);
            cellStyle15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            border4.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle15.Border = border4;
            cellStyle15.Font = new System.Drawing.Font("BIZ UDPゴシック", 10F);
            this.明細番号.Style = cellStyle15;
            this.明細番号.TabIndex = 0;
            this.明細番号.TabStop = false;
            // 
            // 入金明細番号ボタン
            // 
            this.入金明細番号ボタン.Location = new System.Drawing.Point(20, -1);
            this.入金明細番号ボタン.Name = "入金明細番号ボタン";
            this.入金明細番号ボタン.Size = new System.Drawing.Size(27, 21);
            cellStyle22.BackColor = System.Drawing.SystemColors.Control;
            cellStyle22.Font = new System.Drawing.Font("BIZ UDPゴシック", 8F);
            cellStyle22.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle22.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle22.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.入金明細番号ボタン.Style = cellStyle22;
            this.入金明細番号ボタン.TabIndex = 0;
            this.入金明細番号ボタン.TabStop = false;
            this.入金明細番号ボタン.Value = "No";
            // 
            // 削除ボタン
            // 
            this.削除ボタン.Location = new System.Drawing.Point(0, 0);
            this.削除ボタン.Name = "削除ボタン";
            this.削除ボタン.Size = new System.Drawing.Size(20, 20);
            cellStyle23.BackColor = System.Drawing.SystemColors.Control;
            cellStyle23.Font = new System.Drawing.Font("BIZ UDPゴシック", 8F);
            cellStyle23.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle23.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle23.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.削除ボタン.Style = cellStyle23;
            this.削除ボタン.TabIndex = 1;
            this.削除ボタン.TabStop = false;
            // 
            // 領収書出力ボタン
            // 
            this.領収書出力ボタン.Location = new System.Drawing.Point(47, 0);
            this.領収書出力ボタン.Name = "領収書出力ボタン";
            this.領収書出力ボタン.Size = new System.Drawing.Size(82, 21);
            cellStyle24.BackColor = System.Drawing.SystemColors.Control;
            cellStyle24.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            cellStyle24.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle24.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle24.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.領収書出力ボタン.Style = cellStyle24;
            this.領収書出力ボタン.TabIndex = 2;
            this.領収書出力ボタン.TabStop = false;
            this.領収書出力ボタン.Value = "領収書出力";
            // 
            // 入金区分ボタン
            // 
            this.入金区分ボタン.Location = new System.Drawing.Point(129, 0);
            this.入金区分ボタン.Name = "入金区分ボタン";
            this.入金区分ボタン.Size = new System.Drawing.Size(102, 21);
            cellStyle25.BackColor = System.Drawing.SystemColors.Control;
            cellStyle25.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            cellStyle25.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle25.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle25.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.入金区分ボタン.Style = cellStyle25;
            this.入金区分ボタン.TabIndex = 3;
            this.入金区分ボタン.TabStop = false;
            this.入金区分ボタン.Value = "入金区分";
            // 
            // 備考コードボタン
            // 
            this.備考コードボタン.Location = new System.Drawing.Point(231, 0);
            this.備考コードボタン.Name = "備考コードボタン";
            this.備考コードボタン.Size = new System.Drawing.Size(68, 21);
            cellStyle26.BackColor = System.Drawing.SystemColors.Control;
            cellStyle26.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            cellStyle26.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle26.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle26.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.備考コードボタン.Style = cellStyle26;
            this.備考コードボタン.TabIndex = 4;
            this.備考コードボタン.TabStop = false;
            this.備考コードボタン.Value = "備考コード";
            // 
            // 備考ボタン
            // 
            this.備考ボタン.Location = new System.Drawing.Point(299, 0);
            this.備考ボタン.Name = "備考ボタン";
            this.備考ボタン.Size = new System.Drawing.Size(296, 21);
            cellStyle27.BackColor = System.Drawing.SystemColors.Control;
            cellStyle27.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            cellStyle27.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle27.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle27.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.備考ボタン.Style = cellStyle27;
            this.備考ボタン.TabIndex = 5;
            this.備考ボタン.TabStop = false;
            this.備考ボタン.Value = "備　　考";
            // 
            // 入金金額ボタン
            // 
            this.入金金額ボタン.Location = new System.Drawing.Point(595, -1);
            this.入金金額ボタン.Name = "入金金額ボタン";
            this.入金金額ボタン.Size = new System.Drawing.Size(112, 21);
            cellStyle28.BackColor = System.Drawing.SystemColors.Control;
            cellStyle28.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            cellStyle28.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle28.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle28.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.入金金額ボタン.Style = cellStyle28;
            this.入金金額ボタン.TabIndex = 6;
            this.入金金額ボタン.TabStop = false;
            this.入金金額ボタン.Value = "入金金額";
            // 
            // 明細削除ボタン
            // 
            this.明細削除ボタン.ButtonCommand = GrapeCity.Win.MultiRow.RowActionButtonCommands.Remove;
            this.明細削除ボタン.Location = new System.Drawing.Point(0, 0);
            this.明細削除ボタン.Name = "明細削除ボタン";
            this.明細削除ボタン.Size = new System.Drawing.Size(20, 17);
            cellStyle16.BackColor = System.Drawing.Color.White;
            cellStyle16.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            cellStyle16.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle16.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle16.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.明細削除ボタン.Style = cellStyle16;
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
            cellStyle17.Font = new System.Drawing.Font("BIZ UDPゴシック", 10F);
            cellStyle17.SelectionForeColor = System.Drawing.Color.Black;
            this.領収証出力コード.Style = cellStyle17;
            this.領収証出力コード.TabIndex = 1;
            // 
            // 入金区分コード
            // 
            this.入金区分コード.DataField = "入金区分コード";
            this.入金区分コード.Location = new System.Drawing.Point(129, 0);
            this.入金区分コード.Name = "入金区分コード";
            this.入金区分コード.Size = new System.Drawing.Size(102, 17);
            cellStyle18.Font = new System.Drawing.Font("BIZ UDPゴシック", 10F);
            cellStyle18.SelectionForeColor = System.Drawing.Color.Black;
            this.入金区分コード.Style = cellStyle18;
            this.入金区分コード.TabIndex = 2;
            // 
            // 備考コード
            // 
            this.備考コード.DataField = "備考コード";
            this.備考コード.Location = new System.Drawing.Point(231, 0);
            this.備考コード.Name = "備考コード";
            this.備考コード.Size = new System.Drawing.Size(68, 17);
            cellStyle19.Font = new System.Drawing.Font("BIZ UDPゴシック", 10F);
            cellStyle19.SelectionForeColor = System.Drawing.Color.Black;
            this.備考コード.Style = cellStyle19;
            this.備考コード.TabIndex = 3;
            // 
            // 備考
            // 
            this.備考.DataField = "備考";
            this.備考.Location = new System.Drawing.Point(299, 0);
            this.備考.Name = "備考";
            this.備考.Size = new System.Drawing.Size(296, 17);
            border5.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle20.Border = border5;
            cellStyle20.Font = new System.Drawing.Font("BIZ UDPゴシック", 10F);
            cellStyle20.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.備考.Style = cellStyle20;
            this.備考.TabIndex = 4;
            // 
            // 入金金額
            // 
            this.入金金額.DataField = "入金金額";
            this.入金金額.Location = new System.Drawing.Point(595, 0);
            this.入金金額.Name = "入金金額";
            this.入金金額.Size = new System.Drawing.Size(112, 17);
            border6.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle21.Border = border6;
            cellStyle21.Font = new System.Drawing.Font("BIZ UDPゴシック", 10F);
            cellStyle21.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.入金金額.Style = cellStyle21;
            this.入金金額.TabIndex = 5;
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
    }
}

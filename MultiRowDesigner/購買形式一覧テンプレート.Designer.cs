namespace MultiRowDesigner
{          
    [System.ComponentModel.ToolboxItem(true)]
    partial class 購買形式一覧テンプレート
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
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle6 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle7 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle8 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border1 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border2 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border3 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border4 = new GrapeCity.Win.MultiRow.Border();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.明細番号ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.型式コードボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.型式名ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.購買数量ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.明細番号 = new GrapeCity.Win.MultiRow.RowHeaderCell();
            this.型式コード = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.型式名 = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.購買数量 = new GrapeCity.Win.MultiRow.TextBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.明細番号);
            this.Row.Cells.Add(this.型式コード);
            this.Row.Cells.Add(this.型式名);
            this.Row.Cells.Add(this.購買数量);
            this.Row.Height = 17;
            this.Row.Width = 285;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.明細番号ボタン);
            this.columnHeaderSection1.Cells.Add(this.型式コードボタン);
            this.columnHeaderSection1.Cells.Add(this.型式名ボタン);
            this.columnHeaderSection1.Cells.Add(this.購買数量ボタン);
            this.columnHeaderSection1.Height = 21;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 285;
            // 
            // 明細番号ボタン
            // 
            this.明細番号ボタン.Location = new System.Drawing.Point(0, 0);
            this.明細番号ボタン.Name = "明細番号ボタン";
            this.明細番号ボタン.Size = new System.Drawing.Size(24, 21);
            cellStyle5.BackColor = System.Drawing.SystemColors.Control;
            cellStyle5.Font = new System.Drawing.Font("BIZ UDPゴシック", 8F);
            cellStyle5.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle5.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.明細番号ボタン.Style = cellStyle5;
            this.明細番号ボタン.TabIndex = 0;
            this.明細番号ボタン.Value = "No";
            // 
            // 型式コードボタン
            // 
            this.型式コードボタン.Location = new System.Drawing.Point(24, 0);
            this.型式コードボタン.Name = "型式コードボタン";
            this.型式コードボタン.Size = new System.Drawing.Size(68, 21);
            cellStyle6.BackColor = System.Drawing.SystemColors.Control;
            cellStyle6.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            cellStyle6.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle6.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.型式コードボタン.Style = cellStyle6;
            this.型式コードボタン.TabIndex = 1;
            this.型式コードボタン.Value = "型式コード";
            // 
            // 型式名ボタン
            // 
            this.型式名ボタン.Location = new System.Drawing.Point(92, 0);
            this.型式名ボタン.Name = "型式名ボタン";
            this.型式名ボタン.Size = new System.Drawing.Size(136, 21);
            cellStyle7.BackColor = System.Drawing.SystemColors.Control;
            cellStyle7.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            cellStyle7.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle7.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle7.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.型式名ボタン.Style = cellStyle7;
            this.型式名ボタン.TabIndex = 2;
            this.型式名ボタン.Value = "型式名";
            // 
            // 購買数量ボタン
            // 
            this.購買数量ボタン.Location = new System.Drawing.Point(228, 0);
            this.購買数量ボタン.Name = "購買数量ボタン";
            this.購買数量ボタン.Size = new System.Drawing.Size(57, 21);
            cellStyle8.BackColor = System.Drawing.SystemColors.Control;
            cellStyle8.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            cellStyle8.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle8.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle8.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.購買数量ボタン.Style = cellStyle8;
            this.購買数量ボタン.TabIndex = 3;
            this.購買数量ボタン.Value = "購買数量";
            // 
            // 明細番号
            // 
            this.明細番号.DataField = "明細番号";
            this.明細番号.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.明細番号.Location = new System.Drawing.Point(0, 0);
            this.明細番号.Name = "明細番号";
            this.明細番号.ShowIndicator = false;
            this.明細番号.ShowRowError = false;
            this.明細番号.Size = new System.Drawing.Size(24, 17);
            cellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle1.Border = border1;
            cellStyle1.Font = new System.Drawing.Font("BIZ UDPゴシック", 10F);
            cellStyle1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.明細番号.Style = cellStyle1;
            this.明細番号.TabIndex = 0;
            // 
            // 型式コード
            // 
            this.型式コード.DataField = "型式コード";
            this.型式コード.Location = new System.Drawing.Point(24, 0);
            this.型式コード.Name = "型式コード";
            this.型式コード.Size = new System.Drawing.Size(68, 17);
            cellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(153)))));
            border2.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle2.Border = border2;
            cellStyle2.Font = new System.Drawing.Font("BIZ UDPゴシック", 10F);
            cellStyle2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.型式コード.Style = cellStyle2;
            this.型式コード.TabIndex = 1;
            // 
            // 型式名
            // 
            this.型式名.DataField = "型式名";
            this.型式名.Location = new System.Drawing.Point(92, 0);
            this.型式名.Name = "型式名";
            this.型式名.Size = new System.Drawing.Size(136, 17);
            border3.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle3.Border = border3;
            cellStyle3.Font = new System.Drawing.Font("BIZ UDPゴシック", 10F);
            cellStyle3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.型式名.Style = cellStyle3;
            this.型式名.TabIndex = 2;
            // 
            // 購買数量
            // 
            this.購買数量.DataField = "購買数量";
            this.購買数量.Location = new System.Drawing.Point(228, 0);
            this.購買数量.Name = "購買数量";
            this.購買数量.Size = new System.Drawing.Size(57, 17);
            border4.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle4.Border = border4;
            cellStyle4.Font = new System.Drawing.Font("BIZ UDPゴシック", 10F);
            cellStyle4.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.購買数量.Style = cellStyle4;
            this.購買数量.TabIndex = 3;
            // 
            // 購買形式一覧テンプレート
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 38;
            this.Width = 285;

        }
        

        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private GrapeCity.Win.MultiRow.ButtonCell 明細番号ボタン;
        private GrapeCity.Win.MultiRow.ButtonCell 型式コードボタン;
        private GrapeCity.Win.MultiRow.ButtonCell 型式名ボタン;
        private GrapeCity.Win.MultiRow.ButtonCell 購買数量ボタン;
        private GrapeCity.Win.MultiRow.RowHeaderCell 明細番号;
        private GrapeCity.Win.MultiRow.TextBoxCell 型式コード;
        private GrapeCity.Win.MultiRow.TextBoxCell 型式名;
        private GrapeCity.Win.MultiRow.TextBoxCell 購買数量;
    }
}

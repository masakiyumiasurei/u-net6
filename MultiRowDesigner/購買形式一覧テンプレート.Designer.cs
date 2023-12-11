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
            GrapeCity.Win.MultiRow.CellStyle cellStyle13 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border5 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle14 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border6 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle15 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border7 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle16 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border8 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle9 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle10 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle11 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle12 = new GrapeCity.Win.MultiRow.CellStyle();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.明細番号 = new GrapeCity.Win.MultiRow.RowHeaderCell();
            this.型式コード = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.型式名 = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.購買数量 = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.明細番号ボタン = new GrapeCity.Win.MultiRow.HeaderCell();
            this.型式コードボタン = new GrapeCity.Win.MultiRow.HeaderCell();
            this.型式名ボタン = new GrapeCity.Win.MultiRow.HeaderCell();
            this.購買数量ボタン = new GrapeCity.Win.MultiRow.HeaderCell();
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
            // 明細番号
            // 
            this.明細番号.DataField = "明細番号";
            this.明細番号.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.明細番号.Location = new System.Drawing.Point(0, 0);
            this.明細番号.Name = "明細番号";
            this.明細番号.ShowIndicator = false;
            this.明細番号.ShowRowError = false;
            this.明細番号.Size = new System.Drawing.Size(24, 17);
            cellStyle13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            border5.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle13.Border = border5;
            cellStyle13.Font = new System.Drawing.Font("BIZ UDPゴシック", 10F);
            cellStyle13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.明細番号.Style = cellStyle13;
            this.明細番号.TabIndex = 0;
            // 
            // 型式コード
            // 
            this.型式コード.DataField = "型式コード";
            this.型式コード.Location = new System.Drawing.Point(24, 0);
            this.型式コード.Name = "型式コード";
            this.型式コード.Size = new System.Drawing.Size(68, 17);
            cellStyle14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(153)))));
            border6.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle14.Border = border6;
            cellStyle14.Font = new System.Drawing.Font("BIZ UDPゴシック", 10F);
            cellStyle14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.型式コード.Style = cellStyle14;
            this.型式コード.TabIndex = 1;
            // 
            // 型式名
            // 
            this.型式名.DataField = "型式名";
            this.型式名.Location = new System.Drawing.Point(92, 0);
            this.型式名.Name = "型式名";
            this.型式名.Size = new System.Drawing.Size(136, 17);
            border7.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle15.Border = border7;
            cellStyle15.Font = new System.Drawing.Font("BIZ UDPゴシック", 10F);
            cellStyle15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.型式名.Style = cellStyle15;
            this.型式名.TabIndex = 2;
            // 
            // 購買数量
            // 
            this.購買数量.DataField = "購買数量";
            this.購買数量.Location = new System.Drawing.Point(228, 0);
            this.購買数量.Name = "購買数量";
            this.購買数量.Size = new System.Drawing.Size(57, 17);
            border8.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle16.Border = border8;
            cellStyle16.Font = new System.Drawing.Font("BIZ UDPゴシック", 10F);
            cellStyle16.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.購買数量.Style = cellStyle16;
            this.購買数量.TabIndex = 3;
            // 
            // 明細番号ボタン
            // 
            this.明細番号ボタン.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.明細番号ボタン.Location = new System.Drawing.Point(0, 0);
            this.明細番号ボタン.Name = "明細番号ボタン";
            this.明細番号ボタン.Size = new System.Drawing.Size(24, 21);
            cellStyle9.BackColor = System.Drawing.Color.Transparent;
            cellStyle9.Font = new System.Drawing.Font("BIZ UDPゴシック", 7F);
            cellStyle9.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.明細番号ボタン.Style = cellStyle9;
            this.明細番号ボタン.TabIndex = 4;
            this.明細番号ボタン.TabStop = false;
            this.明細番号ボタン.Value = "No";
            // 
            // 型式コードボタン
            // 
            this.型式コードボタン.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.型式コードボタン.Location = new System.Drawing.Point(24, 0);
            this.型式コードボタン.Name = "型式コードボタン";
            this.型式コードボタン.Size = new System.Drawing.Size(68, 21);
            cellStyle10.BackColor = System.Drawing.Color.Transparent;
            cellStyle10.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            cellStyle10.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.型式コードボタン.Style = cellStyle10;
            this.型式コードボタン.TabIndex = 5;
            this.型式コードボタン.TabStop = false;
            this.型式コードボタン.Value = "型式コード";
            // 
            // 型式名ボタン
            // 
            this.型式名ボタン.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.型式名ボタン.Location = new System.Drawing.Point(92, 0);
            this.型式名ボタン.Name = "型式名ボタン";
            this.型式名ボタン.Size = new System.Drawing.Size(136, 21);
            cellStyle11.BackColor = System.Drawing.Color.Transparent;
            cellStyle11.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            cellStyle11.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.型式名ボタン.Style = cellStyle11;
            this.型式名ボタン.TabIndex = 6;
            this.型式名ボタン.TabStop = false;
            this.型式名ボタン.Value = "型式名";
            // 
            // 購買数量ボタン
            // 
            this.購買数量ボタン.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.購買数量ボタン.Location = new System.Drawing.Point(228, 0);
            this.購買数量ボタン.Name = "購買数量ボタン";
            this.購買数量ボタン.Size = new System.Drawing.Size(57, 21);
            cellStyle12.BackColor = System.Drawing.Color.Transparent;
            cellStyle12.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            cellStyle12.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.購買数量ボタン.Style = cellStyle12;
            this.購買数量ボタン.TabIndex = 7;
            this.購買数量ボタン.TabStop = false;
            this.購買数量ボタン.Value = "購買数量";
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
        private GrapeCity.Win.MultiRow.RowHeaderCell 明細番号;
        private GrapeCity.Win.MultiRow.TextBoxCell 型式コード;
        private GrapeCity.Win.MultiRow.TextBoxCell 型式名;
        private GrapeCity.Win.MultiRow.TextBoxCell 購買数量;
        private GrapeCity.Win.MultiRow.HeaderCell 明細番号ボタン;
        private GrapeCity.Win.MultiRow.HeaderCell 型式コードボタン;
        private GrapeCity.Win.MultiRow.HeaderCell 型式名ボタン;
        private GrapeCity.Win.MultiRow.HeaderCell 購買数量ボタン;
    }
}

namespace MultiRowDesigner
{          
    [System.ComponentModel.ToolboxItem(true)]
    partial class 部品購買設定明細テンプレート
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
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle6 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle7 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle8 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle9 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle10 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle11 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border1 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border2 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.明細番号ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.購買対象_ラベル = new GrapeCity.Win.MultiRow.HeaderCell();
            this.部品コード_ラベル = new GrapeCity.Win.MultiRow.HeaderCell();
            this.部品参照_ラベル = new GrapeCity.Win.MultiRow.HeaderCell();
            this.廃止_ラベル = new GrapeCity.Win.MultiRow.HeaderCell();
            this.分類記号_ラベル = new GrapeCity.Win.MultiRow.HeaderCell();
            this.型番_ラベル = new GrapeCity.Win.MultiRow.HeaderCell();
            this.メーカー名_ラベル = new GrapeCity.Win.MultiRow.HeaderCell();
            this.明細番号 = new GrapeCity.Win.MultiRow.RowHeaderCell();
            this.購買対象 = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.購買指定ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.明細番号);
            this.Row.Cells.Add(this.購買対象);
            this.Row.Cells.Add(this.購買指定ボタン);
            this.Row.Height = 72;
            this.Row.Width = 779;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.明細番号ボタン);
            this.columnHeaderSection1.Cells.Add(this.購買対象_ラベル);
            this.columnHeaderSection1.Cells.Add(this.部品コード_ラベル);
            this.columnHeaderSection1.Cells.Add(this.部品参照_ラベル);
            this.columnHeaderSection1.Cells.Add(this.廃止_ラベル);
            this.columnHeaderSection1.Cells.Add(this.分類記号_ラベル);
            this.columnHeaderSection1.Cells.Add(this.型番_ラベル);
            this.columnHeaderSection1.Cells.Add(this.メーカー名_ラベル);
            this.columnHeaderSection1.Height = 24;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 779;
            // 
            // 明細番号ボタン
            // 
            this.明細番号ボタン.Location = new System.Drawing.Point(0, 0);
            this.明細番号ボタン.Name = "明細番号ボタン";
            this.明細番号ボタン.Size = new System.Drawing.Size(24, 24);
            cellStyle4.BackColor = System.Drawing.SystemColors.Control;
            cellStyle4.Font = new System.Drawing.Font("BIZ UDPゴシック", 8F);
            cellStyle4.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle4.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.明細番号ボタン.Style = cellStyle4;
            this.明細番号ボタン.TabIndex = 0;
            this.明細番号ボタン.TabStop = false;
            this.明細番号ボタン.Value = "No";
            // 
            // 購買対象_ラベル
            // 
            this.購買対象_ラベル.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.購買対象_ラベル.Location = new System.Drawing.Point(24, 0);
            this.購買対象_ラベル.Name = "購買対象_ラベル";
            this.購買対象_ラベル.Size = new System.Drawing.Size(57, 24);
            cellStyle5.BackColor = System.Drawing.Color.Transparent;
            cellStyle5.Font = new System.Drawing.Font("BIZ UDPゴシック", 8F);
            cellStyle5.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.購買対象_ラベル.Style = cellStyle5;
            this.購買対象_ラベル.TabIndex = 3;
            this.購買対象_ラベル.TabStop = false;
            this.購買対象_ラベル.Value = "購指定";
            // 
            // 部品コード_ラベル
            // 
            this.部品コード_ラベル.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.部品コード_ラベル.Location = new System.Drawing.Point(81, 0);
            this.部品コード_ラベル.Name = "部品コード_ラベル";
            this.部品コード_ラベル.Size = new System.Drawing.Size(82, 24);
            cellStyle6.BackColor = System.Drawing.Color.Transparent;
            cellStyle6.Font = new System.Drawing.Font("BIZ UDPゴシック", 8F);
            cellStyle6.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.部品コード_ラベル.Style = cellStyle6;
            this.部品コード_ラベル.TabIndex = 4;
            this.部品コード_ラベル.TabStop = false;
            this.部品コード_ラベル.Value = "部品コード";
            // 
            // 部品参照_ラベル
            // 
            this.部品参照_ラベル.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.部品参照_ラベル.Location = new System.Drawing.Point(163, 0);
            this.部品参照_ラベル.Name = "部品参照_ラベル";
            this.部品参照_ラベル.Size = new System.Drawing.Size(24, 24);
            cellStyle7.BackColor = System.Drawing.Color.Transparent;
            cellStyle7.Font = new System.Drawing.Font("BIZ UDPゴシック", 8F);
            cellStyle7.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.部品参照_ラベル.Style = cellStyle7;
            this.部品参照_ラベル.TabIndex = 5;
            this.部品参照_ラベル.TabStop = false;
            this.部品参照_ラベル.Value = "参";
            // 
            // 廃止_ラベル
            // 
            this.廃止_ラベル.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.廃止_ラベル.Location = new System.Drawing.Point(187, 0);
            this.廃止_ラベル.Name = "廃止_ラベル";
            this.廃止_ラベル.Size = new System.Drawing.Size(24, 24);
            cellStyle8.BackColor = System.Drawing.Color.Transparent;
            cellStyle8.Font = new System.Drawing.Font("BIZ UDPゴシック", 8F);
            cellStyle8.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.廃止_ラベル.Style = cellStyle8;
            this.廃止_ラベル.TabIndex = 6;
            this.廃止_ラベル.TabStop = false;
            this.廃止_ラベル.Value = "廃";
            // 
            // 分類記号_ラベル
            // 
            this.分類記号_ラベル.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.分類記号_ラベル.Location = new System.Drawing.Point(211, 0);
            this.分類記号_ラベル.Name = "分類記号_ラベル";
            this.分類記号_ラベル.Size = new System.Drawing.Size(24, 24);
            cellStyle9.BackColor = System.Drawing.Color.Transparent;
            cellStyle9.Font = new System.Drawing.Font("BIZ UDPゴシック", 8F);
            cellStyle9.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.分類記号_ラベル.Style = cellStyle9;
            this.分類記号_ラベル.TabIndex = 7;
            this.分類記号_ラベル.TabStop = false;
            this.分類記号_ラベル.Value = "類";
            // 
            // 型番_ラベル
            // 
            this.型番_ラベル.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.型番_ラベル.Location = new System.Drawing.Point(235, 0);
            this.型番_ラベル.Name = "型番_ラベル";
            this.型番_ラベル.Size = new System.Drawing.Size(272, 24);
            cellStyle10.BackColor = System.Drawing.Color.Transparent;
            cellStyle10.Font = new System.Drawing.Font("BIZ UDPゴシック", 8F);
            cellStyle10.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.型番_ラベル.Style = cellStyle10;
            this.型番_ラベル.TabIndex = 8;
            this.型番_ラベル.TabStop = false;
            this.型番_ラベル.Value = "型　番";
            // 
            // メーカー名_ラベル
            // 
            this.メーカー名_ラベル.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.メーカー名_ラベル.Location = new System.Drawing.Point(507, 0);
            this.メーカー名_ラベル.Name = "メーカー名_ラベル";
            this.メーカー名_ラベル.Size = new System.Drawing.Size(272, 24);
            cellStyle11.BackColor = System.Drawing.Color.Transparent;
            cellStyle11.Font = new System.Drawing.Font("BIZ UDPゴシック", 8F);
            cellStyle11.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.メーカー名_ラベル.Style = cellStyle11;
            this.メーカー名_ラベル.TabIndex = 9;
            this.メーカー名_ラベル.TabStop = false;
            this.メーカー名_ラベル.Value = "メーカー名";
            // 
            // 明細番号
            // 
            this.明細番号.DataField = "明細番号";
            this.明細番号.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.明細番号.Location = new System.Drawing.Point(0, 0);
            this.明細番号.Name = "明細番号";
            this.明細番号.ShowIndicator = false;
            this.明細番号.ShowRowError = false;
            this.明細番号.Size = new System.Drawing.Size(24, 34);
            cellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle1.Border = border1;
            cellStyle1.Font = new System.Drawing.Font("BIZ UDPゴシック", 10F);
            this.明細番号.Style = cellStyle1;
            this.明細番号.TabIndex = 0;
            this.明細番号.TabStop = false;
            // 
            // 購買対象
            // 
            this.購買対象.DataField = "購買対象";
            this.購買対象.Location = new System.Drawing.Point(58, 0);
            this.購買対象.Name = "購買対象";
            this.購買対象.Size = new System.Drawing.Size(23, 34);
            border2.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle2.Border = border2;
            cellStyle2.Font = new System.Drawing.Font("BIZ UDPゴシック", 10F);
            cellStyle2.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.購買対象.Style = cellStyle2;
            this.購買対象.TabIndex = 2;
            this.購買対象.TabStop = false;
            // 
            // 購買指定ボタン
            // 
            this.購買指定ボタン.Location = new System.Drawing.Point(24, 0);
            this.購買指定ボタン.Name = "購買指定ボタン";
            this.購買指定ボタン.Size = new System.Drawing.Size(34, 34);
            cellStyle3.BackColor = System.Drawing.SystemColors.Control;
            cellStyle3.Font = new System.Drawing.Font("BIZ UDPゴシック", 12F);
            cellStyle3.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle3.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.購買指定ボタン.Style = cellStyle3;
            this.購買指定ボタン.TabIndex = 3;
            this.購買指定ボタン.TabStop = false;
            this.購買指定ボタン.Value = "⇒";
            // 
            // 部品購買設定明細テンプレート
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 96;
            this.Width = 779;

        }
        

        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private GrapeCity.Win.MultiRow.ButtonCell 明細番号ボタン;
        private GrapeCity.Win.MultiRow.HeaderCell 購買対象_ラベル;
        private GrapeCity.Win.MultiRow.HeaderCell 部品コード_ラベル;
        private GrapeCity.Win.MultiRow.HeaderCell 部品参照_ラベル;
        private GrapeCity.Win.MultiRow.HeaderCell 廃止_ラベル;
        private GrapeCity.Win.MultiRow.HeaderCell 分類記号_ラベル;
        private GrapeCity.Win.MultiRow.HeaderCell 型番_ラベル;
        private GrapeCity.Win.MultiRow.HeaderCell メーカー名_ラベル;
        private GrapeCity.Win.MultiRow.RowHeaderCell 明細番号;
        private GrapeCity.Win.MultiRow.TextBoxCell 購買対象;
        private GrapeCity.Win.MultiRow.ButtonCell 購買指定ボタン;
    }
}

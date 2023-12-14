using GrapeCity.Win.MultiRow;

namespace MultiRowDesigner
{          
    [System.ComponentModel.ToolboxItem(true)]
    partial class 商品明細テンプレート
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
            GrapeCity.Win.MultiRow.CellStyle cellStyle14 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle15 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle16 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle17 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle18 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle19 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle20 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle21 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle22 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle23 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border1 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle6 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border2 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle7 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border3 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle8 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border4 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle9 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border5 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle10 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border6 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle11 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border7 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle12 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border8 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle13 = new GrapeCity.Win.MultiRow.CellStyle();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            //this.削除ボタン = new GrapeCity.Win.MultiRow.HeaderCell();
            //this.明細行選択ボタン = new GrapeCity.Win.MultiRow.HeaderCell();
            //this.headerCell1 = new GrapeCity.Win.MultiRow.HeaderCell();
            //this.headerCell2 = new GrapeCity.Win.MultiRow.HeaderCell();
            this.明細番号ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.型式名ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.定価ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.原価ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.機能ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.型式番号ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.明細削除ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.行挿入ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.行移動上ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.行移動下ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.明細番号 = new GrapeCity.Win.MultiRow.RowHeaderCell();
            this.型式名 = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.定価 = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.原価 = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.機能 = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.型式番号 = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.構成番号 = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.商品コード = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.選択 = new GrapeCity.Win.MultiRow.ButtonCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.明細削除ボタン);
            this.Row.Cells.Add(this.行挿入ボタン);
            this.Row.Cells.Add(this.行移動上ボタン);
            this.Row.Cells.Add(this.行移動下ボタン);
            this.Row.Cells.Add(this.明細番号);
            this.Row.Cells.Add(this.型式名);
            this.Row.Cells.Add(this.定価);
            this.Row.Cells.Add(this.原価);
            this.Row.Cells.Add(this.機能);
            this.Row.Cells.Add(this.型式番号);
            this.Row.Cells.Add(this.構成番号);
            this.Row.Cells.Add(this.商品コード);
            this.Row.Cells.Add(this.選択);
            this.Row.Height = 17;
            this.Row.Width = 782;
            // 
            // columnHeaderSection1
            // 
            //this.columnHeaderSection1.Cells.Add(this.削除ボタン);
            this.columnHeaderSection1.Cells.Add(this.明細行選択ボタン);
            this.columnHeaderSection1.Cells.Add(this.明細番号ボタン);
            this.columnHeaderSection1.Cells.Add(this.buttonCell1);
            this.columnHeaderSection1.Cells.Add(this.buttonCell2);
            this.columnHeaderSection1.Cells.Add(this.型式名ボタン);
            this.columnHeaderSection1.Cells.Add(this.定価ボタン);
            this.columnHeaderSection1.Cells.Add(this.原価ボタン);
            this.columnHeaderSection1.Cells.Add(this.buttonCell3);
            this.columnHeaderSection1.Cells.Add(this.型式番号ボタン);
            this.columnHeaderSection1.Height = 21;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 782;
            // 
            // 削除ボタン
            // 
            this.削除ボタン.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.削除ボタン.Location = new System.Drawing.Point(0, 0);
            this.削除ボタン.Name = "削除ボタン";
            this.削除ボタン.Size = new System.Drawing.Size(21, 21);
            cellStyle14.BackColor = System.Drawing.Color.Transparent;
            cellStyle14.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            cellStyle14.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.削除ボタン.Style = cellStyle14;
            this.削除ボタン.TabIndex = 10;
            this.削除ボタン.TabStop = false;
            // 
            // 明細行選択ボタン
            // 
            this.明細行選択ボタン.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.明細行選択ボタン.Location = new System.Drawing.Point(21, 0);
            this.明細行選択ボタン.Name = "明細行選択ボタン";
            this.明細行選択ボタン.Size = new System.Drawing.Size(17, 21);
            cellStyle15.BackColor = System.Drawing.Color.Transparent;
            cellStyle15.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            cellStyle15.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.明細行選択ボタン.Style = cellStyle15;
            this.明細行選択ボタン.TabIndex = 11;
            this.明細行選択ボタン.TabStop = false;
            // 
            // headerCell1
            // 
            //this.headerCell1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            //this.headerCell1.Location = new System.Drawing.Point(38, 0);
            //this.headerCell1.Name = "headerCell1";
            //this.headerCell1.Size = new System.Drawing.Size(17, 21);
            //cellStyle16.BackColor = System.Drawing.Color.Transparent;
            //cellStyle16.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            //cellStyle16.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            //this.headerCell1.Style = cellStyle16;
            //this.headerCell1.TabIndex = 12;
            //this.headerCell1.TabStop = false;
            //// 
            //// headerCell2
            //// 
            //this.headerCell2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            //this.headerCell2.Location = new System.Drawing.Point(55, 0);
            //this.headerCell2.Name = "headerCell2";
            //this.headerCell2.Size = new System.Drawing.Size(17, 21);
            //cellStyle17.BackColor = System.Drawing.Color.Transparent;
            //cellStyle17.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            //cellStyle17.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            //this.headerCell2.Style = cellStyle17;
            //this.headerCell2.TabIndex = 13;
            //this.headerCell2.TabStop = false;
            // 
            // 明細番号ボタン
            // 
            this.明細番号ボタン.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.明細番号ボタン.Location = new System.Drawing.Point(72, 0);
            this.明細番号ボタン.Name = "明細番号ボタン";
            this.明細番号ボタン.Size = new System.Drawing.Size(24, 21);
            cellStyle18.BackColor = System.Drawing.Color.Transparent;
            cellStyle18.Font = new System.Drawing.Font("BIZ UDPゴシック", 7F);
            cellStyle18.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.明細番号ボタン.Style = cellStyle18;
            this.明細番号ボタン.TabIndex = 14;
            this.明細番号ボタン.TabStop = false;
            this.明細番号ボタン.Value = "No";
            // 
            // 型式名ボタン
            // 
            this.型式名ボタン.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.型式名ボタン.Location = new System.Drawing.Point(96, 0);
            this.型式名ボタン.Name = "型式名ボタン";
            this.型式名ボタン.Size = new System.Drawing.Size(136, 21);
            cellStyle19.BackColor = System.Drawing.Color.Transparent;
            cellStyle19.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            cellStyle19.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.型式名ボタン.Style = cellStyle19;
            this.型式名ボタン.TabIndex = 15;
            this.型式名ボタン.TabStop = false;
            this.型式名ボタン.Value = "型式名";
            // 
            // 定価ボタン
            // 
            this.定価ボタン.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.定価ボタン.Location = new System.Drawing.Point(232, 0);
            this.定価ボタン.Name = "定価ボタン";
            this.定価ボタン.Size = new System.Drawing.Size(82, 21);
            cellStyle20.BackColor = System.Drawing.Color.Transparent;
            cellStyle20.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            cellStyle20.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.定価ボタン.Style = cellStyle20;
            this.定価ボタン.TabIndex = 16;
            this.定価ボタン.TabStop = false;
            this.定価ボタン.Value = "定価";
            // 
            // 原価ボタン
            // 
            this.原価ボタン.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.原価ボタン.Location = new System.Drawing.Point(314, 0);
            this.原価ボタン.Name = "原価ボタン";
            this.原価ボタン.Size = new System.Drawing.Size(82, 21);
            cellStyle21.BackColor = System.Drawing.Color.Transparent;
            cellStyle21.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            cellStyle21.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.原価ボタン.Style = cellStyle21;
            this.原価ボタン.TabIndex = 17;
            this.原価ボタン.TabStop = false;
            this.原価ボタン.Value = "定価";
            // 
            // 機能ボタン
            // 
            this.機能ボタン.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.機能ボタン.Location = new System.Drawing.Point(396, 0);
            this.機能ボタン.Name = "機能ボタン";
            this.機能ボタン.Size = new System.Drawing.Size(326, 21);
            cellStyle22.BackColor = System.Drawing.Color.Transparent;
            cellStyle22.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            cellStyle22.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.機能ボタン.Style = cellStyle22;
            this.機能ボタン.TabIndex = 18;
            this.機能ボタン.TabStop = false;
            this.機能ボタン.Value = "機　能";
            // 
            // 型式番号ボタンd
            // 
            this.型式番号ボタン.Enabled = false;
            this.型式番号ボタン.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.型式番号ボタン.Location = new System.Drawing.Point(732, 21);
            this.型式番号ボタン.Name = "型式番号ボタンd";
            this.型式番号ボタン.Size = new System.Drawing.Size(28, 21);
            cellStyle23.BackColor = System.Drawing.Color.Transparent;
            cellStyle23.Font = new System.Drawing.Font("BIZ UDPゴシック", 7F);
            cellStyle23.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.型式番号ボタン.Style = cellStyle23;
            this.型式番号ボタン.TabIndex = 19;
            this.型式番号ボタン.TabStop = false;
            this.型式番号ボタン.Value = "MN";
            // 
            // 削除ボタン
            // 
            this.削除ボタン.Location = new System.Drawing.Point(0, 0);
            this.削除ボタン.Name = "削除ボタン";
            this.削除ボタン.Size = new System.Drawing.Size(21, 21);
            cellStyle14.BackColor = System.Drawing.SystemColors.Control;
            cellStyle14.Font = new System.Drawing.Font("BIZ UDPゴシック", 8F);
            cellStyle14.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle14.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle14.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.削除ボタン.Style = cellStyle14;
            this.削除ボタン.TabIndex = 0;
            this.削除ボタン.TabStop = false;
            // 
            // 明細行選択ボタン
            // 
            this.明細行選択ボタン.Location = new System.Drawing.Point(21, 0);
            this.明細行選択ボタン.Name = "明細行選択ボタン";
            this.明細行選択ボタン.Size = new System.Drawing.Size(17, 21);
            cellStyle15.BackColor = System.Drawing.SystemColors.Control;
            cellStyle15.Font = new System.Drawing.Font("BIZ UDPゴシック", 8F);
            cellStyle15.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle15.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle15.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.明細行選択ボタン.Style = cellStyle15;
            this.明細行選択ボタン.TabIndex = 1;
            this.明細行選択ボタン.TabStop = false;
            // 
            // 明細番号ボタン
            // 
            this.明細番号ボタン.Location = new System.Drawing.Point(72, 0);
            this.明細番号ボタン.Name = "明細番号ボタン";
            this.明細番号ボタン.Size = new System.Drawing.Size(24, 21);
            cellStyle16.BackColor = System.Drawing.SystemColors.Control;
            cellStyle16.Font = new System.Drawing.Font("BIZ UDPゴシック", 8F);
            cellStyle16.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle16.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle16.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.明細番号ボタン.Style = cellStyle16;
            this.明細番号ボタン.TabIndex = 2;
            this.明細番号ボタン.TabStop = false;
            this.明細番号ボタン.Value = "No";
            // 
            // buttonCell1
            // 
            this.buttonCell1.Location = new System.Drawing.Point(38, 0);
            this.buttonCell1.Name = "buttonCell1";
            this.buttonCell1.Size = new System.Drawing.Size(17, 21);
            cellStyle17.BackColor = System.Drawing.SystemColors.Control;
            cellStyle17.Font = new System.Drawing.Font("BIZ UDPゴシック", 8F);
            cellStyle17.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle17.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle17.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.buttonCell1.Style = cellStyle17;
            this.buttonCell1.TabIndex = 3;
            this.buttonCell1.TabStop = false;
            // 
            // buttonCell2
            // 
            this.buttonCell2.Location = new System.Drawing.Point(55, 0);
            this.buttonCell2.Name = "buttonCell2";
            this.buttonCell2.Size = new System.Drawing.Size(17, 21);
            cellStyle18.BackColor = System.Drawing.SystemColors.Control;
            cellStyle18.Font = new System.Drawing.Font("BIZ UDPゴシック", 8F);
            cellStyle18.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle18.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle18.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.buttonCell2.Style = cellStyle18;
            this.buttonCell2.TabIndex = 4;
            this.buttonCell2.TabStop = false;
            // 
            // 型式名ボタン
            // 
            this.型式名ボタン.Location = new System.Drawing.Point(96, 0);
            this.型式名ボタン.Name = "型式名ボタン";
            this.型式名ボタン.Size = new System.Drawing.Size(136, 21);
            cellStyle19.BackColor = System.Drawing.SystemColors.Control;
            cellStyle19.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            cellStyle19.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle19.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle19.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.型式名ボタン.Style = cellStyle19;
            this.型式名ボタン.TabIndex = 5;
            this.型式名ボタン.TabStop = false;
            this.型式名ボタン.Value = "型式名";
            // 
            // 定価ボタン
            // 
            this.定価ボタン.Location = new System.Drawing.Point(232, 0);
            this.定価ボタン.Name = "定価ボタン";
            this.定価ボタン.Size = new System.Drawing.Size(82, 21);
            cellStyle20.BackColor = System.Drawing.SystemColors.Control;
            cellStyle20.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            cellStyle20.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle20.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle20.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.定価ボタン.Style = cellStyle20;
            this.定価ボタン.TabIndex = 6;
            this.定価ボタン.TabStop = false;
            this.定価ボタン.Value = "定価";
            // 
            // 原価ボタン
            // 
            this.原価ボタン.Location = new System.Drawing.Point(314, 0);
            this.原価ボタン.Name = "原価ボタン";
            this.原価ボタン.Size = new System.Drawing.Size(82, 21);
            cellStyle21.BackColor = System.Drawing.SystemColors.Control;
            cellStyle21.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            cellStyle21.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle21.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle21.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.原価ボタン.Style = cellStyle21;
            this.原価ボタン.TabIndex = 7;
            this.原価ボタン.TabStop = false;
            this.原価ボタン.Value = "原価";
            // 
            // buttonCell3
            // 
            this.buttonCell3.Location = new System.Drawing.Point(396, 0);
            this.buttonCell3.Name = "buttonCell3";
            this.buttonCell3.Size = new System.Drawing.Size(326, 21);
            cellStyle22.BackColor = System.Drawing.SystemColors.Control;
            cellStyle22.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            cellStyle22.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle22.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle22.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.buttonCell3.Style = cellStyle22;
            this.buttonCell3.TabIndex = 8;
            this.buttonCell3.TabStop = false;
            this.buttonCell3.Value = "機　能";
            // 
            // 型式番号ボタン
            // 
            this.型式番号ボタン.Enabled = false;
            this.型式番号ボタン.Location = new System.Drawing.Point(732, 0);
            this.型式番号ボタン.Name = "型式番号ボタン";
            this.型式番号ボタン.Size = new System.Drawing.Size(28, 21);
            cellStyle23.BackColor = System.Drawing.SystemColors.Control;
            cellStyle23.Font = new System.Drawing.Font("BIZ UDPゴシック", 8F);
            cellStyle23.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle23.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle23.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.型式番号ボタン.Style = cellStyle23;
            this.型式番号ボタン.TabIndex = 9;
            this.型式番号ボタン.TabStop = false;
            this.型式番号ボタン.Value = "MN";
            // 
            // 明細削除ボタン
            // 
            this.明細削除ボタン.ButtonCommand = GrapeCity.Win.MultiRow.RowActionButtonCommands.Remove;
            this.明細削除ボタン.Location = new System.Drawing.Point(0, 0);
            this.明細削除ボタン.Name = "明細削除ボタン";
            this.明細削除ボタン.Size = new System.Drawing.Size(21, 17);
            cellStyle1.BackColor = System.Drawing.Color.White;
            cellStyle1.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle1.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.明細削除ボタン.Style = cellStyle1;
            this.明細削除ボタン.TabIndex = 0;
            this.明細削除ボタン.TabStop = false;
            this.明細削除ボタン.Value = "×";
            // 
            // 行挿入ボタン
            // 
            this.行挿入ボタン.ButtonCommand = GrapeCity.Win.MultiRow.RowActionButtonCommands.Insert;
            this.行挿入ボタン.Location = new System.Drawing.Point(21, 0);
            this.行挿入ボタン.Name = "行挿入ボタン";
            this.行挿入ボタン.Size = new System.Drawing.Size(17, 17);
            cellStyle2.BackColor = System.Drawing.SystemColors.Control;
            cellStyle2.Font = new System.Drawing.Font("BIZ UDPゴシック", 12F);
            cellStyle2.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle2.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.行挿入ボタン.Style = cellStyle2;
            this.行挿入ボタン.TabIndex = 1;
            this.行挿入ボタン.TabStop = false;
            this.行挿入ボタン.Value = "▶";
            // 
            // 行移動上ボタン
            // 
            this.行移動上ボタン.ButtonCommand = GrapeCity.Win.MultiRow.NavigationButtonCommands.MoveToPreviousRow;
            this.行移動上ボタン.Location = new System.Drawing.Point(38, 0);
            this.行移動上ボタン.Name = "行移動上ボタン";
            this.行移動上ボタン.Size = new System.Drawing.Size(17, 17);
            cellStyle3.BackColor = System.Drawing.SystemColors.Control;
            cellStyle3.Font = new System.Drawing.Font("BIZ UDPゴシック", 7.5F);
            cellStyle3.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle3.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.行移動上ボタン.Style = cellStyle3;
            this.行移動上ボタン.TabIndex = 2;
            this.行移動上ボタン.TabStop = false;
            this.行移動上ボタン.Value = "↑";
            // 
            // 行移動下ボタン
            // 
            this.行移動下ボタン.ButtonCommand = GrapeCity.Win.MultiRow.NavigationButtonCommands.MoveToNextRow;
            this.行移動下ボタン.Location = new System.Drawing.Point(55, 0);
            this.行移動下ボタン.Name = "行移動下ボタン";
            this.行移動下ボタン.Size = new System.Drawing.Size(17, 17);
            cellStyle4.BackColor = System.Drawing.SystemColors.Control;
            cellStyle4.Font = new System.Drawing.Font("BIZ UDPゴシック", 7.5F);
            cellStyle4.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle4.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.行移動下ボタン.Style = cellStyle4;
            this.行移動下ボタン.TabIndex = 3;
            this.行移動下ボタン.TabStop = false;
            this.行移動下ボタン.Value = "↓";
            // 
            // 明細番号
            // 
            this.明細番号.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.明細番号.Location = new System.Drawing.Point(72, 0);
            this.明細番号.Name = "明細番号";
            this.明細番号.ShowIndicator = false;
            this.明細番号.ShowRowError = false;
            this.明細番号.Size = new System.Drawing.Size(24, 17);
            cellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle5.Border = border1;
            cellStyle5.Font = new System.Drawing.Font("BIZ UDPゴシック", 10F);
            this.明細番号.Style = cellStyle5;
            this.明細番号.TabIndex = 4;
            this.明細番号.TabStop = false;
            // 
            // 型式名
            // 
            this.型式名.DataField = "型式名";
            this.型式名.Location = new System.Drawing.Point(96, 0);
            this.型式名.Name = "型式名";
            this.型式名.Size = new System.Drawing.Size(136, 17);
            border2.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle6.Border = border2;
            cellStyle6.Font = new System.Drawing.Font("BIZ UDPゴシック", 10F);
            cellStyle6.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.型式名.Style = cellStyle6;
            this.型式名.TabIndex = 1;
            // 
            // 定価
            // 
            this.定価.DataField = "定価";
            this.定価.Location = new System.Drawing.Point(232, 0);
            this.定価.Name = "定価";
            this.定価.Size = new System.Drawing.Size(82, 17);
            border3.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle7.Border = border3;
            cellStyle7.Font = new System.Drawing.Font("BIZ UDPゴシック", 10F);
            cellStyle7.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.定価.Style = cellStyle7;
            this.定価.TabIndex = 2;
            // 
            // 原価
            // 
            this.原価.DataField = "原価";
            this.原価.Location = new System.Drawing.Point(314, 0);
            this.原価.Name = "原価";
            this.原価.Size = new System.Drawing.Size(82, 17);
            border4.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle8.Border = border4;
            cellStyle8.Font = new System.Drawing.Font("BIZ UDPゴシック", 10F);
            cellStyle8.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.原価.Style = cellStyle8;
            this.原価.TabIndex = 3;
            // 
            // 機能
            // 
            this.機能.DataField = "機能";
            this.機能.Location = new System.Drawing.Point(396, 0);
            this.機能.Name = "機能";
            this.機能.Size = new System.Drawing.Size(326, 17);
            border5.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle9.Border = border5;
            cellStyle9.Font = new System.Drawing.Font("BIZ UDPゴシック", 10F);
            cellStyle9.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.機能.Style = cellStyle9;
            this.機能.TabIndex = 4;
            // 
            // 型式番号
            // 
            this.型式番号.DataField = "型式番号";
            this.型式番号.Enabled = false;
            this.型式番号.Location = new System.Drawing.Point(722, 0);
            this.型式番号.Name = "型式番号";
            this.型式番号.ReadOnly = true;
            this.型式番号.Size = new System.Drawing.Size(10, 17);
            border6.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle10.Border = border6;
            cellStyle10.DisabledBackColor = System.Drawing.SystemColors.Window;
            cellStyle10.Font = new System.Drawing.Font("BIZ UDPゴシック", 10F);
            cellStyle10.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.型式番号.Style = cellStyle10;
            this.型式番号.TabIndex = 5;
            this.型式番号.TabStop = false;
            this.型式番号.Visible = false;
            // 
            // 構成番号
            // 
            this.構成番号.DataField = "構成番号";
            this.構成番号.Enabled = false;
            this.構成番号.Location = new System.Drawing.Point(732, 0);
            this.構成番号.Name = "構成番号";
            this.構成番号.ReadOnly = true;
            this.構成番号.Size = new System.Drawing.Size(10, 17);
            border7.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle11.Border = border7;
            cellStyle11.DisabledBackColor = System.Drawing.SystemColors.Window;
            cellStyle11.Font = new System.Drawing.Font("BIZ UDPゴシック", 10F);
            cellStyle11.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.構成番号.Style = cellStyle11;
            this.構成番号.TabIndex = 6;
            this.構成番号.TabStop = false;
            this.構成番号.Visible = false;
            // 
            // 商品コード
            // 
            this.商品コード.DataField = "商品コード";
            this.商品コード.Enabled = false;
            this.商品コード.Location = new System.Drawing.Point(742, 0);
            this.商品コード.Name = "商品コード";
            this.商品コード.ReadOnly = true;
            this.商品コード.Size = new System.Drawing.Size(10, 17);
            border8.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle12.Border = border8;
            cellStyle12.DisabledBackColor = System.Drawing.SystemColors.Window;
            cellStyle12.Font = new System.Drawing.Font("BIZ UDPゴシック", 10F);
            cellStyle12.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.商品コード.Style = cellStyle12;
            this.商品コード.TabIndex = 7;
            this.商品コード.TabStop = false;
            this.商品コード.Visible = false;
            // 
            // 選択
            // 
            this.選択.Location = new System.Drawing.Point(752, 0);
            this.選択.Name = "選択";
            this.選択.Size = new System.Drawing.Size(17, 17);
            cellStyle13.BackColor = System.Drawing.SystemColors.Control;
            cellStyle13.Font = new System.Drawing.Font("BIZ UDPゴシック", 12F);
            cellStyle13.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle13.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle13.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.選択.Style = cellStyle13;
            this.選択.TabIndex = 8;
            this.選択.TabStop = false;
            this.選択.Value = "▶";
            // 
            // 商品明細テンプレート
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 38;
            this.Width = 782;

        }
        

        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private GrapeCity.Win.MultiRow.ButtonCell 削除ボタン;
        private GrapeCity.Win.MultiRow.ButtonCell 明細行選択ボタン;
        private GrapeCity.Win.MultiRow.ButtonCell 明細番号ボタン;
        private GrapeCity.Win.MultiRow.ButtonCell buttonCell1;
        private GrapeCity.Win.MultiRow.ButtonCell buttonCell2;
        private GrapeCity.Win.MultiRow.ButtonCell 型式名ボタン;
        private GrapeCity.Win.MultiRow.ButtonCell 定価ボタン;
        private GrapeCity.Win.MultiRow.ButtonCell 原価ボタン;
        private GrapeCity.Win.MultiRow.ButtonCell 機能ボタン;
        
        private GrapeCity.Win.MultiRow.ButtonCell buttonCell3;
        private GrapeCity.Win.MultiRow.ButtonCell 型式番号ボタン;
        private GrapeCity.Win.MultiRow.ButtonCell 明細削除ボタン;
        private GrapeCity.Win.MultiRow.ButtonCell 行挿入ボタン;
        private GrapeCity.Win.MultiRow.ButtonCell 行移動上ボタン;
        private GrapeCity.Win.MultiRow.ButtonCell 行移動下ボタン;
        private GrapeCity.Win.MultiRow.RowHeaderCell 明細番号;
        private GrapeCity.Win.MultiRow.TextBoxCell 型式名;
        private GrapeCity.Win.MultiRow.TextBoxCell 定価;
        private GrapeCity.Win.MultiRow.TextBoxCell 原価;
        private GrapeCity.Win.MultiRow.TextBoxCell 機能;
        private GrapeCity.Win.MultiRow.TextBoxCell 型式番号;
        private GrapeCity.Win.MultiRow.TextBoxCell 構成番号;
        private GrapeCity.Win.MultiRow.TextBoxCell 商品コード;
        private GrapeCity.Win.MultiRow.ButtonCell 選択;
    }
}

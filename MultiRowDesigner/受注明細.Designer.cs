namespace MultiRowDesigner
{
    partial class 受注明細
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            gcMultiRow1 = new GrapeCity.Win.MultiRow.GcMultiRow();
            受注明細テンプレート1 = new 受注明細テンプレート();
            ((System.ComponentModel.ISupportInitialize)gcMultiRow1).BeginInit();
            SuspendLayout();
            // 
            // gcMultiRow1
            // 
            gcMultiRow1.BackColor = Color.White;
            gcMultiRow1.Dock = DockStyle.Fill;
            gcMultiRow1.Location = new Point(0, 0);
            gcMultiRow1.Name = "gcMultiRow1";
            gcMultiRow1.Size = new Size(1100, 338);
            gcMultiRow1.TabIndex = 0;
            gcMultiRow1.Template = 受注明細テンプレート1;
            gcMultiRow1.Text = "gcMultiRow1";
            gcMultiRow1.CellValidating += gcMultiRow1_CellValidating;
            gcMultiRow1.CellFormatting += gcMultiRow1_CellFormatting;
            gcMultiRow1.CellEnter += gcMultiRow1_CellEnter;
            gcMultiRow1.RowValidating += gcMultiRow1_RowValidating;
            gcMultiRow1.DefaultValuesNeeded += gcMultiRow1_DefaultValuesNeeded;
            gcMultiRow1.EditingControlShowing += gcMultiRow1_EditingControlShowing;
            gcMultiRow1.CellDoubleClick += gcMultiRow1_CellDoubleClick;
            gcMultiRow1.CellContentButtonClick += gcMultiRow1_CellContentButtonClick;
            // 
            // 受注明細テンプレート1
            // 
            受注明細テンプレート1.Height = 189;
            // 
            // 
            // 
            受注明細テンプレート1.Row.Height = 84;
            受注明細テンプレート1.Row.Width = 1060;
            受注明細テンプレート1.Width = 1060;
            // 
            // 受注明細
            // 
            AutoScaleMode = AutoScaleMode.None;
            Controls.Add(gcMultiRow1);
            Name = "受注明細";
            Size = new Size(1100, 338);
            Load += 受注明細_Load;
            ((System.ComponentModel.ISupportInitialize)gcMultiRow1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GrapeCity.Win.MultiRow.GcMultiRow gcMultiRow1;
        private 受注明細テンプレート 受注明細テンプレート1;
    }
}

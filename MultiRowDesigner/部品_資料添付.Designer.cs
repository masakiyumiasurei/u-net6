namespace MultiRowDesigner
{
    partial class 部品_資料添付
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
            部品_資料添付テンプレート1 = new 部品_資料添付テンプレート();
            ((System.ComponentModel.ISupportInitialize)gcMultiRow1).BeginInit();
            SuspendLayout();
            // 
            // gcMultiRow1
            // 
            gcMultiRow1.EditMode = GrapeCity.Win.MultiRow.EditMode.EditOnEnter;
            gcMultiRow1.Location = new Point(-9, 0);
            gcMultiRow1.Margin = new Padding(4);
            gcMultiRow1.Name = "gcMultiRow1";
            gcMultiRow1.ScrollBars = ScrollBars.Vertical;
            gcMultiRow1.Size = new Size(691, 270);
            gcMultiRow1.TabIndex = 0;
            gcMultiRow1.Template = 部品_資料添付テンプレート1;
            gcMultiRow1.TemplateScaleSize = new SizeF(1.16666675F, 1.25F);
            gcMultiRow1.Text = "gcMultiRow1";
            gcMultiRow1.RowsRemoved += gcMultiRow1_RowsRemoved;
            gcMultiRow1.DefaultValuesNeeded += gcMultiRow1_DefaultValuesNeeded;
            gcMultiRow1.CellContentButtonClick += gcMultiRow1_CellContentButtonClick;
            // 
            // 部品_資料添付テンプレート1
            // 
            部品_資料添付テンプレート1.Height = 218;
            // 
            // 
            // 
            部品_資料添付テンプレート1.Row.Height = 218;
            部品_資料添付テンプレート1.Row.Width = 577;
            部品_資料添付テンプレート1.Width = 577;
            // 
            // 部品_資料添付
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(gcMultiRow1);
            Margin = new Padding(4);
            Name = "部品_資料添付";
            Size = new Size(683, 249);
            Load += 部品_資料添付_Load;
            ((System.ComponentModel.ISupportInitialize)gcMultiRow1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GrapeCity.Win.MultiRow.GcMultiRow gcMultiRow1;
        private 部品_資料添付テンプレート 部品_資料添付テンプレート1;
    }
}

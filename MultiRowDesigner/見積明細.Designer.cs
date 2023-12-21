namespace MultiRowDesigner
{
    partial class 見積明細
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
            見積明細テンプレート1 = new 見積明細テンプレート();
            ((System.ComponentModel.ISupportInitialize)gcMultiRow1).BeginInit();
            SuspendLayout();
            // 
            // gcMultiRow1
            // 
            gcMultiRow1.AllowUserToReverseSelect = true;
            gcMultiRow1.AllowUserToZoom = false;
            gcMultiRow1.BackColor = Color.White;
            gcMultiRow1.EditMode = GrapeCity.Win.MultiRow.EditMode.EditOnEnter;
            gcMultiRow1.HorizontalScrollBarMode = GrapeCity.Win.MultiRow.ScrollBarMode.Automatic;
            gcMultiRow1.Location = new Point(0, 0);
            gcMultiRow1.Name = "gcMultiRow1";
            gcMultiRow1.Size = new Size(1000, 350);
            gcMultiRow1.TabIndex = 0;
            gcMultiRow1.Template = 見積明細テンプレート1;
            gcMultiRow1.Text = "gcMultiRow1";
            gcMultiRow1.CellValidating += gcMultiRow1_CellValidating;
            gcMultiRow1.CellEnter += gcMultiRow1_CellEnter;
            gcMultiRow1.DefaultValuesNeeded += gcMultiRow1_DefaultValuesNeeded;
            gcMultiRow1.EditingControlShowing += gcMultiRow1_EditingControlShowing;
            gcMultiRow1.CellContentButtonClick += gcMultiRow1_CellContentButtonClick;
            gcMultiRow1.Sorted += gcMultiRow1_Sorted;
            // 
            // 見積明細テンプレート1
            // 
            見積明細テンプレート1.Height = 147;
            // 
            // 
            // 
            見積明細テンプレート1.Row.Height = 84;
            見積明細テンプレート1.Row.Width = 860;
            見積明細テンプレート1.Width = 860;
            // 
            // 見積明細
            // 
            AutoScaleMode = AutoScaleMode.None;
            Controls.Add(gcMultiRow1);
            Name = "見積明細";
            Size = new Size(1000, 350);
            Load += 受注明細_Load;
            ((System.ComponentModel.ISupportInitialize)gcMultiRow1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GrapeCity.Win.MultiRow.GcMultiRow gcMultiRow1;
        private 見積明細テンプレート 見積明細テンプレート1;
    }
}

namespace MultiRowDesigner
{
    partial class 棚卸作業サブ
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
            棚卸作業サブテンプレート1 = new 棚卸作業サブテンプレート();
            ((System.ComponentModel.ISupportInitialize)gcMultiRow1).BeginInit();
            SuspendLayout();
            // 
            // gcMultiRow1
            // 
            gcMultiRow1.EditMode = GrapeCity.Win.MultiRow.EditMode.EditOnEnter;
            gcMultiRow1.Location = new Point(0, 0);
            gcMultiRow1.Margin = new Padding(4);
            gcMultiRow1.Name = "gcMultiRow1";
            gcMultiRow1.Size = new Size(466, 180);
            gcMultiRow1.TabIndex = 0;
            gcMultiRow1.Template = 棚卸作業サブテンプレート1;
            gcMultiRow1.TemplateScaleSize = new SizeF(1.16666663F, 1.25F);
            gcMultiRow1.Text = "gcMultiRow1";
            // 
            // 棚卸作業サブテンプレート1
            // 
            棚卸作業サブテンプレート1.Height = 42;
            // 
            // 
            // 
            棚卸作業サブテンプレート1.Row.Height = 21;
            棚卸作業サブテンプレート1.Row.Width = 383;
            棚卸作業サブテンプレート1.Width = 383;
            // 
            // 棚卸作業サブ
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(gcMultiRow1);
            Margin = new Padding(4);
            Name = "棚卸作業サブ";
            Size = new Size(467, 180);
            Load += 棚卸作業サブ_Load;
            ((System.ComponentModel.ISupportInitialize)gcMultiRow1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GrapeCity.Win.MultiRow.GcMultiRow gcMultiRow1;
        private 棚卸作業サブテンプレート 棚卸作業サブテンプレート1;
    }
}

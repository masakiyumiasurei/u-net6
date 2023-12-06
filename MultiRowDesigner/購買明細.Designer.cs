namespace MultiRowDesigner
{
    partial class 購買明細
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
            this.gcMultiRow1 = new GrapeCity.Win.MultiRow.GcMultiRow();
            this.購買明細テンプレート1 = new MultiRowDesigner.購買明細テンプレート();
            ((System.ComponentModel.ISupportInitialize)(this.gcMultiRow1)).BeginInit();
            this.SuspendLayout();
            // 
            // gcMultiRow1
            // 
            this.gcMultiRow1.EditMode = GrapeCity.Win.MultiRow.EditMode.EditOnEnter;
            this.gcMultiRow1.Location = new System.Drawing.Point(0, 0);
            this.gcMultiRow1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gcMultiRow1.Name = "gcMultiRow1";
            this.gcMultiRow1.Size = new System.Drawing.Size(856, 302);
            this.gcMultiRow1.TabIndex = 0;
            this.gcMultiRow1.Template = this.購買明細テンプレート1;
            this.gcMultiRow1.TemplateScaleSize = new System.Drawing.SizeF(1.166667F, 1.25F);
            this.gcMultiRow1.Text = "gcMultiRow1";
            this.gcMultiRow1.CellEnter += new System.EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.gcMultiRow1_CellEnter);
            // 
            // 購買明細テンプレート1
            // 
            this.購買明細テンプレート1.Height = 100;
            // 
            // 
            // 
            this.購買明細テンプレート1.Row.Height = 34;
            this.購買明細テンプレート1.Row.Width = 986;
            this.購買明細テンプレート1.Width = 986;
            // 
            // 購買明細
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcMultiRow1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "購買明細";
            this.Size = new System.Drawing.Size(855, 302);
            ((System.ComponentModel.ISupportInitialize)(this.gcMultiRow1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private GrapeCity.Win.MultiRow.GcMultiRow gcMultiRow1;
        private 購買明細テンプレート 購買明細テンプレート1;
    }
}

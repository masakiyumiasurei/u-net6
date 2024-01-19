using System.Drawing;

namespace MultiRowDesigner
{
    partial class 文書添付
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
            文書添付テンプレート1 = new 文書添付テンプレート();
            ((System.ComponentModel.ISupportInitialize)gcMultiRow1).BeginInit();
            SuspendLayout();
            // 
            // gcMultiRow1
            // 
            gcMultiRow1.EditMode = GrapeCity.Win.MultiRow.EditMode.EditOnEnter;
            gcMultiRow1.Location = new Point(0, 0);
            gcMultiRow1.Margin = new Padding(5);
            gcMultiRow1.Name = "gcMultiRow1";
            gcMultiRow1.Size = new Size(400, 503);
            gcMultiRow1.TabIndex = 0;
            gcMultiRow1.Template = 文書添付テンプレート1;
            gcMultiRow1.Text = "gcMultiRow1";
            // 
            // 文書添付テンプレート1
            // 
            文書添付テンプレート1.Height = 190;
            // 
            // 
            // 
            文書添付テンプレート1.Row.Height = 159;
            文書添付テンプレート1.Row.Width = 380;
            文書添付テンプレート1.Width = 380;
            // 
            // 文書添付
            // 
            Controls.Add(gcMultiRow1);
            Margin = new Padding(5);
            Name = "文書添付";
            Size = new Size(401, 504);
            ((System.ComponentModel.ISupportInitialize)gcMultiRow1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GrapeCity.Win.MultiRow.GcMultiRow gcMultiRow1;
        private 文書添付テンプレート 文書添付テンプレート1;
    }
}

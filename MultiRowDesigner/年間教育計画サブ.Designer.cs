using System.Drawing;
using System.Windows.Forms;

namespace MultiRowDesigner
{
    partial class 年間教育計画サブ
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
            年間教育計画サブテンプレート1 = new 年間教育計画サブテンプレート();
            ((System.ComponentModel.ISupportInitialize)gcMultiRow1).BeginInit();
            SuspendLayout();
            // 
            // gcMultiRow1
            // 
            gcMultiRow1.Dock = DockStyle.Fill;
            gcMultiRow1.EditMode = GrapeCity.Win.MultiRow.EditMode.EditOnEnter;
            gcMultiRow1.Location = new Point(0, 0);
            gcMultiRow1.Margin = new Padding(5);
            gcMultiRow1.Name = "gcMultiRow1";
            gcMultiRow1.Size = new Size(1086, 387);
            gcMultiRow1.TabIndex = 0;
            gcMultiRow1.Template = 年間教育計画サブテンプレート1;
            gcMultiRow1.TemplateScaleSize = new SizeF(1.33333337F, 1.66666675F);
            gcMultiRow1.Text = "gcMultiRow1";
            gcMultiRow1.CellContentClick += gcMultiRow1_CellContentClick;
            // 
            // 年間教育計画サブテンプレート1
            // 
            年間教育計画サブテンプレート1.Height = 76;
            // 
            // 
            // 
            年間教育計画サブテンプレート1.Row.Height = 36;
            年間教育計画サブテンプレート1.Row.Width = 840;
            年間教育計画サブテンプレート1.Width = 840;
            // 
            // 年間教育計画サブ
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(gcMultiRow1);
            Margin = new Padding(5);
            Name = "年間教育計画サブ";
            Size = new Size(1086, 387);
            Load += ユニット明細_Load;
            ((System.ComponentModel.ISupportInitialize)gcMultiRow1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GrapeCity.Win.MultiRow.GcMultiRow gcMultiRow1;
        private 年間教育計画サブテンプレート 年間教育計画サブテンプレート1;
    }
}

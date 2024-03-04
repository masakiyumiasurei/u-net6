using System.Drawing;
using System.Windows.Forms;

namespace MultiRowDesigner
{
    partial class 発注明細
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
            発注明細テンプレート1 = new 発注明細テンプレート();
            ((System.ComponentModel.ISupportInitialize)gcMultiRow1).BeginInit();
            SuspendLayout();
            // 
            // gcMultiRow1
            // 
            gcMultiRow1.AllowRowMove = true;
            gcMultiRow1.Dock = DockStyle.Fill;
            gcMultiRow1.EditMode = GrapeCity.Win.MultiRow.EditMode.EditOnEnter;
            gcMultiRow1.Location = new Point(0, 0);
            gcMultiRow1.Margin = new Padding(5);
            gcMultiRow1.Name = "gcMultiRow1";
            gcMultiRow1.Size = new Size(1537, 672);
            gcMultiRow1.TabIndex = 0;
            gcMultiRow1.Template = 発注明細テンプレート1;
            gcMultiRow1.TemplateScaleSize = new SizeF(1.33333349F, 1.66666675F);
            gcMultiRow1.Text = "gcMultiRow1";
            gcMultiRow1.RowsAdding += gcMultiRow1_RowsAdding;
            gcMultiRow1.RowsRemoved += gcMultiRow1_RowsRemoved;
            gcMultiRow1.CellValidating += gcMultiRow1_CellValidating;
            gcMultiRow1.CellValidated += gcMultiRow1_CellValidated;
            gcMultiRow1.CellValueChanged += gcMultiRow1_CellValueChanged;
            gcMultiRow1.CellFormatting += gcMultiRow1_CellFormatting;
            gcMultiRow1.CellBeginEdit += gcMultiRow1_CellBeginEdit;
            gcMultiRow1.CellEnter += gcMultiRow1_CellEnter;
            gcMultiRow1.RowValidating += gcMultiRow1_RowValidating;
            gcMultiRow1.DefaultValuesNeeded += gcMultiRow1_DefaultValuesNeeded;
            gcMultiRow1.EditingControlShowing += gcMultiRow1_EditingControlShowing;
            gcMultiRow1.CellDoubleClick += gcMultiRow1_CellDoubleClick;
            gcMultiRow1.CellContentClick += gcMultiRow1_CellContentButtonClick;
            gcMultiRow1.CellContentButtonClick += gcMultiRow1_CellContentButtonClick;
            gcMultiRow1.CellContentDoubleClick += gcMultiRow1_CellContentDoubleClick;
            gcMultiRow1.CellMouseDoubleClick += gcMultiRow1_CellMouseDoubleClick;
            gcMultiRow1.Sorted += gcMultiRow1_Sorted;
            gcMultiRow1.KeyPress += gcMultiRow1_KeyPress;
            gcMultiRow1.PreviewKeyDown += gcMultiRow1_PreviewKeyDown;
            // 
            // 発注明細テンプレート1
            // 
            発注明細テンプレート1.Height = 101;
            // 
            // 
            // 
            発注明細テンプレート1.Row.Height = 34;
            発注明細テンプレート1.Row.Width = 1124;
            発注明細テンプレート1.Width = 1124;
            // 
            // 発注明細
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            Controls.Add(gcMultiRow1);
            Margin = new Padding(5);
            Name = "発注明細";
            Size = new Size(1537, 672);
            Load += 発注明細_Load;
            ((System.ComponentModel.ISupportInitialize)gcMultiRow1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GrapeCity.Win.MultiRow.GcMultiRow gcMultiRow1;
        private 発注明細テンプレート 発注明細テンプレート1;
    }
}

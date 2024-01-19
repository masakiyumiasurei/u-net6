
namespace u_net
{
    partial class F_シリーズ危険在庫警告
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

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_シリーズ危険在庫警告));
            newDataSet = new newDataSet();
            combBox商品コードBindingSource = new BindingSource(components);
            m商品分類BindingSource = new BindingSource(components);
            mシリーズBindingSource = new BindingSource(components);
            dataGridView1 = new DataGridView();
            明細削除ボタン = new DataGridViewButtonColumn();
            行挿入ボタン = new DataGridViewButtonColumn();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn8 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn9 = new DataGridViewTextBoxColumn();
            日誌IDDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn10 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn11 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn12 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn13 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn14 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn15 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn16 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn17 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn18 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn19 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn20 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn21 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn22 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn23 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn24 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn25 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn26 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn27 = new DataGridViewTextBoxColumn();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            v商品ヘッダBindingSource = new BindingSource(components);
            v商品ヘッダTableAdapter = new uiDataSetTableAdapters.V商品ヘッダTableAdapter();
            combBoxMシリーズTableAdapter = new uiDataSetTableAdapters.combBoxMシリーズTableAdapter();
            m商品分類TableAdapter = new uiDataSetTableAdapters.M商品分類TableAdapter();
            notifyIcon1 = new NotifyIcon(components);
            mshomeisaiTableAdapter = new newDataSetTableAdapters.MshomeisaiTableAdapter();
            ((System.ComponentModel.ISupportInitialize)newDataSet).BeginInit();
            ((System.ComponentModel.ISupportInitialize)combBox商品コードBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)m商品分類BindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mシリーズBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)v商品ヘッダBindingSource).BeginInit();
            SuspendLayout();
            // 
            // newDataSet
            // 
            newDataSet.DataSetName = "newDataSet";
            newDataSet.Namespace = "http://tempuri.org/newDataSet.xsd";
            newDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // combBox商品コードBindingSource
            // 
            combBox商品コードBindingSource.DataMember = "CombBox商品コード";
            // 
            // m商品分類BindingSource
            // 
            m商品分類BindingSource.DataMember = "M商品分類";
            // 
            // mシリーズBindingSource
            // 
            mシリーズBindingSource.DataMember = "Mシリーズ";
            // 
            // dataGridView1
            // 
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.ButtonFace;
            dataGridViewCellStyle1.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { 明細削除ボタン, 行挿入ボタン });
            dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
            dataGridView1.ImeMode = ImeMode.On;
            dataGridView1.Location = new Point(0, 10);
            dataGridView1.Margin = new Padding(4, 3, 4, 3);
            dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle4.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridView1.RowTemplate.Height = 21;
            dataGridView1.Size = new Size(943, 329);
            dataGridView1.TabIndex = 109;
            dataGridView1.CellPainting += DataGridView1_CellPainting;
            // 
            // 明細削除ボタン
            // 
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.ForeColor = Color.Maroon;
            明細削除ボタン.DefaultCellStyle = dataGridViewCellStyle2;
            明細削除ボタン.HeaderText = "";
            明細削除ボタン.Name = "明細削除ボタン";
            明細削除ボタン.Text = "X";
            明細削除ボタン.ToolTipText = "明細行を削除します";
            明細削除ボタン.UseColumnTextForButtonValue = true;
            明細削除ボタン.Width = 22;
            // 
            // 行挿入ボタン
            // 
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.ForeColor = Color.Maroon;
            行挿入ボタン.DefaultCellStyle = dataGridViewCellStyle3;
            行挿入ボタン.HeaderText = "";
            行挿入ボタン.Name = "行挿入ボタン";
            行挿入ボタン.Text = "▶";
            行挿入ボタン.ToolTipText = "行挿入";
            行挿入ボタン.UseColumnTextForButtonValue = true;
            行挿入ボタン.Width = 19;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.DataPropertyName = "商品コード";
            dataGridViewTextBoxColumn1.HeaderText = "商品コード";
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.DataPropertyName = "Revision";
            dataGridViewTextBoxColumn2.HeaderText = "Revision";
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.DataPropertyName = "明細番号";
            dataGridViewTextBoxColumn3.HeaderText = "明細番号";
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.DataPropertyName = "型式番号";
            dataGridViewTextBoxColumn4.HeaderText = "型式番号";
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewTextBoxColumn5.DataPropertyName = "型式名";
            dataGridViewTextBoxColumn5.HeaderText = "型式名";
            dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewTextBoxColumn6.DataPropertyName = "定価";
            dataGridViewTextBoxColumn6.HeaderText = "定価";
            dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewTextBoxColumn7
            // 
            dataGridViewTextBoxColumn7.DataPropertyName = "原価";
            dataGridViewTextBoxColumn7.HeaderText = "原価";
            dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewTextBoxColumn8
            // 
            dataGridViewTextBoxColumn8.DataPropertyName = "機能";
            dataGridViewTextBoxColumn8.HeaderText = "機能";
            dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            // 
            // dataGridViewTextBoxColumn9
            // 
            dataGridViewTextBoxColumn9.DataPropertyName = "構成番号";
            dataGridViewTextBoxColumn9.HeaderText = "構成番号";
            dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            // 
            // 日誌IDDataGridViewTextBoxColumn
            // 
            日誌IDDataGridViewTextBoxColumn.Name = "日誌IDDataGridViewTextBoxColumn";
            // 
            // dataGridViewTextBoxColumn10
            // 
            dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            // 
            // dataGridViewTextBoxColumn11
            // 
            dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            // 
            // dataGridViewTextBoxColumn12
            // 
            dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            // 
            // dataGridViewTextBoxColumn13
            // 
            dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            // 
            // dataGridViewTextBoxColumn14
            // 
            dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            // 
            // dataGridViewTextBoxColumn15
            // 
            dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            // 
            // dataGridViewTextBoxColumn16
            // 
            dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            // 
            // dataGridViewTextBoxColumn17
            // 
            dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
            // 
            // dataGridViewTextBoxColumn18
            // 
            dataGridViewTextBoxColumn18.Name = "dataGridViewTextBoxColumn18";
            // 
            // dataGridViewTextBoxColumn19
            // 
            dataGridViewTextBoxColumn19.Name = "dataGridViewTextBoxColumn19";
            // 
            // dataGridViewTextBoxColumn20
            // 
            dataGridViewTextBoxColumn20.Name = "dataGridViewTextBoxColumn20";
            // 
            // dataGridViewTextBoxColumn21
            // 
            dataGridViewTextBoxColumn21.Name = "dataGridViewTextBoxColumn21";
            // 
            // dataGridViewTextBoxColumn22
            // 
            dataGridViewTextBoxColumn22.Name = "dataGridViewTextBoxColumn22";
            // 
            // dataGridViewTextBoxColumn23
            // 
            dataGridViewTextBoxColumn23.Name = "dataGridViewTextBoxColumn23";
            // 
            // dataGridViewTextBoxColumn24
            // 
            dataGridViewTextBoxColumn24.Name = "dataGridViewTextBoxColumn24";
            // 
            // dataGridViewTextBoxColumn25
            // 
            dataGridViewTextBoxColumn25.Name = "dataGridViewTextBoxColumn25";
            // 
            // dataGridViewTextBoxColumn26
            // 
            dataGridViewTextBoxColumn26.Name = "dataGridViewTextBoxColumn26";
            // 
            // dataGridViewTextBoxColumn27
            // 
            dataGridViewTextBoxColumn27.Name = "dataGridViewTextBoxColumn27";
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 348);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 19, 0);
            statusStrip1.Size = new Size(939, 22);
            statusStrip1.TabIndex = 110;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(89, 17);
            toolStripStatusLabel1.Text = "各種項目の説明";
            // 
            // v商品ヘッダBindingSource
            // 
            v商品ヘッダBindingSource.DataMember = "V商品ヘッダ";
            // 
            // v商品ヘッダTableAdapter
            // 
            v商品ヘッダTableAdapter.ClearBeforeFill = true;
            // 
            // combBoxMシリーズTableAdapter
            // 
            combBoxMシリーズTableAdapter.ClearBeforeFill = true;
            // 
            // m商品分類TableAdapter
            // 
            m商品分類TableAdapter.ClearBeforeFill = true;
            // 
            // notifyIcon1
            // 
            notifyIcon1.Text = "notifyIcon1";
            notifyIcon1.Visible = true;
            // 
            // mshomeisaiTableAdapter
            // 
            mshomeisaiTableAdapter.ClearBeforeFill = true;
            // 
            // F_シリーズ危険在庫警告
            // 
            AutoScaleDimensions = new SizeF(8F, 12F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(939, 370);
            Controls.Add(statusStrip1);
            Controls.Add(dataGridView1);
            Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Margin = new Padding(3, 2, 3, 2);
            Name = "F_シリーズ危険在庫警告";
            Text = "シリーズ危険在庫警告";
            Load += Form_Load;
            ((System.ComponentModel.ISupportInitialize)newDataSet).EndInit();
            ((System.ComponentModel.ISupportInitialize)combBox商品コードBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)m商品分類BindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)mシリーズBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)v商品ヘッダBindingSource).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn20;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn21;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn22;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn23;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn24;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn25;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn26;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn27;
        private CalendarColumn calendarColumn1;


        private CalendarColumn calendarColumn2;
        private CalendarColumn calendarColumn3;
        private DataGridViewTextBoxColumn 日誌IDDataGridViewTextBoxColumn;

        private CalendarColumn 登録日;

        private DataGridView dataGridView1;
        private BindingSource m商品分類BindingSource;
        private uiDataSetTableAdapters.M商品分類TableAdapter m商品分類TableAdapter;
        private BindingSource combBox商品コードBindingSource;
        private StatusStrip statusStrip1;
        internal ToolStripStatusLabel toolStripStatusLabel1;

        private BindingSource mシリーズBindingSource;
        private BindingSource v商品ヘッダBindingSource;
        private uiDataSetTableAdapters.V商品ヘッダTableAdapter v商品ヘッダTableAdapter;
        private uiDataSetTableAdapters.combBoxMシリーズTableAdapter combBoxMシリーズTableAdapter;
        private newDataSet newDataSet;
        private NotifyIcon notifyIcon1;
        private newDataSetTableAdapters.MshomeisaiTableAdapter mshomeisaiTableAdapter;
        private DataGridViewButtonColumn 明細削除ボタン;
        private DataGridViewButtonColumn 行挿入ボタン;
    }
}


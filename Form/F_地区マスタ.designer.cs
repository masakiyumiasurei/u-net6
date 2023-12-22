
namespace u_net
{
    partial class F_地区マスタ
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_地区マスタ));
            dataGridView1 = new DataGridView();
            地区コード1 = new DataGridViewTextBoxColumn();
            地区名 = new DataGridViewTextBoxColumn();
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
            label1 = new Label();
            コマンド削除 = new Button();
            コマンド複写 = new Button();
            コマンド修正 = new Button();
            コマンド新規 = new Button();
            コマンド製品 = new Button();
            コマンド承認 = new Button();
            コマンドシリーズ = new Button();
            コマンド確定 = new Button();
            label2 = new Label();
            label3 = new Label();
            dataGridView2 = new DataGridView();
            dataGridViewTextBoxColumn28 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn29 = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.ButtonFace;
            dataGridViewCellStyle1.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { 地区コード1, 地区名 });
            dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
            dataGridView1.ImeMode = ImeMode.On;
            dataGridView1.Location = new Point(13, 42);
            dataGridView1.Margin = new Padding(4, 3, 4, 3);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle2.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.RowTemplate.Height = 21;
            dataGridView1.Size = new Size(417, 399);
            dataGridView1.TabIndex = 109;
            dataGridView1.CellEnter += dataGridView1_CellEnter;
            // 
            // 地区コード1
            // 
            地区コード1.DataPropertyName = "地区コード";
            地区コード1.HeaderText = "地区コード";
            地区コード1.Name = "地区コード1";
            地区コード1.ReadOnly = true;
            地区コード1.Width = 190;
            // 
            // 地区名
            // 
            地区名.DataPropertyName = "地区名";
            地区名.HeaderText = "地区名";
            地区名.Name = "地区名";
            地区名.ReadOnly = true;
            地区名.Width = 190;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewTextBoxColumn7
            // 
            dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewTextBoxColumn8
            // 
            dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            // 
            // dataGridViewTextBoxColumn9
            // 
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
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(242, 542);
            label1.Name = "label1";
            label1.Size = new Size(425, 12);
            label1.TabIndex = 111;
            label1.Text = "※各マスタにデータを追加する場合、必ず重複しないコードを入力してください。";
            // 
            // コマンド削除
            // 
            コマンド削除.Location = new Point(0, 0);
            コマンド削除.Name = "コマンド削除";
            コマンド削除.Size = new Size(75, 23);
            コマンド削除.TabIndex = 0;
            // 
            // コマンド複写
            // 
            コマンド複写.Location = new Point(0, 0);
            コマンド複写.Name = "コマンド複写";
            コマンド複写.Size = new Size(75, 23);
            コマンド複写.TabIndex = 0;
            // 
            // コマンド修正
            // 
            コマンド修正.Location = new Point(0, 0);
            コマンド修正.Name = "コマンド修正";
            コマンド修正.Size = new Size(75, 23);
            コマンド修正.TabIndex = 0;
            // 
            // コマンド新規
            // 
            コマンド新規.Location = new Point(0, 0);
            コマンド新規.Name = "コマンド新規";
            コマンド新規.Size = new Size(75, 23);
            コマンド新規.TabIndex = 0;
            // 
            // コマンド製品
            // 
            コマンド製品.Location = new Point(0, 0);
            コマンド製品.Name = "コマンド製品";
            コマンド製品.Size = new Size(75, 23);
            コマンド製品.TabIndex = 0;
            // 
            // コマンド承認
            // 
            コマンド承認.Location = new Point(0, 0);
            コマンド承認.Name = "コマンド承認";
            コマンド承認.Size = new Size(75, 23);
            コマンド承認.TabIndex = 0;
            // 
            // コマンドシリーズ
            // 
            コマンドシリーズ.Location = new Point(0, 0);
            コマンドシリーズ.Name = "コマンドシリーズ";
            コマンドシリーズ.Size = new Size(75, 23);
            コマンドシリーズ.TabIndex = 0;
            // 
            // コマンド確定
            // 
            コマンド確定.Location = new Point(0, 0);
            コマンド確定.Name = "コマンド確定";
            コマンド確定.Size = new Size(75, 23);
            コマンド確定.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(13, 27);
            label2.Name = "label2";
            label2.Size = new Size(53, 12);
            label2.TabIndex = 114;
            label2.Text = "地区区分";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(488, 27);
            label3.Name = "label3";
            label3.Size = new Size(53, 12);
            label3.TabIndex = 115;
            label3.Text = "地区設定";
            // 
            // dataGridView2
            // 
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.ButtonFace;
            dataGridViewCellStyle3.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn28, dataGridViewTextBoxColumn29 });
            dataGridView2.EditMode = DataGridViewEditMode.EditOnEnter;
            dataGridView2.ImeMode = ImeMode.On;
            dataGridView2.Location = new Point(488, 42);
            dataGridView2.Margin = new Padding(4, 3, 4, 3);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle4.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dataGridView2.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridView2.RowTemplate.Height = 21;
            dataGridView2.Size = new Size(417, 399);
            dataGridView2.TabIndex = 116;
            dataGridView2.CellEnter += dataGridView2_CellEnter;
            // 
            // dataGridViewTextBoxColumn28
            // 
            dataGridViewTextBoxColumn28.DataPropertyName = "地区コード";
            dataGridViewTextBoxColumn28.HeaderText = "地区コード";
            dataGridViewTextBoxColumn28.Name = "dataGridViewTextBoxColumn28";
            dataGridViewTextBoxColumn28.ReadOnly = true;
            dataGridViewTextBoxColumn28.Width = 190;
            // 
            // dataGridViewTextBoxColumn29
            // 
            dataGridViewTextBoxColumn29.DataPropertyName = "都道府県名";
            dataGridViewTextBoxColumn29.HeaderText = "都道府県名";
            dataGridViewTextBoxColumn29.Name = "dataGridViewTextBoxColumn29";
            dataGridViewTextBoxColumn29.ReadOnly = true;
            dataGridViewTextBoxColumn29.Width = 190;
            // 
            // F_地区マスタ
            // 
            AutoScaleDimensions = new SizeF(8F, 12F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(939, 618);
            Controls.Add(dataGridView2);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dataGridView1);
            Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            //Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Margin = new Padding(3, 2, 3, 2);
            Name = "F_地区マスタ";
            Text = "地区マスタ";
            FormClosing += Form_FormClosing;
            Load += Form_Load;
            Shown += F_地区マスタ_Shown;
            KeyDown += F_地区マスタ_KeyDown;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
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
        private BindingSource M商品BindingSource;
        private BindingSource M商品明細BindingSource;

        private DataGridView dataGridView1;
        private BindingSource m単位BindingSource;
        private uiDataSetTableAdapters.M単位TableAdapter M単位TableAdapter;
        private BindingSource manufactureFlowBindingSource;
        private uiDataSetTableAdapters.ComboBoxManufactureFlowTableAdapter comboBoxManufactureFlowTableAdapter;
        private BindingSource comboBox売上区分bindingSource;
        private uiDataSetTableAdapters.ComboBox売上区分TableAdapter comboBox売上区分TableAdapter;
        private BindingSource m商品分類BindingSource;
        private uiDataSetTableAdapters.M商品分類TableAdapter m商品分類TableAdapter;
        private BindingSource combBox商品コードBindingSource;
        private uiDataSetTableAdapters.CombBox商品コードTableAdapter combBox商品コードTableAdapter;
        private StatusStrip statusStrip1;
        internal ToolStripStatusLabel toolStripStatusLabel1;

        private BindingSource mシリーズBindingSource;
        private BindingSource v商品ヘッダBindingSource;

        private uiDataSet uiDataSet;
        private uiDataSetTableAdapters.M商品TableAdapter m商品TableAdapter;
        private uiDataSetTableAdapters.V商品ヘッダTableAdapter v商品ヘッダTableAdapter;
        private uiDataSetTableAdapters.combBoxMシリーズTableAdapter combBoxMシリーズTableAdapter;
        private uiDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private newDataSet newDataSet;
        private NotifyIcon notifyIcon1;
        private newDataSetTableAdapters.MshomeisaiTableAdapter mshomeisaiTableAdapter;
        private Label label1;
        private Button コマンド製品;
        private Button コマンドシリーズ;
        private Button コマンド確定;
        private Button コマンド承認;
        private Button コマンド削除;
        private Button コマンド複写;
        private Button コマンド修正;
        private Button コマンド新規;
        private Label label2;
        private Label label3;
        private DataGridViewTextBoxColumn 地区コード1;
        private DataGridViewTextBoxColumn 地区名;
        private DataGridView dataGridView2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn28;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn29;
    }
}


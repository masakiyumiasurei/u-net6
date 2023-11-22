
namespace u_net
{
    partial class F_メイン
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_メイン));
            コマンド終了 = new Button();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            tabControl1 = new TabControl();
            販売 = new TabPage();
            出荷管理ボタン = new Button();
            シリーズ在庫参照ボタン = new Button();
            売上分析ボタン = new Button();
            売上計画ボタン = new Button();
            受注管理ボタン = new Button();
            承認管理ボタン = new Button();
            見積入力ボタン = new Button();
            見積管理ボタン = new Button();
            商品構成ボタン = new Button();
            購買申請入力ボタン = new Button();
            購買申請管理ボタン = new Button();
            商品登録ボタン = new Button();
            依頼主登録 = new Button();
            RunShippingListButton = new Button();
            依頼主管理ボタン = new Button();
            顧客管理ボタン = new Button();
            顧客登録ボタン = new Button();
            シリーズ登録ボタン = new Button();
            商品管理ボタン = new Button();
            受注入力ボタン = new Button();
            不在イメージコマンド = new Button();
            在席イメージコマンド = new Button();
            営業部部長不在ボタン = new Button();
            製造 = new TabPage();
            ProductionSystemButton = new Button();
            発注入力ボタン = new Button();
            発注管理ボタン = new Button();
            button16 = new Button();
            button15 = new Button();
            button14 = new Button();
            button13 = new Button();
            button12 = new Button();
            button11 = new Button();
            button10 = new Button();
            button9 = new Button();
            button8 = new Button();
            button7 = new Button();
            仕入先別買掛一覧表ボタン = new Button();
            購買買掛一覧表ボタン = new Button();
            部品納期管理ボタン = new Button();
            入庫完了ボタン = new Button();
            入庫管理ボタン = new Button();
            入庫入力ボタン = new Button();
            label33 = new Label();
            技術 = new TabPage();
            ログインユーザー名 = new TextBox();
            ログインボタン = new Button();
            日付 = new TextBox();
            toolTip1 = new ToolTip(components);
            label23 = new Label();
            ユーアイホームボタン = new Button();
            システム設定ボタン = new Button();
            statusStrip1.SuspendLayout();
            tabControl1.SuspendLayout();
            販売.SuspendLayout();
            製造.SuspendLayout();
            SuspendLayout();
            // 
            // コマンド終了
            // 
            コマンド終了.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド終了.ForeColor = Color.Red;
            コマンド終了.Location = new Point(505, 519);
            コマンド終了.Margin = new Padding(3, 2, 3, 2);
            コマンド終了.Name = "コマンド終了";
            コマンド終了.Size = new Size(93, 36);
            コマンド終了.TabIndex = 1021;
            コマンド終了.TabStop = false;
            コマンド終了.Text = "終了";
            コマンド終了.UseVisualStyleBackColor = true;
            コマンド終了.Click += コマンド終了_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, toolStripStatusLabel2 });
            statusStrip1.Location = new Point(0, 572);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 19, 0);
            statusStrip1.Size = new Size(627, 22);
            statusStrip1.TabIndex = 10001;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(0, 17);
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new Size(89, 17);
            toolStripStatusLabel2.Text = "各種項目の説明";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(販売);
            tabControl1.Controls.Add(製造);
            tabControl1.Controls.Add(技術);
            tabControl1.ItemSize = new Size(80, 20);
            tabControl1.Location = new Point(0, 49);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(615, 462);
            tabControl1.TabIndex = 10002;
            // 
            // 販売
            // 
            販売.BackColor = SystemColors.Control;
            販売.Controls.Add(出荷管理ボタン);
            販売.Controls.Add(シリーズ在庫参照ボタン);
            販売.Controls.Add(売上分析ボタン);
            販売.Controls.Add(売上計画ボタン);
            販売.Controls.Add(受注管理ボタン);
            販売.Controls.Add(承認管理ボタン);
            販売.Controls.Add(見積入力ボタン);
            販売.Controls.Add(見積管理ボタン);
            販売.Controls.Add(商品構成ボタン);
            販売.Controls.Add(購買申請入力ボタン);
            販売.Controls.Add(購買申請管理ボタン);
            販売.Controls.Add(商品登録ボタン);
            販売.Controls.Add(依頼主登録);
            販売.Controls.Add(RunShippingListButton);
            販売.Controls.Add(依頼主管理ボタン);
            販売.Controls.Add(顧客管理ボタン);
            販売.Controls.Add(顧客登録ボタン);
            販売.Controls.Add(シリーズ登録ボタン);
            販売.Controls.Add(商品管理ボタン);
            販売.Controls.Add(受注入力ボタン);
            販売.Controls.Add(不在イメージコマンド);
            販売.Controls.Add(在席イメージコマンド);
            販売.Controls.Add(営業部部長不在ボタン);
            販売.Location = new Point(4, 24);
            販売.Name = "販売";
            販売.Padding = new Padding(3);
            販売.Size = new Size(607, 434);
            販売.TabIndex = 0;
            販売.Text = "販売";
            // 
            // 出荷管理ボタン
            // 
            出荷管理ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            出荷管理ボタン.ForeColor = Color.Black;
            出荷管理ボタン.Location = new Point(196, 275);
            出荷管理ボタン.Margin = new Padding(3, 2, 3, 2);
            出荷管理ボタン.Name = "出荷管理ボタン";
            出荷管理ボタン.Size = new Size(182, 36);
            出荷管理ボタン.TabIndex = 10028;
            出荷管理ボタン.TabStop = false;
            出荷管理ボタン.Text = "旧出荷管理";
            出荷管理ボタン.UseVisualStyleBackColor = true;
            出荷管理ボタン.Click += 出荷管理ボタン_Click;
            // 
            // シリーズ在庫参照ボタン
            // 
            シリーズ在庫参照ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            シリーズ在庫参照ボタン.ForeColor = Color.Black;
            シリーズ在庫参照ボタン.Location = new Point(384, 275);
            シリーズ在庫参照ボタン.Margin = new Padding(3, 2, 3, 2);
            シリーズ在庫参照ボタン.Name = "シリーズ在庫参照ボタン";
            シリーズ在庫参照ボタン.Size = new Size(182, 36);
            シリーズ在庫参照ボタン.TabIndex = 10027;
            シリーズ在庫参照ボタン.TabStop = false;
            シリーズ在庫参照ボタン.Text = "在庫管理";
            シリーズ在庫参照ボタン.UseVisualStyleBackColor = true;
            シリーズ在庫参照ボタン.Click += シリーズ在庫参照ボタン_Click;
            // 
            // 売上分析ボタン
            // 
            売上分析ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            売上分析ボタン.ForeColor = Color.Black;
            売上分析ボタン.Location = new Point(8, 315);
            売上分析ボタン.Margin = new Padding(3, 2, 3, 2);
            売上分析ボタン.Name = "売上分析ボタン";
            売上分析ボタン.Size = new Size(182, 36);
            売上分析ボタン.TabIndex = 10026;
            売上分析ボタン.TabStop = false;
            売上分析ボタン.Text = "売上分析";
            売上分析ボタン.UseVisualStyleBackColor = true;
            売上分析ボタン.Click += 売上分析ボタン_Click;
            // 
            // 売上計画ボタン
            // 
            売上計画ボタン.Font = new Font("Yu Gothic UI", 9F, FontStyle.Underline, GraphicsUnit.Point);
            売上計画ボタン.ForeColor = Color.Blue;
            売上計画ボタン.Location = new Point(196, 315);
            売上計画ボタン.Margin = new Padding(3, 2, 3, 2);
            売上計画ボタン.Name = "売上計画ボタン";
            売上計画ボタン.Size = new Size(182, 36);
            売上計画ボタン.TabIndex = 10025;
            売上計画ボタン.TabStop = false;
            売上計画ボタン.Text = "売上計画";
            売上計画ボタン.UseVisualStyleBackColor = true;
            売上計画ボタン.Click += 売上計画ボタン_Click;
            // 
            // 受注管理ボタン
            // 
            受注管理ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            受注管理ボタン.ForeColor = Color.Blue;
            受注管理ボタン.Location = new Point(196, 20);
            受注管理ボタン.Margin = new Padding(3, 2, 3, 2);
            受注管理ボタン.Name = "受注管理ボタン";
            受注管理ボタン.Size = new Size(182, 36);
            受注管理ボタン.TabIndex = 10024;
            受注管理ボタン.TabStop = false;
            受注管理ボタン.Text = "受注管理";
            受注管理ボタン.UseVisualStyleBackColor = true;
            // 
            // 承認管理ボタン
            // 
            承認管理ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            承認管理ボタン.ForeColor = Color.Red;
            承認管理ボタン.Location = new Point(384, 20);
            承認管理ボタン.Margin = new Padding(3, 2, 3, 2);
            承認管理ボタン.Name = "承認管理ボタン";
            承認管理ボタン.Size = new Size(182, 36);
            承認管理ボタン.TabIndex = 10023;
            承認管理ボタン.TabStop = false;
            承認管理ボタン.Text = "変更承認";
            承認管理ボタン.UseVisualStyleBackColor = true;
            // 
            // 見積入力ボタン
            // 
            見積入力ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            見積入力ボタン.ForeColor = Color.Red;
            見積入力ボタン.Location = new Point(8, 60);
            見積入力ボタン.Margin = new Padding(3, 2, 3, 2);
            見積入力ボタン.Name = "見積入力ボタン";
            見積入力ボタン.Size = new Size(182, 36);
            見積入力ボタン.TabIndex = 10022;
            見積入力ボタン.TabStop = false;
            見積入力ボタン.Text = "見積入力";
            見積入力ボタン.UseVisualStyleBackColor = true;
            // 
            // 見積管理ボタン
            // 
            見積管理ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            見積管理ボタン.ForeColor = Color.Blue;
            見積管理ボタン.Location = new Point(196, 60);
            見積管理ボタン.Margin = new Padding(3, 2, 3, 2);
            見積管理ボタン.Name = "見積管理ボタン";
            見積管理ボタン.Size = new Size(182, 36);
            見積管理ボタン.TabIndex = 10021;
            見積管理ボタン.TabStop = false;
            見積管理ボタン.Text = "見積管理";
            見積管理ボタン.UseVisualStyleBackColor = true;
            // 
            // 商品構成ボタン
            // 
            商品構成ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            商品構成ボタン.ForeColor = Color.Black;
            商品構成ボタン.Location = new Point(384, 60);
            商品構成ボタン.Margin = new Padding(3, 2, 3, 2);
            商品構成ボタン.Name = "商品構成ボタン";
            商品構成ボタン.Size = new Size(182, 36);
            商品構成ボタン.TabIndex = 10020;
            商品構成ボタン.TabStop = false;
            商品構成ボタン.Text = "価格参照";
            商品構成ボタン.UseVisualStyleBackColor = true;
            // 
            // 購買申請入力ボタン
            // 
            購買申請入力ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            購買申請入力ボタン.ForeColor = Color.Red;
            購買申請入力ボタン.Location = new Point(8, 100);
            購買申請入力ボタン.Margin = new Padding(3, 2, 3, 2);
            購買申請入力ボタン.Name = "購買申請入力ボタン";
            購買申請入力ボタン.Size = new Size(182, 36);
            購買申請入力ボタン.TabIndex = 10019;
            購買申請入力ボタン.TabStop = false;
            購買申請入力ボタン.Text = "購買申請入力";
            購買申請入力ボタン.UseVisualStyleBackColor = true;
            // 
            // 購買申請管理ボタン
            // 
            購買申請管理ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            購買申請管理ボタン.ForeColor = Color.Blue;
            購買申請管理ボタン.Location = new Point(196, 100);
            購買申請管理ボタン.Margin = new Padding(3, 2, 3, 2);
            購買申請管理ボタン.Name = "購買申請管理ボタン";
            購買申請管理ボタン.Size = new Size(182, 36);
            購買申請管理ボタン.TabIndex = 10018;
            購買申請管理ボタン.TabStop = false;
            購買申請管理ボタン.Text = "購買申請管理";
            購買申請管理ボタン.UseVisualStyleBackColor = true;
            // 
            // 商品登録ボタン
            // 
            商品登録ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            商品登録ボタン.ForeColor = Color.Red;
            商品登録ボタン.Location = new Point(8, 149);
            商品登録ボタン.Margin = new Padding(3, 2, 3, 2);
            商品登録ボタン.Name = "商品登録ボタン";
            商品登録ボタン.Size = new Size(182, 36);
            商品登録ボタン.TabIndex = 10017;
            商品登録ボタン.TabStop = false;
            商品登録ボタン.Text = "商品登録";
            商品登録ボタン.UseVisualStyleBackColor = true;
            商品登録ボタン.Click += 商品登録ボタン_Click;
            // 
            // 依頼主登録
            // 
            依頼主登録.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            依頼主登録.ForeColor = Color.Red;
            依頼主登録.Location = new Point(8, 231);
            依頼主登録.Margin = new Padding(3, 2, 3, 2);
            依頼主登録.Name = "依頼主登録";
            依頼主登録.Size = new Size(182, 36);
            依頼主登録.TabIndex = 10016;
            依頼主登録.TabStop = false;
            依頼主登録.Text = "依頼主登録";
            依頼主登録.UseVisualStyleBackColor = true;
            依頼主登録.Click += 依頼主登録_Click;
            // 
            // RunShippingListButton
            // 
            RunShippingListButton.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            RunShippingListButton.ForeColor = Color.Blue;
            RunShippingListButton.Location = new Point(8, 275);
            RunShippingListButton.Margin = new Padding(3, 2, 3, 2);
            RunShippingListButton.Name = "RunShippingListButton";
            RunShippingListButton.Size = new Size(182, 36);
            RunShippingListButton.TabIndex = 10015;
            RunShippingListButton.TabStop = false;
            RunShippingListButton.Text = "出荷管理";
            RunShippingListButton.UseVisualStyleBackColor = true;
            RunShippingListButton.Click += RunShippingListButton_Click;
            // 
            // 依頼主管理ボタン
            // 
            依頼主管理ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            依頼主管理ボタン.ForeColor = Color.Blue;
            依頼主管理ボタン.Location = new Point(196, 231);
            依頼主管理ボタン.Margin = new Padding(3, 2, 3, 2);
            依頼主管理ボタン.Name = "依頼主管理ボタン";
            依頼主管理ボタン.Size = new Size(182, 36);
            依頼主管理ボタン.TabIndex = 10014;
            依頼主管理ボタン.TabStop = false;
            依頼主管理ボタン.Text = "依頼主管理ボタン";
            依頼主管理ボタン.UseVisualStyleBackColor = true;
            依頼主管理ボタン.Click += 依頼主管理ボタン_Click;
            // 
            // 顧客管理ボタン
            // 
            顧客管理ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            顧客管理ボタン.ForeColor = Color.Blue;
            顧客管理ボタン.Location = new Point(196, 191);
            顧客管理ボタン.Margin = new Padding(3, 2, 3, 2);
            顧客管理ボタン.Name = "顧客管理ボタン";
            顧客管理ボタン.Size = new Size(182, 36);
            顧客管理ボタン.TabIndex = 10013;
            顧客管理ボタン.TabStop = false;
            顧客管理ボタン.Text = "顧客管理";
            顧客管理ボタン.UseVisualStyleBackColor = true;
            顧客管理ボタン.Click += 顧客管理ボタン_Click;
            // 
            // 顧客登録ボタン
            // 
            顧客登録ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            顧客登録ボタン.ForeColor = Color.Red;
            顧客登録ボタン.Location = new Point(8, 189);
            顧客登録ボタン.Margin = new Padding(3, 2, 3, 2);
            顧客登録ボタン.Name = "顧客登録ボタン";
            顧客登録ボタン.Size = new Size(182, 36);
            顧客登録ボタン.TabIndex = 10012;
            顧客登録ボタン.TabStop = false;
            顧客登録ボタン.Text = "顧客登録";
            顧客登録ボタン.UseVisualStyleBackColor = true;
            顧客登録ボタン.Click += 顧客登録ボタン_Click;
            // 
            // シリーズ登録ボタン
            // 
            シリーズ登録ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            シリーズ登録ボタン.ForeColor = Color.Red;
            シリーズ登録ボタン.Location = new Point(384, 149);
            シリーズ登録ボタン.Margin = new Padding(3, 2, 3, 2);
            シリーズ登録ボタン.Name = "シリーズ登録ボタン";
            シリーズ登録ボタン.Size = new Size(182, 36);
            シリーズ登録ボタン.TabIndex = 10011;
            シリーズ登録ボタン.TabStop = false;
            シリーズ登録ボタン.Text = "シリーズ登録";
            シリーズ登録ボタン.UseVisualStyleBackColor = true;
            シリーズ登録ボタン.Click += シリーズ登録ボタン_Click;
            // 
            // 商品管理ボタン
            // 
            商品管理ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            商品管理ボタン.ForeColor = Color.Blue;
            商品管理ボタン.Location = new Point(196, 149);
            商品管理ボタン.Margin = new Padding(3, 2, 3, 2);
            商品管理ボタン.Name = "商品管理ボタン";
            商品管理ボタン.Size = new Size(182, 36);
            商品管理ボタン.TabIndex = 10010;
            商品管理ボタン.TabStop = false;
            商品管理ボタン.Text = "商品管理";
            商品管理ボタン.UseVisualStyleBackColor = true;
            商品管理ボタン.Click += 商品管理ボタン_Click;
            // 
            // 受注入力ボタン
            // 
            受注入力ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            受注入力ボタン.ForeColor = Color.Red;
            受注入力ボタン.Location = new Point(8, 20);
            受注入力ボタン.Margin = new Padding(3, 2, 3, 2);
            受注入力ボタン.Name = "受注入力ボタン";
            受注入力ボタン.Size = new Size(182, 36);
            受注入力ボタン.TabIndex = 10008;
            受注入力ボタン.TabStop = false;
            受注入力ボタン.Text = "受注入力";
            受注入力ボタン.UseVisualStyleBackColor = true;
            受注入力ボタン.Click += 受注入力ボタン_Click;
            // 
            // 不在イメージコマンド
            // 
            不在イメージコマンド.Font = new Font("Yu Gothic UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            不在イメージコマンド.ForeColor = Color.Black;
            不在イメージコマンド.Image = (Image)resources.GetObject("不在イメージコマンド.Image");
            不在イメージコマンド.Location = new Point(532, 212);
            不在イメージコマンド.Name = "不在イメージコマンド";
            不在イメージコマンド.Size = new Size(51, 45);
            不在イメージコマンド.TabIndex = 10009;
            不在イメージコマンド.UseVisualStyleBackColor = true;
            不在イメージコマンド.Visible = false;
            // 
            // 在席イメージコマンド
            // 
            在席イメージコマンド.Font = new Font("Yu Gothic UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            在席イメージコマンド.ForeColor = Color.Black;
            在席イメージコマンド.Image = (Image)resources.GetObject("在席イメージコマンド.Image");
            在席イメージコマンド.Location = new Point(436, 212);
            在席イメージコマンド.Name = "在席イメージコマンド";
            在席イメージコマンド.Size = new Size(51, 45);
            在席イメージコマンド.TabIndex = 10008;
            在席イメージコマンド.UseVisualStyleBackColor = true;
            在席イメージコマンド.Visible = false;
            // 
            // 営業部部長不在ボタン
            // 
            営業部部長不在ボタン.Font = new Font("Yu Gothic UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            営業部部長不在ボタン.ForeColor = Color.Black;
            営業部部長不在ボタン.Image = (Image)resources.GetObject("営業部部長不在ボタン.Image");
            営業部部長不在ボタン.Location = new Point(426, 367);
            営業部部長不在ボタン.Name = "営業部部長不在ボタン";
            営業部部長不在ボタン.Size = new Size(51, 45);
            営業部部長不在ボタン.TabIndex = 10007;
            営業部部長不在ボタン.UseVisualStyleBackColor = true;
            // 
            // 製造
            // 
            製造.BackColor = SystemColors.Control;
            製造.Controls.Add(ProductionSystemButton);
            製造.Controls.Add(発注入力ボタン);
            製造.Controls.Add(発注管理ボタン);
            製造.Controls.Add(button16);
            製造.Controls.Add(button15);
            製造.Controls.Add(button14);
            製造.Controls.Add(button13);
            製造.Controls.Add(button12);
            製造.Controls.Add(button11);
            製造.Controls.Add(button10);
            製造.Controls.Add(button9);
            製造.Controls.Add(button8);
            製造.Controls.Add(button7);
            製造.Controls.Add(仕入先別買掛一覧表ボタン);
            製造.Controls.Add(購買買掛一覧表ボタン);
            製造.Controls.Add(部品納期管理ボタン);
            製造.Controls.Add(入庫完了ボタン);
            製造.Controls.Add(入庫管理ボタン);
            製造.Controls.Add(入庫入力ボタン);
            製造.Controls.Add(label33);
            製造.Location = new Point(4, 24);
            製造.Name = "製造";
            製造.Padding = new Padding(3);
            製造.Size = new Size(607, 434);
            製造.TabIndex = 1;
            製造.Text = "製造";
            // 
            // ProductionSystemButton
            // 
            ProductionSystemButton.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            ProductionSystemButton.ForeColor = Color.Black;
            ProductionSystemButton.Location = new Point(6, 18);
            ProductionSystemButton.Margin = new Padding(3, 2, 3, 2);
            ProductionSystemButton.Name = "ProductionSystemButton";
            ProductionSystemButton.Size = new Size(182, 36);
            ProductionSystemButton.TabIndex = 10045;
            ProductionSystemButton.TabStop = false;
            ProductionSystemButton.Text = "生産管理システム";
            ProductionSystemButton.UseVisualStyleBackColor = true;
            ProductionSystemButton.Click += ProductionSystemButton_Click;
            // 
            // 発注入力ボタン
            // 
            発注入力ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            発注入力ボタン.ForeColor = Color.Red;
            発注入力ボタン.Location = new Point(6, 58);
            発注入力ボタン.Margin = new Padding(3, 2, 3, 2);
            発注入力ボタン.Name = "発注入力ボタン";
            発注入力ボタン.Size = new Size(182, 36);
            発注入力ボタン.TabIndex = 10044;
            発注入力ボタン.TabStop = false;
            発注入力ボタン.Text = "発注入力";
            発注入力ボタン.UseVisualStyleBackColor = true;
            // 
            // 発注管理ボタン
            // 
            発注管理ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            発注管理ボタン.ForeColor = Color.Blue;
            発注管理ボタン.Location = new Point(194, 58);
            発注管理ボタン.Margin = new Padding(3, 2, 3, 2);
            発注管理ボタン.Name = "発注管理ボタン";
            発注管理ボタン.Size = new Size(182, 36);
            発注管理ボタン.TabIndex = 10043;
            発注管理ボタン.TabStop = false;
            発注管理ボタン.Text = "発注管理";
            発注管理ボタン.UseVisualStyleBackColor = true;
            // 
            // button16
            // 
            button16.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            button16.ForeColor = Color.Blue;
            button16.Location = new Point(332, 319);
            button16.Margin = new Padding(3, 2, 3, 2);
            button16.Name = "button16";
            button16.Size = new Size(182, 36);
            button16.TabIndex = 10042;
            button16.TabStop = false;
            button16.Text = "受注管理";
            button16.UseVisualStyleBackColor = true;
            // 
            // button15
            // 
            button15.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            button15.ForeColor = Color.Blue;
            button15.Location = new Point(324, 311);
            button15.Margin = new Padding(3, 2, 3, 2);
            button15.Name = "button15";
            button15.Size = new Size(182, 36);
            button15.TabIndex = 10041;
            button15.TabStop = false;
            button15.Text = "受注管理";
            button15.UseVisualStyleBackColor = true;
            // 
            // button14
            // 
            button14.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            button14.ForeColor = Color.Blue;
            button14.Location = new Point(316, 303);
            button14.Margin = new Padding(3, 2, 3, 2);
            button14.Name = "button14";
            button14.Size = new Size(182, 36);
            button14.TabIndex = 10040;
            button14.TabStop = false;
            button14.Text = "受注管理";
            button14.UseVisualStyleBackColor = true;
            // 
            // button13
            // 
            button13.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            button13.ForeColor = Color.Blue;
            button13.Location = new Point(308, 295);
            button13.Margin = new Padding(3, 2, 3, 2);
            button13.Name = "button13";
            button13.Size = new Size(182, 36);
            button13.TabIndex = 10039;
            button13.TabStop = false;
            button13.Text = "受注管理";
            button13.UseVisualStyleBackColor = true;
            // 
            // button12
            // 
            button12.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            button12.ForeColor = Color.Blue;
            button12.Location = new Point(300, 287);
            button12.Margin = new Padding(3, 2, 3, 2);
            button12.Name = "button12";
            button12.Size = new Size(182, 36);
            button12.TabIndex = 10038;
            button12.TabStop = false;
            button12.Text = "受注管理";
            button12.UseVisualStyleBackColor = true;
            // 
            // button11
            // 
            button11.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            button11.ForeColor = Color.Blue;
            button11.Location = new Point(292, 279);
            button11.Margin = new Padding(3, 2, 3, 2);
            button11.Name = "button11";
            button11.Size = new Size(182, 36);
            button11.TabIndex = 10037;
            button11.TabStop = false;
            button11.Text = "受注管理";
            button11.UseVisualStyleBackColor = true;
            // 
            // button10
            // 
            button10.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            button10.ForeColor = Color.Blue;
            button10.Location = new Point(284, 271);
            button10.Margin = new Padding(3, 2, 3, 2);
            button10.Name = "button10";
            button10.Size = new Size(182, 36);
            button10.TabIndex = 10036;
            button10.TabStop = false;
            button10.Text = "受注管理";
            button10.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            button9.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            button9.ForeColor = Color.Blue;
            button9.Location = new Point(276, 263);
            button9.Margin = new Padding(3, 2, 3, 2);
            button9.Name = "button9";
            button9.Size = new Size(182, 36);
            button9.TabIndex = 10035;
            button9.TabStop = false;
            button9.Text = "受注管理";
            button9.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            button8.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            button8.ForeColor = Color.Blue;
            button8.Location = new Point(268, 255);
            button8.Margin = new Padding(3, 2, 3, 2);
            button8.Name = "button8";
            button8.Size = new Size(182, 36);
            button8.TabIndex = 10034;
            button8.TabStop = false;
            button8.Text = "受注管理";
            button8.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            button7.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            button7.ForeColor = Color.Blue;
            button7.Location = new Point(260, 247);
            button7.Margin = new Padding(3, 2, 3, 2);
            button7.Name = "button7";
            button7.Size = new Size(182, 36);
            button7.TabIndex = 10033;
            button7.TabStop = false;
            button7.Text = "受注管理";
            button7.UseVisualStyleBackColor = true;
            // 
            // 仕入先別買掛一覧表ボタン
            // 
            仕入先別買掛一覧表ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            仕入先別買掛一覧表ボタン.ForeColor = Color.Blue;
            仕入先別買掛一覧表ボタン.Location = new Point(194, 183);
            仕入先別買掛一覧表ボタン.Margin = new Padding(3, 2, 3, 2);
            仕入先別買掛一覧表ボタン.Name = "仕入先別買掛一覧表ボタン";
            仕入先別買掛一覧表ボタン.Size = new Size(182, 36);
            仕入先別買掛一覧表ボタン.TabIndex = 10032;
            仕入先別買掛一覧表ボタン.TabStop = false;
            仕入先別買掛一覧表ボタン.Text = "仕入先別買掛一覧表";
            仕入先別買掛一覧表ボタン.UseVisualStyleBackColor = true;
            仕入先別買掛一覧表ボタン.Click += 仕入先別買掛一覧表ボタン_Click;
            // 
            // 購買買掛一覧表ボタン
            // 
            購買買掛一覧表ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            購買買掛一覧表ボタン.ForeColor = Color.Blue;
            購買買掛一覧表ボタン.Location = new Point(6, 183);
            購買買掛一覧表ボタン.Margin = new Padding(3, 2, 3, 2);
            購買買掛一覧表ボタン.Name = "購買買掛一覧表ボタン";
            購買買掛一覧表ボタン.Size = new Size(182, 36);
            購買買掛一覧表ボタン.TabIndex = 10031;
            購買買掛一覧表ボタン.TabStop = false;
            購買買掛一覧表ボタン.Text = "購買買掛一覧表";
            購買買掛一覧表ボタン.UseVisualStyleBackColor = true;
            購買買掛一覧表ボタン.Click += 購買買掛一覧表ボタン_Click;
            // 
            // 部品納期管理ボタン
            // 
            部品納期管理ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            部品納期管理ボタン.ForeColor = Color.Blue;
            部品納期管理ボタン.Location = new Point(194, 138);
            部品納期管理ボタン.Margin = new Padding(3, 2, 3, 2);
            部品納期管理ボタン.Name = "部品納期管理ボタン";
            部品納期管理ボタン.Size = new Size(182, 36);
            部品納期管理ボタン.TabIndex = 10030;
            部品納期管理ボタン.TabStop = false;
            部品納期管理ボタン.Text = "部品納期管理";
            部品納期管理ボタン.UseVisualStyleBackColor = true;
            部品納期管理ボタン.Click += 部品納期管理ボタン_Click;
            // 
            // 入庫完了ボタン
            // 
            入庫完了ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            入庫完了ボタン.ForeColor = Color.Black;
            入庫完了ボタン.Location = new Point(382, 98);
            入庫完了ボタン.Margin = new Padding(3, 2, 3, 2);
            入庫完了ボタン.Name = "入庫完了ボタン";
            入庫完了ボタン.Size = new Size(182, 36);
            入庫完了ボタン.TabIndex = 10029;
            入庫完了ボタン.TabStop = false;
            入庫完了ボタン.Text = "入庫完了";
            入庫完了ボタン.UseVisualStyleBackColor = true;
            入庫完了ボタン.Click += 入庫完了ボタン_Click;
            // 
            // 入庫管理ボタン
            // 
            入庫管理ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            入庫管理ボタン.ForeColor = Color.Blue;
            入庫管理ボタン.Location = new Point(194, 98);
            入庫管理ボタン.Margin = new Padding(3, 2, 3, 2);
            入庫管理ボタン.Name = "入庫管理ボタン";
            入庫管理ボタン.Size = new Size(182, 36);
            入庫管理ボタン.TabIndex = 10028;
            入庫管理ボタン.TabStop = false;
            入庫管理ボタン.Text = "入庫管理";
            入庫管理ボタン.UseVisualStyleBackColor = true;
            入庫管理ボタン.Click += 入庫管理ボタン_Click;
            // 
            // 入庫入力ボタン
            // 
            入庫入力ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            入庫入力ボタン.ForeColor = Color.Red;
            入庫入力ボタン.Location = new Point(6, 98);
            入庫入力ボタン.Margin = new Padding(3, 2, 3, 2);
            入庫入力ボタン.Name = "入庫入力ボタン";
            入庫入力ボタン.Size = new Size(182, 36);
            入庫入力ボタン.TabIndex = 10027;
            入庫入力ボタン.TabStop = false;
            入庫入力ボタン.Text = "入庫入力";
            入庫入力ボタン.UseVisualStyleBackColor = true;
            入庫入力ボタン.Click += 入庫入力ボタン_Click;
            // 
            // label33
            // 
            label33.AllowDrop = true;
            label33.AutoEllipsis = true;
            label33.Font = new Font("BIZ UDPゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label33.ForeColor = SystemColors.ActiveCaptionText;
            label33.ImageAlign = ContentAlignment.MiddleLeft;
            label33.Location = new Point(29, 594);
            label33.Margin = new Padding(0);
            label33.Name = "label33";
            label33.Size = new Size(97, 23);
            label33.TabIndex = 10026;
            label33.Text = "相殺有無";
            label33.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 技術
            // 
            技術.Location = new Point(4, 24);
            技術.Name = "技術";
            技術.Padding = new Padding(3);
            技術.Size = new Size(607, 434);
            技術.TabIndex = 2;
            技術.Text = "技術";
            技術.UseVisualStyleBackColor = true;
            // 
            // ログインユーザー名
            // 
            ログインユーザー名.BackColor = Color.Black;
            ログインユーザー名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            ログインユーザー名.ForeColor = Color.White;
            ログインユーザー名.Location = new Point(212, 512);
            ログインユーザー名.Margin = new Padding(3, 2, 3, 2);
            ログインユーザー名.Multiline = true;
            ログインユーザー名.Name = "ログインユーザー名";
            ログインユーザー名.ReadOnly = true;
            ログインユーザー名.Size = new Size(188, 23);
            ログインユーザー名.TabIndex = 181;
            // 
            // ログインボタン
            // 
            ログインボタン.Font = new Font("Yu Gothic UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            ログインボタン.ForeColor = Color.Black;
            ログインボタン.Location = new Point(406, 518);
            ログインボタン.Name = "ログインボタン";
            ログインボタン.Size = new Size(93, 36);
            ログインボタン.TabIndex = 10003;
            ログインボタン.Text = "ログアウト";
            ログインボタン.UseVisualStyleBackColor = true;
            ログインボタン.Click += ログインボタン_Click;
            // 
            // 日付
            // 
            日付.BackColor = Color.Black;
            日付.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            日付.ForeColor = Color.White;
            日付.Location = new Point(212, 539);
            日付.Margin = new Padding(3, 2, 3, 2);
            日付.Multiline = true;
            日付.Name = "日付";
            日付.ReadOnly = true;
            日付.Size = new Size(188, 23);
            日付.TabIndex = 10004;
            // 
            // label23
            // 
            label23.AllowDrop = true;
            label23.AutoEllipsis = true;
            label23.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label23.ForeColor = SystemColors.ActiveCaptionText;
            label23.ImageAlign = ContentAlignment.MiddleLeft;
            label23.Location = new Point(334, 25);
            label23.Margin = new Padding(0);
            label23.Name = "label23";
            label23.Size = new Size(135, 21);
            label23.TabIndex = 10007;
            label23.Text = "UINICS CO.,LTD.";
            label23.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ユーアイホームボタン
            // 
            ユーアイホームボタン.Font = new Font("Yu Gothic UI Light", 11.25F, FontStyle.Underline, GraphicsUnit.Point);
            ユーアイホームボタン.ForeColor = Color.Blue;
            ユーアイホームボタン.Location = new Point(14, 519);
            ユーアイホームボタン.Margin = new Padding(3, 2, 3, 2);
            ユーアイホームボタン.Name = "ユーアイホームボタン";
            ユーアイホームボタン.Size = new Size(93, 36);
            ユーアイホームボタン.TabIndex = 10008;
            ユーアイホームボタン.TabStop = false;
            ユーアイホームボタン.Text = "UI Home";
            ユーアイホームボタン.UseVisualStyleBackColor = true;
            ユーアイホームボタン.Click += ユーアイホームボタン_Click;
            // 
            // システム設定ボタン
            // 
            システム設定ボタン.Font = new Font("BIZ UDPゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            システム設定ボタン.ForeColor = Color.Red;
            システム設定ボタン.Location = new Point(113, 519);
            システム設定ボタン.Margin = new Padding(3, 2, 3, 2);
            システム設定ボタン.Name = "システム設定ボタン";
            システム設定ボタン.Size = new Size(93, 36);
            システム設定ボタン.TabIndex = 10009;
            システム設定ボタン.TabStop = false;
            システム設定ボタン.Text = "システム設定";
            システム設定ボタン.UseVisualStyleBackColor = true;
            システム設定ボタン.Click += システム設定ボタン_Click;
            // 
            // F_メイン
            // 
            BackColor = SystemColors.Control;
            ClientSize = new Size(627, 594);
            Controls.Add(システム設定ボタン);
            Controls.Add(ユーアイホームボタン);
            Controls.Add(label23);
            Controls.Add(日付);
            Controls.Add(ログインボタン);
            Controls.Add(tabControl1);
            Controls.Add(statusStrip1);
            Controls.Add(コマンド終了);
            Controls.Add(ログインユーザー名);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Name = "F_メイン";
            Text = " メイン";
            FormClosing += Form_Unload;
            Load += Form_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            tabControl1.ResumeLayout(false);
            販売.ResumeLayout(false);
            製造.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CalendarColumn calendarColumn1;
        private CalendarColumn calendarColumn2;
        private CalendarColumn calendarColumn3;
        private System.Windows.Forms.Button コマンド終了;


        private CalendarColumn 登録日;
        private System.Windows.Forms.Panel panel1;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private TabControl tabControl1;
        private TabPage 製造;
        private Label label33;
        private TextBox Contact3PhoneNumber;
        private TextBox Contact2PhoneNumber;
        private TabPage 販売;
        private TextBox ActiveDate;
        private TextBox 無効日時;
        private TextBox 無効者名;
        private TextBox 無効者コード;
        private TextBox 更新者名;
        private TextBox 更新者コード;
        private TextBox 更新日時;
        private TextBox 作成者名;
        private TextBox 作成者コード;
        private TextBox ログインユーザー名;
        private Button ログインボタン;
        private TextBox 日付;
        private Button 営業部部長不在ボタン;
        private ToolTip toolTip1;
        private Button 不在イメージコマンド;
        private Button 在席イメージコマンド;
        private Button 受注入力ボタン;
        private TabPage 技術;
        private Label label23;
        private Button 受注管理ボタン;
        private Button 承認管理ボタン;
        private Button 見積入力ボタン;
        private Button 見積管理ボタン;
        private Button 商品構成ボタン;
        private Button 購買申請入力ボタン;
        private Button 購買申請管理ボタン;
        private Button 商品登録ボタン;
        private Button 依頼主登録;
        private Button RunShippingListButton;
        private Button 依頼主管理ボタン;
        private Button 顧客管理ボタン;
        private Button 顧客登録ボタン;
        private Button シリーズ登録ボタン;
        private Button 商品管理ボタン;
        private Button 出荷管理ボタン;
        private Button シリーズ在庫参照ボタン;
        private Button 売上分析ボタン;
        private Button 売上計画ボタン;
        private Button ユーアイホームボタン;
        private Button システム設定ボタン;
        private Button ProductionSystemButton;
        private Button 発注入力ボタン;
        private Button 発注管理ボタン;
        private Button button16;
        private Button button15;
        private Button button14;
        private Button button13;
        private Button button12;
        private Button button11;
        private Button button10;
        private Button button9;
        private Button button8;
        private Button button7;
        private Button 仕入先別買掛一覧表ボタン;
        private Button 購買買掛一覧表ボタン;
        private Button 部品納期管理ボタン;
        private Button 入庫完了ボタン;
        private Button 入庫管理ボタン;
        private Button 入庫入力ボタン;
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using u_net;

namespace MultiRowDesigner
{
    public partial class 発注明細 : UserControl
    {
        public GcMultiRow Detail
        {
            get
            {
                return gcMultiRow1;
            }
        }

        //親フォームへの参照を保存
        private F_発注 f_発注;

        public 発注明細(F_発注 f_発注)
        {
            this.f_発注 = f_発注;

            InitializeComponent();
        }

        private void gcMultiRow1_CellContentClick(object sender, CellEventArgs e)
        {

            switch(gcMultiRow1.CurrentCell)
            {
                //ボタンClick時の処理
                case ButtonCell:
                    switch (e.CellName)
                    {
                        case "明細削除ボタン":
                            MessageBox.Show("削除します。", "削除", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            break;
                        default:
                            break;
                    }
                    break;

                default:
                    break;
            }

        }

        private void gcMultiRow1_CellEnter(object sender, CellEventArgs e)
        {

            switch(gcMultiRow1.CurrentCell)
            {
                //テキストボックスEnter時の処理
                case TextBoxCell:
                    switch (e.CellName)
                    {
                        case "部品コード":
                            this.f_発注.toolStripStatusLabel1.Text = "■部品コードを8文字以内で入力します。　■ダブルクリックするか、[space]キーを押して部品選択ウィンドウを表示します。　■マウスの左ボタンを押しながら右ボタンをクリックすると入力履歴を表示します。";
                            break;
                        case "品名":
                            this.f_発注.toolStripStatusLabel1.Text = "■全角２５文字まで入力できます。";
                            break;
                        case "型番":
                            this.f_発注.toolStripStatusLabel1.Text = "■半角５０文字まで入力できます。";
                            break;
                        case "メーカー名":
                            this.f_発注.toolStripStatusLabel1.Text = "■全角２５文字まで入力できます。";
                            break;
                        case "入数":
                            this.f_発注.toolStripStatusLabel1.Text = "■部品の現在の入数です。　■修正は出来ません。";
                            break;
                        case "発注納期":
                            this.f_発注.toolStripStatusLabel1.Text = "■ダブルクリックするか、[space]キーを押してカレンダーを開くことができます。";
                            break;
                        case "必要数量":
                            this.f_発注.toolStripStatusLabel1.Text = "■実際に必要な数量を入力します。";
                            break;
                        case "発注数量":
                            this.f_発注.toolStripStatusLabel1.Text = "■仕入先に発注する数量を入力します。　■在庫管理する場合は自動的に計算されます。";
                            break;
                        case "発注単価":
                            this.f_発注.toolStripStatusLabel1.Text = "";
                            break;
                        case "回答納期":
                            this.f_発注.toolStripStatusLabel1.Text = "■ダブルクリックするか、[space]キーを押してカレンダーを開くことができます。";
                            break;
                        case "買掛区分":
                            this.f_発注.toolStripStatusLabel1.Text = "■買掛区分を選択します。　■確定後入力するには入力欄をダブルクリックしてください。";
                            break;
                        default:
                            this.f_発注.toolStripStatusLabel1.Text = "各種項目の説明";
                            break;
                    }
                    break;

                default:
                    break;
            }

        }
    }
}

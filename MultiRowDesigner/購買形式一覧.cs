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

namespace MultiRowDesigner
{
    public partial class 購買形式一覧 : UserControl
    {
        public GcMultiRow Detail
        {
            get
            {
                return gcMultiRow1;
            }
        }

        public 購買形式一覧()
        {
            InitializeComponent();
        }

        private void gcMultiRow1_CellEnter(object sender, CellEventArgs e)
        {
            u_net.F_購買 objForm = (u_net.F_購買)Application.OpenForms["F_購買"];

            if (objForm != null)
            {
                switch (gcMultiRow1.CurrentCell)
                {
                    //テキストボックスEnter時の処理
                    case TextBoxCell:
                        switch (e.CellName)
                        {
                            case "明細削除ボタン":
                                objForm.toolStripStatusLabel1.Text = "■明細行の削除または取消を行います。";
                                break;
                            case "構成番号":
                                objForm.toolStripStatusLabel1.Text = "■半角１０文字まで入力できます。　■ユニット内での構成番号の重複はできません。";
                                break;
                            case "部品コード":
                                objForm.toolStripStatusLabel1.Text = "■部品コードを入力するか、[space]キー押下またはダブルクリックで部品選択ウィンドウを表示します。";
                                break;
                            case "型番":
                                objForm.toolStripStatusLabel1.Text = "■半角５０文字まで入力できます。";
                                break;
                            case "メーカー名":
                                objForm.toolStripStatusLabel1.Text = "■全角２５文字まで入力できます。";
                                break;
                            case "入数":
                                objForm.toolStripStatusLabel1.Text = "■部品の現在の入数です。　■修正は出来ません。";
                                break;
                            case "発注納期":
                                objForm.toolStripStatusLabel1.Text = "■ダブルクリックするか、[space]キーを押してカレンダーを開くことができます。";
                                break;
                            case "必要数量":
                                objForm.toolStripStatusLabel1.Text = "■実際に必要な数量を入力します。";
                                break;
                            case "発注数量":
                                objForm.toolStripStatusLabel1.Text = "■仕入先に発注する数量を入力します。　■在庫管理する場合は自動的に計算されます。";
                                break;
                            case "発注単価":
                                objForm.toolStripStatusLabel1.Text = "";
                                break;
                            case "回答納期":
                                objForm.toolStripStatusLabel1.Text = "■ダブルクリックするか、[space]キーを押してカレンダーを開くことができます。";
                                break;
                            case "買掛区分":
                                objForm.toolStripStatusLabel1.Text = "■買掛区分を選択します。　■確定後入力するには入力欄をダブルクリックしてください。";
                                break;
                            default:
                                objForm.toolStripStatusLabel1.Text = "各種項目の説明";
                                break;
                        }
                        break;

                    case ComboBoxCell:
                        switch (e.CellName)
                        {
                            case "変更操作コード":
                                objForm.toolStripStatusLabel1.Text = "■改版時に入力します。変更操作を選択してください。";
                                break;
                            default:
                                break;

                        }
                        break;

                    default:
                        break;
                }
            }

        }

    }
}

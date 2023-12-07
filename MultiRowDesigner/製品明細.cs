﻿using System;
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
    public partial class 製品明細 : UserControl
    {
        public GcMultiRow Detail
        {
            get
            {
                return gcMultiRow1;
            }
        }

        public 製品明細()
        {
            InitializeComponent();
        }

        private void gcMultiRow1_CellContentClick(object sender, CellEventArgs e)
        {

            switch (gcMultiRow1.CurrentCell)
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
            u_net.F_製品 objForm = (u_net.F_製品)Application.OpenForms["F_製品"];

            switch (gcMultiRow1.CurrentCell)
            {
                //テキストボックスEnter時の処理
                case TextBoxCell:
                    switch (e.CellName)
                    {
                        case "型式名":
                            objForm.toolStripStatusLabel1.Text = "■製品の型式名を入力します。　■半角１６文字まで入力できます。　■型式は購買時の最小単位となります。";
                            break;

                        case "ユニットコード":
                            objForm.toolStripStatusLabel1.Text = "";
                            break;

                        case "ユニット版数":
                            objForm.toolStripStatusLabel1.Text = "";
                            break;

                        case "改版中":
                            objForm.toolStripStatusLabel1.Text = "";
                            break;

                        case "品名":
                            objForm.toolStripStatusLabel1.Text = "";
                            break;

                        case "型番":
                            objForm.toolStripStatusLabel1.Text = "";
                            break;

                        case "RohsStatusSign":
                            objForm.toolStripStatusLabel1.Text = "";
                            break;

                        case "非含有証明書":
                            objForm.toolStripStatusLabel1.Text = "";
                            break;

                        case "ユニット材料費":
                            objForm.toolStripStatusLabel1.Text = "";
                            break;

                        case "変更内容":
                            objForm.toolStripStatusLabel1.Text = "■全角文字で30文字まで入力できます。";
                            break;
                        
                        default:
                            objForm.toolStripStatusLabel1.Text = "各種項目の説明";
                            break;
                    }
                    break;

                //コンボボックスEnter時の処理
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

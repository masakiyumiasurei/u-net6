using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace u_net
{
    //汎用フォーム　検索コード　に呼び元のフォームオブジェクトを渡すため
    public partial class MidForm : Form
    {
        public MidForm()
        {
            InitializeComponent();
        }
        public virtual void SearchCode(string serchcode)
        {
        }
    }
}

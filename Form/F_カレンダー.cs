using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using u_net.Public;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.Drawing.Imaging;
using System.Drawing.Printing;


namespace u_net
{
    public partial class F_カレンダー : Form
    {
        private DateTime myDate = DateTime.Now.Date;
        private string myYear = DateTime.Now.Year.ToString();
        private string myMonth = DateTime.Now.Month.ToString();
        private string myDay = DateTime.Now.Day.ToString();
        private object? cmdSelectedDay;
        public event EventHandler<DateTime> Selected;
        public string SelectedDate { get; private set; }

        public F_カレンダー()
        {

            this.Text = "カレンダー";       // ウィンドウタイトルを設定
            this.MaximizeBox = false;  // 最大化ボタンを無効化
            this.MinimizeBox = false; //最小化ボタンを無効化

            InitializeComponent();
        }

        private void Form_Open(object sender, EventArgs e)
        {


            DateToElements();

        }

        private void DateToElements()
        {
            
            myYear = myDate.Year.ToString();
            myMonth = myDate.Month.ToString(); ;
            myDay = myDate.Day.ToString(); ;
            int month;
            int.TryParse(myMonth, out month);

            txtYear.Text = myYear;
            cboMonth.Text = month.ToString() + "月"; // コンボボックスのインデックスは0から始まるため、月の値から1を引きます

            DrawDateButtons();
        }


        private void Done(DateTime SelectDate)
        {
            SelectedDate = SelectDate.ToString();

            // ダイアログをOKで閉じる
            DialogResult = DialogResult.OK;
            Close();
        }

        private void DateClick(object sender)
        {
            Button clickedButton = (Button)sender;
            myDay = clickedButton.Text;
            ElementsToDate();
            OnSelected(myDate);
            Done(myDate);

        }

        private void ElementsToDate()
        {
            myDate = DateTime.Parse(myYear + '/' + myMonth + '/' + myDay);

            DrawDateButtons();
        }

        private void DrawDateButtons()
        {
            DateTime MonthDayOne;
            int MonthLength, DayOfWeek;
            int i, Y, X;
            Button btn;
            int year, month,day;
            int.TryParse(myYear, out year);
            int.TryParse(myMonth, out month);
            int.TryParse(myDay, out day);

            MonthDayOne = new DateTime(year, month, 1);
            DayOfWeek = (int)MonthDayOne.DayOfWeek; // DayOfWeek is an enum, so cast it to int
            MonthLength = DateTime.DaysInMonth(year, month);

            cmdSelectedDay = null;
            i = 1 - DayOfWeek;
            for (Y = 0; Y <= 5; Y++)
            {
                for (X = 0; X <= 6; X++)
                {
                    btn = (Button)this.Controls["d" + Y + X];
                    if (i >= 1 && i <= MonthLength)
                    {
                        btn.Text = i.ToString();
                        btn.Tag = i;
                        btn.Visible = true;
                    }
                    else
                    {
                        btn.Visible = false;
                    }
                
                    if (i == day)
                    {
                        cmdSelectedDay = btn;
                    }
                    i++;
                }
            }
        }


        protected virtual void OnSelected(DateTime selectedDate)
        {
            Selected?.Invoke(this, selectedDate);
        }




        private void d00_Click(object sender, EventArgs e)
        {
            DateClick(sender);
        }

        private void d01_Click(object sender, EventArgs e)
        {
            DateClick(sender);
        }

        private void d02_Click(object sender, EventArgs e)
        {
            DateClick(sender);
        }

        private void d03_Click(object sender, EventArgs e)
        {
            DateClick(sender);
        }

        private void d04_Click(object sender, EventArgs e)
        {
            DateClick(sender);
        }

        private void d05_Click(object sender, EventArgs e)
        {
            DateClick(sender);
        }

        private void d06_Click(object sender, EventArgs e)
        {
            DateClick(sender);
        }

        private void d10_Click(object sender, EventArgs e)
        {
            DateClick(sender);
        }

        private void d11_Click(object sender, EventArgs e)
        {
            DateClick(sender);
        }

        private void d12_Click(object sender, EventArgs e)
        {
            DateClick(sender);
        }

        private void d13_Click(object sender, EventArgs e)
        {
            DateClick(sender);
        }

        private void d14_Click(object sender, EventArgs e)
        {
            DateClick(sender);
        }

        private void d15_Click(object sender, EventArgs e)
        {
            DateClick(sender);
        }

        private void d16_Click(object sender, EventArgs e)
        {
            DateClick(sender);
        }

        private void d20_Click(object sender, EventArgs e)
        {
            DateClick(sender);
        }

        private void d21_Click(object sender, EventArgs e)
        {
            DateClick(sender);
        }

        private void d22_Click(object sender, EventArgs e)
        {
            DateClick(sender);
        }

        private void d23_Click(object sender, EventArgs e)
        {
            DateClick(sender);
        }

        private void d24_Click(object sender, EventArgs e)
        {
            DateClick(sender);
        }

        private void d25_Click(object sender, EventArgs e)
        {
            DateClick(sender);
        }

        private void d26_Click(object sender, EventArgs e)
        {
            DateClick(sender);
        }

        private void d30_Click(object sender, EventArgs e)
        {
            DateClick(sender);
        }

        private void d31_Click(object sender, EventArgs e)
        {
            DateClick(sender);
        }

        private void d32_Click(object sender, EventArgs e)
        {
            DateClick(sender);
        }

        private void d33_Click(object sender, EventArgs e)
        {
            DateClick(sender);
        }

        private void d34_Click(object sender, EventArgs e)
        {
            DateClick(sender);
        }

        private void d35_Click(object sender, EventArgs e)
        {
            DateClick(sender);
        }

        private void d36_Click(object sender, EventArgs e)
        {
            DateClick(sender);
        }

        private void d40_Click(object sender, EventArgs e)
        {
            DateClick(sender);
        }

        private void d41_Click(object sender, EventArgs e)
        {
            DateClick(sender);
        }

        private void d42_Click(object sender, EventArgs e)
        {
            DateClick(sender);
        }

        private void d43_Click(object sender, EventArgs e)
        {
            DateClick(sender);
        }

        private void d44_Click(object sender, EventArgs e)
        {
            DateClick(sender);
        }

        private void d45_Click(object sender, EventArgs e)
        {
            DateClick(sender);
        }

        private void d46_Click(object sender, EventArgs e)
        {
            DateClick(sender);
        }

        private void d50_Click(object sender, EventArgs e)
        {
            DateClick(sender);
        }

        private void d51_Click(object sender, EventArgs e)
        {
            DateClick(sender);
        }

        private void d52_Click(object sender, EventArgs e)
        {
            DateClick(sender);
        }

        private void d53_Click(object sender, EventArgs e)
        {
            DateClick(sender);
        }

        private void d54_Click(object sender, EventArgs e)
        {
            DateClick(sender);
        }

        private void d55_Click(object sender, EventArgs e)
        {
            DateClick(sender);
        }

        private void d56_Click(object sender, EventArgs e)
        {
            DateClick(sender);
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {

        }

        private void SetTodayButton_Click(object sender, EventArgs e)
        {

        }

        private void cmdPrevYear_Click(object sender, EventArgs e)
        {

        }

        private void cmdNextYear_Click(object sender, EventArgs e)
        {

        }

        private void cmdPrevMonth_Click(object sender, EventArgs e)
        {

        }

        private void txtYear_Validated(object sender, EventArgs e)
        {

        }

        private void cboMonth_Validated(object sender, EventArgs e)
        {

        }

        private void cmdNextMonth_Click(object sender, EventArgs e)
        {

        }
    }
}

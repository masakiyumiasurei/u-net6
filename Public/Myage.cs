using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace u_net
{
    public class Myage
    {
        public static int CalculateAge(DateTime dateOfBirth)
        {
            DateTime currentDate = DateTime.Today;
            int age = currentDate.Year - dateOfBirth.Year;

            //生年月日が今日の日付移行の場合、年齢が１つ少なくなる
            if (currentDate < dateOfBirth.AddYears(age))
            {
                age--;
            }

            return age;
        }
    }
}
